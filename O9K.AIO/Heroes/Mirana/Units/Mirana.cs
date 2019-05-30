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

namespace O9K.AIO.Heroes.Mirana.Units
{
	// Token: 0x02000102 RID: 258
	[UnitName("npc_dota_hero_mirana")]
	internal class Mirana : ControllableUnit
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x0001A66C File Offset: 0x0001886C
		public Mirana(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.mirana_starfall,
					(ActiveAbility x) => this.starstorm = new NukeAbility(x)
				},
				{
					AbilityId.mirana_arrow,
					(ActiveAbility x) => this.arrow = new DisableAbility(x)
				},
				{
					AbilityId.mirana_leap,
					(ActiveAbility x) => this.leap = new ForceStaff(x)
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
					AbilityId.item_manta,
					(ActiveAbility x) => this.manta = new BuffAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.mirana_leap, (ActiveAbility _) => this.leap);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001A748 File Offset: 0x00018948
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.starstorm, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.diffusal, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.blink, base.Owner.GetAttackRange(null, 0f), 350f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.arrow, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.leap, base.Owner.GetAttackRange(targetManager.Target, 0f) + 100f, 800f))
			{
				return true;
			}
			if (abilityHelper.UseForceStaffAway(this.leap, 200))
			{
				base.OrbwalkSleeper.Sleep(0.2f);
				base.ComboSleeper.Sleep(0.2f);
				this.leap.Sleeper.Sleep(2f);
				return true;
			}
			return abilityHelper.UseAbility(this.manta, base.Owner.GetAttackRange(null, 0f)) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00004AE2 File Offset: 0x00002CE2
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBlinks(abilityHelper) || abilityHelper.UseMoveAbility(this.leap);
		}

		// Token: 0x040002D1 RID: 721
		private DisableAbility arrow;

		// Token: 0x040002D2 RID: 722
		private BlinkAbility blink;

		// Token: 0x040002D3 RID: 723
		private DebuffAbility diffusal;

		// Token: 0x040002D4 RID: 724
		private ForceStaff leap;

		// Token: 0x040002D5 RID: 725
		private BuffAbility manta;

		// Token: 0x040002D6 RID: 726
		private SpeedBuffAbility phase;

		// Token: 0x040002D7 RID: 727
		private NukeAbility starstorm;
	}
}
