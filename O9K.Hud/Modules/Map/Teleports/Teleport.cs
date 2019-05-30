using System;
using Ensage;
using SharpDX;

namespace O9K.Hud.Modules.Map.Teleports
{
	// Token: 0x02000053 RID: 83
	internal struct Teleport
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x0000F130 File Offset: 0x0000D330
		public Teleport(HeroId id, Vector3 position, System.Drawing.Color color)
		{
			this.HeroId = id;
			this.Position = position;
			this.Color = color;
			this.DisplayUntil = Game.RawGameTime + 3f;
			this.MapTexture = this.HeroId + "_rounded";
			this.MinimapTexture = this.HeroId + "_icon";
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000032F8 File Offset: 0x000014F8
		public HeroId HeroId { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00003300 File Offset: 0x00001500
		public string MapTexture { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00003308 File Offset: 0x00001508
		public string MinimapTexture { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00003310 File Offset: 0x00001510
		public float DisplayUntil { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00003318 File Offset: 0x00001518
		public Vector3 Position { get; }

        // Token: 0x1700003E RID: 62
        // (get) Token: 0x060001DE RID: 478 RVA: 0x00003320 File Offset: 0x00001520
        public System.Drawing.Color Color { get; }
	}
}
