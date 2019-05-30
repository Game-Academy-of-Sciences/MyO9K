using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.ElderTitan
{
	// Token: 0x02000387 RID: 903
	[AbilityId(AbilityId.elder_titan_echo_stomp_spirit)]
	public class SpiritEchoStomp : AreaOfEffectAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000F77 RID: 3959 RVA: 0x00028308 File Offset: 0x00026508
		public SpiritEchoStomp(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.ActivationDelayData = new SpecialData(baseAbility, "cast_time");
			this.DamageData = new SpecialData(baseAbility, "stomp_damage");
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0000DA4A File Offset: 0x0000BC4A
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x06000F79 RID: 3961 RVA: 0x0000DA52 File Offset: 0x0000BC52
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.CanBeCasted(checkChanneling) && base.Owner.HasModifier("modifier_elder_titan_ancestral_spirit");
		}
	}
}
