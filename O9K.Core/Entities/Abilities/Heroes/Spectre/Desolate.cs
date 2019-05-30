using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Abilities.Heroes.Spectre
{
	// Token: 0x020002C1 RID: 705
	[AbilityId(AbilityId.spectre_desolate)]
	public class Desolate : PassiveAbility, IHasPassiveDamageIncrease
	{
		// Token: 0x06000C72 RID: 3186 RVA: 0x0000B351 File Offset: 0x00009551
		public Desolate(Ability baseAbility) : base(baseAbility)
		{
			this.radiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "bonus_damage");
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0000B38E File Offset: 0x0000958E
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0000B396 File Offset: 0x00009596
		public bool MultipliedByCrit { get; }

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0000B39E File Offset: 0x0000959E
		public string PassiveDamageModifierName { get; } = string.Empty;

		// Token: 0x06000C76 RID: 3190 RVA: 0x000258D8 File Offset: 0x00023AD8
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			if (!unit.IsBuilding && !unit.IsAlly(base.Owner) && !EntityManager9.Units.Any((Unit9 x) => x.IsUnit && !x.Equals(unit) && !x.IsInvulnerable && x.IsAlive && x.IsVisible && x.IsAlly(unit) && x.Distance(unit) < this.radiusData.GetValue(this.Level)))
			{
				damage[this.DamageType] = this.DamageData.GetValue(this.Level);
			}
			return damage;
		}

		// Token: 0x04000667 RID: 1639
		private readonly SpecialData radiusData;
	}
}
