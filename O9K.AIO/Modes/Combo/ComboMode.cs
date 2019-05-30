using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ensage;
using Ensage.SDK.Handlers;
using Ensage.SDK.Helpers;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Base;
using O9K.AIO.UnitManager;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Modes.Combo
{
	// Token: 0x02000026 RID: 38
	internal class ComboMode : BaseMode
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x0000A218 File Offset: 0x00008418
		public ComboMode(BaseHero baseHero, IEnumerable<ComboModeMenu> comboMenus) : base(baseHero)
		{
			this.UnitManager = baseHero.UnitManager;
			this.updateHandler = UpdateManager.Subscribe(new Action(this.OnUpdate), 0, false);
			foreach (ComboModeMenu comboModeMenu in comboMenus)
			{
				this.comboModeMenus.Add(comboModeMenu.Key, comboModeMenu);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000029DE File Offset: 0x00000BDE
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x000029E6 File Offset: 0x00000BE6
		protected ComboModeMenu ComboModeMenu { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000029EF File Offset: 0x00000BEF
		protected UnitManager UnitManager { get; }

		// Token: 0x060000D4 RID: 212 RVA: 0x0000A2C4 File Offset: 0x000084C4
		public void Disable()
		{
			this.updateHandler.IsEnabled = false;
			Player.OnExecuteOrder -= this.OnExecuteOrder;
			foreach (KeyValuePair<MenuHoldKey, ComboModeMenu> keyValuePair in this.comboModeMenus)
			{
				keyValuePair.Key.ValueChange -= this.KeyOnValueChanged;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000A348 File Offset: 0x00008548
		public override void Dispose()
		{
			UpdateManager.Unsubscribe(this.updateHandler);
			Player.OnExecuteOrder -= this.OnExecuteOrder;
			foreach (KeyValuePair<MenuHoldKey, ComboModeMenu> keyValuePair in this.comboModeMenus)
			{
				keyValuePair.Key.ValueChange -= this.KeyOnValueChanged;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000A3C8 File Offset: 0x000085C8
		public void Enable()
		{
			Player.OnExecuteOrder += this.OnExecuteOrder;
			foreach (KeyValuePair<MenuHoldKey, ComboModeMenu> keyValuePair in this.comboModeMenus)
			{
				keyValuePair.Key.ValueChange += this.KeyOnValueChanged;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000A440 File Offset: 0x00008640
		protected void ComboEnd()
		{
			try
			{
				foreach (uint num in this.disableToggleAbilities.Distinct<uint>().ToList<uint>())
				{
					IToggleable ability;
					if ((ability = (EntityManager9.GetAbility(num) as IToggleable)) != null)
					{
						UpdateManager.BeginInvoke(delegate
						{
							this.ToggleAbility(ability);
						}, 0);
					}
				}
				this.UnitManager.EndCombo(this.ComboModeMenu);
				this.disableToggleAbilities.Clear();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000A500 File Offset: 0x00008700
		protected void OnUpdate()
		{
			if (Game.IsPaused)
			{
				return;
			}
			try
			{
				if (base.TargetManager.HasValidTarget)
				{
					this.UnitManager.ExecuteCombo(this.ComboModeMenu);
				}
				this.UnitManager.Orbwalk(this.ComboModeMenu);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000A560 File Offset: 0x00008760
		private void KeyOnValueChanged(object sender, KeyEventArgs e)
		{
			if (e.NewValue)
			{
				if (this.updateHandler.IsEnabled)
				{
					this.ignoreComboEnd = true;
				}
				this.ComboModeMenu = this.comboModeMenus[(MenuHoldKey)sender];
				base.TargetManager.TargetLocked = true;
				this.updateHandler.IsEnabled = true;
				return;
			}
			if (this.ignoreComboEnd)
			{
				this.ignoreComboEnd = false;
				return;
			}
			this.updateHandler.IsEnabled = false;
			base.TargetManager.TargetLocked = false;
			this.ComboEnd();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000A5E8 File Offset: 0x000087E8
		private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
		{
			try
			{
				if (this.updateHandler.IsEnabled && args.Process && !args.IsPlayerInput)
				{
					OrderId orderId = args.OrderId;
					if (orderId == OrderId.ToggleAbility || orderId == OrderId.ToggleAutoCast)
					{
						if (!this.ignoreToggleDisable.Contains(args.Ability.Id))
						{
							this.disableToggleAbilities.Add(args.Ability.Handle);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000A674 File Offset: 0x00008874
		private async void ToggleAbility(IToggleable toggle)
		{
			try
			{
				if (!toggle.Enabled)
				{
					await Task.Delay(200);
				}
				while (toggle.IsValid && toggle.Enabled && !this.updateHandler.IsEnabled)
				{
					if (toggle.CanBeCasted(true) && !toggle.Owner.IsCasting)
					{
						toggle.Enabled = false;
						break;
					}
					await Task.Delay(200);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x04000071 RID: 113
		private readonly Dictionary<MenuHoldKey, ComboModeMenu> comboModeMenus = new Dictionary<MenuHoldKey, ComboModeMenu>();

		// Token: 0x04000072 RID: 114
		private readonly List<uint> disableToggleAbilities = new List<uint>();

		// Token: 0x04000073 RID: 115
		private readonly HashSet<AbilityId> ignoreToggleDisable = new HashSet<AbilityId>
		{
			AbilityId.troll_warlord_berserkers_rage
		};

		// Token: 0x04000074 RID: 116
		private readonly IUpdateHandler updateHandler;

		// Token: 0x04000075 RID: 117
		private bool ignoreComboEnd;
	}
}
