using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Mars.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Mars.Units
{
	// Token: 0x02000104 RID: 260
	[UnitName("npc_dota_hero_mars")]
	internal class Mars : ControllableUnit
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x0001A92C File Offset: 0x00018B2C
		public Mars(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.mars_spear,
					(ActiveAbility x) => this.spear = new SpearOfMars(x)
				},
				{
					AbilityId.mars_gods_rebuke,
					(ActiveAbility x) => this.rebuke = new NukeAbility(x)
				},
				{
					AbilityId.mars_arena_of_blood,
					(ActiveAbility x) => this.arena = new ArenaOfBlood(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_orchid,
					(ActiveAbility x) => this.orchid = new DisableAbility(x)
				},
				{
					AbilityId.item_nullifier,
					(ActiveAbility x) => this.nullifier = new Nullifier(x)
				},
				{
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.bloodthorn = new Bloodthorn(x)
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
					AbilityId.item_heavens_halberd,
					(ActiveAbility x) => this.halberd = new DisableAbility(x)
				},
				{
					AbilityId.item_armlet,
					(ActiveAbility x) => this.armlet = new BuffAbility(x)
				},
				{
					AbilityId.item_abyssal_blade,
					(ActiveAbility x) => this.abyssal = new DisableAbility(x)
				}
			};
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001AA58 File Offset: 0x00018C58
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.bkb, 400f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.abyssal, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.bloodthorn, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.orchid, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.nullifier, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.halberd, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.blink, 400f, 0f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.armlet, 400f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.arena, true))
			{
				base.ComboSleeper.ExtendSleep(0.1f);
				return true;
			}
			if (targetManager.Target.HasModifier("modifier_mars_arena_of_blood_leash"))
			{
				if (abilityHelper.UseAbility(this.rebuke, true))
				{
					return true;
				}
				if (abilityHelper.UseAbility(this.spear, true))
				{
					return true;
				}
			}
			else
			{
				if (abilityHelper.UseAbility(this.spear, true))
				{
					return true;
				}
				if (abilityHelper.UseAbility(this.rebuke, true))
				{
					return true;
				}
			}
			return abilityHelper.UseAbility(this.bladeMail, 400f);
		}

		// Token: 0x040002D8 RID: 728
		private ArenaOfBlood arena;

		// Token: 0x040002D9 RID: 729
		private SpearOfMars spear;

		// Token: 0x040002DA RID: 730
		private NukeAbility rebuke;

		// Token: 0x040002DB RID: 731
		private Nullifier nullifier;

		// Token: 0x040002DC RID: 732
		private DisableAbility orchid;

		// Token: 0x040002DD RID: 733
		private DisableAbility bloodthorn;

		// Token: 0x040002DE RID: 734
		private BlinkAbility blink;

		// Token: 0x040002DF RID: 735
		private ShieldAbility bladeMail;

		// Token: 0x040002E0 RID: 736
		private ShieldAbility bkb;

		// Token: 0x040002E1 RID: 737
		private BuffAbility armlet;

		// Token: 0x040002E2 RID: 738
		private DisableAbility halberd;

		// Token: 0x040002E3 RID: 739
		private DisableAbility abyssal;
	}
}
