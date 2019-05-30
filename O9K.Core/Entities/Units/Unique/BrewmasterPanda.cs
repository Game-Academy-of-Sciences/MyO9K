using System;
using Ensage;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.Core.Entities.Units.Unique
{
	// Token: 0x020000B9 RID: 185
	[UnitName("npc_dota_brewmaster_fire_1")]
	[UnitName("npc_dota_brewmaster_fire_2")]
	[UnitName("npc_dota_brewmaster_fire_3")]
	[UnitName("npc_dota_brewmaster_storm_1")]
	[UnitName("npc_dota_brewmaster_storm_2")]
	[UnitName("npc_dota_brewmaster_storm_3")]
	[UnitName("npc_dota_brewmaster_earth_1")]
	[UnitName("npc_dota_brewmaster_earth_2")]
	[UnitName("npc_dota_brewmaster_earth_3")]
	internal class BrewmasterPanda : Unit9
	{
		// Token: 0x060005DE RID: 1502 RVA: 0x00005DF1 File Offset: 0x00003FF1
		public BrewmasterPanda(Unit baseUnit) : base(baseUnit)
		{
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00005DFA File Offset: 0x00003FFA
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

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00020B0C File Offset: 0x0001ED0C
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

		// Token: 0x040002AC RID: 684
		private Vector2 healthBarPositionCorrection;

		// Token: 0x040002AD RID: 685
		private Vector2 healthBarSize;
	}
}
