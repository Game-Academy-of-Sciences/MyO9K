using System;
using Ensage;
using SharpDX;

namespace O9K.Core.Managers.Input.EventArgs
{
	// Token: 0x02000066 RID: 102
	public sealed class MouseMoveEventArgs : EventArgs
	{
		// Token: 0x06000333 RID: 819 RVA: 0x000040B1 File Offset: 0x000022B1
		public MouseMoveEventArgs(WndEventArgs args)
		{
			this.ScreenPosition = Game.MouseScreenPosition;
			this.GamePosition = Game.MousePosition;
			this.Process = args.Process;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000334 RID: 820 RVA: 0x000040DB File Offset: 0x000022DB
		public Vector2 ScreenPosition { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000335 RID: 821 RVA: 0x000040E3 File Offset: 0x000022E3
		public Vector3 GamePosition { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000336 RID: 822 RVA: 0x000040EB File Offset: 0x000022EB
		// (set) Token: 0x06000337 RID: 823 RVA: 0x000040F3 File Offset: 0x000022F3
		public bool Process { get; set; }
	}
}
