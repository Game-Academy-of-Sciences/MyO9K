using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Chen
{
	// Token: 0x02000259 RID: 601
	[AbilityId(AbilityId.chen_divine_favor)]
	public class DivineFavor : RangedAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000AFA RID: 2810 RVA: 0x00009EA0 File Offset: 0x000080A0
		public DivineFavor(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00009EC2 File Offset: 0x000080C2
		public string BuffModifierName { get; } = "modifier_chen_divine_favor";

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00009ECA File Offset: 0x000080CA
		public bool BuffsAlly { get; } = 1;

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00009ED2 File Offset: 0x000080D2
		public bool BuffsOwner { get; } = 1;
	}
}
