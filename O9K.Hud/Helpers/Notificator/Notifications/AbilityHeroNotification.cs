using System;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Hud.Helpers.Notificator.Notifications
{
	// Token: 0x020000AA RID: 170
	internal class AbilityHeroNotification : Notification
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x00004638 File Offset: 0x00002838
		public AbilityHeroNotification(string heroName, string abilityName, string targetName)
		{
			this.heroName = heroName;
			this.abilityName = abilityName + "_rounded";
			this.targetName = targetName;
			base.TimeToShow = 3;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001D9B8 File Offset: 0x0001BBB8
		public override void Draw(IRenderer renderer, RectangleF position)
		{
			RectangleF heroPosition = AbilityHeroNotification.GetHeroPosition(position);
			RectangleF targetPosition = AbilityHeroNotification.GetTargetPosition(position, heroPosition);
			RectangleF abilityPosition = AbilityHeroNotification.GetAbilityPosition(position, heroPosition, targetPosition);
			float opacity = base.GetOpacity();
			renderer.DrawTexture("notification_bg", position, 0f, opacity);
			renderer.DrawTexture(this.heroName, heroPosition, 0f, opacity);
			renderer.DrawTexture(this.abilityName, abilityPosition, 0f, opacity);
			renderer.DrawTexture(this.targetName, targetPosition, 0f, opacity);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001DA30 File Offset: 0x0001BC30
		private static RectangleF GetAbilityPosition(RectangleF position, RectangleF heroPosition, RectangleF itemPosition)
		{
			RectangleF result = default(RectangleF);
			result.Width = position.Width * 0.18f;
			result.Height = position.Height * 0.6f;
			result.X = (heroPosition.Right + itemPosition.Left) / 2f - result.Width / 2f;
			result.Y = position.Y + position.Height / 2f - result.Height / 2f;
			return result;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001DAC0 File Offset: 0x0001BCC0
		private static RectangleF GetHeroPosition(RectangleF position)
		{
			RectangleF result = position;
			result.X += position.Width * 0.05f;
			result.Y += position.Height * 0.15f;
			result.Width = position.Width * 0.3f;
			result.Height = position.Height * 0.7f;
			return result;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001DB30 File Offset: 0x0001BD30
		private static RectangleF GetTargetPosition(RectangleF position, RectangleF heroPosition)
		{
			RectangleF result = heroPosition;
			result.X = position.Right - position.Width * 0.05f - result.Width;
			return result;
		}

		// Token: 0x04000274 RID: 628
		private readonly string abilityName;

		// Token: 0x04000275 RID: 629
		private readonly string heroName;

		// Token: 0x04000276 RID: 630
		private readonly string targetName;
	}
}
