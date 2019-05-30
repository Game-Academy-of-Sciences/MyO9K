using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Enchantress
{
	// Token: 0x0200037D RID: 893
	[AbilityId(AbilityId.enchantress_enchant)]
	public class Enchant : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000F4C RID: 3916 RVA: 0x0000D83F File Offset: 0x0000BA3F
		public Enchant(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x0000D853 File Offset: 0x0000BA53
		public string DebuffModifierName { get; } = "modifier_enchantress_enchant_slow";
	}
}
