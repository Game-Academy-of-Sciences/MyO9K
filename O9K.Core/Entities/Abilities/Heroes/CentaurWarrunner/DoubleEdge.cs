using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.CentaurWarrunner
{
	// Token: 0x020003A6 RID: 934
	[AbilityId(AbilityId.centaur_double_edge)]
	public class DoubleEdge : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000FD2 RID: 4050 RVA: 0x0000DF53 File Offset: 0x0000C153
		public DoubleEdge(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "edge_damage");
			this.strengthDamageData = new SpecialData(baseAbility, "strength_damage");
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0000720E File Offset: 0x0000540E
		public override float CastRange
		{
			get
			{
				return base.CastRange + 100f;
			}
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00028764 File Offset: 0x00026964
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = this.DamageData.GetValue(this.Level) + base.Owner.TotalStrength * this.strengthDamageData.GetValue(this.Level) / 100f;
			return damage;
		}

		// Token: 0x04000838 RID: 2104
		private readonly SpecialData strengthDamageData;
	}
}
