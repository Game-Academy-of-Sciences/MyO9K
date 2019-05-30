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

namespace O9K.Hud.Modules.Particles.Units
{
	// Token: 0x0200003A RID: 58
	internal class Snowball : IDisposable, IHudModule
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00002CE1 File Offset: 0x00000EE1
		[ImportingConstructor]
		public Snowball(IHudMenu hudMenu)
		{
			this.show = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Units")).Add<MenuSwitcher>(new MenuSwitcher("Snowball", "snowball", true, false));
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00002D1A File Offset: 0x00000F1A
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000C79C File Offset: 0x0000A99C
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

		// Token: 0x0600015A RID: 346 RVA: 0x0000C804 File Offset: 0x0000AA04
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.Id == AbilityId.tusk_snowball)
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

		// Token: 0x0600015B RID: 347 RVA: 0x0000C860 File Offset: 0x0000AA60
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team == this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_tusk_snowball_target"))
					{
						this.effect = new ParticleEffect("particles/units/heroes/hero_tusk/tusk_snowball_target.vpcf", sender, ParticleAttachment.OverheadFollow);
						Unit.OnModifierRemoved += this.OnModifierRemoved;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
		private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team == this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_tusk_snowball_target"))
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

		// Token: 0x0600015D RID: 349 RVA: 0x0000C948 File Offset: 0x0000AB48
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

		// Token: 0x040000F2 RID: 242
		private readonly MenuSwitcher show;

		// Token: 0x040000F3 RID: 243
		private ParticleEffect effect;

		// Token: 0x040000F4 RID: 244
		private Team ownerTeam;
	}
}
