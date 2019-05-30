using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.PhantomAssassin
{
	// Token: 0x02000304 RID: 772
	[AbilityId(AbilityId.phantom_assassin_stifling_dagger)]
	public class StiflingDagger : RangedAbility, IDebuff, INuke, IActiveAbility
	{
		// Token: 0x06000D54 RID: 3412 RVA: 0x00026930 File Offset: 0x00024B30
		public StiflingDagger(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "base_damage");
			this.SpeedData = new SpecialData(baseAbility, "dagger_speed");
			this.multiplierData = new SpecialData(baseAbility, "attack_factor_tooltip");
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0000BDB9 File Offset: 0x00009FB9
		public override bool IntelligenceAmplify { get; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x0000BDC1 File Offset: 0x00009FC1
		public string DebuffModifierName { get; } = "modifier_phantom_assassin_stiflingdagger";

		// Token: 0x06000D57 RID: 3415 RVA: 0x00026984 File Offset: 0x00024B84
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage rawDamage = base.GetRawDamage(unit, remainingHealth);
			Damage rawAttackDamage = base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
			float num = this.multiplierData.GetValue(this.Level) / 100f;
			Damage damage = rawAttackDamage;
			DamageType damageType = this.DamageType;
			damage[damageType] *= num;
			return rawDamage + rawAttackDamage;
		}

		// Token: 0x040006E0 RID: 1760
		private readonly SpecialData multiplierData;
	}
}
