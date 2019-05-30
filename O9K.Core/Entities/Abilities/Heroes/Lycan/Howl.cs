using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Lycan
{
	// Token: 0x0200033B RID: 827
	[AbilityId(AbilityId.lycan_howl)]
	public class Howl : AreaOfEffectAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000E29 RID: 3625 RVA: 0x0000C7EF File Offset: 0x0000A9EF
		public Howl(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0000C81C File Offset: 0x0000AA1C
		public string BuffModifierName { get; } = "modifier_lycan_howl";

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0000C824 File Offset: 0x0000AA24
		public bool BuffsAlly { get; } = 1;

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0000C82C File Offset: 0x0000AA2C
		public bool BuffsOwner { get; } = 1;

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0000C834 File Offset: 0x0000AA34
		public override float Radius { get; } = 9999999f;
	}
}
