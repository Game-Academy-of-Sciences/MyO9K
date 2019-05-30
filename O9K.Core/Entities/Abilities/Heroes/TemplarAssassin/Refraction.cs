using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.TemplarAssassin
{
	// Token: 0x020002B3 RID: 691
	[AbilityId(AbilityId.templar_assassin_refraction)]
	public class Refraction : ActiveAbility, IBuff, IShield, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06000C34 RID: 3124 RVA: 0x000256F4 File Offset: 0x000238F4
		public Refraction(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0000AFE1 File Offset: 0x000091E1
		public UnitState AppliesUnitState { get; } = 256L;

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0000AFE9 File Offset: 0x000091E9
		public override bool TargetsEnemy { get; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0000AFF1 File Offset: 0x000091F1
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0000AFF9 File Offset: 0x000091F9
		public string AmplifierModifierName { get; } = "modifier_templar_assassin_refraction_absorb";

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0000B001 File Offset: 0x00009201
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0000B009 File Offset: 0x00009209
		public string BuffModifierName { get; } = "modifier_templar_assassin_refraction_damage";

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0000B011 File Offset: 0x00009211
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x0000B019 File Offset: 0x00009219
		public bool BuffsAlly { get; set; }

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0000B022 File Offset: 0x00009222
		// (set) Token: 0x06000C3E RID: 3134 RVA: 0x0000B02A File Offset: 0x0000922A
		public bool BuffsOwner { get; set; } = true;

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0000B033 File Offset: 0x00009233
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0000B03B File Offset: 0x0000923B
		public bool IsAmplifierPermanent { get; }

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0000B043 File Offset: 0x00009243
		public string ShieldModifierName { get; } = "modifier_templar_assassin_refraction_absorb";

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0000B04B File Offset: 0x0000924B
		public bool ShieldsAlly { get; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0000B053 File Offset: 0x00009253
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x0000B05B File Offset: 0x0000925B
		public bool ShieldsOwner { get; set; } = true;

		// Token: 0x06000C45 RID: 3141 RVA: 0x00006D18 File Offset: 0x00004F18
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return -1f;
		}
	}
}
