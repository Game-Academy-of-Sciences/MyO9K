using System;
using Ensage;
using O9K.Core.Helpers;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Helpers;
using SharpDX;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base
{
	// Token: 0x02000097 RID: 151
	internal class DrawableUnitAbility : IDrawableAbility
	{
		// Token: 0x0600033E RID: 830 RVA: 0x00004187 File Offset: 0x00002387
		public DrawableUnitAbility()
		{
			this.addedTime = Game.RawGameTime;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000419A File Offset: 0x0000239A
		// (set) Token: 0x06000340 RID: 832 RVA: 0x000041A2 File Offset: 0x000023A2
		public string HeroTexture { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000041AB File Offset: 0x000023AB
		// (set) Token: 0x06000342 RID: 834 RVA: 0x000041B3 File Offset: 0x000023B3
		public string MinimapHeroTexture { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000343 RID: 835 RVA: 0x000041BC File Offset: 0x000023BC
		// (set) Token: 0x06000344 RID: 836 RVA: 0x000041C4 File Offset: 0x000023C4
		public bool IsShowingRange { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000345 RID: 837 RVA: 0x000041CD File Offset: 0x000023CD
		// (set) Token: 0x06000346 RID: 838 RVA: 0x000041D5 File Offset: 0x000023D5
		public string AbilityTexture { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000347 RID: 839 RVA: 0x000041DE File Offset: 0x000023DE
		// (set) Token: 0x06000348 RID: 840 RVA: 0x000041E6 File Offset: 0x000023E6
		public float ShowUntil { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000349 RID: 841 RVA: 0x000041EF File Offset: 0x000023EF
		// (set) Token: 0x0600034A RID: 842 RVA: 0x000041F7 File Offset: 0x000023F7
		public bool ShowTimer { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00004200 File Offset: 0x00002400
		// (set) Token: 0x0600034C RID: 844 RVA: 0x00004208 File Offset: 0x00002408
		public float Duration { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00004211 File Offset: 0x00002411
		public bool IsValid
		{
			get
			{
				return Game.RawGameTime <= this.ShowUntil && this.Unit.IsValid && this.Unit.IsAlive;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000423C File Offset: 0x0000243C
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00004244 File Offset: 0x00002444
		public Unit Unit { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000424D File Offset: 0x0000244D
		// (set) Token: 0x06000351 RID: 849 RVA: 0x00004255 File Offset: 0x00002455
		public Vector3 Position { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000425E File Offset: 0x0000245E
		public bool Draw
		{
			get
			{
				return !this.Unit.IsVisible;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000426E File Offset: 0x0000246E
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00004276 File Offset: 0x00002476
		public Entity Owner { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000427F File Offset: 0x0000247F
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00004287 File Offset: 0x00002487
		public float ShowHeroUntil { get; set; }

		// Token: 0x06000357 RID: 855 RVA: 0x0001BF5C File Offset: 0x0001A15C
		public void DrawOnMap(IRenderer renderer, IMinimap minimap)
		{
			Rectangle9 rec = minimap.WorldToScreen(this.Position, 45f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
			if (rec.IsZero)
			{
				return;
			}
			float rawGameTime = Game.RawGameTime;
			if (rawGameTime < this.ShowHeroUntil)
			{
				renderer.DrawTexture("outline_red", rec * 1.12f, 0f, 1f);
				renderer.DrawTexture(this.HeroTexture, rec, 0f, 1f);
				Rectangle9 rec2 = rec * 0.5f;
				rec2.X += rec2.Width * 0.8f;
				rec2.Y += rec2.Height * 0.6f;
				renderer.DrawTexture("outline_green_pct100", rec2 * 1.2f, 0f, 1f);
				renderer.DrawTexture(this.AbilityTexture, rec2, 0f, 1f);
				return;
			}
			renderer.DrawTexture("outline_red", rec, 0f, 1f);
			if (this.ShowTimer)
			{
				int val = (int)((rawGameTime - this.addedTime) / this.Duration * 100f);
				renderer.DrawTexture("outline_black" + Math.Min(val, 100), rec * 1.05f, 0f, 1f);
			}
			renderer.DrawTexture(this.AbilityTexture, rec * 0.8f, 0f, 1f);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001C0FC File Offset: 0x0001A2FC
		public void DrawOnMinimap(IRenderer renderer, IMinimap minimap)
		{
			if (Game.RawGameTime > this.ShowHeroUntil || this.Owner.IsVisible)
			{
				return;
			}
			Rectangle9 rec = minimap.WorldToMinimap(this.Position, 25f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
			if (rec.IsZero)
			{
				return;
			}
			renderer.DrawTexture("outline_red", rec * 1.08f, 0f, 1f);
			renderer.DrawTexture(this.MinimapHeroTexture, rec, 0f, 1f);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00004290 File Offset: 0x00002490
		public virtual void RemoveRange()
		{
			ParticleEffect rangeParticle = this.RangeParticle;
			if (rangeParticle == null)
			{
				return;
			}
			rangeParticle.Dispose();
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600035A RID: 858 RVA: 0x000042A2 File Offset: 0x000024A2
		// (set) Token: 0x0600035B RID: 859 RVA: 0x000042AA File Offset: 0x000024AA
		public float Range { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600035C RID: 860 RVA: 0x000042B3 File Offset: 0x000024B3
		// (set) Token: 0x0600035D RID: 861 RVA: 0x000042BB File Offset: 0x000024BB
		public Vector3 RangeColor { get; set; }

		// Token: 0x0600035E RID: 862 RVA: 0x0001C188 File Offset: 0x0001A388
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

		// Token: 0x0400022C RID: 556
		private readonly float addedTime;

		// Token: 0x04000238 RID: 568
		protected ParticleEffect RangeParticle;
	}
}
