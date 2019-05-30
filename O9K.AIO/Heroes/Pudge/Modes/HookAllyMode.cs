using System;
using System.Linq;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Pudge.Units;
using O9K.AIO.Modes.KeyPress;

namespace O9K.AIO.Heroes.Pudge.Modes
{
	// Token: 0x020000CC RID: 204
	internal class HookAllyMode : KeyPressAllyMode
	{
		// Token: 0x0600042A RID: 1066 RVA: 0x00003273 File Offset: 0x00001473
		public HookAllyMode(BaseHero baseHero, KeyPressModeMenu menu) : base(baseHero, menu)
		{
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x000042E4 File Offset: 0x000024E4
		private Pudge Hero
		{
			get
			{
				if (this.hero == null)
				{
					this.hero = (base.UnitManager.ControllableUnits.FirstOrDefault((ControllableUnit x) => base.Owner.Hero.Handle == x.Handle) as Pudge);
				}
				return this.hero;
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000431B File Offset: 0x0000251B
		protected override void ExecuteCombo()
		{
			if (this.Hero == null)
			{
				return;
			}
			if (base.TargetManager.HasValidTarget)
			{
				this.hero.HookAlly(base.TargetManager);
			}
			base.UnitManager.Orbwalk(this.hero, false, true);
		}

		// Token: 0x04000259 RID: 601
		private Pudge hero;
	}
}
