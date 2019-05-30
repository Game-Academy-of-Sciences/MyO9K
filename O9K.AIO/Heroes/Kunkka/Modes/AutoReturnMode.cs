using System;
using System.Linq;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Kunkka.Units;
using O9K.AIO.Modes.Permanent;

namespace O9K.AIO.Heroes.Kunkka.Modes
{
	// Token: 0x0200012F RID: 303
	internal class AutoReturnMode : PermanentMode
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x000051EA File Offset: 0x000033EA
		public AutoReturnMode(BaseHero baseHero, PermanentModeMenu menu) : base(baseHero, menu)
		{
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0001DFD8 File Offset: 0x0001C1D8
		private Kunkka Hero
		{
			get
			{
				Kunkka kunkka = this.hero;
				if (kunkka == null || !kunkka.IsValid)
				{
					this.hero = (base.UnitManager.ControllableUnits.FirstOrDefault((ControllableUnit x) => base.Owner.Hero.Handle == x.Handle) as Kunkka);
				}
				return this.hero;
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000051F4 File Offset: 0x000033F4
		protected override void Execute()
		{
			if (this.Hero == null || !this.hero.Owner.IsAlive)
			{
				return;
			}
			this.hero.AutoReturn();
		}

		// Token: 0x04000356 RID: 854
		private Kunkka hero;
	}
}
