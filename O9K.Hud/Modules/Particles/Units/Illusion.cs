using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Ensage;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Hud.Core;
using SharpDX;

namespace O9K.Hud.Modules.Particles.Units
{
	// Token: 0x0200003D RID: 61
	internal class Illusion : IDisposable, IHudModule
	{
		// Token: 0x0600016D RID: 365 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		[ImportingConstructor]
		public Illusion(IHudMenu hudMenu)
		{
			this.showIllusions = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Units")).Add<MenuSwitcher>(new MenuSwitcher("Illusion", "illusion", true, false));
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00002DBB File Offset: 0x00000FBB
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.showIllusions.ValueChange += this.ShowIllusionsOnValueChanging;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000D01C File Offset: 0x0000B21C
		public void Dispose()
		{
			this.showIllusions.ValueChange -= this.ShowIllusionsOnValueChanging;
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			foreach (ParticleEffect particleEffect in this.effects.Values)
			{
				particleEffect.Dispose();
			}
			this.effects.Clear();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		private void OnUnitAdded(Unit9 entity)
		{
			try
			{
				if (entity.IsIllusion && entity.Team != this.ownerTeam && !entity.CanUseAbilities)
				{
					if ((entity.UnitState & UnitState.Unselectable) == (UnitState)0UL)
					{
						ParticleEffect particleEffect = new ParticleEffect("materials/ensage_ui/particles/illusions_mod_v2.vpcf", entity.BaseUnit, ParticleAttachment.CenterFollow);
						particleEffect.SetControlPoint(1u, new Vector3(255f));
						particleEffect.SetControlPoint(2u, new Vector3(65f, 105f, 255f));
						this.effects.Add(entity.Handle, particleEffect);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000D164 File Offset: 0x0000B364
		private void OnUnitRemoved(Unit9 entity)
		{
			try
			{
				ParticleEffect particleEffect;
				if (this.effects.TryGetValue(entity.Handle, out particleEffect))
				{
					particleEffect.Dispose();
					this.effects.Remove(entity.Handle);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000D1BC File Offset: 0x0000B3BC
		private void ShowIllusionsOnValueChanging(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				EntityManager9.UnitAdded += this.OnUnitAdded;
				EntityManager9.UnitRemoved += this.OnUnitRemoved;
				return;
			}
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			foreach (ParticleEffect particleEffect in this.effects.Values)
			{
				particleEffect.Dispose();
			}
			this.effects.Clear();
		}

		// Token: 0x040000FB RID: 251
		private readonly Dictionary<uint, ParticleEffect> effects = new Dictionary<uint, ParticleEffect>();

		// Token: 0x040000FC RID: 252
		private readonly MenuSwitcher showIllusions;

		// Token: 0x040000FD RID: 253
		private Team ownerTeam;
	}
}
