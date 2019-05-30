using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Gyrocopter.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Gyrocopter.Units
{
	// Token: 0x02000135 RID: 309
	[UnitName("npc_dota_hero_gyrocopter")]
	internal class Gyrocopter : ControllableUnit
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x0001E16C File Offset: 0x0001C36C
		public Gyrocopter(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.gyrocopter_rocket_barrage,
					(ActiveAbility x) => this.barrage = new NukeAbility(x)
				},
				{
					AbilityId.gyrocopter_homing_missile,
					(ActiveAbility x) => this.missile = new NukeAbility(x)
				},
				{
					AbilityId.gyrocopter_flak_cannon,
					(ActiveAbility x) => this.flak = new FlakCannon(x)
				},
				{
					AbilityId.gyrocopter_call_down,
					(ActiveAbility x) => this.callDown = new NukeAbility(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
				},
				{
					AbilityId.item_hurricane_pike,
					(ActiveAbility x) => this.pike = new HurricanePike(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_manta,
					(ActiveAbility x) => this.manta = new BuffAbility(x)
				},
				{
					AbilityId.item_mjollnir,
					(ActiveAbility x) => this.mjollnir = new ShieldAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.gyrocopter_homing_missile, (ActiveAbility _) => this.missile);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001E274 File Offset: 0x0001C474
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.missile, true) || abilityHelper.UseAbility(this.callDown, true) || abilityHelper.UseAbility(this.force, 500f, 300f) || abilityHelper.UseAbility(this.pike, 500f, 300f) || abilityHelper.UseAbility(this.flak, true) || abilityHelper.UseAbility(this.barrage, true) || abilityHelper.UseAbility(this.manta, base.Owner.GetAttackRange(null, 0f)) || abilityHelper.UseAbility(this.mjollnir, 600f) || (abilityHelper.CanBeCasted(this.pike, true, true, true, true) && !base.MoveSleeper.IsSleeping && this.pike.UseAbilityOnTarget(targetManager, base.ComboSleeper)) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001E380 File Offset: 0x0001C580
		public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			if (target != null && base.Owner.HasModifier("modifier_gyrocopter_rocket_barrage") && base.Owner.Distance(target) > this.barrage.Ability.Radius / 2f)
			{
				return base.Move(target.Position);
			}
			return base.Orbwalk(target, attack, move, comboMenu);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00005320 File Offset: 0x00003520
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseAbility(this.missile, true);
		}

		// Token: 0x0400035D RID: 861
		private NukeAbility barrage;

		// Token: 0x0400035E RID: 862
		private NukeAbility callDown;

		// Token: 0x0400035F RID: 863
		private FlakCannon flak;

		// Token: 0x04000360 RID: 864
		private ForceStaff force;

		// Token: 0x04000361 RID: 865
		private BuffAbility manta;

		// Token: 0x04000362 RID: 866
		private NukeAbility missile;

		// Token: 0x04000363 RID: 867
		private ShieldAbility mjollnir;

		// Token: 0x04000364 RID: 868
		private SpeedBuffAbility phase;

		// Token: 0x04000365 RID: 869
		private HurricanePike pike;
	}
}
