using System;
using Ensage;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Buildings
{
	// Token: 0x020000D8 RID: 216
	public class Building9 : Unit9
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x000064A1 File Offset: 0x000046A1
		public Building9(Unit baseUnit) : base(baseUnit)
		{
			base.IsBuilding = true;
			base.IsUnit = false;
			base.IsVisible = true;
		}
	}
}
