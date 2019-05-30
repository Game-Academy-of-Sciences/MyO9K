using System;
using System.Collections.Generic;
using Ensage.SDK.Helpers;
using O9K.AIO.FailSafe;
using O9K.AIO.KillStealer;
using O9K.AIO.Menu;
using O9K.AIO.Modes.Combo;
using O9K.AIO.Modes.MoveCombo;
using O9K.AIO.ShieldBreaker;
using O9K.AIO.TargetManager;
using O9K.AIO.UnitManager;
using O9K.Core.Entities.Heroes;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;

namespace O9K.AIO.Heroes.Base
{
	// Token: 0x0200015A RID: 346
	internal class BaseHero : IDisposable
	{
		// Token: 0x060006CB RID: 1739 RVA: 0x00020B0C File Offset: 0x0001ED0C
		public BaseHero(IContext9 context)
		{
			this.Owner = EntityManager9.Owner;
			this.Menu = new MenuManager(this.Owner.Hero, context.MenuManager);
			this.TargetManager = new TargetManager(this.Menu);
			this.KillSteal = new KillSteal(this);
			this.FailSafe = new FailSafe(this);
			this.ShieldBreaker = new ShieldBreaker(this);
			this.MoveComboModeMenu = new MoveComboModeMenu(this.Menu.RootMenu, "Move");
			this.ComboMenus.Add(new ComboModeMenu(this.Menu.RootMenu, "Harass"));
			this.CreateComboMenus();
			this.ShieldBreaker.AddComboMenu(this.ComboMenus);
			this.CreateUnitManager();
			this.ShieldBreaker.UnitManager = this.UnitManager;
			this.combo = new ComboMode(this, this.ComboMenus);
			this.moveCombo = new MoveComboMode(this, this.MoveComboModeMenu);
			UpdateManager.BeginInvoke(delegate
			{
				this.Menu.Enabled.ValueChange += this.EnabledOnValueChange;
			}, 1000);
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x000056CA File Offset: 0x000038CA
		public MoveComboModeMenu MoveComboModeMenu { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x000056D2 File Offset: 0x000038D2
		public List<ComboModeMenu> ComboMenus { get; } = new List<ComboModeMenu>();

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x000056DA File Offset: 0x000038DA
		public Owner Owner { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x000056E2 File Offset: 0x000038E2
		public KillSteal KillSteal { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x000056EA File Offset: 0x000038EA
		public FailSafe FailSafe { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x000056F2 File Offset: 0x000038F2
		public ShieldBreaker ShieldBreaker { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x000056FA File Offset: 0x000038FA
		public TargetManager TargetManager { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00005702 File Offset: 0x00003902
		public MenuManager Menu { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0000570A File Offset: 0x0000390A
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00005712 File Offset: 0x00003912
		public UnitManager UnitManager { get; protected set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0000571B File Offset: 0x0000391B
		public MultiSleeper AbilitySleeper { get; } = new MultiSleeper();

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00005723 File Offset: 0x00003923
		public MultiSleeper OrbwalkSleeper { get; } = new MultiSleeper();

		// Token: 0x060006D8 RID: 1752 RVA: 0x0000572B File Offset: 0x0000392B
		public virtual void CreateUnitManager()
		{
			this.UnitManager = new UnitManager(this);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00020C40 File Offset: 0x0001EE40
		public virtual void Dispose()
		{
			this.Menu.Enabled.ValueChange -= this.EnabledOnValueChange;
			this.ComboMenus.Clear();
			this.combo.Dispose();
			this.moveCombo.Dispose();
			this.KillSteal.Dispose();
			this.FailSafe.Dispose();
			this.ShieldBreaker.Dispose();
			this.UnitManager.Dispose();
			this.Menu.Dispose();
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00020CC4 File Offset: 0x0001EEC4
		protected virtual void CreateComboMenus()
		{
			this.ComboMenus.Add(new ComboModeMenu(this.Menu.RootMenu, "Combo"));
			this.ComboMenus.Add(new ComboModeMenu(this.Menu.RootMenu, "Alternative combo"));
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00002B3D File Offset: 0x00000D3D
		protected virtual void DisableCustomModes()
		{
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00002B3D File Offset: 0x00000D3D
		protected virtual void EnableCustomModes()
		{
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00020D14 File Offset: 0x0001EF14
		private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
		{
			try
			{
				if (e.NewValue)
				{
					this.TargetManager.Enable();
					this.KillSteal.Enable();
					this.FailSafe.Enable();
					this.ShieldBreaker.Enable();
					this.UnitManager.Enable();
					this.combo.Enable();
					this.moveCombo.Enable();
					this.EnableCustomModes();
				}
				else
				{
					this.DisableCustomModes();
					this.TargetManager.Disable();
					this.KillSteal.Disable();
					this.FailSafe.Disable();
					this.ShieldBreaker.Disable();
					this.UnitManager.Disable();
					this.combo.Disable();
					this.moveCombo.Disable();
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x040003B6 RID: 950
		private readonly ComboMode combo;

		// Token: 0x040003B7 RID: 951
		private readonly MoveComboMode moveCombo;
	}
}
