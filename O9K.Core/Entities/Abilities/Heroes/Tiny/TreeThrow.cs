using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Tiny
{
	// Token: 0x020002A4 RID: 676
	[AbilityId(AbilityId.tiny_toss_tree)]
	public class TreeThrow : LineAbility
	{
		// Token: 0x06000C01 RID: 3073 RVA: 0x0000AC66 File Offset: 0x00008E66
		public TreeThrow(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "splash_radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0000AC91 File Offset: 0x00008E91
		public override bool BreaksLinkens { get; }

		// Token: 0x06000C03 RID: 3075 RVA: 0x0000AC99 File Offset: 0x00008E99
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			return base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
		}
	}
}
