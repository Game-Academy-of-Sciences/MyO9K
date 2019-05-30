using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Io
{
	// Token: 0x02000226 RID: 550
	[AbilityId(AbilityId.wisp_overcharge)]
	public class Overcharge : ActiveAbility, IHasDamageAmplify
	{
		// Token: 0x06000A61 RID: 2657 RVA: 0x000095C0 File Offset: 0x000077C0
		public Overcharge(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "bonus_damage_pct");
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x000095F3 File Offset: 0x000077F3
		public override bool TargetsEnemy { get; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x000095FB File Offset: 0x000077FB
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00009603 File Offset: 0x00007803
		public string AmplifierModifierName { get; } = "modifier_wisp_overcharge";

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0000960B File Offset: 0x0000780B
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00009613 File Offset: 0x00007813
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0000961B File Offset: 0x0000781B
		public bool IsAmplifierPermanent { get; }

		// Token: 0x06000A68 RID: 2664 RVA: 0x00009623 File Offset: 0x00007823
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / 100f;
		}

		// Token: 0x04000540 RID: 1344
		private readonly SpecialData amplifierData;
	}
}
