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
	// Token: 0x0200004D RID: 77
	public class MenuHoldKey : MenuItem
	{
		// Token: 0x0600025C RID: 604 RVA: 0x00003754 File Offset: 0x00001954
		public MenuHoldKey(string displayName, Key key = Key.None, bool heroUnique = false) : this(displayName, displayName, key, heroUnique)
		{
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00003760 File Offset: 0x00001960
		public MenuHoldKey(string displayName, string name, Key key = Key.None, bool heroUnique = false) : base(displayName, name, heroUnique)
		{
			this.keyText = key.ToString();
			this.keyValue = key;
			this.defaultKey = key;
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600025E RID: 606 RVA: 0x00018370 File Offset: 0x00016570
		// (remove) Token: 0x0600025F RID: 607 RVA: 0x000183A8 File Offset: 0x000165A8
		public event EventHandler<O9K.Core.Managers.Menu.EventArgs.KeyEventArgs> ValueChange;

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000378D File Offset: 0x0000198D
		// (set) Token: 0x06000261 RID: 609 RVA: 0x000183E0 File Offset: 0x000165E0
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

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00003795 File Offset: 0x00001995
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00018440 File Offset: 0x00016640
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

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000379D File Offset: 0x0000199D
		// (set) Token: 0x06000265 RID: 613 RVA: 0x000037A5 File Offset: 0x000019A5
		public bool IsActive { get; private set; }

		// Token: 0x06000266 RID: 614 RVA: 0x000037AE File Offset: 0x000019AE
		public static implicit operator bool(MenuHoldKey item)
		{
			return item.IsActive;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000344F File Offset: 0x0000164F
		public MenuHoldKey SetTooltip(string tooltip)
		{
			base.Tooltip = tooltip;
			return this;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000037B6 File Offset: 0x000019B6
		internal override void CalculateSize()
		{
			base.CalculateSize();
			base.Size = new Vector2(base.Size.X + 40f, base.Size.Y);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000037E5 File Offset: 0x000019E5
		internal override object GetSaveValue()
		{
			if (this.MouseKey != MouseKey.None)
			{
				return new
				{
					this.MouseKey
				};
			}
			if (this.Key != Key.None && this.Key != this.defaultKey)
			{
				return new
				{
					this.Key
				};
			}
			return null;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000184A0 File Offset: 0x000166A0
		internal override void Load(JToken token)
		{
			try
			{
				token = ((token != null) ? token[base.Name] : null);
				if (token != null)
				{
					JToken jtoken = token["Key"];
					if (jtoken != null)
					{
						this.Key = jtoken.ToObject<Key>();
						if (this.Key != Key.None)
						{
							base.InputManager.KeyDown += this.OnKeyDown;
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
								base.InputManager.MouseKeyDown += this.OnMouseKeyDown;
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

		// Token: 0x0600026B RID: 619 RVA: 0x00018588 File Offset: 0x00016788
		internal override bool OnMouseRelease(Vector2 position)
		{
			if (this.changingKey)
			{
				return true;
			}
			if (this.IsActive)
			{
				this.IsActive = false;
				EventHandler<O9K.Core.Managers.Menu.EventArgs.KeyEventArgs> valueChange = this.ValueChange;
				if (valueChange != null)
				{
					valueChange(this, new O9K.Core.Managers.Menu.EventArgs.KeyEventArgs(false, true));
				}
			}
			this.Remove();
			this.changingKey = true;
			base.InputManager.KeyUp += this.GetKey;
			base.InputManager.MouseKeyUp += this.GetMouseKey;
			return true;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00018604 File Offset: 0x00016804
		internal override void Remove()
		{
			if (base.InputManager == null)
			{
				return;
			}
			base.InputManager.KeyDown -= this.OnKeyDown;
			base.InputManager.KeyUp -= this.OnKeyUp;
			base.InputManager.MouseKeyDown -= this.OnMouseKeyDown;
			base.InputManager.MouseKeyUp -= this.MouseKeyUp;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00018678 File Offset: 0x00016878
		internal override void SetInputManager(IInputManager9 inputManager)
		{
			base.SetInputManager(inputManager);
			if (this.keyValue != Key.None)
			{
				base.InputManager.KeyDown += this.OnKeyDown;
				base.InputManager.KeyUp += this.OnKeyUp;
				return;
			}
			if (this.mouseKeyValue != MouseKey.None)
			{
				base.InputManager.MouseKeyDown += this.OnMouseKeyDown;
				base.InputManager.MouseKeyUp += this.MouseKeyUp;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000381E File Offset: 0x00001A1E
		internal override void SetRenderer(IRendererManager9 renderer)
		{
			base.SetRenderer(renderer);
			this.keyTextSize = base.Renderer.MeasureText(this.keyText, base.MenuStyle.TextSize, base.MenuStyle.Font);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000186FC File Offset: 0x000168FC
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

		// Token: 0x06000270 RID: 624 RVA: 0x0001884C File Offset: 0x00016A4C
		private void GetKey(object sender, O9K.Core.Managers.Input.EventArgs.KeyEventArgs e)
		{
			this.Key = ((e.Key == Key.Escape) ? Key.None : e.Key);
			this.mouseKeyValue = MouseKey.None;
			e.Process = false;
			base.InputManager.KeyUp -= this.GetKey;
			base.InputManager.MouseKeyUp -= this.GetMouseKey;
			if (this.Key != Key.None)
			{
				base.InputManager.KeyDown += this.OnKeyDown;
				base.InputManager.KeyUp += this.OnKeyUp;
			}
			this.changingKey = false;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000188EC File Offset: 0x00016AEC
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
				base.InputManager.MouseKeyDown += this.OnMouseKeyDown;
				base.InputManager.MouseKeyUp += this.MouseKeyUp;
			}
			this.changingKey = false;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00003854 File Offset: 0x00001A54
		private void MouseKeyUp(object sender, MouseEventArgs e)
		{
			if (e.MouseKey != this.mouseKeyValue)
			{
				return;
			}
			this.IsActive = false;
			EventHandler<O9K.Core.Managers.Menu.EventArgs.KeyEventArgs> valueChange = this.ValueChange;
			if (valueChange == null)
			{
				return;
			}
			valueChange(this, new O9K.Core.Managers.Menu.EventArgs.KeyEventArgs(false, true));
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00003884 File Offset: 0x00001A84
		private void OnKeyDown(object sender, O9K.Core.Managers.Input.EventArgs.KeyEventArgs e)
		{
			if (e.Key != this.keyValue)
			{
				return;
			}
			this.IsActive = true;
			EventHandler<O9K.Core.Managers.Menu.EventArgs.KeyEventArgs> valueChange = this.ValueChange;
			if (valueChange == null)
			{
				return;
			}
			valueChange(this, new O9K.Core.Managers.Menu.EventArgs.KeyEventArgs(true, false));
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000038B4 File Offset: 0x00001AB4
		private void OnKeyUp(object sender, O9K.Core.Managers.Input.EventArgs.KeyEventArgs e)
		{
			if (e.Key != this.keyValue)
			{
				return;
			}
			this.IsActive = false;
			EventHandler<O9K.Core.Managers.Menu.EventArgs.KeyEventArgs> valueChange = this.ValueChange;
			if (valueChange == null)
			{
				return;
			}
			valueChange(this, new O9K.Core.Managers.Menu.EventArgs.KeyEventArgs(false, true));
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000038E4 File Offset: 0x00001AE4
		private void OnMouseKeyDown(object sender, MouseEventArgs e)
		{
			if (e.MouseKey != this.mouseKeyValue)
			{
				return;
			}
			this.IsActive = true;
			EventHandler<O9K.Core.Managers.Menu.EventArgs.KeyEventArgs> valueChange = this.ValueChange;
			if (valueChange == null)
			{
				return;
			}
			valueChange(this, new O9K.Core.Managers.Menu.EventArgs.KeyEventArgs(true, false));
		}

		// Token: 0x04000114 RID: 276
		private readonly Key defaultKey;

		// Token: 0x04000115 RID: 277
		private bool changingKey;

		// Token: 0x04000116 RID: 278
		private string keyText;

		// Token: 0x04000117 RID: 279
		private Vector2 keyTextSize;

		// Token: 0x04000118 RID: 280
		private Key keyValue;

		// Token: 0x04000119 RID: 281
		private MouseKey mouseKeyValue;
	}
}
