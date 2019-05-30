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
	// Token: 0x0200018A RID: 394
	[AbilityId(AbilityId.item_faerie_fire)]
	public class FaerieFire : ActiveAbility, IHealthRestore, IActiveAbility
	{
		// Token: 0x060007EF RID: 2031 RVA: 0x0000763C File Offset: 0x0000583C
		public FaerieFire(Ability baseAbility) : base(baseAbility)
		{
			this.healthRestoreData = new SpecialData(baseAbility, "hp_restore");
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0000766F File Offset: 0x0000586F
		public bool InstantHealthRestore { get; } = 1;

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00007677 File Offset: 0x00005877
		public string HealModifierName { get; } = string.Empty;

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0000767F File Offset: 0x0000587F
		public bool RestoresAlly { get; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00007687 File Offset: 0x00005887
		public bool RestoresOwner { get; } = 1;

		// Token: 0x060007F4 RID: 2036 RVA: 0x0000768F File Offset: 0x0000588F
		public int HealthRestoreValue(Unit9 unit)
		{
			return (int)this.healthRestoreData.GetValue(this.Level);
		}

		// Token: 0x040003B5 RID: 949
		private readonly SpecialData healthRestoreData;
	}
}
