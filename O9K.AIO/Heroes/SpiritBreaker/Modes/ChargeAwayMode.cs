using System;
using System.Linq;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.SpiritBreaker.Units;
using O9K.AIO.Modes.KeyPress;

namespace O9K.AIO.Heroes.SpiritBreaker.Modes
{
	// Token: 0x02000047 RID: 71
	internal class ChargeAwayMode : KeyPressMode
	{
		// Token: 0x06000196 RID: 406 RVA: 0x00003183 File Offset: 0x00001383
		public ChargeAwayMode(BaseHero baseHero, KeyPressModeMenu menu) : base(baseHero, menu)
		{
			base.LockTarget = false;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00003194 File Offset: 0x00001394
		private SpiritBreaker Hero
		{
			get
			{
				if (this.hero == null)
				{
					this.hero = (base.UnitManager.ControllableUnits.FirstOrDefault((ControllableUnit x) => base.Owner.Hero.Handle == x.Handle) as SpiritBreaker);
				}
				return this.hero;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000031CB File Offset: 0x000013CB
		protected override void ExecuteCombo()
		{
			if (this.Hero == null)
			{
				return;
			}
			this.hero.ChargeAway(base.TargetManager);
			this.hero.Orbwalk(base.TargetManager.Target, true, true, null);
		}

		// Token: 0x040000E7 RID: 231
		private SpiritBreaker hero;
	}
}
