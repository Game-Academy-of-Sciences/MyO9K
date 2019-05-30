using System;
using O9K.Core.Managers.Renderer.Utils;
using SharpDX;

namespace O9K.Hud.Helpers
{
	// Token: 0x020000A1 RID: 161
	internal interface IMinimap
	{
		// Token: 0x06000390 RID: 912
		Vector2 WorldToMinimap(Vector3 position);

		// Token: 0x06000391 RID: 913
		Rectangle9 WorldToMinimap(Vector3 position, float size);

		// Token: 0x06000392 RID: 914
		Rectangle9 WorldToScreen(Vector3 position, float size);
	}
}
