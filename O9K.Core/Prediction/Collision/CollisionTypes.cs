using System;

namespace O9K.Core.Prediction.Collision
{
	// Token: 0x02000021 RID: 33
	[Flags]
	public enum CollisionTypes
	{
		// Token: 0x0400004D RID: 77
		None = 0,
		// Token: 0x0400004E RID: 78
		AllyCreeps = 2,
		// Token: 0x0400004F RID: 79
		EnemyCreeps = 4,
		// Token: 0x04000050 RID: 80
		AllyHeroes = 8,
		// Token: 0x04000051 RID: 81
		EnemyHeroes = 16,
		// Token: 0x04000052 RID: 82
		Runes = 32,
		// Token: 0x04000053 RID: 83
		Trees = 64,
		// Token: 0x04000054 RID: 84
		AllUnits = 30,
		// Token: 0x04000055 RID: 85
		AlliedUnits = 10,
		// Token: 0x04000056 RID: 86
		EnemyUnits = 20
	}
}
