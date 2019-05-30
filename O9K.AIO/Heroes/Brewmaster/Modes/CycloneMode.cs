using System;
using System.Linq;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Brewmaster.Units;
using O9K.AIO.Modes.KeyPress;

namespace O9K.AIO.Heroes.Brewmaster.Modes
{
	// Token: 0x020001D9 RID: 473
	internal class CycloneMode : KeyPressMode
	{
		// Token: 0x0600097C RID: 2428 RVA: 0x00002900 File Offset: 0x00000B00
		public CycloneMode(BaseHero baseHero, KeyPressModeMenu menu) : base(baseHero, menu)
		{
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00029A10 File Offset: 0x00027C10
		protected override void ExecuteCombo()
		{
			if (!base.TargetManager.HasValidTarget)
			{
				return;
			}
			Storm storm = (Storm)base.UnitManager.ControllableUnits.FirstOrDefault((ControllableUnit x) => x is Storm);
			if (storm == null)
			{
				return;
			}
			storm.CycloneTarget(base.TargetManager);
			base.UnitManager.Orbwalk(storm, false, true);
		}
	}
}
