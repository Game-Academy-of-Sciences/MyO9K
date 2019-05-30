using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Tidehunter.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Tidehunter.Units
{
	// Token: 0x02000088 RID: 136
	[UnitName("npc_dota_hero_tidehunter")]
	internal class Tidehunter : ControllableUnit
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		public Tidehunter(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.tidehunter_gush,
					(ActiveAbility x) => this.gush = new NukeAbility(x)
				},
				{
					AbilityId.tidehunter_anchor_smash,
					(ActiveAbility x) => this.smash = new NukeAbility(x)
				},
				{
					AbilityId.tidehunter_ravage,
					(ActiveAbility x) => this.ravage = new Ravage(x)
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
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				},
				{
					AbilityId.item_refresher,
					(ActiveAbility x) => this.refresher = new UntargetableAbility(x)
				},
				{
					AbilityId.item_refresher_shard,
					(ActiveAbility x) => this.refresherShard = new UntargetableAbility(x)
				}
			};
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00011088 File Offset: 0x0000F288
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			Unit9 target = targetManager.Target;
			if ((!abilityHelper.CanBeCasted(this.blink, true, true, true, true) || base.Owner.Distance(target) < 400f) && abilityHelper.UseAbility(this.ravage, true))
			{
				return true;
			}
			if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink, 0f))
			{
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
			{
				this.ravage
			}))
			{
				return true;
			}
			if (!abilityHelper.CanBeCasted(this.ravage, false, false, true, true))
			{
				if (abilityHelper.UseAbility(this.blink, 400f, 0f))
				{
					return true;
				}
				if (abilityHelper.UseAbility(this.force, 400f, 0f))
				{
					return true;
				}
			}
			if (abilityHelper.UseAbility(this.shiva, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.bladeMail, 400f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.smash, true))
			{
				return true;
			}
			if ((abilityHelper.CanBeCasted(this.refresher, true, true, true, true) || abilityHelper.CanBeCasted(this.refresherShard, true, true, true, true)) && abilityHelper.CanBeCasted(this.ravage, true, true, true, false) && !this.ravage.Ability.IsReady)
			{
				UntargetableAbility untargetableAbility = abilityHelper.CanBeCasted(this.refresherShard, true, true, true, true) ? this.refresherShard : this.refresher;
				if (abilityHelper.HasMana(new UsableAbility[]
				{
					this.ravage,
					untargetableAbility
				}) && abilityHelper.UseAbility(untargetableAbility, true))
				{
					return true;
				}
			}
			return abilityHelper.UseAbility(this.gush, true);
		}

		// Token: 0x04000174 RID: 372
		private ShieldAbility bladeMail;

		// Token: 0x04000175 RID: 373
		private BlinkDaggerAOE blink;

		// Token: 0x04000176 RID: 374
		private ForceStaff force;

		// Token: 0x04000177 RID: 375
		private NukeAbility gush;

		// Token: 0x04000178 RID: 376
		private Ravage ravage;

		// Token: 0x04000179 RID: 377
		private UntargetableAbility refresher;

		// Token: 0x0400017A RID: 378
		private UntargetableAbility refresherShard;

		// Token: 0x0400017B RID: 379
		private DebuffAbility shiva;

		// Token: 0x0400017C RID: 380
		private NukeAbility smash;
	}
}
