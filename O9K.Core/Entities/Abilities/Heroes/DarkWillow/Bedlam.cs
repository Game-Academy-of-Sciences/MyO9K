using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.DarkWillow
{
	// Token: 0x02000397 RID: 919
	[AbilityId(AbilityId.dark_willow_bedlam)]
	public class Bedlam : AreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x06000F9F RID: 3999 RVA: 0x00028460 File Offset: 0x00026660
		public Bedlam(Ability baseAbility) : base(baseAbility)
		{
			this.attackRadius = new SpecialData(baseAbility, "attack_radius");
			this.RadiusData = new SpecialData(baseAbility, "roaming_radius");
			this.DamageData = new SpecialData(baseAbility, "attack_damage");
			base.IsUltimate = false;
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0000DC8C File Offset: 0x0000BE8C
		public override float Radius
		{
			get
			{
				return this.RadiusData.GetValue(this.Level) + this.attackRadius.GetValue(this.Level);
			}
		}

		// Token: 0x04000818 RID: 2072
		private readonly SpecialData attackRadius;
	}
}
