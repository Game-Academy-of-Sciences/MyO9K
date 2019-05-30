using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Brewmaster
{
	// Token: 0x020003B2 RID: 946
	[AbilityId(AbilityId.brewmaster_cinder_brew)]
	public class CinderBrew : CircleAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000FEF RID: 4079 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		public CinderBrew(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x0000E0D0 File Offset: 0x0000C2D0
		public override float ActivationDelay { get; } = 0.15f;

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
		public string DebuffModifierName { get; } = "modifier_brewmaster_cinder_brew";
	}
}
