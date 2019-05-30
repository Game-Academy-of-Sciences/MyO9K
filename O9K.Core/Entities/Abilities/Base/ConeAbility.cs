using System;
using System.Collections.Generic;
using Ensage;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Base
{
	// Token: 0x020003E0 RID: 992
	public abstract class ConeAbility : LineAbility
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x0000E840 File Offset: 0x0000CA40
		protected ConeAbility(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x0000E850 File Offset: 0x0000CA50
		public virtual float EndRadius
		{
			get
			{
				SpecialData endRadiusData = this.EndRadiusData;
				if (endRadiusData == null)
				{
					return this.Radius;
				}
				return endRadiusData.GetValue(this.Level);
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x0000E86E File Offset: 0x0000CA6E
		public override float Range
		{
			get
			{
				SpecialData rangeData = this.RangeData;
				return ((rangeData != null) ? rangeData.GetValue(this.Level) : this.CastRange) + this.EndRadius;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x0000E894 File Offset: 0x0000CA94
		public override SkillShotType SkillShotType { get; } = 5;

		// Token: 0x0600108E RID: 4238 RVA: 0x0000E89C File Offset: 0x0000CA9C
		public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
		{
			PredictionInput9 predictionInput = base.GetPredictionInput(target, aoeTargets);
			predictionInput.EndRadius = this.EndRadius;
			return predictionInput;
		}

		// Token: 0x0400089E RID: 2206
		protected SpecialData EndRadiusData;
	}
}
