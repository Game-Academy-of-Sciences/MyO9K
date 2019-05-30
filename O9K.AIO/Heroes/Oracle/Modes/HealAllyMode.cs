using System;
using System.Linq;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Oracle.Units;
using O9K.AIO.Modes.KeyPress;

namespace O9K.AIO.Heroes.Oracle.Modes
{
	// Token: 0x0200004B RID: 75
	internal class HealAllyMode : KeyPressAllyMode
	{
		// Token: 0x060001AA RID: 426 RVA: 0x00003273 File Offset: 0x00001473
		public HealAllyMode(BaseHero baseHero, KeyPressModeMenu menu) : base(baseHero, menu)
		{
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000327D File Offset: 0x0000147D
		private Oracle Hero
		{
			get
			{
				if (this.hero == null)
				{
					this.hero = (base.UnitManager.ControllableUnits.FirstOrDefault((ControllableUnit x) => base.Owner.Hero.Handle == x.Handle) as Oracle);
				}
				return this.hero;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000032B4 File Offset: 0x000014B4
		protected override void ExecuteCombo()
		{
			if (this.Hero == null)
			{
				return;
			}
			if (base.TargetManager.HasValidTarget)
			{
				this.hero.HealAllyCombo(base.TargetManager);
			}
			base.UnitManager.Orbwalk(this.hero, false, true);
		}

		// Token: 0x040000F0 RID: 240
		private Oracle hero;
	}
}
