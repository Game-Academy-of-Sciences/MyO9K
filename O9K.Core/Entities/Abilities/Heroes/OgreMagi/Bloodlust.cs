using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.OgreMagi
{
	// Token: 0x02000312 RID: 786
	[AbilityId(AbilityId.ogre_magi_bloodlust)]
	public class Bloodlust : RangedAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000D9C RID: 3484 RVA: 0x0000C07B File Offset: 0x0000A27B
		public Bloodlust(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0000C09D File Offset: 0x0000A29D
		public string BuffModifierName { get; } = "modifier_ogre_magi_bloodlust";

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0000C0A5 File Offset: 0x0000A2A5
		public bool BuffsAlly { get; } = 1;

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0000C0AD File Offset: 0x0000A2AD
		public bool BuffsOwner { get; } = 1;
	}
}
