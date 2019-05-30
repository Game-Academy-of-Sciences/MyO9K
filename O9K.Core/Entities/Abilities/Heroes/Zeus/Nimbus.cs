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

namespace O9K.Core.Entities.Abilities.Heroes.Zeus
{
	// Token: 0x020001B4 RID: 436
	[AbilityId(AbilityId.zuus_cloud)]
	public class Nimbus : CircleAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x060008D6 RID: 2262 RVA: 0x000080D7 File Offset: 0x000062D7
		public Nimbus(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "cloud_radius");
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00008105 File Offset: 0x00006305
		public override bool IntelligenceAmplify { get; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0000810D File Offset: 0x0000630D
		public override float CastRange { get; } = 9999999f;

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00008115 File Offset: 0x00006315
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x060008DA RID: 2266 RVA: 0x00022694 File Offset: 0x00020894
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			StaticField staticField = this.staticField;
			if (staticField != null && staticField.CanBeCasted(true))
			{
				damage += this.staticField.GetRawDamage(unit, remainingHealth);
			}
			LightningBolt lightningBolt = this.lightningBolt;
			if (lightningBolt != null && lightningBolt.IsValid)
			{
				damage += this.lightningBolt.GetBaseDamage();
			}
			return damage;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000226F8 File Offset: 0x000208F8
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.zuus_static_field)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability != null)
			{
				this.staticField = (StaticField)EntityManager9.AddAbility(ability);
			}
			ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.zuus_lightning_bolt)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability != null)
			{
				this.lightningBolt = (LightningBolt)EntityManager9.AddAbility(ability);
			}
		}

		// Token: 0x04000461 RID: 1121
		private LightningBolt lightningBolt;

		// Token: 0x04000462 RID: 1122
		private StaticField staticField;
	}
}
