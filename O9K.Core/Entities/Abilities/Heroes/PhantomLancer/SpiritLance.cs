using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.PhantomLancer
{
	// Token: 0x02000300 RID: 768
	[AbilityId(AbilityId.phantom_lancer_spirit_lance)]
	public class SpiritLance : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000D4E RID: 3406 RVA: 0x0000BD76 File Offset: 0x00009F76
		public SpiritLance(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "lance_damage");
			this.SpeedData = new SpecialData(baseAbility, "lance_speed");
		}
	}
}
