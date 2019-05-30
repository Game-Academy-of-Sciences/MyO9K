using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Enigma
{
	// Token: 0x0200022F RID: 559
	[AbilityId(AbilityId.enigma_demonic_conversion)]
	public class DemonicConversion : RangedAbility
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x00006527 File Offset: 0x00004727
		public DemonicConversion(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00009768 File Offset: 0x00007968
		public override bool TargetsEnemy { get; }
	}
}
