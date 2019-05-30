using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Hud.Modules.Map.Predictions.LaneCreeps.LaneData;
using SharpDX;

namespace O9K.Hud.Modules.Map.Predictions.LaneCreeps
{
	// Token: 0x02000057 RID: 87
	internal class CreepWave
	{
		// Token: 0x060001EE RID: 494 RVA: 0x0000F954 File Offset: 0x0000DB54
		public CreepWave(LanePosition lane, Vector3[] path)
		{
			this.Lane = lane;
			this.Path = path;
			this.pathLength = this.Path.Length;
			this.endPosition = path[this.pathLength - 1];
			this.predictedPosition = path[0];
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00003403 File Offset: 0x00001603
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000340B File Offset: 0x0000160B
		public bool IsSpawned { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00003414 File Offset: 0x00001614
		public LanePosition Lane { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000341C File Offset: 0x0000161C
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00003424 File Offset: 0x00001624
		public float SpawnTime { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000342D File Offset: 0x0000162D
		public bool IsVisible
		{
			get
			{
				return this.Creeps.Any((Unit9 x) => x.IsValid && x.IsVisible);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000F9B0 File Offset: 0x0000DBB0
		public bool IsValid
		{
			get
			{
				return this.Creeps.Any((Unit9 x) => x.IsValid && x.BaseUnit.IsAlive) && this.PredictedPosition.Distance2D(this.endPosition, false) > 300f;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00003459 File Offset: 0x00001659
		public Vector3[] Path { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00003461 File Offset: 0x00001661
		public bool WasVisible
		{
			get
			{
				return this.Creeps.Any((Unit9 x) => x.IsValid && x.BaseUnit.IsSpawned);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000FA04 File Offset: 0x0000DC04
		public Vector3[] RemainingPath
		{
			get
			{
				if (this.remainingPath != null)
				{
					return this.remainingPath;
				}
				int num = this.pathLength - this.lastPoint;
				this.remainingPath = new Vector3[num + 1];
				this.remainingPath[0] = this.lastVisiblePosition;
				Array.Copy(this.Path, this.lastPoint, this.remainingPath, 1, num);
				return this.remainingPath;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000FA70 File Offset: 0x0000DC70
		public Vector3 Position
		{
			get
			{
				List<Unit9> list = (from x in this.Creeps
				where x.IsValid && x.IsVisible
				select x).ToList<Unit9>();
				this.lastVisiblePosition = list.Aggregate(default(Vector3), (Vector3 position, Unit9 creep) => position + creep.Position) / (float)list.Count;
				this.LastVisibleTime = Game.RawGameTime;
				this.remainingPath = null;
				return this.lastVisiblePosition;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000348D File Offset: 0x0000168D
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00003495 File Offset: 0x00001695
		public float LastVisibleTime { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000349E File Offset: 0x0000169E
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000FB08 File Offset: 0x0000DD08
		public Vector3 PredictedPosition
		{
			get
			{
				return this.predictedPosition;
			}
			set
			{
				this.predictedPosition = value;
				if (this.predictedPosition.Distance2D(this.Path[this.lastPoint], false) < 500f)
				{
					this.lastPoint = Math.Min(this.lastPoint + 1, this.pathLength - 1);
				}
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000034A6 File Offset: 0x000016A6
		public void Spawn()
		{
			this.IsSpawned = true;
			this.SpawnTime = Game.RawGameTime + 0.4f;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		public void Update()
		{
			if (!this.WasVisible)
			{
				this.PredictedPosition = this.Path.PositionAfter(Game.RawGameTime - this.SpawnTime, 325f, 0f);
				return;
			}
			if (this.IsVisible)
			{
				this.PredictedPosition = this.Position;
				return;
			}
			if (this.LastVisibleTime <= 0f)
			{
				this.LastVisibleTime = Game.RawGameTime;
			}
			this.PredictedPosition = this.RemainingPath.PositionAfter(Game.RawGameTime - this.LastVisibleTime, 325f, 0f);
		}

		// Token: 0x0400015D RID: 349
		public List<Unit9> Creeps = new List<Unit9>();

		// Token: 0x0400015E RID: 350
		private readonly Vector3 endPosition;

		// Token: 0x0400015F RID: 351
		private readonly int pathLength;

		// Token: 0x04000160 RID: 352
		private int lastPoint;

		// Token: 0x04000161 RID: 353
		private Vector3 lastVisiblePosition;

		// Token: 0x04000162 RID: 354
		private Vector3 predictedPosition;

		// Token: 0x04000163 RID: 355
		private Vector3[] remainingPath;
	}
}
