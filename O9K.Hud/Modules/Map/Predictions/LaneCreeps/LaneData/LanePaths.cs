using System;
using System.Collections.Generic;
using Ensage;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;
using SharpDX;

namespace O9K.Hud.Modules.Map.Predictions.LaneCreeps.LaneData
{
	// Token: 0x0200005E RID: 94
	internal class LanePaths
	{
		// Token: 0x0600021A RID: 538 RVA: 0x000103D4 File Offset: 0x0000E5D4
		public LanePaths()
		{
			if (EntityManager9.Owner.Team == Team.Radiant)
			{
				this.Lanes[LanePosition.Easy] = this.direTopPath;
				this.Lanes[LanePosition.Middle] = this.direMidPath;
				this.Lanes[LanePosition.Hard] = this.direBotPath;
				return;
			}
			this.Lanes[LanePosition.Easy] = this.radiantBotPath;
			this.Lanes[LanePosition.Middle] = this.radiantMidPath;
			this.Lanes[LanePosition.Hard] = this.radiantTopPath;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000035B3 File Offset: 0x000017B3
		public Dictionary<LanePosition, Vector3[]> Lanes { get; } = new Dictionary<LanePosition, Vector3[]>();

		// Token: 0x0600021C RID: 540 RVA: 0x00010E40 File Offset: 0x0000F040
		public LanePosition GetCreepLane(Unit9 unit)
		{
			foreach (KeyValuePair<LanePosition, Vector3[]> keyValuePair in this.Lanes)
			{
				if (unit.Distance(keyValuePair.Value[0]) < 300f)
				{
					return keyValuePair.Key;
				}
			}
			return LanePosition.Unknown;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000035BB File Offset: 0x000017BB
		public Vector3[] GetLanePath(LanePosition lane)
		{
			return this.Lanes[lane];
		}

		// Token: 0x0400017E RID: 382
		private readonly Vector3[] direBotPath = new Vector3[]
		{
			new Vector3(6239f, 3717f, 384f),
			new Vector3(6166f, 3097f, 384f),
			new Vector3(6407f, 635f, 384f),
			new Vector3(6422f, -816f, 384f),
			new Vector3(6477f, -2134f, 384f),
			new Vector3(5995f, -3054f, 384f),
			new Vector3(6071f, -3601f, 384f),
			new Vector3(6127f, -4822f, 384f),
			new Vector3(5460f, -5893f, 384f),
			new Vector3(5070f, -5915f, 384f),
			new Vector3(4412f, -6048f, 384f),
			new Vector3(3388f, -6125f, 384f),
			new Vector3(2641f, -6135f, 384f),
			new Vector3(1320f, -6374f, 384f),
			new Vector3(-174f, -6369f, 384f),
			new Vector3(-1201f, -6308f, 384f),
			new Vector3(-3036f, -6072f, 257f)
		};

		// Token: 0x0400017F RID: 383
		private readonly Vector3[] direMidPath = new Vector3[]
		{
			new Vector3(4101f, 3652f, 384f),
			new Vector3(3549f, 3041f, 256f),
			new Vector3(2639f, 2061f, 256f),
			new Vector3(2091f, 1605f, 256f),
			new Vector3(1091f, 845f, 256f),
			new Vector3(-163f, -62f, 256f),
			new Vector3(-789f, -579f, 178f),
			new Vector3(-1327f, -1017f, 256f),
			new Vector3(-2211f, -1799f, 256f),
			new Vector3(-2988f, -2522f, 256f),
			new Vector3(-4103f, -3532f, 256f)
		};

		// Token: 0x04000180 RID: 384
		private readonly Vector3[] direTopPath = new Vector3[]
		{
			new Vector3(3154f, 5811f, 384f),
			new Vector3(2903f, 5818f, 299f),
			new Vector3(-248f, 5852f, 384f),
			new Vector3(-1585f, 5979f, 384f),
			new Vector3(-3253f, 5981f, 384f),
			new Vector3(-3587f, 5954f, 384f),
			new Vector3(-3954f, 5860f, 384f),
			new Vector3(-4988f, 5618f, 384f),
			new Vector3(-5714f, 5514f, 384f),
			new Vector3(-6051f, 5092f, 384f),
			new Vector3(-6299f, 4171f, 384f),
			new Vector3(-6342f, 3806f, 384f),
			new Vector3(-6419f, 2888f, 384f),
			new Vector3(-6434f, -2797f, 256f)
		};

		// Token: 0x04000181 RID: 385
		private readonly Vector3[] radiantBotPath = new Vector3[]
		{
			new Vector3(-3628f, -6121f, 384f),
			new Vector3(-3138f, -6168f, 256f),
			new Vector3(-2126f, -6234f, 256f),
			new Vector3(-1064f, -6383f, 384f),
			new Vector3(196f, -6602f, 384f),
			new Vector3(1771f, -6374f, 384f),
			new Vector3(3740f, -6281f, 384f),
			new Vector3(5328f, -5813f, 384f),
			new Vector3(5766f, -5602f, 384f),
			new Vector3(6265f, -4992f, 384f),
			new Vector3(6112f, -3603f, 384f),
			new Vector3(6103f, 1724f, 256f),
			new Vector3(6133f, 2167f, 256f)
		};

		// Token: 0x04000182 RID: 386
		private readonly Vector3[] radiantMidPath = new Vector3[]
		{
			new Vector3(-5000f, -4458f, 384f),
			new Vector3(-4775f, -4020f, 384f),
			new Vector3(-4242f, -3594f, 256f),
			new Vector3(-3294f, -2941f, 256f),
			new Vector3(-2771f, -2454f, 256f),
			new Vector3(-2219f, -1873f, 256f),
			new Vector3(-1193f, -1006f, 256f),
			new Vector3(-573f, -449f, 128f),
			new Vector3(-46f, -49f, 256f),
			new Vector3(679f, 461f, 256f),
			new Vector3(979f, 692f, 256f),
			new Vector3(2167f, 1703f, 256f),
			new Vector3(2919f, 2467f, 256f),
			new Vector3(3785f, 3132f, 256f)
		};

		// Token: 0x04000183 RID: 387
		private readonly Vector3[] radiantTopPath = new Vector3[]
		{
			new Vector3(-6604f, -3979f, 384f),
			new Vector3(-6613f, -3679f, 384f),
			new Vector3(-6775f, -3420f, 384f),
			new Vector3(-6743f, -3042f, 369f),
			new Vector3(-6682f, -2124f, 256f),
			new Vector3(-6640f, -1758f, 384f),
			new Vector3(-6411f, -226f, 384f),
			new Vector3(-6370f, 2523f, 384f),
			new Vector3(-6198f, 3997f, 384f),
			new Vector3(-6015f, 4932f, 384f),
			new Vector3(-5888f, 5204f, 384f),
			new Vector3(-5389f, 5548f, 384f),
			new Vector3(-4757f, 5740f, 384f),
			new Vector3(-4066f, 5873f, 384f),
			new Vector3(-3068f, 6009f, 384f),
			new Vector3(-2139f, 6080f, 384f),
			new Vector3(-1327f, 6052f, 384f),
			new Vector3(-82f, 6011f, 384f),
			new Vector3(1682f, 5993f, 311f),
			new Vector3(2404f, 6051f, 256f)
		};
	}
}
