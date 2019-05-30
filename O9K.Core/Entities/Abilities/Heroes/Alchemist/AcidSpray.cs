using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Alchemist
{
	// Token: 0x020003D2 RID: 978
	[AbilityId(AbilityId.alchemist_acid_spray)]
	public class AcidSpray : CircleAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06001045 RID: 4165 RVA: 0x0000E551 File Offset: 0x0000C751
		public AcidSpray(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0000E576 File Offset: 0x0000C776
		public string DebuffModifierName { get; } = "modifier_alchemist_acid_spray";
	}
}
