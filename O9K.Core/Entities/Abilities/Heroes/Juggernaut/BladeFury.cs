using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Juggernaut
{
	// Token: 0x0200035A RID: 858
	[AbilityId(AbilityId.juggernaut_blade_fury)]
	public class BladeFury : AreaOfEffectAbility, IShield, IActiveAbility
	{
		// Token: 0x06000E88 RID: 3720 RVA: 0x0002762C File Offset: 0x0002582C
		public BladeFury(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "blade_fury_radius");
			this.DamageData = new SpecialData(baseAbility, "blade_fury_damage");
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0000CCA7 File Offset: 0x0000AEA7
		public UnitState AppliesUnitState { get; } = 512L;

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x0000CCAF File Offset: 0x0000AEAF
		public string ShieldModifierName { get; } = "modifier_juggernaut_blade_fury";

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0000CCB7 File Offset: 0x0000AEB7
		public bool ShieldsAlly { get; }

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0000CCBF File Offset: 0x0000AEBF
		public bool ShieldsOwner { get; } = 1;
	}
}
