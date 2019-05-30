using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Enchantress
{
	// Token: 0x0200037F RID: 895
	[AbilityId(AbilityId.enchantress_natures_attendants)]
	public class NaturesAttendants : AreaOfEffectAbility, IHealthRestore, IActiveAbility
	{
		// Token: 0x06000F56 RID: 3926 RVA: 0x0000D873 File Offset: 0x0000BA73
		public NaturesAttendants(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0000D8A6 File Offset: 0x0000BAA6
		public bool InstantHealthRestore { get; }

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0000D8AE File Offset: 0x0000BAAE
		public string HealModifierName { get; } = "modifier_enchantress_natures_attendants";

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0000D8B6 File Offset: 0x0000BAB6
		public bool RestoresAlly { get; } = 1;

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0000D8BE File Offset: 0x0000BABE
		public bool RestoresOwner { get; } = 1;

		// Token: 0x06000F5B RID: 3931 RVA: 0x0000372C File Offset: 0x0000192C
		public int HealthRestoreValue(Unit9 unit)
		{
			return 0;
		}
	}
}
