using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000170 RID: 368
	[AbilityId(AbilityId.item_force_staff)]
	public class ForceStaff : RangedAbility, IBlink, IActiveAbility
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x00006E6C File Offset: 0x0000506C
		public ForceStaff(Ability baseAbility) : base(baseAbility)
		{
			this.RangeData = new SpecialData(baseAbility, "push_length");
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00006E98 File Offset: 0x00005098
		public override float Range
		{
			get
			{
				return this.RangeData.GetValue(this.Level);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00006EAB File Offset: 0x000050AB
		public override float Speed { get; } = 1200f;

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00006EB3 File Offset: 0x000050B3
		public BlinkType BlinkType { get; } = 1;

		// Token: 0x06000763 RID: 1891 RVA: 0x00006EBB File Offset: 0x000050BB
		public override float GetHitTime(Vector3 position)
		{
			return this.GetCastDelay(position) + this.ActivationDelay + this.Range / this.Speed;
		}
	}
}
