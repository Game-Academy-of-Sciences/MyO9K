using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using EnsageSharp.Sandbox;
using O9K.Core.Logger;
using O9K.Core.Managers.Input;
using O9K.Core.Managers.Input.EventArgs;
using O9K.Core.Managers.Input.Keys;
using O9K.Core.Managers.Menu.Items;
using O9K.Core.Managers.Renderer;

namespace O9K.Core.Managers.Menu
{
	// Token: 0x02000042 RID: 66
	[Export(typeof(IMenuManager9))]
	public sealed class MenuManager9 : IDisposable, IMenuManager9
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00014F78 File Offset: 0x00013178
		[ImportingConstructor]
		public MenuManager9(IInputManager9 inputManager, IRendererManager9 renderer)
		{
			this.inputManager = inputManager;
			this.renderer = renderer;
			this.mainMenu = new MainMenu(renderer, inputManager);
			int virtualKey;
			if (SandboxConfig.Config.HotKeys.TryGetValue("Menu", out virtualKey))
			{
				this.menuHoldKey = KeyInterop.KeyFromVirtualKey(virtualKey);
			}
			if (SandboxConfig.Config.HotKeys.TryGetValue("MenuToggle", out virtualKey))
			{
				this.menuToggleKey = KeyInterop.KeyFromVirtualKey(virtualKey);
			}
			inputManager.KeyDown += this.OnKeyDown;
			AppDomain.CurrentDomain.DomainUnload += this.OnDomainUnload;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00015018 File Offset: 0x00013218
		public void AddRootMenu(Menu menu)
		{
			try
			{
				this.mainMenu.Add<Menu>(menu);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0001504C File Offset: 0x0001324C
		public void Dispose()
		{
			this.inputManager.MouseKeyDown -= this.OnMouseKeyDown;
			this.inputManager.MouseKeyUp -= this.OnMouseKeyUp;
			this.inputManager.MouseMove -= this.OnMouseMove;
			this.inputManager.MouseWheel -= this.OnMouseWheel;
			this.inputManager.KeyDown -= this.OnKeyDown;
			this.inputManager.KeyUp -= this.OnKeyUp;
			this.renderer.Draw -= this.OnDraw;
			AppDomain.CurrentDomain.DomainUnload -= this.OnDomainUnload;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00015110 File Offset: 0x00013310
		public void RemoveRootMenu(Menu menu)
		{
			try
			{
				this.mainMenu.Remove(menu);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00015144 File Offset: 0x00013344
		private void OnDomainUnload(object sender, EventArgs e)
		{
			try
			{
				this.mainMenu.Save();
				this.renderer.Dispose();
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00015184 File Offset: 0x00013384
		private void OnDraw(IRenderer args)
		{
			try
			{
				this.mainMenu.DrawMenu();
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000151B8 File Offset: 0x000133B8
		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key != this.menuToggleKey)
			{
				if (!this.menuVisible && e.Key == this.menuHoldKey)
				{
					this.inputManager.MouseKeyDown += this.OnMouseKeyDown;
					this.inputManager.MouseKeyUp += this.OnMouseKeyUp;
					this.inputManager.MouseMove += this.OnMouseMove;
					this.inputManager.MouseWheel += this.OnMouseWheel;
					this.inputManager.KeyUp += this.OnKeyUp;
					this.renderer.Draw += this.OnDraw;
					this.menuVisible = true;
				}
				return;
			}
			if (this.menuVisible)
			{
				this.inputManager.MouseKeyDown -= this.OnMouseKeyDown;
				this.inputManager.MouseKeyUp -= this.OnMouseKeyUp;
				this.inputManager.MouseMove -= this.OnMouseMove;
				this.inputManager.MouseWheel -= this.OnMouseWheel;
				this.inputManager.KeyUp -= this.OnKeyUp;
				this.renderer.Draw -= this.OnDraw;
				this.menuVisible = false;
				return;
			}
			this.inputManager.MouseKeyDown += this.OnMouseKeyDown;
			this.inputManager.MouseKeyUp += this.OnMouseKeyUp;
			this.inputManager.MouseMove += this.OnMouseMove;
			this.inputManager.MouseWheel += this.OnMouseWheel;
			this.inputManager.KeyUp += this.OnKeyUp;
			this.renderer.Draw += this.OnDraw;
			this.menuVisible = true;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000153B4 File Offset: 0x000135B4
		private void OnKeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key != this.menuHoldKey)
			{
				return;
			}
			this.inputManager.MouseKeyDown -= this.OnMouseKeyDown;
			this.inputManager.MouseKeyUp -= this.OnMouseKeyUp;
			this.inputManager.MouseMove -= this.OnMouseMove;
			this.inputManager.MouseWheel -= this.OnMouseWheel;
			this.inputManager.KeyUp -= this.OnKeyUp;
			this.renderer.Draw -= this.OnDraw;
			this.menuVisible = false;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000032E0 File Offset: 0x000014E0
		private void OnMouseKeyDown(object sender, MouseEventArgs e)
		{
			if (e.MouseKey != MouseKey.Left)
			{
				return;
			}
			if (this.mainMenu.OnMousePress(e.ScreenPosition))
			{
				e.Process = false;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00003306 File Offset: 0x00001506
		private void OnMouseKeyUp(object sender, MouseEventArgs e)
		{
			if (e.MouseKey != MouseKey.Left)
			{
				return;
			}
			if (this.mainMenu.OnMouseRelease(e.ScreenPosition))
			{
				e.Process = false;
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00015464 File Offset: 0x00013664
		private void OnMouseMove(object sender, MouseMoveEventArgs e)
		{
			try
			{
				if (this.mainMenu.OnMouseMove(e.ScreenPosition))
				{
					e.Process = false;
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000154A8 File Offset: 0x000136A8
		private void OnMouseWheel(object sender, MouseWheelEventArgs e)
		{
			try
			{
				if (this.mainMenu.OnMouseWheel(e.Position, e.Up))
				{
					e.Process = false;
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x040000BA RID: 186
		private readonly IInputManager9 inputManager;

		// Token: 0x040000BB RID: 187
		private readonly MainMenu mainMenu;

		// Token: 0x040000BC RID: 188
		private readonly Key menuHoldKey;

		// Token: 0x040000BD RID: 189
		private readonly Key menuToggleKey;

		// Token: 0x040000BE RID: 190
		private readonly IRendererManager9 renderer;

		// Token: 0x040000BF RID: 191
		private bool menuVisible;
	}
}
