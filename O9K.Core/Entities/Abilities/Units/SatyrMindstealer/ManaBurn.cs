using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Units.SatyrMindstealer
{
	// Token: 0x020000E0 RID: 224
	[AbilityId(AbilityId.satyr_soulstealer_mana_burn)]
	public class ManaBurn : RangedAbility, IHarass, IActiveAbility
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x00006527 File Offset: 0x00004727
		public ManaBurn(Ability baseAbility) : base(baseAbility)
		{
		}
	}
}
