using System;
using System.Drawing;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Base;
using O9K.Core.Entities.Abilities.Heroes.StormSpirit;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.AIO.Heroes.StormSpirit.Modes
{
	// Token: 0x02000097 RID: 151
	internal class ManaCalculatorMode : BaseMode
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x00003D6B File Offset: 0x00001F6B
		public ManaCalculatorMode(BaseHero baseHero, ManaCalculatorModeMenu menu, IRendererManager9 renderer) : base(baseHero)
		{
			this.menu = menu;
			this.renderer = renderer;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00003D82 File Offset: 0x00001F82
		private BallLightning BallLightning
		{
			get
			{
				BallLightning ballLightning = this.ballLightning;
				if (ballLightning == null || !ballLightning.IsValid)
				{
					this.ballLightning = EntityManager9.GetAbility<BallLightning>(base.Owner.Hero);
				}
				return this.ballLightning;
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00003DB7 File Offset: 0x00001FB7
		public void Disable()
		{
			this.menu.Enabled.ValueChange -= this.EnabledOnValueChanged;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00003DB7 File Offset: 0x00001FB7
		public override void Dispose()
		{
			this.menu.Enabled.ValueChange -= this.EnabledOnValueChanged;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00003DD5 File Offset: 0x00001FD5
		public void Enable()
		{
			this.menu.Enabled.ValueChange += this.EnabledOnValueChanged;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00003DF3 File Offset: 0x00001FF3
		private void EnabledOnValueChanged(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				this.renderer.Draw += new RendererManager9.EventHandler(this.OnDraw);
				return;
			}
			this.renderer.Draw -= new RendererManager9.EventHandler(this.OnDraw);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000122F8 File Offset: 0x000104F8
		private void OnDraw(IRenderer renderer1)
		{
			try
			{
				if (!(this.BallLightning == null) && this.ballLightning.Level > 0u && this.ballLightning.Owner.IsAlive)
				{
					Vector3 mousePosition = Game.MousePosition;
					string text = this.menu.ShowRemainingMp ? this.BallLightning.GetRemainingMana(mousePosition).ToString() : this.BallLightning.GetRequiredMana(mousePosition).ToString();
					this.renderer.DrawText(Game.MouseScreenPosition + new Vector2(30f, 30f) * Hud.Info.ScreenRatio, text, Color.White, 16f * Hud.Info.ScreenRatio, "Calibri");
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x040001A4 RID: 420
		private readonly ManaCalculatorModeMenu menu;

		// Token: 0x040001A5 RID: 421
		private readonly IRendererManager9 renderer;

		// Token: 0x040001A6 RID: 422
		private BallLightning ballLightning;
	}
}
