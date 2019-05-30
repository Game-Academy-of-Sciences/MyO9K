using System;
using Ensage;
using O9K.Core.Helpers;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Helpers;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.FireRemnant
{
	// Token: 0x0200008B RID: 139
	internal class DrawableFireRemnantAbility : DrawableAbility
	{
		// Token: 0x060002FE RID: 766 RVA: 0x00003E95 File Offset: 0x00002095
		public DrawableFireRemnantAbility()
		{
			this.addedTime = Game.RawGameTime;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00003EA8 File Offset: 0x000020A8
		// (set) Token: 0x06000300 RID: 768 RVA: 0x00003EB0 File Offset: 0x000020B0
		public float ShowHeroUntil { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00003EB9 File Offset: 0x000020B9
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00003EC1 File Offset: 0x000020C1
		public float Duration { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00003ECA File Offset: 0x000020CA
		public override bool Draw
		{
			get
			{
				return this.Unit.IsValid && !this.Unit.IsVisible;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00003EE9 File Offset: 0x000020E9
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00003EF1 File Offset: 0x000020F1
		public Entity Owner { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00003EFA File Offset: 0x000020FA
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00003F02 File Offset: 0x00002102
		public Unit Unit { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00003F0B File Offset: 0x0000210B
		public override bool IsValid
		{
			get
			{
				return Game.RawGameTime <= base.ShowUntil && this.Unit.IsValid && this.Unit.IsAlive;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001B448 File Offset: 0x00019648
		public override void DrawOnMap(IRenderer renderer, IMinimap minimap)
		{
			Rectangle9 rec = minimap.WorldToScreen(base.Position, 35f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
			if (rec.IsZero)
			{
				return;
			}
			int num = (int)((Game.RawGameTime - this.addedTime) / this.Duration * 100f);
			renderer.DrawTexture("outline_red", rec * 1.2f, 0f, 1f);
			renderer.DrawTexture("outline_black" + num, rec * 1.25f, 0f, 1f);
			renderer.DrawTexture(base.AbilityTexture, rec, 0f, 1f);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0001B504 File Offset: 0x00019704
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
			renderer.DrawTexture(base.AbilityTexture, rec, 0f, 1f);
		}

		// Token: 0x04000211 RID: 529
		private readonly float addedTime;
	}
}
