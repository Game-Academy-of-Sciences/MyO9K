using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Warlock.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Warlock.Units
{
	// Token: 0x02000054 RID: 84
	[UnitName("npc_dota_hero_warlock")]
	internal class Warlock : ControllableUnit
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		public Warlock(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.warlock_fatal_bonds,
					(ActiveAbility x) => this.bonds = new FatalBonds(x)
				},
				{
					AbilityId.warlock_shadow_word,
					(ActiveAbility x) => this.word = new DebuffAbility(x)
				},
				{
					AbilityId.warlock_upheaval,
					(ActiveAbility x) => this.upheaval = new DebuffAbility(x)
				},
				{
					AbilityId.warlock_rain_of_chaos,
					(ActiveAbility x) => this.chaos = new ChaoticOffering(x)
				},
				{
					AbilityId.item_refresher,
					(ActiveAbility x) => this.refresher = new UntargetableAbility(x)
				},
				{
					AbilityId.item_refresher_shard,
					(ActiveAbility x) => this.refresherShard = new UntargetableAbility(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				}
			};
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.chaos, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.force, 600f, 500f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.bonds, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.word, true))
			{
				return true;
			}
			if ((abilityHelper.CanBeCasted(this.refresher, true, true, true, true) || abilityHelper.CanBeCasted(this.refresherShard, true, true, true, true)) && abilityHelper.CanBeCasted(this.chaos, true, true, true, false) && !this.chaos.Ability.IsReady)
			{
				UntargetableAbility untargetableAbility = abilityHelper.CanBeCasted(this.refresherShard, true, true, true, true) ? this.refresherShard : this.refresher;
				if (abilityHelper.HasMana(new UsableAbility[]
				{
					this.chaos,
					untargetableAbility
				}) && abilityHelper.UseAbility(untargetableAbility, true))
				{
					base.ComboSleeper.ExtendSleep(0.2f);
					DebuffAbility debuffAbility = this.upheaval;
					if (debuffAbility != null)
					{
						debuffAbility.Sleeper.Sleep(0.5f);
					}
					return true;
				}
			}
			return abilityHelper.UseAbility(this.upheaval, true);
		}

		// Token: 0x040000F9 RID: 249
		private DebuffAbility bonds;

		// Token: 0x040000FA RID: 250
		private DisableAbility chaos;

		// Token: 0x040000FB RID: 251
		private ForceStaff force;

		// Token: 0x040000FC RID: 252
		private UntargetableAbility refresher;

		// Token: 0x040000FD RID: 253
		private UntargetableAbility refresherShard;

		// Token: 0x040000FE RID: 254
		private DebuffAbility upheaval;

		// Token: 0x040000FF RID: 255
		private DebuffAbility word;
	}
}
