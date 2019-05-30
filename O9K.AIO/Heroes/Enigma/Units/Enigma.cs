using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Enigma.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Enigma.Units
{
	// Token: 0x02000138 RID: 312
	[UnitName("npc_dota_hero_enigma")]
	internal class Enigma : ControllableUnit
	{
		// Token: 0x06000633 RID: 1587 RVA: 0x0001E4E0 File Offset: 0x0001C6E0
		public Enigma(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.enigma_malefice,
					(ActiveAbility x) => this.malefice = new DisableAbility(x)
				},
				{
					AbilityId.enigma_midnight_pulse,
					(ActiveAbility x) => this.pulse = new AoeAbility(x)
				},
				{
					AbilityId.enigma_black_hole,
					(ActiveAbility x) => this.blackHole = new BlackHole(x)
				},
				{
					AbilityId.item_black_king_bar,
					(ActiveAbility x) => this.bkb = new ShieldAbility(x)
				},
				{
					AbilityId.item_ghost,
					(ActiveAbility x) => this.ghost = new ShieldAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkDaggerEnigma(x)
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
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				}
			};
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001E5C4 File Offset: 0x0001C7C4
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.CanBeCasted(this.blackHole, true, true, true, true))
			{
				if (abilityHelper.CanBeCasted(this.bkb, false, false, true, true) && abilityHelper.ForceUseAbility(this.bkb, false, true))
				{
					return true;
				}
				if (abilityHelper.HasMana(new UsableAbility[]
				{
					this.pulse,
					this.blackHole
				}) && abilityHelper.UseAbility(this.pulse, true))
				{
					return true;
				}
				if (abilityHelper.HasMana(new UsableAbility[]
				{
					this.shiva,
					this.blackHole
				}) && abilityHelper.UseAbility(this.shiva, true))
				{
					return true;
				}
				if (abilityHelper.UseAbility(this.blackHole, true))
				{
					return true;
				}
			}
			if (abilityHelper.CanBeCastedIfCondition(this.blink, new UsableAbility[]
			{
				this.blackHole
			}))
			{
				if (abilityHelper.CanBeCasted(this.bkb, false, false, true, true) && abilityHelper.ForceUseAbility(this.bkb, false, true))
				{
					return true;
				}
				if (abilityHelper.CanBeCasted(this.ghost, false, false, true, true) && abilityHelper.ForceUseAbility(this.ghost, false, true))
				{
					return true;
				}
			}
			if (abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
			{
				this.blackHole
			}))
			{
				return true;
			}
			if ((abilityHelper.CanBeCasted(this.refresher, true, true, true, true) || abilityHelper.CanBeCasted(this.refresherShard, true, true, true, true)) && abilityHelper.CanBeCasted(this.blackHole, true, true, true, false) && !this.blackHole.Ability.IsReady)
			{
				UntargetableAbility untargetableAbility = abilityHelper.CanBeCasted(this.refresherShard, true, true, true, true) ? this.refresherShard : this.refresher;
				if (abilityHelper.HasMana(new UsableAbility[]
				{
					this.blackHole,
					untargetableAbility
				}) && abilityHelper.UseAbility(untargetableAbility, true))
				{
					return true;
				}
			}
			return abilityHelper.UseAbility(this.malefice, true);
		}

		// Token: 0x04000366 RID: 870
		private ShieldAbility bkb;

		// Token: 0x04000367 RID: 871
		private BlackHole blackHole;

		// Token: 0x04000368 RID: 872
		private BlinkDaggerEnigma blink;

		// Token: 0x04000369 RID: 873
		private ShieldAbility ghost;

		// Token: 0x0400036A RID: 874
		private DisableAbility malefice;

		// Token: 0x0400036B RID: 875
		private AoeAbility pulse;

		// Token: 0x0400036C RID: 876
		private UntargetableAbility refresher;

		// Token: 0x0400036D RID: 877
		private UntargetableAbility refresherShard;

		// Token: 0x0400036E RID: 878
		private DebuffAbility shiva;
	}
}
