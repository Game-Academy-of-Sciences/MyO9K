using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Viper
{
	// Token: 0x0200027C RID: 636
	[AbilityId(AbilityId.viper_poison_attack)]
	public class PoisonAttack : OrbAbility, IHarass, IActiveAbility
	{
		// Token: 0x06000B75 RID: 2933 RVA: 0x0000A50F File Offset: 0x0000870F
		public PoisonAttack(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0000A523 File Offset: 0x00008723
		public override string OrbModifier { get; } = "modifier_viper_poison_attack_slow";
	}
}
