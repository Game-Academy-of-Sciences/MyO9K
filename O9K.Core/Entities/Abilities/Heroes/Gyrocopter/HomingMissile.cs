using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Gyrocopter
{
	// Token: 0x0200022E RID: 558
	[AbilityId(AbilityId.gyrocopter_homing_missile)]
	public class HomingMissile : RangedAbility
	{
		// Token: 0x06000A72 RID: 2674 RVA: 0x00009719 File Offset: 0x00007919
		public HomingMissile(Ability baseAbility) : base(baseAbility)
		{
			this.ActivationDelayData = new SpecialData(baseAbility, "pre_flight_time");
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.speedAccelerationData = new SpecialData(baseAbility, "acceleration");
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00009755 File Offset: 0x00007955
		public float Acceleration
		{
			get
			{
				return this.speedAccelerationData.GetValue(this.Level);
			}
		}

		// Token: 0x04000548 RID: 1352
		private readonly SpecialData speedAccelerationData;
	}
}
