using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Heroes.Alchemist
{
	// Token: 0x020003D5 RID: 981
	[AbilityId(AbilityId.alchemist_unstable_concoction_throw)]
	public class UnstableConcoctionThrow : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x0600104E RID: 4174 RVA: 0x00028BD0 File Offset: 0x00026DD0
		public UnstableConcoctionThrow(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "midair_explosion_radius");
			this.SpeedData = new SpecialData(baseAbility, "movement_speed");
			this.brewTimeData = new SpecialData(baseAbility, "brew_time");
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0000E602 File Offset: 0x0000C802
		public float BrewTime
		{
			get
			{
				return this.brewTimeData.GetValue(this.Level);
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0000E615 File Offset: 0x0000C815
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x06001051 RID: 4177 RVA: 0x0000E61D File Offset: 0x0000C81D
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			if (this.IsUsable)
			{
				return base.CanBeCasted(checkChanneling);
			}
			return this.unstableConcoction.CanBeCasted(checkChanneling);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0000E63B File Offset: 0x0000C83B
		public override bool UseAbility(bool queue = false, bool bypass = false)
		{
			return this.unstableConcoction.UseAbility(queue, bypass);
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0000E64A File Offset: 0x0000C84A
		public override bool UseAbility(Unit9 mainTarget, List<Unit9> aoeTargets, HitChance minimumChance, int minAOETargets = 0, bool queue = false, bool bypass = false)
		{
			if (!this.IsUsable)
			{
				return this.unstableConcoction.UseAbility(queue, bypass);
			}
			return base.UseAbility(mainTarget, aoeTargets, minimumChance, minAOETargets, queue, bypass);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0000E673 File Offset: 0x0000C873
		public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
		{
			if (!this.IsUsable)
			{
				this.unstableConcoction.UseAbility(queue, bypass);
			}
			return base.UseAbility(target, minimumChance, queue, bypass);
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00028C20 File Offset: 0x00026E20
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.alchemist_unstable_concoction)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				throw new ArgumentNullException("unstableConcoction");
			}
			this.unstableConcoction = (UnstableConcoction)EntityManager9.AddAbility(ability);
		}

		// Token: 0x04000880 RID: 2176
		private readonly SpecialData brewTimeData;

		// Token: 0x04000881 RID: 2177
		private UnstableConcoction unstableConcoction;
	}
}
