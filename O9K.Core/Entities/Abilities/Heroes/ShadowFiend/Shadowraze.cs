using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.ShadowFiend
{
	// Token: 0x020002D7 RID: 727
	[AbilityId(AbilityId.nevermore_shadowraze1)]
	[AbilityId(AbilityId.nevermore_shadowraze2)]
	[AbilityId(AbilityId.nevermore_shadowraze3)]
	public class Shadowraze : AreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x06000CB9 RID: 3257 RVA: 0x00025C98 File Offset: 0x00023E98
		public Shadowraze(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "shadowraze_radius");
			this.DamageData = new SpecialData(baseAbility, "shadowraze_damage");
			this.castRangeData = new SpecialData(baseAbility, "shadowraze_range");
			this.bonusDamageData = new SpecialData(baseAbility, "stack_bonus_damage");
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0000B70E File Offset: 0x0000990E
		public override float Radius
		{
			get
			{
				return base.Radius + 25f;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0000B71C File Offset: 0x0000991C
		public override float CastRange
		{
			get
			{
				return this.castRangeData.GetValue(this.Level);
			}
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00025CF0 File Offset: 0x00023EF0
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage rawDamage = base.GetRawDamage(unit, remainingHealth);
			Modifier modifier = unit.BaseModifiers.FirstOrDefault((Modifier x) => x.Name == "modifier_nevermore_shadowraze_debuff");
			int num = (modifier != null) ? modifier.StackCount : 0;
			float value = this.bonusDamageData.GetValue(this.Level);
			Damage damage = rawDamage;
			DamageType damageType = this.DamageType;
			damage[damageType] += (float)num * value;
			return rawDamage;
		}

		// Token: 0x04000690 RID: 1680
		private readonly SpecialData bonusDamageData;

		// Token: 0x04000691 RID: 1681
		private readonly SpecialData castRangeData;
	}
}
