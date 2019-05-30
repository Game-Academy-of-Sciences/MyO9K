using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using Ensage.SDK.Handlers;
using Ensage.SDK.Helpers;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Input;
using O9K.Core.Managers.Input.EventArgs;
using O9K.Core.Managers.Input.Keys;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Core;
using O9K.Hud.Helpers.Notificator.Notifications;
using SharpDX;

namespace O9K.Hud.Helpers.Notificator
{
	// Token: 0x020000A8 RID: 168
	[Export(typeof(INotificator))]
	internal class Notificator : IDisposable, INotificator, IHudModule
	{
		// Token: 0x060003BA RID: 954 RVA: 0x0001D36C File Offset: 0x0001B56C
		[ImportingConstructor]
		public Notificator(IContext9 context, IInputManager9 inputManager, IHudMenu hudMenu)
		{
			this.context = context;
			this.inputManager = inputManager;
			Menu notificationsSettingsMenu = hudMenu.NotificationsSettingsMenu;
			this.debug = notificationsSettingsMenu.Add<MenuSwitcher>(new MenuSwitcher("Debug", false, false));
			this.debug.SetTooltip("Use this to adjust side notification messages");
			this.size = notificationsSettingsMenu.Add<MenuSlider>(new MenuSlider("Size", "size", 65, 50, 100, false));
			this.position = notificationsSettingsMenu.Add<MenuSlider>(new MenuSlider("Position", "position", (int) (O9K.Core.Helpers.Hud.Info.ScreenSize.Y * 0.7f), 0, (int)O9K.Core.Helpers.Hud.Info.ScreenSize.Y, false));
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001D430 File Offset: 0x0001B630
		public void Activate()
		{
			this.LoadTextures();
			this.updateHandler = UpdateManager.Subscribe(new Action(this.OnUpdate), 300, false);
			this.debug.ValueChange += this.DebugOnValueChange;
			this.size.ValueChange += this.SizeOnValueChange;
			this.position.ValueChange += this.PositionOnValueChange;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001D4A8 File Offset: 0x0001B6A8
		public void Dispose()
		{
			this.debug.ValueChange -= this.DebugOnValueChange;
			this.context.Renderer.Draw -= this.OnDebugDraw;
			this.context.Renderer.Draw -= this.OnDraw;
			this.inputManager.MouseKeyDown -= this.OnMouseKeyDown;
			this.size.ValueChange -= this.SizeOnValueChange;
			this.position.ValueChange -= this.PositionOnValueChange;
			this.updateHandler.IsEnabled = false;
			this.queue.Clear();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001D560 File Offset: 0x0001B760
		public void PushNotification(Notification notification)
		{
			this.queue.Enqueue(notification);
			if (!this.updateHandler.IsEnabled)
			{
				this.updateHandler.IsEnabled = true;
				this.context.Renderer.Draw += this.OnDraw;
				this.inputManager.MouseKeyDown += this.OnMouseKeyDown;
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001D5C8 File Offset: 0x0001B7C8
		private void DebugOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				this.context.Renderer.Draw += this.OnDebugDraw;
				return;
			}
			this.context.Renderer.Draw -= this.OnDebugDraw;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001D618 File Offset: 0x0001B818
		private void LoadTextures()
		{
			ITextureManager textureManager = this.context.Renderer.TextureManager;
			textureManager.LoadFromDota("notification_bg", "panorama\\images\\hud\\reborn\\bg_deathsummary_psd.vtex_c", 300, 200, false, 10, null);
			textureManager.LoadFromDota("gold", "panorama\\images\\hud\\reborn\\gold_large_png.vtex_c", 0, 0, false, 0, null);
			textureManager.LoadFromDota("ping", "panorama\\images\\hud\\reborn\\ping_icon_default_psd.vtex_c", 0, 0, false, 0, null);
			textureManager.LoadFromResource("rune_regen", "rune_regen.png");
			textureManager.LoadFromResource("rune_bounty", "rune_bounty.png");
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001D6B4 File Offset: 0x0001B8B4
		private void OnDebugDraw(IRenderer renderer)
		{
			try
			{
				Rectangle9 rec = this.panel;
				for (int i = 0; i < 3; i++)
				{
					renderer.DrawRectangle(rec, System.Drawing.Color.White, 1f);
					rec += new Vector2(0f, -(rec.Height + 20f));
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001D724 File Offset: 0x0001B924
		private void OnDraw(IRenderer renderer)
		{
			try
			{
				Rectangle9 rec = this.panel;
				foreach (Notification notification in this.notifications)
				{
					notification.Draw(renderer, rec);
					rec += new Vector2(0f, -(rec.Height + 20f));
				}
			}
			catch (InvalidOperationException)
			{
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001D7C8 File Offset: 0x0001B9C8
		private void OnMouseKeyDown(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.MouseKey == MouseKey.Left)
				{
					Rectangle9 rec = this.panel;
					foreach (Notification notification in this.notifications)
					{
						if (rec.Contains(e.ScreenPosition))
						{
							if (notification.OnClick())
							{
								e.Process = false;
							}
							break;
						}
						rec += new Vector2(0f, -(rec.Height + 20f));
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001D87C File Offset: 0x0001BA7C
		private void OnUpdate()
		{
			try
			{
				this.notifications.RemoveAll((Notification x) => x.IsExpired);
				int num = Math.Min(this.queue.Count, 3 - this.notifications.Count);
				for (int i = 0; i < num; i++)
				{
					Notification notification = this.queue.Dequeue();
					notification.Pushed();
					this.notifications.Insert(0, notification);
				}
				if (this.notifications.Count == 0)
				{
					this.updateHandler.IsEnabled = false;
					this.context.Renderer.Draw -= this.OnDraw;
					this.inputManager.MouseKeyDown -= this.OnMouseKeyDown;
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000045F5 File Offset: 0x000027F5
		private void PositionOnValueChange(object sender, SliderEventArgs e)
		{
			this.panel.Location = new Vector2 (O9K.Core.Helpers.Hud.Info.ScreenSize.X - this.panel.Width, (float)e.NewValue);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001D964 File Offset: 0x0001BB64
		private void SizeOnValueChange(object sender, SliderEventArgs e)
		{
			this.panel.Size = new Size2F((float)e.NewValue * 3.5f, (float)e.NewValue);
			this.panel.X = O9K.Core.Helpers.Hud.Info.ScreenSize.X - this.panel.Width;
		}

		// Token: 0x04000268 RID: 616
		private const int MaxNotifications = 3;

		// Token: 0x04000269 RID: 617
		private readonly IContext9 context;

		// Token: 0x0400026A RID: 618
		private readonly MenuSwitcher debug;

		// Token: 0x0400026B RID: 619
		private readonly IInputManager9 inputManager;

		// Token: 0x0400026C RID: 620
		private readonly List<Notification> notifications = new List<Notification>();

		// Token: 0x0400026D RID: 621
		private readonly MenuSlider position;

		// Token: 0x0400026E RID: 622
		private readonly Queue<Notification> queue = new Queue<Notification>();

		// Token: 0x0400026F RID: 623
		private readonly MenuSlider size;

		// Token: 0x04000270 RID: 624
		private Rectangle9 panel;

		// Token: 0x04000271 RID: 625
		private IUpdateHandler updateHandler;
	}
}
