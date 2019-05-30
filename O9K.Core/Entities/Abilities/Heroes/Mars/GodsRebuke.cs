using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Mars
{
	// Token: 0x02000336 RID: 822
	[AbilityId(AbilityId.mars_gods_rebuke)]
	public class GodsRebuke : ConeAbility, INuke, IActiveAbility
	{
		// Token: 0x06000E18 RID: 3608 RVA: 0x000272E0 File Offset: 0x000254E0
		public GodsRebuke(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "angle");
			this.EndRadiusData = new SpecialData(baseAbility, "radius");
			this.RangeData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "crit_mult");
			this.bonusHeroDamageData = new SpecialData(baseAbility, "bonus_damage_vs_heroes");
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0000C786 File Offset: 0x0000A986
		public override bool CanHitSpellImmuneEnemy { get; } = 1;

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00006E98 File Offset: 0x00005098
		public override float Range
		{
			get
			{
				return this.RangeData.GetValue(this.Level);
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0000C78E File Offset: 0x0000A98E
		public override bool IntelligenceAmplify { get; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0000C796 File Offset: 0x0000A996
		protected override float BaseCastRange
		{
			get
			{
				return base.BaseCastRange - 100f;
			}
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00027350 File Offset: 0x00025550
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			float physCritMultiplier = this.DamageData.GetValue(this.Level) / 100f;
			float additionalPhysicalDamage = unit.IsHero ? this.bonusHeroDamageData.GetValue(this.Level) : 0f;
			return base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, physCritMultiplier, additionalPhysicalDamage);
		}

		// Token: 0x0400075C RID: 1884
		private readonly SpecialData bonusHeroDamageData;
	}
}
