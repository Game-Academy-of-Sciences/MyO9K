using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Ensage;
using Newtonsoft.Json.Linq;
using O9K.Core.Entities.Heroes;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Core.Managers.Menu.Items
{
	// Token: 0x0200004B RID: 75
	public class MenuHeroToggler : MenuItem
	{
		// Token: 0x0600021F RID: 543 RVA: 0x000035D8 File Offset: 0x000017D8
		public MenuHeroToggler(string displayName, bool allies = false, bool enemies = false, bool defaultValue = true, bool heroUnique = false) : this(displayName, displayName, allies, enemies, defaultValue, heroUnique)
		{
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00017584 File Offset: 0x00015784
		public MenuHeroToggler(string displayName, string name, bool allies = false, bool enemies = false, bool defaultValue = true, bool heroUnique = false) : base(displayName, name, heroUnique)
		{
			this.allies = allies;
			this.enemies = enemies;
			this.defaultValue = defaultValue;
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000221 RID: 545 RVA: 0x000175D4 File Offset: 0x000157D4
		// (remove) Token: 0x06000222 RID: 546 RVA: 0x0001760C File Offset: 0x0001580C
		public event EventHandler<HeroEventArgs> ValueChange;

		// Token: 0x06000223 RID: 547 RVA: 0x00017644 File Offset: 0x00015844
		public bool IsEnabled(string name)
		{
			bool result;
			this.heroes.TryGetValue(name, out result);
			return result;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000344F File Offset: 0x0000164F
		public MenuHeroToggler SetTooltip(string tooltip)
		{
			base.Tooltip = tooltip;
			return this;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00017664 File Offset: 0x00015864
		internal override void CalculateSize()
		{
			base.DisplayNameSize = base.Renderer.MeasureText(base.DisplayName, base.MenuStyle.TextSize, base.MenuStyle.Font);
			float num = base.DisplayNameSize.X + base.MenuStyle.LeftIndent + base.MenuStyle.RightIndent + 10f + base.MenuStyle.TextureArrowSize * 2f + (float)this.heroes.Count * base.MenuStyle.TextureHeroSize.X;
			base.Size = new Vector2(num, base.MenuStyle.Height);
			base.SizeCalculated = true;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00017718 File Offset: 0x00015918
		internal override object GetSaveValue()
		{
			foreach (KeyValuePair<string, bool> keyValuePair in this.heroes)
			{
				this.savedHeroes[keyValuePair.Key] = keyValuePair.Value;
			}
			return this.savedHeroes;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00017784 File Offset: 0x00015984
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
							this.savedHeroes[key] = value;
							if (this.heroes.ContainsKey(key))
							{
								this.heroes[key] = value;
							}
						}
					}
					catch
					{
						foreach (JToken jtoken in token.ToObject<JArray>())
						{
							string key2 = jtoken.ToString();
							this.savedHeroes[key2] = !this.defaultValue;
							if (this.heroes.ContainsKey(key2))
							{
								this.heroes[key2] = !this.defaultValue;
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

		// Token: 0x06000228 RID: 552 RVA: 0x000178C8 File Offset: 0x00015AC8
		internal override bool OnMouseRelease(Vector2 position)
		{
			if (this.heroes.Count == 0)
			{
				return false;
			}
			Vector2 vector;
			vector..ctor(base.Position.X + base.Size.X - base.MenuStyle.TextureHeroSize.X - 4f, base.Position.Y + (base.Size.Y - base.MenuStyle.TextureHeroSize.Y) / 2.2f);
			foreach (KeyValuePair<string, bool> keyValuePair in this.heroes.Reverse<KeyValuePair<string, bool>>())
			{
				RectangleF rectangleF;
				rectangleF..ctor(vector.X - 1.5f, vector.Y - 1.5f, base.MenuStyle.TextureHeroSize.X + 3f, base.MenuStyle.TextureHeroSize.Y + 3f);
				if (rectangleF.Contains(position))
				{
					bool flag = this.heroes[keyValuePair.Key];
					this.heroes[keyValuePair.Key] = !flag;
					EventHandler<HeroEventArgs> valueChange = this.ValueChange;
					if (valueChange != null)
					{
						valueChange(this, new HeroEventArgs(keyValuePair.Key, !flag, flag));
					}
					return true;
				}
				vector -= new Vector2(base.MenuStyle.TextureHeroSize.X + 4f, 0f);
			}
			return false;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00017A60 File Offset: 0x00015C60
		internal override void SetRenderer(IRendererManager9 renderer)
		{
			base.SetRenderer(renderer);
			foreach (HeroId heroId in this.loadTextures)
			{
				base.Renderer.TextureManager.LoadFromDota(heroId, false, false);
			}
			EntityManager9.UnitAdded += this.OnUnitAdded;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00017AD8 File Offset: 0x00015CD8
		protected override void Draw()
		{
			base.Draw();
			Vector2 vector;
			vector..ctor(base.Position.X + base.Size.X - base.MenuStyle.TextureHeroSize.X - 4f, base.Position.Y + (base.Size.Y - base.MenuStyle.TextureHeroSize.Y) / 2.2f);
			foreach (KeyValuePair<string, bool> keyValuePair in this.heroes.Reverse<KeyValuePair<string, bool>>())
			{
				base.Renderer.DrawFilledRectangle(new RectangleF(vector.X - 1.5f, vector.Y - 1.5f, base.MenuStyle.TextureHeroSize.X + 3f, base.MenuStyle.TextureHeroSize.Y + 3f), keyValuePair.Value ? Color.LightGreen : Color.Red);
				base.Renderer.DrawTexture(keyValuePair.Key, new RectangleF(vector.X, vector.Y, base.MenuStyle.TextureHeroSize.X, base.MenuStyle.TextureHeroSize.Y), 0f, 1f);
				vector -= new Vector2(base.MenuStyle.TextureHeroSize.X + 4f, 0f);
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00017C70 File Offset: 0x00015E70
		private void AddHero(HeroId id)
		{
			string key = id.ToString();
			if (this.heroes.ContainsKey(key))
			{
				return;
			}
			if (base.Renderer == null)
			{
				this.loadTextures.Add(id);
			}
			else
			{
				base.Renderer.TextureManager.LoadFromDota(id, false, false);
			}
			bool value;
			if (this.savedHeroes.TryGetValue(key, out value))
			{
				this.heroes[key] = value;
			}
			else
			{
				this.heroes[key] = this.defaultValue;
			}
			if (base.SizeCalculated)
			{
				float num = base.DisplayNameSize.X + base.MenuStyle.LeftIndent + base.MenuStyle.RightIndent + 10f + base.MenuStyle.TextureArrowSize * 2f + (float)this.heroes.Count * base.MenuStyle.TextureHeroSize.X;
				base.Size = new Vector2(num, base.MenuStyle.Height);
				base.ParentMenu.CalculateWidth(false);
			}
			if (this.heroes.Count >= 5)
			{
				EntityManager9.UnitAdded -= this.OnUnitAdded;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00017D98 File Offset: 0x00015F98
		private void OnUnitAdded(Unit9 entity)
		{
			try
			{
				if (!entity.IsIllusion)
				{
					Hero9 hero;
					if ((hero = (entity as Hero9)) != null)
					{
						if ((this.allies && hero.IsAlly()) || (this.enemies && hero.IsEnemy()))
						{
							this.AddHero(hero.Id);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x040000FA RID: 250
		private readonly bool allies;

		// Token: 0x040000FB RID: 251
		private readonly bool defaultValue;

		// Token: 0x040000FC RID: 252
		private readonly bool enemies;

		// Token: 0x040000FD RID: 253
		private readonly Dictionary<string, bool> heroes = new Dictionary<string, bool>();

		// Token: 0x040000FE RID: 254
		private readonly List<HeroId> loadTextures = new List<HeroId>();

		// Token: 0x040000FF RID: 255
		private readonly Dictionary<string, bool> savedHeroes = new Dictionary<string, bool>();
	}
}
