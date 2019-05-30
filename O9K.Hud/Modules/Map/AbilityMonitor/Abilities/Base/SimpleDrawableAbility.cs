using System;
using O9K.Core.Helpers;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Helpers;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base
{
	// Token: 0x0200009D RID: 157
	internal class SimpleDrawableAbility : DrawableAbility
	{
		// Token: 0x0600037C RID: 892 RVA: 0x0001C3A4 File Offset: 0x0001A5A4
		public override void DrawOnMap(IRenderer renderer, IMinimap minimap)
		{
			Rectangle9 rec = minimap.WorldToScreen(base.Position, 35f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
			if (rec.IsZero)
			{
				return;
			}
			renderer.DrawTexture("outline_red", rec * 1.12f, 0f, 1f);
			renderer.DrawTexture(base.AbilityTexture, rec, 0f, 1f);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001C414 File Offset: 0x0001A614
		public override void DrawOnMinimap(IRenderer renderer, IMinimap minimap)
		{
			Rectangle9 rec = minimap.WorldToMinimap(base.Position, 25f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
			if (rec.IsZero)
			{
				return;
			}
			renderer.DrawTexture("outline_red", rec * 1.08f, 0f, 1f);
			renderer.DrawTexture(base.AbilityTexture, rec, 0f, 1f);
		}
	}
}
