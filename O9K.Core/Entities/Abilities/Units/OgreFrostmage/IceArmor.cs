using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Units.OgreFrostmage
{
	// Token: 0x020000E5 RID: 229
	[AbilityId(AbilityId.ogre_magi_frost_armor)]
	public class IceArmor : RangedAbility, IBuff, IActiveAbility
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x00006580 File Offset: 0x00004780
		public IceArmor(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x000065A2 File Offset: 0x000047A2
		public string BuffModifierName { get; } = "modifier_ogre_magi_frost_armor";

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x000065AA File Offset: 0x000047AA
		public bool BuffsAlly { get; } = 1;

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x000065B2 File Offset: 0x000047B2
		public bool BuffsOwner { get; } = 1;
	}
}
