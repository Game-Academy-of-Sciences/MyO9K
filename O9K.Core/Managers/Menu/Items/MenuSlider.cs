using System;
using System.Drawing;
using Newtonsoft.Json.Linq;
using O9K.Core.Logger;
using O9K.Core.Managers.Input.EventArgs;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Core.Managers.Menu.Items
{
	// Token: 0x02000050 RID: 80
	public class MenuSlider : MenuItem
	{
		// Token: 0x0600028B RID: 651 RVA: 0x000039B9 File Offset: 0x00001BB9
		public MenuSlider(string displayName, int value, int minValue, int maxValue, bool heroUnique = false) : this(displayName, displayName, value, minValue, maxValue, heroUnique)
		{
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000039C9 File Offset: 0x00001BC9
		public MenuSlider(string displayName, string name, int value, int minValue, int maxValue, bool heroUnique = false) : base(displayName, name, heroUnique)
		{
			this.MinValue = minValue;
			this.MaxValue = maxValue;
			this.Value = value;
			this.defaultValue = value;
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600028D RID: 653 RVA: 0x000039F3 File Offset: 0x00001BF3
		// (remove) Token: 0x0600028E RID: 654 RVA: 0x00003A24 File Offset: 0x00001C24
		public event EventHandler<SliderEventArgs> ValueChange
		{
			add
			{
				value(this, new SliderEventArgs(this.value, this.value));
				this.valueChange = (EventHandler<SliderEventArgs>)Delegate.Combine(this.valueChange, value);
			}
			remove
			{
				this.valueChange = (EventHandler<SliderEventArgs>)Delegate.Remove(this.valueChange, value);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00003A3D File Offset: 0x00001C3D
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00019014 File Offset: 0x00017214
		public int Value
		{
			get
			{
				return this.value;
			}
			set
			{
				int num = Math.Max(Math.Min(value, this.MaxValue), this.MinValue);
				if (num == this.value)
				{
					return;
				}
				EventHandler<SliderEventArgs> eventHandler = this.valueChange;
				if (eventHandler != null)
				{
					eventHandler(this, new SliderEventArgs(num, this.value));
				}
				this.value = num;
				if (base.SizeCalculated)
				{
					this.valueTextSize = base.Renderer.MeasureText(this.value.ToString(), base.MenuStyle.TextSize, base.MenuStyle.Font);
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00003A45 File Offset: 0x00001C45
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00003A4D File Offset: 0x00001C4D
		public int MinValue { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00003A56 File Offset: 0x00001C56
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00003A5E File Offset: 0x00001C5E
		public int MaxValue { get; set; }

		// Token: 0x06000295 RID: 661 RVA: 0x00003A3D File Offset: 0x00001C3D
		public static implicit operator int(MenuSlider item)
		{
			return item.value;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00003A67 File Offset: 0x00001C67
		public static implicit operator float(MenuSlider item)
		{
			return (float)item.value;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000344F File Offset: 0x0000164F
		public MenuSlider SetTooltip(string tooltip)
		{
			base.Tooltip = tooltip;
			return this;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000190A4 File Offset: 0x000172A4
		internal override void CalculateSize()
		{
			base.CalculateSize();
			Vector2 vector = base.Renderer.MeasureText(this.MaxValue.ToString(), base.MenuStyle.TextSize, base.MenuStyle.Font);
			base.Size = new Vector2(base.Size.X + vector.X, base.Size.Y);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00003A70 File Offset: 0x00001C70
		internal override MenuItem GetItemUnder(Vector2 position)
		{
			if (this.drag)
			{
				return this;
			}
			return base.GetItemUnder(position);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00003A83 File Offset: 0x00001C83
		internal override object GetSaveValue()
		{
			if (this.value == this.defaultValue)
			{
				return null;
			}
			return this.Value;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00019110 File Offset: 0x00017310
		internal override void Load(JToken token)
		{
			try
			{
				token = ((token != null) ? token[base.Name] : null);
				if (token != null)
				{
					this.Value = (int)token;
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00003AA0 File Offset: 0x00001CA0
		internal override bool OnMousePress(Vector2 position)
		{
			this.drag = true;
			this.SetValue(position);
			base.InputManager.MouseMove += this.OnMouseMove;
			base.InputManager.MouseKeyUp += this.OnMouseKeyUp;
			return true;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00003ADF File Offset: 0x00001CDF
		internal override bool OnMouseWheel(Vector2 position, bool up)
		{
			this.Value += (up ? 1 : -1);
			return true;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00003AF6 File Offset: 0x00001CF6
		internal override void Remove()
		{
			if (base.InputManager == null)
			{
				return;
			}
			base.InputManager.MouseKeyUp -= this.OnMouseKeyUp;
			base.InputManager.MouseMove -= this.OnMouseMove;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00003B2F File Offset: 0x00001D2F
		internal override void SetRenderer(IRendererManager9 renderer)
		{
			base.SetRenderer(renderer);
			this.valueTextSize = base.Renderer.MeasureText(this.value.ToString(), base.MenuStyle.TextSize, base.MenuStyle.Font);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0001915C File Offset: 0x0001735C
		protected override void Draw()
		{
			float num = ((float)this.Value - (float)this.MinValue) / (float)(this.MaxValue - this.MinValue);
			base.Renderer.DrawLine(base.Position + new Vector2(0f, base.Size.Y / 2f), base.Position + new Vector2(base.Size.X * num, base.Size.Y / 2f), base.MenuStyle.BackgroundColor, base.Size.Y);
			base.Draw();
			Vector2 position;
			position..ctor(base.Position.X + base.Size.X - base.MenuStyle.RightIndent - this.valueTextSize.X, base.Position.Y + (base.Size.Y - base.MenuStyle.TextSize) / 3.3f);
			base.Renderer.DrawText(position, this.Value.ToString(), Color.White, base.MenuStyle.TextSize, base.MenuStyle.Font);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00003B6A File Offset: 0x00001D6A
		private void OnMouseKeyUp(object sender, MouseEventArgs e)
		{
			this.drag = false;
			base.InputManager.MouseKeyUp -= this.OnMouseKeyUp;
			base.InputManager.MouseMove -= this.OnMouseMove;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00003BA1 File Offset: 0x00001DA1
		private void OnMouseMove(object sender, MouseMoveEventArgs e)
		{
			this.SetValue(e.ScreenPosition);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00019298 File Offset: 0x00017498
		private void SetValue(Vector2 position)
		{
			float x = base.Position.X;
			float num = x + base.Size.X;
			float num2 = (position.X - x) / (num - x);
			this.Value = (int)(num2 * (float)(this.MaxValue - this.MinValue) + (float)this.MinValue);
		}

		// Token: 0x04000122 RID: 290
		private readonly int defaultValue;

		// Token: 0x04000123 RID: 291
		private bool drag;

		// Token: 0x04000124 RID: 292
		private int value;

		// Token: 0x04000125 RID: 293
		private EventHandler<SliderEventArgs> valueChange;

		// Token: 0x04000126 RID: 294
		private Vector2 valueTextSize;
	}
}
