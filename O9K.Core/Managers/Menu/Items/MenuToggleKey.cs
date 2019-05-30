using System;
using System.Drawing;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using O9K.Core.Logger;
using O9K.Core.Managers.Input;
using O9K.Core.Managers.Input.EventArgs;
using O9K.Core.Managers.Input.Keys;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Core.Managers.Menu.Items
{
	// Token: 0x02000057 RID: 87
	public class MenuToggleKey : MenuItem
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x00003DCD File Offset: 0x00001FCD
		public MenuToggleKey(string displayName, Key key = Key.None, bool defaultValue = true, bool heroUnique = false) : this(displayName, displayName, key, defaultValue, heroUnique)
		{
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00003DDB File Offset: 0x00001FDB
		public MenuToggleKey(string displayName, string name, Key key = Key.None, bool defaultValue = true, bool heroUnique = false) : base(displayName, name, heroUnique)
		{
			this.IsActive = defaultValue;
			this.defaultValue = defaultValue;
			this.keyText = key.ToString();
			this.keyValue = key;
			this.defaultKey = key;
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060002E2 RID: 738 RVA: 0x00003E18 File Offset: 0x00002018
		// (remove) Token: 0x060002E3 RID: 739 RVA: 0x00003E51 File Offset: 0x00002051
		public event EventHandler<O9K.Core.Managers.Menu.EventArgs.KeyEventArgs> ValueChange
		{
			add
			{
				if (this.isActive)
				{
					value(this, new O9K.Core.Managers.Menu.EventArgs.KeyEventArgs(this.isActive, this.isActive));
				}
				this.valueChange = (EventHandler<O9K.Core.Managers.Menu.EventArgs.KeyEventArgs>)Delegate.Combine(this.valueChange, value);
			}
			remove
			{
				this.valueChange = (EventHandler<O9K.Core.Managers.Menu.EventArgs.KeyEventArgs>)Delegate.Remove(this.valueChange, value);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00003E6A File Offset: 0x0000206A
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00019E38 File Offset: 0x00018038
		public Key Key
		{
			get
			{
				return this.keyValue;
			}
			set
			{
				this.keyValue = value;
				if (base.SizeCalculated)
				{
					this.keyText = this.keyValue.ToString();
					this.keyTextSize = base.Renderer.MeasureText(this.keyText, base.MenuStyle.TextSize, base.MenuStyle.Font);
				}
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00003E72 File Offset: 0x00002072
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00019E98 File Offset: 0x00018098
		public MouseKey MouseKey
		{
			get
			{
				return this.mouseKeyValue;
			}
			set
			{
				this.mouseKeyValue = value;
				if (base.SizeCalculated)
				{
					this.keyText = this.mouseKeyValue.ToString();
					this.keyTextSize = base.Renderer.MeasureText(this.keyText, base.MenuStyle.TextSize, base.MenuStyle.Font);
				}
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00003E7A File Offset: 0x0000207A
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00003E82 File Offset: 0x00002082
		public bool IsActive
		{
			get
			{
				return this.isActive;
			}
			set
			{
				if (this.isActive == value)
				{
					return;
				}
				this.isActive = value;
				EventHandler<O9K.Core.Managers.Menu.EventArgs.KeyEventArgs> eventHandler = this.valueChange;
				if (eventHandler == null)
				{
					return;
				}
				eventHandler(this, new O9K.Core.Managers.Menu.EventArgs.KeyEventArgs(this.isActive, !this.isActive));
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00003EBA File Offset: 0x000020BA
		public static implicit operator bool(MenuToggleKey item)
		{
			return item.IsActive;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000344F File Offset: 0x0000164F
		public MenuToggleKey SetTooltip(string tooltip)
		{
			base.Tooltip = tooltip;
			return this;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000037B6 File Offset: 0x000019B6
		internal override void CalculateSize()
		{
			base.CalculateSize();
			base.Size = new Vector2(base.Size.X + 40f, base.Size.Y);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00019EF8 File Offset: 0x000180F8
		internal override object GetSaveValue()
		{
			if (this.MouseKey != MouseKey.None)
			{
				return new
				{
					this.MouseKey,
					this.IsActive
				};
			}
			if (this.defaultKey == this.keyValue && this.defaultValue == this.isActive)
			{
				return null;
			}
			return new
			{
				Key = this.Key,
				IsActive = (base.SaveValue ? this.IsActive : this.defaultValue)
			};
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00019F60 File Offset: 0x00018160
		internal override void Load(JToken token)
		{
			try
			{
				token = ((token != null) ? token[base.Name] : null);
				if (token != null)
				{
					this.IsActive = token["IsActive"].ToObject<bool>();
					JToken jtoken = token["Key"];
					if (jtoken != null)
					{
						this.Key = jtoken.ToObject<Key>();
						if (this.Key != Key.None)
						{
							base.InputManager.KeyUp += this.OnKeyUp;
						}
					}
					else
					{
						JToken jtoken2 = token["MouseKey"];
						if (jtoken2 != null)
						{
							this.MouseKey = jtoken2.ToObject<MouseKey>();
							if (this.MouseKey != MouseKey.None)
							{
								base.InputManager.MouseKeyUp += this.MouseKeyUp;
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0001A030 File Offset: 0x00018230
		internal override bool OnMouseRelease(Vector2 position)
		{
			if (this.changingKey)
			{
				return true;
			}
			this.Remove();
			this.changingKey = true;
			base.InputManager.KeyUp += this.GetKey;
			base.InputManager.MouseKeyUp += this.GetMouseKey;
			return true;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00003EC2 File Offset: 0x000020C2
		internal override void Remove()
		{
			base.InputManager.KeyUp -= this.OnKeyUp;
			base.InputManager.MouseKeyUp -= this.MouseKeyUp;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001A084 File Offset: 0x00018284
		internal override void SetInputManager(IInputManager9 inputManager)
		{
			base.SetInputManager(inputManager);
			if (this.keyValue != Key.None)
			{
				base.InputManager.KeyUp += this.OnKeyUp;
				return;
			}
			if (this.mouseKeyValue != MouseKey.None)
			{
				base.InputManager.MouseKeyUp += this.MouseKeyUp;
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00003EF2 File Offset: 0x000020F2
		internal override void SetRenderer(IRendererManager9 renderer)
		{
			base.SetRenderer(renderer);
			this.keyTextSize = base.Renderer.MeasureText(this.keyText, base.MenuStyle.TextSize, base.MenuStyle.Font);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0001A0D8 File Offset: 0x000182D8
		protected override void Draw()
		{
			Vector2 position;
			position..ctor(base.Position.X + base.Size.X - base.MenuStyle.RightIndent - this.keyTextSize.X, base.Position.Y + (base.Size.Y - base.MenuStyle.TextSize) / 3.3f);
			if (this.IsActive)
			{
				base.Renderer.DrawLine(base.Position + new Vector2(base.Size.X - (this.keyTextSize.X + base.MenuStyle.RightIndent * 2f), base.Size.Y / 2f), base.Position + new Vector2(base.Size.X, base.Size.Y / 2f), base.MenuStyle.BackgroundColor, base.Size.Y);
			}
			base.Draw();
			base.Renderer.DrawText(position, this.changingKey ? "?" : this.keyText, Color.White, base.MenuStyle.TextSize, base.MenuStyle.Font);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0001A228 File Offset: 0x00018428
		private void GetKey(object sender, O9K.Core.Managers.Input.EventArgs.KeyEventArgs e)
		{
			this.Key = ((e.Key == Key.Escape) ? Key.None : e.Key);
			this.mouseKeyValue = MouseKey.None;
			e.Process = false;
			base.InputManager.KeyUp -= this.GetKey;
			base.InputManager.MouseKeyUp -= this.GetMouseKey;
			if (this.Key != Key.None)
			{
				base.InputManager.KeyUp += this.OnKeyUp;
			}
			this.changingKey = false;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001A2B0 File Offset: 0x000184B0
		private void GetMouseKey(object sender, MouseEventArgs e)
		{
			if (e.MouseKey == MouseKey.Left || e.MouseKey == MouseKey.Right)
			{
				this.keyValue = Key.None;
				this.MouseKey = MouseKey.None;
			}
			else
			{
				this.keyValue = Key.None;
				this.MouseKey = e.MouseKey;
			}
			e.Process = false;
			base.InputManager.KeyUp -= this.GetKey;
			base.InputManager.MouseKeyUp -= this.GetMouseKey;
			if (this.MouseKey != MouseKey.None)
			{
				base.InputManager.MouseKeyUp += this.MouseKeyUp;
			}
			this.changingKey = false;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00003F28 File Offset: 0x00002128
		private void MouseKeyUp(object sender, MouseEventArgs e)
		{
			if (e.MouseKey == this.mouseKeyValue)
			{
				this.IsActive = !this.IsActive;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00003F47 File Offset: 0x00002147
		private void OnKeyUp(object sender, O9K.Core.Managers.Input.EventArgs.KeyEventArgs e)
		{
			if (e.Key == this.keyValue)
			{
				this.IsActive = !this.IsActive;
			}
		}

		// Token: 0x04000139 RID: 313
		private readonly Key defaultKey;

		// Token: 0x0400013A RID: 314
		private readonly bool defaultValue;

		// Token: 0x0400013B RID: 315
		private bool changingKey;

		// Token: 0x0400013C RID: 316
		private bool isActive;

		// Token: 0x0400013D RID: 317
		private string keyText;

		// Token: 0x0400013E RID: 318
		private Vector2 keyTextSize;

		// Token: 0x0400013F RID: 319
		private Key keyValue;

		// Token: 0x04000140 RID: 320
		private MouseKey mouseKeyValue;

		// Token: 0x04000141 RID: 321
		private EventHandler<O9K.Core.Managers.Menu.EventArgs.KeyEventArgs> valueChange;
	}
}
