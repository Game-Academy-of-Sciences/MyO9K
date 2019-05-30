using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Hud.Core;
using SharpDX;

namespace O9K.Hud.Modules.Particles.Units
{
	// Token: 0x02000036 RID: 54
	internal class EyesInTheForest : IDisposable, IHudModule
	{
		// Token: 0x06000138 RID: 312 RVA: 0x0000BB40 File Offset: 0x00009D40
		[ImportingConstructor]
		public EyesInTheForest(IHudMenu hudMenu)
		{
			this.show = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Units")).Add<MenuSwitcher>(new MenuSwitcher("Eyes in the forest", "eyes", true, false));
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00002C04 File Offset: 0x00000E04
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000BB90 File Offset: 0x00009D90
		public void Dispose()
		{
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			EntityManager9.UnitMonitor.UnitDied -= this.OnUnitRemoved;
			this.show.ValueChange -= this.ShowOnValueChange;
			foreach (ParticleEffect particleEffect in this.effects.Values)
			{
				particleEffect.Dispose();
			}
			this.effects.Clear();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000BC50 File Offset: 0x00009E50
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.Id == AbilityId.treant_eyes_in_the_forest)
				{
					UpdateManager.BeginInvoke(delegate
					{
						EntityManager9.AbilityAdded -= this.OnAbilityAdded;
					}, 0);
					EntityManager9.UnitAdded += this.OnUnitAdded;
					EntityManager9.UnitRemoved += this.OnUnitRemoved;
					EntityManager9.UnitMonitor.UnitDied += this.OnUnitRemoved;
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		private void OnUnitAdded(Unit9 unit)
		{
			try
			{
				if (unit.Team != this.ownerTeam && !(unit.Name != "npc_dota_treant_eyes"))
				{
					ParticleEffect particleEffect = new ParticleEffect("particles/units/heroes/hero_treant/treant_eyesintheforest.vpcf", unit.Position);
					particleEffect.SetControlPoint(1u, new Vector3(800f));
					this.effects.Add(unit.Handle, particleEffect);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000BD50 File Offset: 0x00009F50
		private void OnUnitRemoved(Unit9 unit)
		{
			try
			{
				if (unit.Team != this.ownerTeam)
				{
					ParticleEffect particleEffect;
					if (this.effects.TryGetValue(unit.Handle, out particleEffect))
					{
						particleEffect.Dispose();
						this.effects.Remove(unit.Handle);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		private void ShowOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				EntityManager9.AbilityAdded += this.OnAbilityAdded;
				return;
			}
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			EntityManager9.UnitMonitor.UnitDied -= this.OnUnitRemoved;
			foreach (ParticleEffect particleEffect in this.effects.Values)
			{
				particleEffect.Dispose();
			}
			this.effects.Clear();
		}

		// Token: 0x040000E1 RID: 225
		private readonly Dictionary<uint, ParticleEffect> effects = new Dictionary<uint, ParticleEffect>();

		// Token: 0x040000E2 RID: 226
		private readonly MenuSwitcher show;

		// Token: 0x040000E3 RID: 227
		private Team ownerTeam;
	}
}
