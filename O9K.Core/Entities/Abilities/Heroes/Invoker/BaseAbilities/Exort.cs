using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Invoker.BaseAbilities
{
	// Token: 0x0200036B RID: 875
	[AbilityId(AbilityId.invoker_exort)]
	public class Exort : ActiveAbility
	{
		// Token: 0x06000F20 RID: 3872 RVA: 0x0000D57E File Offset: 0x0000B77E
		public Exort(Ability ability) : base(ability)
		{
			this.orbDamage = new SpecialData(ability, "bonus_damage_per_instance");
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0000D5A3 File Offset: 0x0000B7A3
		public int DamagePerOrb
		{
			get
			{
				return (int)this.orbDamage.GetValue(base.Level);
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00027F24 File Offset: 0x00026124
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

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0000D5B7 File Offset: 0x0000B7B7
		public string ModifierName { get; } = "modifier_invoker_exort_instance";

		// Token: 0x06000F24 RID: 3876 RVA: 0x0000D5BF File Offset: 0x0000B7BF
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return this.IsReady && !base.Owner.IsStunned && !base.Owner.IsSilenced;
		}

		// Token: 0x040007D7 RID: 2007
		private readonly SpecialData orbDamage;
	}
}
