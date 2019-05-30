using System;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Hud.Helpers.Notificator.Notifications
{
	// Token: 0x020000AC RID: 172
	internal class HealthNotification : Notification
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x00004683 File Offset: 0x00002883
		public HealthNotification(Unit9 unit, bool moveCamera)
		{
			this.unit = unit;
			this.moveCamera = moveCamera;
			base.TimeToShow = 6;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001DC54 File Offset: 0x0001BE54
		public override void Draw(IRenderer renderer, RectangleF position)
		{
			RectangleF notificationSize = HealthNotification.GetNotificationSize(position);
			RectangleF textureSize = HealthNotification.GetTextureSize(notificationSize);
			RectangleF healthSize = HealthNotification.GetHealthSize(textureSize);
			RectangleF manaSize = HealthNotification.GetManaSize(textureSize);
			float opacity = base.GetOpacity();
			renderer.DrawTexture("notification_bg", notificationSize, 0f, opacity);
			renderer.DrawTexture(this.unit.Name, textureSize, 0f, opacity);
			renderer.DrawTexture("health_enemy_bg", healthSize, 0f, opacity);
			renderer.DrawTexture("mana_bg", manaSize, 0f, opacity);
			if (!this.unit.IsAlive)
			{
				return;
			}
			renderer.DrawTexture("health_enemy", this.GetCurrentHealthSize(healthSize), 0f, opacity);
			renderer.DrawTexture("mana", this.GetCurrentManaSize(manaSize), 0f, opacity);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000046A0 File Offset: 0x000028A0
		public override bool OnClick()
		{
			if (!this.moveCamera)
			{
				return false;
			}
			O9K.Core.Helpers.Hud.CameraPosition = this.unit.Position;
			return true;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001DD18 File Offset: 0x0001BF18
		private static RectangleF GetHealthSize(RectangleF position)
		{
			RectangleF result = position;
			result.Y += result.Height * 0.85f;
			result.Height *= 0.15f;
			return result;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001DD58 File Offset: 0x0001BF58
		private static RectangleF GetManaSize(RectangleF position)
		{
			RectangleF result = position;
			result.Y += result.Height;
			result.Height *= 0.15f;
			return result;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001DD90 File Offset: 0x0001BF90
		private static RectangleF GetNotificationSize(RectangleF position)
		{
			RectangleF result = position;
			result.X = position.Center.X;
			result.Width = position.Width * 0.5f;
			return result;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001DDC8 File Offset: 0x0001BFC8
		private static RectangleF GetTextureSize(RectangleF position)
		{
			RectangleF result = position;
			result.X += position.Width * 0.15f;
			result.Y += position.Height * 0.12f;
			result.Width = position.Width * 0.7f;
			result.Height = position.Height * 0.7f;
			return result;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001DE38 File Offset: 0x0001C038
		private RectangleF GetCurrentHealthSize(RectangleF position)
		{
			RectangleF result = position;
			result.Width *= this.unit.HealthPercentageBase;
			return result;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001DE64 File Offset: 0x0001C064
		private RectangleF GetCurrentManaSize(RectangleF position)
		{
			RectangleF result = position;
			result.Width *= this.unit.ManaPercentageBase;
			return result;
		}

		// Token: 0x04000279 RID: 633
		private readonly bool moveCamera;

		// Token: 0x0400027A RID: 634
		private readonly Unit9 unit;
	}
}
