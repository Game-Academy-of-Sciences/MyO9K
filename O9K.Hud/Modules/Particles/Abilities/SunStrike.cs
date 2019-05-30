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

namespace O9K.Hud.Modules.Particles.Abilities
{
	// Token: 0x02000044 RID: 68
	internal class SunStrike : IDisposable, IHudModule
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00002F70 File Offset: 0x00001170
		[ImportingConstructor]
		public SunStrike(IHudMenu hudMenu)
		{
			this.show = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Abilities")).Add<MenuSwitcher>(new MenuSwitcher("Sun strike", true, false));
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00002FA4 File Offset: 0x000011A4
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000DC00 File Offset: 0x0000BE00
		public void Dispose()
		{
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			this.show.ValueChange -= this.ShowOnValueChange;
			Unit.OnModifierAdded -= this.OnModifierAdded;
			ParticleEffect particleEffect = this.effect;
			if (particleEffect == null)
			{
				return;
			}
			particleEffect.Dispose();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000DC58 File Offset: 0x0000BE58
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.Id == AbilityId.invoker_sun_strike)
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

		// Token: 0x06000197 RID: 407 RVA: 0x0000DCB4 File Offset: 0x0000BEB4
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team != this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_invoker_sun_strike"))
					{
						this.effect = new ParticleEffect("particles/econ/items/invoker/invoker_apex/invoker_sun_strike_team_immortal1.vpcf", sender.Position);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000DD1C File Offset: 0x0000BF1C
		private void ShowOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				EntityManager9.AbilityAdded += this.OnAbilityAdded;
				return;
			}
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			Unit.OnModifierAdded -= this.OnModifierAdded;
			ParticleEffect particleEffect = this.effect;
			if (particleEffect == null)
			{
				return;
			}
			particleEffect.Dispose();
		}

		// Token: 0x04000115 RID: 277
		private readonly MenuSwitcher show;

		// Token: 0x04000116 RID: 278
		private ParticleEffect effect;

		// Token: 0x04000117 RID: 279
		private Team ownerTeam;
	}
}
