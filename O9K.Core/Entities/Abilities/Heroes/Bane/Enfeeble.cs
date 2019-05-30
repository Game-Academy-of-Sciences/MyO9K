using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Bane
{
	// Token: 0x020001B1 RID: 433
	[AbilityId(AbilityId.bane_enfeeble)]
	public class Enfeeble : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x060008CB RID: 2251 RVA: 0x00008023 File Offset: 0x00006223
		public Enfeeble(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00008037 File Offset: 0x00006237
		public string DebuffModifierName { get; } = "modifier_bane_enfeeble";
	}
}
