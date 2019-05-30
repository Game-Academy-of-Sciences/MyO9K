using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.DragonKnight
{
	// Token: 0x02000392 RID: 914
	[AbilityId(AbilityId.dragon_knight_dragon_tail)]
	public class DragonTail : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000F92 RID: 3986 RVA: 0x0000DB87 File Offset: 0x0000BD87
		public DragonTail(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
			this.castRangeData = new SpecialData(baseAbility, "dragon_cast_range");
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0000DBBB File Offset: 0x0000BDBB
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0000DBC3 File Offset: 0x0000BDC3
		protected override float BaseCastRange
		{
			get
			{
				if (base.Owner.HasModifier("modifier_dragon_knight_dragon_form"))
				{
					return this.castRangeData.GetValue(this.Level);
				}
				return base.BaseCastRange + 100f;
			}
		}

		// Token: 0x04000810 RID: 2064
		private readonly SpecialData castRangeData;
	}
}
