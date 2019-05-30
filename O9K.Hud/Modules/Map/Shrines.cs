using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Core.Managers.Renderer;
using O9K.Hud.Core;
using SharpDX;

namespace O9K.Hud.Modules.Map
{
	// Token: 0x02000049 RID: 73
	internal class Shrines : IDisposable, IHudModule
	{
		// Token: 0x060001AF RID: 431 RVA: 0x0000E330 File Offset: 0x0000C530
		[ImportingConstructor]
		public Shrines(IContext9 context, IHudMenu hudMenu)
		{
			this.context = context;
			Menu menu = hudMenu.MapMenu.Add<Menu>(new Menu("Shrines"));
			this.showCooldown = menu.Add<MenuSwitcher>(new MenuSwitcher("Show cooldown", true, false)).SetTooltip("Show ally shrine cooldown");
			this.showRadius = menu.Add<MenuSwitcher>(new MenuSwitcher("Show radius", true, false)).SetTooltip("Show ally shrine radius when used");
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00003164 File Offset: 0x00001364
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.showCooldown.ValueChange += this.ShowCooldownOnValueChange;
			this.showRadius.ValueChange += this.ShowRadiusOnValueChange;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000E3B0 File Offset: 0x0000C5B0
		public void Dispose()
		{
			Entity.OnParticleEffectAdded -= this.OnParticleEffectRadiantAdded;
			Entity.OnParticleEffectAdded -= this.OnParticleEffectDireAdded;
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
			this.showCooldown.ValueChange -= this.ShowCooldownOnValueChange;
			this.showRadius.ValueChange -= this.ShowRadiusOnValueChange;
			this.context.Renderer.Draw -= this.OnDraw;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000E44C File Offset: 0x0000C64C
		private void CheckAllyShrine(Vector3 position)
		{
			Ability9 ability = EntityManager9.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.filler_ability && x.Owner.Distance(position) < 500f);
			if (ability == null)
			{
				return;
			}
			ParticleEffect shrineRadius = new ParticleEffect("particles/ui_mouseactions/drag_selected_ring.vpcf", ability.Owner.Position);
			shrineRadius.SetControlPoint(1u, (this.ownerTeam == Team.Radiant) ? new Vector3(0f, 191f, 255f) : new Vector3(240f, 128f, 128f));
			shrineRadius.SetControlPoint(2u, new Vector3(-ability.Radius * 1.1f, 255f, 0f));
			UpdateManager.BeginInvoke(delegate
			{
				shrineRadius.Dispose();
			}, (int)ability.Duration * 1000);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000E528 File Offset: 0x0000C728
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.Id == AbilityId.filler_ability && ability.Owner.Team == this.ownerTeam)
				{
					this.shrineAbilities.Add(ability);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000E580 File Offset: 0x0000C780
		private void OnAbilityRemoved(Ability9 ability)
		{
			try
			{
				if (ability.Id == AbilityId.filler_ability && ability.Owner.Team == this.ownerTeam)
				{
					this.shrineAbilities.Remove(ability);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000E5D8 File Offset: 0x0000C7D8
		private void OnDraw(IRenderer renderer)
		{
			try
			{
				foreach (Ability9 ability in this.shrineAbilities)
				{
					if (ability.IsValid)
					{
						Vector2 vector = Drawing.WorldToScreen(ability.Owner.Position);
						if (!vector.IsZero)
						{
							float remainingCooldown = ability.RemainingCooldown;
							if (remainingCooldown > 0f)
							{
								renderer.DrawText(vector + new Vector2(0f, 75f), TimeSpan.FromSeconds((double)remainingCooldown).ToString("m\\:ss"), System.Drawing.Color.White, 17f * O9K.Core.Helpers.Hud.Info.ScreenRatio, "Calibri");
							}
						}
					}
				}
			}
			catch (InvalidOperationException)
			{
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
		private void OnParticleEffectDireAdded(Entity sender, ParticleEffectAddedEventArgs args)
		{
			try
			{
				if (((sender != null) ? new Team?(sender.Team) : null) == this.ownerTeam && args.ParticleEffect.Name == "particles/world_shrine/dire_shrine_active.vpcf")
				{
					this.CheckAllyShrine(args.ParticleEffect.GetControlPoint(0u));
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000E74C File Offset: 0x0000C94C
		private void OnParticleEffectRadiantAdded(Entity sender, ParticleEffectAddedEventArgs args)
		{
			try
			{
				if (((sender != null) ? new Team?(sender.Team) : null) == this.ownerTeam && args.ParticleEffect.Name == "particles/world_shrine/radiant_shrine_active.vpcf")
				{
					this.CheckAllyShrine(args.ParticleEffect.GetControlPoint(0u));
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000E7D4 File Offset: 0x0000C9D4
		private void ShowCooldownOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				EntityManager9.AbilityAdded += this.OnAbilityAdded;
				EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
				this.context.Renderer.Draw += this.OnDraw;
				return;
			}
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
			this.context.Renderer.Draw -= this.OnDraw;
			this.shrineAbilities.Clear();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000E874 File Offset: 0x0000CA74
		private void ShowRadiusOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (!e.NewValue)
			{
				Entity.OnParticleEffectAdded -= this.OnParticleEffectRadiantAdded;
				Entity.OnParticleEffectAdded -= this.OnParticleEffectDireAdded;
				return;
			}
			if (this.ownerTeam == Team.Radiant)
			{
				Entity.OnParticleEffectAdded += this.OnParticleEffectRadiantAdded;
				return;
			}
			Entity.OnParticleEffectAdded += this.OnParticleEffectDireAdded;
		}

		// Token: 0x0400012B RID: 299
		private readonly IContext9 context;

		// Token: 0x0400012C RID: 300
		private readonly MenuSwitcher showCooldown;

		// Token: 0x0400012D RID: 301
		private readonly MenuSwitcher showRadius;

		// Token: 0x0400012E RID: 302
		private readonly List<Ability9> shrineAbilities = new List<Ability9>();

		// Token: 0x0400012F RID: 303
		private Team ownerTeam;
	}
}
