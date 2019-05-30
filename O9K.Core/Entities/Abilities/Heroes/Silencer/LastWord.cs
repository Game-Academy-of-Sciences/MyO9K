using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Silencer
{
	// Token: 0x020001DC RID: 476
	[AbilityId(AbilityId.silencer_last_word)]
	public class LastWord : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x0600097F RID: 2431 RVA: 0x000088C7 File Offset: 0x00006AC7
		public LastWord(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x000088DB File Offset: 0x00006ADB
		public string DebuffModifierName { get; } = "modifier_silencer_last_word";
	}
}
