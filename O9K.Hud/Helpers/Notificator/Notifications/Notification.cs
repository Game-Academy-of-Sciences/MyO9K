using System;
using Ensage;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Hud.Helpers.Notificator.Notifications
{
	// Token: 0x020000AD RID: 173
	internal abstract class Notification
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003DC RID: 988 RVA: 0x000046BD File Offset: 0x000028BD
		public bool IsExpired
		{
			get
			{
				return Game.RawGameTime > this.stopDisplayTime;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003DD RID: 989 RVA: 0x000046CC File Offset: 0x000028CC
		// (set) Token: 0x060003DE RID: 990 RVA: 0x000046D4 File Offset: 0x000028D4
		protected int TimeToShow { get; set; } = 4;

		// Token: 0x060003DF RID: 991
		public abstract void Draw(IRenderer renderer, RectangleF position);

		// Token: 0x060003E0 RID: 992 RVA: 0x000046DD File Offset: 0x000028DD
		public virtual bool OnClick()
		{
			return false;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000046E0 File Offset: 0x000028E0
		public void Pushed()
		{
			this.startDisplayTime = Game.RawGameTime;
			this.stopDisplayTime = this.startDisplayTime + (float)this.TimeToShow;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001DE90 File Offset: 0x0001C090
		protected float GetOpacity()
		{
			float result = 1f;
			float rawGameTime = Game.RawGameTime;
			if (this.startDisplayTime + 0.5f > rawGameTime)
			{
				result = (rawGameTime - this.startDisplayTime) * 2f;
			}
			else if (rawGameTime + 0.5f > this.stopDisplayTime)
			{
				result = (this.stopDisplayTime - rawGameTime) * 2f;
			}
			return result;
		}

		// Token: 0x0400027B RID: 635
		private float startDisplayTime;

		// Token: 0x0400027C RID: 636
		private float stopDisplayTime;
	}
}
