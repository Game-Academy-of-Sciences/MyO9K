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
	// Token: 0x02000041 RID: 65
	internal class LightStrikeArray : IDisposable, IHudModule
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00002E20 File Offset: 0x00001020
		[ImportingConstructor]
		public LightStrikeArray(IHudMenu hudMenu)
		{
			this.show = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Abilities")).Add<MenuSwitcher>(new MenuSwitcher("Light strike array", true, false));
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00002E54 File Offset: 0x00001054
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000D758 File Offset: 0x0000B958
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

		// Token: 0x06000181 RID: 385 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.Id == AbilityId.lina_light_strike_array)
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

		// Token: 0x06000182 RID: 386 RVA: 0x0000D80C File Offset: 0x0000BA0C
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team != this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_lina_light_strike_array"))
					{
						this.effect = new ParticleEffect("particles/econ/items/lina/lina_ti7/light_strike_array_pre_ti7.vpcf", sender.Position);
						this.effect.SetControlPoint(1u, new Vector3(225f, 1f, 1f));
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000D894 File Offset: 0x0000BA94
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

		// Token: 0x0400010C RID: 268
		private readonly MenuSwitcher show;

		// Token: 0x0400010D RID: 269
		private ParticleEffect effect;

		// Token: 0x0400010E RID: 270
		private Team ownerTeam;
	}
}
