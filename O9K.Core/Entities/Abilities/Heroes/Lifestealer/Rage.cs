using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Lifestealer
{
	// Token: 0x02000352 RID: 850
	[AbilityId(AbilityId.life_stealer_rage)]
	public class Rage : ActiveAbility, IBuff, IShield, IActiveAbility
	{
		// Token: 0x06000E63 RID: 3683 RVA: 0x0000CABD File Offset: 0x0000ACBD
		public Rage(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0000CAF6 File Offset: 0x0000ACF6
		public UnitState AppliesUnitState { get; } = 512L;

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0000CAFE File Offset: 0x0000ACFE
		public string BuffModifierName { get; } = "modifier_life_stealer_rage";

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0000CB06 File Offset: 0x0000AD06
		public bool BuffsAlly { get; }

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0000CB0E File Offset: 0x0000AD0E
		public bool BuffsOwner { get; } = 1;

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0000CB16 File Offset: 0x0000AD16
		public string ShieldModifierName { get; } = "modifier_life_stealer_rage";

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0000CB1E File Offset: 0x0000AD1E
		public bool ShieldsAlly { get; }

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0000CB26 File Offset: 0x0000AD26
		public bool ShieldsOwner { get; } = 1;
	}
}
