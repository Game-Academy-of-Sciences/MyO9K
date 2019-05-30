using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Tusk
{
	// Token: 0x02000296 RID: 662
	[AbilityId(AbilityId.tusk_walrus_punch)]
	public class WalrusPunch : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000BBF RID: 3007 RVA: 0x0000A995 File Offset: 0x00008B95
		public WalrusPunch(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "crit_multiplier");
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override float CastPoint
		{
			get
			{
				return base.Owner.GetAttackPoint(null);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0000720E File Offset: 0x0000540E
		public override float CastRange
		{
			get
			{
				return base.CastRange + 100f;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0000A9C4 File Offset: 0x00008BC4
		public override DamageType DamageType { get; } = 1;

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0000A9CC File Offset: 0x00008BCC
		public override bool IntelligenceAmplify { get; }

		// Token: 0x06000BC4 RID: 3012 RVA: 0x000251F0 File Offset: 0x000233F0
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			float physCritMultiplier = this.DamageData.GetValue(this.Level) / 100f;
			return base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, physCritMultiplier, 0f);
		}
	}
}
