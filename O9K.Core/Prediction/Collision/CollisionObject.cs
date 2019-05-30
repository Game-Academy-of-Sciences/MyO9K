using System;
using Ensage.SDK.Geometry;
using O9K.Core.Entities;
using O9K.Core.Entities.Units;
using SharpDX;

namespace O9K.Core.Prediction.Collision
{
	// Token: 0x0200001F RID: 31
	public class CollisionObject
	{
		// Token: 0x06000099 RID: 153 RVA: 0x0000272C File Offset: 0x0000092C
		public CollisionObject(Unit9 unit)
		{
			this.Entity = unit;
			this.Position = unit.Position.To2D();
			this.Radius = unit.HullRadius;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002758 File Offset: 0x00000958
		public CollisionObject(Entity9 entity, Vector2 position, float radius)
		{
			this.Entity = entity;
			this.Position = position;
			this.Radius = radius;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002775 File Offset: 0x00000975
		public CollisionObject(Entity9 entity, Vector3 position, float radius)
		{
			this.Entity = entity;
			this.Position = position.To2D();
			this.Radius = radius;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002797 File Offset: 0x00000997
		public Entity9 Entity { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000279F File Offset: 0x0000099F
		public Vector2 Position { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000027A7 File Offset: 0x000009A7
		public float Radius { get; }
	}
}
