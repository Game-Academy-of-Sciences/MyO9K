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

namespace O9K.Core.Entities.Abilities.Heroes.Tinker
{
	// Token: 0x020001CB RID: 459
	[AbilityId(AbilityId.tinker_heat_seeking_missile)]
	public class HeatSeekingMissile : AreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x0600093A RID: 2362 RVA: 0x00022E58 File Offset: 0x00021058
		public HeatSeekingMissile(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.scpeterTargetsData = new SpecialData(baseAbility, "targets_scepter");
			this.targetsData = new SpecialData(baseAbility, "targets");
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00022EC4 File Offset: 0x000210C4
		public override bool CanHit(Unit9 target)
		{
			int count = (int)(base.Owner.HasAghanimsScepter ? this.scpeterTargetsData.GetValue(this.Level) : this.targetsData.GetValue(this.Level));
			return (from x in EntityManager9.Units
			where x.IsHero && x.IsAlive && x.IsVisible && !x.IsMagicImmune && !x.IsInvulnerable && x.Distance(base.Owner) < this.Radius
			orderby x.Distance(base.Owner)
			select x).Take(count).Contains(target) && base.CanHit(target);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00022F44 File Offset: 0x00021144
		public override bool CanHit(Unit9 mainTarget, List<Unit9> aoeTargets, int minCount)
		{
			int count = (int)(base.Owner.HasAghanimsScepter ? this.scpeterTargetsData.GetValue(this.Level) : this.targetsData.GetValue(this.Level));
			return (from x in EntityManager9.Units
			where x.IsHero && x.IsAlive && x.IsVisible && !x.IsMagicImmune && !x.IsInvulnerable && x.Distance(base.Owner) < this.Radius
			orderby x.Distance(base.Owner)
			select x).Take(count).Contains(mainTarget) && base.CanHit(mainTarget, aoeTargets, minCount);
		}

		// Token: 0x0400049F RID: 1183
		private readonly SpecialData scpeterTargetsData;

		// Token: 0x040004A0 RID: 1184
		private readonly SpecialData targetsData;
	}
}
