using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.TemplarAssassin
{
	// Token: 0x020002B2 RID: 690
	[AbilityId(AbilityId.templar_assassin_meld)]
	public class Meld : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000C2A RID: 3114 RVA: 0x0000AF17 File Offset: 0x00009117
		public Meld(Ability baseAbility) : base(baseAbility)
		{
			base.IsInvisibility = true;
			this.DamageData = new SpecialData(baseAbility, "bonus_damage");
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0000AF3F File Offset: 0x0000913F
		public bool IsDispelActive
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_templar_assassin_4);
				return abilityById != null && abilityById.Level > 0u;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0000AF65 File Offset: 0x00009165
		public override bool BreaksLinkens { get; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0000AF6D File Offset: 0x0000916D
		public override float CastRange
		{
			get
			{
				return base.Owner.GetAttackRange(null, 0f);
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0000AF80 File Offset: 0x00009180
		public override bool NoTargetCast { get; }

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0000AF88 File Offset: 0x00009188
		public override float Speed
		{
			get
			{
				return (float)base.Owner.ProjectileSpeed;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0000AF96 File Offset: 0x00009196
		public override bool UnitTargetCast { get; } = 1;

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0000AF9E File Offset: 0x0000919E
		public override bool IntelligenceAmplify { get; }

		// Token: 0x06000C32 RID: 3122 RVA: 0x000256C0 File Offset: 0x000238C0
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage rawDamage = base.GetRawDamage(unit, remainingHealth);
			Damage rawAttackDamage = base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
			return rawDamage + rawAttackDamage;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0000AFA6 File Offset: 0x000091A6
		public override bool UseAbility(Unit9 target, bool queue = false, bool bypass = false)
		{
			bool flag = base.BaseAbility.UseAbility() && base.Owner.BaseUnit.Attack(target.BaseUnit);
			if (flag)
			{
				base.ActionSleeper.Sleep(0.1f);
			}
			return flag;
		}
	}
}
