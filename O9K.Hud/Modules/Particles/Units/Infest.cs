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
	// Token: 0x02000038 RID: 56
	internal class Infest : IDisposable, IHudModule
	{
		// Token: 0x06000148 RID: 328 RVA: 0x0000C140 File Offset: 0x0000A340
		[ImportingConstructor]
		public Infest(INotificator notificator, IHudMenu hudMenu)
		{
			this.notificator = notificator;
			this.show = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Units")).Add<MenuSwitcher>(new MenuSwitcher("Infest", "infest", true, false));
			this.notificationsEnabled = hudMenu.NotificationsMenu.GetOrAdd<Menu>(new Menu("Abilities")).GetOrAdd<MenuSwitcher>(new MenuSwitcher("Enabled", true, false));
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00002C7C File Offset: 0x00000E7C
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
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

		// Token: 0x0600014B RID: 331 RVA: 0x0000C220 File Offset: 0x0000A420
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.Id == AbilityId.life_stealer_infest)
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

		// Token: 0x0600014C RID: 332 RVA: 0x0000C27C File Offset: 0x0000A47C
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team != this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_life_stealer_infest_effect"))
					{
						this.effect = new ParticleEffect("particles/units/heroes/hero_life_stealer/life_stealer_infested_unit.vpcf", sender, ParticleAttachment.OverheadFollow);
						if (this.notificationsEnabled && sender is Hero)
						{
							this.notificator.PushNotification(new AbilityHeroNotification("npc_dota_hero_life_stealer", "life_stealer_infest", sender.Name));
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

		// Token: 0x0600014D RID: 333 RVA: 0x0000C324 File Offset: 0x0000A524
		private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team != this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_life_stealer_infest_effect"))
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

		// Token: 0x0600014E RID: 334 RVA: 0x0000C398 File Offset: 0x0000A598
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

		// Token: 0x040000E9 RID: 233
		private readonly MenuSwitcher notificationsEnabled;

		// Token: 0x040000EA RID: 234
		private readonly INotificator notificator;

		// Token: 0x040000EB RID: 235
		private readonly MenuSwitcher show;

		// Token: 0x040000EC RID: 236
		private ParticleEffect effect;

		// Token: 0x040000ED RID: 237
		private Team ownerTeam;
	}
}
