using System;
using O9K.Core.Helpers;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using SharpDX;

namespace O9K.Hud.Helpers
{
	// Token: 0x020000A0 RID: 160
	internal class MenuVectorSlider : IDisposable
	{
		// Token: 0x06000387 RID: 903 RVA: 0x0001C53C File Offset: 0x0001A73C
		public MenuVectorSlider(Menu menu, MenuSlider slider1, MenuSlider slider2)
		{
			this.xItem = menu.Add<MenuSlider>(slider1);
			this.yItem = menu.Add<MenuSlider>(slider2);
			this.xItem.SetTooltip("You can use mouse wheel for better precision");
			this.yItem.SetTooltip("You can use mouse wheel for better precision");
			this.Value = new Vector2(this.xItem, this.yItem);
			this.xItem.ValueChange += this.XOnValueChange;
			this.yItem.ValueChange += this.YOnValueChange;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001C5DC File Offset: 0x0001A7DC
		public MenuVectorSlider(Menu menu, Vector3 values1, Vector3 values2) : this(menu, new MenuSlider("X coordinate", "x", (int)values1.X, (int)values1.Y, (int)values1.Z, false), new MenuSlider("Y coordinate", "y", (int)values2.X, (int)values2.Y, (int)values2.Z, false))
		{
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001C63C File Offset: 0x0001A83C
		public MenuVectorSlider(Menu menu, Vector2 value) : this(menu, new Vector3((float)((int)value.X), 0f, (float)((int)O9K.Core.Helpers.Hud.Info.ScreenSize.X)), new Vector3((float)((int)value.Y), 0f, (float)((int)O9K.Core.Helpers.Hud.Info.ScreenSize.Y)))
		{
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000440D File Offset: 0x0000260D
		// (set) Token: 0x0600038B RID: 907 RVA: 0x00004415 File Offset: 0x00002615
		public Vector2 Value { get; private set; }

		// Token: 0x0600038C RID: 908 RVA: 0x0000441E File Offset: 0x0000261E
		public static implicit operator Vector2(MenuVectorSlider item)
		{
			return item.Value;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00004426 File Offset: 0x00002626
		public void Dispose()
		{
			this.xItem.ValueChange -= this.XOnValueChange;
			this.yItem.ValueChange -= this.YOnValueChange;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00004456 File Offset: 0x00002656
		private void XOnValueChange(object sender, SliderEventArgs e)
		{
			this.Value = new Vector2((float)e.NewValue, this.Value.Y);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00004475 File Offset: 0x00002675
		private void YOnValueChange(object sender, SliderEventArgs e)
		{
			this.Value = new Vector2(this.Value.X, (float)e.NewValue);
		}

		// Token: 0x04000249 RID: 585
		private readonly MenuSlider xItem;

		// Token: 0x0400024A RID: 586
		private readonly MenuSlider yItem;
	}
}
