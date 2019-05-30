using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Abilities.Heroes.Tiny
{
	// Token: 0x020002A2 RID: 674
	[AbilityId(AbilityId.tiny_toss)]
	public class Toss : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000BF4 RID: 3060 RVA: 0x0000ABCC File Offset: 0x00008DCC
		public Toss(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "toss_damage");
			this.RadiusData = new SpecialData(baseAbility, "grab_radius");
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0000ABF7 File Offset: 0x00008DF7
		public override bool BreaksLinkens { get; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0000ABFF File Offset: 0x00008DFF
		public override bool CanHitSpellImmuneEnemy { get; }

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002533C File Offset: 0x0002353C
		public override bool CanHit(Unit9 target)
		{
			if (target.IsMagicImmune && ((target.IsEnemy(base.Owner) && !this.CanHitSpellImmuneEnemy) || (target.IsAlly(base.Owner) && !this.CanHitSpellImmuneAlly)))
			{
				return false;
			}
			if (base.Owner.Distance(target) > this.CastRange)
			{
				return false;
			}
			Unit9 unit = (from x in EntityManager9.Units
			where x.IsUnit && x.IsAlive && x.IsVisible && !x.IsMagicImmune && !x.IsInvulnerable && !x.Equals(base.Owner) && x.Distance(base.Owner) < this.RadiusData.GetValue(this.Level)
			orderby x.Distance(base.Owner)
			select x).FirstOrDefault<Unit9>();
			return !(unit == null) && (!unit.IsHero || unit.IsIllusion || !unit.IsAlly(base.Owner));
		}
	}
}
