using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Invoker.BaseAbilities
{
	// Token: 0x0200036D RID: 877
	[AbilityId(AbilityId.invoker_quas)]
	public class Quas : ActiveAbility
	{
		// Token: 0x06000F26 RID: 3878 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		public Quas(Ability ability) : base(ability)
		{
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00027F24 File Offset: 0x00026124
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

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0000D5FC File Offset: 0x0000B7FC
		public string ModifierName { get; } = "modifier_invoker_quas_instance";

		// Token: 0x06000F29 RID: 3881 RVA: 0x0000D5BF File Offset: 0x0000B7BF
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return this.IsReady && !base.Owner.IsStunned && !base.Owner.IsSilenced;
		}
	}
}
