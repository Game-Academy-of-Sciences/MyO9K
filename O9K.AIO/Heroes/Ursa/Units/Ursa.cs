using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Ursa.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Ursa.Units
{
	// Token: 0x0200005F RID: 95
	[UnitName("npc_dota_hero_ursa")]
	internal class Ursa : ControllableUnit
	{
		// Token: 0x060001FE RID: 510 RVA: 0x0000EB64 File Offset: 0x0000CD64
		public Ursa(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.ursa_earthshock,
					(ActiveAbility x) => this.shock = new DebuffAbility(x)
				},
				{
					AbilityId.ursa_overpower,
					(ActiveAbility x) => this.overpower = new BuffAbility(x)
				},
				{
					AbilityId.ursa_enrage,
					(ActiveAbility x) => this.enrage = new Enrage(x)
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
					AbilityId.item_abyssal_blade,
					(ActiveAbility x) => this.abyssal = new DisableAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_black_king_bar,
					(ActiveAbility x) => this.bkb = new ShieldAbility(x)
				}
			};
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000EC78 File Offset: 0x0000CE78
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.bkb, 400f) || abilityHelper.UseAbility(this.abyssal, true) || abilityHelper.UseAbility(this.orchid, true) || abilityHelper.UseAbility(this.bloodthorn, true) || abilityHelper.UseAbility(this.nullifier, true) || abilityHelper.UseAbility(this.diffusal, true) || abilityHelper.UseAbility(this.overpower, true) || abilityHelper.UseAbility(this.shock, true) || abilityHelper.UseAbility(this.blink, 400f, 0f) || abilityHelper.UseAbility(this.enrage, 300f) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x04000112 RID: 274
		private DisableAbility abyssal;

		// Token: 0x04000113 RID: 275
		private ShieldAbility bkb;

		// Token: 0x04000114 RID: 276
		private BlinkAbility blink;

		// Token: 0x04000115 RID: 277
		private DisableAbility bloodthorn;

		// Token: 0x04000116 RID: 278
		private DebuffAbility diffusal;

		// Token: 0x04000117 RID: 279
		private ShieldAbility enrage;

		// Token: 0x04000118 RID: 280
		private Nullifier nullifier;

		// Token: 0x04000119 RID: 281
		private DisableAbility orchid;

		// Token: 0x0400011A RID: 282
		private BuffAbility overpower;

		// Token: 0x0400011B RID: 283
		private SpeedBuffAbility phase;

		// Token: 0x0400011C RID: 284
		private DebuffAbility shock;
	}
}
