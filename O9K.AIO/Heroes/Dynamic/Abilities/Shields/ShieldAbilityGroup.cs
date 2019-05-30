using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Dynamic.Abilities.Blinks;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Shields
{
	// Token: 0x020001A4 RID: 420
	internal class ShieldAbilityGroup : OldAbilityGroup<IShield, OldShieldAbility>
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x00026854 File Offset: 0x00024A54
		public ShieldAbilityGroup(BaseHero baseHero) : base(baseHero)
		{
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0000643C File Offset: 0x0000463C
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x00006444 File Offset: 0x00004644
		public BlinkAbilityGroup Blinks { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0000644D File Offset: 0x0000464D
		protected override HashSet<AbilityId> Ignored { get; } = new HashSet<AbilityId>
		{
			AbilityId.item_ethereal_blade,
			AbilityId.dark_willow_shadow_realm,
			AbilityId.puck_phase_shift,
			AbilityId.obsidian_destroyer_astral_imprisonment,
			AbilityId.bane_nightmare,
			AbilityId.chen_holy_persuasion,
			AbilityId.dazzle_shallow_grave,
			AbilityId.earth_spirit_petrify,
			AbilityId.life_stealer_assimilate,
			AbilityId.naga_siren_song_of_the_siren,
			AbilityId.necrolyte_sadist,
			AbilityId.omniknight_guardian_angel,
			AbilityId.oracle_false_promise,
			AbilityId.oracle_fates_edict,
			AbilityId.phoenix_supernova,
			AbilityId.pugna_decrepify,
			AbilityId.shadow_demon_disruption,
			AbilityId.slark_shadow_dance,
			AbilityId.vengefulspirit_nether_swap,
			AbilityId.weaver_time_lapse,
			AbilityId.winter_wyvern_cold_embrace,
			AbilityId.item_cyclone,
			AbilityId.item_sphere
		};

		// Token: 0x06000884 RID: 2180 RVA: 0x00026984 File Offset: 0x00024B84
		public override bool Use(Unit9 target, ComboModeMenu menu, params AbilityId[] except)
		{
			foreach (OldShieldAbility oldShieldAbility in base.Abilities)
			{
				if (oldShieldAbility.Ability.IsValid && !except.Contains(oldShieldAbility.Ability.Id) && oldShieldAbility.CanBeCasted(oldShieldAbility.Shield.Owner, target, this.Blinks, menu) && oldShieldAbility.Use(oldShieldAbility.Shield.Owner))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00026A28 File Offset: 0x00024C28
		protected override bool IsIgnored(Ability9 ability)
		{
			IDisable disable;
			return base.IsIgnored(ability) || ((disable = (ability as IDisable)) != null && AbilityExtensions.IsInvulnerability(disable));
		}
	}
}
