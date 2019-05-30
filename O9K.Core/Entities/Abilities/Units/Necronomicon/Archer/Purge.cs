using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Units.Necronomicon.Archer
{
	// Token: 0x020000E9 RID: 233
	[AbilityId(AbilityId.necronomicon_archer_purge)]
	public class Purge : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x000065BA File Offset: 0x000047BA
		public Purge(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x000065CE File Offset: 0x000047CE
		public string DebuffModifierName { get; } = "modifier_necronomicon_archer_purge";
	}
}
