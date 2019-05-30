using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Heroes.Pangolier
{
	// Token: 0x02000306 RID: 774
	[AbilityId(AbilityId.pangolier_gyroshell)]
	public class RollingThunder : ActiveAbility
	{
		// Token: 0x06000D59 RID: 3417 RVA: 0x0000BDC9 File Offset: 0x00009FC9
		public RollingThunder(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "hit_radius");
			this.SpeedData = new SpecialData(baseAbility, "forward_move_speed");
			this.turnRateData = new SpecialData(baseAbility, "turn_rate");
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0000BE05 File Offset: 0x0000A005
		public float TurnRate
		{
			get
			{
				return MathUtil.DegreesToRadians(this.turnRateData.GetValue(this.Level));
			}
		}

		// Token: 0x040006E3 RID: 1763
		private readonly SpecialData turnRateData;
	}
}
