using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.Warlock
{
	// Token: 0x02000272 RID: 626
	[AbilityId(AbilityId.warlock_shadow_word)]
	public class ShadowWord : RangedAbility, IDebuff, IHealthRestore, IActiveAbility
	{
		// Token: 0x06000B55 RID: 2901 RVA: 0x0000A375 File Offset: 0x00008575
		public ShadowWord(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0000A3A2 File Offset: 0x000085A2
		public bool InstantHealthRestore { get; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0000A3AA File Offset: 0x000085AA
		public string HealModifierName { get; } = "modifier_warlock_shadow_word";

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0000A3B2 File Offset: 0x000085B2
		public override bool PositionCast
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_warlock_6);
				return abilityById != null && abilityById.Level > 0u;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0000A3D8 File Offset: 0x000085D8
		public bool RestoresAlly { get; } = 1;

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0000A3E0 File Offset: 0x000085E0
		public bool RestoresOwner { get; } = 1;

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00007F9C File Offset: 0x0000619C
		public override bool UnitTargetCast
		{
			get
			{
				return !this.PositionCast;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0000A3E8 File Offset: 0x000085E8
		public string DebuffModifierName { get; } = "modifier_warlock_shadow_word";

		// Token: 0x06000B5D RID: 2909 RVA: 0x0000372C File Offset: 0x0000192C
		public int HealthRestoreValue(Unit9 unit)
		{
			return 0;
		}
	}
}
