using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.TrollWarlord
{
	// Token: 0x0200029B RID: 667
	[AbilityId(AbilityId.troll_warlord_battle_trance)]
	public class BattleTrance : ActiveAbility, IBuff, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06000BD3 RID: 3027 RVA: 0x0000AA66 File Offset: 0x00008C66
		public BattleTrance(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0000AA9A File Offset: 0x00008C9A
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0000AAA2 File Offset: 0x00008CA2
		public string AmplifierModifierName { get; } = "modifier_troll_warlord_battle_trance";

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0000AAAA File Offset: 0x00008CAA
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0000AAB2 File Offset: 0x00008CB2
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0000AABA File Offset: 0x00008CBA
		public bool IsAmplifierPermanent { get; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0000AAC2 File Offset: 0x00008CC2
		public string BuffModifierName { get; } = "modifier_troll_warlord_battle_trance";

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0000AACA File Offset: 0x00008CCA
		public bool BuffsAlly { get; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0000AAD2 File Offset: 0x00008CD2
		public bool BuffsOwner { get; } = 1;

		// Token: 0x06000BDC RID: 3036 RVA: 0x00006D18 File Offset: 0x00004F18
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return -1f;
		}
	}
}
