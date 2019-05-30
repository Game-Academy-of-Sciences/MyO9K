using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.MonkeyKing.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.MonkeyKing.Units
{
	// Token: 0x020000FA RID: 250
	[UnitName("npc_dota_hero_monkey_king")]
	internal class MonkeyKing : ControllableUnit
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x00019DF4 File Offset: 0x00017FF4
		public MonkeyKing(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.monkey_king_boundless_strike,
					(ActiveAbility x) => this.strike = new NukeAbility(x)
				},
				{
					AbilityId.monkey_king_tree_dance,
					(ActiveAbility x) => this.treeDance = new TreeDance(x)
				},
				{
					AbilityId.monkey_king_primal_spring,
					(ActiveAbility x) => this.spring = new PrimalSpring(x)
				},
				{
					AbilityId.monkey_king_wukongs_command,
					(ActiveAbility x) => this.command = new WukongsCommand(x)
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
				}
			};
			base.MoveComboAbilities.Add(AbilityId.monkey_king_tree_dance, (ActiveAbility _) => this.treeDance);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00019ED4 File Offset: 0x000180D4
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (this.spring.CancelChanneling(targetManager))
			{
				base.ComboSleeper.Sleep(0.1f);
				return true;
			}
			if (abilityHelper.UseAbility(this.abyssal, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.diffusal, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.spring, true))
			{
				return true;
			}
			if (abilityHelper.CanBeCastedHidden(this.spring) && abilityHelper.UseAbility(this.treeDance, true))
			{
				return true;
			}
			Unit9 target = targetManager.Target;
			return (!target.IsRooted && !target.IsStunned && !target.IsHexed && abilityHelper.UseAbility(this.treeDance, 500f, 0f)) || ((base.Owner.HasModifier("modifier_monkey_king_quadruple_tap_bonuses") || (target.Distance(base.Owner) > 600f && target.HealthPercentage < 30f)) && abilityHelper.UseAbility(this.strike, true)) || abilityHelper.UseAbility(this.command, true) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00019FFC File Offset: 0x000181FC
		public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			return (!(target != null) || !this.spring.CanHit(target, comboMenu)) && (!this.spring.Ability.IsUsable || this.treeDance.Ability.IsReady) && base.Orbwalk(target, attack, move, comboMenu);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000494D File Offset: 0x00002B4D
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBlinks(abilityHelper) || abilityHelper.UseMoveAbility(this.treeDance);
		}

		// Token: 0x040002BE RID: 702
		private DisableAbility abyssal;

		// Token: 0x040002BF RID: 703
		private WukongsCommand command;

		// Token: 0x040002C0 RID: 704
		private DebuffAbility diffusal;

		// Token: 0x040002C1 RID: 705
		private SpeedBuffAbility phase;

		// Token: 0x040002C2 RID: 706
		private PrimalSpring spring;

		// Token: 0x040002C3 RID: 707
		private NukeAbility strike;

		// Token: 0x040002C4 RID: 708
		private TreeDance treeDance;
	}
}
