using System;
using Ensage;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.Core.Entities.Units.Unique
{
	// Token: 0x020000BF RID: 191
	[UnitName("npc_dota_roshan")]
	public class Roshan : Unit9
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x00005DF1 File Offset: 0x00003FF1
		public Roshan(Unit baseUnit) : base(baseUnit)
		{
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00005EE4 File Offset: 0x000040E4
		public override Vector2 HealthBarSize
		{
			get
			{
				if (this.healthBarSize.IsZero)
				{
					this.healthBarSize = new Vector2(Hud.Info.ScreenSize.X / 8.7f, Hud.Info.ScreenSize.Y / 155.1f);
				}
				return this.healthBarSize;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00020CA4 File Offset: 0x0001EEA4
		protected override Vector2 HealthBarPositionCorrection
		{
			get
			{
				if (this.healthBarPositionCorrection.IsZero)
				{
					this.healthBarPositionCorrection = new Vector2(this.HealthBarSize.X / 2.02f, Hud.Info.ScreenSize.Y / 110f);
				}
				return this.healthBarPositionCorrection;
			}
		}

		// Token: 0x040002B3 RID: 691
		private Vector2 healthBarPositionCorrection;

		// Token: 0x040002B4 RID: 692
		private Vector2 healthBarSize;
	}
}
