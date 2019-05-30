using System;
using System.Collections.Generic;
using System.Linq;
using O9K.Core.Entities.Units;
using SharpDX;

namespace O9K.Core.Extensions
{
	// Token: 0x020000A3 RID: 163
	public static class UnitExtensions
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x0001F640 File Offset: 0x0001D840
		public static Vector3 GetCenterPosition(this IEnumerable<Unit9> units)
		{
			Unit9[] array = units.ToArray<Unit9>();
			if (array.Length == 0)
			{
				return Vector3.Zero;
			}
			Vector3 vector = array[0].Position;
			for (int i = 1; i < array.Length; i++)
			{
				vector = (vector + array[i].Position) / 2f;
			}
			return vector;
		}
	}
}
