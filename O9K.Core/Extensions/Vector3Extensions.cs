using System;
using Ensage.SDK.Extensions;
using O9K.Core.Entities.Units;
using SharpDX;

namespace O9K.Core.Extensions
{
	// Token: 0x020000A5 RID: 165
	public static class Vector3Extensions
	{
		// Token: 0x060004B4 RID: 1204 RVA: 0x00004F65 File Offset: 0x00003165
		public static float AngleBetween(this Vector3 start, Vector3 center, Vector3 end)
		{
			return (center - start).AngleBetween(end - center);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00004F7A File Offset: 0x0000317A
		public static float AngleBetween(this Unit9 start, Unit9 center, Unit9 end)
		{
			return start.Position.AngleBetween(center.Position, end.Position);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001F690 File Offset: 0x0001D890
		public static Vector3 Extend2D(this Vector3 position, Vector3 to, float distance)
		{
			Vector2 vector = position.ToVector2();
			Vector2 vector2 = to.ToVector2();
			return (vector + distance * (vector2 - vector).Normalized()).ToVector3(0f);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00004F93 File Offset: 0x00003193
		public static Vector3 IncreaseX(this Vector3 vector, float x)
		{
			vector.X += x;
			return vector;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00004FA2 File Offset: 0x000031A2
		public static Vector3 IncreaseY(this Vector3 vector, float y)
		{
			vector.Y += y;
			return vector;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00004FB1 File Offset: 0x000031B1
		public static Vector3 IncreaseZ(this Vector3 vector, float z)
		{
			vector.Z += z;
			return vector;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00004FC0 File Offset: 0x000031C0
		public static Vector3 MultiplyX(this Vector3 vector, float x)
		{
			vector.X *= x;
			return vector;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00004FCF File Offset: 0x000031CF
		public static Vector3 MultiplyY(this Vector3 vector, float y)
		{
			vector.Y *= y;
			return vector;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00004FDE File Offset: 0x000031DE
		public static Vector3 MultiplyZ(this Vector3 vector, float z)
		{
			vector.Z *= z;
			return vector;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001F6D0 File Offset: 0x0001D8D0
		public static Vector3 PositionAfter(this Vector3[] path, float time, float speed, float delay = 0f)
		{
			float num = Math.Max(0f, time - delay) * speed;
			for (int i = 0; i <= path.Length - 2; i++)
			{
				Vector3 vector = path[i];
				Vector3 vector2 = path[i + 1];
				float num2 = vector2.Distance(vector);
				if (num2 > num)
				{
					return vector + num * (vector2 - vector).Normalized();
				}
				num -= num2;
			}
			return path[path.Length - 1];
		}
	}
}
