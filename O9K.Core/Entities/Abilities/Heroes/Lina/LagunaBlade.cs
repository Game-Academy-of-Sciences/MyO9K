using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Lina
{
	// Token: 0x0200034C RID: 844
	[AbilityId(AbilityId.lina_laguna_blade)]
	public class LagunaBlade : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000E4E RID: 3662 RVA: 0x0000C9BD File Offset: 0x0000ABBD
		public LagunaBlade(Ability baseAbility) : base(baseAbility)
		{
			this.ActivationDelayData = new SpecialData(baseAbility, "damage_delay");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x00007F8A File Offset: 0x0000618A
		public override bool CanHitSpellImmuneEnemy
		{
			get
			{
				return base.Owner.HasAghanimsScepter;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		public override DamageType DamageType
		{
			get
			{
				if (base.Owner.HasAghanimsScepter)
				{
					return DamageType.Pure;
				}
				return base.DamageType;
			}
		}
	}
}
