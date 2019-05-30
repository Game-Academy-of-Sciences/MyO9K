using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Axe
{
	// Token: 0x020003CA RID: 970
	[AbilityId(AbilityId.axe_battle_hunger)]
	public class BattleHunger : RangedAbility, IDebuff, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06001025 RID: 4133 RVA: 0x0000E337 File Offset: 0x0000C537
		public BattleHunger(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "damage_reduction_scepter");
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0000E375 File Offset: 0x0000C575
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x0000E37D File Offset: 0x0000C57D
		public string AmplifierModifierName { get; } = "modifier_axe_battle_hunger";

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0000E385 File Offset: 0x0000C585
		public AmplifiesDamage AmplifiesDamage { get; } = 2;

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x0000E38D File Offset: 0x0000C58D
		public string DebuffModifierName { get; } = "modifier_axe_battle_hunger";

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0000E395 File Offset: 0x0000C595
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0000E39D File Offset: 0x0000C59D
		public bool IsAmplifierPermanent { get; }

		// Token: 0x0600102C RID: 4140 RVA: 0x0000E3A5 File Offset: 0x0000C5A5
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			if (!base.Owner.HasAghanimsScepter)
			{
				return 0f;
			}
			return this.amplifierData.GetValue(this.Level) / -100f;
		}

		// Token: 0x04000865 RID: 2149
		private readonly SpecialData amplifierData;
	}
}
