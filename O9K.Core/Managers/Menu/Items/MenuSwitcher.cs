using System;
using System.Drawing;
using Newtonsoft.Json.Linq;
using O9K.Core.Logger;
using O9K.Core.Managers.Menu.EventArgs;
using SharpDX;

namespace O9K.Core.Managers.Menu.Items
{
	// Token: 0x02000051 RID: 81
	public class MenuSwitcher : MenuItem
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x00003BAF File Offset: 0x00001DAF
		public MenuSwitcher(string displayName, bool defaultValue = true, bool heroUnique = false) : this(displayName, displayName, defaultValue, heroUnique)
		{
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00003BBB File Offset: 0x00001DBB
		public MenuSwitcher(string displayName, string name, bool defaultValue = true, bool heroUnique = false) : base(displayName, name, heroUnique)
		{
			this.defaultValue = defaultValue;
			this.isEnabled = defaultValue;
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060002A6 RID: 678 RVA: 0x000192EC File Offset: 0x000174EC
		// (remove) Token: 0x060002A7 RID: 679 RVA: 0x00003BD5 File Offset: 0x00001DD5
		public event EventHandler<SwitcherEventArgs> ValueChange
		{
			add
			{
				if (this.loaded && this.IsEnabled)
				{
					value(this, new SwitcherEventArgs(this.IsEnabled, this.IsEnabled));
				}
				this.valueChange = (EventHandler<SwitcherEventArgs>)Delegate.Combine(this.valueChange, value);
			}
			remove
			{
				this.valueChange = (EventHandler<SwitcherEventArgs>)Delegate.Remove(this.valueChange, value);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00003BEE File Offset: 0x00001DEE
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x00019338 File Offset: 0x00017538
		public bool IsEnabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				if (value == this.isEnabled)
				{
					return;
				}
				SwitcherEventArgs switcherEventArgs = new SwitcherEventArgs(value, this.isEnabled);
				EventHandler<SwitcherEventArgs> eventHandler = this.valueChange;
				if (eventHandler != null)
				{
					eventHandler(this, switcherEventArgs);
				}
				if (switcherEventArgs.Process)
				{
					this.isEnabled = value;
				}
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00003BEE File Offset: 0x00001DEE
		public static implicit operator bool(MenuSwitcher item)
		{
			return item.isEnabled;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000344F File Offset: 0x0000164F
		public MenuSwitcher SetTooltip(string tooltip)
		{
			base.Tooltip = tooltip;
			return this;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00003BF6 File Offset: 0x00001DF6
		internal override void CalculateSize()
		{
			base.CalculateSize();
			base.Size = new Vector2(base.Size.X + base.MenuStyle.TextureArrowSize + 10f, base.Size.Y);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00003C31 File Offset: 0x00001E31
		internal override object GetSaveValue()
		{
			if (this.defaultValue == this.isEnabled)
			{
				return null;
			}
			return this.IsEnabled;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00019380 File Offset: 0x00017580
		internal override void Load(JToken token)
		{
			try
			{
				token = ((token != null) ? token[base.Name] : null);
				if (token != null)
				{
					this.isEnabled = token.ToObject<bool>();
				}
				if (this.isEnabled)
				{
					this.isEnabled = false;
					this.IsEnabled = true;
				}
				this.loaded = true;
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00003C4E File Offset: 0x00001E4E
		internal override bool OnMouseRelease(Vector2 position)
		{
			this.IsEnabled = !this.IsEnabled;
			return true;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000193E8 File Offset: 0x000175E8
		protected override void Draw()
		{
			base.Draw();
			base.Renderer.DrawFilledRectangle(new RectangleF(base.Position.X + base.Size.X - base.MenuStyle.TextureArrowSize - base.MenuStyle.RightIndent, base.Position.Y + (base.Size.Y - base.MenuStyle.TextureArrowSize) / 2.2f, base.MenuStyle.TextureArrowSize, base.MenuStyle.TextureArrowSize), this.IsEnabled ? Color.White : base.MenuStyle.BackgroundColor, Color.FromArgb(255, 50, 50, 50), 2f);
		}

		// Token: 0x04000129 RID: 297
		private readonly bool defaultValue;

		// Token: 0x0400012A RID: 298
		private bool isEnabled;

		// Token: 0x0400012B RID: 299
		private bool loaded;

		// Token: 0x0400012C RID: 300
		private EventHandler<SwitcherEventArgs> valueChange;
	}
}
