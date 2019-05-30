using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.AntiMage.Abilities;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.AntiMage.Units
{
	// Token: 0x020001EA RID: 490
	[UnitName("npc_dota_hero_antimage")]
	internal class AntiMage : ControllableUnit
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x0002A538 File Offset: 0x00028738
		public AntiMage(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.antimage_blink,
					(ActiveAbility x) => this.blink = new AntiMageBlink(x)
				},
				{
					AbilityId.antimage_counterspell,
					(ActiveAbility x) => this.counterspell = new Counterspell(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
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
					AbilityId.item_nullifier,
					(ActiveAbility x) => this.nullifier = new Nullifier(x)
				},
				{
					AbilityId.item_black_king_bar,
					(ActiveAbility x) => this.bkb = new ShieldAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.antimage_blink, (ActiveAbility _) => this.blink);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002A614 File Offset: 0x00028814
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.abyssal, true) || abilityHelper.UseAbility(this.blink, true) || abilityHelper.UseAbility(this.nullifier, true) || abilityHelper.UseAbility(this.manta, base.Owner.GetAttackRange(null, 0f)) || abilityHelper.UseAbility(this.counterspell, true) || (!base.Owner.IsSpellShieldProtected && abilityHelper.UseAbility(this.bkb, 500f)) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00006D32 File Offset: 0x00004F32
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBlinks(abilityHelper) || abilityHelper.UseMoveAbility(this.blink);
		}

		// Token: 0x04000520 RID: 1312
		private DisableAbility abyssal;

		// Token: 0x04000521 RID: 1313
		private ShieldAbility bkb;

		// Token: 0x04000522 RID: 1314
		private AntiMageBlink blink;

		// Token: 0x04000523 RID: 1315
		private ShieldAbility counterspell;

		// Token: 0x04000524 RID: 1316
		private BuffAbility manta;

		// Token: 0x04000525 RID: 1317
		private Nullifier nullifier;

		// Token: 0x04000526 RID: 1318
		private SpeedBuffAbility phase;
	}
}
