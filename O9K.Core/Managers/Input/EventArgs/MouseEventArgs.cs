using System;
using Ensage;
using O9K.Core.Managers.Input.Keys;
using SharpDX;

namespace O9K.Core.Managers.Input.EventArgs
{
	// Token: 0x02000065 RID: 101
	public sealed class MouseEventArgs : EventArgs
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0001A910 File Offset: 0x00018B10
		public MouseEventArgs(WndEventArgs args)
		{
			this.ScreenPosition = Game.MouseScreenPosition;
			this.GamePosition = Game.MousePosition;
			this.Process = args.Process;
			switch (args.Msg)
			{
			case 513u:
			case 514u:
				this.MouseKey = 1;
				return;
			case 515u:
			case 518u:
			case 521u:
			case 522u:
				break;
			case 516u:
			case 517u:
				this.MouseKey = 2;
				return;
			case 519u:
			case 520u:
				this.MouseKey = 3;
				break;
			case 523u:
			case 524u:
				this.MouseKey = ((args.WParam >> 16 == 1UL) ? 4 : 5);
				return;
			default:
				return;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00004088 File Offset: 0x00002288
		public MouseKey MouseKey { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00004090 File Offset: 0x00002290
		public Vector3 GamePosition { get; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00004098 File Offset: 0x00002298
		public Vector2 ScreenPosition { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000331 RID: 817 RVA: 0x000040A0 File Offset: 0x000022A0
		// (set) Token: 0x06000332 RID: 818 RVA: 0x000040A8 File Offset: 0x000022A8
		public bool Process { get; set; }
	}
}
