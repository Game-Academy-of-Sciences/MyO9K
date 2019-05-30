using System;
using Ensage;

namespace O9K.Core.Extensions
{
	// Token: 0x020000A2 RID: 162
	public static class ProjectileExtensions
	{
		// Token: 0x060004AE RID: 1198 RVA: 0x00004F18 File Offset: 0x00003118
		public static bool IsAutoAttackProjectile(this TrackingProjectile projectile)
		{
			return (projectile.Flags & 256u) > 0u;
		}
	}
}
