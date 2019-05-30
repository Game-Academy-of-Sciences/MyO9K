using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.NyxAssassin
{
	// Token: 0x02000317 RID: 791
	[AbilityId(AbilityId.nyx_assassin_vendetta)]
	public class Vendetta : ActiveAbility
	{
		// Token: 0x06000DAD RID: 3501 RVA: 0x0000C189 File Offset: 0x0000A389
		public Vendetta(Ability baseAbility) : base(baseAbility)
		{
			base.IsInvisibility = true;
		}
	}
}
