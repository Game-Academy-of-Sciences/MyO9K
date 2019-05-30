using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Magnus
{
	// Token: 0x02000208 RID: 520
	[AbilityId(AbilityId.magnataur_skewer)]
	public class Skewer : LineAbility, IBlink, IActiveAbility
	{
		// Token: 0x06000A00 RID: 2560 RVA: 0x0002397C File Offset: 0x00021B7C
		public Skewer(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "skewer_radius");
			this.DamageData = new SpecialData(baseAbility, "skewer_damage");
			this.SpeedData = new SpecialData(baseAbility, "skewer_speed");
			this.castRangeData = new SpecialData(baseAbility, "range");
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0000907A File Offset: 0x0000727A
		public BlinkType BlinkType { get; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00009082 File Offset: 0x00007282
		protected override float BaseCastRange
		{
			get
			{
				return this.castRangeData.GetValue(this.Level);
			}
		}

		// Token: 0x0400050E RID: 1294
		private readonly SpecialData castRangeData;
	}
}
