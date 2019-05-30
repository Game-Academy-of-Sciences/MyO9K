using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Axe
{
	// Token: 0x020003CC RID: 972
	[AbilityId(AbilityId.axe_culling_blade)]
	public class CullingBlade : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06001030 RID: 4144 RVA: 0x0000E40F File Offset: 0x0000C60F
		public CullingBlade(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.killThresholdData = new SpecialData(baseAbility, "kill_threshold");
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0000CB9C File Offset: 0x0000AD9C
		protected override float BaseCastRange
		{
			get
			{
				return base.BaseCastRange + 100f;
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00028A94 File Offset: 0x00026C94
		public override int GetDamage(Unit9 unit)
		{
			float value = this.killThresholdData.GetValue(this.Level);
			float num = unit.HealthRegeneration * this.GetCastDelay(unit) * 1.5f;
			if (unit.Health + num < value)
			{
				return (int)unit.MaximumHealth;
			}
			float value2 = this.DamageData.GetValue(this.Level);
			float damageAmplification = unit.GetDamageAmplification(base.Owner, this.DamageType, true);
			float damageBlock = unit.GetDamageBlock(this.DamageType);
			return (int)((value2 - damageBlock) * damageAmplification);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0000E43A File Offset: 0x0000C63A
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			damage[DamageType.HealthRemoval] = this.killThresholdData.GetValue(this.Level);
			return damage;
		}

		// Token: 0x0400086E RID: 2158
		private readonly SpecialData killThresholdData;
	}
}
