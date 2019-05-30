using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using Newtonsoft.Json.Linq;
using O9K.Core.Logger;
using O9K.Core.Managers.Input.EventArgs;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Core.Managers.Menu.Items
{
	// Token: 0x02000045 RID: 69
	public class MenuAbilityPriorityChanger : MenuItem
	{
		// Token: 0x060001DE RID: 478 RVA: 0x000033C4 File Offset: 0x000015C4
		public MenuAbilityPriorityChanger(string displayName, IDictionary<AbilityId, bool> abilities = null, bool defaultValue = true, bool heroUnique = false) : this(displayName, displayName, abilities, defaultValue, heroUnique)
		{
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000159E4 File Offset: 0x00013BE4
		public MenuAbilityPriorityChanger(string displayName, string name, IDictionary<AbilityId, bool> abilities = null, bool defaultValue = true, bool heroUnique = false) : base(displayName, name, heroUnique)
		{
			this.defaultValue = defaultValue;
			if (abilities == null)
			{
				return;
			}
			foreach (KeyValuePair<AbilityId, bool> keyValuePair in abilities)
			{
				string text = keyValuePair.Key.ToString();
				this.abilities[text] = keyValuePair.Value;
				Dictionary<string, int> dictionary = this.abilityPriority;
				string key = text;
				int num = this.autoPriority;
				this.autoPriority = num + 1;
				dictionary[key] = num;
				this.loadTextures.Add(keyValuePair.Key);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060001E0 RID: 480 RVA: 0x000033D2 File Offset: 0x000015D2
		// (remove) Token: 0x060001E1 RID: 481 RVA: 0x000033F7 File Offset: 0x000015F7
		public event EventHandler<EventArgs> Change
		{
			add
			{
				value(this, EventArgs.Empty);
				this.change = (EventHandler<EventArgs>)Delegate.Combine(this.change, value);
			}
			remove
			{
				this.change = (EventHandler<EventArgs>)Delegate.Remove(this.change, value);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060001E2 RID: 482 RVA: 0x00015AD0 File Offset: 0x00013CD0
		// (remove) Token: 0x060001E3 RID: 483 RVA: 0x00015B08 File Offset: 0x00013D08
		public event EventHandler<AbilityPriorityEventArgs> OrderChange;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060001E4 RID: 484 RVA: 0x00015B40 File Offset: 0x00013D40
		// (remove) Token: 0x060001E5 RID: 485 RVA: 0x00015B78 File Offset: 0x00013D78
		public event EventHandler<AbilityEventArgs> ValueChange;

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00015BB0 File Offset: 0x00013DB0
		public IEnumerable<string> Abilities
		{
			get
			{
				return from x in this.abilities
				where x.Value
				orderby this.abilityPriority[x.Key]
				select x.Key;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00003410 File Offset: 0x00001610
		public void AddAbility(AbilityId id, bool? value = null, int priority = 0)
		{
			if (base.Renderer == null)
			{
				this.loadTextures.Add(id);
			}
			else
			{
				base.Renderer.TextureManager.LoadFromDota(id, false);
			}
			this.AddAbility(id.ToString(), value, priority);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00015C1C File Offset: 0x00013E1C
		public void AddAbility(string name, bool? value = null, int priority = 0)
		{
			if (this.abilities.ContainsKey(name))
			{
				return;
			}
			bool value2;
			if (this.savedAbilities.TryGetValue(name, out value2))
			{
				this.abilities[name] = value2;
			}
			else
			{
				this.abilities[name] = (value ?? this.defaultValue);
			}
			int value3;
			if (this.savedAbilityPriority.TryGetValue(name, out value3))
			{
				this.abilityPriority[name] = value3;
			}
			else if (priority != 0)
			{
				this.abilityPriority[name] = this.TryGetPriority(priority);
			}
			else
			{
				Dictionary<string, int> dictionary = this.abilityPriority;
				int num = this.autoPriority;
				this.autoPriority = num + 1;
				dictionary[name] = num;
			}
			if (base.SizeCalculated)
			{
				float num2 = base.DisplayNameSize.X + base.MenuStyle.LeftIndent + base.MenuStyle.RightIndent + 10f + base.MenuStyle.TextureArrowSize * 2f + (float)this.abilities.Count * base.MenuStyle.TextureAbilitySize;
				base.Size = new Vector2(num2, base.MenuStyle.Height);
				base.ParentMenu.CalculateWidth(false);
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00015D54 File Offset: 0x00013F54
		public int GetPriority(string name)
		{
			int result;
			if (this.abilityPriority.TryGetValue(name, out result))
			{
				return result;
			}
			return 99999;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00015D78 File Offset: 0x00013F78
		public bool IsEnabled(string name)
		{
			bool result;
			this.abilities.TryGetValue(name, out result);
			return result;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000344F File Offset: 0x0000164F
		public MenuAbilityPriorityChanger SetTooltip(string tooltip)
		{
			base.Tooltip = tooltip;
			return this;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00015D98 File Offset: 0x00013F98
		internal override void CalculateSize()
		{
			base.DisplayNameSize = base.Renderer.MeasureText(base.DisplayName, base.MenuStyle.TextSize, base.MenuStyle.Font);
			float num = base.DisplayNameSize.X + base.MenuStyle.LeftIndent + base.MenuStyle.RightIndent + 10f + base.MenuStyle.TextureArrowSize * 2f + (float)this.abilities.Count * base.MenuStyle.TextureAbilitySize;
			base.Size = new Vector2(num, base.MenuStyle.Height);
			base.SizeCalculated = true;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00003459 File Offset: 0x00001659
		internal override MenuItem GetItemUnder(Vector2 position)
		{
			if (this.drag)
			{
				return this;
			}
			return base.GetItemUnder(position);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00015E48 File Offset: 0x00014048
		internal override object GetSaveValue()
		{
			foreach (KeyValuePair<string, bool> keyValuePair in this.abilities)
			{
				this.savedAbilities[keyValuePair.Key] = keyValuePair.Value;
			}
			foreach (KeyValuePair<string, int> keyValuePair2 in this.abilityPriority)
			{
				this.savedAbilityPriority[keyValuePair2.Key] = keyValuePair2.Value;
			}
			return new
			{
				Abilities = this.savedAbilities,
				Priority = this.savedAbilityPriority
			};
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00015F14 File Offset: 0x00014114
		internal override void Load(JToken token)
		{
			try
			{
				token = ((token != null) ? token[base.Name] : null);
				if (token != null)
				{
					try
					{
						foreach (KeyValuePair<string, JToken> keyValuePair in token["Abilities"].ToObject<JObject>())
						{
							string key = keyValuePair.Key;
							bool value = (bool)keyValuePair.Value;
							this.savedAbilities[key] = value;
							if (this.abilities.ContainsKey(key))
							{
								this.abilities[key] = value;
							}
						}
					}
					catch
					{
						foreach (JToken jtoken in token["Abilities"].ToObject<JArray>())
						{
							string key2 = jtoken.ToString();
							this.savedAbilities[key2] = !this.defaultValue;
							if (this.abilities.ContainsKey(key2))
							{
								this.abilities[key2] = !this.defaultValue;
							}
						}
					}
					foreach (KeyValuePair<string, JToken> keyValuePair2 in token["Priority"].ToObject<JObject>())
					{
						string key3 = keyValuePair2.Key;
						int num = (int)keyValuePair2.Value;
						this.savedAbilityPriority[key3] = num;
						if (num >= this.autoPriority)
						{
							this.autoPriority = num + 1;
						}
						if (this.abilityPriority.ContainsKey(key3))
						{
							this.abilityPriority[key3] = num;
						}
					}
					EventHandler<EventArgs> eventHandler = this.change;
					if (eventHandler != null)
					{
						eventHandler(this, EventArgs.Empty);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00016158 File Offset: 0x00014358
		internal override bool OnMousePress(Vector2 position)
		{
			if (this.abilities.Count == 0)
			{
				return false;
			}
			Vector2 vector;
			vector..ctor(base.Position.X + base.Size.X - base.MenuStyle.TextureAbilitySize - 4f, base.Position.Y + (base.Size.Y - base.MenuStyle.TextureAbilitySize) / 2.2f);
			foreach (KeyValuePair<string, bool> keyValuePair in from x in this.abilities
			orderby this.abilityPriority[x.Key] descending
			select x)
			{
				RectangleF rectangleF;
				rectangleF..ctor(vector.X - 1.5f, vector.Y - 1.5f, base.MenuStyle.TextureAbilitySize + 3f, base.MenuStyle.TextureAbilitySize + 3f);
				if (rectangleF.Contains(position))
				{
					this.currentMousePosition = position;
					this.mousePressPosition = position;
					this.mousePressDiff = position - vector;
					this.dragAbilityPosition = position - this.mousePressDiff;
					this.dragAbility = keyValuePair.Key;
					this.drag = true;
					base.InputManager.MouseMove += this.OnMouseMove;
					base.InputManager.MouseKeyUp += this.OnMouseKeyUp;
					return true;
				}
				vector -= new Vector2(base.MenuStyle.TextureAbilitySize + 4f, 0f);
			}
			return false;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00016304 File Offset: 0x00014504
		internal override bool OnMouseRelease(Vector2 position)
		{
			if (this.abilities.Count == 0)
			{
				return false;
			}
			if (!this.drawDrag)
			{
				Vector2 vector;
				vector..ctor(base.Position.X + base.Size.X - base.MenuStyle.TextureAbilitySize - 4f, base.Position.Y + (base.Size.Y - base.MenuStyle.TextureAbilitySize) / 2.2f);
				foreach (KeyValuePair<string, bool> keyValuePair in from x in this.abilities
				orderby this.abilityPriority[x.Key] descending
				select x)
				{
					RectangleF rectangleF;
					rectangleF..ctor(vector.X - 1.5f, vector.Y - 1.5f, base.MenuStyle.TextureAbilitySize + 3f, base.MenuStyle.TextureAbilitySize + 3f);
					if (rectangleF.Contains(position))
					{
						bool flag = this.abilities[keyValuePair.Key];
						this.abilities[keyValuePair.Key] = !flag;
						EventHandler<AbilityEventArgs> valueChange = this.ValueChange;
						if (valueChange != null)
						{
							valueChange(this, new AbilityEventArgs(keyValuePair.Key, !flag, flag));
						}
						EventHandler<EventArgs> eventHandler = this.change;
						if (eventHandler != null)
						{
							eventHandler(this, EventArgs.Empty);
						}
						return true;
					}
					vector -= new Vector2(base.MenuStyle.TextureAbilitySize + 4f, 0f);
				}
				return false;
			}
			this.drag = false;
			this.drawDrag = false;
			if (string.IsNullOrEmpty(this.dragTargetAbility) || this.dragTargetAbility == this.dragAbility)
			{
				return false;
			}
			int oldValue = this.abilityPriority[this.dragAbility];
			int setPriority;
			if (this.increasePriority)
			{
				setPriority = this.abilityPriority[this.dragTargetAbility] - 1;
				IEnumerable<KeyValuePair<string, int>> source = this.abilityPriority;
				Func<KeyValuePair<string, int>, bool> <>9__0;
				Func<KeyValuePair<string, int>, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((KeyValuePair<string, int> x) => x.Value <= setPriority));
				}
				foreach (string text in (from x in source.Where(predicate)
				select x.Key).ToList<string>())
				{
					Dictionary<string, int> dictionary = this.abilityPriority;
					string key = text;
					int num = dictionary[key];
					dictionary[key] = num - 1;
				}
				this.abilityPriority[this.dragAbility] = setPriority;
				this.increasePriority = false;
			}
			else
			{
				setPriority = this.abilityPriority[this.dragTargetAbility] + 1;
				IEnumerable<KeyValuePair<string, int>> source2 = this.abilityPriority;
				Func<KeyValuePair<string, int>, bool> <>9__2;
				Func<KeyValuePair<string, int>, bool> predicate2;
				if ((predicate2 = <>9__2) == null)
				{
					predicate2 = (<>9__2 = ((KeyValuePair<string, int> x) => x.Value >= setPriority));
				}
				foreach (string text2 in (from x in source2.Where(predicate2)
				select x.Key).ToList<string>())
				{
					Dictionary<string, int> dictionary2 = this.abilityPriority;
					string key = text2;
					int num = dictionary2[key];
					dictionary2[key] = num + 1;
				}
				this.abilityPriority[this.dragAbility] = setPriority;
			}
			this.autoPriority = this.abilityPriority.Values.Max() + 1;
			EventHandler<AbilityPriorityEventArgs> orderChange = this.OrderChange;
			if (orderChange != null)
			{
				orderChange(this, new AbilityPriorityEventArgs(this.dragAbility, setPriority, oldValue));
			}
			EventHandler<EventArgs> eventHandler2 = this.change;
			if (eventHandler2 != null)
			{
				eventHandler2(this, EventArgs.Empty);
			}
			return true;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000346C File Offset: 0x0000166C
		internal override void Remove()
		{
			if (base.InputManager == null)
			{
				return;
			}
			base.InputManager.MouseKeyUp -= this.OnMouseKeyUp;
			base.InputManager.MouseMove -= this.OnMouseMove;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00016720 File Offset: 0x00014920
		internal override void SetRenderer(IRendererManager9 renderer)
		{
			base.SetRenderer(renderer);
			foreach (AbilityId abilityId in this.loadTextures)
			{
				base.Renderer.TextureManager.LoadFromDota(abilityId, false);
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00016788 File Offset: 0x00014988
		protected override void Draw()
		{
			base.Draw();
			if (this.drawDrag)
			{
				base.Renderer.DrawTexture(this.dragAbility, new RectangleF(this.dragAbilityPosition.X, this.dragAbilityPosition.Y, base.MenuStyle.TextureAbilitySize, base.MenuStyle.TextureAbilitySize), 0f, 1f);
				this.dragTargetAbility = null;
			}
			Vector2 vector;
			vector..ctor(base.Position.X + base.Size.X - base.MenuStyle.TextureAbilitySize - 4f, base.Position.Y + (base.Size.Y - base.MenuStyle.TextureAbilitySize) / 2.2f);
			int num = this.abilities.Count((KeyValuePair<string, bool> x) => x.Value);
			int num2 = 0;
			foreach (KeyValuePair<string, bool> keyValuePair in from x in this.abilities
			orderby this.abilityPriority[x.Key] descending
			select x)
			{
				num2++;
				if (this.drawDrag)
				{
					int num3 = 3;
					if (keyValuePair.Key == this.dragAbility)
					{
						num3 = 0;
					}
					if ((num2 == 1 && this.currentMousePosition.X > base.Position.X + base.Size.X) || (this.currentMousePosition.X >= vector.X - (float)num3 && this.currentMousePosition.X <= vector.X + base.MenuStyle.TextureAbilitySize + (float)num3))
					{
						this.dragTargetAbility = keyValuePair.Key;
						vector -= new Vector2(base.MenuStyle.TextureAbilitySize + 4f, 0f);
						this.increasePriority = false;
					}
					if (keyValuePair.Key == this.dragAbility)
					{
						if (keyValuePair.Value)
						{
							num--;
							continue;
						}
						continue;
					}
				}
				base.Renderer.DrawRectangle(new RectangleF(vector.X - 1.5f, vector.Y - 1.5f, base.MenuStyle.TextureAbilitySize + 3f, base.MenuStyle.TextureAbilitySize + 3f), keyValuePair.Value ? Color.LightGreen : Color.Red, 1.5f);
				base.Renderer.DrawTexture(keyValuePair.Key, new RectangleF(vector.X, vector.Y, base.MenuStyle.TextureAbilitySize, base.MenuStyle.TextureAbilitySize), 0f, 1f);
				if (keyValuePair.Value)
				{
					base.Renderer.DrawLine(vector + new Vector2(0f, base.MenuStyle.TextureAbilitySize - 6f), vector + new Vector2(6f, base.MenuStyle.TextureAbilitySize - 6f), Color.Black, 12f);
					IRenderer renderer = base.Renderer;
					Vector2 position = vector + new Vector2(0f, base.MenuStyle.TextureAbilitySize - 12f);
					int num4 = num;
					num = num4 - 1;
					renderer.DrawText(position, num4.ToString(), Color.White, 12f, base.MenuStyle.Font);
				}
				vector -= new Vector2(base.MenuStyle.TextureAbilitySize + 4f, 0f);
			}
			if (this.drawDrag && this.dragTargetAbility == null)
			{
				this.dragTargetAbility = (from x in this.abilities
				select x.Key into x
				orderby this.abilityPriority[x]
				select x).FirstOrDefault<string>();
				this.increasePriority = true;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000034A5 File Offset: 0x000016A5
		private void OnMouseKeyUp(object sender, MouseEventArgs e)
		{
			this.drag = false;
			this.drawDrag = false;
			base.InputManager.MouseKeyUp -= this.OnMouseKeyUp;
			base.InputManager.MouseMove -= this.OnMouseMove;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00016B9C File Offset: 0x00014D9C
		private void OnMouseMove(object sender, MouseMoveEventArgs e)
		{
			this.currentMousePosition = e.ScreenPosition;
			this.dragAbilityPosition = e.ScreenPosition - this.mousePressDiff;
			this.drawDrag = (this.mousePressPosition.Distance2D(e.ScreenPosition, false) > 5f);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00016BEC File Offset: 0x00014DEC
		private int TryGetPriority(int priority)
		{
			while (this.abilityPriority.Values.Any((int x) => x == priority) || this.savedAbilityPriority.Values.Any((int x) => x == priority))
			{
				int priority2 = priority;
				priority = priority2 + 1;
			}
			if (priority >= this.autoPriority)
			{
				this.autoPriority = priority + 1;
			}
			return priority;
		}

		// Token: 0x040000D3 RID: 211
		private readonly Dictionary<string, bool> abilities = new Dictionary<string, bool>();

		// Token: 0x040000D4 RID: 212
		private readonly Dictionary<string, int> abilityPriority = new Dictionary<string, int>();

		// Token: 0x040000D5 RID: 213
		private readonly bool defaultValue;

		// Token: 0x040000D6 RID: 214
		private readonly List<AbilityId> loadTextures = new List<AbilityId>();

		// Token: 0x040000D7 RID: 215
		private readonly Dictionary<string, bool> savedAbilities = new Dictionary<string, bool>();

		// Token: 0x040000D8 RID: 216
		private readonly Dictionary<string, int> savedAbilityPriority = new Dictionary<string, int>();

		// Token: 0x040000D9 RID: 217
		private int autoPriority;

		// Token: 0x040000DA RID: 218
		private EventHandler<EventArgs> change;

		// Token: 0x040000DB RID: 219
		private Vector2 currentMousePosition;

		// Token: 0x040000DC RID: 220
		private bool drag;

		// Token: 0x040000DD RID: 221
		private string dragAbility;

		// Token: 0x040000DE RID: 222
		private Vector2 dragAbilityPosition;

		// Token: 0x040000DF RID: 223
		private string dragTargetAbility;

		// Token: 0x040000E0 RID: 224
		private bool drawDrag;

		// Token: 0x040000E1 RID: 225
		private bool increasePriority;

		// Token: 0x040000E2 RID: 226
		private Vector2 mousePressDiff;

		// Token: 0x040000E3 RID: 227
		private Vector2 mousePressPosition;
	}
}
