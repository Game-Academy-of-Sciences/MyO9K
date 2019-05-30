using System;
using Ensage;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Base
{
	// Token: 0x020003DF RID: 991
	public abstract class CircleAbility : PredictionAbility
	{
		// Token: 0x06001088 RID: 4232 RVA: 0x0000E828 File Offset: 0x0000CA28
		protected CircleAbility(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x0000E838 File Offset: 0x0000CA38
		public override SkillShotType SkillShotType { get; } = 4;
	}
}
