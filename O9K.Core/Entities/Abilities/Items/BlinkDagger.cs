using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200016B RID: 363
	[AbilityId(AbilityId.item_blink)]
	public class BlinkDagger : RangedAbility, IBlink, IActiveAbility
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x00006D8B File Offset: 0x00004F8B
		public BlinkDagger(Ability baseAbility) : base(baseAbility)
		{
			this.castRangeData = new SpecialData(baseAbility, "blink_range");
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00021A0C File Offset: 0x0001FC0C
		public override float TimeSinceCasted
		{
			get
			{
				float cooldownLength = base.BaseAbility.CooldownLength;
				if (cooldownLength > 0f)
				{
					return Math.Max(cooldownLength, 10f) - base.BaseAbility.Cooldown;
				}
				return 9999999f;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x00006DA5 File Offset: 0x00004FA5
		public BlinkType BlinkType { get; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x00006DAD File Offset: 0x00004FAD
		protected override float BaseCastRange
		{
			get
			{
				return this.castRangeData.GetValue(this.Level) - 25f;
			}
		}

		// Token: 0x04000341 RID: 833
		private readonly SpecialData castRangeData;
	}
}
