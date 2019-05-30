using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Heroes;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Abilities.Heroes.SkywrathMage
{
	// Token: 0x020001D5 RID: 469
	[AbilityId(AbilityId.skywrath_mage_concussive_shot)]
	public class ConcussiveShot : AreaOfEffectAbility, IDebuff, INuke, IActiveAbility
	{
		// Token: 0x0600095D RID: 2397 RVA: 0x00023064 File Offset: 0x00021264
		public ConcussiveShot(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "launch_radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0000875E File Offset: 0x0000695E
		public override float Radius
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_skywrath_4);
				if (abilityById != null && abilityById.Level > 0u)
				{
					return 9999999f;
				}
				return base.Radius;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0000878D File Offset: 0x0000698D
		public string DebuffModifierName { get; } = "modifier_skywrath_mage_concussive_shot_slow";

		// Token: 0x06000960 RID: 2400 RVA: 0x000230B8 File Offset: 0x000212B8
		public override bool CanHit(Unit9 target)
		{
			Hero9 other = (from x in EntityManager9.Heroes
			where x.IsAlive && x.IsVisible && !x.IsMagicImmune && !x.IsInvulnerable && !x.IsAlly(base.Owner) && x.Distance(base.Owner) < this.Radius
			orderby x.Distance(base.Owner)
			select x).FirstOrDefault<Hero9>();
			return target.Equals(other) && base.CanHit(target);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00023104 File Offset: 0x00021304
		public override bool CanHit(Unit9 mainTarget, List<Unit9> aoeTargets, int minCount)
		{
			Hero9 other = (from x in EntityManager9.Heroes
			where x.IsAlive && x.IsVisible && !x.IsMagicImmune && !x.IsInvulnerable && !x.IsAlly(base.Owner) && x.Distance(base.Owner) < this.Radius
			orderby x.Distance(base.Owner)
			select x).FirstOrDefault<Hero9>();
			return mainTarget.Equals(other) && base.CanHit(mainTarget, aoeTargets, minCount);
		}
	}
}
