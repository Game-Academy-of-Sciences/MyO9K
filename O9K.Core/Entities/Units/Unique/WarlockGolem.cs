using System;
using Ensage;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.Core.Entities.Units.Unique
{
	// Token: 0x020000C1 RID: 193
	[UnitName("npc_dota_warlock_golem_1")]
	[UnitName("npc_dota_warlock_golem_2")]
	[UnitName("npc_dota_warlock_golem_3")]
	[UnitName("npc_dota_warlock_golem_scepter_1")]
	[UnitName("npc_dota_warlock_golem_scepter_2")]
	[UnitName("npc_dota_warlock_golem_scepter_3")]
	internal class WarlockGolem : Unit9
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x00005DF1 File Offset: 0x00003FF1
		public WarlockGolem(Unit baseUnit) : base(baseUnit)
		{
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00005F64 File Offset: 0x00004164
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

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00020D3C File Offset: 0x0001EF3C
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

		// Token: 0x040002B7 RID: 695
		private Vector2 healthBarPositionCorrection;

		// Token: 0x040002B8 RID: 696
		private Vector2 healthBarSize;
	}
}
