using System;
using Ensage;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using SharpDX;

namespace O9K.Core.Entities.Buildings
{
	// Token: 0x020000DA RID: 218
	public class Tower9 : Building9
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x000064C8 File Offset: 0x000046C8
		public Tower9(Tower baseUnit) : base(baseUnit)
		{
			this.BaseTower = baseUnit;
			base.IsTower = true;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x000064DF File Offset: 0x000046DF
		public override Vector2 HealthBarSize
		{
			get
			{
				if (this.healthBarSize.IsZero)
				{
					this.healthBarSize = new Vector2(Hud.Info.ScreenSize.X / 17.5f, Hud.Info.ScreenSize.Y / 155.1f);
				}
				return this.healthBarSize;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0000651F File Offset: 0x0000471F
		public Tower BaseTower { get; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x000217C4 File Offset: 0x0001F9C4
		public Unit9 TowerTarget
		{
			get
			{
				Unit attackTarget = this.BaseTower.AttackTarget;
				if (attackTarget != null && attackTarget.IsValid)
				{
					return EntityManager9.GetUnitFast(attackTarget.Handle);
				}
				return null;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x000217FC File Offset: 0x0001F9FC
		protected override Vector2 HealthBarPositionCorrection
		{
			get
			{
				if (this.healthBarPositionCorrection.IsZero)
				{
					this.healthBarPositionCorrection = new Vector2(this.HealthBarSize.X / 2f, Hud.Info.ScreenSize.Y / 16.8f);
				}
				return this.healthBarPositionCorrection;
			}
		}

		// Token: 0x040002E4 RID: 740
		private Vector2 healthBarPositionCorrection;

		// Token: 0x040002E5 RID: 741
		private Vector2 healthBarSize;
	}
}
