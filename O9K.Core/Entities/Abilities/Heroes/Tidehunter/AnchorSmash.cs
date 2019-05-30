using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Tidehunter
{
	// Token: 0x020002AD RID: 685
	[AbilityId(AbilityId.tidehunter_anchor_smash)]
	public class AnchorSmash : AreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x06000C17 RID: 3095 RVA: 0x0000ADBF File Offset: 0x00008FBF
		public AnchorSmash(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "attack_damage");
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0000ADEA File Offset: 0x00008FEA
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			return base.GetRawDamage(unit, remainingHealth) + base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
		}
	}
}
