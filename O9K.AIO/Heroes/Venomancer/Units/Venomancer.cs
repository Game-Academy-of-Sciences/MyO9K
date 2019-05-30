using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Venomancer.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Venomancer.Units
{
	// Token: 0x02000059 RID: 89
	[UnitName("npc_dota_hero_venomancer")]
	internal class Venomancer : ControllableUnit
	{
		// Token: 0x060001DB RID: 475 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		public Venomancer(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.venomancer_venomous_gale,
					(ActiveAbility x) => this.gale = new DebuffAbility(x)
				},
				{
					AbilityId.venomancer_plague_ward,
					(ActiveAbility x) => this.ward = new PlagueWardAbility(x)
				},
				{
					AbilityId.venomancer_poison_nova,
					(ActiveAbility x) => this.nova = new PoisonNova(x)
				},
				{
					AbilityId.item_veil_of_discord,
					(ActiveAbility x) => this.veil = new DebuffAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkDaggerAOE(x)
				},
				{
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_orchid,
					(ActiveAbility x) => this.orchid = new DisableAbility(x)
				},
				{
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.bloodthorn = new Bloodthorn(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.venomancer_venomous_gale, (ActiveAbility _) => this.gale);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			Unit9 target = targetManager.Target;
			if (abilityHelper.UseAbility(this.bloodthorn, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.orchid, true))
			{
				return true;
			}
			if ((!abilityHelper.CanBeCasted(this.blink, true, true, true, true) || base.Owner.Distance(target) < 400f) && abilityHelper.UseAbility(this.nova, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.shiva, true))
			{
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
			{
				this.nova
			}))
			{
				return true;
			}
			if (!abilityHelper.CanBeCasted(this.nova, false, false, true, true))
			{
				if (abilityHelper.UseAbility(this.blink, 500f, 300f))
				{
					return true;
				}
				if (abilityHelper.UseAbility(this.force, 500f, 300f))
				{
					return true;
				}
			}
			return abilityHelper.UseAbility(this.veil, true) || abilityHelper.UseAbility(this.gale, true) || abilityHelper.UseAbility(this.ward, true);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00003543 File Offset: 0x00001743
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseAbility(this.gale, true);
		}

		// Token: 0x04000100 RID: 256
		private BlinkDaggerAOE blink;

		// Token: 0x04000101 RID: 257
		private DisableAbility bloodthorn;

		// Token: 0x04000102 RID: 258
		private ForceStaff force;

		// Token: 0x04000103 RID: 259
		private DebuffAbility gale;

		// Token: 0x04000104 RID: 260
		private DebuffAbility nova;

		// Token: 0x04000105 RID: 261
		private DisableAbility orchid;

		// Token: 0x04000106 RID: 262
		private DebuffAbility shiva;

		// Token: 0x04000107 RID: 263
		private DebuffAbility veil;

		// Token: 0x04000108 RID: 264
		private AoeAbility ward;
	}
}
