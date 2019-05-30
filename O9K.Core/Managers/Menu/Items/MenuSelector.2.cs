using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json.Linq;
using O9K.Core.Extensions;
using O9K.Core.Logger;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Core.Managers.Menu.Items
{
	// Token: 0x0200004F RID: 79
	public class MenuSelector<T> : MenuItem
	{
		// Token: 0x06000278 RID: 632 RVA: 0x0000392C File Offset: 0x00001B2C
		public MenuSelector(string displayName, IEnumerable<T> items = null, bool heroUnique = false) : this(displayName, displayName, items, heroUnique)
		{
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000189A0 File Offset: 0x00016BA0
		public MenuSelector(string displayName, string name, IEnumerable<T> items = null, bool heroUnique = false) : base(displayName, name, heroUnique)
		{
			if (items == null)
			{
				return;
			}
			this.items = items.Distinct<T>().ToList<T>();
			if (this.items.Count > 0)
			{
				this.selected = this.items[0];
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600027A RID: 634 RVA: 0x000189F8 File Offset: 0x00016BF8
		// (remove) Token: 0x0600027B RID: 635 RVA: 0x00003938 File Offset: 0x00001B38
		public event EventHandler<SelectorEventArgs<T>> ValueChange
		{
			add
			{
				if (!string.IsNullOrEmpty(this.selected.ToString()))
				{
					value(this, new SelectorEventArgs<T>(this.selected, this.selected));
				}
				this.valueChange = (EventHandler<SelectorEventArgs<T>>)Delegate.Combine(this.valueChange, value);
			}
			remove
			{
				this.valueChange = (EventHandler<SelectorEventArgs<T>>)Delegate.Remove(this.valueChange, value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00003951 File Offset: 0x00001B51
		// (set) Token: 0x0600027D RID: 637 RVA: 0x00018A4C File Offset: 0x00016C4C
		public T Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				if (!this.items.Contains(value))
				{
					return;
				}
				if (this.selected.Equals(value))
				{
					return;
				}
				EventHandler<SelectorEventArgs<T>> eventHandler = this.valueChange;
				if (eventHandler != null)
				{
					eventHandler(this, new SelectorEventArgs<T>(value, this.selected));
				}
				this.selected = value;
				if (base.SizeCalculated)
				{
					this.valueText = this.selected.ToString().ToSentenceCase();
					this.valueTextSize = base.Renderer.MeasureText(this.valueText, base.MenuStyle.TextSize, base.MenuStyle.Font);
				}
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00003959 File Offset: 0x00001B59
		public static implicit operator T(MenuSelector<T> item)
		{
			return item.Selected;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00018AF8 File Offset: 0x00016CF8
		public unsafe void AddItem(T name)
		{
			if (this.items.Contains(name))
			{
				return;
			}
			this.items.Add(name);
			T* ptr = ref this.selected;
			T t = default(T);
			string value;
			if (t == null)
			{
				t = this.selected;
				ptr = ref t;
				if (t == null)
				{
					value = null;
					goto IL_50;
				}
			}
			value = ptr.ToString();
			IL_50:
			if (string.IsNullOrEmpty(value))
			{
				this.Selected = name;
			}
			if (base.SizeCalculated)
			{
				this.CalculateFullWidth(name);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00003961 File Offset: 0x00001B61
		public T SetTooltip(string tooltip)
		{
			base.Tooltip = tooltip;
			return this;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00003970 File Offset: 0x00001B70
		internal override void CalculateSize()
		{
			base.CalculateSize();
			base.Size = new Vector2(this.maxWidth, base.MenuStyle.Height);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00018B74 File Offset: 0x00016D74
		internal override object GetSaveValue()
		{
			if (this.items.Count != 0)
			{
				T t = this.items[0];
				if (!t.Equals(this.Selected))
				{
					return this.Selected;
				}
			}
			return null;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00018BC4 File Offset: 0x00016DC4
		internal override void Load(JToken token)
		{
			try
			{
				token = ((token != null) ? token[base.Name] : null);
				if (token != null)
				{
					T item = token.ToObject<T>();
					if (this.items.Contains(item))
					{
						this.Selected = item;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00018C20 File Offset: 0x00016E20
		internal override bool OnMouseRelease(Vector2 position)
		{
			if (this.items.Count == 0)
			{
				return false;
			}
			int num = this.items.FindIndex((T x) => x.ToString() == this.selected.ToString());
			if (num == this.items.Count - 1)
			{
				num = 0;
			}
			else
			{
				num++;
			}
			this.Selected = this.items[num];
			return true;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00018C80 File Offset: 0x00016E80
		internal override bool OnMouseWheel(Vector2 position, bool up)
		{
			if (this.items.Count == 0)
			{
				return false;
			}
			int num = this.items.FindIndex((T x) => x.ToString() == this.selected.ToString());
			if (up)
			{
				if (num == this.items.Count - 1)
				{
					num = 0;
				}
				else
				{
					num++;
				}
			}
			else if (num == 0)
			{
				num = this.items.Count - 1;
			}
			else
			{
				num--;
			}
			this.Selected = this.items[num];
			return true;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00018CFC File Offset: 0x00016EFC
		internal unsafe override void SetRenderer(IRendererManager9 renderer)
		{
			base.SetRenderer(renderer);
			T* ptr = ref this.selected;
			T t = default(T);
			string value;
			if (t == null)
			{
				t = this.selected;
				ptr = ref t;
				if (t == null)
				{
					value = null;
					goto IL_3C;
				}
			}
			value = ptr.ToString();
			IL_3C:
			if (!string.IsNullOrEmpty(value))
			{
				this.valueText = this.selected.ToString().ToSentenceCase();
				this.valueTextSize = base.Renderer.MeasureText(this.valueText, base.MenuStyle.TextSize, base.MenuStyle.Font);
				foreach (T name in this.items)
				{
					this.CalculateFullWidth(name);
				}
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00018DDC File Offset: 0x00016FDC
		protected unsafe override void Draw()
		{
			base.Draw();
			T* ptr = ref this.selected;
			T t = default(T);
			string value;
			if (t == null)
			{
				t = this.selected;
				ptr = ref t;
				if (t == null)
				{
					value = null;
					goto IL_3B;
				}
			}
			value = ptr.ToString();
			IL_3B:
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			Vector2 position;
			position..ctor(base.Position.X + base.Size.X - base.MenuStyle.RightIndent * 1.5f - this.valueTextSize.X - base.MenuStyle.TextureArrowSize, base.Position.Y + (base.Size.Y - base.MenuStyle.TextSize) / 3.3f);
			base.Renderer.DrawText(position, this.valueText, Color.White, base.MenuStyle.TextSize, base.MenuStyle.Font);
			float num = base.MenuStyle.TextureArrowSize * 1.4f;
			base.Renderer.DrawTexture(base.MenuStyle.TextureLeftArrowKey, new RectangleF(base.Position.X + base.Size.X - num - base.MenuStyle.RightIndent * 0.7f, base.Position.Y + (base.Size.Y - num) / 2f, num, num), 1.57079637f, 1f);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00018F50 File Offset: 0x00017150
		private void CalculateFullWidth(T name)
		{
			string text = name.ToString().ToSentenceCase();
			Vector2 vector = base.Renderer.MeasureText(text, base.MenuStyle.TextSize, base.MenuStyle.Font);
			float num = base.DisplayNameSize.X + base.MenuStyle.LeftIndent + base.MenuStyle.RightIndent + 10f + base.MenuStyle.TextureArrowSize * 4f + vector.X * 1.75f;
			if (num > this.maxWidth)
			{
				this.maxWidth = num;
				base.Size = new Vector2(this.maxWidth, base.MenuStyle.Height);
				base.ParentMenu.CalculateWidth(false);
			}
		}

		// Token: 0x0400011C RID: 284
		private readonly List<T> items = new List<T>();

		// Token: 0x0400011D RID: 285
		private float maxWidth;

		// Token: 0x0400011E RID: 286
		private T selected;

		// Token: 0x0400011F RID: 287
		private EventHandler<SelectorEventArgs<T>> valueChange;

		// Token: 0x04000120 RID: 288
		private string valueText;

		// Token: 0x04000121 RID: 289
		private Vector2 valueTextSize;
	}
}
