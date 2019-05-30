using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Hud.Core;

namespace O9K.Hud.Modules.Particles.Units
{
	// Token: 0x02000039 RID: 57
	internal class LinkensSphere : IDisposable, IHudModule
	{
		// Token: 0x06000150 RID: 336 RVA: 0x0000C404 File Offset: 0x0000A604
		[ImportingConstructor]
		public LinkensSphere(IHudMenu hudMenu)
		{
			this.show = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Units")).Add<MenuSwitcher>(new MenuSwitcher("Linken's sphere", "sphere", false, false));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000C460 File Offset: 0x0000A660
		public void Dispose()
		{
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			UpdateManager.Unsubscribe(new Action(this.OnUpdate));
			this.show.ValueChange -= this.ShowOnValueChange;
			foreach (KeyValuePair<uint, ParticleEffect> keyValuePair in this.effects)
			{
				keyValuePair.Value.Dispose();
			}
			this.effects.Clear();
			this.units.Clear();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000C518 File Offset: 0x0000A718
		private void OnUnitAdded(Unit9 hero)
		{
			try
			{
				if (hero.IsHero && !hero.IsIllusion && hero.Team != this.ownerTeam)
				{
					this.units.Add(hero);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000C56C File Offset: 0x0000A76C
		private void OnUnitRemoved(Unit9 hero)
		{
			try
			{
				if (hero.IsHero && !hero.IsIllusion && hero.Team != this.ownerTeam)
				{
					this.units.Remove(hero);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		private void OnUpdate()
		{
			try
			{
				foreach (Unit9 unit in this.units)
				{
					if (unit.IsValid)
					{
						ParticleEffect particleEffect;
						if ((unit.IsLinkensProtected || unit.IsSpellShieldProtected) && unit.IsVisible && unit.IsAlive)
						{
							if (!this.effects.ContainsKey(unit.Handle))
							{
								this.effects[unit.Handle] = new ParticleEffect("particles/items_fx/immunity_sphere_buff.vpcf", unit.BaseUnit, ParticleAttachment.CenterFollow);
							}
						}
						else if (this.effects.TryGetValue(unit.Handle, out particleEffect))
						{
							particleEffect.Dispose();
							this.effects.Remove(unit.Handle);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
		private void ShowOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				EntityManager9.UnitAdded += this.OnUnitAdded;
				EntityManager9.UnitRemoved += this.OnUnitRemoved;
				UpdateManager.Subscribe(new Action(this.OnUpdate), 500, true);
				return;
			}
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			UpdateManager.Unsubscribe(new Action(this.OnUpdate));
			foreach (KeyValuePair<uint, ParticleEffect> keyValuePair in this.effects)
			{
				keyValuePair.Value.Dispose();
			}
			this.effects.Clear();
			this.units.Clear();
		}

		// Token: 0x040000EE RID: 238
		private readonly Dictionary<uint, ParticleEffect> effects = new Dictionary<uint, ParticleEffect>();

		// Token: 0x040000EF RID: 239
		private readonly MenuSwitcher show;

		// Token: 0x040000F0 RID: 240
		private readonly List<Unit9> units = new List<Unit9>();

		// Token: 0x040000F1 RID: 241
		private Team ownerTeam;
	}
}
