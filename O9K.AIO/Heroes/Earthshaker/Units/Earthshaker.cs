using System;
using System.Collections.Generic;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Earthshaker.Abilities;
using O9K.AIO.Heroes.Earthshaker.Modes;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Earthshaker.Units
{
	// Token: 0x02000141 RID: 321
	[UnitName("npc_dota_hero_earthshaker")]
	internal class Earthshaker : ControllableUnit
	{
		// Token: 0x06000660 RID: 1632 RVA: 0x0001EF60 File Offset: 0x0001D160
		public Earthshaker(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.earthshaker_fissure,
					(ActiveAbility x) => this.fissure = new Fissure(x)
				},
				{
					AbilityId.earthshaker_enchant_totem,
					(ActiveAbility x) => this.totem = new DisableAbility(x)
				},
				{
					AbilityId.earthshaker_echo_slam,
					(ActiveAbility x) => this.echo = new EchoSlam(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkDaggerShaker(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				}
			};
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001EFF0 File Offset: 0x0001D1F0
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (comboModeMenu != null)
			{
				EarthshakerComboModeMenu earthshakerComboModeMenu;
				if ((earthshakerComboModeMenu = (comboModeMenu as EarthshakerComboModeMenu)) != null && earthshakerComboModeMenu.PreferEnchantTotem)
				{
					return this.TotemCombo(targetManager, abilityHelper);
				}
				if (comboModeMenu is EarthshakerEchoSlamComboModeMenu)
				{
					return this.EchoSlamCombo(targetManager, abilityHelper);
				}
			}
			if (abilityHelper.UseAbility(this.echo, true))
			{
				return true;
			}
			if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink, 0f))
			{
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
			{
				this.echo
			}))
			{
				UpdateManager.BeginInvoke(delegate
				{
					this.echo.ForceUseAbility(targetManager, this.ComboSleeper);
					this.OrbwalkSleeper.ExtendSleep(0.1f);
					this.ComboSleeper.ExtendSleep(0.1f);
				}, 111);
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
			{
				this.totem
			}))
			{
				UpdateManager.BeginInvoke(delegate
				{
					this.totem.ForceUseAbility(targetManager, this.ComboSleeper);
					this.OrbwalkSleeper.ExtendSleep(0.2f);
					this.ComboSleeper.ExtendSleep(0.2f);
				}, 111);
				return true;
			}
			return abilityHelper.UseAbility(this.force, 500f, 100f) || abilityHelper.UseAbility(this.fissure, true) || abilityHelper.UseAbility(this.totem, true);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001F130 File Offset: 0x0001D330
		public bool TotemCombo(TargetManager targetManager, AbilityHelper abilityHelper)
		{
			float num = base.Owner.Distance(targetManager.Target);
			if (num < 250f && base.Owner.HasModifier("modifier_earthshaker_enchant_totem"))
			{
				return false;
			}
			if (abilityHelper.UseAbility(this.totem, true))
			{
				return true;
			}
			if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink, 0f))
			{
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
			{
				this.totem
			}))
			{
				if (!base.Owner.HasModifier("modifier_earthshaker_enchant_totem"))
				{
					UpdateManager.BeginInvoke(delegate
					{
						this.totem.ForceUseAbility(targetManager, this.ComboSleeper);
						this.OrbwalkSleeper.ExtendSleep(0.2f);
						this.ComboSleeper.ExtendSleep(0.2f);
					}, 111);
				}
				else if (base.Owner.BaseUnit.Attack(targetManager.Target.BaseUnit))
				{
					base.OrbwalkSleeper.ExtendSleep(0.1f);
					base.ComboSleeper.ExtendSleep(0.1f);
					return true;
				}
				return true;
			}
			if (abilityHelper.UseAbility(this.force, 500f, 100f))
			{
				return true;
			}
			if (!abilityHelper.CanBeCasted(this.totem, true, true, true, true) && (!abilityHelper.CanBeCasted(this.blink, true, true, true, true) || num < 300f) && abilityHelper.UseAbility(this.fissure, true))
			{
				base.OrbwalkSleeper.ExtendSleep(0.1f);
				return true;
			}
			return abilityHelper.UseAbility(this.echo, true);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001F2B4 File Offset: 0x0001D4B4
		private bool EchoSlamCombo(TargetManager targetManager, AbilityHelper abilityHelper)
		{
			if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink, 0f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.echo, true))
			{
				base.ComboSleeper.ExtendSleep(0.1f);
				base.OrbwalkSleeper.ExtendSleep(0.1f);
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
			{
				this.echo
			}))
			{
				UpdateManager.BeginInvoke(delegate
				{
					this.echo.ForceUseAbility(targetManager, this.ComboSleeper);
					this.ComboSleeper.ExtendSleep(0.2f);
					this.OrbwalkSleeper.ExtendSleep(0.2f);
				}, 111);
				return true;
			}
			return !abilityHelper.CanBeCasted(this.echo, false, false, true, true) && (abilityHelper.UseAbility(this.totem, true) || abilityHelper.UseAbility(this.fissure, true));
		}

		// Token: 0x0400037F RID: 895
		private BlinkDaggerShaker blink;

		// Token: 0x04000380 RID: 896
		private EchoSlam echo;

		// Token: 0x04000381 RID: 897
		private Fissure fissure;

		// Token: 0x04000382 RID: 898
		private ForceStaff force;

		// Token: 0x04000383 RID: 899
		private DisableAbility totem;
	}
}
