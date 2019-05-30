using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Bloodseeker.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Bloodseeker.Units
{
	// Token: 0x020001E2 RID: 482
	[UnitName("npc_dota_hero_bloodseeker")]
	internal class Bloodseeker : ControllableUnit
	{
		// Token: 0x06000990 RID: 2448 RVA: 0x00029B8C File Offset: 0x00027D8C
		public Bloodseeker(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.bloodseeker_bloodrage,
					(ActiveAbility x) => this.rage = new BuffAbility(x)
				},
				{
					AbilityId.bloodseeker_blood_bath,
					(ActiveAbility x) => this.blood = new BloodRite(x)
				},
				{
					AbilityId.bloodseeker_rupture,
					(ActiveAbility x) => this.rupture = new TargetableAbility(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
				},
				{
					AbilityId.item_blade_mail,
					(ActiveAbility x) => this.bladeMail = new ShieldAbility(x)
				},
				{
					AbilityId.item_abyssal_blade,
					(ActiveAbility x) => this.abyssal = new DisableAbility(x)
				},
				{
					AbilityId.item_cyclone,
					(ActiveAbility x) => this.euls = new EulsScepterOfDivinity(x)
				},
				{
					AbilityId.item_mjollnir,
					(ActiveAbility x) => this.mjollnir = new ShieldAbility(x)
				}
			};
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00029C60 File Offset: 0x00027E60
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.abyssal, true))
			{
				return true;
			}
			if (!targetManager.Target.IsRuptured && abilityHelper.UseAbilityIfAny(this.euls, new UsableAbility[]
			{
				this.blood
			}))
			{
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.blood, new UsableAbility[]
			{
				this.euls,
				this.rupture
			}))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.rupture, true))
			{
				this.rupture.Sleeper.ExtendSleep(1f);
				base.ComboSleeper.ExtendSleep(0.25f);
				return true;
			}
			return abilityHelper.UseAbility(this.rage, true) || abilityHelper.UseAbility(this.bladeMail, 600f) || abilityHelper.UseAbility(this.mjollnir, 600f) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x0400050A RID: 1290
		private DisableAbility abyssal;

		// Token: 0x0400050B RID: 1291
		private ShieldAbility bladeMail;

		// Token: 0x0400050C RID: 1292
		private BloodRite blood;

		// Token: 0x0400050D RID: 1293
		private EulsScepterOfDivinity euls;

		// Token: 0x0400050E RID: 1294
		private ShieldAbility mjollnir;

		// Token: 0x0400050F RID: 1295
		private SpeedBuffAbility phase;

		// Token: 0x04000510 RID: 1296
		private BuffAbility rage;

		// Token: 0x04000511 RID: 1297
		private TargetableAbility rupture;
	}
}
