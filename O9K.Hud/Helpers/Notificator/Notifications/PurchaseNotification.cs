using System;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Hud.Helpers.Notificator.Notifications
{
	// Token: 0x020000AE RID: 174
	internal class PurchaseNotification : Notification
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x00004710 File Offset: 0x00002910
		public PurchaseNotification(string heroName, string itemName)
		{
			this.heroName = heroName;
			this.itemName = itemName;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001DEE8 File Offset: 0x0001C0E8
		public override void Draw(IRenderer renderer, RectangleF position)
		{
			RectangleF heroPosition = PurchaseNotification.GetHeroPosition(position);
			RectangleF itemPosition = PurchaseNotification.GetItemPosition(position, heroPosition);
			RectangleF goldPosition = PurchaseNotification.GetGoldPosition(position, heroPosition, itemPosition);
			float opacity = base.GetOpacity();
			renderer.DrawTexture("notification_bg", position, 0f, opacity);
			renderer.DrawTexture(this.heroName, heroPosition, 0f, opacity);
			renderer.DrawTexture("gold", goldPosition, 0f, opacity);
			renderer.DrawTexture(this.itemName, itemPosition, 0f, opacity);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001DA30 File Offset: 0x0001BC30
		private static RectangleF GetGoldPosition(RectangleF position, RectangleF heroPosition, RectangleF itemPosition)
		{
			RectangleF result = default(RectangleF);
			result.Width = position.Width * 0.18f;
			result.Height = position.Height * 0.6f;
			result.X = (heroPosition.Right + itemPosition.Left) / 2f - result.Width / 2f;
			result.Y = position.Y + position.Height / 2f - result.Height / 2f;
			return result;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0001DAC0 File Offset: 0x0001BCC0
		private static RectangleF GetHeroPosition(RectangleF position)
		{
			RectangleF result = position;
			result.X += position.Width * 0.05f;
			result.Y += position.Height * 0.15f;
			result.Width = position.Width * 0.3f;
			result.Height = position.Height * 0.7f;
			return result;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001DF60 File Offset: 0x0001C160
		private static RectangleF GetItemPosition(RectangleF position, RectangleF heroPosition)
		{
			RectangleF result = heroPosition;
			result.Width *= 0.85f;
			result.X = position.Right - position.Width * 0.05f - result.Width;
			return result;
		}

		// Token: 0x0400027E RID: 638
		private readonly string heroName;

		// Token: 0x0400027F RID: 639
		private readonly string itemName;
	}
}
