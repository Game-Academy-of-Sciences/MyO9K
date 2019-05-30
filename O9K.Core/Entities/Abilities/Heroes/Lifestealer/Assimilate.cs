using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Lifestealer
{
	// Token: 0x0200034F RID: 847
	[AbilityId(AbilityId.life_stealer_assimilate)]
	public class Assimilate : RangedAbility, IShield, IActiveAbility
	{
		// Token: 0x06000E59 RID: 3673 RVA: 0x0000CA5A File Offset: 0x0000AC5A
		public Assimilate(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0000CA81 File Offset: 0x0000AC81
		public UnitState AppliesUnitState { get; } = 256L;

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x0000720E File Offset: 0x0000540E
		public override float CastRange
		{
			get
			{
				return base.CastRange + 100f;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x0000CA89 File Offset: 0x0000AC89
		public string ShieldModifierName { get; } = "modifier_life_stealer_assimilate";

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0000CA91 File Offset: 0x0000AC91
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0000CA99 File Offset: 0x0000AC99
		public bool ShieldsOwner { get; }
	}
}
