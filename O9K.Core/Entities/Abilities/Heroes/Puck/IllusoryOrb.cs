using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Puck
{
	// Token: 0x020001F1 RID: 497
	[AbilityId(AbilityId.puck_illusory_orb)]
	public class IllusoryOrb : LineAbility, INuke, IActiveAbility
	{
		// Token: 0x060009BD RID: 2493 RVA: 0x00008C72 File Offset: 0x00006E72
		public IllusoryOrb(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.SpeedData = new SpecialData(baseAbility, "orb_speed");
			this.castRangeData = new SpecialData(baseAbility, "max_distance");
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x00008CAE File Offset: 0x00006EAE
		public override float Speed
		{
			get
			{
				return this.SpeedData.GetValueWithTalentMultiply(this.Level);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00008CC1 File Offset: 0x00006EC1
		protected override float BaseCastRange
		{
			get
			{
				return this.castRangeData.GetValueWithTalentMultiply(this.Level);
			}
		}

		// Token: 0x040004EA RID: 1258
		private readonly SpecialData castRangeData;
	}
}
