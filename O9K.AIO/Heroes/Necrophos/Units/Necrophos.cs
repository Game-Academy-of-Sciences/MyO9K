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

namespace O9K.AIO.Heroes.Necrophos.Units
{
	// Token: 0x020000EB RID: 235
	[UnitName("npc_dota_hero_necrolyte")]
	internal class Necrophos : ControllableUnit
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x00018D88 File Offset: 0x00016F88
		public Necrophos(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.necrolyte_death_pulse,
					(ActiveAbility x) => this.pulse = new NukeAbility(x)
				},
				{
					AbilityId.necrolyte_sadist,
					(ActiveAbility x) => this.shroud = new ShieldAbility(x)
				},
				{
					AbilityId.necrolyte_reapers_scythe,
					(ActiveAbility x) => this.scythe = new NukeAbility(x)
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
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
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
				},
				{
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				},
				{
					AbilityId.item_dagon_5,
					(ActiveAbility x) => this.dagon = new NukeAbility(x)
				}
			};
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00018EDC File Offset: 0x000170DC
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.blink, 700f, 300f))
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
			if (abilityHelper.UseAbility(this.veil, true))
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
			if (abilityHelper.UseAbility(this.dagon, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.shiva, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.pulse, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.scythe, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.shroud, 300f))
			{
				base.ComboSleeper.ExtendSleep(0.3f);
				return true;
			}
			return (!base.Owner.IsAttackImmune && !abilityHelper.CanBeCasted(this.shroud, true, true, true, true) && abilityHelper.UseAbility(this.bladeMail, 500f)) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x04000294 RID: 660
		private DisableAbility atos;

		// Token: 0x04000295 RID: 661
		private ShieldAbility bladeMail;

		// Token: 0x04000296 RID: 662
		private BlinkAbility blink;

		// Token: 0x04000297 RID: 663
		private DisableAbility bloodthorn;

		// Token: 0x04000298 RID: 664
		private NukeAbility dagon;

		// Token: 0x04000299 RID: 665
		private DisableAbility hex;

		// Token: 0x0400029A RID: 666
		private Nullifier nullifier;

		// Token: 0x0400029B RID: 667
		private DisableAbility orchid;

		// Token: 0x0400029C RID: 668
		private SpeedBuffAbility phase;

		// Token: 0x0400029D RID: 669
		private NukeAbility pulse;

		// Token: 0x0400029E RID: 670
		private NukeAbility scythe;

		// Token: 0x0400029F RID: 671
		private DebuffAbility shiva;

		// Token: 0x040002A0 RID: 672
		private ShieldAbility shroud;

		// Token: 0x040002A1 RID: 673
		private DebuffAbility veil;
	}
}
