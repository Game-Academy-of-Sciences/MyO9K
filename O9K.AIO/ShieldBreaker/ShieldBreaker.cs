using System;
using System.Collections.Generic;
using System.Linq;
using Ensage.SDK.Handlers;
using Ensage.SDK.Helpers;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Base;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.AIO.UnitManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.ShieldBreaker
{
	// Token: 0x0200001B RID: 27
	internal class ShieldBreaker : BaseMode
	{
		// Token: 0x0600009B RID: 155 RVA: 0x000098B4 File Offset: 0x00007AB4
		public ShieldBreaker(BaseHero baseHero) : base(baseHero)
		{
			this.targetManager = baseHero.TargetManager;
			this.orbwalkerSleeper = baseHero.OrbwalkSleeper;
			this.shieldBreakerMenu = new ShieldBreakerMenu(baseHero.Menu.RootMenu);
			this.updateHandler = UpdateManager.Subscribe(new Action(this.OnUpdate), 0, false);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000270E File Offset: 0x0000090E
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00002716 File Offset: 0x00000916
		public UnitManager UnitManager { get; set; }

		// Token: 0x0600009E RID: 158 RVA: 0x00009928 File Offset: 0x00007B28
		public void AddComboMenu(IEnumerable<ComboModeMenu> menus)
		{
			foreach (ComboModeMenu comboModeMenu in menus)
			{
				this.comboMenus.Add(comboModeMenu.Key, comboModeMenu);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000997C File Offset: 0x00007B7C
		public void Disable()
		{
			EntityManager9.AbilityAdded -= new EntityManager9.EventHandler<Ability9>(this.OnAbilityAdded);
			this.updateHandler.IsEnabled = false;
			foreach (KeyValuePair<MenuHoldKey, ComboModeMenu> keyValuePair in this.comboMenus)
			{
				keyValuePair.Key.ValueChange -= this.KeyOnValueChanged;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00009A00 File Offset: 0x00007C00
		public override void Dispose()
		{
			UpdateManager.Unsubscribe(this.updateHandler);
			EntityManager9.AbilityAdded -= new EntityManager9.EventHandler<Ability9>(this.OnAbilityAdded);
			foreach (KeyValuePair<MenuHoldKey, ComboModeMenu> keyValuePair in this.comboMenus)
			{
				keyValuePair.Key.ValueChange -= this.KeyOnValueChanged;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00009A80 File Offset: 0x00007C80
		public void Enable()
		{
			EntityManager9.AbilityAdded += new EntityManager9.EventHandler<Ability9>(this.OnAbilityAdded);
			foreach (KeyValuePair<MenuHoldKey, ComboModeMenu> keyValuePair in this.comboMenus)
			{
				keyValuePair.Key.ValueChange += this.KeyOnValueChanged;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000271F File Offset: 0x0000091F
		private void KeyOnValueChanged(object sender, KeyEventArgs e)
		{
			this.updateHandler.IsEnabled = e.NewValue;
			this.comboModeMenu = this.comboMenus[(MenuHoldKey)sender];
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00009AF8 File Offset: 0x00007CF8
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				ActiveAbility activeAbility;
				if (ability.IsControllable && ability.Owner.CanUseAbilities && ability.Owner.IsAlly(base.Owner) && (activeAbility = (ability as ActiveAbility)) != null)
				{
					if (activeAbility.UnitTargetCast && activeAbility.TargetsEnemy && activeAbility.BreaksLinkens)
					{
						this.shieldBreakerMenu.AddBreakerAbility(ability);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00009B80 File Offset: 0x00007D80
		private void OnUpdate()
		{
			try
			{
				if (this.targetManager.HasValidTarget)
				{
					if (this.comboModeMenu.IgnoreInvisibility || !base.Owner.Hero.IsInvisible)
					{
						if (!this.targetManager.TargetSleeper.IsSleeping)
						{
							Unit9 target = this.targetManager.Target;
							foreach (ControllableUnit controllableUnit in this.UnitManager.ControllableUnits)
							{
								foreach (ActiveAbility activeAbility in from x in (from x in controllableUnit.Owner.Abilities
								where (this.shieldBreakerMenu.IsLinkensBreakerEnabled(x.Name) || this.shieldBreakerMenu.IsSpellShieldBreakerEnabled(x.Name)) && x.CanBeCasted(true)
								select x).OfType<ActiveAbility>()
								orderby x.CastPoint
								select x)
								{
									if (activeAbility.CanHit(target) && this.ShouldUseBreaker(activeAbility, controllableUnit.Owner, target) && activeAbility.UseAbility(target, false, false))
									{
										this.linkensSleeper.Sleep(controllableUnit.Handle, activeAbility.GetHitTime(target) + 5.5f);
										this.orbwalkerSleeper.Sleep(controllableUnit.Handle, activeAbility.GetCastDelay(target));
										this.targetManager.TargetSleeper.Sleep(activeAbility.GetHitTime(target) + 0.1f);
										return;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00009D64 File Offset: 0x00007F64
		private bool ShouldUseBreaker(IActiveAbility ability, Unit9 owner, Unit9 target)
		{
			if (!ability.UnitTargetCast || this.linkensSleeper.IsSleeping(target.Handle))
			{
				return false;
			}
			if (target.IsUntargetable || !target.IsVisible || target.IsInvulnerable)
			{
				return false;
			}
			if (owner.Abilities.OfType<IDisable>().Any((IDisable x) => x.NoTargetCast && x.CanBeCasted(true) && x.CanHit(target)))
			{
				return false;
			}
			if ((target.IsLinkensProtected && target.IsLotusProtected) || target.IsSpellShieldProtected)
			{
				return this.shieldBreakerMenu.IsSpellShieldBreakerEnabled(ability.Name);
			}
			return target.IsLinkensProtected && this.shieldBreakerMenu.IsLinkensBreakerEnabled(ability.Name);
		}

		// Token: 0x04000058 RID: 88
		private readonly Dictionary<MenuHoldKey, ComboModeMenu> comboMenus = new Dictionary<MenuHoldKey, ComboModeMenu>();

		// Token: 0x04000059 RID: 89
		private readonly MultiSleeper linkensSleeper = new MultiSleeper();

		// Token: 0x0400005A RID: 90
		private readonly MultiSleeper orbwalkerSleeper;

		// Token: 0x0400005B RID: 91
		private readonly ShieldBreakerMenu shieldBreakerMenu;

		// Token: 0x0400005C RID: 92
		private readonly TargetManager targetManager;

		// Token: 0x0400005D RID: 93
		private readonly IUpdateHandler updateHandler;

		// Token: 0x0400005E RID: 94
		private ComboModeMenu comboModeMenu;
	}
}
