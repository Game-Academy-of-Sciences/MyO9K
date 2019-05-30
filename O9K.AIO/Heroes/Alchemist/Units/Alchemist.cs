using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Heroes.Alchemist.Abilities;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Alchemist.Units
{
	// Token: 0x020001F3 RID: 499
	[UnitName("npc_dota_hero_alchemist")]
	internal class Alchemist : ControllableUnit
	{
		// Token: 0x060009E0 RID: 2528 RVA: 0x0002AEAC File Offset: 0x000290AC
		public Alchemist(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.alchemist_acid_spray,
					(ActiveAbility x) => this.acid = new DebuffAbility(x)
				},
				{
					AbilityId.alchemist_unstable_concoction_throw,
					(ActiveAbility x) => this.concoction = new UnstableConcoction(x)
				},
				{
					AbilityId.alchemist_chemical_rage,
					(ActiveAbility x) => this.rage = new BuffAbility(x)
				},
				{
					AbilityId.item_manta,
					(ActiveAbility x) => this.manta = new BuffAbility(x)
				},
				{
					AbilityId.item_armlet,
					(ActiveAbility x) => this.armlet = new BuffAbility(x)
				},
				{
					AbilityId.item_abyssal_blade,
					(ActiveAbility x) => this.abyssal = new DisableAbility(x)
				},
				{
					AbilityId.item_mjollnir,
					(ActiveAbility x) => this.mjollnir = new ShieldAbility(x)
				}
			};
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0002AF70 File Offset: 0x00029170
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.abyssal, true) || abilityHelper.UseAbility(this.armlet, 400f) || abilityHelper.UseAbility(this.concoction, true) || this.concoction.ThrowAway(targetManager, base.ComboSleeper) || abilityHelper.UseAbility(this.acid, true) || abilityHelper.UseAbility(this.mjollnir, 400f) || abilityHelper.UseAbility(this.rage, 400f) || abilityHelper.UseAbility(this.manta, 400f);
		}

		// Token: 0x04000533 RID: 1331
		private DisableAbility abyssal;

		// Token: 0x04000534 RID: 1332
		private DebuffAbility acid;

		// Token: 0x04000535 RID: 1333
		private BuffAbility armlet;

		// Token: 0x04000536 RID: 1334
		private UnstableConcoction concoction;

		// Token: 0x04000537 RID: 1335
		private BuffAbility manta;

		// Token: 0x04000538 RID: 1336
		private ShieldAbility mjollnir;

		// Token: 0x04000539 RID: 1337
		private BuffAbility rage;
	}
}
