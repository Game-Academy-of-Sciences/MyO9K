using System;
using Ensage;
using Ensage.SDK.Handlers;
using Ensage.SDK.Helpers;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Base;
using O9K.AIO.UnitManager;
using O9K.Core.Logger;
using O9K.Core.Managers.Menu.EventArgs;

namespace O9K.AIO.Modes.KeyPress
{
	// Token: 0x02000024 RID: 36
	internal abstract class KeyPressMode : BaseMode
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x0000290A File Offset: 0x00000B0A
		protected KeyPressMode(BaseHero baseHero, KeyPressModeMenu menu) : base(baseHero)
		{
			this.UnitManager = baseHero.UnitManager;
			this.menu = menu;
			this.UpdateHandler = UpdateManager.Subscribe(new Action(this.OnUpdate), 0, false);
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002946 File Offset: 0x00000B46
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x0000294E File Offset: 0x00000B4E
		protected bool LockTarget { get; set; } = true;

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00002957 File Offset: 0x00000B57
		protected UnitManager UnitManager { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000295F File Offset: 0x00000B5F
		protected IUpdateHandler UpdateHandler { get; }

		// Token: 0x060000C8 RID: 200 RVA: 0x00002967 File Offset: 0x00000B67
		public virtual void Disable()
		{
			this.UpdateHandler.IsEnabled = false;
			this.menu.Key.ValueChange -= this.KeyOnValueChanged;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00002992 File Offset: 0x00000B92
		public override void Dispose()
		{
			base.Dispose();
			this.menu.Key.ValueChange -= this.KeyOnValueChanged;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000029B7 File Offset: 0x00000BB7
		public virtual void Enable()
		{
			this.menu.Key.ValueChange += this.KeyOnValueChanged;
		}

		// Token: 0x060000CB RID: 203
		protected abstract void ExecuteCombo();

		// Token: 0x060000CC RID: 204 RVA: 0x0000A120 File Offset: 0x00008320
		protected virtual void KeyOnValueChanged(object sender, KeyEventArgs e)
		{
			if (e.NewValue)
			{
				if (this.LockTarget)
				{
					base.TargetManager.TargetLocked = true;
				}
				this.UpdateHandler.IsEnabled = true;
				return;
			}
			this.UpdateHandler.IsEnabled = false;
			if (this.LockTarget)
			{
				base.TargetManager.TargetLocked = false;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000A178 File Offset: 0x00008378
		protected void OnUpdate()
		{
			if (Game.IsPaused)
			{
				return;
			}
			try
			{
				this.ExecuteCombo();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x0400006C RID: 108
		private readonly KeyPressModeMenu menu;
	}
}
