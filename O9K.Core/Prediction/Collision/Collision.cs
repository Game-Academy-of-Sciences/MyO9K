using System;
using System.Collections.Generic;
using System.Linq;
using Ensage.SDK.Geometry;
using SharpDX;

namespace O9K.Core.Prediction.Collision
{
	// Token: 0x0200001D RID: 29
	public static class Collision
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00010B28 File Offset: 0x0000ED28
		public static CollisionResult GetCollision(Vector2 startPosition, Vector2 endPosition, float radius, IEnumerable<CollisionObject> collisionObjects)
		{
			List<CollisionObject> list = new List<CollisionObject>();
			foreach (CollisionObject collisionObject in collisionObjects)
			{
				if (collisionObject.Position.Distance2D(startPosition, endPosition, true, true) < (radius + collisionObject.Radius) * (radius + collisionObject.Radius))
				{
					list.Add(collisionObject);
				}
			}
			list = (from x in list
			orderby startPosition.Distance2D(x.Position, false)
			select x).ToList<CollisionObject>();
			return new CollisionResult(list);
		}
	}
}
