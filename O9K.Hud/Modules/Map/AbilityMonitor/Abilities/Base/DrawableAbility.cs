using System;
using Ensage;
using O9K.Core.Helpers;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Helpers;
using SharpDX;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base
{
	// Token: 0x02000096 RID: 150
	internal class DrawableAbility : IDrawableAbility
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000040C7 File Offset: 0x000022C7
		// (set) Token: 0x06000328 RID: 808 RVA: 0x000040CF File Offset: 0x000022CF
		public float ShowUntil { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000329 RID: 809 RVA: 0x000040D8 File Offset: 0x000022D8
		// (set) Token: 0x0600032A RID: 810 RVA: 0x000040E0 File Offset: 0x000022E0
		public bool IsShowingRange { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600032B RID: 811 RVA: 0x000040E9 File Offset: 0x000022E9
		// (set) Token: 0x0600032C RID: 812 RVA: 0x000040F1 File Offset: 0x000022F1
		public string HeroTexture { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600032D RID: 813 RVA: 0x000040FA File Offset: 0x000022FA
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00004102 File Offset: 0x00002302
		public string MinimapHeroTexture { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000410B File Offset: 0x0000230B
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00004113 File Offset: 0x00002313
		public string AbilityTexture { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000411C File Offset: 0x0000231C
		public virtual bool IsValid
		{
			get
			{
				return Game.RawGameTime < this.ShowUntil;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000412B File Offset: 0x0000232B
		// (set) Token: 0x06000333 RID: 819 RVA: 0x00004133 File Offset: 0x00002333
		public Vector3 Position { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000413C File Offset: 0x0000233C
		public virtual bool Draw { get; } = true;

		// Token: 0x06000335 RID: 821 RVA: 0x0001BD74 File Offset: 0x00019F74
		public virtual void DrawOnMap(IRenderer renderer, IMinimap minimap)
		{
			Rectangle9 rec = minimap.WorldToScreen(this.Position, 45f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
			if (rec.IsZero)
			{
				return;
			}
			renderer.DrawTexture("outline_red", rec * 1.12f, 0f, 1f);
			renderer.DrawTexture(this.HeroTexture, rec, 0f, 1f);
			Rectangle9 rec2 = rec * 0.5f;
			rec2.X += rec2.Width * 0.8f;
			rec2.Y += rec2.Height * 0.6f;
			renderer.DrawTexture("outline_green_pct100", rec2 * 1.2f, 0f, 1f);
			renderer.DrawTexture(this.AbilityTexture, rec2, 0f, 1f);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0001BE68 File Offset: 0x0001A068
		public virtual void DrawOnMinimap(IRenderer renderer, IMinimap minimap)
		{
			Rectangle9 rec = minimap.WorldToMinimap(this.Position, 25f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
			if (rec.IsZero)
			{
				return;
			}
			renderer.DrawTexture("outline_red", rec * 1.08f, 0f, 1f);
			renderer.DrawTexture(this.MinimapHeroTexture, rec, 0f, 1f);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00004144 File Offset: 0x00002344
		public virtual void RemoveRange()
		{
			ParticleEffect rangeParticle = this.RangeParticle;
			if (rangeParticle == null)
			{
				return;
			}
			rangeParticle.Dispose();
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00004156 File Offset: 0x00002356
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000415E File Offset: 0x0000235E
		public float Range { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00004167 File Offset: 0x00002367
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000416F File Offset: 0x0000236F
		public Vector3 RangeColor { get; set; }

		// Token: 0x0600033C RID: 828 RVA: 0x0001BED8 File Offset: 0x0001A0D8
		public void DrawRange()
		{
			if (!this.IsShowingRange)
			{
				return;
			}
			if (this.RangeParticle != null)
			{
				this.RangeParticle.SetControlPoint(0u, this.Position);
				return;
			}
			this.RangeParticle = new ParticleEffect("particles/ui_mouseactions/drag_selected_ring.vpcf", this.Position);
			this.RangeParticle.SetControlPoint(1u, this.RangeColor);
			this.RangeParticle.SetControlPoint(2u, new Vector3(-this.Range, 255f, 0f));
		}

		// Token: 0x04000229 RID: 553
		protected ParticleEffect RangeParticle;
	}
}
