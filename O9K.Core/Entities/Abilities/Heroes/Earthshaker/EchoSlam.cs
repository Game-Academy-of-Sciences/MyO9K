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

namespace O9K.Core.Entities.Abilities.Heroes.Earthshaker
{
	// Token: 0x0200023A RID: 570
	[AbilityId(AbilityId.earthshaker_echo_slam)]
	public class EchoSlam : AreaOfEffectAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x00024240 File Offset: 0x00022440
		public EchoSlam(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "echo_slam_damage_range");
			this.DamageData = new SpecialData(baseAbility, "echo_slam_initial_damage");
			this.echoDamageData = new SpecialData(baseAbility, "echo_slam_echo_damage");
			this.searchRadius = new SpecialData(baseAbility, "echo_slam_echo_search_range");
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x000098C9 File Offset: 0x00007AC9
		public override float Radius
		{
			get
			{
				return base.Radius - 100f;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x000098D7 File Offset: 0x00007AD7
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x06000A94 RID: 2708 RVA: 0x000242A4 File Offset: 0x000224A4
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = this.DamageData.GetValue(this.Level);
			Damage damage2 = damage;
			float damageSearchRadius = this.searchRadius.GetValue(this.Level);
			float radius = this.Radius;
			int num = (from x in EntityManager9.Units
			where x.IsUnit && !x.Equals(unit) && x.IsAlive && x.IsVisible && !x.IsInvulnerable && x.IsEnemy(this.Owner) && x.Distance(unit) < damageSearchRadius && x.Distance(this.Owner) < radius
			select x).Sum(delegate(Unit9 x)
			{
				if (!x.IsHero || x.IsIllusion)
				{
					return 1;
				}
				return 2;
			});
			Damage damage3 = damage2;
			damageType = this.DamageType;
			damage3[damageType] += this.echoDamageData.GetValue(this.Level) * (float)num;
			Aftershock aftershock = this.aftershock;
			if (aftershock != null && aftershock.CanBeCasted(true) && base.Owner.Distance(unit) < this.aftershock.Radius)
			{
				damage2 += this.aftershock.GetRawDamage(unit, null);
			}
			return damage2;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000243D0 File Offset: 0x000225D0
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.earthshaker_aftershock)
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
			this.aftershock = (Aftershock)EntityManager9.AddAbility(ability);
		}

		// Token: 0x04000558 RID: 1368
		private readonly SpecialData echoDamageData;

		// Token: 0x04000559 RID: 1369
		private readonly SpecialData searchRadius;

		// Token: 0x0400055A RID: 1370
		private Aftershock aftershock;
	}
}
