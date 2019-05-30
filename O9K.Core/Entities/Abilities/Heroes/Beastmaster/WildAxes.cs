using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Beastmaster
{
	// Token: 0x020003C7 RID: 967
	[AbilityId(AbilityId.beastmaster_wild_axes)]
	public class WildAxes : LineAbility, INuke, IActiveAbility
	{
		// Token: 0x0600101D RID: 4125 RVA: 0x00028A10 File Offset: 0x00026C10
		public WildAxes(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "spread");
			this.DamageData = new SpecialData(baseAbility, "axe_damage");
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x0000E311 File Offset: 0x0000C511
		public override float ActivationDelay { get; } = 0.1f;

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x0000E319 File Offset: 0x0000C519
		public override float Radius
		{
			get
			{
				return base.Radius * 0.75f;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0000E327 File Offset: 0x0000C527
		public override float Range
		{
			get
			{
				return this.CastRange;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x0000E32F File Offset: 0x0000C52F
		public override float Speed { get; } = 1200f;

		// Token: 0x06001022 RID: 4130 RVA: 0x00028A5C File Offset: 0x00026C5C
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = this.DamageData.GetValue(this.Level) * 2f;
			return damage;
		}
	}
}
