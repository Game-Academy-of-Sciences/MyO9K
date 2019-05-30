using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.EmberSpirit
{
	// Token: 0x02000383 RID: 899
	[AbilityId(AbilityId.ember_spirit_flame_guard)]
	public class FlameGuard : AreaOfEffectAbility, IShield, IHasDamageBlock, IActiveAbility
	{
		// Token: 0x06000F68 RID: 3944 RVA: 0x000282AC File Offset: 0x000264AC
		public FlameGuard(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.blockData = new SpecialData(baseAbility, "absorb_amount");
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0000D9BA File Offset: 0x0000BBBA
		public UnitState AppliesUnitState { get; }

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0000D9C2 File Offset: 0x0000BBC2
		public DamageType BlockDamageType { get; } = 2;

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0000D9CA File Offset: 0x0000BBCA
		public string BlockModifierName { get; } = "modifier_ember_spirit_flame_guard";

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0000D9D2 File Offset: 0x0000BBD2
		public bool IsDamageBlockPermanent { get; }

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0000D9DA File Offset: 0x0000BBDA
		public string ShieldModifierName { get; } = "modifier_ember_spirit_flame_guard";

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0000D9E2 File Offset: 0x0000BBE2
		public bool ShieldsAlly { get; }

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0000D9EA File Offset: 0x0000BBEA
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0000D9F2 File Offset: 0x0000BBF2
		public bool BlocksDamageAfterReduction { get; }

		// Token: 0x06000F71 RID: 3953 RVA: 0x0000D9FA File Offset: 0x0000BBFA
		public float BlockValue(Unit9 target)
		{
			return this.blockData.GetValue(this.Level);
		}

		// Token: 0x040007F8 RID: 2040
		private readonly SpecialData blockData;
	}
}
