using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Brewmaster.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Brewmaster.Units
{
	// Token: 0x020001D6 RID: 470
	[UnitName("npc_dota_hero_brewmaster")]
	internal class Brewmaster : ControllableUnit
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x00029500 File Offset: 0x00027700
		public Brewmaster(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.brewmaster_thunder_clap,
					(ActiveAbility x) => this.clap = new NukeAbility(x)
				},
				{
					AbilityId.brewmaster_cinder_brew,
					(ActiveAbility x) => this.cinder = new DebuffAbility(x)
				},
				{
					AbilityId.brewmaster_primal_split,
					(ActiveAbility x) => this.split = new PrimalSplit(x)
				},
				{
					AbilityId.brewmaster_drunken_brawler,
					(ActiveAbility x) => this.brawler = new BuffAbility(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkDaggerAOE(x)
				},
				{
					AbilityId.item_abyssal_blade,
					(ActiveAbility x) => this.abyssal = new DisableAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.brewmaster_thunder_clap, (ActiveAbility _) => this.clap);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x000295DC File Offset: 0x000277DC
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.abyssal, true) || abilityHelper.UseAbility(this.cinder, true) || abilityHelper.UseAbility(this.clap, true) || abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
			{
				this.clap
			}) || abilityHelper.UseAbility(this.blink, 400f, 0f) || abilityHelper.UseAbility(this.split, true) || abilityHelper.UseAbility(this.brawler, 600f) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00006B72 File Offset: 0x00004D72
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseAbility(this.clap, true);
		}

		// Token: 0x040004F7 RID: 1271
		private DisableAbility abyssal;

		// Token: 0x040004F8 RID: 1272
		private BlinkDaggerAOE blink;

		// Token: 0x040004F9 RID: 1273
		private BuffAbility brawler;

		// Token: 0x040004FA RID: 1274
		private DebuffAbility cinder;

		// Token: 0x040004FB RID: 1275
		private NukeAbility clap;

		// Token: 0x040004FC RID: 1276
		private SpeedBuffAbility phase;

		// Token: 0x040004FD RID: 1277
		private PrimalSplit split;
	}
}
