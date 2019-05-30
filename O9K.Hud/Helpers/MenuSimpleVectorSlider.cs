using System;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using SharpDX;

namespace O9K.Hud.Helpers
{
	// Token: 0x0200009F RID: 159
	internal class MenuSimpleVectorSlider : IDisposable
	{
		// Token: 0x06000380 RID: 896 RVA: 0x0000437E File Offset: 0x0000257E
		public MenuSimpleVectorSlider(Menu menu, string displayName, string name, int value, int minValue, int maxValue)
		{
			this.slider = menu.Add<MenuSlider>(new MenuSlider(displayName, name, value, minValue, maxValue, false));
			this.slider.ValueChange += this.SliderOnValueChange;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000043B7 File Offset: 0x000025B7
		public MenuSimpleVectorSlider(Menu menu, string displayName, int value, int minValue, int maxValue) : this(menu, displayName, displayName, value, minValue, maxValue)
		{
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000382 RID: 898 RVA: 0x000043C7 File Offset: 0x000025C7
		// (set) Token: 0x06000383 RID: 899 RVA: 0x000043CF File Offset: 0x000025CF
		public Vector2 Value { get; private set; }

		// Token: 0x06000384 RID: 900 RVA: 0x000043D8 File Offset: 0x000025D8
		public static implicit operator Vector2(MenuSimpleVectorSlider item)
		{
			return item.Value;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000043E0 File Offset: 0x000025E0
		public void Dispose()
		{
			this.slider.ValueChange -= this.SliderOnValueChange;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000043F9 File Offset: 0x000025F9
		private void SliderOnValueChange(object sender, SliderEventArgs e)
		{
			this.Value = new Vector2((float)e.NewValue);
		}

		// Token: 0x04000247 RID: 583
		private readonly MenuSlider slider;
	}
}
