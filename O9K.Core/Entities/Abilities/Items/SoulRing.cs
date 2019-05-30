using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000158 RID: 344
	[AbilityId(AbilityId.item_soul_ring)]
	public class SoulRing : ActiveAbility, IManaRestore, IHasHealthCost, IActiveAbility
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x00006BF5 File Offset: 0x00004DF5
		public SoulRing(Ability baseAbility) : base(baseAbility)
		{
			this.healthCostData = new SpecialData(baseAbility, "health_sacrifice");
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x00006C16 File Offset: 0x00004E16
		public bool CanSuicide { get; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00006C1E File Offset: 0x00004E1E
		public int HealthCost
		{
			get
			{
				return (int)this.healthCostData.GetValue(1u);
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x00006C2D File Offset: 0x00004E2D
		public bool RestoresAlly { get; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x00006C35 File Offset: 0x00004E35
		public bool RestoresOwner { get; } = 1;

		// Token: 0x0400032D RID: 813
		private readonly SpecialData healthCostData;
	}
}
