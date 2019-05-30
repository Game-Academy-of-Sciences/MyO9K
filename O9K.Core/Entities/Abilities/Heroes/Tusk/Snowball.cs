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

namespace O9K.Core.Entities.Abilities.Heroes.Tusk
{
	// Token: 0x02000291 RID: 657
	[AbilityId(AbilityId.tusk_snowball)]
	public class Snowball : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000BB1 RID: 2993 RVA: 0x0000A85B File Offset: 0x00008A5B
		public Snowball(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "snowball_damage");
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0000A87E File Offset: 0x00008A7E
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0000A886 File Offset: 0x00008A86
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			if (!base.IsActive)
			{
				return this.launch.CanBeCasted(checkChanneling);
			}
			return base.CanBeCasted(checkChanneling);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		public override bool UseAbility(Unit9 target, bool queue = false, bool bypass = false)
		{
			if (!base.IsActive)
			{
				return this.launch.UseAbility(queue, bypass);
			}
			return base.UseAbility(target, queue, bypass);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0000A8C5 File Offset: 0x00008AC5
		public override bool UseAbility(Unit9 mainTarget, List<Unit9> aoeTargets, HitChance minimumChance, int minAOETargets = 0, bool queue = false, bool bypass = false)
		{
			if (!base.IsActive)
			{
				return this.launch.UseAbility(queue, bypass);
			}
			return base.UseAbility(mainTarget, aoeTargets, minimumChance, minAOETargets, queue, bypass);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0000A8EE File Offset: 0x00008AEE
		public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
		{
			if (!base.IsActive)
			{
				return this.launch.UseAbility(queue, bypass);
			}
			return base.UseAbility(target, minimumChance, queue, bypass);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00025108 File Offset: 0x00023308
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.tusk_launch_snowball)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				throw new ArgumentNullException("launch");
			}
			this.launch = (LaunchSnowball)EntityManager9.AddAbility(ability);
		}

		// Token: 0x040005FF RID: 1535
		private LaunchSnowball launch;
	}
}
