using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Dynamic.Units;
using O9K.AIO.Modes.Combo;
using O9K.AIO.Modes.MoveCombo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Heroes;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Entity.Monitors;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using SharpDX;

namespace O9K.AIO.UnitManager
{
	// Token: 0x02000007 RID: 7
	internal class UnitManager : IDisposable
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00007A3C File Offset: 0x00005C3C
		public UnitManager(BaseHero baseHero)
		{
			foreach (Type type in from x in Assembly.GetExecutingAssembly().GetTypes()
			where !x.IsAbstract && x.IsClass && typeof(ControllableUnit).IsAssignableFrom(x)
			select x)
			{
				foreach (UnitNameAttribute unitNameAttribute in type.GetCustomAttributes<UnitNameAttribute>())
				{
					this.unitTypes.Add(unitNameAttribute.Name, type);
				}
			}
			this.BaseHero = baseHero;
			this.targetManager = baseHero.TargetManager;
			this.owner = baseHero.Owner;
			this.abilitySleeper = baseHero.AbilitySleeper;
			this.orbwalkSleeper = baseHero.OrbwalkSleeper;
			this.Menu = new Menu("Units");
			MenuSwitcher menuSwitcher = new MenuSwitcher("Control allies", "controlAllies", false, false);
			menuSwitcher.SetTooltip("Control disconnected/shared allies");
			this.Menu.Add<MenuSwitcher>(menuSwitcher);
			menuSwitcher.ValueChange += this.ControlAlliesOnValueChanged;
			baseHero.Menu.GeneralSettingsMenu.Add<Menu>(this.Menu);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000020F8 File Offset: 0x000002F8
		public BaseHero BaseHero { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002100 File Offset: 0x00000300
		public Menu Menu { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002108 File Offset: 0x00000308
		public IEnumerable<ControllableUnit> ControllableUnits
		{
			get
			{
				return from x in this.controllableUnits
				where x.IsValid && x.CanBeControlled && x.ShouldControl
				select x;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002134 File Offset: 0x00000334
		public IEnumerable<ControllableUnit> AllControllableUnits
		{
			get
			{
				return from x in this.controllableUnits
				where x.IsValid
				select x;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00007BFC File Offset: 0x00005DFC
		public void Disable()
		{
			EntityManager9.UnitAdded -= new EntityManager9.EventHandler<Unit9>(this.OnUnitAdded);
			EntityManager9.UnitRemoved -= new EntityManager9.EventHandler<Unit9>(this.OnUnitRemoved);
			EntityManager9.AbilityAdded -= new EntityManager9.EventHandler<Ability9>(this.OnAbilityAdded);
			EntityManager9.AbilityRemoved -= new EntityManager9.EventHandler<Ability9>(this.OnAbilityRemoved);
			EntityManager9.UnitMonitor.AttackStart -= new UnitMonitor.EventHandler(this.OnAttackStart);
			ObjectManager.OnAddTrackingProjectile -= this.OnAddTrackingProjectile;
			foreach (IDisposable disposable in this.controllableUnits.OfType<IDisposable>())
			{
				disposable.Dispose();
			}
			this.controllableUnits.Clear();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00007BFC File Offset: 0x00005DFC
		public void Dispose()
		{
			EntityManager9.UnitAdded -= new EntityManager9.EventHandler<Unit9>(this.OnUnitAdded);
			EntityManager9.UnitRemoved -= new EntityManager9.EventHandler<Unit9>(this.OnUnitRemoved);
			EntityManager9.AbilityAdded -= new EntityManager9.EventHandler<Ability9>(this.OnAbilityAdded);
			EntityManager9.AbilityRemoved -= new EntityManager9.EventHandler<Ability9>(this.OnAbilityRemoved);
			EntityManager9.UnitMonitor.AttackStart -= new UnitMonitor.EventHandler(this.OnAttackStart);
			ObjectManager.OnAddTrackingProjectile -= this.OnAddTrackingProjectile;
			foreach (IDisposable disposable in this.controllableUnits.OfType<IDisposable>())
			{
				disposable.Dispose();
			}
			this.controllableUnits.Clear();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00007CC4 File Offset: 0x00005EC4
		public void Enable()
		{
			EntityManager9.UnitAdded += new EntityManager9.EventHandler<Unit9>(this.OnUnitAdded);
			EntityManager9.UnitRemoved += new EntityManager9.EventHandler<Unit9>(this.OnUnitRemoved);
			EntityManager9.AbilityAdded += new EntityManager9.EventHandler<Ability9>(this.OnAbilityAdded);
			EntityManager9.AbilityRemoved += new EntityManager9.EventHandler<Ability9>(this.OnAbilityRemoved);
			EntityManager9.UnitMonitor.AttackStart += new UnitMonitor.EventHandler(this.OnAttackStart);
			ObjectManager.OnAddTrackingProjectile += this.OnAddTrackingProjectile;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00007D40 File Offset: 0x00005F40
		public void EndCombo(ComboModeMenu comboModeMenu)
		{
			foreach (ControllableUnit controllableUnit in from x in this.controllableUnits
			where x.IsValid
			select x)
			{
				controllableUnit.EndCombo(this.targetManager, comboModeMenu);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00007DB8 File Offset: 0x00005FB8
		public virtual void ExecuteCombo(ComboModeMenu comboModeMenu)
		{
			foreach (ControllableUnit controllableUnit in this.ControllableUnits)
			{
				if (!controllableUnit.ComboSleeper.IsSleeping)
				{
					if (!comboModeMenu.IgnoreInvisibility && controllableUnit.IsInvisible)
					{
						break;
					}
					if (controllableUnit.Combo(this.targetManager, comboModeMenu))
					{
						controllableUnit.LastMovePosition = Vector3.Zero;
					}
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00007E3C File Offset: 0x0000603C
		public void ExecuteMoveCombo(MoveComboModeMenu comboModeMenu)
		{
			foreach (ControllableUnit controllableUnit in this.ControllableUnits)
			{
				if (!controllableUnit.ComboSleeper.IsSleeping && controllableUnit.MoveCombo(this.targetManager, comboModeMenu))
				{
					controllableUnit.LastMovePosition = Vector3.Zero;
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00007EAC File Offset: 0x000060AC
		public void Move()
		{
			if (this.issuedAction.IsSleeping)
			{
				return;
			}
			List<ControllableUnit> list = new List<ControllableUnit>();
			foreach (ControllableUnit controllableUnit in from x in this.ControllableUnits
			orderby this.IssuedActionTime(x.Handle)
			select x)
			{
				if (!controllableUnit.OrbwalkEnabled)
				{
					list.Add(controllableUnit);
				}
				else if (!this.unitIssuedAction.IsSleeping(controllableUnit.Handle) && controllableUnit.Orbwalk(null, false, true, null))
				{
					this.issuedActionTimings[controllableUnit.Handle] = Game.RawGameTime;
					this.unitIssuedAction.Sleep(controllableUnit.Handle, 0.1f);
					this.issuedAction.Sleep(0.03f);
					return;
				}
			}
			if (list.Count > 0 && !this.unitIssuedAction.IsSleeping(4294967295u))
			{
				Player.EntitiesMove(from x in list
				where x.Owner.CanMove(true)
				select x.Owner.BaseUnit, Game.MousePosition);
				this.unitIssuedAction.Sleep(uint.MaxValue, 0.25f);
				this.issuedAction.Sleep(0.03f);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00008018 File Offset: 0x00006218
		public void Orbwalk(ControllableUnit controllable, bool attack, bool move)
		{
			if (this.issuedAction.IsSleeping)
			{
				return;
			}
			if (this.unitIssuedAction.IsSleeping(controllable.Handle))
			{
				return;
			}
			if (!controllable.Orbwalk(this.targetManager.Target, attack, move, null))
			{
				return;
			}
			this.issuedActionTimings[controllable.Handle] = Game.RawGameTime;
			this.unitIssuedAction.Sleep(controllable.Handle, 0.1f);
			this.issuedAction.Sleep(0.05f);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000809C File Offset: 0x0000629C
		public virtual void Orbwalk(ComboModeMenu comboModeMenu)
		{
			if (this.issuedAction.IsSleeping)
			{
				return;
			}
			List<ControllableUnit> list = (from x in this.ControllableUnits
			orderby this.IssuedActionTime(x.Handle)
			select x).ToList<ControllableUnit>();
			if (this.BodyBlock(list, comboModeMenu))
			{
				this.issuedAction.Sleep(0.05f);
				return;
			}
			List<ControllableUnit> list2 = new List<ControllableUnit>();
			foreach (ControllableUnit controllableUnit in list)
			{
				if (!controllableUnit.OrbwalkEnabled)
				{
					list2.Add(controllableUnit);
				}
				else if (!this.unitIssuedAction.IsSleeping(controllableUnit.Handle) && controllableUnit.Orbwalk(this.targetManager.Target, comboModeMenu))
				{
					this.issuedActionTimings[controllableUnit.Handle] = Game.RawGameTime;
					this.unitIssuedAction.Sleep(controllableUnit.Handle, 0.1f);
					this.issuedAction.Sleep(0.05f);
					return;
				}
			}
			if (list2.Count > 0 && !this.unitIssuedAction.IsSleeping(4294967295u))
			{
				this.ControlAllUnits(list2);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000081C8 File Offset: 0x000063C8
		protected bool BodyBlock(ICollection<ControllableUnit> allUnits, ComboModeMenu comboModeMenu)
		{
			if (!this.targetManager.HasValidTarget)
			{
				return false;
			}
			Unit9 target = this.targetManager.Target;
			List<ControllableUnit> list = (from x in this.ControllableUnits
			where x.CanBodyBlock && x.Owner.Distance(target) < 1000f
			orderby x.Owner.Distance(target)
			select x).ToList<ControllableUnit>();
			int num = Math.Min(list.Count, 2);
			if (num != 1)
			{
				if (num == 2)
				{
					UnitManager.<>c__DisplayClass32_1 <>c__DisplayClass32_2 = new UnitManager.<>c__DisplayClass32_1();
					<>c__DisplayClass32_2.blockPositions = new List<Vector3>
					{
						target.InFront(150f, 15f, true),
						target.InFront(150f, -15f, true)
					};
					using (List<Vector3>.Enumerator enumerator = <>c__DisplayClass32_2.blockPositions.ToList<Vector3>().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							UnitManager.<>c__DisplayClass32_2 <>c__DisplayClass32_3 = new UnitManager.<>c__DisplayClass32_2();
							<>c__DisplayClass32_3.CS$<>8__locals1 = <>c__DisplayClass32_2;
							<>c__DisplayClass32_3.blockPosition = enumerator.Current;
							ControllableUnit controllable = (from x in list
							orderby x.Owner.Distance(<>c__DisplayClass32_3.blockPosition)
							select x).First<ControllableUnit>();
							ControllableUnit controllableUnit = list.Find((ControllableUnit x) => !x.Equals(controllable) && <>c__DisplayClass32_3.CS$<>8__locals1.blockPositions.Any((Vector3 z) => !z.Equals(<>c__DisplayClass32_3.blockPosition) && x.Owner.Distance(<>c__DisplayClass32_3.blockPosition) < x.Owner.Distance(z)));
							if (controllableUnit != null)
							{
								controllable = controllableUnit;
							}
							if (this.unitIssuedAction.IsSleeping(controllable.Handle))
							{
								allUnits.Remove(controllable);
								list.Remove(controllable);
								<>c__DisplayClass32_3.CS$<>8__locals1.blockPositions.Remove(<>c__DisplayClass32_3.blockPosition);
							}
							else
							{
								bool? flag = controllable.BodyBlock(this.targetManager, <>c__DisplayClass32_3.blockPosition, comboModeMenu);
								bool? flag2 = flag;
								bool flag3 = true;
								if (flag2 == flag3)
								{
									this.issuedActionTimings[controllable.Handle] = Game.RawGameTime;
									this.unitIssuedAction.Sleep(controllable.Handle, 0.1f);
									return true;
								}
								if (flag == false)
								{
									allUnits.Remove(controllable);
									list.Remove(controllable);
									<>c__DisplayClass32_3.CS$<>8__locals1.blockPositions.Remove(<>c__DisplayClass32_3.blockPosition);
								}
							}
						}
					}
					return false;
				}
			}
			else
			{
				ControllableUnit controllableUnit2 = list[0];
				bool? flag4 = controllableUnit2.BodyBlock(this.targetManager, target.InFront(150f, 0f, true), comboModeMenu);
				bool? flag2 = flag4;
				bool flag3 = true;
				if (flag2 == flag3)
				{
					this.issuedActionTimings[controllableUnit2.Handle] = Game.RawGameTime;
					this.unitIssuedAction.Sleep(controllableUnit2.Handle, 0.1f);
					return true;
				}
				flag2 = flag4;
				flag3 = false;
				if (flag2 == flag3)
				{
					allUnits.Remove(controllableUnit2);
				}
			}
			return false;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00008538 File Offset: 0x00006738
		protected void ControlAllUnits(IEnumerable<ControllableUnit> noOrbwalkUnits)
		{
			if (this.targetManager.HasValidTarget)
			{
				List<Unit> list = (from x in noOrbwalkUnits
				where x.Owner.CanAttack(this.targetManager.Target, (float)(x.CanMove() ? 999999 : 0))
				select x.Owner.BaseUnit).ToList<Unit>();
				if (list.Count > 0)
				{
					Player.EntitiesAttack(list, this.targetManager.Target.BaseUnit);
				}
			}
			else
			{
				List<Unit> list2 = (from x in noOrbwalkUnits
				where x.Owner.CanMove(true)
				select x.Owner.BaseUnit).ToList<Unit>();
				if (list2.Count > 0)
				{
					Player.EntitiesMove(list2, Game.MousePosition);
				}
			}
			this.unitIssuedAction.Sleep(uint.MaxValue, 0.25f);
			this.issuedAction.Sleep(0.03f);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00008638 File Offset: 0x00006838
		protected ControllableUnitMenu GetUnitMenu(Unit9 unit)
		{
			ControllableUnitMenu controllableUnitMenu;
			if (!this.unitMenus.TryGetValue(unit.DefaultName + unit.IsIllusion.ToString(), out controllableUnitMenu))
			{
				controllableUnitMenu = new ControllableUnitMenu(unit, this.Menu);
				this.unitMenus[unit.DefaultName + unit.IsIllusion.ToString()] = controllableUnitMenu;
			}
			return controllableUnitMenu;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000086A0 File Offset: 0x000068A0
		protected float IssuedActionTime(uint handle)
		{
			float result;
			this.issuedActionTimings.TryGetValue(handle, out result);
			return result;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000086C0 File Offset: 0x000068C0
		protected virtual void OnAbilityAdded(Ability9 entity)
		{
			try
			{
				ActiveAbility ability;
				if (entity.IsControllable && entity.Owner.CanUseAbilities && entity.Owner.IsAlly(this.owner) && (ability = (entity as ActiveAbility)) != null)
				{
					ControllableUnit controllableUnit = this.controllableUnits.Find((ControllableUnit x) => x.Handle == entity.Owner.Handle);
					if (controllableUnit != null)
					{
						controllableUnit.AddAbility(ability, this.BaseHero.ComboMenus, this.BaseHero.MoveComboModeMenu);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000877C File Offset: 0x0000697C
		protected virtual void OnAbilityRemoved(Ability9 entity)
		{
			try
			{
				ActiveAbility ability;
				if (entity.IsControllable && entity.Owner.CanUseAbilities && entity.Owner.IsAlly(this.owner) && (ability = (entity as ActiveAbility)) != null)
				{
					ControllableUnit controllableUnit = this.controllableUnits.Find((ControllableUnit x) => x.Handle == entity.Owner.Handle);
					if (controllableUnit != null)
					{
						controllableUnit.RemoveAbility(ability);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00008820 File Offset: 0x00006A20
		private void ControlAlliesOnValueChanged(object sender, SwitcherEventArgs e)
		{
			this.controlAllies = e.NewValue;
			foreach (Unit9 entity in EntityManager9.Units)
			{
				this.OnUnitRemoved(entity);
				this.OnUnitAdded(entity);
			}
			foreach (Ability9 entity2 in EntityManager9.Abilities)
			{
				this.OnAbilityAdded(entity2);
				this.OnAbilityRemoved(entity2);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000088C4 File Offset: 0x00006AC4
		private void OnAddTrackingProjectile(TrackingProjectileEventArgs args)
		{
			try
			{
				Entity source = args.Projectile.Source;
				Entity source2 = source;
				if (source2 != null && source2.IsValid)
				{
					ControllableUnit controllableUnit = this.controllableUnits.Find((ControllableUnit x) => x.Handle == source.Handle);
					if (controllableUnit != null)
					{
						controllableUnit.MoveSleeper.Reset();
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00008944 File Offset: 0x00006B44
		private void OnAttackStart(Unit9 unit)
		{
			try
			{
				if (unit.IsControllable)
				{
					ControllableUnit controllableUnit = this.controllableUnits.Find((ControllableUnit x) => x.Handle == unit.Handle);
					if (controllableUnit != null)
					{
						controllableUnit.OnAttackStart();
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000089AC File Offset: 0x00006BAC
		private void OnUnitAdded(Unit9 entity)
		{
			try
			{
				if (entity.IsControllable && entity.IsAlly(this.owner))
				{
					if (!this.ignoredUnits.Contains(entity.Name) && !this.controllableUnits.Any((ControllableUnit x) => x.Handle == entity.Handle))
					{
						if (!this.controlAllies)
						{
							if (entity.IsHero)
							{
								Entity baseOwner = entity.BaseOwner;
								EntityHandle? entityHandle = (baseOwner != null) ? new EntityHandle?(baseOwner.Handle) : null;
								if (((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) != this.owner.PlayerHandle)
								{
									return;
								}
							}
							else
							{
								Entity baseOwner2 = entity.BaseOwner;
								EntityHandle? entityHandle = (baseOwner2 != null) ? new EntityHandle?(baseOwner2.Handle) : null;
								if (((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) != this.owner.HeroHandle)
								{
									return;
								}
							}
						}
						Type type;
						if (!entity.CanUseAbilities || !this.unitTypes.TryGetValue(entity.Name, out type))
						{
							if (entity.CanUseAbilities)
							{
								DynamicUnit item = new DynamicUnit(entity, this.abilitySleeper, this.orbwalkSleeper[entity.Handle], this.GetUnitMenu(entity), this.BaseHero)
								{
									FailSafe = this.BaseHero.FailSafe
								};
								this.controllableUnits.Add(item);
							}
							else
							{
								ControllableUnit item2 = new ControllableUnit(entity, this.abilitySleeper, this.orbwalkSleeper[entity.Handle], this.GetUnitMenu(entity))
								{
									FailSafe = this.BaseHero.FailSafe
								};
								this.controllableUnits.Add(item2);
							}
						}
						else
						{
							ControllableUnit controllableUnit = (ControllableUnit)Activator.CreateInstance(type, new object[]
							{
								entity,
								this.abilitySleeper,
								this.orbwalkSleeper[entity.Handle],
								this.GetUnitMenu(entity)
							});
							controllableUnit.FailSafe = this.BaseHero.FailSafe;
							this.controllableUnits.Add(controllableUnit);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00008CA8 File Offset: 0x00006EA8
		private void OnUnitRemoved(Unit9 entity)
		{
			try
			{
				if (entity.IsControllable && entity.IsAlly(this.owner) && entity.IsUnit)
				{
					ControllableUnit controllableUnit = this.controllableUnits.Find((ControllableUnit x) => x.Handle == entity.Handle);
					if (controllableUnit != null)
					{
						IDisposable disposable;
						if ((disposable = (controllableUnit as IDisposable)) != null)
						{
							disposable.Dispose();
						}
						this.controllableUnits.Remove(controllableUnit);
						this.issuedActionTimings.Remove(entity.Handle);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x0400000F RID: 15
		protected readonly MultiSleeper abilitySleeper;

		// Token: 0x04000010 RID: 16
		protected readonly List<ControllableUnit> controllableUnits = new List<ControllableUnit>();

		// Token: 0x04000011 RID: 17
		protected readonly HashSet<string> ignoredUnits = new HashSet<string>
		{
			"npc_dota_juggernaut_healing_ward",
			"npc_dota_courier"
		};

		// Token: 0x04000012 RID: 18
		protected readonly Sleeper issuedAction = new Sleeper();

		// Token: 0x04000013 RID: 19
		protected readonly Dictionary<uint, float> issuedActionTimings = new Dictionary<uint, float>();

		// Token: 0x04000014 RID: 20
		protected readonly MultiSleeper orbwalkSleeper;

		// Token: 0x04000015 RID: 21
		protected readonly Owner owner;

		// Token: 0x04000016 RID: 22
		protected readonly TargetManager targetManager;

		// Token: 0x04000017 RID: 23
		protected readonly MultiSleeper unitIssuedAction = new MultiSleeper();

		// Token: 0x04000018 RID: 24
		protected readonly Dictionary<string, ControllableUnitMenu> unitMenus = new Dictionary<string, ControllableUnitMenu>();

		// Token: 0x04000019 RID: 25
		protected readonly Dictionary<string, Type> unitTypes = new Dictionary<string, Type>();

		// Token: 0x0400001A RID: 26
		protected bool controlAllies;
	}
}
