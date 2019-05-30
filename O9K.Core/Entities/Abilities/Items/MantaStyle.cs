using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000193 RID: 403
	[AbilityId(AbilityId.item_manta)]
	public class MantaStyle : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x0000792B File Offset: 0x00005B2B
		public MantaStyle(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00007946 File Offset: 0x00005B46
		public string BuffModifierName { get; } = string.Empty;

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0000794E File Offset: 0x00005B4E
		public bool BuffsAlly { get; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00007956 File Offset: 0x00005B56
		public bool BuffsOwner { get; } = 1;
	}
}
