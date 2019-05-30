using System;
using System.ComponentModel.Composition;
using Ensage;
using O9K.Core.Logger;
using O9K.Core.Managers.Input.EventArgs;
using O9K.Core.Managers.Input.Keys;

namespace O9K.Core.Managers.Input
{
	// Token: 0x02000062 RID: 98
	[Export(typeof(IInputManager9))]
	public sealed class InputManager9 : IInputManager9
	{
		// Token: 0x06000315 RID: 789 RVA: 0x00004030 File Offset: 0x00002230
		[ImportingConstructor]
		public InputManager9()
		{
			Game.OnWndProc += this.OnWndProc;
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000316 RID: 790 RVA: 0x0001A350 File Offset: 0x00018550
		// (remove) Token: 0x06000317 RID: 791 RVA: 0x0001A388 File Offset: 0x00018588
		public event EventHandler<KeyEventArgs> KeyDown;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000318 RID: 792 RVA: 0x0001A3C0 File Offset: 0x000185C0
		// (remove) Token: 0x06000319 RID: 793 RVA: 0x0001A3F8 File Offset: 0x000185F8
		public event EventHandler<KeyEventArgs> KeyUp;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600031A RID: 794 RVA: 0x0001A430 File Offset: 0x00018630
		// (remove) Token: 0x0600031B RID: 795 RVA: 0x0001A468 File Offset: 0x00018668
		public event EventHandler<MouseEventArgs> MouseKeyDown;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600031C RID: 796 RVA: 0x0001A4A0 File Offset: 0x000186A0
		// (remove) Token: 0x0600031D RID: 797 RVA: 0x0001A4D8 File Offset: 0x000186D8
		public event EventHandler<MouseEventArgs> MouseKeyUp;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x0600031E RID: 798 RVA: 0x0001A510 File Offset: 0x00018710
		// (remove) Token: 0x0600031F RID: 799 RVA: 0x0001A548 File Offset: 0x00018748
		public event EventHandler<MouseMoveEventArgs> MouseMove;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000320 RID: 800 RVA: 0x0001A580 File Offset: 0x00018780
		// (remove) Token: 0x06000321 RID: 801 RVA: 0x0001A5B8 File Offset: 0x000187B8
		public event EventHandler<MouseWheelEventArgs> MouseWheel;

		// Token: 0x06000322 RID: 802 RVA: 0x0001A5F0 File Offset: 0x000187F0
		private void FireKeyDown(WndEventArgs args)
		{
			if (this.KeyDown == null || Game.IsChatOpen)
			{
				return;
			}
			if (args.LParam >> 30 == 1L)
			{
				return;
			}
			KeyEventArgs keyEventArgs = new KeyEventArgs(args);
			try
			{
				this.KeyDown(this, keyEventArgs);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
			args.Process = (args.Process && keyEventArgs.Process);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001A660 File Offset: 0x00018860
		private void FireKeyUp(WndEventArgs args)
		{
			if (this.KeyUp == null || Game.IsChatOpen)
			{
				return;
			}
			KeyEventArgs keyEventArgs = new KeyEventArgs(args);
			try
			{
				this.KeyUp(this, keyEventArgs);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
			args.Process = (args.Process && keyEventArgs.Process);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001A6C4 File Offset: 0x000188C4
		private void FireMouseDown(WndEventArgs args)
		{
			if (this.MouseKeyDown == null)
			{
				return;
			}
			MouseEventArgs mouseEventArgs = new MouseEventArgs(args);
			if (mouseEventArgs.MouseKey == MouseKey.None)
			{
				return;
			}
			try
			{
				this.MouseKeyDown(this, mouseEventArgs);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
			args.Process = (args.Process && mouseEventArgs.Process);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001A72C File Offset: 0x0001892C
		private void FireMouseMove(WndEventArgs args)
		{
			if (this.MouseMove == null)
			{
				return;
			}
			if (this.lastMousePosition == args.LParam)
			{
				return;
			}
			this.lastMousePosition = args.LParam;
			MouseMoveEventArgs mouseMoveEventArgs = new MouseMoveEventArgs(args);
			try
			{
				this.MouseMove(this, mouseMoveEventArgs);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
			args.Process = (args.Process && mouseMoveEventArgs.Process);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0001A7A4 File Offset: 0x000189A4
		private void FireMouseUp(WndEventArgs args)
		{
			if (this.MouseKeyUp == null)
			{
				return;
			}
			MouseEventArgs mouseEventArgs = new MouseEventArgs(args);
			if (mouseEventArgs.MouseKey == MouseKey.None)
			{
				return;
			}
			try
			{
				this.MouseKeyUp(this, mouseEventArgs);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
			args.Process = (args.Process && mouseEventArgs.Process);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001A80C File Offset: 0x00018A0C
		private void FireMouseWheel(WndEventArgs args)
		{
			if (this.MouseWheel == null)
			{
				return;
			}
			MouseWheelEventArgs mouseWheelEventArgs = new MouseWheelEventArgs(args);
			try
			{
				this.MouseWheel(this, mouseWheelEventArgs);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
			args.Process = (args.Process && mouseWheelEventArgs.Process);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001A868 File Offset: 0x00018A68
		private void OnWndProc(WndEventArgs args)
		{
			uint msg = args.Msg;
			switch (msg)
			{
			case 256u:
			case 260u:
				this.FireKeyDown(args);
				return;
			case 257u:
			case 261u:
				this.FireKeyUp(args);
				return;
			case 258u:
			case 259u:
				break;
			default:
				switch (msg)
				{
				case 512u:
					this.FireMouseMove(args);
					return;
				case 513u:
				case 516u:
				case 519u:
				case 523u:
					this.FireMouseDown(args);
					return;
				case 514u:
				case 517u:
				case 520u:
				case 524u:
					this.FireMouseUp(args);
					return;
				case 515u:
				case 518u:
				case 521u:
					break;
				case 522u:
					this.FireMouseWheel(args);
					break;
				default:
					return;
				}
				break;
			}
		}

		// Token: 0x04000149 RID: 329
		public const uint WM_KEYDOWN = 256u;

		// Token: 0x0400014A RID: 330
		public const uint WM_KEYUP = 257u;

		// Token: 0x0400014B RID: 331
		public const uint WM_LBUTTONDOWN = 513u;

		// Token: 0x0400014C RID: 332
		public const uint WM_LBUTTONUP = 514u;

		// Token: 0x0400014D RID: 333
		public const uint WM_MBUTTONDOWN = 519u;

		// Token: 0x0400014E RID: 334
		public const uint WM_MBUTTONUP = 520u;

		// Token: 0x0400014F RID: 335
		public const uint WM_MOUSEMOVE = 512u;

		// Token: 0x04000150 RID: 336
		public const uint WM_MOUSEWHEEL = 522u;

		// Token: 0x04000151 RID: 337
		public const uint WM_RBUTTONDOWN = 516u;

		// Token: 0x04000152 RID: 338
		public const uint WM_RBUTTONUP = 517u;

		// Token: 0x04000153 RID: 339
		public const uint WM_SYSKEYDOWN = 260u;

		// Token: 0x04000154 RID: 340
		public const uint WM_SYSKEYUP = 261u;

		// Token: 0x04000155 RID: 341
		public const uint WM_XBUTTONDOWN = 523u;

		// Token: 0x04000156 RID: 342
		public const uint WM_XBUTTONUP = 524u;

		// Token: 0x04000157 RID: 343
		private long lastMousePosition;
	}
}
