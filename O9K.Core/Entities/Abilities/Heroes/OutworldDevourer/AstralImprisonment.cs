using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.OutworldDevourer
{
	// Token: 0x0200030B RID: 779
	[AbilityId(AbilityId.obsidian_destroyer_astral_imprisonment)]
	public class AstralImprisonment : RangedAbility, IDisable, INuke, IShield, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000D7A RID: 3450 RVA: 0x00026CD8 File Offset: 0x00024ED8
		public AstralImprisonment(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DurationData = new SpecialData(baseAbility, "prison_duration");
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x0000BF76 File Offset: 0x0000A176
		public UnitState AppliesUnitState { get; } = 288L;

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0000BF7E File Offset: 0x0000A17E
		public string ImmobilityModifierName { get; } = "modifier_obsidian_destroyer_astral_imprisonment_prison";

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x0000BF86 File Offset: 0x0000A186
		public string ShieldModifierName { get; } = "modifier_obsidian_destroyer_astral_imprisonment_prison";

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0000BF8E File Offset: 0x0000A18E
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x0000BF96 File Offset: 0x0000A196
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x06000D80 RID: 3456 RVA: 0x00024170 File Offset: 0x00022370
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
