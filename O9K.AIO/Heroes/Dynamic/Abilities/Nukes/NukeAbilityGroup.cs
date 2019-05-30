using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes
{
	// Token: 0x020001A6 RID: 422
	internal class NukeAbilityGroup : OldAbilityGroup<INuke, OldNukeAbility>
	{
		// Token: 0x0600088A RID: 2186 RVA: 0x00026BB0 File Offset: 0x00024DB0
		public NukeAbilityGroup(BaseHero baseHero) : base(baseHero)
		{
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00026C64 File Offset: 0x00024E64
		public override bool Use(Unit9 target, ComboModeMenu menu, params AbilityId[] except)
		{
			foreach (OldNukeAbility oldNukeAbility in base.Abilities)
			{
				if (oldNukeAbility.Ability.IsValid && !except.Contains(oldNukeAbility.Ability.Id) && oldNukeAbility.CanBeCasted(target, menu, true) && (!this.killStealOnly.Contains(oldNukeAbility.Ability.Id) || (float)oldNukeAbility.Nuke.GetDamage(target) >= target.Health) && oldNukeAbility.Use(target))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00026D18 File Offset: 0x00024F18
		protected override void OrderAbilities()
		{
			base.Abilities = (from x in base.Abilities
			orderby x.Ability is IChanneled, this.castOrder.IndexOf(x.Ability.Id) descending, x.Ability.CastPoint
			select x).ToList<OldNukeAbility>();
		}

		// Token: 0x040004A1 RID: 1185
		private readonly List<AbilityId> castOrder = new List<AbilityId>
		{
			AbilityId.item_dagon,
			AbilityId.item_dagon_2,
			AbilityId.item_dagon_3,
			AbilityId.item_dagon_4,
			AbilityId.item_dagon_5,
			AbilityId.item_ethereal_blade
		};

		// Token: 0x040004A2 RID: 1186
		private readonly HashSet<AbilityId> killStealOnly = new HashSet<AbilityId>
		{
			AbilityId.antimage_mana_void,
			AbilityId.axe_culling_blade,
			AbilityId.necrolyte_reapers_scythe,
			AbilityId.sniper_assassinate,
			AbilityId.zuus_thundergods_wrath,
			AbilityId.item_ethereal_blade
		};
	}
}
