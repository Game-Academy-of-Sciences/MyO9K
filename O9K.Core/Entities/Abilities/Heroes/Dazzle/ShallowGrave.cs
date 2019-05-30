using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.Dazzle
{
	// Token: 0x020001AE RID: 430
	[AbilityId(AbilityId.dazzle_shallow_grave)]
	public class ShallowGrave : RangedAbility, IShield, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x060008BC RID: 2236 RVA: 0x00022640 File Offset: 0x00020840
		public ShallowGrave(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00007F8A File Offset: 0x0000618A
		public override bool PositionCast
		{
			get
			{
				return base.Owner.HasAghanimsScepter;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00007F9C File Offset: 0x0000619C
		public override bool UnitTargetCast
		{
			get
			{
				return !this.PositionCast;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00007FA7 File Offset: 0x000061A7
		public UnitState AppliesUnitState { get; } = 256L;

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00007FAF File Offset: 0x000061AF
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00007FB7 File Offset: 0x000061B7
		public string AmplifierModifierName { get; } = "modifier_dazzle_shallow_grave";

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00007FBF File Offset: 0x000061BF
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00007FC7 File Offset: 0x000061C7
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00007FCF File Offset: 0x000061CF
		public bool IsAmplifierPermanent { get; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00007FD7 File Offset: 0x000061D7
		public string ShieldModifierName { get; } = "modifier_dazzle_shallow_grave";

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00007FDF File Offset: 0x000061DF
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00007FE7 File Offset: 0x000061E7
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x060008C8 RID: 2248 RVA: 0x00006D18 File Offset: 0x00004F18
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return -1f;
		}
	}
}
