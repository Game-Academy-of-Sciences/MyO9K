using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.ShadowShaman.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.ShadowShaman.Units
{
	// Token: 0x020000AF RID: 175
	[UnitName("npc_dota_hero_shadow_shaman")]
	internal class ShadowShaman : ControllableUnit
	{
		// Token: 0x0600037C RID: 892 RVA: 0x00013E10 File Offset: 0x00012010
		public ShadowShaman(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.shadow_shaman_ether_shock,
					(ActiveAbility x) => this.shock = new NukeAbility(x)
				},
				{
					AbilityId.shadow_shaman_voodoo,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				},
				{
					AbilityId.shadow_shaman_shackles,
					(ActiveAbility x) => this.shackles = new DisableAbility(x)
				},
				{
					AbilityId.shadow_shaman_mass_serpent_ward,
					(ActiveAbility x) => this.wards = new Wards(x)
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
					AbilityId.item_cyclone,
					(ActiveAbility x) => this.euls = new EulsScepterOfDivinity(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.shadow_shaman_voodoo, (ActiveAbility _) => this.hex);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00013EE8 File Offset: 0x000120E8
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.hex, true) || (abilityHelper.CanBeCasted(this.wards, true, false, true, true) && abilityHelper.UseAbility(this.euls, true)) || abilityHelper.UseAbility(this.wards, true) || abilityHelper.UseAbility(this.shackles, true) || abilityHelper.UseAbility(this.blink, 500f, 300f) || abilityHelper.UseAbility(this.force, 500f, 300f) || abilityHelper.UseAbility(this.shock, true);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000040E9 File Offset: 0x000022E9
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseAbility(this.hex, true);
		}

		// Token: 0x040001EC RID: 492
		private BlinkAbility blink;

		// Token: 0x040001ED RID: 493
		private EulsScepterOfDivinity euls;

		// Token: 0x040001EE RID: 494
		private ForceStaff force;

		// Token: 0x040001EF RID: 495
		private DisableAbility hex;

		// Token: 0x040001F0 RID: 496
		private DisableAbility shackles;

		// Token: 0x040001F1 RID: 497
		private NukeAbility shock;

		// Token: 0x040001F2 RID: 498
		private Wards wards;
	}
}
