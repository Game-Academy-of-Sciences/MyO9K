using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.EarthSpirit.Abilities;
using O9K.AIO.Heroes.EarthSpirit.Modes;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.EarthSpirit.Units
{
	// Token: 0x02000189 RID: 393
	[UnitName("npc_dota_hero_earth_spirit")]
	internal class EarthSpirit : ControllableUnit
	{
		// Token: 0x060007F7 RID: 2039 RVA: 0x00024520 File Offset: 0x00022720
		public EarthSpirit(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.earth_spirit_boulder_smash,
					(ActiveAbility x) => this.smash = new BoulderSmash(x)
				},
				{
					AbilityId.earth_spirit_rolling_boulder,
					(ActiveAbility x) => this.rolling = new RollingBoulder(x)
				},
				{
					AbilityId.earth_spirit_geomagnetic_grip,
					(ActiveAbility x) => this.grip = new GeomagneticGrip(x)
				},
				{
					AbilityId.earth_spirit_magnetize,
					(ActiveAbility x) => this.mag = new AoeAbility(x)
				},
				{
					AbilityId.earth_spirit_stone_caller,
					(ActiveAbility x) => this.stone = new StoneRemnant(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_spirit_vessel,
					(ActiveAbility x) => this.vessel = new DebuffAbility(x)
				},
				{
					AbilityId.item_urn_of_shadows,
					(ActiveAbility x) => this.urn = new DebuffAbility(x)
				},
				{
					AbilityId.item_veil_of_discord,
					(ActiveAbility x) => this.veil = new DebuffAbility(x)
				},
				{
					AbilityId.item_blade_mail,
					(ActiveAbility x) => this.bladeMail = new ShieldAbility(x)
				},
				{
					AbilityId.item_heavens_halberd,
					(ActiveAbility x) => this.halberd = new DisableAbility(x)
				},
				{
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.earth_spirit_rolling_boulder, (ActiveAbility x) => this.rollingBlink = new RollingBoulderBlink(x));
			base.MoveComboAbilities.Add(AbilityId.earth_spirit_stone_caller, (ActiveAbility x) => this.stoneBlink = new StoneRemnantBlink(x));
			base.MoveComboAbilities.Add(AbilityId.earth_spirit_boulder_smash, (ActiveAbility _) => this.smash);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000246B4 File Offset: 0x000228B4
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.blink, 400f, 200f))
			{
				RollingBoulder rollingBoulder = this.rolling;
				if (rollingBoulder != null)
				{
					rollingBoulder.Sleeper.Sleep(0.8f);
				}
				base.ComboSleeper.ExtendSleep(0.1f);
				return true;
			}
			if (abilityHelper.UseAbility(this.force, 400f, 200f))
			{
				RollingBoulder rollingBoulder2 = this.rolling;
				if (rollingBoulder2 != null)
				{
					rollingBoulder2.Sleeper.Sleep(0.8f);
				}
				base.ComboSleeper.ExtendSleep(0.1f);
				return true;
			}
			if (abilityHelper.UseAbility(this.veil, true))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.rolling, true, true, true, true))
			{
				if (abilityHelper.UseAbility(this.rolling, true))
				{
					return true;
				}
				if (abilityHelper.CanBeCasted(this.stone, false, true, true, true))
				{
					if (abilityHelper.ForceUseAbility(this.stone, true, true))
					{
						BoulderSmash boulderSmash = this.smash;
						if (boulderSmash != null)
						{
							boulderSmash.Sleeper.Sleep(1f);
						}
						return true;
					}
				}
				else if (abilityHelper.ForceUseAbility(this.rolling, true, true))
				{
					return true;
				}
			}
			if (abilityHelper.CanBeCasted(this.smash, true, true, true, true))
			{
				if (abilityHelper.UseAbility(this.smash, true))
				{
					return true;
				}
				if (abilityHelper.CanBeCasted(this.stone, false, true, true, true))
				{
					if (this.stone.UseAbility(targetManager, base.ComboSleeper, this.smash))
					{
						return true;
					}
				}
				else if (abilityHelper.ForceUseAbility(this.smash, true, true))
				{
					return true;
				}
			}
			if (abilityHelper.CanBeCasted(this.grip, true, true, true, true))
			{
				if (abilityHelper.UseAbility(this.grip, true))
				{
					return true;
				}
				if (abilityHelper.CanBeCasted(this.stone, false, true, true, true))
				{
					if (this.stone.UseAbility(targetManager, base.ComboSleeper, this.grip))
					{
						return true;
					}
				}
				else if (abilityHelper.ForceUseAbility(this.grip, true, true))
				{
					return true;
				}
			}
			return abilityHelper.UseAbility(this.halberd, true) || abilityHelper.UseAbility(this.mag, true) || abilityHelper.UseAbility(this.vessel, true) || abilityHelper.UseAbility(this.urn, true) || abilityHelper.UseAbility(this.shiva, true) || abilityHelper.UseAbility(this.bladeMail, true) || abilityHelper.UseAbility(this.stone, true);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00024918 File Offset: 0x00022B18
		public void RollSmashCombo(TargetManager targetManager, RollSmashModeMenu menu)
		{
			if (base.ComboSleeper.IsSleeping)
			{
				return;
			}
			Unit9 target = targetManager.Target;
			float num = base.Owner.Distance(target.Position);
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, null, this);
			if (base.Owner.HasModifier("modifier_earth_spirit_rolling_boulder_caster") && abilityHelper.ForceUseAbility(this.stone, true, true))
			{
				base.ComboSleeper.Sleep(3f);
				return;
			}
			Unit9 unit = (from x in targetManager.AllyHeroes
			where !x.Equals(this.Owner) && menu.IsAllyEnabled(x.Name) && x.Distance(target) > 300f && x.Distance(target) < 1500f
			orderby x.Distance(target)
			select x).FirstOrDefault<Unit9>();
			if (unit == null)
			{
				return;
			}
			if (target.HasModifier("modifier_earth_spirit_boulder_smash"))
			{
				if (abilityHelper.CanBeCasted(this.rolling, false, false, true, true) && this.rolling.SimpleUseAbility(target.Position))
				{
					base.ComboSleeper.Sleep(0.5f);
					return;
				}
				return;
			}
			else
			{
				BoulderSmash boulderSmash = this.smash;
				if (boulderSmash == null || !boulderSmash.Ability.CanBeCasted(true))
				{
					return;
				}
				if (num < this.smash.Ability.CastRange + 100f)
				{
					if (Vector3Extensions.AngleBetween(base.Owner.Position, target.Position, unit.Position) < 30f)
					{
						this.smash.Ability.UseAbility(target, false, false);
						base.ComboSleeper.Sleep(0.3f);
						return;
					}
					if (target.GetImmobilityDuration() > 0.5f)
					{
						base.Owner.BaseUnit.Move(Vector3Extensions.Extend2D(target.Position, unit.Position, -100f));
						base.OrbwalkSleeper.Sleep(0.1f);
						base.ComboSleeper.Sleep(0.1f);
						return;
					}
				}
				if (abilityHelper.CanBeCasted(this.blink, true, true, true, true))
				{
					Vector3 position = Vector3Extensions.Extend2D(target.GetPredictedPosition(base.Owner.GetTurnTime(target.Position) + Game.Ping / 2000f + 0.2f), unit.Position, -125f);
					if (abilityHelper.UseAbility(this.blink, position))
					{
						base.ComboSleeper.ExtendSleep(0.1f);
						return;
					}
					return;
				}
				else
				{
					if (!abilityHelper.CanBeCasted(this.rolling, true, true, true, true))
					{
						return;
					}
					if (abilityHelper.UseAbility(this.rolling, true))
					{
						return;
					}
					if (abilityHelper.CanBeCasted(this.stone, false, true, true, true))
					{
						abilityHelper.ForceUseAbility(this.stone, true, true);
						return;
					}
					abilityHelper.ForceUseAbility(this.rolling, true, true);
					return;
				}
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00024BE4 File Offset: 0x00022DE4
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			if (base.MoveComboUseBlinks(abilityHelper))
			{
				return true;
			}
			if (abilityHelper.UseMoveAbility(this.rollingBlink))
			{
				BoulderSmash boulderSmash = this.smash;
				if (boulderSmash != null)
				{
					boulderSmash.Sleeper.Sleep(2f);
				}
				return true;
			}
			return base.Owner.HasModifier("modifier_earth_spirit_rolling_boulder_caster") && abilityHelper.UseMoveAbility(this.stoneBlink);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00024C4C File Offset: 0x00022E4C
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			if (abilityHelper.CanBeCasted(this.smash, true, true, true, true))
			{
				if (abilityHelper.UseAbility(this.smash, true))
				{
					return true;
				}
				if (abilityHelper.CanBeCasted(this.stone, false, true, true, true))
				{
					if (this.stone.UseAbility(abilityHelper.TargetManager, base.ComboSleeper, this.smash))
					{
						return true;
					}
				}
				else if (abilityHelper.ForceUseAbility(this.smash, true, true))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000460 RID: 1120
		private ShieldAbility bladeMail;

		// Token: 0x04000461 RID: 1121
		private BlinkAbility blink;

		// Token: 0x04000462 RID: 1122
		private ForceStaff force;

		// Token: 0x04000463 RID: 1123
		private GeomagneticGrip grip;

		// Token: 0x04000464 RID: 1124
		private DisableAbility halberd;

		// Token: 0x04000465 RID: 1125
		private AoeAbility mag;

		// Token: 0x04000466 RID: 1126
		private RollingBoulder rolling;

		// Token: 0x04000467 RID: 1127
		private BlinkAbility rollingBlink;

		// Token: 0x04000468 RID: 1128
		private DebuffAbility shiva;

		// Token: 0x04000469 RID: 1129
		private BoulderSmash smash;

		// Token: 0x0400046A RID: 1130
		private StoneRemnant stone;

		// Token: 0x0400046B RID: 1131
		private StoneRemnantBlink stoneBlink;

		// Token: 0x0400046C RID: 1132
		private DebuffAbility urn;

		// Token: 0x0400046D RID: 1133
		private DebuffAbility veil;

		// Token: 0x0400046E RID: 1134
		private DebuffAbility vessel;
	}
}
