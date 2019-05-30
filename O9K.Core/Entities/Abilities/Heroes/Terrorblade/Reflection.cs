using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Terrorblade
{
	// Token: 0x020001CF RID: 463
	[AbilityId(AbilityId.terrorblade_reflection)]
	public class Reflection : AreaOfEffectAbility, IDebuff, IActiveAbility
	{
		// Token: 0x0600094E RID: 2382 RVA: 0x00008630 File Offset: 0x00006830
		public Reflection(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "range");
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00008655 File Offset: 0x00006855
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x0000865D File Offset: 0x0000685D
		public string DebuffModifierName { get; set; } = "modifier_terrorblade_reflection_slow";
	}
}
