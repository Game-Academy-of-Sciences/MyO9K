using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Razor.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Razor.Units
{
	// Token: 0x020000C1 RID: 193
	[UnitName("npc_dota_hero_razor")]
	internal class Razor : ControllableUnit
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x00015880 File Offset: 0x00013A80
		public Razor(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.razor_plasma_field,
					(ActiveAbility x) => this.plasma = new NukeAbility(x)
				},
				{
					AbilityId.razor_static_link,
					(ActiveAbility x) => this.link = new StaticLink(x)
				},
				{
					AbilityId.razor_eye_of_the_storm,
					(ActiveAbility x) => this.storm = new AoeAbility(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
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
					AbilityId.item_blade_mail,
					(ActiveAbility x) => this.bladeMail = new ShieldAbility(x)
				},
				{
					AbilityId.item_black_king_bar,
					(ActiveAbility x) => this.bkb = new ShieldAbility(x)
				},
				{
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				},
				{
					AbilityId.item_heavens_halberd,
					(ActiveAbility x) => this.halberd = new DisableAbility(x)
				}
			};
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001597C File Offset: 0x00013B7C
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.bkb, 600f) || abilityHelper.UseAbility(this.bladeMail, 400f) || abilityHelper.UseAbility(this.shiva, true) || abilityHelper.UseAbility(this.plasma, true) || abilityHelper.UseAbility(this.halberd, true) || abilityHelper.UseAbility(this.force, 550f, 300f) || abilityHelper.UseAbility(this.pike, 550f, 300f) || abilityHelper.UseAbility(this.link, true) || abilityHelper.UseAbility(this.storm, true) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x04000222 RID: 546
		private ShieldAbility bkb;

		// Token: 0x04000223 RID: 547
		private ShieldAbility bladeMail;

		// Token: 0x04000224 RID: 548
		private ForceStaff force;

		// Token: 0x04000225 RID: 549
		private DisableAbility halberd;

		// Token: 0x04000226 RID: 550
		private DebuffAbility link;

		// Token: 0x04000227 RID: 551
		private SpeedBuffAbility phase;

		// Token: 0x04000228 RID: 552
		private HurricanePike pike;

		// Token: 0x04000229 RID: 553
		private NukeAbility plasma;

		// Token: 0x0400022A RID: 554
		private DebuffAbility shiva;

		// Token: 0x0400022B RID: 555
		private AoeAbility storm;
	}
}
