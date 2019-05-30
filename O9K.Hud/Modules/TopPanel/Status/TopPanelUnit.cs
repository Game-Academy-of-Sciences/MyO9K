using System;
using System.Collections.Generic;

using System.Linq;
using Ensage;
using Ensage.SDK.Renderer;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Heroes;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using SharpDX;

namespace O9K.Hud.Modules.TopPanel.Status
{
	// Token: 0x02000029 RID: 41
	internal class TopPanelUnit
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00008E1C File Offset: 0x0000701C
		public TopPanelUnit(Unit9 hero)
		{
			this.hero = (Hero9)hero;
			this.Handle = hero.Handle;
			this.IsAlly = hero.IsAlly();
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000027B5 File Offset: 0x000009B5
		public bool IsValid
		{
			get
			{
				return this.hero.IsValid;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000027C2 File Offset: 0x000009C2
		public uint Handle { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000027CA File Offset: 0x000009CA
		public bool IsAlly { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000027D2 File Offset: 0x000009D2
		public Sleeper BuybackSleeper { get; } = new Sleeper();

		// Token: 0x060000DB RID: 219 RVA: 0x000027DA File Offset: 0x000009DA
		public void AddItem(Ability9 ability)
		{
			this.items.Add(ability);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000027E8 File Offset: 0x000009E8
		public void AddModifier(Modifier modifier)
		{
			this.modifiers[modifier.TextureName] = modifier;
			this.modifiersTime[modifier.TextureName] = Game.RawGameTime + modifier.RemainingTime;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00008E80 File Offset: 0x00007080
		public void DrawAllyHealth(O9K.Core.Managers.Renderer.IRenderer renderer, RectangleF position)
		{
			if (!this.hero.IsAlive)
			{
				return;
			}
			renderer.DrawTexture("health_ally_bg", position, 0f, 1f);
			position.Width *= this.hero.HealthPercentageBase;
			renderer.DrawTexture(this.hero.IsVisibleToEnemies ? "health_ally_visible" : "health_ally", position, 0f, 1f);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008EF4 File Offset: 0x000070F4
		public void DrawAllyMana(O9K.Core.Managers.Renderer.IRenderer renderer, RectangleF position)
		{
			if (!this.hero.IsAlive)
			{
				return;
			}
			renderer.DrawTexture("mana_bg", position, 0f, 1f);
			position.Width *= this.hero.ManaPercentageBase;
			renderer.DrawTexture(this.hero.IsVisibleToEnemies ? "mana" : "mana_invis", position, 0f, 1f);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002819 File Offset: 0x00000A19
		public void DrawBuyback(O9K.Core.Managers.Renderer.IRenderer renderer, RectangleF position)
		{
			if (this.BuybackSleeper.IsSleeping || this.hero.IsAlive)
			{
				return;
			}
			renderer.DrawTexture("buyback", position, 0f, 1f);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00008F68 File Offset: 0x00007168
		public void DrawEnemyHealth(O9K.Core.Managers.Renderer.IRenderer renderer, RectangleF position)
		{
			if (!this.hero.IsAlive)
			{
				return;
			}
			renderer.DrawTexture("health_enemy_bg", position, 0f, 1f);
			position.Width *= this.hero.HealthPercentageBase;
			renderer.DrawTexture(this.hero.IsVisible ? "health_enemy" : "health_enemy_invis", position, 0f, 1f);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00008FDC File Offset: 0x000071DC
		public void DrawEnemyMana(O9K.Core.Managers.Renderer.IRenderer renderer, RectangleF position)
		{
			if (!this.hero.IsAlive)
			{
				return;
			}
			renderer.DrawTexture("mana_bg", position, 0f, 1f);
			position.Width *= this.hero.ManaPercentageBase;
			renderer.DrawTexture(this.hero.IsVisible ? "mana" : "mana_invis", position, 0f, 1f);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00009050 File Offset: 0x00007250
		public void DrawFowTime(O9K.Core.Managers.Renderer.IRenderer renderer, RectangleF position)
		{
			if (this.hero.IsVisible || !this.hero.IsAlive)
			{
				return;
			}
			if (this.hero.Team == Team.Radiant)
			{
				position.Width *= 0.88f;
			}
			int num = (int)(Game.RawGameTime - (this.hero.IsAlive ? this.hero.LastVisibleTime : this.hero.BaseHero.RespawnTime));
			renderer.DrawText(position, num.ToString(), System.Drawing.Color.White, RendererFontFlags.Right, 16f * O9K.Core.Helpers.Hud.Info.ScreenRatio, "Calibri");
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000090F0 File Offset: 0x000072F0
		public void DrawItems(O9K.Core.Managers.Renderer.IRenderer renderer, RectangleF position)
		{
			if (this.items.Count == 0 || !this.hero.IsAlive)
			{
				return;
			}

            Vector2 vector = new Vector2(position.X, position.Y);
			float num = position.Width * 0.3f;
			foreach (Ability9 ability in this.items)
			{
				if (ability.IsValid)
				{
					renderer.DrawTexture(ability.Id + "_rounded", vector, new Vector2(num), 0f, 1f);
					vector += new Vector2(num + 2f, 0f);
					if (vector.X + num > position.Right)
					{
						vector = new Vector2(position.X, position.Y + num + 2f);
					}
				}
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000091F8 File Offset: 0x000073F8
		public void DrawRunes(O9K.Core.Managers.Renderer.IRenderer renderer, RectangleF position)
		{
			float num = position.Width * 0.35f;
			Vector2 vector=new Vector2(position.X, position.Y - num);
			if (!this.hero.IsVisible)
			{
				foreach (KeyValuePair<string, float> keyValuePair in this.modifiersTime.ToList<KeyValuePair<string, float>>())
				{
					string key = keyValuePair.Key;
					float value = keyValuePair.Value;
					if (Game.RawGameTime > value)
					{
						this.modifiers.Remove(key);
						this.modifiersTime.Remove(key);
					}
					else
					{
						renderer.DrawTexture(key + "_rounded", vector, new Vector2(num), 0f, 1f);
						renderer.DrawTexture("outline", vector, new Vector2(num), 0f, 1f);
						vector += new Vector2(num + 2f, 0f);
					}
				}
				return;
			}
			foreach (KeyValuePair<string, Modifier> keyValuePair2 in this.modifiers.ToList<KeyValuePair<string, Modifier>>())
			{
				string key2 = keyValuePair2.Key;
				if (!keyValuePair2.Value.IsValid)
				{
					this.modifiers.Remove(key2);
					this.modifiersTime.Remove(key2);
				}
				else
				{
					renderer.DrawTexture(key2 + "_rounded", vector, new Vector2(num), 0f, 1f);
					renderer.DrawTexture("outline", vector, new Vector2(num), 0f, 1f);
					vector += new Vector2(num + 2f, 0f);
				}
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000093EC File Offset: 0x000075EC
		public bool DrawUltimate(O9K.Core.Managers.Renderer.IRenderer renderer, RectangleF position, Rectangle9 cdPosition, bool cdTime)
		{
			Ability9 ability = this.ultimate;
			if (ability == null || !ability.IsValid)
			{
				return false;
			}
			float remainingCooldown = this.ultimate.RemainingCooldown;
			if (remainingCooldown > 0f)
			{
				renderer.DrawTexture("ult_cd", position, 0f, 1f);
				if (!cdPosition.IsZero)
				{
					if (!this.hero.IsAlive)
					{
						return false;
					}
					int num = (int)(100f - remainingCooldown / this.ultimate.Cooldown * 100f);
					Rectangle9 rec = cdPosition * 1.1f;
					renderer.DrawTexture(this.ultimate.Name + "_rounded", cdPosition, 0f, 1f);
					renderer.DrawTexture("outline_black100", rec, 0f, 1f);
					renderer.DrawTexture("outline_green_pct" + num, rec, 0f, 1f);
					if (cdTime)
					{
						renderer.DrawTexture("top_ult_cd_bg", cdPosition, 0f, 1f);
						renderer.DrawText(cdPosition, remainingCooldown.ToString("N0"), System.Drawing.Color.White, RendererFontFlags.Center | RendererFontFlags.VerticalCenter, 20f * O9K.Core.Helpers.Hud.Info.ScreenRatio, "Calibri");
					}
					return true;
				}
			}
			else if (this.ultimate.ManaCost > this.hero.Mana)
			{
				renderer.DrawTexture("ult_mp", position, 0f, 1f);
				if (!cdPosition.IsZero)
				{
					if (!this.hero.IsAlive)
					{
						return false;
					}
					int num2 = (int)(this.hero.Mana / this.ultimate.ManaCost * 100f);
					Rectangle9 rec2 = cdPosition * 1.1f;
					renderer.DrawTexture(this.ultimate.Name + "_rounded", cdPosition, 0f, 1f);
					renderer.DrawTexture("outline_black100", rec2, 0f, 1f);
					renderer.DrawTexture("outline_blue_pct" + num2, rec2, 0f, 1f);
					if (cdTime)
					{
						string text = Math.Ceiling((double)((this.ultimate.ManaCost - this.hero.Mana) / this.hero.ManaRegeneration)).ToString("N0");
						renderer.DrawTexture("top_ult_cd_bg", cdPosition, 0f, 1f);
						renderer.DrawText(cdPosition, text, System.Drawing.Color.White, RendererFontFlags.Center | RendererFontFlags.VerticalCenter, 20f, "Calibri");
					}
					return true;
				}
			}
			else if (this.ultimate.IsUsable && this.ultimate.Level > 0u)
			{
				renderer.DrawTexture("ult_rdy", position, 0f, 1f);
			}
			return false;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000284C File Offset: 0x00000A4C
		public void RemoveItem(Ability9 ability)
		{
			this.items.Remove(ability);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000285B File Offset: 0x00000A5B
		public void SetUltimate(Ability9 ability)
		{
			Ability9 ability2 = this.ultimate;
			if (ability2 != null && ability2.IsValid)
			{
				return;
			}
			if (!this.hero.Equals(ability.Owner))
			{
				return;
			}
			this.ultimate = ability;
		}

		// Token: 0x0400008F RID: 143
		private readonly Hero9 hero;

		// Token: 0x04000090 RID: 144
		private readonly List<Ability9> items = new List<Ability9>();

		// Token: 0x04000091 RID: 145
		private readonly Dictionary<string, Modifier> modifiers = new Dictionary<string, Modifier>();

		// Token: 0x04000092 RID: 146
		private readonly Dictionary<string, float> modifiersTime = new Dictionary<string, float>();

		// Token: 0x04000093 RID: 147
		private Ability9 ultimate;
	}
}
