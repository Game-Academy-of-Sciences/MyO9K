using System;
using System.ComponentModel.Composition;

using System.Linq;
using Ensage;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Core;
using SharpDX;

namespace O9K.Hud.Helpers
{
	// Token: 0x020000A2 RID: 162
	[Export(typeof(IMinimap))]
	internal class Minimap : IDisposable, IMinimap, IHudModule
	{
		// Token: 0x06000393 RID: 915 RVA: 0x0001C68C File Offset: 0x0001A88C
		[ImportingConstructor]
		public Minimap(IContext9 context, IHudMenu hudMenu)
		{
			this.context = context;
			Vector2 vector=new Vector2(O9K.Core.Helpers.Hud.Info.ScreenSize.X * 0.127f, O9K.Core.Helpers.Hud.Info.ScreenSize.Y * 0.226f);
			if (Game.GetConsoleVar("dota_hud_extra_large_minimap").GetInt() == 1)
			{
				vector *= 1.145f;
			}
			Menu minimapSettingsMenu = hudMenu.MinimapSettingsMenu;
			this.debug = minimapSettingsMenu.Add<MenuSwitcher>(new MenuSwitcher("Debug", false, false));
			this.debug.SetTooltip("Use this to adjust minimap position and size");
			this.xPosition = minimapSettingsMenu.Add<MenuSlider>(new MenuSlider("X coordinate", "x", 0, 0, (int)O9K.Core.Helpers.Hud.Info.ScreenSize.X, false));
			this.yPosition = minimapSettingsMenu.Add<MenuSlider>(new MenuSlider("Y coordinate", "y", (int)(O9K.Core.Helpers.Hud.Info.ScreenSize.Y - vector.Y), 0, (int)O9K.Core.Helpers.Hud.Info.ScreenSize.Y, false));
			this.xSize = minimapSettingsMenu.Add<MenuSlider>(new MenuSlider("X Size", "xSize", (int)vector.X, 0, 600, false));
			this.ySize = minimapSettingsMenu.Add<MenuSlider>(new MenuSlider("Y Size", "ySize", (int)vector.Y, 0, 600, false));
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001C7D0 File Offset: 0x0001A9D0
		public void Activate()
		{
			this.debug.ValueChange += this.DebugOnValueChange;
			this.xPosition.ValueChange += this.XPositionOnValueChange;
			this.yPosition.ValueChange += this.YPositionOnValueChange;
			this.xSize.ValueChange += this.XSizeOnValueChange;
			this.ySize.ValueChange += this.YSizeOnValueChange;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001C850 File Offset: 0x0001AA50
		public void Dispose()
		{
			this.context.Renderer.Draw -= this.OnDraw;
			this.debug.ValueChange -= this.DebugOnValueChange;
			this.xPosition.ValueChange -= this.XPositionOnValueChange;
			this.yPosition.ValueChange -= this.YPositionOnValueChange;
			this.xSize.ValueChange -= this.XSizeOnValueChange;
			this.ySize.ValueChange -= this.YSizeOnValueChange;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001C8EC File Offset: 0x0001AAEC
		public Vector2 WorldToMinimap(Vector3 position)
		{
			float num = position.X - -7700f;
			float num2 = position.Y - -7700f;
			float num3 = num * this.minimapMapScaleX;
			float num4 = num2 * this.minimapMapScaleY;
			float num5 = this.minimap.X + num3;
			float num6 = this.minimap.Bottom - num4;
			Vector2 vector= new Vector2(num5, num6);
			if (!this.minimap.Contains(vector))
			{
				return Vector2.Zero;
			}
			return vector;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001C964 File Offset: 0x0001AB64
		public Rectangle9 WorldToMinimap(Vector3 position, float size)
		{
			float num = position.X - -7700f;
			float num2 = position.Y - -7700f;
			float num3 = num * this.minimapMapScaleX;
			float num4 = num2 * this.minimapMapScaleY;
			float num5 = this.minimap.X + num3;
			float num6 = this.minimap.Bottom - num4;
            Vector2 vector = new Vector2(num5, num6);
			if (!this.minimap.Contains(vector))
			{
				return Rectangle9.Zero;
			}
			return new Rectangle9(vector - size / 2f, size, size);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001C9EC File Offset: 0x0001ABEC
		public Rectangle9 WorldToScreen(Vector3 position, float size)
		{
			Vector2 vector = Drawing.WorldToScreen(position);
			if (vector.IsZero)
			{
				return Rectangle9.Zero;
			}
			return new Rectangle9(vector - size / 2f, size, size);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001CA24 File Offset: 0x0001AC24
		private void DebugOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				this.context.Renderer.Draw += this.OnDraw;
				return;
			}
			this.context.Renderer.Draw -= this.OnDraw;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001CA74 File Offset: 0x0001AC74
		private void OnDraw(IRenderer renderer)
		{
			try
			{
				renderer.DrawRectangle(this.minimap, System.Drawing.Color.White, 2f);
				renderer.DrawCircle(this.WorldToMinimap(Game.MousePosition), 2f, System.Drawing.Color.White, 1f);
				foreach (Unit9 unit in from x in EntityManager9.Units
				where x.IsTower
				select x)
				{
					renderer.DrawCircle(this.WorldToMinimap(unit.Position), 6f, System.Drawing.Color.White, 1f);
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

		// Token: 0x0600039B RID: 923 RVA: 0x00004494 File Offset: 0x00002694
		private void XPositionOnValueChange(object sender, SliderEventArgs e)
		{
			this.minimap.X = (float)e.NewValue;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000044A8 File Offset: 0x000026A8
		private void XSizeOnValueChange(object sender, SliderEventArgs e)
		{
			this.minimap.Width = (float)e.NewValue;
			this.minimapMapScaleX = this.minimap.Width / (Math.Abs(-7700f) + Math.Abs(7700f));
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000044E3 File Offset: 0x000026E3
		private void YPositionOnValueChange(object sender, SliderEventArgs e)
		{
			this.minimap.Y = (float)e.NewValue;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000044F7 File Offset: 0x000026F7
		private void YSizeOnValueChange(object sender, SliderEventArgs e)
		{
			this.minimap.Height = (float)e.NewValue;
			this.minimapMapScaleY = this.minimap.Height / (Math.Abs(-7700f) + Math.Abs(7700f));
		}

		// Token: 0x0400024C RID: 588
		private const float MapBottom = -7700f;

		// Token: 0x0400024D RID: 589
		private const float MapLeft = -7700f;

		// Token: 0x0400024E RID: 590
		private const float MapRight = 7700f;

		// Token: 0x0400024F RID: 591
		private const float MapTop = 7700f;

		// Token: 0x04000250 RID: 592
		private readonly IContext9 context;

		// Token: 0x04000251 RID: 593
		private readonly MenuSwitcher debug;

		// Token: 0x04000252 RID: 594
		private readonly MenuSlider xPosition;

		// Token: 0x04000253 RID: 595
		private readonly MenuSlider xSize;

		// Token: 0x04000254 RID: 596
		private readonly MenuSlider yPosition;

		// Token: 0x04000255 RID: 597
		private readonly MenuSlider ySize;

		// Token: 0x04000256 RID: 598
		private RectangleF minimap;

		// Token: 0x04000257 RID: 599
		private float minimapMapScaleX;

		// Token: 0x04000258 RID: 600
		private float minimapMapScaleY;
	}
}
