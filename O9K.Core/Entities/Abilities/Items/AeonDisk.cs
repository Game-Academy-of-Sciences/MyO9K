using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000169 RID: 361
	[AbilityId(AbilityId.item_aeon_disk)]
	public class AeonDisk : PassiveAbility, IHasDamageAmplify
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x00006CCE File Offset: 0x00004ECE
		public AeonDisk(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00006CF0 File Offset: 0x00004EF0
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00006CF8 File Offset: 0x00004EF8
		public string AmplifierModifierName { get; } = "modifier_item_aeon_disk_buff";

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00006D00 File Offset: 0x00004F00
		public AmplifiesDamage AmplifiesDamage { get; } = 3;

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00006D08 File Offset: 0x00004F08
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00006D10 File Offset: 0x00004F10
		public bool IsAmplifierPermanent { get; }

		// Token: 0x06000749 RID: 1865 RVA: 0x00006D18 File Offset: 0x00004F18
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return -1f;
		}
	}
}
