using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000190 RID: 400
	[AbilityId(AbilityId.item_sphere)]
	public class LinkensSphere : RangedAbility, IShield, IActiveAbility
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x00007815 File Offset: 0x00005A15
		public LinkensSphere(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00007830 File Offset: 0x00005A30
		public UnitState AppliesUnitState { get; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00007838 File Offset: 0x00005A38
		public string ShieldModifierName { get; } = "modifier_item_sphere_target";

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00007840 File Offset: 0x00005A40
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00007848 File Offset: 0x00005A48
		public bool ShieldsOwner { get; }

		// Token: 0x06000818 RID: 2072 RVA: 0x00007850 File Offset: 0x00005A50
		public override void Dispose()
		{
			base.Dispose();
			base.Owner.LinkensSphere = null;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00007864 File Offset: 0x00005A64
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			owner.LinkensSphere = this;
		}
	}
}
