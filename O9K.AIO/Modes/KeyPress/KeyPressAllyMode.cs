using System;
using O9K.AIO.Heroes.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Menu.EventArgs;

namespace O9K.AIO.Modes.KeyPress
{
	// Token: 0x02000023 RID: 35
	internal abstract class KeyPressAllyMode : KeyPressMode
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00002900 File Offset: 0x00000B00
		protected KeyPressAllyMode(BaseHero baseHero, KeyPressModeMenu menu) : base(baseHero, menu)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000A0A4 File Offset: 0x000082A4
		protected override void KeyOnValueChanged(object sender, KeyEventArgs e)
		{
			if (e.NewValue)
			{
				if (base.LockTarget)
				{
					Unit9 target = base.TargetManager.ClosestAllyHeroToMouse(base.Owner, true);
					base.TargetManager.ForceSetTarget(target);
					base.TargetManager.TargetLocked = true;
				}
				base.UpdateHandler.IsEnabled = true;
				return;
			}
			base.UpdateHandler.IsEnabled = false;
			if (base.LockTarget)
			{
				base.TargetManager.TargetLocked = false;
			}
		}
	}
}
