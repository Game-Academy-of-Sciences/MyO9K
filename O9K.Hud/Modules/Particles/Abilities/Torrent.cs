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
	// Token: 0x02000043 RID: 67
	internal class Torrent : IDisposable, IHudModule
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00002F00 File Offset: 0x00001100
		[ImportingConstructor]
		public Torrent(IHudMenu hudMenu)
		{
			this.show = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Abilities")).Add<MenuSwitcher>(new MenuSwitcher("Torrent", true, false));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00002F34 File Offset: 0x00001134
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000DA88 File Offset: 0x0000BC88
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

		// Token: 0x0600018F RID: 399 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.Id == AbilityId.kunkka_torrent)
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

		// Token: 0x06000190 RID: 400 RVA: 0x0000DB3C File Offset: 0x0000BD3C
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team != this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_kunkka_torrent_thinker"))
					{
						this.effect = new ParticleEffect("particles/econ/items/kunkka/divine_anchor/hero_kunkka_dafx_skills/kunkka_spell_torrent_bubbles_fxset.vpcf", sender.Position);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000DBA4 File Offset: 0x0000BDA4
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

		// Token: 0x04000112 RID: 274
		private readonly MenuSwitcher show;

		// Token: 0x04000113 RID: 275
		private ParticleEffect effect;

		// Token: 0x04000114 RID: 276
		private Team ownerTeam;
	}
}
