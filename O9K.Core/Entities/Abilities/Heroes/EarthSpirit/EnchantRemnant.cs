using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.EarthSpirit
{
	// Token: 0x02000234 RID: 564
	[AbilityId(AbilityId.earth_spirit_petrify)]
	public class EnchantRemnant : RangedAbility, IShield, IActiveAbility
	{
		// Token: 0x06000A80 RID: 2688 RVA: 0x0002411C File Offset: 0x0002231C
		public EnchantRemnant(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.DurationData = new SpecialData(baseAbility, "duration");
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00009806 File Offset: 0x00007A06
		public UnitState AppliesUnitState { get; } = 256L;

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0000720E File Offset: 0x0000540E
		public override float CastRange
		{
			get
			{
				return base.CastRange + 100f;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0000980E File Offset: 0x00007A0E
		public string ShieldModifierName { get; } = "modifier_earthspirit_petrify";

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00009816 File Offset: 0x00007A16
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0000981E File Offset: 0x00007A1E
		public bool ShieldsOwner { get; }

		// Token: 0x06000A86 RID: 2694 RVA: 0x00024170 File Offset: 0x00022370
		public override int GetDamage(Unit9 unit)
		{
			float num = unit.HealthRegeneration * base.Duration;
			float num2 = this.DamageData.GetValue(this.Level) - num;
			float damageAmplification = unit.GetDamageAmplification(base.Owner, this.DamageType, true);
			float damageBlock = unit.GetDamageBlock(this.DamageType);
			return (int)((num2 - damageBlock) * damageAmplification);
		}
	}
}
