using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Viper
{
	// Token: 0x0200027D RID: 637
	[AbilityId(AbilityId.viper_nethertoxin)]
	public class Nethertoxin : CircleAbility, IDisable, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x00024FFC File Offset: 0x000231FC
		public Nethertoxin(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
			this.amplifierData = new SpecialData(baseAbility, "magic_resistance");
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0000A52B File Offset: 0x0000872B
		public DamageType AmplifierDamageType { get; } = 2;

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0000A533 File Offset: 0x00008733
		public string AmplifierModifierName { get; } = "modifier_viper_nethertoxin";

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0000A53B File Offset: 0x0000873B
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0000A543 File Offset: 0x00008743
		public UnitState AppliesUnitState
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_viper_3);
				if (abilityById != null && abilityById.Level > 0u)
				{
					return UnitState.Silenced | UnitState.PassivesDisabled;
				}
				return UnitState.PassivesDisabled;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0000A573 File Offset: 0x00008773
		public bool IsAmplifierAddedToStats { get; } = 1;

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0000A57B File Offset: 0x0000877B
		public bool IsAmplifierPermanent { get; }

		// Token: 0x06000B7E RID: 2942 RVA: 0x0000A583 File Offset: 0x00008783
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / -100f;
		}

		// Token: 0x040005DD RID: 1501
		private readonly SpecialData amplifierData;
	}
}
