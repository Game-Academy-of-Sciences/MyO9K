using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.SkywrathMage
{
	// Token: 0x020001D7 RID: 471
	[AbilityId(AbilityId.skywrath_mage_ancient_seal)]
	public class AncientSeal : RangedAbility, IDebuff, IDisable, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x000231A8 File Offset: 0x000213A8
		public AncientSeal(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "resist_debuff");
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0000879D File Offset: 0x0000699D
		public DamageType AmplifierDamageType { get; } = 2;

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x000087A5 File Offset: 0x000069A5
		public string AmplifierModifierName { get; } = "modifier_skywrath_mage_ancient_seal";

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x000087AD File Offset: 0x000069AD
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x000087B5 File Offset: 0x000069B5
		public UnitState AppliesUnitState { get; } = 8L;

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x000087BD File Offset: 0x000069BD
		public bool IsAmplifierAddedToStats { get; } = 1;

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x000087C5 File Offset: 0x000069C5
		public bool IsAmplifierPermanent { get; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x000087CD File Offset: 0x000069CD
		public string DebuffModifierName { get; } = "modifier_skywrath_mage_ancient_seal";

		// Token: 0x06000970 RID: 2416 RVA: 0x000087D5 File Offset: 0x000069D5
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / -100f;
		}

		// Token: 0x040004B4 RID: 1204
		private readonly SpecialData amplifierData;
	}
}
