using System;
using System.ComponentModel.Composition;
using Ensage;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Hud.Core;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Helpers.Notificator.Notifications;

namespace O9K.Hud.Modules.Notifications
{
	// Token: 0x02000045 RID: 69
	internal class Health : IDisposable, IHudModule
	{
		// Token: 0x0600019A RID: 410 RVA: 0x0000DD78 File Offset: 0x0000BF78
		[ImportingConstructor]
		public Health(IHudMenu hudMenu, INotificator notificator)
		{
			this.notificator = notificator;
			Menu menu = hudMenu.NotificationsMenu.Add<Menu>(new Menu("Health"));
			this.enabled = menu.Add<MenuSwitcher>(new MenuSwitcher("Enabled", false, false)).SetTooltip("Notify on low enemy health");
			this.moveCamera = menu.Add<MenuSwitcher>(new MenuSwitcher("Move camera", true, false)).SetTooltip("Move camera when clicked");
			this.hpThreshold = menu.Add<MenuSlider>(new MenuSlider("Health%", 30, 5, 60, false));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00002FE0 File Offset: 0x000011E0
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.enabled.ValueChange += this.EnabledOnValueChange;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00003009 File Offset: 0x00001209
		public void Dispose()
		{
			this.enabled.ValueChange -= this.EnabledOnValueChange;
			EntityManager9.UnitMonitor.UnitHealthChange -= this.OnUnitHealthChange;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00003038 File Offset: 0x00001238
		private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				EntityManager9.UnitMonitor.UnitHealthChange += this.OnUnitHealthChange;
				return;
			}
			EntityManager9.UnitMonitor.UnitHealthChange -= this.OnUnitHealthChange;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000DE14 File Offset: 0x0000C014
		private void OnUnitHealthChange(Unit9 unit, float health)
		{
			try
			{
				if (unit.IsHero && !unit.IsIllusion && unit.Team != this.ownerTeam)
				{
					if (!this.sleeper.IsSleeping(unit.Handle) && unit.Distance(O9K.Core.Helpers.Hud.CameraPosition) >= 900f)
					{
						if (health / unit.MaximumHealth * 100f <= this.hpThreshold)
						{
							this.notificator.PushNotification(new HealthNotification(unit, this.moveCamera));
							this.sleeper.Sleep(unit.Handle, 20f);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x04000118 RID: 280
		private readonly MenuSwitcher enabled;

		// Token: 0x04000119 RID: 281
		private readonly MenuSlider hpThreshold;

		// Token: 0x0400011A RID: 282
		private readonly MenuSwitcher moveCamera;

		// Token: 0x0400011B RID: 283
		private readonly INotificator notificator;

		// Token: 0x0400011C RID: 284
		private readonly MultiSleeper sleeper = new MultiSleeper();

		// Token: 0x0400011D RID: 285
		private Team ownerTeam;
	}
}
