using System;
using Ensage;
using O9K.Core.Managers.Renderer.Utils;

namespace O9K.Hud.Helpers
{
	// Token: 0x020000A5 RID: 165
	internal interface ITopPanel
	{
		// Token: 0x060003A9 RID: 937
		Rectangle9 GetPlayerPosition(int id);

		// Token: 0x060003AA RID: 938
		Rectangle9 GetPlayersHealthBarPosition(int id, float height, float topIndent);

		// Token: 0x060003AB RID: 939
		Rectangle9 GetPlayersUltimatePosition(int id, float size, float topIndent);

		// Token: 0x060003AC RID: 940
		Rectangle9 GetScorePosition(Team team);
	}
}
