using System;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.NyxAssassin
{
	// Token: 0x02000318 RID: 792
	[AbilityId(AbilityId.nyx_assassin_impale)]
	public class Impale : LineAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000DAE RID: 3502 RVA: 0x00026E54 File Offset: 0x00025054
		public Impale(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "width");
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.DamageData = new SpecialData(baseAbility, "impale_damage");
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0000C199 File Offset: 0x0000A399
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0000C1A1 File Offset: 0x0000A3A1
		protected override float BaseCastRange
		{
			get
			{
				if (base.Owner.HasModifier("modifier_nyx_assassin_burrow"))
				{
					return base.Owner.GetAbilityById(AbilityId.nyx_assassin_burrow).GetAbilitySpecialData("impale_burrow_range_tooltip", 0u);
				}
				return base.BaseCastRange;
			}
		}
	}
}
