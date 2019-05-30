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

namespace O9K.AIO.Heroes.VengefulSpirit.Units
{
	// Token: 0x0200005D RID: 93
	[UnitName("npc_dota_hero_vengefulspirit")]
	internal class VengefulSpirit : ControllableUnit
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x0000E89C File Offset: 0x0000CA9C
		public VengefulSpirit(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.vengefulspirit_magic_missile,
					(ActiveAbility x) => this.missile = new DisableAbility(x)
				},
				{
					AbilityId.vengefulspirit_wave_of_terror,
					(ActiveAbility x) => this.wave = new DebuffAbility(x)
				},
				{
					AbilityId.vengefulspirit_nether_swap,
					(ActiveAbility x) => this.swap = new TargetableAbility(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_hurricane_pike,
					(ActiveAbility x) => this.pike = new HurricanePike(x)
				},
				{
					AbilityId.item_solar_crest,
					(ActiveAbility x) => this.solar = new DebuffAbility(x)
				},
				{
					AbilityId.item_medallion_of_courage,
					(ActiveAbility x) => this.medallion = new DebuffAbility(x)
				},
				{
					AbilityId.item_spirit_vessel,
					(ActiveAbility x) => this.vessel = new DebuffAbility(x)
				},
				{
					AbilityId.item_urn_of_shadows,
					(ActiveAbility x) => this.urn = new DebuffAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.lion_impale, (ActiveAbility _) => this.missile);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.missile, true) || abilityHelper.UseAbility(this.swap, true) || abilityHelper.UseAbility(this.vessel, true) || abilityHelper.UseAbility(this.urn, true) || abilityHelper.UseAbility(this.wave, true) || abilityHelper.UseAbility(this.solar, true) || abilityHelper.UseAbility(this.medallion, true) || abilityHelper.UseAbility(this.force, 550f, 400f) || abilityHelper.UseAbility(this.pike, 550f, 400f);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000035A4 File Offset: 0x000017A4
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseMoveAbility(this.missile);
		}

		// Token: 0x04000109 RID: 265
		private ForceStaff force;

		// Token: 0x0400010A RID: 266
		private DebuffAbility medallion;

		// Token: 0x0400010B RID: 267
		private DisableAbility missile;

		// Token: 0x0400010C RID: 268
		private HurricanePike pike;

		// Token: 0x0400010D RID: 269
		private DebuffAbility solar;

		// Token: 0x0400010E RID: 270
		private TargetableAbility swap;

		// Token: 0x0400010F RID: 271
		private DebuffAbility urn;

		// Token: 0x04000110 RID: 272
		private DebuffAbility vessel;

		// Token: 0x04000111 RID: 273
		private DebuffAbility wave;
	}
}
