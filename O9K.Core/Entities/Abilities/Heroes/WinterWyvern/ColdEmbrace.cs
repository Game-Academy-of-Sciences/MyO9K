using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.WinterWyvern
{
	// Token: 0x020001C3 RID: 451
	[AbilityId(AbilityId.winter_wyvern_cold_embrace)]
	public class ColdEmbrace : RangedAbility, IShield, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x00022CC4 File Offset: 0x00020EC4
		public ColdEmbrace(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00008352 File Offset: 0x00006552
		public UnitState AppliesUnitState { get; } = 36L;

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0000835A File Offset: 0x0000655A
		public DamageType AmplifierDamageType { get; } = 1;

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00008362 File Offset: 0x00006562
		public string AmplifierModifierName { get; } = "modifier_winter_wyvern_cold_embrace";

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0000836A File Offset: 0x0000656A
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00008372 File Offset: 0x00006572
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0000837A File Offset: 0x0000657A
		public bool IsAmplifierPermanent { get; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00008382 File Offset: 0x00006582
		public string ShieldModifierName { get; } = "modifier_winter_wyvern_cold_embrace";

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x0000838A File Offset: 0x0000658A
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00008392 File Offset: 0x00006592
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x06000916 RID: 2326 RVA: 0x00006D18 File Offset: 0x00004F18
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return -1f;
		}
	}
}
