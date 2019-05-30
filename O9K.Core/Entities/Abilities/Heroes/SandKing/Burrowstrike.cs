using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.SandKing
{
	// Token: 0x020002DC RID: 732
	[AbilityId(AbilityId.sandking_burrowstrike)]
	public class Burrowstrike : LineAbility, IBlink, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000CC6 RID: 3270 RVA: 0x00025D6C File Offset: 0x00023F6C
		public Burrowstrike(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "burrow_width");
			this.SpeedData = new SpecialData(baseAbility, "burrow_speed");
			this.scepterSpeedData = new SpecialData(baseAbility, "burrow_speed_scepter");
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0000B7C9 File Offset: 0x000099C9
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0000B7D1 File Offset: 0x000099D1
		public BlinkType BlinkType { get; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0000B7D9 File Offset: 0x000099D9
		public override float Speed
		{
			get
			{
				if (base.Owner.HasAghanimsScepter)
				{
					return this.scepterSpeedData.GetValue(this.Level);
				}
				return base.Speed;
			}
		}

		// Token: 0x04000697 RID: 1687
		private readonly SpecialData scepterSpeedData;
	}
}
