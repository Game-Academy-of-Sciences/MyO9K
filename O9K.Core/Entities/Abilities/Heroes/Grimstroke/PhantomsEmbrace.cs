using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Grimstroke
{
	// Token: 0x02000375 RID: 885
	[AbilityId(AbilityId.grimstroke_ink_creature)]
	public class PhantomsEmbrace : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000F3B RID: 3899 RVA: 0x0000D715 File Offset: 0x0000B915
		public PhantomsEmbrace(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0000D737 File Offset: 0x0000B937
		public UnitState AppliesUnitState { get; } = 8L;
	}
}
