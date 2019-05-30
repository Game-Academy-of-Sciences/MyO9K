using System;
using System.Linq;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Tiny.Units;
using O9K.AIO.Modes.KeyPress;

namespace O9K.AIO.Heroes.Tiny.Modes
{
	// Token: 0x02000070 RID: 112
	internal class TossUnderTowerMode : KeyPressMode
	{
		// Token: 0x06000259 RID: 601 RVA: 0x00002900 File Offset: 0x00000B00
		public TossUnderTowerMode(BaseHero baseHero, KeyPressModeMenu menu) : base(baseHero, menu)
		{
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600025A RID: 602 RVA: 0x000037D6 File Offset: 0x000019D6
		private Tiny Hero
		{
			get
			{
				if (this.hero == null)
				{
					this.hero = (base.UnitManager.ControllableUnits.FirstOrDefault((ControllableUnit x) => base.Owner.Hero.Handle == x.Handle) as Tiny);
				}
				return this.hero;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000380D File Offset: 0x00001A0D
		protected override void ExecuteCombo()
		{
			if (this.Hero == null)
			{
				return;
			}
			if (!this.hero.Owner.IsAlive)
			{
				return;
			}
			this.hero.Toss();
		}

		// Token: 0x04000143 RID: 323
		private Tiny hero;
	}
}
