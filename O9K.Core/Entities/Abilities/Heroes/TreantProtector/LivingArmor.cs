using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.TreantProtector
{
	// Token: 0x020002A0 RID: 672
	[AbilityId(AbilityId.treant_living_armor)]
	public class LivingArmor : RangedAbility, IHealthRestore, IHasDamageBlock, IActiveAbility
	{
		// Token: 0x06000BE5 RID: 3045 RVA: 0x000252D8 File Offset: 0x000234D8
		public LivingArmor(Ability baseAbility) : base(baseAbility)
		{
			this.blockData = new SpecialData(baseAbility, "damage_block");
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0000AB4B File Offset: 0x00008D4B
		public bool BlocksDamageAfterReduction { get; } = 1;

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0000AB53 File Offset: 0x00008D53
		public bool InstantHealthRestore { get; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0000AB5B File Offset: 0x00008D5B
		public DamageType BlockDamageType { get; } = 7;

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0000AB63 File Offset: 0x00008D63
		public string BlockModifierName { get; } = "modifier_treant_living_armor";

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0000AB6B File Offset: 0x00008D6B
		public override float CastRange { get; } = 9999999f;

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0000AB73 File Offset: 0x00008D73
		public string HealModifierName { get; } = "modifier_treant_living_armor";

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0000AB7B File Offset: 0x00008D7B
		public bool IsDamageBlockPermanent { get; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0000AB83 File Offset: 0x00008D83
		public override bool PositionCast
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_treant_7);
				return abilityById != null && abilityById.Level > 0u;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0000ABA9 File Offset: 0x00008DA9
		public bool RestoresAlly { get; } = 1;

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0000ABB1 File Offset: 0x00008DB1
		public bool RestoresOwner { get; } = 1;

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00007F9C File Offset: 0x0000619C
		public override bool UnitTargetCast
		{
			get
			{
				return !this.PositionCast;
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0000ABB9 File Offset: 0x00008DB9
		public float BlockValue(Unit9 target)
		{
			return this.blockData.GetValue(this.Level);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0000372C File Offset: 0x0000192C
		public int HealthRestoreValue(Unit9 unit)
		{
			return 0;
		}

		// Token: 0x0400061B RID: 1563
		private readonly SpecialData blockData;
	}
}
