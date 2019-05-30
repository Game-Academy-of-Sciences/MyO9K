using System;
using Ensage;
using SharpDX;

namespace O9K.Core.Managers.Input.EventArgs
{
	// Token: 0x02000067 RID: 103
	public sealed class MouseWheelEventArgs : EventArgs
	{
		// Token: 0x06000338 RID: 824 RVA: 0x000040FC File Offset: 0x000022FC
		public MouseWheelEventArgs(WndEventArgs args)
		{
			this.Position = Game.MouseScreenPosition;
			this.Up = ((short)(args.WParam >> 16 & 65535UL) > 0);
			this.Process = args.Process;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00004135 File Offset: 0x00002335
		public Vector2 Position { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000413D File Offset: 0x0000233D
		public bool Up { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00004145 File Offset: 0x00002345
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000414D File Offset: 0x0000234D
		public bool Process { get; set; }
	}
}
