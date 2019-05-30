using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200017B RID: 379
	[AbilityId(AbilityId.item_arcane_boots)]
	public class ArcaneBoots : AreaOfEffectAbility, IManaRestore, IActiveAbility
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x0000721C File Offset: 0x0000541C
		public ArcaneBoots(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "replenish_radius");
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00007244 File Offset: 0x00005444
		public bool RestoresAlly { get; } = 1;

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x0000724C File Offset: 0x0000544C
		public bool RestoresOwner { get; } = 1;
	}
}
