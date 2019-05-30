using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Spectre.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Spectre.Units
{
	// Token: 0x0200009D RID: 157
	[UnitName("npc_dota_hero_spectre")]
	internal class Spectre : ControllableUnit
	{
		// Token: 0x0600030F RID: 783 RVA: 0x00012830 File Offset: 0x00010A30
		public Spectre(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.spectre_spectral_dagger,
					(ActiveAbility x) => this.dagger = new NukeAbility(x)
				},
				{
					AbilityId.spectre_reality,
					(ActiveAbility x) => this.reality = new Reality(x)
				},
				{
					AbilityId.spectre_haunt,
					(ActiveAbility x) => this.haunt = new Haunt(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
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
					AbilityId.item_manta,
					(ActiveAbility x) => this.manta = new BuffAbility(x)
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
					AbilityId.item_black_king_bar,
					(ActiveAbility x) => this.bkb = new ShieldAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.spectre_spectral_dagger, (ActiveAbility x) => this.daggerMove = new DaggerMove(x));
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001294C File Offset: 0x00010B4C
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.abyssal, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.diffusal, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.haunt, true))
			{
				return true;
			}
			Haunt haunt = this.haunt;
			return (haunt != null && haunt.Ability.TimeSinceCasted < (float)10 && abilityHelper.UseAbility(this.reality, true)) || abilityHelper.UseAbility(this.bkb, true) || abilityHelper.UseAbility(this.dagger, true) || abilityHelper.UseAbility(this.manta, base.Owner.GetAttackRange(null, 0f)) || abilityHelper.UseAbility(this.vessel, true) || abilityHelper.UseAbility(this.urn, true) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00003EE7 File Offset: 0x000020E7
		public override void EndCombo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			base.EndCombo(targetManager, comboModeMenu);
			if (this.reality != null)
			{
				this.reality.RealityUseOnFakeTarget = true;
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00003F05 File Offset: 0x00002105
		protected override bool MoveComboUseBuffs(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBuffs(abilityHelper) || abilityHelper.UseMoveAbility(this.daggerMove);
		}

		// Token: 0x040001B0 RID: 432
		private DisableAbility abyssal;

		// Token: 0x040001B1 RID: 433
		private ShieldAbility bkb;

		// Token: 0x040001B2 RID: 434
		private NukeAbility dagger;

		// Token: 0x040001B3 RID: 435
		private DaggerMove daggerMove;

		// Token: 0x040001B4 RID: 436
		private DebuffAbility diffusal;

		// Token: 0x040001B5 RID: 437
		private Haunt haunt;

		// Token: 0x040001B6 RID: 438
		private BuffAbility manta;

		// Token: 0x040001B7 RID: 439
		private SpeedBuffAbility phase;

		// Token: 0x040001B8 RID: 440
		private Reality reality;

		// Token: 0x040001B9 RID: 441
		private DebuffAbility urn;

		// Token: 0x040001BA RID: 442
		private DebuffAbility vessel;
	}
}
