using System;
using System.ComponentModel.Composition;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Hud.Core;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Helpers.Notificator.Notifications;

namespace O9K.Hud.Modules.Particles.Units
{
	// Token: 0x02000037 RID: 55
	internal class ChargeOfDarkness : IDisposable, IHudModule
	{
		// Token: 0x06000140 RID: 320 RVA: 0x0000BE7C File Offset: 0x0000A07C
		[ImportingConstructor]
		public ChargeOfDarkness(INotificator notificator, IHudMenu hudMenu)
		{
			this.notificator = notificator;
			this.show = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Units")).Add<MenuSwitcher>(new MenuSwitcher("Charge of darkness", "charge", true, false));
			this.notificationsEnabled = hudMenu.NotificationsMenu.GetOrAdd<Menu>(new Menu("Abilities")).GetOrAdd<MenuSwitcher>(new MenuSwitcher("Enabled", true, false));
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00002C40 File Offset: 0x00000E40
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		public void Dispose()
		{
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			this.show.ValueChange -= this.ShowOnValueChange;
			Unit.OnModifierAdded -= this.OnModifierAdded;
			Unit.OnModifierRemoved -= this.OnModifierRemoved;
			ParticleEffect particleEffect = this.effect;
			if (particleEffect == null)
			{
				return;
			}
			particleEffect.Dispose();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000BF5C File Offset: 0x0000A15C
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.Id == AbilityId.spirit_breaker_charge_of_darkness)
				{
					UpdateManager.BeginInvoke(delegate
					{
						EntityManager9.AbilityAdded -= this.OnAbilityAdded;
					}, 0);
					Unit.OnModifierAdded += this.OnModifierAdded;
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team == this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_spirit_breaker_charge_of_darkness_vision"))
					{
						this.effect = new ParticleEffect("particles/units/heroes/hero_spirit_breaker/spirit_breaker_charge_target.vpcf", sender, ParticleAttachment.OverheadFollow);
						if (this.notificationsEnabled && sender is Hero)
						{
							this.notificator.PushNotification(new AbilityHeroNotification("npc_dota_hero_spirit_breaker", "spirit_breaker_charge_of_darkness", sender.Name));
						}
						Unit.OnModifierRemoved += this.OnModifierRemoved;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000C060 File Offset: 0x0000A260
		private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team == this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_spirit_breaker_charge_of_darkness_vision"))
					{
						ParticleEffect particleEffect = this.effect;
						if (particleEffect != null)
						{
							particleEffect.Dispose();
						}
						Unit.OnModifierRemoved -= this.OnModifierRemoved;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		private void ShowOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				EntityManager9.AbilityAdded += this.OnAbilityAdded;
				return;
			}
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			Unit.OnModifierAdded -= this.OnModifierAdded;
			Unit.OnModifierRemoved -= this.OnModifierRemoved;
			ParticleEffect particleEffect = this.effect;
			if (particleEffect == null)
			{
				return;
			}
			particleEffect.Dispose();
		}

		// Token: 0x040000E4 RID: 228
		private readonly MenuSwitcher notificationsEnabled;

		// Token: 0x040000E5 RID: 229
		private readonly INotificator notificator;

		// Token: 0x040000E6 RID: 230
		private readonly MenuSwitcher show;

		// Token: 0x040000E7 RID: 231
		private ParticleEffect effect;

		// Token: 0x040000E8 RID: 232
		private Team ownerTeam;
	}
}
