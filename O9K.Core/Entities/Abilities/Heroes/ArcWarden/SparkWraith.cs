using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.ArcWarden
{
	// Token: 0x0200025E RID: 606
	[AbilityId(AbilityId.arc_warden_spark_wraith)]
	public class SparkWraith : CircleAbility, INuke, IActiveAbility
	{
		// Token: 0x06000B09 RID: 2825 RVA: 0x00009F84 File Offset: 0x00008184
		public SparkWraith(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "spark_damage");
			this.ActivationDelayData = new SpecialData(baseAbility, "activation_delay");
		}
	}
}
