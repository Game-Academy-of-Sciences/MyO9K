using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Heroes
{
	// Token: 0x020000C7 RID: 199
	public sealed class Owner
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x00020D88 File Offset: 0x0001EF88
		public Owner()
		{
			this.Player = ObjectManager.LocalPlayer;
			this.PlayerHandle = this.Player.Handle;
			this.Team = this.Player.Team;
			this.EnemyTeam = ((this.Team == Team.Radiant) ? 3 : 2);
			this.HeroId = this.Player.SelectedHeroId;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00006002 File Offset: 0x00004202
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x0000600A File Offset: 0x0000420A
		public Hero9 Hero { get; private set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00006013 File Offset: 0x00004213
		public HeroId HeroId { get; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0000601B File Offset: 0x0000421B
		public uint PlayerHandle { get; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00006023 File Offset: 0x00004223
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x0000602B File Offset: 0x0000422B
		public uint HeroHandle { get; private set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00006034 File Offset: 0x00004234
		public Player Player { get; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000603C File Offset: 0x0000423C
		public Team Team { get; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00006044 File Offset: 0x00004244
		public Team EnemyTeam { get; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00020DF4 File Offset: 0x0001EFF4
		public IEnumerable<Unit9> SelectedUnits
		{
			get
			{
				return from x in this.Player.Selection
				select EntityManager9.GetUnitFast(x.Handle) into x
				where x != null
				select x;
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0000604C File Offset: 0x0000424C
		public static implicit operator Hero9(Owner owner)
		{
			return owner.Hero;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00006054 File Offset: 0x00004254
		internal void SetHero(Unit9 myHero)
		{
			this.Hero = (Hero9)myHero;
			this.Hero.IsMyHero = true;
			this.HeroHandle = this.Hero.Handle;
		}
	}
}
