using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Clockwerk
{
	// Token: 0x02000250 RID: 592
	[AbilityId(AbilityId.rattletrap_battery_assault)]
	public class BatteryAssault : AreaOfEffectAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000AD3 RID: 2771 RVA: 0x00009C80 File Offset: 0x00007E80
		public BatteryAssault(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00009CAC File Offset: 0x00007EAC
		public string BuffModifierName { get; } = "modifier_rattletrap_battery_assault";

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x00009CB4 File Offset: 0x00007EB4
		public bool BuffsAlly { get; }

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x00009CBC File Offset: 0x00007EBC
		public bool BuffsOwner { get; } = 1;
	}
}
