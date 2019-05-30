using System;
using System.Linq;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.EarthSpirit.Units;
using O9K.AIO.Modes.KeyPress;

namespace O9K.AIO.Heroes.EarthSpirit.Modes
{
	// Token: 0x0200018B RID: 395
	internal class RollSmashMode : KeyPressMode
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x0000603E File Offset: 0x0000423E
		public RollSmashMode(BaseHero baseHero, RollSmashModeMenu menu) : base(baseHero, menu)
		{
			this.menu = menu;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0000604F File Offset: 0x0000424F
		private EarthSpirit Hero
		{
			get
			{
				if (this.hero == null)
				{
					this.hero = (base.UnitManager.ControllableUnits.FirstOrDefault((ControllableUnit x) => base.Owner.Hero.Handle == x.Handle) as EarthSpirit);
				}
				return this.hero;
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00024EC4 File Offset: 0x000230C4
		protected override void ExecuteCombo()
		{
			if (this.Hero == null)
			{
				return;
			}
			if (base.TargetManager.HasValidTarget)
			{
				this.hero.RollSmashCombo(base.TargetManager, this.menu);
			}
			base.UnitManager.Orbwalk(this.hero, false, true);
		}

		// Token: 0x04000472 RID: 1138
		private readonly RollSmashModeMenu menu;

		// Token: 0x04000473 RID: 1139
		private EarthSpirit hero;
	}
}
