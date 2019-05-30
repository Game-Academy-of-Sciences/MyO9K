using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Axe.Abilities;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Axe.Units
{
	// Token: 0x020001E6 RID: 486
	[UnitName("npc_dota_hero_axe")]
	internal class Axe : ControllableUnit
	{
		// Token: 0x060009A2 RID: 2466 RVA: 0x00029F6C File Offset: 0x0002816C
		public Axe(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.axe_berserkers_call,
					(ActiveAbility x) => this.call = new DisableAbility(x)
				},
				{
					AbilityId.axe_battle_hunger,
					(ActiveAbility x) => this.hunger = new DebuffAbility(x)
				},
				{
					AbilityId.axe_culling_blade,
					(ActiveAbility x) => this.blade = new CullingBlade(x)
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
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				},
				{
					AbilityId.item_mjollnir,
					(ActiveAbility x) => this.mjollnir = new ShieldAbility(x)
				},
				{
					AbilityId.item_meteor_hammer,
					(ActiveAbility x) => this.meteor = new MeteorHammerAxe(x)
				},
				{
					AbilityId.item_dagon_5,
					(ActiveAbility x) => this.dagon = new NukeAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.axe_berserkers_call, (ActiveAbility _) => this.call);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0002A098 File Offset: 0x00028298
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.bkb, 400f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.blade, true))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.blade, false, true, true, true) && !abilityHelper.CanBeCasted(this.blade, true, true, true, true) && abilityHelper.UseAbility(this.blink, 200f, 0f))
			{
				return true;
			}
			if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink, 0f))
			{
				return true;
			}
			if (abilityHelper.CanBeCastedIfCondition(this.blink, new UsableAbility[]
			{
				this.call
			}) && abilityHelper.UseAbility(this.bkb, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.call, true))
			{
				this.call.Sleeper.ExtendSleep(1f);
				return true;
			}
			if (abilityHelper.CanBeCasted(this.meteor, true, true, true, true) && targetManager.Target.HasModifier("modifier_axe_berserkers_call") && abilityHelper.UseAbility(this.meteor, true))
			{
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
			{
				this.call
			}))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.force, 500f, 0f))
			{
				return true;
			}
			if (!abilityHelper.CanBeCasted(this.call, false, true, true, true) && abilityHelper.UseAbility(this.bladeMail, 400f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.mjollnir, 400f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.shiva, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.dagon, true))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.hunger, true, true, true, true))
			{
				if (abilityHelper.CanBeCasted(this.call, true, true, true, true))
				{
					return false;
				}
				if (abilityHelper.CanBeCasted(this.meteor, true, true, true, true))
				{
					DisableAbility disableAbility = this.call;
					if (disableAbility != null && disableAbility.Sleeper.IsSleeping)
					{
						return false;
					}
				}
				if (abilityHelper.UseAbility(this.hunger, true))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00006D0B File Offset: 0x00004F0B
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseAbility(this.call, true);
		}

		// Token: 0x04000515 RID: 1301
		private ShieldAbility bkb;

		// Token: 0x04000516 RID: 1302
		private CullingBlade blade;

		// Token: 0x04000517 RID: 1303
		private ShieldAbility bladeMail;

		// Token: 0x04000518 RID: 1304
		private BlinkDaggerAOE blink;

		// Token: 0x04000519 RID: 1305
		private DisableAbility call;

		// Token: 0x0400051A RID: 1306
		private ForceStaff force;

		// Token: 0x0400051B RID: 1307
		private DebuffAbility hunger;

		// Token: 0x0400051C RID: 1308
		private DisableAbility meteor;

		// Token: 0x0400051D RID: 1309
		private ShieldAbility mjollnir;

		// Token: 0x0400051E RID: 1310
		private DebuffAbility shiva;

		// Token: 0x0400051F RID: 1311
		private NukeAbility dagon;
	}
}
