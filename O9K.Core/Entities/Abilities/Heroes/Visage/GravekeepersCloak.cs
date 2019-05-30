using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Visage
{
	// Token: 0x02000276 RID: 630
	[AbilityId(AbilityId.visage_gravekeepers_cloak)]
	public class GravekeepersCloak : PassiveAbility, IHasDamageAmplify
	{
		// Token: 0x06000B64 RID: 2916 RVA: 0x0000A441 File Offset: 0x00008641
		public GravekeepersCloak(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "damage_reduction");
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0000A47B File Offset: 0x0000867B
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0000A483 File Offset: 0x00008683
		public string AmplifierModifierName { get; } = "modifier_visage_gravekeepers_cloak";

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0000A48B File Offset: 0x0000868B
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0000A493 File Offset: 0x00008693
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0000A49B File Offset: 0x0000869B
		public bool IsAmplifierPermanent { get; } = 1;

		// Token: 0x06000B6A RID: 2922 RVA: 0x00024E90 File Offset: 0x00023090
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			if (this.Level == 0u)
			{
				return 0f;
			}
			Modifier modifier = target.BaseModifiers.FirstOrDefault((Modifier x) => x.Name == this.AmplifierModifierName);
			int num = (modifier != null) ? modifier.StackCount : 0;
			return this.amplifierData.GetValue(this.Level) / -100f * (float)num;
		}

		// Token: 0x040005D2 RID: 1490
		private readonly SpecialData amplifierData;
	}
}
