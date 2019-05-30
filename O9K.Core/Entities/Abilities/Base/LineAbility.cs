using System;
using Ensage;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Base
{
	// Token: 0x020003E1 RID: 993
	public abstract class LineAbility : PredictionAbility
	{
		// Token: 0x0600108F RID: 4239 RVA: 0x0000E8B2 File Offset: 0x0000CAB2
		protected LineAbility(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x0000E8C2 File Offset: 0x0000CAC2
		public override SkillShotType SkillShotType { get; } = 3;
	}
}
