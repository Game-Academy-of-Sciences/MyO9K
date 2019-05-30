using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Clockwerk
{
	// Token: 0x0200024F RID: 591
	[AbilityId(AbilityId.rattletrap_power_cogs)]
	public class PowerCogs : AreaOfEffectAbility
	{
		// Token: 0x06000AD0 RID: 2768 RVA: 0x00009C17 File Offset: 0x00007E17
		public PowerCogs(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "cogs_radius");
			this.triggerRadiusData = new SpecialData(baseAbility, "trigger_distance");
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00009C4D File Offset: 0x00007E4D
		public override float ActivationDelay { get; } = 0.1f;

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00009C55 File Offset: 0x00007E55
		public override float Radius
		{
			get
			{
				return this.RadiusData.GetValue(this.Level) + this.triggerRadiusData.GetValue(this.Level) - 25f;
			}
		}

		// Token: 0x04000577 RID: 1399
		private readonly SpecialData triggerRadiusData;
	}
}
