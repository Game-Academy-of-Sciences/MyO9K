using System;
using Ensage;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.Core.Entities.Heroes
{
	// Token: 0x020000C9 RID: 201
	public class Hero9 : Unit9
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x00020E54 File Offset: 0x0001F054
		public Hero9(Hero baseHero) : base(baseHero)
		{
			this.BaseHero = baseHero;
			this.Id = baseHero.HeroId;
			base.IsHero = true;
			base.PrimaryAttribute = baseHero.PrimaryAttribute;
			base.IsIllusion = (baseHero.IsIllusion && !base.HasModifier("modifier_morphling_replicate"));
			base.CanUseAbilities = (!base.IsIllusion || base.HasModifier(new string[]
			{
				"modifier_arc_warden_tempest_double",
				"modifier_vengefulspirit_hybrid_special"
			}));
			base.IsImportant = (!baseHero.IsIllusion || base.HasModifier("modifier_morphling_replicate"));
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x000060A6 File Offset: 0x000042A6
		public override float HealthRegeneration
		{
			get
			{
				return base.BaseUnit.BaseHealthRegeneration;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x000060B3 File Offset: 0x000042B3
		public override float ManaRegeneration
		{
			get
			{
				return base.BaseUnit.BaseManaRegeneration;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00005E51 File Offset: 0x00004051
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

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00020EF8 File Offset: 0x0001F0F8
		public override Vector2 HealthBarSize
		{
			get
			{
				if (this.healthBarSize.IsZero)
				{
					this.healthBarSize = (base.IsMyHero ? new Vector2(Hud.Info.ScreenSize.X / 17.8f, Hud.Info.ScreenSize.Y / 95.1f) : new Vector2(Hud.Info.ScreenSize.X / 19.4f, Hud.Info.ScreenSize.Y / 95.1f));
				}
				return this.healthBarSize;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x000060C0 File Offset: 0x000042C0
		public Hero BaseHero { get; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x000060C8 File Offset: 0x000042C8
		public HeroId Id { get; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x000060D0 File Offset: 0x000042D0
		public override bool IsInvulnerable
		{
			get
			{
				return base.IsInvulnerable || this.BaseHero.IsReincarnating;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00020F74 File Offset: 0x0001F174
		public override float Mana
		{
			get
			{
				if (base.IsVisible)
				{
					return base.BaseUnit.Mana;
				}
				if (base.BaseUnit.IsAlive)
				{
					return Math.Min(base.BaseUnit.Mana + (Game.RawGameTime - base.LastVisibleTime) * this.ManaRegeneration, base.BaseUnit.MaximumMana);
				}
				if (Game.RawGameTime > this.BaseHero.RespawnTime)
				{
					return base.BaseUnit.MaximumMana;
				}
				return 0f;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00020FF8 File Offset: 0x0001F1F8
		public override float Health
		{
			get
			{
				if (base.IsVisible)
				{
					return base.BaseUnit.Health;
				}
				if (base.BaseUnit.IsAlive)
				{
					return Math.Min(base.BaseUnit.Health + (Game.RawGameTime - base.LastVisibleTime) * this.HealthRegeneration, base.BaseUnit.MaximumHealth);
				}
				if (Game.RawGameTime > this.BaseHero.RespawnTime)
				{
					return base.BaseUnit.MaximumHealth;
				}
				return 0f;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x000060E7 File Offset: 0x000042E7
		public override bool IsAlive
		{
			get
			{
				if (!base.IsVisible)
				{
					return Game.RawGameTime > this.BaseHero.RespawnTime;
				}
				return base.BaseUnit.IsAlive || this.BaseHero.IsReincarnating;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000611E File Offset: 0x0000431E
		public override float TotalAgility
		{
			get
			{
				return this.BaseHero.TotalAgility;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0000612B File Offset: 0x0000432B
		public override float Agility
		{
			get
			{
				return this.BaseHero.Agility;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00006138 File Offset: 0x00004338
		public override float TotalIntelligence
		{
			get
			{
				return this.BaseHero.TotalIntelligence;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00006145 File Offset: 0x00004345
		public override float Intelligence
		{
			get
			{
				return this.BaseHero.Intelligence;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00006152 File Offset: 0x00004352
		public override float Strength
		{
			get
			{
				return this.BaseHero.Strength;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0000615F File Offset: 0x0000435F
		public override float TotalStrength
		{
			get
			{
				return this.BaseHero.TotalStrength;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00021084 File Offset: 0x0001F284
		protected override Vector2 HealthBarPositionCorrection
		{
			get
			{
				if (this.healthBarPositionCorrection.IsZero)
				{
					this.healthBarPositionCorrection = (base.IsMyHero ? new Vector2(this.HealthBarSize.X / 1.91f, Hud.Info.ScreenSize.Y / 29f) : new Vector2(this.HealthBarSize.X / 1.91f, Hud.Info.ScreenSize.Y / 35f));
				}
				return this.healthBarPositionCorrection;
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00021100 File Offset: 0x0001F300
		public override float GetAngle(Vector3 position, bool rotationDifference = false)
		{
			float num = base.BaseUnit.NetworkRotationRad;
			if (rotationDifference)
			{
				num += MathUtil.DegreesToRadians(base.BaseUnit.RotationDifference);
			}
			double num2 = Math.Abs(Math.Atan2((double)(position.Y - this.Position.Y), (double)(position.X - this.Position.X)) - (double)num);
			if (num2 > 3.1415926535897931)
			{
				num2 = Math.Abs(6.2831853071795862 - num2);
			}
			return (float)num2;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0000616C File Offset: 0x0000436C
		public override float GetImmobilityDuration()
		{
			if (this.BaseHero.IsReincarnating)
			{
				return this.BaseHero.RespawnTime - Game.RawGameTime + 0.1f;
			}
			return base.GetImmobilityDuration();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00006199 File Offset: 0x00004399
		public override float GetInvulnerabilityDuration()
		{
			if (this.BaseHero.IsReincarnating)
			{
				return this.BaseHero.RespawnTime - Game.RawGameTime + 0.1f;
			}
			return base.GetInvulnerabilityDuration();
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00021184 File Offset: 0x0001F384
		public override Vector3 GetPredictedPosition(float delay = 0f)
		{
			if (!base.IsMoving)
			{
				return base.CachedPosition;
			}
			float num = base.BaseUnit.NetworkRotationRad;
			if (base.IsVisible)
			{
				float num2 = MathUtil.DegreesToRadians(base.BaseUnit.RotationDifference);
				delay = Math.Max(delay - this.GetTurnTime(Math.Abs(num2)), 0f);
				num += num2;
			}
			Vector3 vector;
			vector..ctor((float)Math.Cos((double)num), (float)Math.Sin((double)num), 0f);
			return base.CachedPosition + vector * (Game.RawGameTime - base.LastVisibleTime + delay) * base.Speed;
		}

		// Token: 0x040002C6 RID: 710
		private Vector2 healthBarPositionCorrection;

		// Token: 0x040002C7 RID: 711
		private Vector2 healthBarSize;
	}
}
