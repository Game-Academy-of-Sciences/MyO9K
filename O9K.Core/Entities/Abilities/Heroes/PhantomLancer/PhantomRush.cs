using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.PhantomLancer
{
	// Token: 0x020002FE RID: 766
	[AbilityId(AbilityId.phantom_lancer_phantom_edge)]
	public class PhantomRush : ToggleAbility
	{
		// Token: 0x06000D47 RID: 3399 RVA: 0x0000BCF1 File Offset: 0x00009EF1
		public PhantomRush(Ability baseAbility) : base(baseAbility)
		{
			this.rangeData = new SpecialData(baseAbility, "max_distance");
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0000BD0B File Offset: 0x00009F0B
		public override float Range
		{
			get
			{
				return this.rangeData.GetValue(this.Level);
			}
		}

		// Token: 0x040006DA RID: 1754
		private readonly SpecialData rangeData;
	}
}
