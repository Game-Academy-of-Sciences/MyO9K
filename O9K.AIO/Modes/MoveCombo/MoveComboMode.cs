using System;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.KeyPress;

namespace O9K.AIO.Modes.MoveCombo
{
	// Token: 0x02000021 RID: 33
	internal class MoveComboMode : KeyPressMode
	{
		// Token: 0x060000BA RID: 186 RVA: 0x000028A4 File Offset: 0x00000AA4
		public MoveComboMode(BaseHero baseHero, MoveComboModeMenu menu) : base(baseHero, menu)
		{
			this.menu = menu;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000028B5 File Offset: 0x00000AB5
		protected override void ExecuteCombo()
		{
			base.UnitManager.ExecuteMoveCombo(this.menu);
			base.UnitManager.Move();
		}

		// Token: 0x04000069 RID: 105
		private readonly MoveComboModeMenu menu;
	}
}
