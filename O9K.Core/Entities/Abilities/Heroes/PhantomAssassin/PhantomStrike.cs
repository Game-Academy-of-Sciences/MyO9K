using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.PhantomAssassin
{
	// Token: 0x02000303 RID: 771
	[AbilityId(AbilityId.phantom_assassin_phantom_strike)]
	public class PhantomStrike : RangedAbility, IBlink, INuke, IActiveAbility
	{
		// Token: 0x06000D51 RID: 3409 RVA: 0x0000BDA1 File Offset: 0x00009FA1
		public PhantomStrike(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0000BDB1 File Offset: 0x00009FB1
		public BlinkType BlinkType { get; } = 2;

		// Token: 0x06000D53 RID: 3411 RVA: 0x0000AC99 File Offset: 0x00008E99
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			return base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
		}
	}
}
