using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Abilities.Heroes.Sniper
{
	// Token: 0x020002CA RID: 714
	[AbilityId(AbilityId.sniper_assassinate)]
	public class Assassinate : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000C8E RID: 3214 RVA: 0x000259CC File Offset: 0x00023BCC
		public Assassinate(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
			this.RadiusData = new SpecialData(baseAbility, "scepter_radius");
			this.scepterDamageData = new SpecialData(baseAbility, "scepter_crit_bonus");
			this.castPointData = new SpecialData(baseAbility, "total_cast_time_tooltip");
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0000B52F File Offset: 0x0000972F
		public override float CastPoint
		{
			get
			{
				return this.castPointData.GetValueWithTalentSubtract(this.Level);
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0000B542 File Offset: 0x00009742
		public override DamageType DamageType
		{
			get
			{
				if (base.Owner.HasAghanimsScepter)
				{
					return DamageType.Physical;
				}
				return base.DamageType;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x00007F8A File Offset: 0x0000618A
		public override bool PositionCast
		{
			get
			{
				return base.Owner.HasAghanimsScepter;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0000AE10 File Offset: 0x00009010
		public override float Radius
		{
			get
			{
				if (base.Owner.HasAghanimsScepter)
				{
					return this.RadiusData.GetValue(this.Level);
				}
				return 0f;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00007F9C File Offset: 0x0000619C
		public override bool UnitTargetCast
		{
			get
			{
				return !this.PositionCast;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0000B559 File Offset: 0x00009759
		public override bool IntelligenceAmplify
		{
			get
			{
				return !base.Owner.HasAghanimsScepter;
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00025A24 File Offset: 0x00023C24
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			if (base.Owner.HasAghanimsScepter)
			{
				float physCritMultiplier = this.scepterDamageData.GetValue(this.Level) / 100f;
				Damage rawAttackDamage = base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, physCritMultiplier, 0f);
				Headshot headshot = this.headshot;
				return rawAttackDamage + ((headshot != null) ? headshot.GetRawDamage(unit, null) : null);
			}
			return base.GetRawDamage(unit, remainingHealth);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00025A94 File Offset: 0x00023C94
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.sniper_headshot)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				return;
			}
			this.headshot = (Headshot)EntityManager9.AddAbility(ability);
		}

		// Token: 0x0400067B RID: 1659
		private readonly SpecialData castPointData;

		// Token: 0x0400067C RID: 1660
		private readonly SpecialData scepterDamageData;

		// Token: 0x0400067D RID: 1661
		private Headshot headshot;
	}
}
