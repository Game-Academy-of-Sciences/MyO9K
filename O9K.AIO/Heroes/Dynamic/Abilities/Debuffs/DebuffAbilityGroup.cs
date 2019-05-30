using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Debuffs
{
	// Token: 0x020001BA RID: 442
	internal class DebuffAbilityGroup : OldAbilityGroup<IDebuff, OldDebuffAbility>
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x00027D34 File Offset: 0x00025F34
		public DebuffAbilityGroup(BaseHero baseHero) : base(baseHero)
		{
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00027D88 File Offset: 0x00025F88
		public bool UseAmplifiers(Unit9 target, ComboModeMenu menu)
		{
			foreach (OldDebuffAbility oldDebuffAbility in base.Abilities)
			{
				if (oldDebuffAbility.Debuff is IHasDamageAmplify && oldDebuffAbility.Debuff.IsValid && oldDebuffAbility.CanBeCasted(target, menu, true) && oldDebuffAbility.Use(target))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00027E0C File Offset: 0x0002600C
		protected override void OrderAbilities()
		{
			base.Abilities = (from x in base.Abilities
			orderby this.castOrderUp.IndexOf(x.Debuff.Id) descending, this.castOrderDown.IndexOf(x.Debuff.Id), x.Debuff.CastPoint
			select x).ToList<OldDebuffAbility>();
		}

		// Token: 0x040004BD RID: 1213
		private readonly List<AbilityId> castOrderDown = new List<AbilityId>
		{
			AbilityId.leshrac_lightning_storm,
			AbilityId.item_dagon
		};

		// Token: 0x040004BE RID: 1214
		private readonly List<AbilityId> castOrderUp = new List<AbilityId>
		{
			AbilityId.item_ethereal_blade,
			AbilityId.item_veil_of_discord
		};
	}
}
