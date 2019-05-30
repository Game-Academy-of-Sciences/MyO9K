using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Heroes.StormSpirit
{
	// Token: 0x020002BA RID: 698
	[AbilityId(AbilityId.storm_spirit_ball_lightning)]
	public class BallLightning : LineAbility, IBlink, IActiveAbility
	{
		// Token: 0x06000C5C RID: 3164 RVA: 0x000257EC File Offset: 0x000239EC
		public BallLightning(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "ball_lightning_move_speed");
			this.RadiusData = new SpecialData(baseAbility, "ball_lightning_aoe");
			this.travelCostBase = new SpecialData(baseAbility, "ball_lightning_travel_cost_base");
			this.travelCostPercent = new SpecialData(baseAbility, "ball_lightning_travel_cost_percent");
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0000B1C9 File Offset: 0x000093C9
		public float MaxCastRange
		{
			get
			{
				return (float)Math.Ceiling((double)((base.Owner.Mana - base.ManaCost) / this.TravelCost)) * 100f;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0000B1F1 File Offset: 0x000093F1
		public override float CastRange { get; } = 600f;

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0000B1F9 File Offset: 0x000093F9
		public BlinkType BlinkType { get; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0000B201 File Offset: 0x00009401
		private float TravelCost
		{
			get
			{
				return this.travelCostBase.GetValue(this.Level) + base.Owner.MaximumMana * (this.travelCostPercent.GetValue(this.Level) / 100f);
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0000B238 File Offset: 0x00009438
		public int GetRemainingMana(Vector3 position)
		{
			return (int)Math.Max(base.Owner.Mana - (float)this.GetRequiredMana(position), 0f);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00025850 File Offset: 0x00023A50
		public int GetRequiredMana(Vector3 position)
		{
			float num = base.Owner.Distance(position);
			return (int)(base.ManaCost + (float)Math.Floor((double)(num / 100f)) * this.TravelCost);
		}

		// Token: 0x0400065D RID: 1629
		private readonly SpecialData travelCostBase;

		// Token: 0x0400065E RID: 1630
		private readonly SpecialData travelCostPercent;
	}
}
