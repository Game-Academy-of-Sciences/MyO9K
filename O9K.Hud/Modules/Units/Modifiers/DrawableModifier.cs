using System;
using Ensage;

namespace O9K.Hud.Modules.Units.Modifiers
{
	// Token: 0x02000015 RID: 21
	internal class DrawableModifier
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00006344 File Offset: 0x00004544
		public DrawableModifier(Modifier modifier, bool isAura, string textureName)
		{
			this.Modifier = modifier;
			this.Handle = modifier.Handle;
			this.IsDebuff = modifier.IsDebuff;
			this.IsAura = isAura;
			this.TextureName = textureName + "_rounded";
			this.UpdateTimings();
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002433 File Offset: 0x00000633
		public string TextureName { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000243B File Offset: 0x0000063B
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002443 File Offset: 0x00000643
		public float Duration { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000244C File Offset: 0x0000064C
		public float RemainingTime
		{
			get
			{
				return this.endTime - Game.RawGameTime;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000245A File Offset: 0x0000065A
		public bool IsDebuff { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002462 File Offset: 0x00000662
		public Modifier Modifier { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000246A File Offset: 0x0000066A
		public bool IsAura { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002472 File Offset: 0x00000672
		public bool ShouldDraw
		{
			get
			{
				return this.IsAura || this.RemainingTime > 0f;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000248B File Offset: 0x0000068B
		public uint Handle { get; }

		// Token: 0x0600007C RID: 124 RVA: 0x00002493 File Offset: 0x00000693
		public void UpdateTimings()
		{
			if (this.IsAura)
			{
				return;
			}
			this.Duration = this.Modifier.Duration;
			this.endTime = Game.RawGameTime - this.Modifier.ElapsedTime + this.Duration;
		}

		// Token: 0x04000041 RID: 65
		private float endTime;
	}
}
