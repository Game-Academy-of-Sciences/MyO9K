using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Dynamic.Units;
using O9K.AIO.Modes.Combo;
using O9K.AIO.UnitManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Heroes.Unique;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using SharpDX;

namespace O9K.AIO.Heroes.Morphling
{
	// Token: 0x020000ED RID: 237
	internal class MorphlingUnitManager : UnitManager
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x000047B2 File Offset: 0x000029B2
		public MorphlingUnitManager(BaseHero baseHero) : base(baseHero)
		{
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000191AC File Offset: 0x000173AC
		public override void ExecuteCombo(ComboModeMenu comboModeMenu)
		{
			Morphling morphling;
			string controlMorphedUnitName = ((morphling = (this.owner.Hero as Morphling)) != null && morphling.IsMorphed) ? morphling.MorphedHero.Name : this.owner.Hero.Name;
			IEnumerable<ControllableUnit> controllableUnits = base.ControllableUnits;
			Func<ControllableUnit, bool> <>9__0;
			Func<ControllableUnit, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((ControllableUnit x) => x.MorphedUnitName == null || x.MorphedUnitName == controlMorphedUnitName));
			}
			foreach (ControllableUnit controllableUnit in controllableUnits.Where(predicate))
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

		// Token: 0x060004CC RID: 1228 RVA: 0x000192A0 File Offset: 0x000174A0
		public override void Orbwalk(ComboModeMenu comboModeMenu)
		{
			if (this.issuedAction.IsSleeping)
			{
				return;
			}
			Morphling morphling;
			string controlMorphedUnitName = ((morphling = (this.owner.Hero as Morphling)) != null && morphling.IsMorphed) ? morphling.MorphedHero.Name : this.owner.Hero.Name;
			List<ControllableUnit> list = (from x in base.ControllableUnits
			where x.MorphedUnitName == null || x.MorphedUnitName == controlMorphedUnitName
			orderby this.IssuedActionTime(x.Handle)
			select x).ToList<ControllableUnit>();
			if (base.BodyBlock(list, comboModeMenu))
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
				base.ControlAllUnits(list2);
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00019434 File Offset: 0x00017634
		protected override void OnAbilityAdded(Ability9 entity)
		{
			try
			{
				ActiveAbility activeAbility;
				if (entity.IsControllable && entity.Owner.CanUseAbilities && entity.Owner.IsAlly(this.owner) && (activeAbility = (entity as ActiveAbility)) != null)
				{
					Unit9 abilityOwner = entity.Owner;
					Morphling morphling = entity.Owner as Morphling;
					if (morphling != null && morphling.IsMorphed)
					{
						ControllableUnit controllableUnit;
						Type type;
						if (this.unitTypes.TryGetValue(morphling.MorphedHero.Name, out type))
						{
							controllableUnit = this.controllableUnits.Find((ControllableUnit x) => x.Handle == abilityOwner.Handle && x.GetType() == type);
							if (controllableUnit == null)
							{
								controllableUnit = (ControllableUnit)Activator.CreateInstance(type, new object[]
								{
									abilityOwner,
									this.abilitySleeper,
									this.orbwalkSleeper[abilityOwner.Handle],
									base.GetUnitMenu(abilityOwner)
								});
								controllableUnit.FailSafe = base.BaseHero.FailSafe;
								controllableUnit.MorphedUnitName = morphling.MorphedHero.Name;
								foreach (ActiveAbility ability in (from x in abilityOwner.Abilities
								where x.IsItem
								select x).OfType<ActiveAbility>())
								{
									controllableUnit.AddAbility(ability, base.BaseHero.ComboMenus, base.BaseHero.MoveComboModeMenu);
								}
								this.controllableUnits.Add(controllableUnit);
							}
						}
						else
						{
							controllableUnit = this.controllableUnits.Find((ControllableUnit x) => x.Handle == abilityOwner.Handle && x is DynamicUnit);
							if (controllableUnit == null)
							{
								controllableUnit = new DynamicUnit(abilityOwner, this.abilitySleeper, this.orbwalkSleeper[abilityOwner.Handle], base.GetUnitMenu(abilityOwner), base.BaseHero)
								{
									FailSafe = base.BaseHero.FailSafe,
									MorphedUnitName = morphling.MorphedHero.Name
								};
								foreach (ActiveAbility ability2 in (from x in abilityOwner.Abilities
								where x.IsItem
								select x).OfType<ActiveAbility>())
								{
									controllableUnit.AddAbility(ability2, base.BaseHero.ComboMenus, base.BaseHero.MoveComboModeMenu);
								}
								this.controllableUnits.Add(controllableUnit);
							}
						}
						if (activeAbility.IsItem)
						{
							IEnumerable<ControllableUnit> controllableUnits = this.controllableUnits;
							Func<ControllableUnit, bool> predicate;
							Func<ControllableUnit, bool> <>9__4;
							if ((predicate = <>9__4) == null)
							{
								predicate = (<>9__4 = ((ControllableUnit x) => x.Handle == abilityOwner.Handle));
							}
							using (IEnumerator<ControllableUnit> enumerator2 = controllableUnits.Where(predicate).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									ControllableUnit controllableUnit2 = enumerator2.Current;
									controllableUnit2.AddAbility(activeAbility, base.BaseHero.ComboMenus, base.BaseHero.MoveComboModeMenu);
								}
								goto IL_3D3;
							}
						}
						controllableUnit.AddAbility(activeAbility, base.BaseHero.ComboMenus, base.BaseHero.MoveComboModeMenu);
						IL_3D3:;
					}
					else
					{
						if (activeAbility.IsItem)
						{
							IEnumerable<ControllableUnit> controllableUnits2 = this.controllableUnits;
							Func<ControllableUnit, bool> predicate2;
							Func<ControllableUnit, bool> <>9__5;
							if ((predicate2 = <>9__5) == null)
							{
								predicate2 = (<>9__5 = ((ControllableUnit x) => x.Handle == abilityOwner.Handle));
							}
							using (IEnumerator<ControllableUnit> enumerator2 = controllableUnits2.Where(predicate2).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									ControllableUnit controllableUnit3 = enumerator2.Current;
									controllableUnit3.AddAbility(activeAbility, base.BaseHero.ComboMenus, base.BaseHero.MoveComboModeMenu);
								}
								goto IL_488;
							}
						}
						ControllableUnit controllableUnit4 = this.controllableUnits.Find((ControllableUnit x) => x.Handle == entity.Owner.Handle);
						if (controllableUnit4 != null)
						{
							controllableUnit4.AddAbility(activeAbility, base.BaseHero.ComboMenus, base.BaseHero.MoveComboModeMenu);
						}
						IL_488:;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00019950 File Offset: 0x00017B50
		protected override void OnAbilityRemoved(Ability9 entity)
		{
			try
			{
				ActiveAbility ability;
				if (entity.IsControllable && entity.Owner.CanUseAbilities && entity.Owner.IsAlly(this.owner) && (ability = (entity as ActiveAbility)) != null)
				{
					IEnumerable<ControllableUnit> controllableUnits = this.controllableUnits;
					Func<ControllableUnit, bool> <>9__0;
					Func<ControllableUnit, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((ControllableUnit x) => x.Handle == entity.Owner.Handle));
					}
					foreach (ControllableUnit controllableUnit in controllableUnits.Where(predicate))
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
	}
}
