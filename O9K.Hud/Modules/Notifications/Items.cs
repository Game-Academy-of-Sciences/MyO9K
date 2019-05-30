using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Hud.Core;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Helpers.Notificator.Notifications;

namespace O9K.Hud.Modules.Notifications
{
	// Token: 0x02000046 RID: 70
	internal class Items : IDisposable, IHudModule
	{
		// Token: 0x0600019F RID: 415 RVA: 0x0000DED4 File Offset: 0x0000C0D4
		[ImportingConstructor]
		public Items(IHudMenu hudMenu, INotificator notificator)
		{
			this.notificator = notificator;
			Menu menu = hudMenu.NotificationsMenu.Add<Menu>(new Menu("Items"));
			this.enabled = menu.Add<MenuSwitcher>(new MenuSwitcher("Enabled", true, false)).SetTooltip("Notify about enemy item purchases");
			this.goldThreshold = menu.Add<MenuSlider>(new MenuSlider("Item cost", 2000, 500, 6000, false)).SetTooltip("Item cost threshold");
			this.force = menu.Add<MenuAbilityToggler>(new MenuAbilityToggler("Always show:", new Dictionary<AbilityId, bool>
			{
				{
					AbilityId.item_ward_observer,
					true
				},
				{
					AbilityId.item_ward_sentry,
					true
				},
				{
					AbilityId.item_dust,
					true
				},
				{
					AbilityId.item_gem,
					true
				},
				{
					AbilityId.item_smoke_of_deceit,
					true
				}
			}, true, false));
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.ignoredItems = (from x in EntityManager9.Abilities
			where x.IsItem && x.Owner.Team != this.ownerTeam
			select x.Handle).ToHashSet<uint>();
			this.enabled.ValueChange += this.EnabledOnValueChange;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000306F File Offset: 0x0000126F
		public void Dispose()
		{
			this.enabled.ValueChange -= this.EnabledOnValueChange;
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00003099 File Offset: 0x00001299
		private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				EntityManager9.AbilityAdded += this.OnAbilityAdded;
				return;
			}
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000E020 File Offset: 0x0000C220
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.IsItem && ability.Owner.IsHero && !ability.Owner.IsIllusion && ability.Owner.Team != this.ownerTeam)
				{
					if (!this.ignoredItems.Contains(ability.Handle))
					{
						if ((this.force.IsEnabled(ability.Name) || (ulong)ability.BaseItem.Cost >= (ulong)((long)this.goldThreshold)) && !ability.BaseItem.IsRecipe)
						{
							this.notificator.PushNotification(new PurchaseNotification(ability.Owner.Name, ability.Name));
							this.ignoredItems.Add(ability.Handle);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0400011E RID: 286
		private readonly MenuSwitcher enabled;

		// Token: 0x0400011F RID: 287
		private readonly MenuAbilityToggler force;

		// Token: 0x04000120 RID: 288
		private readonly MenuSlider goldThreshold;

		// Token: 0x04000121 RID: 289
		private readonly INotificator notificator;

		// Token: 0x04000122 RID: 290
		private HashSet<uint> ignoredItems;

		// Token: 0x04000123 RID: 291
		private Team ownerTeam;
	}
}
