using System;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Hud.Helpers.Notificator.Notifications
{
	// Token: 0x020000AF RID: 175
	internal class RuneNotification : Notification
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x00004726 File Offset: 0x00002926
		public RuneNotification(bool bounty)
		{
			this.bounty = bounty;
			base.TimeToShow = 6;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001DFA8 File Offset: 0x0001C1A8
		public override void Draw(IRenderer renderer, RectangleF position)
		{
			RectangleF notificationSize = RuneNotification.GetNotificationSize(position);
			RectangleF textureSize = RuneNotification.GetTextureSize(notificationSize);
			float opacity = base.GetOpacity();
			renderer.DrawTexture("notification_bg", notificationSize, 0f, opacity);
			renderer.DrawTexture(this.bounty ? "rune_bounty" : "rune_regen", textureSize, 0f, opacity);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001DD90 File Offset: 0x0001BF90
		private static RectangleF GetNotificationSize(RectangleF position)
		{
			RectangleF result = position;
			result.X = position.Center.X;
			result.Width = position.Width * 0.5f;
			return result;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001E000 File Offset: 0x0001C200
		private static RectangleF GetTextureSize(RectangleF position)
		{
			RectangleF result = default(RectangleF);
			result.Width = position.Width * 0.5f;
			result.Height = position.Height;
			result.X = position.Center.X - result.Width / 2f;
			result.Y = position.Y;
			return result;
		}

		// Token: 0x04000280 RID: 640
		private readonly bool bounty;
	}
}
