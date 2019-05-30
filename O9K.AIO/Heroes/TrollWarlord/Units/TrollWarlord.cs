using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.TrollWarlord.Units
{
	// Token: 0x0200006B RID: 107
	[UnitName("npc_dota_hero_troll_warlord")]
	internal class TrollWarlord : ControllableUnit
	{
		// Token: 0x06000230 RID: 560 RVA: 0x0000F728 File Offset: 0x0000D928
		public TrollWarlord(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.troll_warlord_berserkers_rage,
					(ActiveAbility x) => this.rage = new UntargetableAbility(x)
				},
				{
					AbilityId.troll_warlord_whirling_axes_melee,
					(ActiveAbility x) => this.axeMelee = new NukeAbility(x)
				},
				{
					AbilityId.troll_warlord_whirling_axes_ranged,
					(ActiveAbility x) => this.axeRanged = new NukeAbility(x)
				},
				{
					AbilityId.troll_warlord_battle_trance,
					(ActiveAbility x) => this.trance = new BuffAbility(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
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
				},
				{
					AbilityId.item_diffusal_blade,
					(ActiveAbility x) => this.diffusal = new DebuffAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_mask_of_madness,
					(ActiveAbility x) => this.mom = new BuffAbility(x)
				},
				{
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				},
				{
					AbilityId.item_abyssal_blade,
					(ActiveAbility x) => this.abyssal = new DisableAbility(x)
				}
			};
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000F86C File Offset: 0x0000DA6C
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			Unit9 target = targetManager.Target;
			float num = base.Owner.Distance(target);
			float attackRange = base.Owner.GetAttackRange(target, 0f);
			if (num < 225f || num > 575f)
			{
				if (base.Owner.IsRanged && abilityHelper.UseAbility(this.rage, true))
				{
					return true;
				}
			}
			else if (!base.Owner.IsRanged && abilityHelper.UseAbility(this.rage, true))
			{
				return true;
			}
			return abilityHelper.UseAbility(this.blink, 550f, 0f) || abilityHelper.UseAbility(this.abyssal, true) || abilityHelper.UseAbility(this.hex, true) || abilityHelper.UseAbility(this.diffusal, true) || abilityHelper.UseAbility(this.orchid, true) || abilityHelper.UseAbility(this.bloodthorn, true) || abilityHelper.UseAbility(this.nullifier, true) || abilityHelper.UseAbility(this.axeMelee, true) || (!base.Owner.IsRanged && num > 200f && abilityHelper.CanBeCastedHidden(this.axeRanged) && this.axeRanged.CanHit(targetManager, comboModeMenu) && abilityHelper.UseAbility(this.rage, true)) || abilityHelper.UseAbility(this.axeRanged, true) || abilityHelper.UseAbility(this.trance, attackRange) || abilityHelper.UseAbility(this.phase, true) || (!base.Owner.IsRanged && abilityHelper.UseAbilityIfNone(this.mom, new UsableAbility[]
			{
				this.axeMelee,
				this.trance
			}));
		}

		// Token: 0x0400012A RID: 298
		private NukeAbility axeMelee;

		// Token: 0x0400012B RID: 299
		private NukeAbility axeRanged;

		// Token: 0x0400012C RID: 300
		private BlinkAbility blink;

		// Token: 0x0400012D RID: 301
		private DisableAbility bloodthorn;

		// Token: 0x0400012E RID: 302
		private DebuffAbility diffusal;

		// Token: 0x0400012F RID: 303
		private DisableAbility hex;

		// Token: 0x04000130 RID: 304
		private BuffAbility mom;

		// Token: 0x04000131 RID: 305
		private Nullifier nullifier;

		// Token: 0x04000132 RID: 306
		private DisableAbility orchid;

		// Token: 0x04000133 RID: 307
		private SpeedBuffAbility phase;

		// Token: 0x04000134 RID: 308
		private UntargetableAbility rage;

		// Token: 0x04000135 RID: 309
		private BuffAbility trance;

		// Token: 0x04000136 RID: 310
		private DisableAbility abyssal;
	}
}
