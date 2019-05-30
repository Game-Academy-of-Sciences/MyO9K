using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.Rubick
{
	// Token: 0x020002E0 RID: 736
	[AbilityId(AbilityId.special_bonus_unique_rubick_5)]
	public class StolenSpellTalentAmplifier : Talent
	{
		// Token: 0x06000CD3 RID: 3283 RVA: 0x00025DBC File Offset: 0x00023FBC
		public StolenSpellTalentAmplifier(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierValue = base.BaseAbility.AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "value").Value / 100f;
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0000B884 File Offset: 0x00009A84
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0000B88C File Offset: 0x00009A8C
		public string AmplifierModifierName { get; } = string.Empty;

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0000B894 File Offset: 0x00009A94
		public AmplifiesDamage AmplifiesDamage { get; } = 2;

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0000B89C File Offset: 0x00009A9C
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0000B8A4 File Offset: 0x00009AA4
		public bool IsAmplifierPermanent { get; } = 1;

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0000B8AC File Offset: 0x00009AAC
		public float AmplifierValue(Unit9 target)
		{
			if (this.Level == 0u)
			{
				return 0f;
			}
			return this.amplifierValue;
		}

		// Token: 0x040006A0 RID: 1696
		private readonly float amplifierValue;
	}
}
