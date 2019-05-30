using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Abaddon
{
	// Token: 0x020003D9 RID: 985
	[AbilityId(AbilityId.abaddon_aphotic_shield)]
	public class AphoticShield : RangedAbility, IShield, IHasDamageBlock, IActiveAbility
	{
		// Token: 0x06001062 RID: 4194 RVA: 0x00028D08 File Offset: 0x00026F08
		public AphoticShield(Ability baseAbility) : base(baseAbility)
		{
			this.blockData = new SpecialData(baseAbility, "damage_absorb");
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		public bool BlocksDamageAfterReduction { get; } = 1;

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0000E700 File Offset: 0x0000C900
		public UnitState AppliesUnitState { get; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0000E708 File Offset: 0x0000C908
		public DamageType BlockDamageType { get; } = 7;

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0000E710 File Offset: 0x0000C910
		public string BlockModifierName { get; } = "modifier_abaddon_aphotic_shield";

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0000E718 File Offset: 0x0000C918
		public bool IsDamageBlockPermanent { get; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x0000E720 File Offset: 0x0000C920
		public string ShieldModifierName { get; } = "modifier_abaddon_aphotic_shield";

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x0000E728 File Offset: 0x0000C928
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0000E730 File Offset: 0x0000C930
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x0600106B RID: 4203 RVA: 0x0000E738 File Offset: 0x0000C938
		public float BlockValue(Unit9 target)
		{
			return this.blockData.GetValue(this.Level);
		}

		// Token: 0x0400088B RID: 2187
		private readonly SpecialData blockData;
	}
}
