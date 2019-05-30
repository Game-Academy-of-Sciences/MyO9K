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
using O9K.Core.Helpers.Damage;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Abilities.Heroes.LegionCommander
{
	// Token: 0x02000355 RID: 853
	[AbilityId(AbilityId.legion_commander_overwhelming_odds)]
	public class OverwhelmingOdds : CircleAbility, INuke, IActiveAbility
	{
		// Token: 0x06000E76 RID: 3702 RVA: 0x000274C8 File Offset: 0x000256C8
		public OverwhelmingOdds(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.heroDamageData = new SpecialData(baseAbility, "damage_per_hero");
			this.unitDamageData = new SpecialData(baseAbility, "damage_per_unit");
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00027520 File Offset: 0x00025720
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage rawDamage = base.GetRawDamage(unit, remainingHealth);
			float radius = this.Radius;
			float heroDamage = this.heroDamageData.GetValue(this.Level);
			float unitDamage = this.unitDamageData.GetValue(this.Level);
			IEnumerable<Unit9> source = from x in EntityManager9.Units
			where x.IsUnit && x.IsAlive && !x.IsInvulnerable && !x.IsMagicImmune && x.IsVisible && x.IsEnemy(this.Owner) && x.Distance(unit) < radius
			select x;
			Damage damage = rawDamage;
			DamageType damageType = this.DamageType;
			damage[damageType] += source.Sum(delegate(Unit9 x)
			{
				if (!x.IsHero || x.IsIllusion)
				{
					return unitDamage;
				}
				return heroDamage;
			});
			return rawDamage;
		}

		// Token: 0x0400078E RID: 1934
		private readonly SpecialData heroDamageData;

		// Token: 0x0400078F RID: 1935
		private readonly SpecialData unitDamageData;
	}
}
