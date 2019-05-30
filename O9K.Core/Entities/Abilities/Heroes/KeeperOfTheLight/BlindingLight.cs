using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.KeeperOfTheLight
{
	// Token: 0x02000220 RID: 544
	[AbilityId(AbilityId.keeper_of_the_light_blinding_light)]
	public class BlindingLight : CircleAbility, IDebuff, INuke, IActiveAbility
	{
		// Token: 0x06000A55 RID: 2645 RVA: 0x000094E5 File Offset: 0x000076E5
		public BlindingLight(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00009522 File Offset: 0x00007722
		public override DamageType DamageType { get; } = 2;

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0000952A File Offset: 0x0000772A
		public string DebuffModifierName { get; } = "modifier_keeper_of_the_light_blinding_light";
	}
}
