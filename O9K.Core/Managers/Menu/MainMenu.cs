using System;
using System.Drawing;
using System.Linq;
using Ensage.SDK.Helpers;
using Ensage.SDK.Menu.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Input;
using O9K.Core.Managers.Menu.Items;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Core.Managers.Menu
{
	// Token: 0x02000041 RID: 65
	internal sealed class MainMenu : Menu
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x0001477C File Offset: 0x0001297C
		public MainMenu(IRendererManager9 renderer, IInputManager9 inputManager) : base("O9K", "O9K.Menu")
		{
			base.Renderer = renderer;
			base.InputManager = inputManager;
			base.MenuStyle = new MenuStyle();
			base.IsMainMenu = true;
			base.ChildWidth = 175f * Hud.Info.ScreenRatio;
			this.CalculateSize();
			this.LoadResources();
			this.menuSerializer = new MenuSerializer(new JsonConverter[0]);
			this.Load(this.menuSerializer.Deserialize(this));
			Messenger<Ensage.SDK.Menu.Messages.RootMenuExpandMessage>.Subscribe(new Action<RootMenuExpandMessage>(this.RootMenuExpandMessage));
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000327E File Offset: 0x0000147E
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00003286 File Offset: 0x00001486
		public override bool IsVisible { get; internal set; } = true;

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000328F File Offset: 0x0000148F
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00014828 File Offset: 0x00012A28
		internal Vector2 MenuPosition
		{
			get
			{
				return this.menuPosition;
			}
			set
			{
				Vector2 vector = Hud.Info.ScreenSize - base.Size;
				float num = Math.Max(Math.Min(value.X, vector.X), 0f);
				float num2 = Math.Max(Math.Min(value.Y, vector.Y), 0f);
				this.menuPosition = new Vector2(num, num2);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00003297 File Offset: 0x00001497
		public override T Add<T>(T item)
		{
			base.Add<T>(item);
			item.Load(this.menuSerializer.Deserialize(item));
			return item;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0001488C File Offset: 0x00012A8C
		public void DrawMenu()
		{
			base.Size = new Vector2(base.ChildWidth, base.MenuStyle.Height * 0.75f);
			base.Renderer.DrawLine(this.MenuPosition + new Vector2(0f, base.Size.Y / 2f), this.MenuPosition + new Vector2(base.Size.X, base.Size.Y / 2f), base.MenuStyle.HeaderBackgroundColor, base.Size.Y);
			Vector2 vector;
			vector..ctor(this.MenuPosition.X + base.ChildWidth - base.DisplayNameSize.X - base.MenuStyle.RightIndent, this.MenuPosition.Y + (base.Size.Y - base.MenuStyle.TextSize) / 4f);
			Vector2 vector2 = new Vector2(8f, 14f) * Hud.Info.ScreenRatio;
			Vector2 vector3;
			vector3..ctor(vector.X - 6f * Hud.Info.ScreenRatio, this.menuPosition.Y + (base.Size.Y - vector2.Y) / 2f);
			for (int i = 1; i <= 2; i++)
			{
				base.Renderer.DrawTexture(base.MenuStyle.TextureIconKey, vector3 - new Vector2(vector2.X * (float)i, 0f), vector2, 0f, 1f);
			}
			base.Renderer.DrawText(vector, base.DisplayName, Color.White, base.MenuStyle.TextSize, base.MenuStyle.Font);
			Vector2 vector4 = this.MenuPosition + new Vector2(0f, base.Size.Y);
			for (int j = 0; j < base.MenuItems.Count; j++)
			{
				base.MenuItems[j].OnDraw(vector4, base.ChildWidth);
				if (j < base.MenuItems.Count)
				{
					base.Renderer.DrawLine(vector4, vector4 + new Vector2(base.ChildWidth, 0f), base.MenuStyle.MenuSplitLineColor, base.MenuStyle.MenuSplitLineSize);
				}
				vector4 += new Vector2(0f, base.MenuStyle.Height);
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00014B14 File Offset: 0x00012D14
		public void Save()
		{
			try
			{
				this.menuSerializer.Serialize(this);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00014B48 File Offset: 0x00012D48
		internal override MenuItem GetItemUnder(Vector2 position)
		{
			Vector2 vector = this.MenuPosition;
			Vector2 vector2 = vector + base.Size;
			if (position.X >= vector.X && position.X <= vector2.X && position.Y >= vector.Y && position.Y <= vector2.Y)
			{
				return this;
			}
			return null;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000032BE File Offset: 0x000014BE
		internal override object GetSaveValue()
		{
			return new
			{
				MenuPosition = new
				{
					this.MenuPosition.X,
					this.MenuPosition.Y
				}
			};
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00014BA4 File Offset: 0x00012DA4
		internal override void Load(JToken token)
		{
			JToken jtoken;
			if (token == null)
			{
				jtoken = null;
			}
			else
			{
				JToken jtoken2 = token[base.Name];
				jtoken = ((jtoken2 != null) ? jtoken2["MenuPosition"] : null);
			}
			token = jtoken;
			if (token == null)
			{
				return;
			}
			this.MenuPosition = new Vector2((float)token["X"], (float)token["Y"]);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00014C08 File Offset: 0x00012E08
		internal bool OnMouseMove(Vector2 position)
		{
			if (this.headerDrag)
			{
				this.MenuPosition = position - this.mousePressDiff;
				return false;
			}
			bool flag = true;
			foreach (MenuItem menuItem in base.MenuItems)
			{
				MenuItem itemUnder = menuItem.GetItemUnder(position);
				if (itemUnder != null)
				{
					flag = false;
					if (itemUnder != this.hoover)
					{
						MenuItem menuItem2 = this.hoover;
						if (menuItem2 != null)
						{
							menuItem2.HooverEnd();
						}
						this.hoover = itemUnder;
						this.hoover.HooverStart();
					}
				}
			}
			if (flag && this.hoover != null)
			{
				this.hoover.HooverEnd();
				this.hoover = null;
			}
			return false;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00014CC8 File Offset: 0x00012EC8
		internal override bool OnMousePress(Vector2 position)
		{
			if (this.GetItemUnder(position) != null)
			{
				this.mousePressDiff = position - this.MenuPosition;
				this.headerDrag = true;
				return true;
			}
			foreach (MenuItem menuItem in base.MenuItems)
			{
				MenuItem itemUnder = menuItem.GetItemUnder(position);
				if (itemUnder != null && itemUnder.OnMousePress(position))
				{
					break;
				}
			}
			return false;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00014D50 File Offset: 0x00012F50
		internal override bool OnMouseRelease(Vector2 position)
		{
			if (this.headerDrag)
			{
				this.headerDrag = false;
				return true;
			}
			foreach (MenuItem menuItem in base.MenuItems)
			{
				MenuItem itemUnder = menuItem.GetItemUnder(position);
				if (itemUnder != null && itemUnder.OnMouseRelease(position))
				{
					if (itemUnder.ParentMenu.IsMainMenu)
					{
						Messenger<Ensage.SDK.Menu.Messages.RootMenuExpandMessage>.Publish(new RootMenuExpandMessage("O9K"));
						break;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00014DE0 File Offset: 0x00012FE0
		internal override bool OnMouseWheel(Vector2 position, bool up)
		{
			foreach (MenuItem menuItem in base.MenuItems)
			{
				MenuItem itemUnder = menuItem.GetItemUnder(position);
				if (itemUnder != null && itemUnder.OnMouseWheel(position, up))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00014E48 File Offset: 0x00013048
		private void LoadResources()
		{
			ITextureManager textureManager = base.Renderer.TextureManager;
			textureManager.LoadFromDota(base.MenuStyle.TextureArrowKey, "panorama\\images\\control_icons\\double_arrow_right_png.vtex_c", 0, 0, false, 0, null);
			textureManager.LoadFromDota(base.MenuStyle.TextureLeftArrowKey, "panorama\\images\\control_icons\\arrow_dropdown_png.vtex_c", 0, 0, false, 0, null);
			textureManager.LoadFromDota(base.MenuStyle.TextureIconKey, "panorama\\images\\hud\\reborn\\tournament_pip_on_psd.vtex_c", 0, 0, false, 0, null);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00014EC8 File Offset: 0x000130C8
		private void RootMenuExpandMessage(RootMenuExpandMessage args)
		{
			if (args.MainMenuName == "O9K")
			{
				return;
			}
			foreach (Menu menu in base.MenuItems.OfType<Menu>())
			{
				if (!menu.IsCollapsed)
				{
					menu.IsCollapsed = true;
					base.HooverEnd();
					foreach (MenuItem menuItem in menu.Items)
					{
						menuItem.IsVisible = false;
					}
				}
			}
		}

		// Token: 0x040000B4 RID: 180
		private readonly MenuSerializer menuSerializer;

		// Token: 0x040000B5 RID: 181
		private bool headerDrag;

		// Token: 0x040000B6 RID: 182
		private MenuItem hoover;

		// Token: 0x040000B7 RID: 183
		private Vector2 menuPosition = new Vector2(300f, 400f);

		// Token: 0x040000B8 RID: 184
		private Vector2 mousePressDiff;
	}
}
