using System;
using SharpDX;

namespace O9K.Core.Extensions
{
	// Token: 0x020000A4 RID: 164
	public static class Vector2Extensions
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x00004F29 File Offset: 0x00003129
		public static Vector2 IncreaseX(this Vector2 vector, float x)
		{
			vector.X += x;
			return vector;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00004F38 File Offset: 0x00003138
		public static Vector2 IncreaseY(this Vector2 vector, float y)
		{
			vector.Y += y;
			return vector;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00004F47 File Offset: 0x00003147
		public static Vector2 MultiplyX(this Vector2 vector, float x)
		{
			vector.X *= x;
			return vector;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00004F56 File Offset: 0x00003156
		public static Vector2 MultiplyY(this Vector2 vector, float y)
		{
			vector.Y *= y;
			return vector;
		}
	}
}
