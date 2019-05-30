using System;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Hud.Helpers.Notificator.Notifications
{
	// Token: 0x020000AB RID: 171
	internal class AbilityNotification : Notification
	{
		// Token: 0x060003CE RID: 974 RVA: 0x00004666 File Offset: 0x00002866
		public AbilityNotification(string heroName, string abilityName)
		{
			this.heroName = heroName;
			this.abilityName = abilityName;
			base.TimeToShow = 3;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001DB64 File Offset: 0x0001BD64
		public override void Draw(IRenderer renderer, RectangleF position)
		{
			RectangleF heroPosition = AbilityNotification.GetHeroPosition(position);
			RectangleF abilityPosition = AbilityNotification.GetAbilityPosition(position, heroPosition);
			RectangleF arrowPosition = AbilityNotification.GetArrowPosition(position, heroPosition, abilityPosition);
			float opacity = base.GetOpacity();
			if (this.heroName == null)
			{
				position.X += heroPosition.Width;
				renderer.DrawTexture("notification_bg", position, 0f, opacity);
			}
			else
			{
				renderer.DrawTexture("notification_bg", position, 0f, opacity);
				renderer.DrawTexture(this.heroName, heroPosition, 0f, opacity);
			}
			renderer.DrawTexture("ping", arrowPosition, 0f, opacity);
			renderer.DrawTexture(this.abilityName, abilityPosition, 0f, opacity);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001DC0C File Offset: 0x0001BE0C
		private static RectangleF GetAbilityPosition(RectangleF position, RectangleF heroPosition)
		{
			RectangleF result = heroPosition;
			result.Width *= 0.75f;
			result.X = position.Right - position.Width * 0.05f - result.Width;
			return result;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001DA30 File Offset: 0x0001BC30
		private static RectangleF GetArrowPosition(RectangleF position, RectangleF heroPosition, RectangleF itemPosition)
		{
			RectangleF result = default(RectangleF);
			result.Width = position.Width * 0.18f;
			result.Height = position.Height * 0.6f;
			result.X = (heroPosition.Right + itemPosition.Left) / 2f - result.Width / 2f;
			result.Y = position.Y + position.Height / 2f - result.Height / 2f;
			return result;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001DAC0 File Offset: 0x0001BCC0
		private static RectangleF GetHeroPosition(RectangleF position)
		{
			RectangleF result = position;
			result.X += position.Width * 0.05f;
			result.Y += position.Height * 0.15f;
			result.Width = position.Width * 0.3f;
			result.Height = position.Height * 0.7f;
			return result;
		}

		// Token: 0x04000277 RID: 631
		private readonly string abilityName;

		// Token: 0x04000278 RID: 632
		private readonly string heroName;
	}
}
