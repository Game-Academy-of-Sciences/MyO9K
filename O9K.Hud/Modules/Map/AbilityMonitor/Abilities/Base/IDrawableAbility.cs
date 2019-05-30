using System;
using O9K.Core.Managers.Renderer;
using O9K.Hud.Helpers;
using SharpDX;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base
{
	// Token: 0x0200009C RID: 156
	internal interface IDrawableAbility
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000372 RID: 882
		bool IsValid { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000373 RID: 883
		bool Draw { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000374 RID: 884
		bool IsShowingRange { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000375 RID: 885
		string AbilityTexture { get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000376 RID: 886
		string HeroTexture { get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000377 RID: 887
		// (set) Token: 0x06000378 RID: 888
		Vector3 Position { get; set; }

		// Token: 0x06000379 RID: 889
		void DrawOnMap(IRenderer renderer, IMinimap minimap);

		// Token: 0x0600037A RID: 890
		void DrawOnMinimap(IRenderer renderer, IMinimap minimap);

		// Token: 0x0600037B RID: 891
		void RemoveRange();
	}
}
