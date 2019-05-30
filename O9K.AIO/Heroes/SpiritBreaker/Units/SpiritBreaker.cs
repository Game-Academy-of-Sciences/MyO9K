using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.SpiritBreaker.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.SpiritBreaker.Units
{
	// Token: 0x02000045 RID: 69
	[UnitName("npc_dota_hero_spirit_breaker")]
	internal class SpiritBreaker : ControllableUnit
	{
		// Token: 0x06000181 RID: 385 RVA: 0x0000D618 File Offset: 0x0000B818
		public SpiritBreaker(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.spirit_breaker_charge_of_darkness,
					(ActiveAbility x) => this.charge = new ChargeOfDarkness(x)
				},
				{
					AbilityId.spirit_breaker_bulldoze,
					(ActiveAbility x) => this.bulldoze = new SpeedBuffAbility(x)
				},
				{
					AbilityId.spirit_breaker_nether_strike,
					(ActiveAbility x) => this.strike = new NukeAbility(x)
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
					AbilityId.item_spirit_vessel,
					(ActiveAbility x) => this.vessel = new DebuffAbility(x)
				},
				{
					AbilityId.item_urn_of_shadows,
					(ActiveAbility x) => this.urn = new DebuffAbility(x)
				},
				{
					AbilityId.item_mask_of_madness,
					(ActiveAbility x) => this.mom = new BuffAbility(x)
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
					AbilityId.item_heavens_halberd,
					(ActiveAbility x) => this.halberd = new DisableAbility(x)
				}
			};
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000D744 File Offset: 0x0000B944
		public void ChargeAway(TargetManager targetManager)
		{
			ChargeOfDarkness chargeOfDarkness = this.charge;
			if (chargeOfDarkness == null || !chargeOfDarkness.Ability.CanBeCasted(true))
			{
				return;
			}
			Unit9 unit = (from x in targetManager.EnemyUnits
			orderby x.IsHero, x.Distance(base.Owner) descending
			select x).FirstOrDefault((Unit9 x) => x.Distance(base.Owner) > 2000f);
			if (unit == null)
			{
				return;
			}
			this.charge.Ability.UseAbility(unit, false, false);
			base.OrbwalkSleeper.Sleep(0.5f);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			if (base.Owner.IsCharging)
			{
				return false;
			}
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.nullifier, true) || abilityHelper.UseAbility(this.bloodthorn, true) || abilityHelper.UseAbility(this.orchid, true) || abilityHelper.UseAbility(this.halberd, true) || abilityHelper.UseAbility(this.charge, true) || abilityHelper.UseAbility(this.bulldoze, 500f) || abilityHelper.UseAbility(this.bladeMail, 400f) || abilityHelper.UseAbility(this.vessel, true) || abilityHelper.UseAbility(this.urn, true) || abilityHelper.UseAbility(this.strike, true) || abilityHelper.UseAbilityIfNone(this.mom, new UsableAbility[]
			{
				this.charge,
				this.bulldoze,
				this.strike
			}) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00003138 File Offset: 0x00001338
		public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			return !base.Owner.IsCharging && base.Orbwalk(target, attack, move, comboMenu);
		}

		// Token: 0x040000D9 RID: 217
		private ShieldAbility bladeMail;

		// Token: 0x040000DA RID: 218
		private SpeedBuffAbility bulldoze;

		// Token: 0x040000DB RID: 219
		private ChargeOfDarkness charge;

		// Token: 0x040000DC RID: 220
		private BuffAbility mom;

		// Token: 0x040000DD RID: 221
		private SpeedBuffAbility phase;

		// Token: 0x040000DE RID: 222
		private NukeAbility strike;

		// Token: 0x040000DF RID: 223
		private DisableAbility bloodthorn;

		// Token: 0x040000E0 RID: 224
		private DisableAbility orchid;

		// Token: 0x040000E1 RID: 225
		private DebuffAbility urn;

		// Token: 0x040000E2 RID: 226
		private Nullifier nullifier;

		// Token: 0x040000E3 RID: 227
		private DisableAbility halberd;

		// Token: 0x040000E4 RID: 228
		private DebuffAbility vessel;
	}
}
