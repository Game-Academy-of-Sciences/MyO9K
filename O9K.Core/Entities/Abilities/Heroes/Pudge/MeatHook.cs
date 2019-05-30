using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using O9K.Core.Prediction.Collision;

namespace O9K.Core.Entities.Abilities.Heroes.Pudge
{
	// Token: 0x020002F1 RID: 753
	[AbilityId(AbilityId.pudge_meat_hook)]
	public class MeatHook : LineAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000D15 RID: 3349 RVA: 0x00026360 File Offset: 0x00024560
		public MeatHook(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "hook_width");
			this.SpeedData = new SpecialData(baseAbility, "hook_speed");
			this.scepterDamageData = new SpecialData(baseAbility, "damage_scepter");
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0000BB1A File Offset: 0x00009D1A
		public override bool CanHitSpellImmuneEnemy { get; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0000BB22 File Offset: 0x00009D22
		public override CollisionTypes CollisionTypes { get; } = 30;

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0000BB2A File Offset: 0x00009D2A
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x06000D19 RID: 3353 RVA: 0x000263B8 File Offset: 0x000245B8
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			if (base.Owner.HasAghanimsScepter)
			{
				Damage damage = new Damage();
				DamageType damageType = this.DamageType;
				damage[damageType] = this.scepterDamageData.GetValue(this.Level);
				return damage;
			}
			return base.GetRawDamage(unit, remainingHealth);
		}

		// Token: 0x040006C4 RID: 1732
		private readonly SpecialData scepterDamageData;
	}
}
