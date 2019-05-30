using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.FacelessVoid.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.FacelessVoid.Units
{
	// Token: 0x02000170 RID: 368
	[UnitName("npc_dota_hero_faceless_void")]
	internal class FacelessVoid : ControllableUnit
	{
		// Token: 0x0600078A RID: 1930 RVA: 0x00022E48 File Offset: 0x00021048
		public FacelessVoid(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.faceless_void_time_walk,
					(ActiveAbility x) => this.timeWalk = new TimeWalk(x)
				},
				{
					AbilityId.faceless_void_time_dilation,
					(ActiveAbility x) => this.dilation = new TimeDilation(x)
				},
				{
					AbilityId.faceless_void_chronosphere,
					(ActiveAbility x) => this.chronosphere = new Chronosphere(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
				},
				{
					AbilityId.item_mask_of_madness,
					(ActiveAbility x) => this.mom = new BuffAbility(x)
				},
				{
					AbilityId.item_mjollnir,
					(ActiveAbility x) => this.mjollnir = new ShieldAbility(x)
				},
				{
					AbilityId.item_manta,
					(ActiveAbility x) => this.manta = new BuffAbility(x)
				},
				{
					AbilityId.item_diffusal_blade,
					(ActiveAbility x) => this.diffusal = new DebuffAbility(x)
				},
				{
					AbilityId.item_black_king_bar,
					(ActiveAbility x) => this.bkb = new ShieldAbility(x)
				},
				{
					AbilityId.item_silver_edge,
					(ActiveAbility x) => this.silver = new BuffAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.faceless_void_time_walk, (ActiveAbility _) => this.timeWalk);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00022F68 File Offset: 0x00021168
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.bkb, 500f) || ((!abilityHelper.CanBeCasted(this.timeWalk, false, true, true, true) || base.Owner.Distance(targetManager.Target) < 400f) && abilityHelper.UseAbility(this.chronosphere, true)) || abilityHelper.UseAbilityIfCondition(this.timeWalk, new UsableAbility[]
			{
				this.chronosphere
			}) || abilityHelper.UseAbility(this.diffusal, true) || abilityHelper.UseAbility(this.dilation, true) || abilityHelper.UseAbility(this.silver, true) || abilityHelper.UseAbility(this.mjollnir, 600f) || abilityHelper.UseAbility(this.manta, 300f) || (!abilityHelper.CanBeCasted(this.chronosphere, false, false, true, true) && abilityHelper.UseAbilityIfNone(this.mom, new UsableAbility[]
			{
				this.timeWalk
			})) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00005CD0 File Offset: 0x00003ED0
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBlinks(abilityHelper) || abilityHelper.UseMoveAbility(this.timeWalk);
		}

		// Token: 0x04000422 RID: 1058
		private ShieldAbility bkb;

		// Token: 0x04000423 RID: 1059
		private Chronosphere chronosphere;

		// Token: 0x04000424 RID: 1060
		private DebuffAbility diffusal;

		// Token: 0x04000425 RID: 1061
		private TimeDilation dilation;

		// Token: 0x04000426 RID: 1062
		private BuffAbility manta;

		// Token: 0x04000427 RID: 1063
		private ShieldAbility mjollnir;

		// Token: 0x04000428 RID: 1064
		private BuffAbility mom;

		// Token: 0x04000429 RID: 1065
		private SpeedBuffAbility phase;

		// Token: 0x0400042A RID: 1066
		private BuffAbility silver;

		// Token: 0x0400042B RID: 1067
		private TimeWalk timeWalk;
	}
}
