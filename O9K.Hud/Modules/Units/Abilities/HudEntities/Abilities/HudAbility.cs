using System;
using System.Drawing;
using Ensage.SDK.Renderer;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Logger;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using SharpDX;

namespace O9K.Hud.Modules.Units.Abilities.HudEntities.Abilities
{
	// Token: 0x02000025 RID: 37
	internal class HudAbility
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00002738 File Offset: 0x00000938
		public HudAbility(Ability9 ability)
		{
			this.Ability = ability;
			this.displayCharges = ability.IsDisplayingCharges;
			this.displayLevel = (ability.MaximumLevel > 1);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00002762 File Offset: 0x00000962
		public Ability9 Ability { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000276A File Offset: 0x0000096A
		public bool ShouldDisplay
		{
			get
			{
				return this.Ability.IsValid && this.Ability.IsUsable;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007934 File Offset: 0x00005B34
		public void Draw(O9K.Core.Managers.Renderer.IRenderer renderer, Rectangle9 position, float cooldownSize)
		{
			if (!this.Ability.IsUsable)
			{
				return;
			}
			try
			{
				renderer.DrawTexture(this.Ability.Name, position, 0f, 1f);
				if (this.Ability.IsCasting || this.Ability.IsChanneling)
				{
					renderer.DrawRectangle(position - 3f, System.Drawing.Color.LightGreen, 3f);
				}
				else
				{
					renderer.DrawRectangle(position - 1f, System.Drawing.Color.Black, 1f);
				}
				if (this.displayLevel)
				{
					uint level = this.Ability.Level;
					if (level == 0u)
					{
						renderer.DrawTexture("ability_0lvl_bg", position, 0f, 1f);
						return;
					}
					string text = level.ToString("N0");
					Vector2 vector = renderer.MeasureText(text, position.Width * 0.45f, "Calibri");
					Rectangle9 rec = position.SinkToBottomLeft(vector.X, vector.Y * 0.8f);
					renderer.DrawTexture("ability_lvl_bg", rec, 0f, 1f);
					renderer.DrawText(rec, text, System.Drawing.Color.White, RendererFontFlags.VerticalCenter, position.Width * 0.45f, "Calibri");
				}
				if (this.displayCharges)
				{
					string text2 = this.Ability.BaseItem.CurrentCharges.ToString("N0");
					Vector2 vector2 = renderer.MeasureText(text2, position.Width * 0.45f, "Calibri");
					Rectangle9 rec2 = position.SinkToBottomRight(vector2.X * 1.1f, vector2.Y * 0.8f);
					renderer.DrawTexture("ability_lvl_bg", rec2, 0f, 1f);
					renderer.DrawText(rec2, text2, System.Drawing.Color.White, RendererFontFlags.VerticalCenter, position.Width * 0.45f, "Calibri");
				}
				if (!this.Ability.IsChanneling)
				{
					float remainingCooldown = this.Ability.RemainingCooldown;
					if (remainingCooldown > 0f)
					{
						renderer.DrawTexture("ability_cd_bg", position, 0f, 1f);
						renderer.DrawText(position * 2f, Math.Ceiling((double)remainingCooldown).ToString("N0"), System.Drawing.Color.White, RendererFontFlags.Center | RendererFontFlags.VerticalCenter, cooldownSize, "Calibri");
					}
					else if (this.Ability.ManaCost > this.Ability.Owner.Mana)
					{
						renderer.DrawTexture("ability_mana_bg", position, 0f, 1f);
						renderer.DrawText(position * 2f, Math.Ceiling((double)((this.Ability.ManaCost - this.Ability.Owner.Mana) / this.Ability.Owner.ManaRegeneration)).ToString("N0"), System.Drawing.Color.White, RendererFontFlags.Center | RendererFontFlags.VerticalCenter, cooldownSize, "Calibri");
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x04000076 RID: 118
		private readonly bool displayCharges;

		// Token: 0x04000077 RID: 119
		private readonly bool displayLevel;
	}
}
