using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Razor
{
	// Token: 0x020002ED RID: 749
	[AbilityId(AbilityId.razor_static_link)]
	public class StaticLink : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000D06 RID: 3334 RVA: 0x0000BA6A File Offset: 0x00009C6A
		public StaticLink(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0000BA7E File Offset: 0x00009C7E
		public string DebuffModifierName { get; } = string.Empty;
	}
}
