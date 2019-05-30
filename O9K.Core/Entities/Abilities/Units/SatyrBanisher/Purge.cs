using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Units.SatyrBanisher
{
	// Token: 0x020000E1 RID: 225
	[AbilityId(AbilityId.satyr_trickster_purge)]
	public class Purge : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000669 RID: 1641 RVA: 0x00006539 File Offset: 0x00004739
		public Purge(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0000654D File Offset: 0x0000474D
		public string DebuffModifierName { get; } = "modifier_satyr_trickster_purge";
	}
}
