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
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Heroes.Earthshaker
{
	// Token: 0x0200023E RID: 574
	[AbilityId(AbilityId.earthshaker_enchant_totem)]
	public class EnchantTotem : CircleAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000A9D RID: 2717 RVA: 0x00009900 File Offset: 0x00007B00
		public EnchantTotem(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "aftershock_range");
			this.scepterRange = new SpecialData(baseAbility, "distance_scepter");
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00009934 File Offset: 0x00007B34
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0000993C File Offset: 0x00007B3C
		public override DamageType DamageType
		{
			get
			{
				if (this.aftershock != null)
				{
					return this.aftershock.DamageType;
				}
				return base.DamageType;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0000995E File Offset: 0x00007B5E
		public override bool NoTargetCast
		{
			get
			{
				return !base.Owner.HasAghanimsScepter;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0000996E File Offset: 0x00007B6E
		public override bool PositionCast
		{
			get
			{
				return !this.NoTargetCast;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00009979 File Offset: 0x00007B79
		public override SkillShotType SkillShotType
		{
			get
			{
				if (!this.NoTargetCast)
				{
					return SkillShotType.Circle;
				}
				return SkillShotType.AreaOfEffect;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00009986 File Offset: 0x00007B86
		protected override float BaseCastRange
		{
			get
			{
				if (this.NoTargetCast)
				{
					return 0f;
				}
				return this.scepterRange.GetValue(this.Level);
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002452C File Offset: 0x0002272C
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Aftershock aftershock = this.aftershock;
			if (aftershock != null && aftershock.CanBeCasted(true) && base.Owner.Distance(unit) < this.aftershock.Radius)
			{
				return this.aftershock.GetRawDamage(unit, null);
			}
			return new Damage();
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00024584 File Offset: 0x00022784
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

		// Token: 0x04000563 RID: 1379
		private readonly SpecialData scepterRange;

		// Token: 0x04000564 RID: 1380
		private Aftershock aftershock;
	}
}
