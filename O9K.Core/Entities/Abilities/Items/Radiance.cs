using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000144 RID: 324
	[AbilityId(AbilityId.item_radiance)]
	public class Radiance : ToggleAbility
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x00006AFF File Offset: 0x00004CFF
		public Radiance(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "aura_radius");
			this.DamageData = new SpecialData(baseAbility, "aura_damage");
		}
	}
}
