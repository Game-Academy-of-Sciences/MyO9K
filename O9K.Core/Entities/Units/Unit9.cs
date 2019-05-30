using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Abilities.Items;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using O9K.Core.Helpers.Range;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Renderer.Utils;
using SharpDX;

namespace O9K.Core.Entities.Units
{
	// Token: 0x020000AF RID: 175
	public class Unit9 : Entity9
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x0001FAF4 File Offset: 0x0001DCF4
		public Unit9(Unit baseUnit) : base(baseUnit)
		{
			this.BaseUnit = baseUnit;
			this.BaseOwner = baseUnit.Owner;
			this.CachedPosition = baseUnit.Position;
			this.IsControllable = baseUnit.IsControllable;
			this.Team = baseUnit.Team;
			this.EnemyTeam = ((this.Team == Team.Radiant) ? Team.Dire : Team.Radiant);
			this.IsRanged = baseUnit.IsRanged;
			this.BaseAttackRange = baseUnit.AttackRange;
			this.HullRadius = baseUnit.HullRadius;
			this.MoveCapability = baseUnit.MoveCapability;
			this.AttackCapability = baseUnit.AttackCapability;
			this.AttackType = baseUnit.AttackDamageType;
			this.ArmorType = baseUnit.ArmorType;
			this.IsCreep = (baseUnit is Creep);
			this.IsLaneCreep = (baseUnit.UnitType == 1152);
			this.IsUnit = (baseUnit.UnitType != 0);
			this.HpBarOffset = (float)baseUnit.HealthBarOffset;
			this.BaseAttackTime = baseUnit.BaseAttackTime;
			this.Speed = (float)baseUnit.MovementSpeed;
			this.CreateTime = (this.LastVisibleTime = Game.RawGameTime);
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00005245 File Offset: 0x00003445
		public float CreateTime { get; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000524D File Offset: 0x0000344D
		public bool IsLaneCreep { get; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00005255 File Offset: 0x00003455
		public ArmorType ArmorType { get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000525D File Offset: 0x0000345D
		public bool CanDie
		{
			get
			{
				return !this.amplifiers.Any((IHasDamageAmplify x) => x.IsValid && (x.AmplifiesDamage & AmplifiesDamage.Incoming) != AmplifiesDamage.None && (x.AmplifierDamageType & DamageType.Pure) != DamageType.None && x.AmplifierValue(this, this) <= -1f);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00005279 File Offset: 0x00003479
		public AttackDamageType AttackType { get; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00005281 File Offset: 0x00003481
		public uint Level
		{
			get
			{
				return this.BaseUnit.Level;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001FCB4 File Offset: 0x0001DEB4
		public Rectangle9 HealthBar
		{
			get
			{
				Vector2 healthBarPosition = this.HealthBarPosition;
				if (healthBarPosition.IsZero)
				{
					return Rectangle9.Zero;
				}
				Vector2 vector = this.HealthBarSize;
				return new Rectangle9(healthBarPosition, vector.X, vector.Y);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000528E File Offset: 0x0000348E
		public virtual Vector2 HealthBarSize
		{
			get
			{
				if (this.healthBarSize.IsZero)
				{
					this.healthBarSize = new Vector2(Hud.Info.ScreenSize.X / 24.5f, Hud.Info.ScreenSize.Y / 155.1f);
				}
				return this.healthBarSize;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001FCF0 File Offset: 0x0001DEF0
		public Vector2 HealthBarPosition
		{
			get
			{
				Vector2 vector;
				if (!Drawing.WorldToScreen(this.Position.IncreaseZ(this.HpBarOffset), out vector))
				{
					return Vector2.Zero;
				}
				return vector - this.HealthBarPositionCorrection;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0001FD2C File Offset: 0x0001DF2C
		public int ProjectileSpeed
		{
			get
			{
				if (this.projectileSpeed < 0)
				{
					try
					{
						this.projectileSpeed = Game.FindKeyValues(base.Name + "/ProjectileSpeed", this.IsHero ? KeyValueSource.Hero : KeyValueSource.Unit).IntValue;
					}
					catch
					{
						this.projectileSpeed = 0;
					}
				}
				return this.projectileSpeed;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x000052CE File Offset: 0x000034CE
		public IEnumerable<Ability9> Abilities
		{
			get
			{
				return from x in this.abilities
				where x.IsValid
				select x;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x000052FA File Offset: 0x000034FA
		public AttackCapability AttackCapability { get; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00005302 File Offset: 0x00003502
		public Inventory BaseInventory
		{
			get
			{
				return this.BaseUnit.Inventory;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000530F File Offset: 0x0000350F
		public IEnumerable<Modifier> BaseModifiers
		{
			get
			{
				return this.BaseUnit.Modifiers;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000531C File Offset: 0x0000351C
		public Spellbook BaseSpellbook
		{
			get
			{
				return this.BaseUnit.Spellbook;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00005329 File Offset: 0x00003529
		public Unit BaseUnit { get; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00005331 File Offset: 0x00003531
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00005339 File Offset: 0x00003539
		public Entity BaseOwner { get; internal set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0001FD90 File Offset: 0x0001DF90
		public bool CanBecomeInvisible
		{
			get
			{
				return this.Abilities.Any((Ability9 x) => x.IsInvisibility && x.CanBeCasted(false)) || this.BaseUnit.InvisiblityLevel > 0f;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00005342 File Offset: 0x00003542
		public bool CanBecomeMagicImmune
		{
			get
			{
				return this.Abilities.OfType<IShield>().Any((IShield x) => x.ShieldsOwner && x.IsMagicImmunity() && x.CanBeCasted(false));
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00005373 File Offset: 0x00003573
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0000537B File Offset: 0x0000357B
		public bool CanBeHealed { get; internal set; } = true;

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00005384 File Offset: 0x00003584
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x0000538C File Offset: 0x0000358C
		public bool IsEthereal { get; internal set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00005395 File Offset: 0x00003595
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x0000539D File Offset: 0x0000359D
		public bool IsCharging { get; internal set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x000053A6 File Offset: 0x000035A6
		public virtual bool CanReincarnate
		{
			get
			{
				return this.HasAegis;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x000053AE File Offset: 0x000035AE
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x000053B6 File Offset: 0x000035B6
		public bool CanUseAbilities { get; protected set; } = true;

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x000053BF File Offset: 0x000035BF
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x000053C7 File Offset: 0x000035C7
		public bool CanUseAbilitiesInInvisibility { get; internal set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x000053D0 File Offset: 0x000035D0
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x000053D8 File Offset: 0x000035D8
		public bool ChannelActivatesOnCast { get; internal set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x000053E1 File Offset: 0x000035E1
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x000053E9 File Offset: 0x000035E9
		public bool IsTeleporting { get; internal set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x000053F2 File Offset: 0x000035F2
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x000053FA File Offset: 0x000035FA
		public float ChannelEndTime
		{
			get
			{
				return this.channelEndTime;
			}
			internal set
			{
				this.channelEndTime = value;
				this.channelingSleeper.Sleep(value - Game.RawGameTime);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00005415 File Offset: 0x00003615
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0000541D File Offset: 0x0000361D
		public Team EnemyTeam { get; internal set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00005426 File Offset: 0x00003626
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x0000542E File Offset: 0x0000362E
		public bool HasAghanimsScepter { get; internal set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0001FDE0 File Offset: 0x0001DFE0
		public virtual float Health
		{
			get
			{
				if (!this.IsVisible)
				{
					return Math.Min(this.BaseUnit.Health + (Game.RawGameTime - this.LastVisibleTime) * this.HealthRegeneration, this.BaseUnit.MaximumHealth);
				}
				return this.BaseUnit.Health;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001FE38 File Offset: 0x0001E038
		public virtual float Mana
		{
			get
			{
				if (!this.IsVisible)
				{
					return Math.Min(this.BaseUnit.Mana + (Game.RawGameTime - this.LastVisibleTime) * this.ManaRegeneration, this.BaseUnit.MaximumMana);
				}
				return this.BaseUnit.Mana;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00005437 File Offset: 0x00003637
		public float HealthPercentage
		{
			get
			{
				return this.HealthPercentageBase * 100f;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00005445 File Offset: 0x00003645
		public float HealthPercentageBase
		{
			get
			{
				return this.Health / this.BaseUnit.MaximumHealth;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0000545B File Offset: 0x0000365B
		public float HullRadius { get; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00005463 File Offset: 0x00003663
		public virtual bool IsAlive
		{
			get
			{
				return this.BaseUnit.IsAlive && this.BaseUnit.IsSpawned;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000547F File Offset: 0x0000367F
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x00005487 File Offset: 0x00003687
		public bool IsAttacking { get; internal set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00005490 File Offset: 0x00003690
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00005498 File Offset: 0x00003698
		public bool IsBarrack { get; internal set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x000054A1 File Offset: 0x000036A1
		public bool IsBlockingAbilities
		{
			get
			{
				return this.IsLotusProtected || this.IsLinkensProtected || this.IsSpellShieldProtected || this.IsUntargetable;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x000054C3 File Offset: 0x000036C3
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x000054CB File Offset: 0x000036CB
		public bool IsBuilding { get; protected set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x000054D4 File Offset: 0x000036D4
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x000054DC File Offset: 0x000036DC
		public bool IsCasting { get; internal set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x000054E5 File Offset: 0x000036E5
		public bool IsChanneling
		{
			get
			{
				if (!this.channelingSleeper.IsSleeping)
				{
					return this.abilities.Any((Ability9 x) => x.IsChanneling);
				}
				return true;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00005520 File Offset: 0x00003720
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00005528 File Offset: 0x00003728
		public bool IsControllable { get; internal set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00005531 File Offset: 0x00003731
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00005539 File Offset: 0x00003739
		public bool IsCourier { get; protected set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00005542 File Offset: 0x00003742
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0000554A File Offset: 0x0000374A
		public bool IsCreep { get; protected set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00005553 File Offset: 0x00003753
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0000555B File Offset: 0x0000375B
		public bool IsDarkPactProtected { get; internal set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00005564 File Offset: 0x00003764
		public bool IsDisarmed
		{
			get
			{
				return (this.UnitState & UnitState.Disarmed) != (UnitState)0UL || this.AttackCapability == AttackCapability.None;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0000557C File Offset: 0x0000377C
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x00005584 File Offset: 0x00003784
		public bool IsFountain { get; internal set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0000558D File Offset: 0x0000378D
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x00005595 File Offset: 0x00003795
		public bool IsHero { get; protected set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0000559E File Offset: 0x0000379E
		public bool IsHexed
		{
			get
			{
				return (this.UnitState & UnitState.Hexed) > (UnitState)0UL;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x000055AE File Offset: 0x000037AE
		public bool IsCommandRestricted
		{
			get
			{
				return (this.UnitState & UnitState.CommandRestricted) > (UnitState)0UL;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x000055C1 File Offset: 0x000037C1
		public bool IsAttackImmune
		{
			get
			{
				return (this.UnitState & UnitState.AttackImmune) > (UnitState)0UL;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x000055D0 File Offset: 0x000037D0
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x000055D8 File Offset: 0x000037D8
		public bool IsIllusion { get; protected set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x000055E1 File Offset: 0x000037E1
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x000055E9 File Offset: 0x000037E9
		public bool IsImportant { get; protected set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x000055F2 File Offset: 0x000037F2
		public bool IsInvisible
		{
			get
			{
				return (this.UnitState & UnitState.Invisible) != (UnitState)0UL || this.BaseUnit.InvisiblityLevel > 0.5f;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00005617 File Offset: 0x00003817
		public virtual bool IsInvulnerable
		{
			get
			{
				return (this.UnitState & (UnitState.Invulnerable | UnitState.NoHealthbar)) > (UnitState)0UL;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000562A File Offset: 0x0000382A
		public virtual bool IsLinkensProtected
		{
			get
			{
				if (!this.IsLinkensTargetProtected)
				{
					LinkensSphere linkensSphere = this.LinkensSphere;
					return linkensSphere != null && linkensSphere.IsReady;
				}
				return true;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00005647 File Offset: 0x00003847
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0000564F File Offset: 0x0000384F
		public bool IsLotusProtected { get; internal set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00005658 File Offset: 0x00003858
		public bool IsMagicImmune
		{
			get
			{
				return (this.UnitState & UnitState.MagicImmune) > (UnitState)0UL;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000566B File Offset: 0x0000386B
		public bool IsMoving
		{
			get
			{
				return this.BaseUnit.IsMoving;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00005678 File Offset: 0x00003878
		public bool IsInNormalState
		{
			get
			{
				return (this.UnitState & (UnitState.Rooted | UnitState.Disarmed | UnitState.Silenced | UnitState.Muted | UnitState.Stunned | UnitState.Hexed)) == (UnitState)0UL && this.GetImmobilityDuration() <= 0f;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00005698 File Offset: 0x00003898
		public bool IsMuted
		{
			get
			{
				return (this.UnitState & UnitState.Muted) > (UnitState)0UL;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x000056A8 File Offset: 0x000038A8
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x000056B0 File Offset: 0x000038B0
		public bool IsMyHero { get; internal set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001FE88 File Offset: 0x0001E088
		public bool IsMyControllable
		{
			get
			{
				if (!this.IsUnit || !this.IsControllable)
				{
					return false;
				}
				if (this.IsHero)
				{
					Entity baseOwner = this.BaseOwner;
					EntityHandle? entityHandle = (baseOwner != null) ? new EntityHandle?(baseOwner.Handle) : null;
					if (((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == EntityManager9.Owner.PlayerHandle)
					{
						return true;
					}
				}
				else
				{
					Entity baseOwner2 = this.BaseOwner;
					EntityHandle? entityHandle = (baseOwner2 != null) ? new EntityHandle?(baseOwner2.Handle) : null;
					if (((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == EntityManager9.Owner.HeroHandle)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x000056B9 File Offset: 0x000038B9
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x000056C1 File Offset: 0x000038C1
		public bool IsRanged { get; internal set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x000056CA File Offset: 0x000038CA
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x000056D2 File Offset: 0x000038D2
		public bool IsReflectingDamage { get; internal set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000056DB File Offset: 0x000038DB
		public bool IsRooted
		{
			get
			{
				return (this.UnitState & UnitState.Rooted) != (UnitState)0UL || this.MoveCapability == MoveCapability.None;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x000056F3 File Offset: 0x000038F3
		public bool IsLeashed
		{
			get
			{
				return (this.UnitState & UnitState.Tethered) > (UnitState)0UL;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00005709 File Offset: 0x00003909
		public bool IsRotating
		{
			get
			{
				return (int)this.BaseUnit.RotationDifference != 0;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000571A File Offset: 0x0000391A
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x00005722 File Offset: 0x00003922
		public bool IsRuptured { get; internal set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0000572B File Offset: 0x0000392B
		public bool IsSilenced
		{
			get
			{
				return (this.UnitState & UnitState.Silenced) > (UnitState)0UL;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000573A File Offset: 0x0000393A
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00005742 File Offset: 0x00003942
		public virtual bool IsSpellShieldProtected { get; internal set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000574B File Offset: 0x0000394B
		public bool IsStunned
		{
			get
			{
				return (this.UnitState & (UnitState.Stunned | UnitState.CommandRestricted)) > (UnitState)0UL;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000575E File Offset: 0x0000395E
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x00005766 File Offset: 0x00003966
		public bool IsTower { get; protected set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0000576F File Offset: 0x0000396F
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00005777 File Offset: 0x00003977
		public bool IsUnit { get; protected set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00005780 File Offset: 0x00003980
		public bool IsUntargetable
		{
			get
			{
				return (this.UnitState & UnitState.Untargetable) > (UnitState)0UL;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00005796 File Offset: 0x00003996
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0000579E File Offset: 0x0000399E
		public bool IsVisible { get; internal set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x000057A7 File Offset: 0x000039A7
		public bool IsVisibleToEnemies
		{
			get
			{
				return this.BaseUnit.IsVisibleToEnemies;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x000057B4 File Offset: 0x000039B4
		public float ManaPercentage
		{
			get
			{
				return this.ManaPercentageBase * 100f;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x000057C2 File Offset: 0x000039C2
		public float ManaPercentageBase
		{
			get
			{
				return this.Mana / this.BaseUnit.MaximumMana;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x000057D6 File Offset: 0x000039D6
		public float MaximumHealth
		{
			get
			{
				return this.BaseUnit.MaximumHealth;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x000057E5 File Offset: 0x000039E5
		public float MaximumMana
		{
			get
			{
				return this.BaseUnit.MaximumMana;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x000057F2 File Offset: 0x000039F2
		public MoveCapability MoveCapability { get; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x000057FA File Offset: 0x000039FA
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00005802 File Offset: 0x00003A02
		public float LastVisibleTime { get; internal set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000580B File Offset: 0x00003A0B
		public virtual Vector3 Position
		{
			get
			{
				return this.CachedPosition;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00005813 File Offset: 0x00003A13
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x0000581B File Offset: 0x00003A1B
		public Ensage.Attribute PrimaryAttribute { get; protected set; } = Ensage.Attribute.Invalid;

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00005824 File Offset: 0x00003A24
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x0000582C File Offset: 0x00003A2C
		public Unit9 Target { get; internal set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00005835 File Offset: 0x00003A35
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x0000583D File Offset: 0x00003A3D
		public Team Team { get; internal set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00005846 File Offset: 0x00003A46
		public virtual float TotalAgility { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0000584E File Offset: 0x00003A4E
		public virtual float Agility { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00005856 File Offset: 0x00003A56
		public virtual float TotalIntelligence { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0000585E File Offset: 0x00003A5E
		public virtual float Intelligence { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00005866 File Offset: 0x00003A66
		public virtual float TotalStrength { get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000586E File Offset: 0x00003A6E
		public virtual float Strength { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0001FF84 File Offset: 0x0001E184
		public float TurnRate
		{
			get
			{
				if (this.turnRate < 0f)
				{
					try
					{
						this.turnRate = Game.FindKeyValues(base.Name + "/MovementTurnRate", this.IsHero ? KeyValueSource.Hero : KeyValueSource.Unit).FloatValue;
					}
					catch
					{
						this.turnRate = 0.5f;
					}
				}
				return this.turnRate;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001FFF0 File Offset: 0x0001E1F0
		public UnitState UnitState
		{
			get
			{
				UnitState unitState = this.BaseUnit.UnitState;
				foreach (KeyValuePair<UnitState, Sleeper> keyValuePair in this.expectedUnitStateSleeper)
				{
					if (keyValuePair.Value.IsSleeping)
					{
						unitState |= keyValuePair.Key;
					}
				}
				return unitState;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00005876 File Offset: 0x00003A76
		public virtual float HealthRegeneration
		{
			get
			{
				return this.BaseUnit.HealthRegeneration;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00005883 File Offset: 0x00003A83
		public virtual float ManaRegeneration
		{
			get
			{
				return this.BaseUnit.ManaRegeneration;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00005890 File Offset: 0x00003A90
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00005898 File Offset: 0x00003A98
		public float Speed { get; internal set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x000058A1 File Offset: 0x00003AA1
		public float SecondsPerAttack
		{
			get
			{
				return this.BaseUnit.SecondsPerAttack;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x000058AE File Offset: 0x00003AAE
		public float AttacksPerSecond
		{
			get
			{
				return this.BaseUnit.AttacksPerSecond;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x000058BB File Offset: 0x00003ABB
		internal virtual float BaseAttackRange { get; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0002005C File Offset: 0x0001E25C
		internal float BonusAttackRange
		{
			get
			{
				return (from x in this.ranges
				where x.IsValid && x.RangeIncreaseType == RangeIncreaseType.Attack
				select x).Sum((IHasRangeIncrease x) => x.RangeIncrease);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x000200B8 File Offset: 0x0001E2B8
		internal float BonusCastRange
		{
			get
			{
				return (from x in this.ranges
				where x.IsValid && x.RangeIncreaseType == RangeIncreaseType.Ability
				select x).Sum((IHasRangeIncrease x) => x.RangeIncrease);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x000058C3 File Offset: 0x00003AC3
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x000058CB File Offset: 0x00003ACB
		internal Vector3 CachedPosition { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x000058D4 File Offset: 0x00003AD4
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x000058DC File Offset: 0x00003ADC
		internal bool HasAegis { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x000058E5 File Offset: 0x00003AE5
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x000058ED File Offset: 0x00003AED
		internal Unit9 HurricanePikeTarget { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x000058F6 File Offset: 0x00003AF6
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x000058FE File Offset: 0x00003AFE
		internal bool IsLinkensTargetProtected { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00005907 File Offset: 0x00003B07
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0000590F File Offset: 0x00003B0F
		internal LinkensSphere LinkensSphere { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00005918 File Offset: 0x00003B18
		protected virtual float BaseAttackTime { get; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00020114 File Offset: 0x0001E314
		protected virtual Vector2 HealthBarPositionCorrection
		{
			get
			{
				if (this.healthBarPositionCorrection.IsZero)
				{
					this.healthBarPositionCorrection = new Vector2(this.HealthBarSize.X / 1.95f, Hud.Info.ScreenSize.Y / 87f);
				}
				return this.healthBarPositionCorrection;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00020160 File Offset: 0x0001E360
		private float BaseAttackAnimationPoint
		{
			get
			{
				if (this.baseAttackAnimationPoint < 0f)
				{
					try
					{
						this.baseAttackAnimationPoint = Game.FindKeyValues(base.Name + "/AttackAnimationPoint", this.IsHero ? KeyValueSource.Hero : KeyValueSource.Unit).FloatValue;
					}
					catch
					{
						this.baseAttackAnimationPoint = 0f;
					}
				}
				return this.baseAttackAnimationPoint;
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00005920 File Offset: 0x00003B20
		public static implicit operator Unit(Unit9 unit)
		{
			return unit.BaseUnit;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00005928 File Offset: 0x00003B28
		public bool Attack(Unit9 target)
		{
			if (this.actionSleeper.IsSleeping)
			{
				return false;
			}
			if (!this.BaseUnit.Attack(target.BaseUnit))
			{
				return false;
			}
			this.actionSleeper.Sleep(0.1f);
			return true;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000201CC File Offset: 0x0001E3CC
		public bool CanAttack(Unit9 target = null, float additionalRange = 0f)
		{
			if (target != null)
			{
				if (!target.IsAlive || target.IsInvulnerable || target.IsUntargetable || target.IsAttackImmune || !target.IsVisible)
				{
					return false;
				}
				if (this.Distance(target) > this.GetAttackRange(target, additionalRange))
				{
					return false;
				}
			}
			return !this.IsDisarmed && !this.IsCasting && !this.IsStunned && !this.IsChanneling && this.AttackCapability > AttackCapability.None;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000595F File Offset: 0x00003B5F
		public bool CanMove(bool checkChanneling = true)
		{
			return (!checkChanneling || !this.IsChanneling) && (!this.IsRooted && !this.IsStunned && !this.IsInvulnerable) && this.MoveCapability > MoveCapability.None;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00005991 File Offset: 0x00003B91
		public float Distance(Unit9 unit)
		{
			return unit.Position.Distance2D(this.Position, false);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000059A5 File Offset: 0x00003BA5
		public float Distance(Vector3 position)
		{
			return this.Position.Distance2D(position, false);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0002024C File Offset: 0x0001E44C
		public Ability GetAbilityById(AbilityId id)
		{
			return this.BaseSpellbook.Spells.FirstOrDefault((Ability x) => x.Id == id);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000059B4 File Offset: 0x00003BB4
		public float GetAngle(Unit9 unit, bool rotationDifference = false)
		{
			return this.GetAngle(unit.Position, rotationDifference);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00020284 File Offset: 0x0001E484
		public virtual float GetAngle(Vector3 position, bool rotationDifference = false)
		{
			double num = Math.Abs(Math.Atan2((double)(position.Y - this.Position.Y), (double)(position.X - this.Position.X)) - (double)this.BaseUnit.NetworkRotationRad);
			if (num > 3.1415926535897931)
			{
				num = Math.Abs(6.2831853071795862 - num);
			}
			return (float)num;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x000059C3 File Offset: 0x00003BC3
		public float GetAttackBackswing(Unit9 target = null)
		{
			return this.GetAttackRate(target) - this.GetAttackPoint(target);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000202F0 File Offset: 0x0001E4F0
		public int GetAttackDamage(Unit9 target, DamageValue damageValue = DamageValue.Minimum, float additionalPhysicalDamage = 0f)
		{
			int num = 0;
			foreach (KeyValuePair<DamageType, float> keyValuePair in this.GetRawAttackDamage(target, damageValue, 1f, additionalPhysicalDamage))
			{
				float damageAmplification = target.GetDamageAmplification(this, keyValuePair.Key, false);
				float damageBlock = target.GetDamageBlock(keyValuePair.Key);
				num += (int)((keyValuePair.Value - damageBlock) * damageAmplification);
			}
			return Math.Max(num, 0);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000059D4 File Offset: 0x00003BD4
		public float GetAttackPoint(Unit9 target = null)
		{
			return this.BaseAttackAnimationPoint / (1f + (this.GetAttackSpeed(target) - 100f) / 100f);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00020378 File Offset: 0x0001E578
		public virtual float GetAttackRange(Unit9 target = null, float additionalRange = 0f)
		{
			Unit9 hurricanePikeTarget = this.HurricanePikeTarget;
			if (hurricanePikeTarget != null && hurricanePikeTarget.Equals(target))
			{
				return 9999999f;
			}
			return this.BaseAttackRange + this.BonusAttackRange + ((target != null) ? target.HullRadius : 0f) + this.HullRadius + additionalRange;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000203C8 File Offset: 0x0001E5C8
		public float GetDamageAmplification(Unit9 source, DamageType damageType, bool intAmplify)
		{
			float num = 1f;
			if (intAmplify)
			{
				num += source.TotalIntelligence * 0.0007f;
			}
			float num2 = (from x in source.amplifiers
			where x.IsValid && (x.AmplifiesDamage & AmplifiesDamage.Outgoing) != AmplifiesDamage.None && (x.AmplifierDamageType & damageType) > DamageType.None
			select x).Sum((IHasDamageAmplify x) => x.AmplifierValue(this, source));
			float num3 = (from x in this.amplifiers
			where x.IsValid && (x.AmplifiesDamage & AmplifiesDamage.Incoming) != AmplifiesDamage.None && (x.AmplifierDamageType & damageType) > DamageType.None
			select x).Sum((IHasDamageAmplify x) => x.AmplifierValue(source, this));
			num *= Math.Max(1f + num2, 0f) * Math.Max(1f + num3, 0f);
			DamageType damageType2 = damageType;
			if (damageType2 != DamageType.Physical)
			{
				if (damageType2 == DamageType.Magical)
				{
					num *= 1f - this.BaseUnit.MagicDamageResist;
				}
			}
			else
			{
				num *= 1f - (this.IsEthereal ? 1f : this.BaseUnit.DamageResist);
			}
			return num;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000204D8 File Offset: 0x0001E6D8
		public float GetDamageBlock(DamageType damageType)
		{
			return (from x in this.blockers.Values
			where x.IsValid && (x.BlockDamageType & damageType) > DamageType.None
			select x).Sum((IHasDamageBlock x) => x.BlockValue(this));
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000059F6 File Offset: 0x00003BF6
		public IEnumerable<IHasDamageBlock> GetDamageBlockers()
		{
			return from x in this.blockers.Values
			where x.IsValid
			select x;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00020528 File Offset: 0x0001E728
		public virtual float GetImmobilityDuration()
		{
			List<Modifier> list = (from x in this.immobilityModifiers
			where x.IsValid && x.ElapsedTime > 0.1f
			select x).ToList<Modifier>();
			if (list.Count == 0)
			{
				return 0f;
			}
			return list.Max((Modifier x) => x.RemainingTime);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00020598 File Offset: 0x0001E798
		public virtual float GetInvulnerabilityDuration()
		{
			List<Modifier> list = (from x in this.invulnerabilityModifiers
			where x.IsValid && x.ElapsedTime > 0.1f
			select x).ToList<Modifier>();
			if (list.Count == 0)
			{
				return 0f;
			}
			return list.Max((Modifier x) => x.RemainingTime);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00020608 File Offset: 0x0001E808
		public Modifier GetModifier(string name)
		{
			return this.BaseModifiers.FirstOrDefault((Modifier x) => x.Name == name);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0002063C File Offset: 0x0001E83C
		public int GetModifierStacks(string name)
		{
			Modifier modifier = this.BaseModifiers.FirstOrDefault((Modifier x) => x.Name == name);
			if (modifier == null)
			{
				return 0;
			}
			return modifier.StackCount;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00020678 File Offset: 0x0001E878
		public virtual Vector3 GetPredictedPosition(float delay = 0f)
		{
			if (!this.IsMoving || delay <= 0f)
			{
				return this.Position;
			}
			float networkRotationRad = this.BaseUnit.NetworkRotationRad;
			Vector3 vector;
			vector..ctor((float)Math.Cos((double)networkRotationRad), (float)Math.Sin((double)networkRotationRad), 0f);
			return this.Position + vector * delay * this.Speed;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00005A27 File Offset: 0x00003C27
		public float GetTurnTime(Vector3 position)
		{
			return this.GetTurnTime(this.GetAngle(position, false));
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00005A37 File Offset: 0x00003C37
		public virtual float GetTurnTime(float angleRad)
		{
			angleRad -= 0.2f;
			if (angleRad <= 0f)
			{
				return 0f;
			}
			return 0.03f / this.TurnRate * angleRad * 1.15f;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000206E4 File Offset: 0x0001E8E4
		public bool HasModifier(params string[] names)
		{
			return this.BaseModifiers.Any((Modifier x) => names.Contains(x.Name));
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00020718 File Offset: 0x0001E918
		public bool HasModifier(string name)
		{
			return this.BaseModifiers.Any((Modifier x) => x.Name == name);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0002074C File Offset: 0x0001E94C
		public Vector3 InFront(float range, float angle = 0f, bool rotationDifference = true)
		{
			float num = MathUtil.DegreesToRadians((rotationDifference ? this.BaseUnit.RotationDifference : 0f) + angle);
			float num2 = this.BaseUnit.NetworkRotationRad + num;
			Vector3 vector;
			vector..ctor((float)Math.Cos((double)num2), (float)Math.Sin((double)num2), 0f);
			return this.Position + vector * range;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00005A64 File Offset: 0x00003C64
		public bool IsAlly()
		{
			return this.Team == EntityManager9.Owner.Team;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00005A78 File Offset: 0x00003C78
		public bool IsAlly(Unit9 unit)
		{
			return unit.Team == this.Team;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000207B4 File Offset: 0x0001E9B4
		public bool IsAttackingHero()
		{
			Unit9 target = this.Target;
			return target != null && target.IsValid && this.Target.IsHero && !this.Target.IsIllusion && this.Target.IsAlive && this.Distance(this.Target) < this.GetAttackRange(this.Target, 200f);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00005A88 File Offset: 0x00003C88
		public bool IsEnemy()
		{
			return this.Team != EntityManager9.Owner.Team;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00005A9F File Offset: 0x00003C9F
		public bool IsEnemy(Unit9 unit)
		{
			return unit.Team == this.EnemyTeam;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00005AAF File Offset: 0x00003CAF
		public bool Move(Unit9 toTarget)
		{
			return this.Move(toTarget.Position);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00005ABD File Offset: 0x00003CBD
		public bool Move(Vector3 position)
		{
			if (this.actionSleeper.IsSleeping)
			{
				return false;
			}
			if (!this.BaseUnit.Move(position))
			{
				return false;
			}
			this.actionSleeper.Sleep(0.1f);
			return true;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00005AEF File Offset: 0x00003CEF
		public void RefreshUnitState()
		{
			this.expectedUnitStateSleeper.Reset();
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00005AFC File Offset: 0x00003CFC
		public void SetExpectedUnitState(UnitState state, float time = 0.1f)
		{
			this.expectedUnitStateSleeper[state].ExtendSleep(Math.Min(time, 2f));
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00005B1A File Offset: 0x00003D1A
		public bool Stop()
		{
			if (this.actionSleeper.IsSleeping)
			{
				return false;
			}
			if (!this.BaseUnit.Stop())
			{
				return false;
			}
			this.actionSleeper.Sleep(0.1f);
			return true;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00005B4B File Offset: 0x00003D4B
		internal virtual void Ability(Ability9 ability, bool added)
		{
			if (added)
			{
				this.abilities.Add(ability);
				return;
			}
			this.abilities.Remove(ability);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00005B6A File Offset: 0x00003D6A
		internal void Amplifier(IHasDamageAmplify amplify, bool added)
		{
			if (added)
			{
				this.amplifiers.Add(amplify);
				return;
			}
			this.amplifiers.Remove(amplify);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00005B89 File Offset: 0x00003D89
		internal void Blocker(IHasDamageBlock block, bool added)
		{
			if (added)
			{
				this.blockers[block.BlockModifierName] = block;
				return;
			}
			this.blockers.Remove(block.BlockModifierName);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00020820 File Offset: 0x0001EA20
		internal void Disabler(Modifier modifier, bool added, bool invulnerability)
		{
			if (added)
			{
				this.immobilityModifiers.Add(modifier);
				if (invulnerability)
				{
					this.invulnerabilityModifiers.Add(modifier);
					return;
				}
			}
			else
			{
				Modifier modifier2 = this.immobilityModifiers.Find((Modifier x) => x.Handle == modifier.Handle);
				if (modifier2 != null)
				{
					this.immobilityModifiers.Remove(modifier2);
					if (invulnerability)
					{
						this.invulnerabilityModifiers.Remove(modifier2);
					}
				}
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00005BB3 File Offset: 0x00003DB3
		internal virtual float GetAttackRate(Unit9 target = null)
		{
			return this.BaseAttackTime / (1f + (this.GetAttackSpeed(target) - 100f) / 100f);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000208A4 File Offset: 0x0001EAA4
		internal virtual float GetAttackSpeed(Unit9 target = null)
		{
			float num = 0f;
			Unit9 hurricanePikeTarget = this.HurricanePikeTarget;
			if (hurricanePikeTarget != null && hurricanePikeTarget.Equals(target))
			{
				num += 90f;
			}
			return Math.Max(Math.Min(this.BaseUnit.AttackSpeedValue + num, 700f), 20f);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000208F8 File Offset: 0x0001EAF8
		internal Damage GetOnHitEffectDamage(Unit9 unit)
		{
			Damage damage = new Damage();
			foreach (IHasPassiveDamageIncrease hasPassiveDamageIncrease in this.passiveDamageAbilities)
			{
				if (hasPassiveDamageIncrease.IsValid)
				{
					damage += hasPassiveDamageIncrease.GetRawDamage(unit, null);
				}
			}
			return damage;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0002096C File Offset: 0x0001EB6C
		internal Damage GetRawAttackDamage(Unit9 target, DamageValue damageValue = DamageValue.Minimum, float physCritMultiplier = 1f, float additionalPhysicalDamage = 0f)
		{
			Damage damage = new Damage();
			float num = 0f;
			switch (damageValue)
			{
			case DamageValue.Minimum:
				num += (float)this.BaseUnit.MinimumDamage;
				break;
			case DamageValue.Average:
				num += (float)this.BaseUnit.DamageAverage;
				break;
			case DamageValue.Maximum:
				num += (float)this.BaseUnit.MaximumDamage;
				break;
			}
			if (this.IsIllusion && !this.CanUseAbilities)
			{
				num *= 0.25f;
			}
			else
			{
				num += (float)this.BaseUnit.BonusDamage;
			}
			if (num <= 0f)
			{
				return damage;
			}
			Damage damage2;
			foreach (IHasPassiveDamageIncrease hasPassiveDamageIncrease in this.passiveDamageAbilities)
			{
				if (hasPassiveDamageIncrease.IsValid)
				{
					foreach (KeyValuePair<DamageType, float> keyValuePair in hasPassiveDamageIncrease.GetRawDamage(target, null))
					{
						if (hasPassiveDamageIncrease.MultipliedByCrit && keyValuePair.Key == DamageType.Physical)
						{
							num += keyValuePair.Value;
						}
						else
						{
							damage2 = damage;
							DamageType key = keyValuePair.Key;
							damage2[key] += keyValuePair.Value;
						}
					}
				}
			}
			damage2 = damage;
			damage2[DamageType.Physical] = damage2[DamageType.Physical] + (num * physCritMultiplier + additionalPhysicalDamage);
			damage2 = damage;
			damage2[DamageType.Physical] = damage2[DamageType.Physical] * DamageExtensions.GetMeleeDamageMultiplier(this, target);
			return damage;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00005BD5 File Offset: 0x00003DD5
		internal void Passive(IHasPassiveDamageIncrease passive, bool added)
		{
			if (added)
			{
				this.passiveDamageAbilities.Add(passive);
				return;
			}
			this.passiveDamageAbilities.Remove(passive);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00005BF4 File Offset: 0x00003DF4
		internal void Range(IHasRangeIncrease range, bool added)
		{
			if (added)
			{
				this.ranges.Add(range);
				return;
			}
			this.ranges.Remove(range);
		}

		// Token: 0x04000249 RID: 585
		public readonly List<IHasDamageAmplify> amplifiers = new List<IHasDamageAmplify>();

		// Token: 0x0400024A RID: 586
		protected readonly float HpBarOffset;

		// Token: 0x0400024B RID: 587
		private readonly List<Ability9> abilities = new List<Ability9>();

		// Token: 0x0400024C RID: 588
		private readonly Sleeper actionSleeper = new Sleeper();

		// Token: 0x0400024D RID: 589
		private readonly Dictionary<string, IHasDamageBlock> blockers = new Dictionary<string, IHasDamageBlock>();

		// Token: 0x0400024E RID: 590
		private readonly Sleeper channelingSleeper = new Sleeper();

		// Token: 0x0400024F RID: 591
		private readonly MultiSleeper<UnitState> expectedUnitStateSleeper = new MultiSleeper<UnitState>();

		// Token: 0x04000250 RID: 592
		private readonly List<Modifier> immobilityModifiers = new List<Modifier>();

		// Token: 0x04000251 RID: 593
		private readonly List<Modifier> invulnerabilityModifiers = new List<Modifier>();

		// Token: 0x04000252 RID: 594
		private readonly List<IHasPassiveDamageIncrease> passiveDamageAbilities = new List<IHasPassiveDamageIncrease>();

		// Token: 0x04000253 RID: 595
		private readonly List<IHasRangeIncrease> ranges = new List<IHasRangeIncrease>();

		// Token: 0x04000254 RID: 596
		private float baseAttackAnimationPoint = -1f;

		// Token: 0x04000255 RID: 597
		private float channelEndTime;

		// Token: 0x04000256 RID: 598
		private Vector2 healthBarPositionCorrection;

		// Token: 0x04000257 RID: 599
		private Vector2 healthBarSize;

		// Token: 0x04000258 RID: 600
		private int projectileSpeed = -1;

		// Token: 0x04000259 RID: 601
		private float turnRate = -1f;
	}
}
