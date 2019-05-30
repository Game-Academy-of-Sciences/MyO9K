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

namespace O9K.AIO.Heroes.ElderTitan.Units
{
	// Token: 0x0200013F RID: 319
	[UnitName("npc_dota_hero_elder_titan")]
	internal class ElderTitan : ControllableUnit
	{
		// Token: 0x06000655 RID: 1621 RVA: 0x0001EC4C File Offset: 0x0001CE4C
		public ElderTitan(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.elder_titan_echo_stomp,
					(ActiveAbility x) => this.stomp = new DisableAbility(x)
				},
				{
					AbilityId.elder_titan_ancestral_spirit,
					(ActiveAbility x) => this.spirit = new NukeAbility(x)
				},
				{
					AbilityId.elder_titan_earth_splitter,
					(ActiveAbility x) => this.splitter = new NukeAbility(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_cyclone,
					(ActiveAbility x) => this.euls = new EulsScepterOfDivinity(x)
				},
				{
					AbilityId.item_meteor_hammer,
					(ActiveAbility x) => this.hammer = new DisableAbility(x)
				},
				{
					AbilityId.item_rod_of_atos,
					(ActiveAbility x) => this.atos = new DisableAbility(x)
				}
			};
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001ED0C File Offset: 0x0001CF0C
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.force, 550f, 400f))
			{
				return true;
			}
			if (abilityHelper.UseAbilityIfAny(this.euls, new UsableAbility[]
			{
				this.stomp,
				this.hammer
			}))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.atos, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.spirit, true))
			{
				return true;
			}
			if (abilityHelper.UseAbilityIfNone(this.stomp, new UsableAbility[]
			{
				this.euls
			}))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.hammer, false, false, true, true))
			{
				Unit9 target = targetManager.Target;
				if (target.GetImmobilityDuration() > this.hammer.Ability.GetHitTime(target) - 0.5f && abilityHelper.UseAbility(this.hammer, true))
				{
					return true;
				}
			}
			return !abilityHelper.CanBeCasted(this.euls, true, true, true, true) && !abilityHelper.CanBeCasted(this.stomp, false, false, true, true) && abilityHelper.UseAbilityIfNone(this.splitter, new UsableAbility[0]);
		}

		// Token: 0x04000378 RID: 888
		private DisableAbility atos;

		// Token: 0x04000379 RID: 889
		private EulsScepterOfDivinity euls;

		// Token: 0x0400037A RID: 890
		private ForceStaff force;

		// Token: 0x0400037B RID: 891
		private DisableAbility hammer;

		// Token: 0x0400037C RID: 892
		private NukeAbility spirit;

		// Token: 0x0400037D RID: 893
		private NukeAbility splitter;

		// Token: 0x0400037E RID: 894
		private DisableAbility stomp;
	}
}
