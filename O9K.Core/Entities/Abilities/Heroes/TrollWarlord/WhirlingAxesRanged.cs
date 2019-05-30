using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.TrollWarlord
{
	// Token: 0x0200029A RID: 666
	[AbilityId(AbilityId.troll_warlord_whirling_axes_ranged)]
	public class WhirlingAxesRanged : ConeAbility, INuke, IActiveAbility
	{
		// Token: 0x06000BD0 RID: 3024 RVA: 0x00025284 File Offset: 0x00023484
		public WhirlingAxesRanged(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "axe_width");
			this.SpeedData = new SpecialData(baseAbility, "axe_speed");
			this.DamageData = new SpecialData(baseAbility, "axe_damage");
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0000AA56 File Offset: 0x00008C56
		public override bool UnitTargetCast { get; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0000AA5E File Offset: 0x00008C5E
		public override float EndRadius { get; } = 200f;
	}
}
