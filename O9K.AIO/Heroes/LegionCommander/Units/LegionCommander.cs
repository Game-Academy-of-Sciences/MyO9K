using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.LegionCommander.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.LegionCommander.Units
{
	// Token: 0x02000124 RID: 292
	[UnitName("npc_dota_hero_legion_commander")]
	internal class LegionCommander : ControllableUnit
	{
		// Token: 0x060005CD RID: 1485 RVA: 0x0001CE34 File Offset: 0x0001B034
		public LegionCommander(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.legion_commander_overwhelming_odds,
					(ActiveAbility x) => this.odds = new OverwhelmingOdds(x)
				},
				{
					AbilityId.legion_commander_press_the_attack,
					(ActiveAbility x) => this.attack = new BuffAbility(x)
				},
				{
					AbilityId.legion_commander_duel,
					(ActiveAbility x) => this.duel = new Duel(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new LegionBlink(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
				},
				{
					AbilityId.item_blade_mail,
					(ActiveAbility x) => this.bladeMail = new ShieldAbility(x)
				},
				{
					AbilityId.item_black_king_bar,
					(ActiveAbility x) => this.bkb = new ShieldAbility(x)
				},
				{
					AbilityId.item_heavens_halberd,
					(ActiveAbility x) => this.halberd = new DisableAbility(x)
				},
				{
					AbilityId.item_mjollnir,
					(ActiveAbility x) => this.mjollnir = new ShieldAbility(x)
				},
				{
					AbilityId.item_armlet,
					(ActiveAbility x) => this.armlet = new BuffAbility(x)
				},
				{
					AbilityId.item_abyssal_blade,
					(ActiveAbility x) => this.abyssal = new DisableAbility(x)
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
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001CF8C File Offset: 0x0001B18C
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			float num = base.Owner.Distance(targetManager.Target);
			if (abilityHelper.CanBeCasted(this.duel, false, false, true, true) && ((num <= 1400f && abilityHelper.CanBeCasted(this.blink, true, true, true, true)) || num < 500f))
			{
				if (abilityHelper.CanBeCasted(this.attack, false, true, true, true) && abilityHelper.ForceUseAbility(this.attack, false, true))
				{
					return true;
				}
				if (abilityHelper.CanBeCasted(this.bladeMail, false, true, true, true) && abilityHelper.ForceUseAbility(this.bladeMail, false, true))
				{
					return true;
				}
				if (abilityHelper.CanBeCasted(this.bkb, false, true, true, true) && abilityHelper.ForceUseAbility(this.bkb, false, true))
				{
					return true;
				}
				if (abilityHelper.CanBeCasted(this.mjollnir, false, true, true, true) && abilityHelper.ForceUseAbility(this.mjollnir, false, true))
				{
					return true;
				}
				if (abilityHelper.CanBeCasted(this.armlet, false, true, true, true) && abilityHelper.ForceUseAbility(this.armlet, false, true))
				{
					return true;
				}
				if (!abilityHelper.CanBeCasted(this.duel, true, true, true, true) && abilityHelper.CanBeCasted(this.blink, false, true, true, true) && abilityHelper.ForceUseAbility(this.blink, false, true))
				{
					return true;
				}
			}
			if (abilityHelper.UseAbility(this.abyssal, true))
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
			if (abilityHelper.UseAbility(this.halberd, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.bkb, 500f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.bladeMail, 500f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.mjollnir, 400f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.attack, 400f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.armlet, 300f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.duel, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.blink, 300f, 0f))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.odds, true, true, true, true) && !abilityHelper.CanBeCasted(this.duel, true, true, true, true) && !abilityHelper.CanBeCasted(this.blink, true, true, true, true))
			{
				BlinkAbility blinkAbility = this.blink;
				if ((blinkAbility == null || !blinkAbility.Sleeper.IsSleeping) && abilityHelper.UseAbility(this.odds, true))
				{
					return true;
				}
			}
			return abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x04000334 RID: 820
		private DisableAbility abyssal;

		// Token: 0x04000335 RID: 821
		private BuffAbility armlet;

		// Token: 0x04000336 RID: 822
		private BuffAbility attack;

		// Token: 0x04000337 RID: 823
		private ShieldAbility bkb;

		// Token: 0x04000338 RID: 824
		private ShieldAbility bladeMail;

		// Token: 0x04000339 RID: 825
		private BlinkAbility blink;

		// Token: 0x0400033A RID: 826
		private DisableAbility bloodthorn;

		// Token: 0x0400033B RID: 827
		private Duel duel;

		// Token: 0x0400033C RID: 828
		private DisableAbility halberd;

		// Token: 0x0400033D RID: 829
		private ShieldAbility mjollnir;

		// Token: 0x0400033E RID: 830
		private Nullifier nullifier;

		// Token: 0x0400033F RID: 831
		private NukeAbility odds;

		// Token: 0x04000340 RID: 832
		private DisableAbility orchid;

		// Token: 0x04000341 RID: 833
		private SpeedBuffAbility phase;
	}
}
