using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Ensage;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Hud.Core;

namespace O9K.Hud.Modules.Particles.Units
{
	// Token: 0x0200003C RID: 60
	internal class TrueSight : IDisposable, IHudModule
	{
		// Token: 0x06000167 RID: 359 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		[ImportingConstructor]
		public TrueSight(IHudMenu hudMenu)
		{
			this.show = hudMenu.ParticlesMenu.GetOrAdd<Menu>(new Menu("Units")).Add<MenuSwitcher>(new MenuSwitcher("True sight", "trueSight", true, false));
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00002D92 File Offset: 0x00000F92
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000CD20 File Offset: 0x0000AF20
		public void Dispose()
		{
			this.show.ValueChange -= this.ShowOnValueChange;
			Unit.OnModifierAdded -= this.OnModifierAdded;
			Unit.OnModifierRemoved -= this.OnModifierRemoved;
			foreach (KeyValuePair<uint, ParticleEffect> keyValuePair in this.effects)
			{
				keyValuePair.Value.Dispose();
			}
			this.effects.Clear();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000CDBC File Offset: 0x0000AFBC
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team == this.ownerTeam)
				{
					if (sender is Hero)
					{
						if (!(args.Modifier.Name != "modifier_truesight") || !(args.Modifier.Name != "modifier_item_dustofappearance"))
						{
							if (!this.effects.ContainsKey(sender.Handle))
							{
								ParticleEffect value = new ParticleEffect("particles/items2_fx/ward_true_sight.vpcf", sender, ParticleAttachment.CenterFollow);
								this.effects.Add(sender.Handle, value);
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000CE70 File Offset: 0x0000B070
		private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Team == this.ownerTeam)
				{
					if (sender is Hero)
					{
						if (!(args.Modifier.Name != "modifier_truesight") || !(args.Modifier.Name != "modifier_item_dustofappearance"))
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
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000CF1C File Offset: 0x0000B11C
		private void ShowOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				Unit.OnModifierAdded += this.OnModifierAdded;
				Unit.OnModifierRemoved += this.OnModifierRemoved;
				return;
			}
			Unit.OnModifierAdded -= this.OnModifierAdded;
			Unit.OnModifierRemoved -= this.OnModifierRemoved;
			foreach (KeyValuePair<uint, ParticleEffect> keyValuePair in this.effects)
			{
				keyValuePair.Value.Dispose();
			}
			this.effects.Clear();
		}

		// Token: 0x040000F8 RID: 248
		private readonly Dictionary<uint, ParticleEffect> effects = new Dictionary<uint, ParticleEffect>();

		// Token: 0x040000F9 RID: 249
		private readonly MenuSwitcher show;

		// Token: 0x040000FA RID: 250
		private Team ownerTeam;
	}
}
