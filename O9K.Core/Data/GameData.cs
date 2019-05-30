using System;
using Ensage;

namespace O9K.Core.Data
{
	// Token: 0x0200009E RID: 158
	public static class GameData
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0001F574 File Offset: 0x0001D774
		public static string Time
		{
			get
			{
				return TimeSpan.FromSeconds((double)Game.GameTime).ToString("mm\\:ss");
			}
		}

		// Token: 0x04000223 RID: 547
		public const int AegisExpirationTime = 300;

		// Token: 0x04000224 RID: 548
		public const int BountyRuneRespawnTime = 300;

		// Token: 0x04000225 RID: 549
		public const int BuybackCooldown = 480;

		// Token: 0x04000226 RID: 550
		public const int CreepSpeed = 325;

		// Token: 0x04000227 RID: 551
		public const int RoshanMaxRespawnTime = 660;

		// Token: 0x04000228 RID: 552
		public const int RoshanMinRespawnTime = 480;

		// Token: 0x04000229 RID: 553
		public const int RuneRespawnTime = 120;

		// Token: 0x0400022A RID: 554
		public const int ScanActiveTime = 8;

		// Token: 0x0400022B RID: 555
		public const int ScanRadius = 900;

		// Token: 0x0400022C RID: 556
		internal const float DamageAmplifyPerIntelligence = 0.0007f;

		// Token: 0x0400022D RID: 557
		internal const int HealthGainPerStrength = 20;

		// Token: 0x0400022E RID: 558
		internal const int HurricanePikeBonusAttackSpeed = 100;

		// Token: 0x0400022F RID: 559
		internal const int MaxAttackSpeed = 700;

		// Token: 0x04000230 RID: 560
		internal const int MaxMovementSpeed = 550;

		// Token: 0x04000231 RID: 561
		internal const int MinAttackSpeed = 20;
	}
}
