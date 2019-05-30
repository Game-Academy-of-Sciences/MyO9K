using System;
using System.Collections.Generic;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Heroes.Lina
{
	// Token: 0x0200034B RID: 843
	[AbilityId(AbilityId.lina_dragon_slave)]
	public class DragonSlave : LineAbility, INuke, IActiveAbility
	{
		// Token: 0x06000E4A RID: 3658 RVA: 0x0000C94E File Offset: 0x0000AB4E
		public DragonSlave(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "dragon_slave_width_initial");
			this.SpeedData = new SpecialData(baseAbility, "dragon_slave_speed");
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0000C979 File Offset: 0x0000AB79
		public override float Speed
		{
			get
			{
				return base.Speed * 1.2f;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0000C987 File Offset: 0x0000AB87
		public override bool UnitTargetCast { get; }

		// Token: 0x06000E4D RID: 3661 RVA: 0x0000C98F File Offset: 0x0000AB8F
		public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
		{
			PredictionInput9 predictionInput = base.GetPredictionInput(target, aoeTargets);
			predictionInput.Range -= 200f;
			predictionInput.CastRange -= 200f;
			return predictionInput;
		}
	}
}
