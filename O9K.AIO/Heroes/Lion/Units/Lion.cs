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

namespace O9K.AIO.Heroes.Lion.Units
{
	// Token: 0x02000119 RID: 281
	[UnitName("npc_dota_hero_lion")]
	internal class Lion : ControllableUnit
	{
		// Token: 0x0600058B RID: 1419 RVA: 0x0001C074 File Offset: 0x0001A274
		public Lion(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.lion_impale,
					(ActiveAbility x) => this.impale = new DisableAbility(x)
				},
				{
					AbilityId.lion_mana_drain,
					(ActiveAbility x) => this.drain = new TargetableAbility(x)
				},
				{
					AbilityId.lion_voodoo,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				},
				{
					AbilityId.lion_finger_of_death,
					(ActiveAbility x) => this.finger = new NukeAbility(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_ethereal_blade,
					(ActiveAbility x) => this.ethereal = new EtherealBlade(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.lion_impale, (ActiveAbility _) => this.impale);
			base.MoveComboAbilities.Add(AbilityId.lion_voodoo, (ActiveAbility _) => this.hex);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001C16C File Offset: 0x0001A36C
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (!comboModeMenu.IsHarassCombo)
			{
				TargetableAbility targetableAbility = this.drain;
				if (targetableAbility != null && targetableAbility.Ability.IsChanneling && (abilityHelper.CanBeCasted(this.hex, true, true, false, true) || abilityHelper.CanBeCasted(this.impale, true, true, false, true)))
				{
					base.Owner.BaseUnit.Stop();
					base.ComboSleeper.Sleep(0.05f);
					return true;
				}
			}
			return abilityHelper.UseKillStealAbility(this.finger, true) || abilityHelper.UseAbility(this.hex, true) || abilityHelper.UseAbility(this.ethereal, true) || abilityHelper.UseAbility(this.impale, true) || abilityHelper.UseDoubleBlinkCombo(this.force, this.blink, 0f) || abilityHelper.UseAbility(this.blink, 550f, 350f) || abilityHelper.UseAbility(this.force, 550f, 350f) || abilityHelper.UseAbility(this.finger, true) || abilityHelper.UseAbility(this.drain, true);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00004EB4 File Offset: 0x000030B4
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseMoveAbility(this.hex) || abilityHelper.UseMoveAbility(this.impale);
		}

		// Token: 0x04000310 RID: 784
		private BlinkAbility blink;

		// Token: 0x04000311 RID: 785
		private TargetableAbility drain;

		// Token: 0x04000312 RID: 786
		private EtherealBlade ethereal;

		// Token: 0x04000313 RID: 787
		private NukeAbility finger;

		// Token: 0x04000314 RID: 788
		private ForceStaff force;

		// Token: 0x04000315 RID: 789
		private DisableAbility hex;

		// Token: 0x04000316 RID: 790
		private DisableAbility impale;
	}
}
