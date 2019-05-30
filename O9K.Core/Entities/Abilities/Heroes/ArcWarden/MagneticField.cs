using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.ArcWarden
{
	// Token: 0x02000261 RID: 609
	[AbilityId(AbilityId.arc_warden_magnetic_field)]
	public class MagneticField : CircleAbility, IShield, IActiveAbility
	{
		// Token: 0x06000B10 RID: 2832 RVA: 0x0000A00F File Offset: 0x0000820F
		public MagneticField(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0000A04A File Offset: 0x0000824A
		public UnitState AppliesUnitState { get; } = 4L;

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0000A052 File Offset: 0x00008252
		public string ShieldModifierName { get; } = "modifier_arc_warden_magnetic_field_evasion";

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0000A05A File Offset: 0x0000825A
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0000A062 File Offset: 0x00008262
		public bool ShieldsOwner { get; } = 1;
	}
}
