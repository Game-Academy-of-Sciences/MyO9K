using System;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.NyxAssassin
{
	// Token: 0x02000319 RID: 793
	[AbilityId(AbilityId.nyx_assassin_mana_burn)]
	public class ManaBurn : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000DB1 RID: 3505 RVA: 0x0000C1D7 File Offset: 0x0000A3D7
		public ManaBurn(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "float_multiplier");
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x0000C1F1 File Offset: 0x0000A3F1
		protected override float BaseCastRange
		{
			get
			{
				if (base.Owner.HasModifier("modifier_nyx_assassin_burrow"))
				{
					return base.Owner.GetAbilityById(AbilityId.nyx_assassin_burrow).GetAbilitySpecialData("mana_burn_burrow_range_tooltip", 0u);
				}
				return base.BaseCastRange;
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00026EA4 File Offset: 0x000250A4
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			float value = this.DamageData.GetValue(this.Level);
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = (float)((int)Math.Min(value * unit.TotalIntelligence, unit.Mana));
			return damage;
		}
	}
}
