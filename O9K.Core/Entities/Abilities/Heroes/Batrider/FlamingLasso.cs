using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Batrider
{
	// Token: 0x0200025D RID: 605
	[AbilityId(AbilityId.batrider_flaming_lasso)]
	public class FlamingLasso : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x00009F6A File Offset: 0x0000816A
		public FlamingLasso(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00009F7C File Offset: 0x0000817C
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0000720E File Offset: 0x0000540E
		public override float CastRange
		{
			get
			{
				return base.CastRange + 100f;
			}
		}
	}
}
