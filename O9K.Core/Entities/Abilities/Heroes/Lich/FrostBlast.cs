using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Lich
{
	// Token: 0x02000212 RID: 530
	[AbilityId(AbilityId.lich_frost_nova)]
	public class FrostBlast : RangedAreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x00009288 File Offset: 0x00007488
		public FrostBlast(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
