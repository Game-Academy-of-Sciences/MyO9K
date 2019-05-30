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
	// Token: 0x02000034 RID: 52
	internal class Assassinate : IDisposable, IHudModule
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00002B24 File Offset: 0x00000D24
		[ImportingConstructor]
		public Assassinate(IHudMenu hudMenu)
		{
			this.show = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Units")).Add<MenuSwitcher>(new MenuSwitcher("Assassinate", true, false));
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00002B58 File Offset: 0x00000D58
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000B628 File Offset: 0x00009828
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

		// Token: 0x0600012A RID: 298 RVA: 0x0000B690 File Offset: 0x00009890
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.Id == AbilityId.sniper_assassinate)
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

		// Token: 0x0600012B RID: 299 RVA: 0x0000B6EC File Offset: 0x000098EC
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team == this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_sniper_assassinate"))
					{
						this.effect = new ParticleEffect("particles/units/heroes/hero_sniper/sniper_crosshair.vpcf", sender, ParticleAttachment.OverheadFollow);
						Unit.OnModifierRemoved += this.OnModifierRemoved;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000B760 File Offset: 0x00009960
		private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team == this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_sniper_assassinate"))
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

		// Token: 0x0600012D RID: 301 RVA: 0x0000B7D4 File Offset: 0x000099D4
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

		// Token: 0x040000D9 RID: 217
		private readonly MenuSwitcher show;

		// Token: 0x040000DA RID: 218
		private ParticleEffect effect;

		// Token: 0x040000DB RID: 219
		private Team ownerTeam;
	}
}
