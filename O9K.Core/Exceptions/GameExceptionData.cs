using System;
using Ensage;

namespace O9K.Core.Exceptions
{
	// Token: 0x020000A9 RID: 169
	[Serializable]
	public class GameExceptionData
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x0001F8E8 File Offset: 0x0001DAE8
		public GameExceptionData()
		{
			try
			{
				Hero localHero = ObjectManager.LocalHero;
				this.Hero = localHero.Name;
				this.HeroTeam = localHero.Team.ToString();
				this.Map = Game.ShortLevelName;
				this.Mode = Game.GameMode.ToString();
				this.State = Game.GameState.ToString();
				this.Time = TimeSpan.FromSeconds((double)Game.GameTime).ToString("mm\\:ss");
			}
			catch
			{
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00005005 File Offset: 0x00003205
		public string Hero { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000500D File Offset: 0x0000320D
		public string HeroTeam { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00005015 File Offset: 0x00003215
		public string Map { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000501D File Offset: 0x0000321D
		public string Mode { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00005025 File Offset: 0x00003225
		public string State { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000502D File Offset: 0x0000322D
		public string Time { get; }
	}
}
