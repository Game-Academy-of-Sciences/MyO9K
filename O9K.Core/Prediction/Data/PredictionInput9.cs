using System;
using System.Collections.Generic;
using O9K.Core.Entities.Units;
using O9K.Core.Prediction.Collision;

namespace O9K.Core.Prediction.Data
{
	// Token: 0x0200001A RID: 26
	public class PredictionInput9
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000259E File Offset: 0x0000079E
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000025A6 File Offset: 0x000007A6
		public bool AreaOfEffect { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000025AF File Offset: 0x000007AF
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000025B7 File Offset: 0x000007B7
		public IReadOnlyList<Unit9> AreaOfEffectTargets { get; set; } = new List<Unit9>();

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000025C0 File Offset: 0x000007C0
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000025C8 File Offset: 0x000007C8
		public Unit9 Caster { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000025D1 File Offset: 0x000007D1
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000025D9 File Offset: 0x000007D9
		public CollisionTypes CollisionTypes { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000025E2 File Offset: 0x000007E2
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000025EA File Offset: 0x000007EA
		public float Delay { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000025F3 File Offset: 0x000007F3
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000025FB File Offset: 0x000007FB
		public bool UseBlink { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002604 File Offset: 0x00000804
		// (set) Token: 0x06000079 RID: 121 RVA: 0x0000260C File Offset: 0x0000080C
		public float CastRange { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002615 File Offset: 0x00000815
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000261D File Offset: 0x0000081D
		public float Range { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002626 File Offset: 0x00000826
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000262E File Offset: 0x0000082E
		public bool RequiresToTurn { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002637 File Offset: 0x00000837
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000263F File Offset: 0x0000083F
		public SkillShotType SkillShotType { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002648 File Offset: 0x00000848
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002650 File Offset: 0x00000850
		public float Speed { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002659 File Offset: 0x00000859
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002661 File Offset: 0x00000861
		public Unit9 Target { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000266A File Offset: 0x0000086A
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002672 File Offset: 0x00000872
		public float Radius { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000267B File Offset: 0x0000087B
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00002683 File Offset: 0x00000883
		public float EndRadius { get; set; }
	}
}
