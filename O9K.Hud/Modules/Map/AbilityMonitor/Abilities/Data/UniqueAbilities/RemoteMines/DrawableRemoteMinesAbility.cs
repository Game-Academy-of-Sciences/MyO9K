using System;
using Ensage;
using O9K.Core.Helpers;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Helpers;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base;
using SharpDX;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.RemoteMines
{
	// Token: 0x02000080 RID: 128
	internal class DrawableRemoteMinesAbility : DrawableAbility
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x00003C49 File Offset: 0x00001E49
		public DrawableRemoteMinesAbility()
		{
			this.addedTime = Game.RawGameTime;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00003C5C File Offset: 0x00001E5C
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00003C64 File Offset: 0x00001E64
		public float ShowHeroUntil { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00003C6D File Offset: 0x00001E6D
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00003C75 File Offset: 0x00001E75
		public float Duration { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00003C7E File Offset: 0x00001E7E
		public override bool Draw
		{
			get
			{
				return this.Unit == null || !this.Unit.IsVisible;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00003C9E File Offset: 0x00001E9E
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00003CA6 File Offset: 0x00001EA6
		public Entity Owner { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00003CAF File Offset: 0x00001EAF
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00003CB7 File Offset: 0x00001EB7
		public Unit Unit { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public override bool IsValid
		{
			get
			{
				return Game.RawGameTime <= base.ShowUntil && (this.Unit == null || (this.Unit.IsValid && this.Unit.IsAlive));
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00003CFB File Offset: 0x00001EFB
		public void AddUnit(Unit unit)
		{
			this.Unit = unit;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0001A9A8 File Offset: 0x00018BA8
		public override void DrawOnMap(IRenderer renderer, IMinimap minimap)
		{
			Vector2 vector = Drawing.WorldToScreen(base.Position);
			if (vector.IsZero)
			{
				return;
			}
			float rawGameTime = Game.RawGameTime;
			if (rawGameTime < this.ShowHeroUntil)
			{
				Rectangle9 rec = new Rectangle9(vector, new Vector2(45f));
				renderer.DrawTexture("outline_red", rec * 1.12f, 0f, 1f);
				renderer.DrawTexture(base.HeroTexture, rec, 0f, 1f);
				Rectangle9 rec2 = new Rectangle9(vector + new Vector2(30f, 20f), new Vector2(27f));
				renderer.DrawTexture("outline_green_pct100", rec2 * 1.2f, 0f, 1f);
				renderer.DrawTexture(base.AbilityTexture, rec2, 0f, 1f);
				return;
			}
			int num = (int)((rawGameTime - this.addedTime) / this.Duration * 100f);
			Rectangle9 rec3 = new Rectangle9(vector - new Vector2(35f) / 2f, new Vector2(35f));
			renderer.DrawTexture("outline_red", rec3 * 1.2f, 0f, 1f);
			renderer.DrawTexture("outline_black" + num, rec3 * 1.25f, 0f, 1f);
			renderer.DrawTexture(base.AbilityTexture, rec3, 0f, 1f);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0001AB50 File Offset: 0x00018D50
		public override void DrawOnMinimap(IRenderer renderer, IMinimap minimap)
		{
			if (Game.RawGameTime > this.ShowHeroUntil || this.Owner.IsVisible)
			{
				return;
			}
			Rectangle9 rec = minimap.WorldToMinimap(base.Position, 25f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
			if (rec.IsZero)
			{
				return;
			}
			renderer.DrawTexture("outline_red", rec * 1.08f, 0f, 1f);
			renderer.DrawTexture(base.MinimapHeroTexture, rec, 0f, 1f);
		}

		// Token: 0x040001FB RID: 507
		private readonly float addedTime;
	}
}
