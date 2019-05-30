using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200018C RID: 396
	[AbilityId(AbilityId.item_glimmer_cape)]
	public class GlimmerCape : RangedAbility, IShield, IActiveAbility
	{
		// Token: 0x060007FA RID: 2042 RVA: 0x000076E6 File Offset: 0x000058E6
		public GlimmerCape(Ability baseAbility) : base(baseAbility)
		{
			base.IsInvisibility = true;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0000770F File Offset: 0x0000590F
		public UnitState AppliesUnitState { get; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00007717 File Offset: 0x00005917
		public string ShieldModifierName { get; } = "modifier_item_glimmer_cape_fade";

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0000771F File Offset: 0x0000591F
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00007727 File Offset: 0x00005927
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x060007FF RID: 2047 RVA: 0x0000772F File Offset: 0x0000592F
		public override float GetCastDelay(Unit9 unit)
		{
			return this.GetCastDelay();
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0000772F File Offset: 0x0000592F
		public override float GetCastDelay(Vector3 position)
		{
			return this.GetCastDelay();
		}
	}
}
