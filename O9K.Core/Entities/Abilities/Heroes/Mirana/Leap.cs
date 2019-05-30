using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Heroes.Mirana
{
	// Token: 0x02000204 RID: 516
	[AbilityId(AbilityId.mirana_leap)]
	public class Leap : ActiveAbility, IBlink, IActiveAbility
	{
		// Token: 0x060009F5 RID: 2549 RVA: 0x00008F84 File Offset: 0x00007184
		public Leap(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "leap_speed");
			this.castRangeData = new SpecialData(baseAbility, "leap_distance");
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00008FB6 File Offset: 0x000071B6
		public BlinkType BlinkType { get; } = 1;

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00008FBE File Offset: 0x000071BE
		public override float CastRange
		{
			get
			{
				return this.castRangeData.GetValue(this.Level);
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00006EBB File Offset: 0x000050BB
		public override float GetHitTime(Vector3 position)
		{
			return this.GetCastDelay(position) + this.ActivationDelay + this.Range / this.Speed;
		}

		// Token: 0x04000509 RID: 1289
		private readonly SpecialData castRangeData;
	}
}
