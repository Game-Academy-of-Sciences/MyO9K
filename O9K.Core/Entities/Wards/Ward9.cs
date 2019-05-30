using System;
using Ensage;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Wards
{
	// Token: 0x020000AE RID: 174
	[UnitName("npc_dota_observer_wards")]
	[UnitName("npc_dota_sentry_wards")]
	public class Ward9 : Unit9
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x00005203 File Offset: 0x00003403
		public Ward9(Unit baseUnit) : base(baseUnit)
		{
			if (base.Name == "npc_dota_observer_wards")
			{
				this.IsObserverWard = 1;
			}
			else
			{
				this.IsSentryWard = 1;
			}
			base.IsUnit = false;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00005235 File Offset: 0x00003435
		public bool IsSentryWard { get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000523D File Offset: 0x0000343D
		public bool IsObserverWard { get; }
	}
}
