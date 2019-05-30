using System;
using System.ComponentModel.Composition;
using Ensage;
using Ensage.SDK.Handlers;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Hud.Core;

namespace O9K.Hud.Modules.Particles.Units
{
	// Token: 0x02000035 RID: 53
	internal class DarkPact : IDisposable, IHudModule
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00002B94 File Offset: 0x00000D94
		[ImportingConstructor]
		public DarkPact(IHudMenu hudMenu)
		{
			this.show = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Units")).Add<MenuSwitcher>(new MenuSwitcher("Dark pact", true, false));
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000B840 File Offset: 0x00009A40
		public void Dispose()
		{
			UpdateManager.Unsubscribe(this.handler);
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

		// Token: 0x06000132 RID: 306 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.Id == AbilityId.slark_dark_pact)
				{
					this.handler = UpdateManager.Subscribe(new Action(this.OnUpdate), 0, false);
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

		// Token: 0x06000133 RID: 307 RVA: 0x0000B928 File Offset: 0x00009B28
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team != this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_slark_dark_pact"))
					{
						this.unit = EntityManager9.GetUnit(sender.Handle);
						this.effect = new ParticleEffect("particles/units/heroes/hero_slark/slark_dark_pact_start.vpcf", sender, ParticleAttachment.AbsOriginFollow);
						Unit.OnModifierRemoved += this.OnModifierRemoved;
						this.handler.IsEnabled = true;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team != this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_slark_dark_pact"))
					{
						this.handler.IsEnabled = false;
						Unit.OnModifierRemoved -= this.OnModifierRemoved;
						ParticleEffect particleEffect = this.effect;
						if (particleEffect != null)
						{
							particleEffect.Dispose();
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000BA40 File Offset: 0x00009C40
		private void OnUpdate()
		{
			try
			{
				ParticleEffect particleEffect = this.effect;
				if (particleEffect != null && particleEffect.IsValid)
				{
					Unit9 unit = this.unit;
					if (unit != null && unit.IsAlive && this.unit.IsVisible)
					{
						this.effect.SetControlPoint(1u, this.unit.Position);
						return;
					}
				}
				this.handler.IsEnabled = false;
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000BAC8 File Offset: 0x00009CC8
		private void ShowOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				EntityManager9.AbilityAdded += this.OnAbilityAdded;
				return;
			}
			UpdateManager.Unsubscribe(this.handler);
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

		// Token: 0x040000DC RID: 220
		private readonly MenuSwitcher show;

		// Token: 0x040000DD RID: 221
		private ParticleEffect effect;

		// Token: 0x040000DE RID: 222
		private IUpdateHandler handler;

		// Token: 0x040000DF RID: 223
		private Team ownerTeam;

		// Token: 0x040000E0 RID: 224
		private Unit9 unit;
	}
}
