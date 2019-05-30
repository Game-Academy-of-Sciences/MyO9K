using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.KeeperOfTheLight
{
	// Token: 0x02000221 RID: 545
	[AbilityId(AbilityId.keeper_of_the_light_chakra_magic)]
	public class ChakraMagic : RangedAbility, IManaRestore, IActiveAbility
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x00009532 File Offset: 0x00007732
		public ChakraMagic(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00009549 File Offset: 0x00007749
		public bool RestoresAlly { get; } = 1;

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00009551 File Offset: 0x00007751
		public bool RestoresOwner { get; } = 1;
	}
}
