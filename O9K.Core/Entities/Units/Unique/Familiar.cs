using System;
using Ensage;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.Core.Entities.Units.Unique
{
	// Token: 0x020000BB RID: 187
	[UnitName("npc_dota_visage_familiar1")]
	[UnitName("npc_dota_visage_familiar2")]
	[UnitName("npc_dota_visage_familiar3")]
	public class Familiar : Unit9
	{
		// Token: 0x060005E4 RID: 1508 RVA: 0x00005DF1 File Offset: 0x00003FF1
		public Familiar(Unit baseUnit) : base(baseUnit)
		{
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00005E6D File Offset: 0x0000406D
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

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00020BF4 File Offset: 0x0001EDF4
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

		// Token: 0x040002AE RID: 686
		private Vector2 healthBarPositionCorrection;

		// Token: 0x040002AF RID: 687
		private Vector2 healthBarSize;
	}
}
