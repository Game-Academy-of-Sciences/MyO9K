using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Grimstroke
{
	// Token: 0x02000377 RID: 887
	[AbilityId(AbilityId.grimstroke_dark_artistry)]
	public class StrokeOfFate : LineAbility, IDebuff, INuke, IActiveAbility
	{
		// Token: 0x06000F3F RID: 3903 RVA: 0x00027FC8 File Offset: 0x000261C8
		public StrokeOfFate(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "start_radius");
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0000D76C File Offset: 0x0000B96C
		public string DebuffModifierName { get; } = "modifier_grimstroke_dark_artistry_slow";

		// Token: 0x06000F41 RID: 3905 RVA: 0x0002801C File Offset: 0x0002621C
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = this.DamageData.GetValueWithTalentMultiplySimple(this.Level);
			return damage;
		}
	}
}
