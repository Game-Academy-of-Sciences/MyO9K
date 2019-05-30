using System;
using System.Linq;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Magnus.Units;
using O9K.AIO.Modes.KeyPress;

namespace O9K.AIO.Heroes.Magnus.Modes
{
	// Token: 0x0200010E RID: 270
	internal class BlinkSkewerMode : KeyPressMode
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x00004C98 File Offset: 0x00002E98
		public BlinkSkewerMode(BaseHero baseHero, BlinkSkewerModeMenu menu) : base(baseHero, menu)
		{
			this.menu = menu;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00004CA9 File Offset: 0x00002EA9
		private Magnus Hero
		{
			get
			{
				if (this.hero == null)
				{
					this.hero = (base.UnitManager.ControllableUnits.FirstOrDefault((ControllableUnit x) => base.Owner.Hero.Handle == x.Handle) as Magnus);
				}
				return this.hero;
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001B7E8 File Offset: 0x000199E8
		protected override void ExecuteCombo()
		{
			if (this.Hero == null)
			{
				return;
			}
			if (base.TargetManager.HasValidTarget)
			{
				this.hero.BlinkSkewerCombo(base.TargetManager, this.menu);
			}
			base.UnitManager.Orbwalk(this.hero, false, true);
		}

		// Token: 0x040002FE RID: 766
		private readonly BlinkSkewerModeMenu menu;

		// Token: 0x040002FF RID: 767
		private Magnus hero;
	}
}
