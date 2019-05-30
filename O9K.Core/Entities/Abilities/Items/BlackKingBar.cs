using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200017D RID: 381
	[AbilityId(AbilityId.item_black_king_bar)]
	public class BlackKingBar : ActiveAbility, IShield, IActiveAbility
	{
		// Token: 0x060007A4 RID: 1956 RVA: 0x00007287 File Offset: 0x00005487
		public BlackKingBar(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x000072AE File Offset: 0x000054AE
		public UnitState AppliesUnitState { get; } = 512L;

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x000072B6 File Offset: 0x000054B6
		public string ShieldModifierName { get; } = "modifier_black_king_bar_immune";

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x000072BE File Offset: 0x000054BE
		public bool ShieldsAlly { get; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x000072C6 File Offset: 0x000054C6
		public bool ShieldsOwner { get; } = 1;
	}
}
