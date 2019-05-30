using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000198 RID: 408
	[AbilityId(AbilityId.item_orchid)]
	public class OrchidMalevolence : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x00007A55 File Offset: 0x00005C55
		public OrchidMalevolence(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "silence_damage_percent");
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00007A90 File Offset: 0x00005C90
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x00007A98 File Offset: 0x00005C98
		public string AmplifierModifierName { get; } = "modifier_orchid_malevolence_debuff";

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x00007AA0 File Offset: 0x00005CA0
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00007AA8 File Offset: 0x00005CA8
		public UnitState AppliesUnitState { get; } = 8L;

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x000070E1 File Offset: 0x000052E1
		public override DamageType DamageType
		{
			get
			{
				return DamageType.Magical;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00007AB0 File Offset: 0x00005CB0
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00007AB8 File Offset: 0x00005CB8
		public bool IsAmplifierPermanent { get; }

		// Token: 0x06000846 RID: 2118 RVA: 0x00007AC0 File Offset: 0x00005CC0
		public float AmplifierValue(Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / 100f;
		}

		// Token: 0x040003F1 RID: 1009
		private readonly SpecialData amplifierData;
	}
}
