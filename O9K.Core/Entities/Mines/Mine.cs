using System;
using Ensage;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Mines
{
	// Token: 0x020000C2 RID: 194
	[UnitName("npc_dota_techies_land_mine")]
	[UnitName("npc_dota_techies_stasis_trap")]
	public class Mine : Unit9
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x00005FA4 File Offset: 0x000041A4
		public Mine(Unit baseUnit) : base(baseUnit)
		{
			base.IsUnit = false;
		}
	}
}
