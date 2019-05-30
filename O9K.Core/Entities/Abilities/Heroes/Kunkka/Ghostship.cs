using System;
using System.Collections.Generic;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Heroes.Kunkka
{
	// Token: 0x02000218 RID: 536
	[AbilityId(AbilityId.kunkka_ghostship)]
	public class Ghostship : CircleAbility, IHarass, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x00023D78 File Offset: 0x00021F78
		public Ghostship(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "ghostship_speed");
			this.RadiusData = new SpecialData(baseAbility, "ghostship_width");
			this.ghostshipDistance = new SpecialData(baseAbility, "ghostship_distance");
			this.amplifierData = new SpecialData(baseAbility, "ghostship_absorb");
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0000939A File Offset: 0x0000759A
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x000093A2 File Offset: 0x000075A2
		public string AmplifierModifierName { get; } = "modifier_kunkka_ghost_ship_damage_absorb";

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x000093AA File Offset: 0x000075AA
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00023DEC File Offset: 0x00021FEC
		public float GhostshipDistance
		{
			get
			{
				float num = this.ghostshipDistance.GetValue(this.Level);
				if (base.Owner.HasAghanimsScepter)
				{
					num /= 2f;
				}
				return num;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x000093B2 File Offset: 0x000075B2
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x000093BA File Offset: 0x000075BA
		public bool IsAmplifierPermanent { get; }

		// Token: 0x06000A3A RID: 2618 RVA: 0x000093C2 File Offset: 0x000075C2
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / -100f;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000093DB File Offset: 0x000075DB
		public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
		{
			PredictionInput9 predictionInput = base.GetPredictionInput(target, aoeTargets);
			predictionInput.Delay += this.GhostshipDistance / this.Speed;
			predictionInput.Speed = 0f;
			return predictionInput;
		}

		// Token: 0x0400052A RID: 1322
		private readonly SpecialData amplifierData;

		// Token: 0x0400052B RID: 1323
		private readonly SpecialData ghostshipDistance;
	}
}
