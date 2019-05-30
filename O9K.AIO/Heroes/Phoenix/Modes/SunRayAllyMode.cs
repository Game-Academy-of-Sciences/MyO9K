using System;
using System.Linq;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Phoenix.Units;
using O9K.AIO.Modes.KeyPress;

namespace O9K.AIO.Heroes.Phoenix.Modes
{
	// Token: 0x020000DD RID: 221
	internal class SunRayAllyMode : KeyPressAllyMode
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x00003273 File Offset: 0x00001473
		public SunRayAllyMode(BaseHero baseHero, KeyPressModeMenu menu) : base(baseHero, menu)
		{
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x000045F8 File Offset: 0x000027F8
		private Phoenix Hero
		{
			get
			{
				if (this.hero == null)
				{
					this.hero = (base.UnitManager.ControllableUnits.FirstOrDefault((ControllableUnit x) => base.Owner.Hero.Handle == x.Handle) as Phoenix);
				}
				return this.hero;
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000462F File Offset: 0x0000282F
		protected override void ExecuteCombo()
		{
			if (this.Hero == null)
			{
				return;
			}
			if (base.TargetManager.HasValidTarget)
			{
				this.hero.SunRayAllyCombo(base.TargetManager);
			}
			base.UnitManager.Orbwalk(this.hero, false, true);
		}

		// Token: 0x0400027A RID: 634
		private Phoenix hero;
	}
}
