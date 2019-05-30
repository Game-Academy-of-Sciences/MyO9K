using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.EmberSpirit.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.EmberSpirit.Units
{
	// Token: 0x0200017A RID: 378
	[UnitName("npc_dota_hero_ember_spirit")]
	internal class EmberSpirit : ControllableUnit
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x00023748 File Offset: 0x00021948
		public EmberSpirit(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.ember_spirit_searing_chains,
					(ActiveAbility x) => this.chains = new DisableAbility(x)
				},
				{
					AbilityId.ember_spirit_sleight_of_fist,
					(ActiveAbility x) => this.fist = new SleightOfFist(x)
				},
				{
					AbilityId.ember_spirit_flame_guard,
					(ActiveAbility x) => this.shield = new ShieldAbility(x)
				},
				{
					AbilityId.ember_spirit_fire_remnant,
					(ActiveAbility x) => this.remnant = new FireRemnant(x)
				},
				{
					AbilityId.ember_spirit_activate_fire_remnant,
					(ActiveAbility x) => this.remnantActivate = new ActivateFireRemnant(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_veil_of_discord,
					(ActiveAbility x) => this.veil = new DebuffAbility(x)
				},
				{
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				},
				{
					AbilityId.item_blade_mail,
					(ActiveAbility x) => this.bladeMail = new ShieldAbility(x)
				},
				{
					AbilityId.item_mjollnir,
					(ActiveAbility x) => this.mjollnir = new ShieldAbility(x)
				},
				{
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				},
				{
					AbilityId.item_orchid,
					(ActiveAbility x) => this.orchid = new DisableAbility(x)
				},
				{
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.bloodthorn = new Bloodthorn(x)
				},
				{
					AbilityId.item_nullifier,
					(ActiveAbility x) => this.nullifier = new Nullifier(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.ember_spirit_fire_remnant, (ActiveAbility x) => this.remnantBlink = new FireRemnantBlink(x));
			base.MoveComboAbilities.Add(AbilityId.ember_spirit_activate_fire_remnant, (ActiveAbility x) => this.remnantActivateBlink = new ActivateFireRemnantBlink(x));
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000238D8 File Offset: 0x00021AD8
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.hex, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.orchid, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.bloodthorn, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.nullifier, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.veil, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.chains, true))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.blink, false, true, true, true) && abilityHelper.CanBeCasted(this.chains, false, true, true, true) && abilityHelper.CanBeCasted(this.fist, false, true, true, true))
			{
				float range = this.fist.Ability.Range;
				float num = this.blink.Ability.Range + range;
				Unit9 target = targetManager.Target;
				float num2 = base.Owner.Distance(target);
				if (num > num2 && num2 > range && abilityHelper.UseAbility(this.blink, target.Position))
				{
					return true;
				}
			}
			if (!base.Owner.IsInvulnerable && abilityHelper.UseAbility(this.blink, 700f, 0f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.fist, true))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.remnantActivate, false, true, true, true) && !this.remnant.Sleeper.IsSleeping && abilityHelper.ForceUseAbility(this.remnantActivate, true, true))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.remnant, true, true, true, true) && abilityHelper.HasMana(new UsableAbility[]
			{
				this.remnantActivate
			}) && !base.Owner.IsInvulnerable)
			{
				Unit9 target2 = targetManager.Target;
				float num3 = target2.Distance(base.Owner);
				if (((double)target2.GetImmobilityDuration() > 0.8 || num3 < 400f) && this.remnant.GetDamage(targetManager) + 100f > target2.Health)
				{
					int num4 = 0;
					while ((float)num4 < this.remnant.GetRequiredRemnants(targetManager))
					{
						abilityHelper.ForceUseAbility(this.remnant, false, true);
						num4++;
					}
					base.ComboSleeper.Sleep(base.Owner.Distance(target2) / this.remnant.Ability.Speed * 0.6f);
					return true;
				}
				if (num3 > 350f && abilityHelper.UseAbility(this.remnant, true))
				{
					return true;
				}
			}
			return abilityHelper.UseAbility(this.shiva, true) || abilityHelper.UseAbility(this.shield, 400f) || abilityHelper.UseAbility(this.mjollnir, 600f) || abilityHelper.UseAbility(this.bladeMail, 500f);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00023BA0 File Offset: 0x00021DA0
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			if (base.MoveComboUseBlinks(abilityHelper))
			{
				return true;
			}
			if (!this.remnantBlink.Sleeper.IsSleeping && abilityHelper.UseMoveAbility(this.remnantActivateBlink))
			{
				this.remnantActivateBlink.Sleeper.Sleep(1f);
				return true;
			}
			return !base.Owner.IsInvulnerable && abilityHelper.UseMoveAbility(this.remnantBlink);
		}

		// Token: 0x0400043E RID: 1086
		private ShieldAbility bladeMail;

		// Token: 0x0400043F RID: 1087
		private BlinkAbility blink;

		// Token: 0x04000440 RID: 1088
		private DisableAbility bloodthorn;

		// Token: 0x04000441 RID: 1089
		private DisableAbility chains;

		// Token: 0x04000442 RID: 1090
		private NukeAbility fist;

		// Token: 0x04000443 RID: 1091
		private DisableAbility hex;

		// Token: 0x04000444 RID: 1092
		private ShieldAbility mjollnir;

		// Token: 0x04000445 RID: 1093
		private Nullifier nullifier;

		// Token: 0x04000446 RID: 1094
		private DisableAbility orchid;

		// Token: 0x04000447 RID: 1095
		private FireRemnant remnant;

		// Token: 0x04000448 RID: 1096
		private ActivateFireRemnant remnantActivate;

		// Token: 0x04000449 RID: 1097
		private ActivateFireRemnantBlink remnantActivateBlink;

		// Token: 0x0400044A RID: 1098
		private FireRemnantBlink remnantBlink;

		// Token: 0x0400044B RID: 1099
		private ShieldAbility shield;

		// Token: 0x0400044C RID: 1100
		private DebuffAbility shiva;

		// Token: 0x0400044D RID: 1101
		private DebuffAbility veil;
	}
}
