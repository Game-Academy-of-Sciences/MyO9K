using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Bloodseeker
{
	// Token: 0x020003C0 RID: 960
	[AbilityId(AbilityId.bloodseeker_bloodrage)]
	public class Bloodrage : RangedAbility, IBuff, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06001006 RID: 4102 RVA: 0x00028968 File Offset: 0x00026B68
		public Bloodrage(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "damage_increase_pct");
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x0000E1E7 File Offset: 0x0000C3E7
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0000E1EF File Offset: 0x0000C3EF
		public string AmplifierModifierName { get; } = "modifier_bloodseeker_bloodrage";

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0000E1F7 File Offset: 0x0000C3F7
		public AmplifiesDamage AmplifiesDamage { get; } = 3;

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x0000E1FF File Offset: 0x0000C3FF
		public string BuffModifierName { get; } = "modifier_bloodseeker_bloodrage";

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x0000E207 File Offset: 0x0000C407
		public bool BuffsAlly { get; } = 1;

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0000E20F File Offset: 0x0000C40F
		public bool BuffsOwner { get; } = 1;

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x0000E217 File Offset: 0x0000C417
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x0000E21F File Offset: 0x0000C41F
		public bool IsAmplifierPermanent { get; }

		// Token: 0x0600100F RID: 4111 RVA: 0x0000E227 File Offset: 0x0000C427
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / 100f;
		}

		// Token: 0x04000853 RID: 2131
		private readonly SpecialData amplifierData;
	}
}
