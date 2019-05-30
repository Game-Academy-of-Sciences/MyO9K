using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Invoker.BaseAbilities
{
	// Token: 0x0200036E RID: 878
	[AbilityId(AbilityId.invoker_wex)]
	public class Wex : ActiveAbility
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x0000D604 File Offset: 0x0000B804
		public Wex(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00027F24 File Offset: 0x00026124
		public override uint Level
		{
			get
			{
				uint num = base.Level;
				if (base.Owner.HasAghanimsScepter)
				{
					num += 1u;
				}
				return num;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0000D618 File Offset: 0x0000B818
		public string ModifierName { get; } = "modifier_invoker_wex_instance";

		// Token: 0x06000F2D RID: 3885 RVA: 0x0000D5BF File Offset: 0x0000B7BF
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return this.IsReady && !base.Owner.IsStunned && !base.Owner.IsSilenced;
		}
	}
}
