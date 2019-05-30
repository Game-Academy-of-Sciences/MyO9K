using System;
using Ensage;
using Ensage.SDK.Handlers;
using Ensage.SDK.Helpers;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Base;
using O9K.AIO.UnitManager;
using O9K.Core.Logger;
using O9K.Core.Managers.Menu.EventArgs;

namespace O9K.AIO.Modes.Permanent
{
	// Token: 0x0200001F RID: 31
	internal abstract class PermanentMode : BaseMode
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000027CB File Offset: 0x000009CB
		protected PermanentMode(BaseHero baseHero, PermanentModeMenu menu) : base(baseHero)
		{
			this.UnitManager = baseHero.UnitManager;
			this.menu = menu;
			this.Handler = UpdateManager.Subscribe(new Action(this.OnUpdate), 0, menu.Enabled);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000280A File Offset: 0x00000A0A
		protected UnitManager UnitManager { get; }

		// Token: 0x060000B2 RID: 178 RVA: 0x00002812 File Offset: 0x00000A12
		public virtual void Disable()
		{
			this.Handler.IsEnabled = false;
			this.menu.Enabled.ValueChange -= this.EnabledOnValueChanged;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000283C File Offset: 0x00000A3C
		public override void Dispose()
		{
			base.Dispose();
			UpdateManager.Unsubscribe(this.Handler);
			this.menu.Enabled.ValueChange -= this.EnabledOnValueChanged;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000286B File Offset: 0x00000A6B
		public virtual void Enable()
		{
			this.menu.Enabled.ValueChange += this.EnabledOnValueChanged;
		}

		// Token: 0x060000B5 RID: 181
		protected abstract void Execute();

		// Token: 0x060000B6 RID: 182 RVA: 0x00002889 File Offset: 0x00000A89
		private void EnabledOnValueChanged(object sender, SwitcherEventArgs e)
		{
			this.Handler.IsEnabled = e.NewValue;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00009EFC File Offset: 0x000080FC
		private void OnUpdate()
		{
			if (Game.IsPaused)
			{
				return;
			}
			try
			{
				this.Execute();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x04000065 RID: 101
		protected readonly IUpdateHandler Handler;

		// Token: 0x04000066 RID: 102
		private readonly PermanentModeMenu menu;
	}
}
