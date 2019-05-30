using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.SpiritBreaker
{
	// Token: 0x020002BE RID: 702
	[AbilityId(AbilityId.spirit_breaker_charge_of_darkness)]
	public class ChargeOfDarkness : RangedAbility
	{
		// Token: 0x06000C69 RID: 3177 RVA: 0x0000B2AF File Offset: 0x000094AF
		public ChargeOfDarkness(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "movement_speed");
			this.RadiusData = new SpecialData(baseAbility, "bash_radius");
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x00025888 File Offset: 0x00023A88
		public override float Speed
		{
			get
			{
				if (base.Owner.IsCharging && base.Owner.IsVisible)
				{
					return base.Owner.Speed;
				}
				return base.Owner.Speed + this.SpeedData.GetValue(this.Level);
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0000B2E5 File Offset: 0x000094E5
		public override float CastRange { get; } = 9999999f;
	}
}
