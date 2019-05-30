using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.AntiMage
{
	// Token: 0x020003D0 RID: 976
	[AbilityId(AbilityId.antimage_mana_void)]
	public class ManaVoid : RangedAreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x06001041 RID: 4161 RVA: 0x0000E51E File Offset: 0x0000C71E
		public ManaVoid(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "mana_void_aoe_radius");
			this.DamageData = new SpecialData(baseAbility, "mana_void_damage_per_mana");
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0000E549 File Offset: 0x0000C749
		public override bool CanHitSpellImmuneEnemy { get; }

		// Token: 0x06001043 RID: 4163 RVA: 0x00028B8C File Offset: 0x00026D8C
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			float value = this.DamageData.GetValue(this.Level);
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = (float)((int)((unit.MaximumMana - unit.Mana) * value));
			return damage;
		}
	}
}
