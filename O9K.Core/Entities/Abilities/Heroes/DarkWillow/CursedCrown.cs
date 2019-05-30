using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.DarkWillow
{
	// Token: 0x02000399 RID: 921
	[AbilityId(AbilityId.dark_willow_cursed_crown)]
	public class CursedCrown : RangedAbility, IDebuff, IDisable, IActiveAbility
	{
		// Token: 0x06000FA5 RID: 4005 RVA: 0x0000DCE6 File Offset: 0x0000BEE6
		public CursedCrown(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "stun_radius");
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0000DD14 File Offset: 0x0000BF14
		public string DebuffModifierName { get; } = "modifier_dark_willow_cursed_crown";

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x0000DD1C File Offset: 0x0000BF1C
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
