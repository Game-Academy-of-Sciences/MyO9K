using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000195 RID: 405
	[AbilityId(AbilityId.item_mekansm)]
	public class Mekansm : AreaOfEffectAbility, IHealthRestore, IActiveAbility
	{
		// Token: 0x06000831 RID: 2097 RVA: 0x0002210C File Offset: 0x0002030C
		public Mekansm(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "heal_radius");
			this.healthRestoreData = new SpecialData(baseAbility, "heal_amount");
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x000079B4 File Offset: 0x00005BB4
		public bool InstantHealthRestore { get; } = 1;

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x000079BC File Offset: 0x00005BBC
		public string HealModifierName { get; } = "modifier_item_mekansm_noheal";

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x000079C4 File Offset: 0x00005BC4
		public bool RestoresAlly { get; } = 1;

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x000079CC File Offset: 0x00005BCC
		public bool RestoresOwner { get; } = 1;

		// Token: 0x06000836 RID: 2102 RVA: 0x000079D4 File Offset: 0x00005BD4
		public int HealthRestoreValue(Unit9 unit)
		{
			return (int)this.healthRestoreData.GetValue(this.Level);
		}

		// Token: 0x040003E7 RID: 999
		private readonly SpecialData healthRestoreData;
	}
}
