using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Chen
{
	// Token: 0x02000257 RID: 599
	[AbilityId(AbilityId.chen_hand_of_god)]
	public class HandOfGod : AreaOfEffectAbility, IHealthRestore, IActiveAbility
	{
		// Token: 0x06000AED RID: 2797 RVA: 0x00024A54 File Offset: 0x00022C54
		public HandOfGod(Ability baseAbility) : base(baseAbility)
		{
			this.healthRestoreData = new SpecialData(baseAbility, "heal_amount");
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00009E21 File Offset: 0x00008021
		public bool InstantHealthRestore { get; } = 1;

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00009E29 File Offset: 0x00008029
		public string HealModifierName { get; } = string.Empty;

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00009E31 File Offset: 0x00008031
		public override float Radius { get; } = 9999999f;

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00009E39 File Offset: 0x00008039
		public bool RestoresAlly { get; } = 1;

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00009E41 File Offset: 0x00008041
		public bool RestoresOwner { get; } = 1;

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00009E49 File Offset: 0x00008049
		public int HealthRestoreValue(Unit9 unit)
		{
			return (int)this.healthRestoreData.GetValue(this.Level);
		}

		// Token: 0x04000589 RID: 1417
		private readonly SpecialData healthRestoreData;
	}
}
