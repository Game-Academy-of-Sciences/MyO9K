using System;
using System.ComponentModel.Composition;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Helpers;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Hud.Core;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Helpers.Notificator.Notifications;

namespace O9K.Hud.Modules.Notifications
{
	// Token: 0x02000048 RID: 72
	internal class Runes : IDisposable, IHudModule
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x0000E108 File Offset: 0x0000C308
		[ImportingConstructor]
		public Runes(INotificator notificator, IHudMenu hudMenu)
		{
			this.notificator = notificator;
			Menu menu = hudMenu.NotificationsMenu.Add<Menu>(new Menu("Runes"));
			this.bountyEnabled = menu.Add<MenuSwitcher>(new MenuSwitcher("Bounty", true, false)).SetTooltip("Notify about bounty rune spawn");
			this.powerEnabled = menu.Add<MenuSwitcher>(new MenuSwitcher("Power-up", true, false)).SetTooltip("Notify about default rune spawn");
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000E194 File Offset: 0x0000C394
		public void Activate()
		{
			if (Game.GameTime < 10f)
			{
				this.bountySleeper.Sleep(Math.Abs(Game.GameTime) + 30f);
				this.powerSleeper.Sleep(Math.Abs(Game.GameTime) + 30f);
			}
			this.bountyEnabled.ValueChange += this.BountyEnabledOnValueChange;
			this.powerEnabled.ValueChange += this.PowerEnabledOnValueChange;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000E214 File Offset: 0x0000C414
		public void Dispose()
		{
			this.powerEnabled.ValueChange -= this.PowerEnabledOnValueChange;
			this.bountyEnabled.ValueChange -= this.BountyEnabledOnValueChange;
			UpdateManager.Unsubscribe(new Action(this.BountyOnUpdate));
			UpdateManager.Unsubscribe(new Action(this.PowerOnUpdate));
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000030FC File Offset: 0x000012FC
		private void BountyEnabledOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				UpdateManager.Subscribe(new Action(this.BountyOnUpdate), 1000, true);
				return;
			}
			UpdateManager.Unsubscribe(new Action(this.BountyOnUpdate));
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000E274 File Offset: 0x0000C474
		private void BountyOnUpdate()
		{
			if (this.bountySleeper.IsSleeping)
			{
				return;
			}
			if (Game.GameTime % 300f > 285f)
			{
				this.notificator.PushNotification(new RuneNotification(true));
				this.bountySleeper.Sleep(295f);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00003130 File Offset: 0x00001330
		private void PowerEnabledOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				UpdateManager.Subscribe(new Action(this.PowerOnUpdate), 1000, true);
				return;
			}
			UpdateManager.Unsubscribe(new Action(this.PowerOnUpdate));
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000E2C4 File Offset: 0x0000C4C4
		private void PowerOnUpdate()
		{
			if (this.powerSleeper.IsSleeping)
			{
				return;
			}
			if (Game.GameTime % 120f > 105f)
			{
				this.notificator.PushNotification(new RuneNotification(false));
				this.powerSleeper.Sleep(115f);
			}
			if (Game.GameTime > 900f)
			{
				UpdateManager.Unsubscribe(new Action(this.PowerOnUpdate));
			}
		}

		// Token: 0x04000126 RID: 294
		private readonly MenuSwitcher bountyEnabled;

		// Token: 0x04000127 RID: 295
		private readonly Sleeper bountySleeper = new Sleeper();

		// Token: 0x04000128 RID: 296
		private readonly INotificator notificator;

		// Token: 0x04000129 RID: 297
		private readonly MenuSwitcher powerEnabled;

		// Token: 0x0400012A RID: 298
		private readonly Sleeper powerSleeper = new Sleeper();
	}
}
