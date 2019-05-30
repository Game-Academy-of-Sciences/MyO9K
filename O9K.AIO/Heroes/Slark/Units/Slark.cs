using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Slark.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Slark.Units
{
	// Token: 0x020000A3 RID: 163
	[UnitName("npc_dota_hero_slark")]
	internal class Slark : ControllableUnit
	{
		// Token: 0x06000330 RID: 816 RVA: 0x00012D94 File Offset: 0x00010F94
		public Slark(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.slark_dark_pact,
					(ActiveAbility x) => this.pact = new NukeAbility(x)
				},
				{
					AbilityId.slark_pounce,
					(ActiveAbility x) => this.pounce = new Pounce(x)
				},
				{
					AbilityId.slark_shadow_dance,
					(ActiveAbility x) => this.dance = new ShadowDance(x)
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
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				},
				{
					AbilityId.item_orchid,
					(ActiveAbility x) => this.orchid = new DisableAbility(x)
				},
				{
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.bloodthorn = new Bloodthorn(x)
				},
				{
					AbilityId.item_nullifier,
					(ActiveAbility x) => this.nullifier = new Nullifier(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_diffusal_blade,
					(ActiveAbility x) => this.diffusal = new DebuffAbility(x)
				},
				{
					AbilityId.item_silver_edge,
					(ActiveAbility x) => this.silver = new BuffAbility(x)
				},
				{
					AbilityId.item_invis_sword,
					(ActiveAbility x) => this.shadow = new BuffAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.slark_pounce, (ActiveAbility x) => this.pounceBlink = new ForceStaff(x));
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00003F91 File Offset: 0x00002191
		public override bool IsInvisible
		{
			get
			{
				return base.Owner.IsInvisible && !base.Owner.HasModifier("modifier_slark_shadow_dance");
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00012EF4 File Offset: 0x000110F4
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.hex, true) || abilityHelper.UseAbility(this.abyssal, true) || abilityHelper.UseAbility(this.orchid, true) || abilityHelper.UseAbility(this.bloodthorn, true) || abilityHelper.UseAbility(this.blink, 400f, 0f) || abilityHelper.UseAbility(this.nullifier, true) || abilityHelper.UseAbility(this.diffusal, true) || abilityHelper.UseAbility(this.pounce, true) || abilityHelper.UseAbility(this.silver, true) || abilityHelper.UseAbility(this.shadow, true) || abilityHelper.UseAbility(this.pact, true) || abilityHelper.UseAbility(this.dance, true) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00003FB5 File Offset: 0x000021B5
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBlinks(abilityHelper) || abilityHelper.UseMoveAbility(this.pounceBlink);
		}

		// Token: 0x040001C0 RID: 448
		private DisableAbility abyssal;

		// Token: 0x040001C1 RID: 449
		private BlinkAbility blink;

		// Token: 0x040001C2 RID: 450
		private DisableAbility bloodthorn;

		// Token: 0x040001C3 RID: 451
		private ShieldAbility dance;

		// Token: 0x040001C4 RID: 452
		private DebuffAbility diffusal;

		// Token: 0x040001C5 RID: 453
		private DisableAbility hex;

		// Token: 0x040001C6 RID: 454
		private Nullifier nullifier;

		// Token: 0x040001C7 RID: 455
		private DisableAbility orchid;

		// Token: 0x040001C8 RID: 456
		private NukeAbility pact;

		// Token: 0x040001C9 RID: 457
		private SpeedBuffAbility phase;

		// Token: 0x040001CA RID: 458
		private Pounce pounce;

		// Token: 0x040001CB RID: 459
		private ForceStaff pounceBlink;

		// Token: 0x040001CC RID: 460
		private BuffAbility shadow;

		// Token: 0x040001CD RID: 461
		private BuffAbility silver;
	}
}
