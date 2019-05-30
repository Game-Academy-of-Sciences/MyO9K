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

namespace O9K.AIO.Heroes.Pugna.Units
{
	// Token: 0x020000C8 RID: 200
	[UnitName("npc_dota_hero_pugna")]
	internal class Pugna : ControllableUnit
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x00016060 File Offset: 0x00014260
		public Pugna(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.pugna_nether_blast,
					(ActiveAbility x) => this.blast = new NukeAbility(x)
				},
				{
					AbilityId.pugna_decrepify,
					(ActiveAbility x) => this.decrepify = new DebuffAbility(x)
				},
				{
					AbilityId.pugna_nether_ward,
					(ActiveAbility x) => this.ward = new AoeAbility(x)
				},
				{
					AbilityId.pugna_life_drain,
					(ActiveAbility x) => this.drain = new TargetableAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_dagon_5,
					(ActiveAbility x) => this.dagon = new NukeAbility(x)
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
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.bloodthorn = new Bloodthorn(x)
				},
				{
					AbilityId.item_nullifier,
					(ActiveAbility x) => this.nullifier = new Nullifier(x)
				},
				{
					AbilityId.item_rod_of_atos,
					(ActiveAbility x) => this.atos = new DisableAbility(x)
				}
			};
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00016190 File Offset: 0x00014390
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			TargetableAbility targetableAbility = this.drain;
			if (targetableAbility != null && targetableAbility.Ability.IsChanneling)
			{
				NukeAbility nukeAbility = this.dagon;
				if (nukeAbility != null && nukeAbility.Ability.CanBeCasted(false) && this.dagon.Ability.CanHit(targetManager.Target) && (float)this.dagon.Ability.GetDamage(targetManager.Target) > targetManager.Target.Health)
				{
					base.Owner.Stop();
					base.ComboSleeper.Sleep(0.1f);
					return true;
				}
			}
			if (abilityHelper.UseAbility(this.blink, 700f, 350f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.veil, true))
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
			if (abilityHelper.UseAbility(this.atos, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.decrepify, true))
			{
				base.ComboSleeper.ExtendSleep(0.1f);
				return true;
			}
			return abilityHelper.UseAbility(this.dagon, true) || abilityHelper.UseAbility(this.blast, true) || abilityHelper.UseAbility(this.dagon, true) || abilityHelper.UseAbility(this.ward, true) || abilityHelper.UseAbility(this.drain, true);
		}

		// Token: 0x04000238 RID: 568
		private NukeAbility blast;

		// Token: 0x04000239 RID: 569
		private BlinkAbility blink;

		// Token: 0x0400023A RID: 570
		private DisableAbility bloodthorn;

		// Token: 0x0400023B RID: 571
		private NukeAbility dagon;

		// Token: 0x0400023C RID: 572
		private DebuffAbility decrepify;

		// Token: 0x0400023D RID: 573
		private TargetableAbility drain;

		// Token: 0x0400023E RID: 574
		private DisableAbility hex;

		// Token: 0x0400023F RID: 575
		private Nullifier nullifier;

		// Token: 0x04000240 RID: 576
		private DisableAbility orchid;

		// Token: 0x04000241 RID: 577
		private DebuffAbility veil;

		// Token: 0x04000242 RID: 578
		private AoeAbility ward;

		// Token: 0x04000243 RID: 579
		private DisableAbility atos;
	}
}
