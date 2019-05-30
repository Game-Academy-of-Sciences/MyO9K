using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000187 RID: 391
	[AbilityId(AbilityId.item_enchanted_mango)]
	public class EnchantedMango : RangedAbility, IManaRestore, IActiveAbility
	{
		// Token: 0x060007D5 RID: 2005 RVA: 0x00007565 File Offset: 0x00005765
		public EnchantedMango(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00007583 File Offset: 0x00005783
		public bool RestoresAlly { get; } = 1;

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0000758B File Offset: 0x0000578B
		public bool RestoresOwner { get; } = 1;

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00007593 File Offset: 0x00005793
		public override bool TargetsEnemy { get; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0000759B File Offset: 0x0000579B
		public override bool UnitTargetCast { get; } = 1;
	}
}
