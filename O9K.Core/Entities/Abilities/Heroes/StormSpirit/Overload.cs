using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.StormSpirit
{
	// Token: 0x020002B9 RID: 697
	[AbilityId(AbilityId.storm_spirit_overload)]
	public class Overload : PassiveAbility, IHasPassiveDamageIncrease
	{
		// Token: 0x06000C57 RID: 3159 RVA: 0x0000B19D File Offset: 0x0000939D
		public Overload(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0000B1B1 File Offset: 0x000093B1
		public bool IsPassiveDamagePermanent { get; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0000B1B9 File Offset: 0x000093B9
		public bool MultipliedByCrit { get; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0000B1C1 File Offset: 0x000093C1
		public string PassiveDamageModifierName { get; } = "modifier_storm_spirit_overload";

		// Token: 0x06000C5B RID: 3163 RVA: 0x000257A4 File Offset: 0x000239A4
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			if (!unit.IsBuilding && !unit.IsAlly(base.Owner))
			{
				damage[this.DamageType] = this.DamageData.GetValue(this.Level);
			}
			return damage;
		}
	}
}
