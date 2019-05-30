using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.SkywrathMage
{
	// Token: 0x020001D6 RID: 470
	[AbilityId(AbilityId.skywrath_mage_mystic_flare)]
	public class MysticFlare : CircleAbility, INuke, IActiveAbility
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x0000672B File Offset: 0x0000492B
		public MysticFlare(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00008795 File Offset: 0x00006995
		public override bool HasAreaOfEffect { get; }
	}
}
