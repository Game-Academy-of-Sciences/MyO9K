using System;
using System.Collections.Generic;

namespace O9K.Core.Prediction.Collision
{
	// Token: 0x02000020 RID: 32
	public class CollisionResult
	{
		// Token: 0x0600009F RID: 159 RVA: 0x000027AF File Offset: 0x000009AF
		public CollisionResult(IReadOnlyCollection<CollisionObject> collisionObjects)
		{
			this.CollisionObjects = collisionObjects;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000027BE File Offset: 0x000009BE
		public bool Collides
		{
			get
			{
				return this.CollisionObjects.Count > 0;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000027CE File Offset: 0x000009CE
		public IReadOnlyCollection<CollisionObject> CollisionObjects { get; }
	}
}
