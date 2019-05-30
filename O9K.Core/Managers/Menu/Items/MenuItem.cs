using System;
using System.Drawing;
using Ensage;
using Ensage.SDK.Renderer;
using Newtonsoft.Json.Linq;
using O9K.Core.Logger;
using O9K.Core.Managers.Input;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Core.Managers.Menu.Items
{
	// Token: 0x0200004C RID: 76
	public abstract class MenuItem : IEquatable<MenuItem>
	{
		// Token: 0x0600022D RID: 557 RVA: 0x00017E04 File Offset: 0x00016004
		protected MenuItem(string displayName, string name, bool heroUnique = false)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (heroUnique)
			{
				this.Name = name + MenuItem.HeroId;
			}
			else
			{
				this.Name = name;
			}
			this.DisplayName = displayName;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600022E RID: 558 RVA: 0x000035E8 File Offset: 0x000017E8
		// (set) Token: 0x0600022F RID: 559 RVA: 0x000035F0 File Offset: 0x000017F0
		public string Tooltip
		{
			get
			{
				return this.tooltip;
			}
			set
			{
				this.tooltip = value;
				if (this.Renderer != null)
				{
					this.tooltipTextSize = this.Renderer.MeasureText(this.tooltip, this.MenuStyle.TooltipTextSize, this.MenuStyle.Font);
				}
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000362E File Offset: 0x0000182E
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00003636 File Offset: 0x00001836
		public Menu ParentMenu { get; internal set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000363F File Offset: 0x0000183F
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00003647 File Offset: 0x00001847
		public virtual bool IsVisible { get; internal set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00003650 File Offset: 0x00001850
		public string Name { get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00003658 File Offset: 0x00001858
		public string DisplayName { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00003660 File Offset: 0x00001860
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00003668 File Offset: 0x00001868
		internal bool IsMainMenu { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00003671 File Offset: 0x00001871
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00003679 File Offset: 0x00001879
		internal Vector2 Size { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00003682 File Offset: 0x00001882
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000368A File Offset: 0x0000188A
		internal bool SizeCalculated { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00003693 File Offset: 0x00001893
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000369B File Offset: 0x0000189B
		protected Vector2 DisplayNameSize { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000036A4 File Offset: 0x000018A4
		// (set) Token: 0x0600023F RID: 575 RVA: 0x000036AC File Offset: 0x000018AC
		protected IInputManager9 InputManager { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000036B5 File Offset: 0x000018B5
		// (set) Token: 0x06000241 RID: 577 RVA: 0x000036BD File Offset: 0x000018BD
		protected MenuStyle MenuStyle { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000242 RID: 578 RVA: 0x000036C6 File Offset: 0x000018C6
		// (set) Token: 0x06000243 RID: 579 RVA: 0x000036CE File Offset: 0x000018CE
		protected Vector2 Position { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000036D7 File Offset: 0x000018D7
		// (set) Token: 0x06000245 RID: 581 RVA: 0x000036DF File Offset: 0x000018DF
		protected IRendererManager9 Renderer { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000036E8 File Offset: 0x000018E8
		// (set) Token: 0x06000247 RID: 583 RVA: 0x000036F0 File Offset: 0x000018F0
		protected bool SaveValue { get; private set; } = true;

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000036F9 File Offset: 0x000018F9
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00003701 File Offset: 0x00001901
		protected int TextIndent { get; set; }

		// Token: 0x0600024A RID: 586 RVA: 0x0000370A File Offset: 0x0000190A
		public void DisableSave()
		{
			this.SaveValue = false;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00003713 File Offset: 0x00001913
		public bool Equals(MenuItem other)
		{
			return this.Name == ((other != null) ? other.Name : null);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00017E5C File Offset: 0x0001605C
		internal virtual void CalculateSize()
		{
			this.DisplayNameSize = this.Renderer.MeasureText(this.DisplayName, this.MenuStyle.TextSize, this.MenuStyle.Font);
			float num = this.DisplayNameSize.X + this.MenuStyle.LeftIndent + this.MenuStyle.RightIndent + this.MenuStyle.TextureArrowSize * 2f + (float)this.TextIndent;
			this.Size = new Vector2(num, this.MenuStyle.Height);
			if (!string.IsNullOrEmpty(this.tooltip))
			{
				this.tooltipTextSize = this.Renderer.MeasureText(this.tooltip, this.MenuStyle.TooltipTextSize, this.MenuStyle.Font);
			}
			this.SizeCalculated = true;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00017F2C File Offset: 0x0001612C
		internal virtual MenuItem GetItemUnder(Vector2 position)
		{
			if (!this.IsVisible)
			{
				return null;
			}
			if (position.X >= this.Position.X && position.X <= this.Position.X + this.Size.X && position.Y >= this.Position.Y && position.Y <= this.Position.Y + this.Size.Y)
			{
				return this;
			}
			return null;
		}

		// Token: 0x0600024E RID: 590
		internal abstract object GetSaveValue();

		// Token: 0x0600024F RID: 591 RVA: 0x00017FAC File Offset: 0x000161AC
		internal void HooverEnd()
		{
			this.isHoovered = false;
			Menu menu;
			if ((menu = (this as Menu)) != null && !menu.IsCollapsed)
			{
				return;
			}
			this.hooverTime = Game.RawGameTime;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00017FE0 File Offset: 0x000161E0
		internal void HooverStart()
		{
			this.isHoovered = true;
			Menu menu;
			if ((menu = (this as Menu)) != null && !menu.IsCollapsed)
			{
				return;
			}
			this.hooverTime = Game.RawGameTime;
		}

		// Token: 0x06000251 RID: 593
		internal abstract void Load(JToken token);

		// Token: 0x06000252 RID: 594 RVA: 0x00018014 File Offset: 0x00016214
		internal void OnDraw(Vector2 position, float childWidth)
		{
			if (!this.IsVisible)
			{
				return;
			}
			this.Position = position;
			this.Size = new Vector2(childWidth, this.MenuStyle.Height);
			try
			{
				this.Draw();
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000372C File Offset: 0x0000192C
		internal virtual bool OnMousePress(Vector2 position)
		{
			return false;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000372C File Offset: 0x0000192C
		internal virtual bool OnMouseRelease(Vector2 position)
		{
			return false;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000372C File Offset: 0x0000192C
		internal virtual bool OnMouseWheel(Vector2 position, bool up)
		{
			return false;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000372F File Offset: 0x0000192F
		internal virtual void Remove()
		{
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00003731 File Offset: 0x00001931
		internal virtual void SetInputManager(IInputManager9 inputManager)
		{
			this.InputManager = inputManager;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00018068 File Offset: 0x00016268
		internal virtual void SetRenderer(IRendererManager9 renderer)
		{
			this.Renderer = renderer;
			if (!string.IsNullOrEmpty(this.tooltip))
			{
				this.tooltipTextSize = this.Renderer.MeasureText(this.tooltip, this.MenuStyle.TooltipTextSize, this.MenuStyle.Font);
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000373A File Offset: 0x0000193A
		internal virtual void SetStyle(MenuStyle menuStyle)
		{
			this.MenuStyle = menuStyle;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000180B8 File Offset: 0x000162B8
		protected virtual void Draw()
		{
			this.Renderer.DrawLine(this.Position + new Vector2(0f, this.Size.Y / 2f), this.Position + new Vector2(this.Size.X, this.Size.Y / 2f), this.MenuStyle.BackgroundColor, this.Size.Y);
			float num = Math.Min(Game.RawGameTime - this.hooverTime, 0.3f) / 0.3f;
			Menu menu;
			int val;
			if (this.isHoovered || ((menu = (this as Menu)) != null && !menu.IsCollapsed))
			{
				val = (int)(num * 85f + 170f);
				if (!string.IsNullOrEmpty(this.Tooltip))
				{
					this.Renderer.DrawFilledRectangle(new RectangleF(this.Position.X + this.Size.X + this.MenuStyle.LeftIndent, this.Position.Y + this.tooltipTextSize.Y / 4f, this.tooltipTextSize.X + this.MenuStyle.LeftIndent + this.MenuStyle.RightIndent, this.tooltipTextSize.Y), Color.FromArgb(200, 5, 5, 5), Color.FromArgb(255, 50, 50, 50), 1f);
					this.Renderer.DrawText(new RectangleF(this.Position.X + this.Size.X + this.MenuStyle.LeftIndent * 2f, this.Position.Y + this.tooltipTextSize.Y / 4f, this.tooltipTextSize.X, this.Size.Y), this.tooltip, Color.White, RendererFontFlags.Left, this.MenuStyle.TooltipTextSize, "Calibri");
				}
			}
			else
			{
				val = (int)(num * -85f + 255f);
			}
			Vector2 position;
			position..ctor(this.Position.X + this.MenuStyle.LeftIndent + (float)this.TextIndent, this.Position.Y + (this.Size.Y - this.MenuStyle.TextSize) / 3.3f);
			this.Renderer.DrawText(position, this.DisplayName, Color.FromArgb(Math.Max(Math.Min(val, 255), 170), 255, 255, 255), this.MenuStyle.TextSize, this.MenuStyle.Font);
		}

		// Token: 0x04000101 RID: 257
		private static readonly int HeroId = (int)ObjectManager.LocalPlayer.SelectedHeroId;

		// Token: 0x04000102 RID: 258
		private float hooverTime;

		// Token: 0x04000103 RID: 259
		private bool isHoovered;

		// Token: 0x04000104 RID: 260
		private string tooltip;

		// Token: 0x04000105 RID: 261
		private Vector2 tooltipTextSize;
	}
}
