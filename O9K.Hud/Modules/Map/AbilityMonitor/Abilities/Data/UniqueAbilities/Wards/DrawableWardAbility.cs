using System;
using System.Drawing;
using Ensage;
using Ensage.SDK.Renderer;
using O9K.Core.Helpers;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Helpers;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Wards
{
	// Token: 0x02000079 RID: 121
	internal class DrawableWardAbility : DrawableAbility
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x00003ABE File Offset: 0x00001CBE
		public DrawableWardAbility()
		{
			this.AddedTime = Game.RawGameTime;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00003AD1 File Offset: 0x00001CD1
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x00003AD9 File Offset: 0x00001CD9
		public float ShowHeroUntil { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00003AE2 File Offset: 0x00001CE2
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00003AEA File Offset: 0x00001CEA
		public float Duration { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00003AF3 File Offset: 0x00001CF3
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00003AFB File Offset: 0x00001CFB
		public float AddedTime { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00003B04 File Offset: 0x00001D04
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00003B0C File Offset: 0x00001D0C
		public string AbilityUnitName { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00003B15 File Offset: 0x00001D15
		public override bool Draw
		{
			get
			{
				return this.Unit == null || (this.Unit.IsValid && !this.Unit.IsVisible);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00003B44 File Offset: 0x00001D44
		// (set) Token: 0x060002BF RID: 703 RVA: 0x00003B4C File Offset: 0x00001D4C
		public Unit Unit { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00003B55 File Offset: 0x00001D55
		public override bool IsValid
		{
			get
			{
				return Game.RawGameTime <= base.ShowUntil && (this.Unit == null || (this.Unit.IsValid && this.Unit.IsAlive));
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00003B90 File Offset: 0x00001D90
		public void AddUnit(Unit unit)
		{
			this.Unit = unit;
			base.Position = unit.Position;
			base.DrawRange();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0001A404 File Offset: 0x00018604
		public override void DrawOnMap(O9K.Core.Managers.Renderer.IRenderer renderer, IMinimap minimap)
		{
			Rectangle9 rec = minimap.WorldToScreen(base.Position, 30f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
			if (rec.IsZero)
			{
				return;
			}
			int num = (int)((Game.RawGameTime - this.AddedTime) / this.Duration * 100f);
			renderer.DrawTexture("outline_red", rec * 1.2f, 0f, 1f);
			renderer.DrawTexture("outline_black" + num, rec * 1.25f, 0f, 1f);
			renderer.DrawTexture(base.AbilityTexture, rec, 0f, 1f);
			rec.Y += 30f * O9K.Core.Helpers.Hud.Info.ScreenRatio;
			rec *= 2f;
			renderer.DrawText(rec, TimeSpan.FromSeconds((double)(this.Duration - (Game.RawGameTime - this.AddedTime))).ToString("m\\:ss"), System.Drawing.Color.White, RendererFontFlags.Center | RendererFontFlags.VerticalCenter, 18f * O9K.Core.Helpers.Hud.Info.ScreenRatio, "Calibri");
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0001A52C File Offset: 0x0001872C
		public override void DrawOnMinimap(O9K.Core.Managers.Renderer.IRenderer renderer, IMinimap minimap)
		{
			Unit unit = this.Unit;
			if (unit != null && unit.IsVisible)
			{
				return;
			}
			Rectangle9 rec = minimap.WorldToMinimap(base.Position, 15f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
			if (rec.IsZero)
			{
				return;
			}
			renderer.DrawTexture("minimap_" + base.AbilityTexture, rec, 0f, 1f);
		}
	}
}
