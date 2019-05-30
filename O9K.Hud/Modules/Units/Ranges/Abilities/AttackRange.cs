using System;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using SharpDX;

namespace O9K.Hud.Modules.Units.Ranges.Abilities
{
	// Token: 0x0200000F RID: 15
	internal class AttackRange : IDisposable, IRange
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002259 File Offset: 0x00000459
		public AttackRange(Unit9 unit)
		{
			this.unit = unit;
			this.Handle = 0xffffffff;
			this.Name = "attack";
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000227A File Offset: 0x0000047A
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002282 File Offset: 0x00000482
		public bool IsEnabled { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000228B File Offset: 0x0000048B
		public string Name { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002293 File Offset: 0x00000493
		public uint Handle { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000229B File Offset: 0x0000049B
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000022A3 File Offset: 0x000004A3
		public float Range { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000022AC File Offset: 0x000004AC
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000022B4 File Offset: 0x000004B4
		public float DrawRange { get; private set; }

		// Token: 0x0600004D RID: 77 RVA: 0x000022BD File Offset: 0x000004BD
		public void Dispose()
		{
			ParticleEffect particleEffect = this.particleEffect;
			if (particleEffect != null)
			{
				particleEffect.Dispose();
			}
			this.particleEffect = null;
			this.IsEnabled = false;
			this.Range = 0f;
			this.DrawRange = 0f;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00005C8C File Offset: 0x00003E8C
		public void Enable(Menu heroMenu)
		{
			Menu menu = heroMenu.GetOrAdd<Menu>(new Menu("Attack Range", "attack")).SetTexture("attack");
			this.red = menu.GetOrAdd<MenuSlider>(new MenuSlider("Red", 255, 0, 255, false));
			this.green = menu.GetOrAdd<MenuSlider>(new MenuSlider("Green", 0, 0, 255, false));
			this.blue = menu.GetOrAdd<MenuSlider>(new MenuSlider("Blue", 0, 0, 255, false));
			this.red.ValueChange += this.ColorOnValueChange;
			this.green.ValueChange += this.ColorOnValueChange;
			this.blue.ValueChange += this.ColorOnValueChange;
			this.IsEnabled = true;
			this.UpdateRange();
			this.DelayedRedraw();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00005D70 File Offset: 0x00003F70
		public void UpdateRange()
		{
			float attackRange = this.unit.GetAttackRange(null, 0f);
			if (attackRange == this.Range)
			{
				return;
			}
			this.Range = attackRange;
			this.DrawRange = attackRange * 1.15f;
			this.RedrawRange();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000022F4 File Offset: 0x000004F4
		private void ColorOnValueChange(object sender, SliderEventArgs e)
		{
			if (e.NewValue == e.OldValue)
			{
				return;
			}
			this.DelayedRedraw();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000230B File Offset: 0x0000050B
		private void DelayedRedraw()
		{
			UpdateManager.BeginInvoke(new Action(this.RedrawRange), 0);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00005DB4 File Offset: 0x00003FB4
		private void RedrawRange()
		{
			if (this.particleEffect == null)
			{
				this.particleEffect = new ParticleEffect("particles\\ui_mouseactions\\drag_selected_ring.vpcf", this.unit.BaseUnit, ParticleAttachment.AbsOriginFollow);
			}
			this.particleEffect.SetControlPoint(1u, new Vector3(this.red, this.green, this.blue));
			this.particleEffect.SetControlPoint(2u, new Vector3(-this.DrawRange, 255f, 0f));
			this.particleEffect.FullRestart();
		}

		// Token: 0x04000026 RID: 38
		private readonly Unit9 unit;

		// Token: 0x04000027 RID: 39
		private MenuSlider blue;

		// Token: 0x04000028 RID: 40
		private MenuSlider green;

		// Token: 0x04000029 RID: 41
		private ParticleEffect particleEffect;

		// Token: 0x0400002A RID: 42
		private MenuSlider red;
	}
}
