using System;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using SharpDX;

namespace O9K.Hud.Modules.Units.Ranges.Abilities
{
	// Token: 0x02000011 RID: 17
	internal class AbilityRange : IDisposable, IRange
	{
		// Token: 0x06000058 RID: 88 RVA: 0x0000231F File Offset: 0x0000051F
		public AbilityRange(Ability9 ability)
		{
			this.ability = ability;
			this.Name = ability.Name;
			this.Handle = ability.Handle;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002346 File Offset: 0x00000546
		public uint Handle { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000234E File Offset: 0x0000054E
		public string Name { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002356 File Offset: 0x00000556
		public bool IsEnabled
		{
			get
			{
				return this.enabled && this.ability.IsValid && this.ability.IsUsable;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000237A File Offset: 0x0000057A
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002382 File Offset: 0x00000582
		public float Range { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000238B File Offset: 0x0000058B
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002393 File Offset: 0x00000593
		public float DrawRange { get; private set; }

		// Token: 0x06000060 RID: 96 RVA: 0x0000239C File Offset: 0x0000059C
		public void Dispose()
		{
			ParticleEffect particleEffect = this.particleEffect;
			if (particleEffect != null)
			{
				particleEffect.Dispose();
			}
			this.particleEffect = null;
			this.enabled = false;
			this.Range = 0f;
			this.DrawRange = 0f;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005E4C File Offset: 0x0000404C
		public void Enable(Menu heroMenu)
		{
			Menu menu = heroMenu.GetOrAdd<Menu>(new Menu(this.ability.DisplayName, this.ability.DefaultName)).SetTexture(this.ability.Name);
			this.red = menu.GetOrAdd<MenuSlider>(new MenuSlider("Red", 255, 0, 255, false));
			this.green = menu.GetOrAdd<MenuSlider>(new MenuSlider("Green", 0, 0, 255, false));
			this.blue = menu.GetOrAdd<MenuSlider>(new MenuSlider("Blue", 0, 0, 255, false));
			this.red.ValueChange += this.ColorOnValueChange;
			this.green.ValueChange += this.ColorOnValueChange;
			this.blue.ValueChange += this.ColorOnValueChange;
			this.enabled = true;
			this.UpdateRange();
			this.DelayedRedraw();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005F44 File Offset: 0x00004144
		public void UpdateRange()
		{
			if ((this.ability.AbilityBehavior & AbilityBehavior.NoTarget) != AbilityBehavior.None)
			{
				float radius = this.ability.Radius;
				if (radius == this.Range)
				{
					return;
				}
				this.Range = radius;
				this.DrawRange = radius + Math.Max(radius / 7f, 40f);
				this.RedrawRange();
				return;
			}
			else
			{
				float castRange = this.ability.CastRange;
				if (castRange == this.Range)
				{
					return;
				}
				this.Range = castRange;
				this.DrawRange = castRange + Math.Max(castRange / 9f, 80f);
				this.RedrawRange();
				return;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000023D3 File Offset: 0x000005D3
		private void ColorOnValueChange(object sender, SliderEventArgs e)
		{
			if (e.NewValue == e.OldValue)
			{
				return;
			}
			this.DelayedRedraw();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000023EA File Offset: 0x000005EA
		private void DelayedRedraw()
		{
			UpdateManager.BeginInvoke(new Action(this.RedrawRange), 0);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00005FDC File Offset: 0x000041DC
		private void RedrawRange()
		{
			if (this.particleEffect == null)
			{
				this.particleEffect = new ParticleEffect("particles\\ui_mouseactions\\drag_selected_ring.vpcf", this.ability.Owner.BaseUnit, ParticleAttachment.AbsOriginFollow);
			}
			this.particleEffect.SetControlPoint(1u, new Vector3(this.red, this.green, this.blue));
			this.particleEffect.SetControlPoint(2u, new Vector3(-this.DrawRange, 255f, 0f));
			this.particleEffect.FullRestart();
		}

		// Token: 0x04000030 RID: 48
		private readonly Ability9 ability;

		// Token: 0x04000031 RID: 49
		private MenuSlider blue;

		// Token: 0x04000032 RID: 50
		private bool enabled;

		// Token: 0x04000033 RID: 51
		private MenuSlider green;

		// Token: 0x04000034 RID: 52
		private ParticleEffect particleEffect;

		// Token: 0x04000035 RID: 53
		private MenuSlider red;
	}
}
