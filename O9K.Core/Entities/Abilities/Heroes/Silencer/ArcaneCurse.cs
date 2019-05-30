using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Silencer
{
	// Token: 0x020001DB RID: 475
	[AbilityId(AbilityId.silencer_curse_of_the_silent)]
	public class ArcaneCurse : CircleAbility, IDebuff, IActiveAbility
	{
		// Token: 0x0600097D RID: 2429 RVA: 0x0000889A File Offset: 0x00006A9A
		public ArcaneCurse(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x000088BF File Offset: 0x00006ABF
		public string DebuffModifierName { get; } = "modifier_silencer_curse_of_the_silent";
	}
}
