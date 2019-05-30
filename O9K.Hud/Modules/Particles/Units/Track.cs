using System;
using System.Collections.Generic;
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
	// Token: 0x0200003B RID: 59
	internal class Track : IDisposable, IHudModule
	{
		// Token: 0x0600015F RID: 351 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
		[ImportingConstructor]
		public Track(IHudMenu hudMenu)
		{
			this.show = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Units")).Add<MenuSwitcher>(new MenuSwitcher("Track", "track", true, false));
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00002D56 File Offset: 0x00000F56
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000CA04 File Offset: 0x0000AC04
		public void Dispose()
		{
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			this.show.ValueChange -= this.ShowOnValueChange;
			Unit.OnModifierAdded -= this.OnModifierAdded;
			Unit.OnModifierRemoved -= this.OnModifierRemoved;
			foreach (KeyValuePair<uint, ParticleEffect> keyValuePair in this.effects)
			{
				keyValuePair.Value.Dispose();
			}
			this.effects.Clear();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.Id == AbilityId.bounty_hunter_track)
				{
					UpdateManager.BeginInvoke(delegate
					{
						EntityManager9.AbilityAdded -= this.OnAbilityAdded;
					}, 0);
					Unit.OnModifierAdded += this.OnModifierAdded;
					Unit.OnModifierRemoved += this.OnModifierRemoved;
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000CB20 File Offset: 0x0000AD20
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team == this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_bounty_hunter_track"))
					{
						ParticleEffect value = new ParticleEffect("particles/units/heroes/hero_bounty_hunter/bounty_hunter_track_shield.vpcf", sender, ParticleAttachment.OverheadFollow);
						this.effects.Add(sender.Handle, value);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000CB94 File Offset: 0x0000AD94
		private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team == this.ownerTeam)
				{
					if (!(args.Modifier.Name != "modifier_bounty_hunter_track"))
					{
						ParticleEffect particleEffect;
						if (this.effects.TryGetValue(sender.Handle, out particleEffect))
						{
							particleEffect.Dispose();
							this.effects.Remove(sender.Handle);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000CC20 File Offset: 0x0000AE20
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
			foreach (KeyValuePair<uint, ParticleEffect> keyValuePair in this.effects)
			{
				keyValuePair.Value.Dispose();
			}
			this.effects.Clear();
		}

		// Token: 0x040000F5 RID: 245
		private readonly Dictionary<uint, ParticleEffect> effects = new Dictionary<uint, ParticleEffect>();

		// Token: 0x040000F6 RID: 246
		private readonly MenuSwitcher show;

		// Token: 0x040000F7 RID: 247
		private Team ownerTeam;
	}
}
