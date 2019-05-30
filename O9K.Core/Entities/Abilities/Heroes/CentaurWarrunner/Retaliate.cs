using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.CentaurWarrunner
{
	// Token: 0x020003A4 RID: 932
	[AbilityId(AbilityId.centaur_return)]
	public class Retaliate : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000FC7 RID: 4039 RVA: 0x0000DECC File Offset: 0x0000C0CC
		public Retaliate(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x0000DEE7 File Offset: 0x0000C0E7
		public string BuffModifierName { get; } = "modifier_centaur_return_bonus_damage";

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0000DEEF File Offset: 0x0000C0EF
		public bool BuffsAlly { get; }

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x0000DEF7 File Offset: 0x0000C0F7
		public bool BuffsOwner { get; } = 1;
	}
}
