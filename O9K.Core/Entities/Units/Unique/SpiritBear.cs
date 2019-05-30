using System;
using Ensage;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.Core.Entities.Units.Unique
{
	// Token: 0x020000C0 RID: 192
	[UnitName("npc_dota_lone_druid_bear1")]
	[UnitName("npc_dota_lone_druid_bear2")]
	[UnitName("npc_dota_lone_druid_bear3")]
	[UnitName("npc_dota_lone_druid_bear4")]
	public class SpiritBear : Unit9
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x00005DF1 File Offset: 0x00003FF1
		public SpiritBear(Unit baseUnit) : base(baseUnit)
		{
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00005F24 File Offset: 0x00004124
		public override Vector2 HealthBarSize
		{
			get
			{
				if (this.healthBarSize.IsZero)
				{
					this.healthBarSize = new Vector2(Hud.Info.ScreenSize.X / 19.2f, Hud.Info.ScreenSize.Y / 133.1f);
				}
				return this.healthBarSize;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00020CF0 File Offset: 0x0001EEF0
		protected override Vector2 HealthBarPositionCorrection
		{
			get
			{
				if (this.healthBarPositionCorrection.IsZero)
				{
					this.healthBarPositionCorrection = new Vector2(this.HealthBarSize.X / 2f, Hud.Info.ScreenSize.Y / 42f);
				}
				return this.healthBarPositionCorrection;
			}
		}

		// Token: 0x040002B5 RID: 693
		private Vector2 healthBarPositionCorrection;

		// Token: 0x040002B6 RID: 694
		private Vector2 healthBarSize;
	}
}
