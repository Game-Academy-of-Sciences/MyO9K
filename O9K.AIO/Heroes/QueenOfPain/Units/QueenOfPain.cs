using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.QueenOfPain.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.QueenOfPain.Units
{
	// Token: 0x020000C4 RID: 196
	[UnitName("npc_dota_hero_queenofpain")]
	internal class QueenOfPain : ControllableUnit
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x00015BB4 File Offset: 0x00013DB4
		public QueenOfPain(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.queenofpain_shadow_strike,
					(ActiveAbility x) => this.shadowStrike = new DebuffAbility(x)
				},
				{
					AbilityId.queenofpain_blink,
					(ActiveAbility x) => this.blink = new BlinkQueen(x)
				},
				{
					AbilityId.queenofpain_scream_of_pain,
					(ActiveAbility x) => this.scream = new NukeAbility(x)
				},
				{
					AbilityId.queenofpain_sonic_wave,
					(ActiveAbility x) => this.sonic = new SonicWave(x)
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
					AbilityId.item_veil_of_discord,
					(ActiveAbility x) => this.veil = new DebuffAbility(x)
				},
				{
					AbilityId.item_black_king_bar,
					(ActiveAbility x) => this.bkb = new ShieldAbility(x)
				},
				{
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.bloodthorn = new Bloodthorn(x)
				},
				{
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				},
				{
					AbilityId.item_mjollnir,
					(ActiveAbility x) => this.mjollnir = new ShieldAbility(x)
				},
				{
					AbilityId.item_nullifier,
					(ActiveAbility x) => this.nullifier = new Nullifier(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.queenofpain_blink, (ActiveAbility _) => this.blink);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00015CFC File Offset: 0x00013EFC
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.bkb, 600f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.hex, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.bloodthorn, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.orchid, true))
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
			if (abilityHelper.UseAbility(this.shiva, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.scream, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.mjollnir, 500f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.shadowStrike, true))
			{
				return true;
			}
			Unit9 target = targetManager.Target;
			return ((abilityHelper.CanBeCasted(this.shadowStrike, true, true, true, true) || abilityHelper.CanBeCasted(this.scream, true, true, true, true) || abilityHelper.CanBeCasted(this.sonic, true, true, true, true) || base.Owner.Distance(target) > base.Owner.GetAttackRange(target, 0f)) && abilityHelper.UseAbility(this.blink, true)) || abilityHelper.UseAbility(this.sonic, true);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000041FE File Offset: 0x000023FE
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBlinks(abilityHelper) || abilityHelper.UseMoveAbility(this.blink);
		}

		// Token: 0x0400022C RID: 556
		private ShieldAbility bkb;

		// Token: 0x0400022D RID: 557
		private BlinkQueen blink;

		// Token: 0x0400022E RID: 558
		private DisableAbility bloodthorn;

		// Token: 0x0400022F RID: 559
		private DisableAbility hex;

		// Token: 0x04000230 RID: 560
		private ShieldAbility mjollnir;

		// Token: 0x04000231 RID: 561
		private Nullifier nullifier;

		// Token: 0x04000232 RID: 562
		private DisableAbility orchid;

		// Token: 0x04000233 RID: 563
		private NukeAbility scream;

		// Token: 0x04000234 RID: 564
		private DebuffAbility shadowStrike;

		// Token: 0x04000235 RID: 565
		private DebuffAbility shiva;

		// Token: 0x04000236 RID: 566
		private NukeAbility sonic;

		// Token: 0x04000237 RID: 567
		private DebuffAbility veil;
	}
}
