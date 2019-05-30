using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Necrophos
{
	// Token: 0x0200031C RID: 796
	[AbilityId(AbilityId.necrolyte_death_pulse)]
	public class DeathPulse : AreaOfEffectAbility, IHealthRestore, INuke, IActiveAbility
	{
		// Token: 0x06000DC0 RID: 3520 RVA: 0x00026EEC File Offset: 0x000250EC
		public DeathPulse(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "area_of_effect");
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
			this.healthRestoreData = new SpecialData(baseAbility, "heal");
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0000C2A3 File Offset: 0x0000A4A3
		public bool InstantHealthRestore { get; } = 1;

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0000C2AB File Offset: 0x0000A4AB
		public string HealModifierName { get; } = string.Empty;

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x0000C2B3 File Offset: 0x0000A4B3
		public bool RestoresAlly { get; } = 1;

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0000C2BB File Offset: 0x0000A4BB
		public bool RestoresOwner { get; } = 1;

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0000C2C3 File Offset: 0x0000A4C3
		public int HealthRestoreValue(Unit9 unit)
		{
			return (int)this.healthRestoreData.GetValue(this.Level);
		}

		// Token: 0x04000723 RID: 1827
		private readonly SpecialData healthRestoreData;
	}
}
