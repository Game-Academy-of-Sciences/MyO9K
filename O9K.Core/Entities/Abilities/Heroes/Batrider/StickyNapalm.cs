using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Batrider
{
	// Token: 0x0200025C RID: 604
	[AbilityId(AbilityId.batrider_sticky_napalm)]
	public class StickyNapalm : CircleAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000B04 RID: 2820 RVA: 0x00009F3D File Offset: 0x0000813D
		public StickyNapalm(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00009F62 File Offset: 0x00008162
		public string DebuffModifierName { get; } = string.Empty;
	}
}
