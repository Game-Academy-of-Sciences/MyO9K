using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ensage;
using Ensage.SDK.Handlers;
using Ensage.SDK.Helpers;
using O9K.Core.Entities;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Buildings;
using O9K.Core.Entities.Heroes;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity.Monitors;
using O9K.Core.Prediction;
using SharpDX;

namespace O9K.Core.Managers.Entity
{
	// Token: 0x02000068 RID: 104
	public static class EntityManager9
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0001A9B8 File Offset: 0x00018BB8
		static EntityManager9()
		{
			foreach (Type type in from x in Assembly.GetExecutingAssembly().GetTypes()
			where !x.IsAbstract && x.IsClass
			select x)
			{
				if (typeof(Ability9).IsAssignableFrom(type))
				{
					using (IEnumerator<AbilityIdAttribute> enumerator2 = type.GetCustomAttributes<AbilityIdAttribute>().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							AbilityIdAttribute abilityIdAttribute = enumerator2.Current;
							EntityManager9.abilityTypes.Add(abilityIdAttribute.AbilityId, type);
						}
						continue;
					}
				}
				if (typeof(Hero9).IsAssignableFrom(type))
				{
					using (IEnumerator<HeroIdAttribute> enumerator3 = type.GetCustomAttributes<HeroIdAttribute>().GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							HeroIdAttribute heroIdAttribute = enumerator3.Current;
							EntityManager9.heroTypes.Add(heroIdAttribute.HeroId, type);
						}
						continue;
					}
				}
				if (typeof(Unit9).IsAssignableFrom(type))
				{
					foreach (UnitNameAttribute unitNameAttribute in type.GetCustomAttributes<UnitNameAttribute>())
					{
						EntityManager9.unitTypes.Add(unitNameAttribute.Name, type);
					}
				}
			}
			EntityManager9.UnitMonitor = new UnitMonitor();
			EntityManager9.AbilityMonitor = new AbilityMonitor();
			EntityManager9.delayedEntityHandler = UpdateManager.Subscribe(new Action(EntityManager9.DelayedEntitiesOnUpdate), 1000, false);
			EntityManager9.AddCurrentUnits();
			EntityManager9.AddCurrentAbilities();
			ObjectManager.OnAddEntity += EntityManager9.OnAddEntity;
			ObjectManager.OnRemoveEntity += EntityManager9.OnRemoveEntity;
			UpdateManager.BeginInvoke(new Action(EntityManager9.DemoModeCheck), 2000);
			UpdateManager.BeginInvoke(new Action(EntityManager9.LoadCheck), 5000);
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600033E RID: 830 RVA: 0x0001ACD8 File Offset: 0x00018ED8
		// (remove) Token: 0x0600033F RID: 831 RVA: 0x00004156 File Offset: 0x00002356
		public static event EntityManager9.EventHandler<Ability9> AbilityAdded
		{
			add
			{
				if (value == null)
				{
					return;
				}
				foreach (Ability9 entity in EntityManager9.Abilities)
				{
					try
					{
						value(entity);
					}
					catch (Exception exception)
					{
						Logger.Error(exception, null);
					}
				}
				EntityManager9.abilityAdded = (EntityManager9.EventHandler<Ability9>)Delegate.Combine(EntityManager9.abilityAdded, value);
			}
			remove
			{
				EntityManager9.abilityAdded = (EntityManager9.EventHandler<Ability9>)Delegate.Remove(EntityManager9.abilityAdded, value);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000340 RID: 832 RVA: 0x0001AD54 File Offset: 0x00018F54
		// (remove) Token: 0x06000341 RID: 833 RVA: 0x0001AD88 File Offset: 0x00018F88
		public static event EntityManager9.EventHandler<Ability9> AbilityRemoved;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000342 RID: 834 RVA: 0x0001ADBC File Offset: 0x00018FBC
		// (remove) Token: 0x06000343 RID: 835 RVA: 0x0000416D File Offset: 0x0000236D
		public static event EntityManager9.EventHandler<Unit9> UnitAdded
		{
			add
			{
				if (value == null)
				{
					return;
				}
				foreach (Unit9 entity in EntityManager9.Units)
				{
					try
					{
						value(entity);
					}
					catch (Exception exception)
					{
						Logger.Error(exception, null);
					}
				}
				EntityManager9.unitAdded = (EntityManager9.EventHandler<Unit9>)Delegate.Combine(EntityManager9.unitAdded, value);
			}
			remove
			{
				EntityManager9.unitAdded = (EntityManager9.EventHandler<Unit9>)Delegate.Remove(EntityManager9.unitAdded, value);
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000344 RID: 836 RVA: 0x0001AE38 File Offset: 0x00019038
		// (remove) Token: 0x06000345 RID: 837 RVA: 0x0001AE6C File Offset: 0x0001906C
		public static event EntityManager9.EventHandler<Unit9> UnitRemoved;

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00004184 File Offset: 0x00002384
		public static AbilityMonitor AbilityMonitor { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000418B File Offset: 0x0000238B
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00004192 File Offset: 0x00002392
		public static Vector3 AllyFountain { get; private set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000419A File Offset: 0x0000239A
		// (set) Token: 0x0600034A RID: 842 RVA: 0x000041A1 File Offset: 0x000023A1
		public static Vector3 EnemyFountain { get; private set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600034B RID: 843 RVA: 0x000041A9 File Offset: 0x000023A9
		public static List<Unit9> AllyUnits
		{
			get
			{
				return (from x in EntityManager9.Units
				where x.IsAlive && !x.IsIllusion && !x.IsInvulnerable && x.IsAlly(EntityManager9.Owner)
				select x).ToList<Unit9>();
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600034C RID: 844 RVA: 0x000041D9 File Offset: 0x000023D9
		public static List<Unit9> AllyHeroes
		{
			get
			{
				return (from x in EntityManager9.Units
				where x.IsHero && x.IsAlive && !x.IsIllusion && !x.IsInvulnerable && x.IsAlly(EntityManager9.Owner)
				select x).ToList<Unit9>();
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00004209 File Offset: 0x00002409
		public static List<Unit9> EnemyUnits
		{
			get
			{
				return (from x in EntityManager9.Units
				where x.IsAlive && !x.IsIllusion && x.IsVisible && !x.IsInvulnerable && !x.IsAlly(EntityManager9.Owner)
				select x).ToList<Unit9>();
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00004239 File Offset: 0x00002439
		public static List<Unit9> EnemyHeroes
		{
			get
			{
				return (from x in EntityManager9.Units
				where x.IsHero && x.IsAlive && !x.IsIllusion && x.IsVisible && !x.IsInvulnerable && !x.IsAlly(EntityManager9.Owner)
				select x).ToList<Unit9>();
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00004269 File Offset: 0x00002469
		public static IEnumerable<Ability9> Abilities
		{
			get
			{
				return from x in EntityManager9.abilities.Values
				where x.IsValid
				select x;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00004299 File Offset: 0x00002499
		public static IEnumerable<Hero9> Heroes
		{
			get
			{
				return (from x in EntityManager9.units.Values
				where x.IsHero && x.IsValid
				select x).Cast<Hero9>();
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000351 RID: 849 RVA: 0x000042CE File Offset: 0x000024CE
		public static Owner Owner { get; } = new Owner();

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000352 RID: 850 RVA: 0x000042D5 File Offset: 0x000024D5
		public static UnitMonitor UnitMonitor { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000353 RID: 851 RVA: 0x000042DC File Offset: 0x000024DC
		public static IEnumerable<Unit9> Units
		{
			get
			{
				return from x in EntityManager9.units.Values
				where x.IsValid
				select x;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000430C File Offset: 0x0000250C
		public static IEnumerable<Tree> Trees
		{
			get
			{
				return from x in EntityManager9.trees
				where x.IsAlive
				select x;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00004337 File Offset: 0x00002537
		internal static IEnumerable<Ability> BaseAbilities
		{
			get
			{
				return from x in ObjectManager.GetEntities<Ability>().Concat(ObjectManager.GetDormantEntities<Ability>())
				where x.IsValid
				select x;
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001AEA0 File Offset: 0x000190A0
		public static void ForceReload()
		{
			try
			{
				foreach (Unit9 unit in EntityManager9.Units)
				{
					try
					{
						EntityManager9.EventHandler<Unit9> unitRemoved = EntityManager9.UnitRemoved;
						if (unitRemoved != null)
						{
							unitRemoved(unit);
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (unit as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
				}
				foreach (Ability9 ability in EntityManager9.Abilities)
				{
					try
					{
						EntityManager9.EventHandler<Ability9> abilityRemoved = EntityManager9.AbilityRemoved;
						if (abilityRemoved != null)
						{
							abilityRemoved(ability);
						}
					}
					finally
					{
						ability.Dispose();
					}
				}
				EntityManager9.units.Clear();
				EntityManager9.abilities.Clear();
				EntityManager9.AddCurrentUnits();
				EntityManager9.AddCurrentAbilities();
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0001AFA8 File Offset: 0x000191A8
		public static Ability9 GetAbility(uint handle)
		{
			Ability9 ability;
			if (EntityManager9.abilities.TryGetValue(handle, out ability) && ability.IsValid)
			{
				return ability;
			}
			return null;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001AFD0 File Offset: 0x000191D0
		public static T GetAbility<T>(Unit9 owner) where T : Ability9
		{
			return (T)((object)EntityManager9.Abilities.FirstOrDefault((Ability9 x) => x is T && x.Owner.Equals(owner)));
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0001B008 File Offset: 0x00019208
		public static T GetAbility<T>(uint ownerHandle) where T : Ability9
		{
			return (T)((object)EntityManager9.Abilities.FirstOrDefault((Ability9 x) => x is T && x.Owner.Handle == ownerHandle));
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001B040 File Offset: 0x00019240
		public static Unit9 GetUnit(uint handle)
		{
			Unit9 unit;
			if (EntityManager9.units.TryGetValue(handle, out unit) && unit.IsValid)
			{
				return unit;
			}
			return null;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001B068 File Offset: 0x00019268
		internal static Ability9 AddAbility(Ability ability)
		{
			Ability9 result;
			try
			{
				Type type;
				if (!EntityManager9.abilityTypes.TryGetValue(ability.Id, out type))
				{
					result = null;
				}
				else
				{
					Ability9 ability2 = EntityManager9.GetAbilityFast(ability.Handle);
					if (ability2 != null)
					{
						result = ability2;
					}
					else
					{
						Entity owner = ability.Owner;
						EntityHandle? entityHandle = (owner != null) ? new EntityHandle?(owner.Handle) : null;
						Unit9 unitFast = EntityManager9.GetUnitFast((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null);
						if (unitFast == null)
						{
							EntityManager9.DelayedAdd(ability);
							result = null;
						}
						else
						{
							Item item;
							if ((item = (ability as Item)) != null)
							{
								if (item.PurchaseTime < 0f)
								{
									return null;
								}
							}
							else if (ability.AbilitySlot < AbilitySlot.Slot_1 && !ability.IsHidden)
							{
								return null;
							}
							ability2 = (Ability9)Activator.CreateInstance(type, new object[]
							{
								ability
							});
							ability2.SetPrediction(EntityManager9.predictionManager);
							EntityManager9.AbilityMonitor.SetOwner(ability2, unitFast);
							EntityManager9.abilities[ability2.Handle] = ability2;
							EntityManager9.EventHandler<Ability9> eventHandler = EntityManager9.abilityAdded;
							if (eventHandler != null)
							{
								eventHandler(ability2);
							}
							result = ability2;
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, ability, null);
				result = null;
			}
			return result;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001B1C8 File Offset: 0x000193C8
		internal static Unit9 AddUnit(Unit unit)
		{
			Unit9 result;
			try
			{
				if (EntityManager9.ignoredUnits.Contains(unit.Name))
				{
					result = null;
				}
				else
				{
					Unit9 unit2 = EntityManager9.GetUnitFast(unit.Handle);
					if (unit2 != null)
					{
						result = unit2;
					}
					else
					{
						Hero hero;
						Type type2;
						if ((hero = (unit as Hero)) != null)
						{
							if (hero.HeroId == HeroId.npc_dota_hero_base || hero.Inventory == null)
							{
								EntityManager9.DelayedAdd(hero);
								return null;
							}
							Type type;
							if (EntityManager9.heroTypes.TryGetValue(hero.HeroId, out type))
							{
								unit2 = (Hero9)Activator.CreateInstance(type, new object[]
								{
									hero
								});
							}
							else
							{
								unit2 = new Hero9(hero);
							}
							if (!unit2.IsIllusion && unit2.Handle == ObjectManager.LocalHero.Handle)
							{
								EntityManager9.Owner.SetHero(unit2);
							}
						}
						else if (EntityManager9.unitTypes.TryGetValue(unit.Name, out type2))
						{
							unit2 = (Unit9)Activator.CreateInstance(type2, new object[]
							{
								unit
							});
						}
						else
						{
							unit2 = new Unit9(unit);
						}
						EntityManager9.units[unit2.Handle] = unit2;
						Unit unit3;
						if ((unit3 = (unit2.BaseOwner as Unit)) != null && unit3.IsValid)
						{
							try
							{
								unit2.Owner = EntityManager9.AddUnit(unit3);
							}
							catch (Exception exception)
							{
								Logger.Error(exception, "Set unit owner");
							}
						}
						EntityManager9.UnitMonitor.CheckModifiers(unit2);
						EntityManager9.EventHandler<Unit9> eventHandler = EntityManager9.unitAdded;
						if (eventHandler != null)
						{
							eventHandler(unit2);
						}
						result = unit2;
					}
				}
			}
			catch (Exception exception2)
			{
				Logger.Error(exception2, unit, null);
				result = null;
			}
			return result;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001B36C File Offset: 0x0001956C
		internal static void ChangeEntityControl(Entity entity)
		{
			try
			{
				Unit9 unit = EntityManager9.GetUnit(entity.Handle);
				if (!(unit == null))
				{
					Entity owner = entity.Owner;
					if (owner != null && owner.IsValid)
					{
						if (!unit.IsHero)
						{
							Entity baseOwner = unit.BaseOwner;
							if (baseOwner != null && baseOwner.IsValid && unit.BaseOwner.Handle == owner.Handle)
							{
								return;
							}
						}
						EntityManager9.EventHandler<Unit9> unitRemoved = EntityManager9.UnitRemoved;
						if (unitRemoved != null)
						{
							unitRemoved(unit);
						}
						foreach (Ability9 entity2 in unit.Abilities)
						{
							EntityManager9.EventHandler<Ability9> abilityRemoved = EntityManager9.AbilityRemoved;
							if (abilityRemoved != null)
							{
								abilityRemoved(entity2);
							}
						}
						unit.IsControllable = unit.BaseUnit.IsControllable;
						unit.Team = unit.BaseUnit.Team;
						unit.EnemyTeam = ((unit.Team == Team.Radiant) ? Team.Dire : Team.Radiant);
						unit.BaseOwner = owner;
						Unit unit2;
						if ((unit2 = (owner as Unit)) != null)
						{
							unit.Owner = EntityManager9.AddUnit(unit2);
						}
						EntityManager9.EventHandler<Unit9> eventHandler = EntityManager9.unitAdded;
						if (eventHandler != null)
						{
							eventHandler(unit);
						}
						foreach (Ability9 entity3 in unit.Abilities)
						{
							EntityManager9.EventHandler<Ability9> eventHandler2 = EntityManager9.abilityAdded;
							if (eventHandler2 != null)
							{
								eventHandler2(entity3);
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, entity, "Change owner");
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001B538 File Offset: 0x00019738
		internal static Ability9 GetAbilityFast(uint handle)
		{
			Ability9 result;
			if (!EntityManager9.abilities.TryGetValue(handle, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001B558 File Offset: 0x00019758
		internal static Unit9 GetUnitFast(uint handle)
		{
			Unit9 result;
			if (!EntityManager9.units.TryGetValue(handle, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000436C File Offset: 0x0000256C
		internal static Unit9 GetUnitFast(uint? handle)
		{
			if (handle != null)
			{
				return EntityManager9.GetUnitFast(handle.Value);
			}
			return null;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001B578 File Offset: 0x00019778
		internal static void RemoveAbility(Ability ability)
		{
			try
			{
				Ability9 abilityFast = EntityManager9.GetAbilityFast(ability.Handle);
				if (!(abilityFast == null))
				{
					abilityFast.Dispose();
					EntityManager9.abilities.Remove(abilityFast.Handle);
					EntityManager9.EventHandler<Ability9> abilityRemoved = EntityManager9.AbilityRemoved;
					if (abilityRemoved != null)
					{
						abilityRemoved(abilityFast);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, ability, null);
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001B5E4 File Offset: 0x000197E4
		private static void AddBuilding(Unit building)
		{
			try
			{
				Unit9 unit = EntityManager9.GetUnitFast(building.Handle);
				if (!(unit != null))
				{
					string networkName = building.NetworkName;
					if (!(networkName == "CDOTA_BaseNPC_Tower"))
					{
						if (!(networkName == "CDOTA_BaseNPC_Barracks"))
						{
							if (!(networkName == "CDOTA_BaseNPC_Healer"))
							{
								if (!(networkName == "CDOTA_Unit_Fountain"))
								{
									if (networkName == "CDOTA_BaseNPC_Shop")
									{
										return;
									}
									unit = new Building9(building);
								}
								else
								{
									unit = new Building9(building)
									{
										IsFountain = true
									};
									if (unit.Team == EntityManager9.Owner.Team)
									{
										EntityManager9.AllyFountain = unit.Position;
									}
									else
									{
										EntityManager9.EnemyFountain = unit.Position;
									}
								}
							}
							else
							{
								unit = new Shrine9(building);
							}
						}
						else
						{
							unit = new Building9(building)
							{
								IsBarrack = true
							};
						}
					}
					else
					{
						unit = new Tower9((Tower)building);
					}
					EntityManager9.units[unit.Handle] = unit;
					EntityManager9.EventHandler<Unit9> eventHandler = EntityManager9.unitAdded;
					if (eventHandler != null)
					{
						eventHandler(unit);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, building, null);
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001B704 File Offset: 0x00019904
		private static void AddCurrentAbilities()
		{
			foreach (Ability ability in ObjectManager.GetEntitiesFast<Ability>().Concat(ObjectManager.GetDormantEntities<Ability>()))
			{
				if (ability.IsValid)
				{
					EntityManager9.AddAbility(ability);
				}
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001B764 File Offset: 0x00019964
		private static void AddCurrentUnits()
		{
			EntityManager9.AddUnit(ObjectManager.LocalHero);
			foreach (Unit unit in ObjectManager.GetEntitiesFast<Unit>().Concat(ObjectManager.GetDormantEntities<Unit>()))
			{
				if (unit.IsValid && !unit.Position.IsZero)
				{
					if (unit is Building)
					{
						EntityManager9.AddBuilding(unit);
					}
					else
					{
						EntityManager9.AddUnit(unit);
					}
				}
			}
			EntityManager9.trees = ObjectManager.GetEntities<Tree>().ToArray<Tree>();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00004385 File Offset: 0x00002585
		private static void AutoReload()
		{
			if (EntityManager9.Heroes.Any((Hero9 x) => x.IsMyHero))
			{
				return;
			}
			EntityManager9.ForceReload();
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000043B8 File Offset: 0x000025B8
		private static void DelayedAdd(Ability ability)
		{
			EntityManager9.delayedAbilities.Add(ability);
			EntityManager9.delayedEntityHandler.IsEnabled = true;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000043D0 File Offset: 0x000025D0
		private static void DelayedAdd(Hero hero)
		{
			EntityManager9.delayedHeroes.Add(hero);
			EntityManager9.delayedEntityHandler.IsEnabled = true;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001B7FC File Offset: 0x000199FC
		private static void DelayedEntitiesOnUpdate()
		{
			try
			{
				for (int i = EntityManager9.delayedHeroes.Count - 1; i > -1; i--)
				{
					Hero hero = EntityManager9.delayedHeroes[i];
					if (!hero.IsValid)
					{
						EntityManager9.delayedHeroes.RemoveAt(i);
					}
					else if (hero.HeroId != HeroId.npc_dota_hero_base && hero.Inventory != null)
					{
						EntityManager9.delayedHeroes.RemoveAt(i);
						EntityManager9.AddUnit(hero);
					}
				}
				for (int j = EntityManager9.delayedAbilities.Count - 1; j > -1; j--)
				{
					Ability ability = EntityManager9.delayedAbilities[j];
					if (!ability.IsValid)
					{
						EntityManager9.delayedAbilities.RemoveAt(j);
					}
					else
					{
						Entity owner = ability.Owner;
						EntityHandle? entityHandle = (owner != null) ? new EntityHandle?(owner.Handle) : null;
						if (!(EntityManager9.GetUnitFast((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == null))
						{
							EntityManager9.delayedAbilities.RemoveAt(j);
							EntityManager9.AddAbility(ability);
						}
					}
				}
				if (EntityManager9.delayedAbilities.Count == 0 && EntityManager9.delayedHeroes.Count == 0)
				{
					EntityManager9.delayedEntityHandler.IsEnabled = false;
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000043E8 File Offset: 0x000025E8
		private static void DemoModeCheck()
		{
			if (Game.GameMode != GameMode.Demo)
			{
				return;
			}
			Hud.DisplayWarning("O9K // Some assemblies will not work correctly in demo mode", 600f);
			Hud.DisplayWarning("O9K // Use custom lobby instead", 600f);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001B954 File Offset: 0x00019B54
		private static void LoadCheck()
		{
			Unit9 unit = EntityManager9.Units.FirstOrDefault((Unit9 x) => x.IsMyHero);
			if (unit != null && unit.IsValid && unit.MoveCapability != MoveCapability.None)
			{
				UpdateManager.Subscribe(new Action(EntityManager9.AutoReload), 5000, true);
				return;
			}
			Logger.Warn("O9K was not loaded successfully, reloading...");
			EntityManager9.ForceReload();
			UpdateManager.Subscribe(new Action(EntityManager9.AutoReload), 5000, true);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001B9E0 File Offset: 0x00019BE0
		private static void OnAddEntity(EntityEventArgs args)
		{
			Entity entity = args.Entity;
			if (entity != null)
			{
				Building <building>5__;
				if ((<building>5__ = (entity as Building)) != null)
				{
					Building <building>5__2 = <building>5__;
					UpdateManager.BeginInvoke(delegate
					{
						if (<building>5__2.IsValid)
						{
							EntityManager9.AddBuilding(<building>5__2);
						}
					}, 100);
					return;
				}
				Unit <unit>5__;
				if ((<unit>5__ = (entity as Unit)) != null)
				{
					Unit <unit>5__3 = <unit>5__;
					UpdateManager.BeginInvoke(delegate
					{
						if (<unit>5__3.IsValid)
						{
							EntityManager9.AddUnit(<unit>5__3);
						}
					}, 100);
					return;
				}
				Ability <ability>5__;
				if ((<ability>5__ = (entity as Ability)) != null)
				{
					Ability <ability>5__4 = <ability>5__;
					UpdateManager.BeginInvoke(delegate
					{
						if (<ability>5__4.IsValid)
						{
							EntityManager9.AddAbility(<ability>5__4);
						}
					}, 300);
					return;
				}
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001BA74 File Offset: 0x00019C74
		private static void OnRemoveEntity(EntityEventArgs args)
		{
			Entity entity = args.Entity;
			if (entity != null)
			{
				Unit unit;
				if ((unit = (entity as Unit)) != null)
				{
					EntityManager9.RemoveUnit(unit);
					return;
				}
				Ability ability;
				if ((ability = (entity as Ability)) != null)
				{
					EntityManager9.RemoveAbility(ability);
					return;
				}
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001BAB0 File Offset: 0x00019CB0
		private static void RemoveUnit(Entity unit)
		{
			try
			{
				Unit9 unitFast = EntityManager9.GetUnitFast(unit.Handle);
				if (!(unitFast == null))
				{
					IDisposable disposable;
					if ((disposable = (unitFast as IDisposable)) != null)
					{
						disposable.Dispose();
					}
					EntityManager9.units.Remove(unitFast.Handle);
					EntityManager9.EventHandler<Unit9> unitRemoved = EntityManager9.UnitRemoved;
					if (unitRemoved != null)
					{
						unitRemoved(unitFast);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, unit, null);
			}
		}

		// Token: 0x04000171 RID: 369
		private static EntityManager9.EventHandler<Ability9> abilityAdded;

		// Token: 0x04000172 RID: 370
		private static EntityManager9.EventHandler<Unit9> unitAdded;

		// Token: 0x0400017A RID: 378
		private static readonly Dictionary<uint, Ability9> abilities = new Dictionary<uint, Ability9>();

		// Token: 0x0400017B RID: 379
		private static Tree[] trees;

		// Token: 0x0400017C RID: 380
		private static readonly Dictionary<AbilityId, Type> abilityTypes = new Dictionary<AbilityId, Type>();

		// Token: 0x0400017D RID: 381
		private static readonly Dictionary<HeroId, Type> heroTypes = new Dictionary<HeroId, Type>();

		// Token: 0x0400017E RID: 382
		private static readonly Dictionary<string, Type> unitTypes = new Dictionary<string, Type>();

		// Token: 0x0400017F RID: 383
		private static readonly IPredictionManager9 predictionManager = new PredictionManager9();

		// Token: 0x04000180 RID: 384
		private static readonly IUpdateHandler delayedEntityHandler;

		// Token: 0x04000181 RID: 385
		private static readonly Dictionary<uint, Unit9> units = new Dictionary<uint, Unit9>();

		// Token: 0x04000182 RID: 386
		private static readonly List<Ability> delayedAbilities = new List<Ability>();

		// Token: 0x04000183 RID: 387
		private static readonly List<Hero> delayedHeroes = new List<Hero>();

		// Token: 0x04000184 RID: 388
		private static readonly HashSet<string> ignoredUnits = new HashSet<string>
		{
			"",
			"ent_dota_halloffame",
			"dota_portrait_entity",
			"portrait_world_unit",
			"npc_dota_wisp_spirit",
			"npc_dota_companion",
			"npc_dota_hero_announcer",
			"npc_dota_neutral_caster",
			"npc_dota_hero_announcer_killing_spree",
			"npc_dota_base",
			"npc_dota_thinker"
		};

		// Token: 0x02000069 RID: 105
		// (Invoke) Token: 0x0600036F RID: 879
		public delegate void EventHandler<in T>(T entity) where T : Entity9;
	}
}
