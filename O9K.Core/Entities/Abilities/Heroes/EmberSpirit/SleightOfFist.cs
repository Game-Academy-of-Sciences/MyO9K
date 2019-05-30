using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.EmberSpirit
{
	// Token: 0x02000382 RID: 898
	[AbilityId(AbilityId.ember_spirit_sleight_of_fist)]
	public class SleightOfFist : CircleAbility, INuke, IActiveAbility
	{
		// Token: 0x06000F64 RID: 3940 RVA: 0x0000D979 File Offset: 0x0000BB79
		public SleightOfFist(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.bonusHeroDamage = new SpecialData(baseAbility, "bonus_hero_damage");
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0000D9A4 File Offset: 0x0000BBA4
		public override float Radius
		{
			get
			{
				return base.Radius - 50f;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0000D9B2 File Offset: 0x0000BBB2
		public override bool IntelligenceAmplify { get; }

		// Token: 0x06000F67 RID: 3943 RVA: 0x00028254 File Offset: 0x00026454
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage rawAttackDamage = base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
			if (unit.IsHero)
			{
				Damage damage = rawAttackDamage;
				DamageType damageType = this.DamageType;
				damage[damageType] += this.bonusHeroDamage.GetValue(this.Level);
			}
			return rawAttackDamage;
		}

		// Token: 0x040007F6 RID: 2038
		private readonly SpecialData bonusHeroDamage;
	}
}
