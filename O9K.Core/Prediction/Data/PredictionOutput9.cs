using System;
using System.Collections.Generic;
using O9K.Core.Entities.Units;
using SharpDX;

namespace O9K.Core.Prediction.Data
{
	// Token: 0x0200001B RID: 27
	public class PredictionOutput9
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000269F File Offset: 0x0000089F
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000026A7 File Offset: 0x000008A7
		public List<PredictionOutput9> AoeTargetsHit { get; set; } = new List<PredictionOutput9>();

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000026B0 File Offset: 0x000008B0
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000026B8 File Offset: 0x000008B8
		public Vector3 CastPosition { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000026C1 File Offset: 0x000008C1
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000026C9 File Offset: 0x000008C9
		public Vector3 BlinkLinePosition { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000026D2 File Offset: 0x000008D2
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000026DA File Offset: 0x000008DA
		public HitChance HitChance { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000026E3 File Offset: 0x000008E3
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000026EB File Offset: 0x000008EB
		public Unit9 Target { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000026F4 File Offset: 0x000008F4
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000026FC File Offset: 0x000008FC
		public Vector3 TargetPosition { get; set; }
	}
}
