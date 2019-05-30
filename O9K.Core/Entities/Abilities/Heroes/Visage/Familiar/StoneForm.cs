using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Visage.Familiar
{
	// Token: 0x0200027A RID: 634
	[AbilityId(AbilityId.visage_summon_familiars_stone_form)]
	public class StoneForm : AreaOfEffectAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000B72 RID: 2930 RVA: 0x00024FAC File Offset: 0x000231AC
		public StoneForm(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "stun_radius");
			this.ActivationDelayData = new SpecialData(baseAbility, "stun_delay");
			this.DamageData = new SpecialData(baseAbility, "stun_damage");
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0000A507 File Offset: 0x00008707
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
