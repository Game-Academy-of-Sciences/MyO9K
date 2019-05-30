using System;
using System.ComponentModel.Composition;
using Ensage;
using Ensage.SDK.Extensions;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Hud.Core;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Helpers.Notificator.Notifications;
using SharpDX;

namespace O9K.Hud.Modules.Particles.Abilities
{
	// Token: 0x0200003E RID: 62
	internal class IceBlast : IDisposable, IHudModule
	{
		// Token: 0x06000173 RID: 371 RVA: 0x0000D26C File Offset: 0x0000B46C
		[ImportingConstructor]
		public IceBlast(INotificator notificator, IHudMenu hudMenu)
		{
			this.notificator = notificator;
			this.show = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Abilities")).Add<MenuSwitcher>(new MenuSwitcher("Ice blast", true, false));
			this.notificationsEnabled = hudMenu.NotificationsMenu.GetOrAdd<Menu>(new Menu("Abilities")).GetOrAdd<MenuSwitcher>(new MenuSwitcher("Enabled", true, false));
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
		public void Dispose()
		{
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			this.show.ValueChange -= this.ShowOnValueChange;
			Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
			ObjectManager.OnAddEntity -= this.OnAddEntity;
			ParticleEffect particleEffect = this.effect;
			if (particleEffect == null)
			{
				return;
			}
			particleEffect.Dispose();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000D348 File Offset: 0x0000B548
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.Id == AbilityId.ancient_apparition_ice_blast)
				{
					this.minRadius = new SpecialData(ability.BaseAbility, "radius_min");
					this.maxRadius = new SpecialData(ability.BaseAbility, "radius_max");
					this.growRadius = new SpecialData(ability.BaseAbility, "radius_grow");
					this.speed = new SpecialData(ability.BaseAbility, "speed");
					UpdateManager.BeginInvoke(delegate
					{
						EntityManager9.AbilityAdded -= this.OnAbilityAdded;
					}, 0);
					ObjectManager.OnAddEntity += this.OnAddEntity;
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000D3FC File Offset: 0x0000B5FC
		private void OnAddEntity(EntityEventArgs args)
		{
			try
			{
				Unit unit = args.Entity as Unit;
				if (!(unit == null) && unit.Team != this.ownerTeam && unit.UnitType == 0 && !(unit.NetworkName != "CDOTA_BaseNPC"))
				{
					if (unit.DayVision == 550u)
					{
						this.unitAddTime = Game.RawGameTime;
						Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;
						if (this.notificationsEnabled)
						{
							this.notificator.PushNotification(new AbilityNotification("npc_dota_hero_ancient_apparition", "ancient_apparition_ice_blast"));
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
		private void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
		{
			try
			{
				string name = args.Name;
				if (!(name == "particles/units/heroes/hero_ancient_apparition/ancient_apparition_ice_blast_final.vpcf") && !(name == "particles/econ/items/ancient_apparition/aa_blast_ti_5/ancient_apparition_ice_blast_final_ti5.vpcf"))
				{
					if (name == "particles/econ/items/ancient_apparition/aa_blast_ti_5/ancient_apparition_ice_blast_explode_ti5.vpcf" || name == "particles/units/heroes/hero_ancient_apparition/ancient_apparition_ice_blast_explode.vpcf")
					{
						Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
						ParticleEffect particleEffect = this.effect;
						if (particleEffect != null)
						{
							particleEffect.Dispose();
						}
					}
				}
				else
				{
					float time = Game.RawGameTime - Game.Ping / 1000f;
					UpdateManager.BeginInvoke(delegate
					{
						try
						{
							ParticleEffect particleEffect2 = args.ParticleEffect;
							if (particleEffect2.IsValid)
							{
								Vector3 controlPoint = particleEffect2.GetControlPoint(0u);
								Vector3 controlPoint2 = particleEffect2.GetControlPoint(1u);
								float num = time - this.unitAddTime;
								Vector3 to = controlPoint + controlPoint2;
								Vector3 v = controlPoint.Extend2D(to, num * this.speed.GetValue(1u));
								float num2 = Math.Min(this.maxRadius.GetValue(1u), Math.Max(num * this.growRadius.GetValue(1u) + this.minRadius.GetValue(1u), this.minRadius.GetValue(1u)));
								this.effect = new ParticleEffect("particles/units/heroes/hero_ancient_apparition/ancient_apparition_ice_blast_marker.vpcf", v.SetZ(new float?((float)384)));
								this.effect.SetControlPoint(1u, new Vector3(num2, 1f, 1f));
							}
						}
						catch (Exception exception2)
						{
							Logger.Error(exception2, null);
						}
					}, 0);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000D58C File Offset: 0x0000B78C
		private void ShowOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				EntityManager9.AbilityAdded += this.OnAbilityAdded;
				return;
			}
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
			ObjectManager.OnAddEntity -= this.OnAddEntity;
			ParticleEffect particleEffect = this.effect;
			if (particleEffect == null)
			{
				return;
			}
			particleEffect.Dispose();
		}

		// Token: 0x040000FE RID: 254
		private readonly MenuSwitcher notificationsEnabled;

		// Token: 0x040000FF RID: 255
		private readonly INotificator notificator;

		// Token: 0x04000100 RID: 256
		private readonly MenuSwitcher show;

		// Token: 0x04000101 RID: 257
		private ParticleEffect effect;

		// Token: 0x04000102 RID: 258
		private SpecialData growRadius;

		// Token: 0x04000103 RID: 259
		private SpecialData maxRadius;

		// Token: 0x04000104 RID: 260
		private SpecialData minRadius;

		// Token: 0x04000105 RID: 261
		private Team ownerTeam;

		// Token: 0x04000106 RID: 262
		private SpecialData speed;

		// Token: 0x04000107 RID: 263
		private float unitAddTime;
	}
}
