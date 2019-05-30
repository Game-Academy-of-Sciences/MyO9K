using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Talents
{
	// Token: 0x02000105 RID: 261
	[AbilityId(AbilityId.special_bonus_spell_amplify_3)]
	[AbilityId(AbilityId.special_bonus_spell_amplify_4)]
	[AbilityId(AbilityId.special_bonus_spell_amplify_5)]
	[AbilityId(AbilityId.special_bonus_spell_amplify_6)]
	[AbilityId(AbilityId.special_bonus_spell_amplify_8)]
	[AbilityId(AbilityId.special_bonus_spell_amplify_10)]
	[AbilityId(AbilityId.special_bonus_spell_amplify_12)]
	[AbilityId(AbilityId.special_bonus_spell_amplify_14)]
	[AbilityId(AbilityId.special_bonus_spell_amplify_15)]
	[AbilityId(AbilityId.special_bonus_spell_amplify_20)]
	[AbilityId(AbilityId.special_bonus_spell_amplify_25)]
	public class SpellAmplifyTalent : Talent, IHasDamageAmplify
	{
		// Token: 0x060006A5 RID: 1701 RVA: 0x0000684D File Offset: 0x00004A4D
		public SpellAmplifyTalent(Ability baseAbility) : base(baseAbility)
		{
			this.amplify = new SpecialData(baseAbility, "value");
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00006887 File Offset: 0x00004A87
		public DamageType AmplifierDamageType { get; } = 2;

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0000688F File Offset: 0x00004A8F
		public string AmplifierModifierName { get; } = "modifier_special_bonus_spell_amplify";

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00006897 File Offset: 0x00004A97
		public AmplifiesDamage AmplifiesDamage { get; } = 2;

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0000689F File Offset: 0x00004A9F
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x000068A7 File Offset: 0x00004AA7
		public bool IsAmplifierPermanent { get; } = 1;

		// Token: 0x060006AB RID: 1707 RVA: 0x000068AF File Offset: 0x00004AAF
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplify.GetValue(this.Level) / 100f;
		}

		// Token: 0x040002FE RID: 766
		private readonly SpecialData amplify;
	}
}
