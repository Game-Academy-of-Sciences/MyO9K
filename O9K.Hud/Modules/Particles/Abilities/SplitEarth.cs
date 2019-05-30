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
using SharpDX;

namespace O9K.Hud.Modules.Particles.Abilities
{
	// Token: 0x02000042 RID: 66
	internal class SplitEarth : IDisposable, IHudModule
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00002E90 File Offset: 0x00001090
		[ImportingConstructor]
		public SplitEarth(IHudMenu hudMenu)
		{
			this.show = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Abilities")).Add<MenuSwitcher>(new MenuSwitcher("Split earth", true, false));
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00002EC4 File Offset: 0x000010C4
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000D8F0 File Offset: 0x0000BAF0
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

		// Token: 0x06000188 RID: 392 RVA: 0x0000D948 File Offset: 0x0000BB48
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.Id == AbilityId.leshrac_split_earth)
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

		// Token: 0x06000189 RID: 393 RVA: 0x0000D9A4 File Offset: 0x0000BBA4
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team != this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_leshrac_split_earth_thinker"))
					{
						this.effect = new ParticleEffect("particles/units/heroes/hero_leshrac/leshrac_split_projected.vpcf", sender.Position);
						this.effect.SetControlPoint(1u, new Vector3(200f, 1f, 1f));
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000DA2C File Offset: 0x0000BC2C
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

		// Token: 0x0400010F RID: 271
		private readonly MenuSwitcher show;

		// Token: 0x04000110 RID: 272
		private ParticleEffect effect;

		// Token: 0x04000111 RID: 273
		private Team ownerTeam;
	}
}
