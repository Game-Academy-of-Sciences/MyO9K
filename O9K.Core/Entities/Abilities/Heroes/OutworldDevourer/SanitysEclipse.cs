using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.OutworldDevourer
{
	// Token: 0x0200030C RID: 780
	[AbilityId(AbilityId.obsidian_destroyer_sanity_eclipse)]
	public class SanitysEclipse : CircleAbility, INuke, IActiveAbility
	{
		// Token: 0x06000D81 RID: 3457 RVA: 0x0000BF9E File Offset: 0x0000A19E
		public SanitysEclipse(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage_multiplier");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00026D50 File Offset: 0x00024F50
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			float num = Math.Max(base.Owner.TotalIntelligence - unit.TotalIntelligence, 0f);
			Damage rawDamage;
			Damage result = rawDamage = base.GetRawDamage(unit, remainingHealth);
			DamageType damageType = this.DamageType;
			rawDamage[damageType] *= num;
			return result;
		}
	}
}
