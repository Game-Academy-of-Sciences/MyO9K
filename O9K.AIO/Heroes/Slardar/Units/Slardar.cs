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

namespace O9K.AIO.Heroes.Slardar.Units
{
	// Token: 0x020000A7 RID: 167
	[UnitName("npc_dota_hero_slardar")]
	internal class Slardar : ControllableUnit
	{
		// Token: 0x06000348 RID: 840 RVA: 0x00013284 File Offset: 0x00011484
		public Slardar(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.slardar_sprint,
					(ActiveAbility x) => this.sprint = new BuffAbility(x)
				},
				{
					AbilityId.slardar_slithereen_crush,
					(ActiveAbility x) => this.crush = new DisableAbility(x)
				},
				{
					AbilityId.slardar_amplify_damage,
					(ActiveAbility x) => this.amplify = new DebuffAbility(x)
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
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_blade_mail,
					(ActiveAbility x) => this.bladeMail = new ShieldAbility(x)
				},
				{
					AbilityId.item_black_king_bar,
					(ActiveAbility x) => this.bkb = new ShieldAbility(x)
				},
				{
					AbilityId.item_armlet,
					(ActiveAbility x) => this.armlet = new BuffAbility(x)
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
					AbilityId.item_heavens_halberd,
					(ActiveAbility x) => this.halberd = new DisableAbility(x)
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
				}
			};
			base.MoveComboAbilities.Add(AbilityId.slardar_slithereen_crush, (ActiveAbility _) => this.crush);
			base.MoveComboAbilities.Add(AbilityId.slardar_sprint, (ActiveAbility _) => this.sprint);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00013428 File Offset: 0x00011628
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseDoubleBlinkCombo(this.force, this.blink, 0f) || abilityHelper.UseAbility(this.bloodthorn, true) || abilityHelper.UseAbility(this.orchid, true) || abilityHelper.UseAbility(this.nullifier, true) || (abilityHelper.CanBeCastedIfCondition(this.blink, new UsableAbility[]
			{
				this.crush
			}) && abilityHelper.UseAbility(this.bkb, true)) || abilityHelper.UseAbility(this.crush, true) || abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
			{
				this.crush
			}) || abilityHelper.UseAbility(this.force, 500f, 0f) || abilityHelper.UseAbility(this.sprint, true) || abilityHelper.UseAbility(this.solar, true) || abilityHelper.UseAbility(this.halberd, true) || abilityHelper.UseAbility(this.medallion, true) || abilityHelper.UseAbility(this.bladeMail, 400f) || abilityHelper.UseAbility(this.armlet, 400f) || (!abilityHelper.CanBeCasted(this.crush, true, true, true, true) && abilityHelper.UseAbility(this.amplify, true)) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00003FF5 File Offset: 0x000021F5
		protected override bool MoveComboUseBuffs(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBuffs(abilityHelper) || abilityHelper.UseMoveAbility(this.sprint);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00004013 File Offset: 0x00002213
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseAbility(this.crush, true);
		}

		// Token: 0x040001CE RID: 462
		private Nullifier nullifier;

		// Token: 0x040001CF RID: 463
		private DebuffAbility amplify;

		// Token: 0x040001D0 RID: 464
		private BuffAbility armlet;

		// Token: 0x040001D1 RID: 465
		private ShieldAbility bkb;

		// Token: 0x040001D2 RID: 466
		private ShieldAbility bladeMail;

		// Token: 0x040001D3 RID: 467
		private BlinkDaggerAOE blink;

		// Token: 0x040001D4 RID: 468
		private DisableAbility crush;

		// Token: 0x040001D5 RID: 469
		private ForceStaff force;

		// Token: 0x040001D6 RID: 470
		private DisableAbility halberd;

		// Token: 0x040001D7 RID: 471
		private DebuffAbility medallion;

		// Token: 0x040001D8 RID: 472
		private SpeedBuffAbility phase;

		// Token: 0x040001D9 RID: 473
		private DisableAbility orchid;

		// Token: 0x040001DA RID: 474
		private DisableAbility bloodthorn;

		// Token: 0x040001DB RID: 475
		private DebuffAbility solar;

		// Token: 0x040001DC RID: 476
		private BuffAbility sprint;
	}
}
