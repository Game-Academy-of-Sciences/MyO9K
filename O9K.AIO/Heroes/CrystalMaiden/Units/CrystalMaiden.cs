using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.CrystalMaiden.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.CrystalMaiden.Units
{
	// Token: 0x020001CF RID: 463
	[UnitName("npc_dota_hero_crystal_maiden")]
	internal class CrystalMaiden : ControllableUnit
	{
		// Token: 0x06000938 RID: 2360 RVA: 0x00028D8C File Offset: 0x00026F8C
		public CrystalMaiden(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.crystal_maiden_crystal_nova,
					(ActiveAbility x) => this.nova = new NukeAbility(x)
				},
				{
					AbilityId.crystal_maiden_frostbite,
					(ActiveAbility x) => this.frostbite = new DisableAbility(x)
				},
				{
					AbilityId.crystal_maiden_freezing_field,
					(ActiveAbility x) => this.field = new FreezingField(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkDaggerCM(x)
				},
				{
					AbilityId.item_glimmer_cape,
					(ActiveAbility x) => this.glimmer = new ShieldAbility(x)
				},
				{
					AbilityId.item_black_king_bar,
					(ActiveAbility x) => this.bkb = new ShieldAbility(x)
				},
				{
					AbilityId.item_rod_of_atos,
					(ActiveAbility x) => this.atos = new DisableAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.crystal_maiden_frostbite, (ActiveAbility _) => this.frostbite);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00028E7C File Offset: 0x0002707C
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.CanBeCasted(this.field, true, true, true, true))
			{
				if (abilityHelper.CanBeCasted(this.bkb, false, false, true, true) && abilityHelper.ForceUseAbility(this.bkb, false, true))
				{
					return true;
				}
				if (abilityHelper.HasMana(new UsableAbility[]
				{
					this.field,
					this.glimmer
				}) && abilityHelper.CanBeCasted(this.glimmer, false, false, true, true) && abilityHelper.ForceUseAbility(this.glimmer, false, true))
				{
					return true;
				}
				if (abilityHelper.HasMana(new UsableAbility[]
				{
					this.field,
					this.atos
				}) && abilityHelper.UseAbility(this.atos, true))
				{
					return true;
				}
				if (abilityHelper.HasMana(new UsableAbility[]
				{
					this.field,
					this.frostbite
				}) && abilityHelper.UseAbility(this.frostbite, true))
				{
					return true;
				}
				if (abilityHelper.UseAbility(this.field, true))
				{
					return true;
				}
			}
			return abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
			{
				this.field
			}) || abilityHelper.UseAbility(this.force, 600f, 400f) || abilityHelper.UseAbility(this.atos, true) || abilityHelper.UseAbility(this.frostbite, true) || abilityHelper.UseAbility(this.nova, true);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00006A3E File Offset: 0x00004C3E
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseMoveAbility(this.frostbite);
		}

		// Token: 0x040004E1 RID: 1249
		private DisableAbility atos;

		// Token: 0x040004E2 RID: 1250
		private ShieldAbility bkb;

		// Token: 0x040004E3 RID: 1251
		private BlinkAbility blink;

		// Token: 0x040004E4 RID: 1252
		private NukeAbility field;

		// Token: 0x040004E5 RID: 1253
		private ForceStaff force;

		// Token: 0x040004E6 RID: 1254
		private DisableAbility frostbite;

		// Token: 0x040004E7 RID: 1255
		private ShieldAbility glimmer;

		// Token: 0x040004E8 RID: 1256
		private NukeAbility nova;
	}
}
