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
using SharpDX;

namespace O9K.Core.Entities.Abilities.Heroes.Kunkka
{
	// Token: 0x0200021A RID: 538
	[AbilityId(AbilityId.kunkka_torrent)]
	public class Torrent : CircleAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000A47 RID: 2631 RVA: 0x00023F0C File Offset: 0x0002210C
		public Torrent(Ability baseAbility) : base(baseAbility)
		{
			this.ActivationDelayData = new SpecialData(baseAbility, "delay");
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "torrent_damage");
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0000947B File Offset: 0x0000767B
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x06000A49 RID: 2633 RVA: 0x0000772F File Offset: 0x0000592F
		public override float GetCastDelay(Unit9 unit)
		{
			return this.GetCastDelay();
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0000772F File Offset: 0x0000592F
		public override float GetCastDelay(Vector3 position)
		{
			return this.GetCastDelay();
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00023F5C File Offset: 0x0002215C
		public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
		{
			PredictionInput9 predictionInput = new PredictionInput9
			{
				Caster = base.Owner,
				Target = target,
				CollisionTypes = this.CollisionTypes,
				Delay = this.CastPoint + this.ActivationDelay + Ability9.InputLag,
				Speed = this.Speed,
				CastRange = this.CastRange,
				Range = this.Range,
				Radius = this.Radius,
				SkillShotType = this.SkillShotType,
				RequiresToTurn = false
			};
			if (aoeTargets != null)
			{
				predictionInput.AreaOfEffect = this.HasAreaOfEffect;
				predictionInput.AreaOfEffectTargets = aoeTargets;
			}
			return predictionInput;
		}
	}
}
