using System;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Entity;
using SharpDX;

namespace O9K.Core.Entities.Units.Unique
{
	// Token: 0x020000BA RID: 186
	[UnitName("npc_dota_courier")]
	internal class Courier : Unit9
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x00005E3A File Offset: 0x0000403A
		public Courier(Unit baseUnit) : base(baseUnit)
		{
			base.IsCourier = true;
			base.IsUnit = false;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00005E51 File Offset: 0x00004051
		public override Vector3 Position
		{
			get
			{
				if (!base.IsVisible)
				{
					return this.GetPredictedPosition(0f);
				}
				return base.Position;
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00020B58 File Offset: 0x0001ED58
		public override Vector3 GetPredictedPosition(float delay = 0f)
		{
			if (base.IsMoving)
			{
				float networkRotationRad = base.BaseUnit.NetworkRotationRad;
				Vector3 vector;
				vector..ctor((float)Math.Cos((double)networkRotationRad), (float)Math.Sin((double)networkRotationRad), 0f);
				return base.CachedPosition + vector * (Game.RawGameTime - base.LastVisibleTime + delay) * base.Speed;
			}
			if (!base.IsVisible)
			{
				return base.CachedPosition.Extend(EntityManager9.EnemyFountain, (Game.RawGameTime - base.LastVisibleTime + delay) * base.Speed);
			}
			return base.CachedPosition;
		}
	}
}
