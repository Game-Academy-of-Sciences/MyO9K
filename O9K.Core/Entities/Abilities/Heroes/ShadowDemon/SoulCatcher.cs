using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.ShadowDemon
{
	// Token: 0x020001E6 RID: 486
	[AbilityId(AbilityId.shadow_demon_soul_catcher)]
	public class SoulCatcher : CircleAbility, IDebuff, IActiveAbility
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x00008A6C File Offset: 0x00006C6C
		public SoulCatcher(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00008A91 File Offset: 0x00006C91
		public string DebuffModifierName { get; } = "modifier_shadow_demon_soul_catcher";
	}
}
