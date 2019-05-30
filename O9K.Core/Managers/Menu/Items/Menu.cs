using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Newtonsoft.Json.Linq;
using O9K.Core.Helpers;
using O9K.Core.Managers.Input;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Core.Managers.Menu.Items
{
	// Token: 0x02000052 RID: 82
	public class Menu : MenuItem
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x00003C60 File Offset: 0x00001E60
		public Menu(string displayName) : this(displayName, displayName)
		{
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00003C6A File Offset: 0x00001E6A
		public Menu(string displayName, string name) : base(displayName, name, false)
		{
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00003C9F File Offset: 0x00001E9F
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x00003CA7 File Offset: 0x00001EA7
		public string TextureKey
		{
			get
			{
				return this.textureKey;
			}
			set
			{
				this.textureKey = value;
				if (base.Renderer != null)
				{
					this.LoadTexture();
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00003CBE File Offset: 0x00001EBE
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x00003CE4 File Offset: 0x00001EE4
		public override bool IsVisible
		{
			get
			{
				return base.ParentMenu.IsMainMenu || (base.ParentMenu.IsVisible && this.isVisible);
			}
			internal set
			{
				this.isVisible = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00003CED File Offset: 0x00001EED
		public IEnumerable<MenuItem> Items
		{
			get
			{
				return this.MenuItems;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00003CF5 File Offset: 0x00001EF5
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x00003CFD File Offset: 0x00001EFD
		internal bool IsCollapsed { get; set; } = true;

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00003D06 File Offset: 0x00001F06
		// (set) Token: 0x060002BB RID: 699 RVA: 0x00003D0E File Offset: 0x00001F0E
		internal List<MenuItem> MenuItems { get; private set; } = new List<MenuItem>();

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00003D17 File Offset: 0x00001F17
		// (set) Token: 0x060002BD RID: 701 RVA: 0x00003D1F File Offset: 0x00001F1F
		internal JToken Token { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00003D28 File Offset: 0x00001F28
		// (set) Token: 0x060002BF RID: 703 RVA: 0x00003D30 File Offset: 0x00001F30
		protected float ChildWidth { get; set; } = 150f * Hud.Info.ScreenRatio;

		// Token: 0x060002C0 RID: 704 RVA: 0x000194A8 File Offset: 0x000176A8
		public virtual T Add<T>(T item) where T : MenuItem
		{
			if (this.MenuItems.Contains(item))
			{
				this.Remove(item.Name);
			}
			item.ParentMenu = this;
			this.MenuItems.Add(item);
			this.OrderItems(item);
			if (base.SizeCalculated)
			{
				item.SetStyle(base.MenuStyle);
				item.SetRenderer(base.Renderer);
				item.SetInputManager(base.InputManager);
				item.CalculateSize();
				this.CalculateWidth(true);
			}
			if (this.Token != null)
			{
				item.Load(this.Token);
			}
			if (!this.IsCollapsed)
			{
				item.IsVisible = true;
			}
			return item;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00019580 File Offset: 0x00017780
		public T GetOrAdd<T>(T item) where T : MenuItem
		{
			MenuItem menuItem = this.MenuItems.Find((MenuItem x) => x.Name == item.Name);
			if (menuItem != null)
			{
				return (T)((object)menuItem);
			}
			return this.Add<T>(item);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000195C8 File Offset: 0x000177C8
		public void Remove(string itemName)
		{
			MenuItem menuItem = this.MenuItems.Find((MenuItem x) => x.Name == itemName);
			if (menuItem == null)
			{
				return;
			}
			this.Remove(menuItem);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00019608 File Offset: 0x00017808
		public void Remove(MenuItem item)
		{
			Menu menu;
			if ((menu = (item as Menu)) != null)
			{
				foreach (MenuItem menuItem in menu.Items)
				{
					menuItem.Remove();
				}
			}
			item.Remove();
			this.MenuItems.Remove(item);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00003D39 File Offset: 0x00001F39
		public Menu SetTexture(string key)
		{
			this.TextureKey = key;
			return this;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00003D43 File Offset: 0x00001F43
		public Menu SetTexture(AbilityId id)
		{
			return this.SetTexture(id.ToString());
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00003D58 File Offset: 0x00001F58
		public Menu SetTexture(HeroId id)
		{
			return this.SetTexture(id.ToString());
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00019670 File Offset: 0x00017870
		internal override void CalculateSize()
		{
			base.CalculateSize();
			foreach (MenuItem menuItem in this.MenuItems)
			{
				menuItem.CalculateSize();
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000196C8 File Offset: 0x000178C8
		internal virtual void CalculateWidth(bool full = false)
		{
			foreach (MenuItem menuItem in this.MenuItems)
			{
				if (menuItem.SizeCalculated)
				{
					Menu menu;
					if (full && (menu = (menuItem as Menu)) != null)
					{
						menu.CalculateWidth(true);
					}
					this.ChildWidth = Math.Max((float)((int)menuItem.Size.X), this.ChildWidth);
				}
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00019750 File Offset: 0x00017950
		internal override MenuItem GetItemUnder(Vector2 position)
		{
			if (!this.IsVisible)
			{
				return null;
			}
			if (position.X >= base.Position.X && position.X <= base.Position.X + base.Size.X && position.Y >= base.Position.Y && position.Y <= base.Position.Y + base.Size.Y)
			{
				return this;
			}
			foreach (MenuItem menuItem in this.MenuItems)
			{
				MenuItem itemUnder = menuItem.GetItemUnder(position);
				if (itemUnder != null)
				{
					return itemUnder;
				}
			}
			return null;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00003D6D File Offset: 0x00001F6D
		internal override object GetSaveValue()
		{
			return null;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001981C File Offset: 0x00017A1C
		internal override void Load(JToken token)
		{
			this.Token = ((token != null) ? token[base.Name] : null);
			foreach (MenuItem menuItem in this.MenuItems.ToList<MenuItem>())
			{
				menuItem.Load(this.Token);
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00019890 File Offset: 0x00017A90
		internal override bool OnMouseRelease(Vector2 position)
		{
			foreach (Menu menu in base.ParentMenu.MenuItems.OfType<Menu>())
			{
				if (menu != this)
				{
					if (!menu.IsCollapsed)
					{
						menu.IsCollapsed = true;
						menu.HooverEnd();
					}
					foreach (MenuItem menuItem in menu.MenuItems)
					{
						menuItem.IsVisible = false;
					}
				}
			}
			this.IsCollapsed = !this.IsCollapsed;
			foreach (MenuItem menuItem2 in this.MenuItems)
			{
				menuItem2.IsVisible = !menuItem2.IsVisible;
			}
			return true;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00019994 File Offset: 0x00017B94
		internal override void Remove()
		{
			foreach (MenuItem menuItem in this.MenuItems)
			{
				menuItem.Remove();
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000199E4 File Offset: 0x00017BE4
		internal override void SetInputManager(IInputManager9 inputManager)
		{
			base.SetInputManager(inputManager);
			foreach (MenuItem menuItem in this.MenuItems)
			{
				menuItem.SetInputManager(inputManager);
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00019A3C File Offset: 0x00017C3C
		internal override void SetRenderer(IRendererManager9 renderer)
		{
			base.SetRenderer(renderer);
			foreach (MenuItem menuItem in this.MenuItems)
			{
				menuItem.SetRenderer(renderer);
			}
			if (!string.IsNullOrEmpty(this.TextureKey))
			{
				this.LoadTexture();
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00019AA8 File Offset: 0x00017CA8
		internal override void SetStyle(MenuStyle menuStyle)
		{
			base.SetStyle(menuStyle);
			foreach (MenuItem menuItem in this.MenuItems)
			{
				menuItem.SetStyle(menuStyle);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00019B00 File Offset: 0x00017D00
		protected override void Draw()
		{
			base.Draw();
			if (!string.IsNullOrEmpty(this.textureKey))
			{
				base.Renderer.DrawTexture(this.textureKey, new RectangleF(base.Position.X + base.MenuStyle.LeftIndent, base.Position.Y + (base.Size.Y - this.textureSize.Y) / 2f, this.textureSize.X, this.textureSize.Y), 0f, 1f);
			}
			base.Renderer.DrawTexture(base.MenuStyle.TextureArrowKey, new RectangleF(base.Position.X + base.Size.X - base.MenuStyle.TextureArrowSize - base.MenuStyle.RightIndent, base.Position.Y + (base.Size.Y - base.MenuStyle.TextureArrowSize) / 2.2f, base.MenuStyle.TextureArrowSize, base.MenuStyle.TextureArrowSize), 0f, 1f);
			Vector2 vector = base.Position + new Vector2(base.Size.X, 0f);
			for (int i = 0; i < this.MenuItems.Count; i++)
			{
				MenuItem menuItem = this.MenuItems[i];
				menuItem.OnDraw(vector, this.ChildWidth);
				if (i > 0 && i < this.MenuItems.Count && menuItem.IsVisible)
				{
					base.Renderer.DrawLine(vector, vector + new Vector2(this.ChildWidth, 0f), base.MenuStyle.MenuSplitLineColor, base.MenuStyle.MenuSplitLineSize);
				}
				vector += new Vector2(0f, base.MenuStyle.Height);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00019CF0 File Offset: 0x00017EF0
		private void LoadTexture()
		{
			if (this.textureKey.Contains("npc_dota"))
			{
				base.Renderer.TextureManager.LoadFromDota(this.textureKey, false);
				this.textureSize = base.MenuStyle.TextureHeroSize * new Vector2(1.4f, 1.2f);
			}
			else
			{
				AbilityId abilityId;
				if (Enum.TryParse<AbilityId>(this.textureKey, out abilityId))
				{
					base.Renderer.TextureManager.LoadFromDota(abilityId, false);
				}
				this.textureSize = new Vector2(base.MenuStyle.TextureAbilitySize) * 1.2f;
			}
			base.TextIndent = (int)(this.textureSize.X + base.MenuStyle.LeftIndent / 2f);
			base.CalculateSize();
			base.ParentMenu.CalculateWidth(false);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00019DC4 File Offset: 0x00017FC4
		private void OrderItems(MenuItem item)
		{
			if (item is Menu)
			{
				if (!this.lastAddedItemIsMenu)
				{
					this.MenuItems = (from x in this.MenuItems
					orderby x is Menu descending
					select x).ToList<MenuItem>();
				}
				this.lastAddedItemIsMenu = (this.MenuItems.LastOrDefault<MenuItem>() is Menu);
				return;
			}
			this.lastAddedItemIsMenu = false;
		}

		// Token: 0x0400012D RID: 301
		private bool isVisible;

		// Token: 0x0400012E RID: 302
		private bool lastAddedItemIsMenu = true;

		// Token: 0x0400012F RID: 303
		private string textureKey;

		// Token: 0x04000130 RID: 304
		private Vector2 textureSize;
	}
}
