using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Ensage;
using Newtonsoft.Json.Linq;
using O9K.Core.Logger;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Core.Managers.Menu.Items
{
	// Token: 0x02000049 RID: 73
	public class MenuAbilityToggler : MenuItem
	{
		// Token: 0x0600020B RID: 523 RVA: 0x0000355F File Offset: 0x0000175F
		public MenuAbilityToggler(string displayName, IDictionary<AbilityId, bool> abilities = null, bool defaultValue = true, bool heroUnique = false) : this(displayName, displayName, abilities, defaultValue, heroUnique)
		{
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00016C78 File Offset: 0x00014E78
		public MenuAbilityToggler(string displayName, string name, IDictionary<AbilityId, bool> abilities = null, bool defaultValue = true, bool heroUnique = false) : base(displayName, name, heroUnique)
		{
			this.defaultValue = defaultValue;
			if (abilities == null)
			{
				return;
			}
			foreach (KeyValuePair<AbilityId, bool> keyValuePair in abilities)
			{
				this.abilities[keyValuePair.Key.ToString()] = keyValuePair.Value;
				this.loadTextures.Add(keyValuePair.Key);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600020D RID: 525 RVA: 0x00016D2C File Offset: 0x00014F2C
		// (remove) Token: 0x0600020E RID: 526 RVA: 0x0000356D File Offset: 0x0000176D
		public event EventHandler<AbilityEventArgs> ValueChange
		{
			add
			{
				if (this.loaded)
				{
					foreach (string ability in this.Abilities)
					{
						value(this, new AbilityEventArgs(ability, true, true));
					}
				}
				this.valueChange = (EventHandler<AbilityEventArgs>)Delegate.Combine(this.valueChange, value);
			}
			remove
			{
				this.valueChange = (EventHandler<AbilityEventArgs>)Delegate.Remove(this.valueChange, value);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00016DA0 File Offset: 0x00014FA0
		public IEnumerable<string> Abilities
		{
			get
			{
				return from x in this.abilities
				where x.Value
				select x.Key;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00003586 File Offset: 0x00001786
		public IReadOnlyDictionary<string, bool> AllAbilities
		{
			get
			{
				return this.abilities;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000358E File Offset: 0x0000178E
		public void AddAbility(AbilityId id, bool? value = null)
		{
			if (base.Renderer == null)
			{
				this.loadTextures.Add(id);
			}
			else
			{
				base.Renderer.TextureManager.LoadFromDota(id, false);
			}
			this.AddAbility(id.ToString(), value);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00016DFC File Offset: 0x00014FFC
		public void AddAbility(string name, bool? value = null)
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
			if (this.abilities[name])
			{
				EventHandler<AbilityEventArgs> eventHandler = this.valueChange;
				if (eventHandler != null)
				{
					eventHandler(this, new AbilityEventArgs(name, true, true));
				}
			}
			if (base.SizeCalculated)
			{
				float num = base.DisplayNameSize.X + base.MenuStyle.LeftIndent + base.MenuStyle.RightIndent + 10f + base.MenuStyle.TextureArrowSize * 2f + (float)this.abilities.Count * base.MenuStyle.TextureAbilitySize;
				base.Size = new Vector2(num, base.MenuStyle.Height);
				base.ParentMenu.CalculateWidth(false);
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00016F08 File Offset: 0x00015108
		public bool IsEnabled(string name)
		{
			bool result;
			this.abilities.TryGetValue(name, out result);
			return result;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000344F File Offset: 0x0000164F
		public MenuAbilityToggler SetTooltip(string tooltip)
		{
			base.Tooltip = tooltip;
			return this;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00016F28 File Offset: 0x00015128
		internal override void CalculateSize()
		{
			base.DisplayNameSize = base.Renderer.MeasureText(base.DisplayName, base.MenuStyle.TextSize, base.MenuStyle.Font);
			float num = base.DisplayNameSize.X + base.MenuStyle.LeftIndent + base.MenuStyle.RightIndent + 10f + base.MenuStyle.TextureArrowSize * 2f + (float)this.abilities.Count * base.MenuStyle.TextureAbilitySize;
			base.Size = new Vector2(num, base.MenuStyle.Height);
			base.SizeCalculated = true;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00016FD8 File Offset: 0x000151D8
		internal override object GetSaveValue()
		{
			foreach (KeyValuePair<string, bool> keyValuePair in this.abilities)
			{
				this.savedAbilities[keyValuePair.Key] = keyValuePair.Value;
			}
			return this.savedAbilities;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00017044 File Offset: 0x00015244
		internal override void Load(JToken token)
		{
			try
			{
				token = ((token != null) ? token[base.Name] : null);
				if (token != null)
				{
					try
					{
						foreach (KeyValuePair<string, JToken> keyValuePair in token.ToObject<JObject>())
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
						foreach (JToken jtoken in token.ToObject<JArray>())
						{
							string key2 = jtoken.ToString();
							this.savedAbilities[key2] = !this.defaultValue;
							if (this.abilities.ContainsKey(key2))
							{
								this.abilities[key2] = !this.defaultValue;
							}
						}
					}
					foreach (string ability in this.Abilities)
					{
						EventHandler<AbilityEventArgs> eventHandler = this.valueChange;
						if (eventHandler != null)
						{
							eventHandler(this, new AbilityEventArgs(ability, true, true));
						}
					}
					this.loaded = true;
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00017220 File Offset: 0x00015420
		internal override bool OnMouseRelease(Vector2 position)
		{
			if (this.abilities.Count == 0)
			{
				return false;
			}
			Vector2 vector;
			vector..ctor(base.Position.X + base.Size.X - base.MenuStyle.TextureAbilitySize - 4f, base.Position.Y + (base.Size.Y - base.MenuStyle.TextureAbilitySize) / 2.2f);
			foreach (KeyValuePair<string, bool> keyValuePair in this.abilities.Reverse<KeyValuePair<string, bool>>())
			{
				RectangleF rectangleF;
				rectangleF..ctor(vector.X - 1.5f, vector.Y - 1.5f, base.MenuStyle.TextureAbilitySize + 3f, base.MenuStyle.TextureAbilitySize + 3f);
				if (rectangleF.Contains(position))
				{
					bool flag = this.abilities[keyValuePair.Key];
					this.abilities[keyValuePair.Key] = !flag;
					EventHandler<AbilityEventArgs> eventHandler = this.valueChange;
					if (eventHandler != null)
					{
						eventHandler(this, new AbilityEventArgs(keyValuePair.Key, !flag, flag));
					}
					return true;
				}
				vector -= new Vector2(base.MenuStyle.TextureAbilitySize + 4f, 0f);
			}
			return false;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000173A0 File Offset: 0x000155A0
		internal override void SetRenderer(IRendererManager9 renderer)
		{
			base.SetRenderer(renderer);
			foreach (AbilityId abilityId in this.loadTextures)
			{
				base.Renderer.TextureManager.LoadFromDota(abilityId, false);
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00017408 File Offset: 0x00015608
		protected override void Draw()
		{
			base.Draw();
			Vector2 vector;
			vector..ctor(base.Position.X + base.Size.X - base.MenuStyle.TextureAbilitySize - 4f, base.Position.Y + (base.Size.Y - base.MenuStyle.TextureAbilitySize) / 2.2f);
			foreach (KeyValuePair<string, bool> keyValuePair in this.abilities.Reverse<KeyValuePair<string, bool>>())
			{
				base.Renderer.DrawRectangle(new RectangleF(vector.X - 1.5f, vector.Y - 1.5f, base.MenuStyle.TextureAbilitySize + 3f, base.MenuStyle.TextureAbilitySize + 3f), keyValuePair.Value ? Color.LightGreen : Color.Red, 1.5f);
				base.Renderer.DrawTexture(keyValuePair.Key, new RectangleF(vector.X, vector.Y, base.MenuStyle.TextureAbilitySize, base.MenuStyle.TextureAbilitySize), 0f, 1f);
				vector -= new Vector2(base.MenuStyle.TextureAbilitySize + 4f, 0f);
			}
		}

		// Token: 0x040000F1 RID: 241
		private readonly Dictionary<string, bool> abilities = new Dictionary<string, bool>();

		// Token: 0x040000F2 RID: 242
		private readonly bool defaultValue;

		// Token: 0x040000F3 RID: 243
		private readonly List<AbilityId> loadTextures = new List<AbilityId>();

		// Token: 0x040000F4 RID: 244
		private readonly Dictionary<string, bool> savedAbilities = new Dictionary<string, bool>();

		// Token: 0x040000F5 RID: 245
		private bool loaded;

		// Token: 0x040000F6 RID: 246
		private EventHandler<AbilityEventArgs> valueChange;
	}
}
