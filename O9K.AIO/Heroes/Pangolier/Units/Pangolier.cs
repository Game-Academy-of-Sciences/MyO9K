using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Pangolier.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using SharpDX;

namespace O9K.AIO.Heroes.Pangolier.Units
{
	// Token: 0x020000E3 RID: 227
	[UnitName("npc_dota_hero_pangolier")]
	internal class Pangolier : ControllableUnit, IDisposable
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x00018500 File Offset: 0x00016700
		public Pangolier(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.pangolier_swashbuckle,
					(ActiveAbility x) => this.swashbuckle = new Swashbuckle(x)
				},
				{
					AbilityId.pangolier_shield_crash,
					(ActiveAbility x) => this.crash = new ShieldCrash(x)
				},
				{
					AbilityId.pangolier_gyroshell,
					(ActiveAbility x) => this.thunder = new RollingThunder(x)
				},
				{
					AbilityId.item_diffusal_blade,
					(ActiveAbility x) => this.diffusal = new DebuffAbility(x)
				},
				{
					AbilityId.item_abyssal_blade,
					(ActiveAbility x) => this.abyssal = new DisableAbility(x)
				},
				{
					AbilityId.item_mjollnir,
					(ActiveAbility x) => this.mjollnir = new ShieldAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkDaggerPangolier(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.pangolier_swashbuckle, (ActiveAbility x) => this.moveSwashbuckle = new SwashbuckleBlink(x));
			Player.OnExecuteOrder += this.OnExecuteOrder;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000185F8 File Offset: 0x000167F8
		private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
		{
			try
			{
				if (args.Process && args.IsPlayerInput && args.OrderId == OrderId.Ability && args.Ability.Id == AbilityId.pangolier_gyroshell)
				{
					if (this.thunder != null)
					{
						this.ultSleeper.Sleep(this.thunder.Ability.GetCastDelay() + 0.15f);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000046CD File Offset: 0x000028CD
		public void Dispose()
		{
			Player.OnExecuteOrder -= this.OnExecuteOrder;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00018678 File Offset: 0x00016878
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			if (this.ultSleeper)
			{
				return false;
			}
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.abyssal, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.diffusal, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.swashbuckle, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.mjollnir, 600f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.crash, true))
			{
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.thunder, new UsableAbility[]
			{
				this.blink
			}))
			{
				base.ComboSleeper.Sleep(this.thunder.Ability.CastPoint + 0.1f);
				return true;
			}
			return abilityHelper.UseAbility(this.blink, true);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00018748 File Offset: 0x00016948
		public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			if (base.OrbwalkSleeper.IsSleeping)
			{
				return false;
			}
			if (base.Owner.HasModifier("modifier_pangolier_gyroshell") && target != null)
			{
				if (base.Owner.GetAngle(target.Position, false) > 1.5f)
				{
					Vector3 position = this.thunder.GetPosition(target);
					if (!position.IsZero && base.Owner.BaseUnit.Move(position))
					{
						base.OrbwalkSleeper.Sleep(1f);
						return true;
					}
				}
				if (base.Owner.BaseUnit.Move(target.Position))
				{
					base.OrbwalkSleeper.Sleep(0.5f);
					return true;
				}
			}
			return base.Orbwalk(target, attack, move, comboMenu);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000046E0 File Offset: 0x000028E0
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBlinks(abilityHelper) || abilityHelper.UseMoveAbility(this.moveSwashbuckle);
		}

		// Token: 0x04000285 RID: 645
		private DisableAbility abyssal;

		// Token: 0x04000286 RID: 646
		private BlinkDaggerPangolier blink;

		// Token: 0x04000287 RID: 647
		private ShieldCrash crash;

		// Token: 0x04000288 RID: 648
		private DebuffAbility diffusal;

		// Token: 0x04000289 RID: 649
		private ShieldAbility mjollnir;

		// Token: 0x0400028A RID: 650
		private SwashbuckleBlink moveSwashbuckle;

		// Token: 0x0400028B RID: 651
		private Swashbuckle swashbuckle;

		// Token: 0x0400028C RID: 652
		private RollingThunder thunder;

		// Token: 0x0400028D RID: 653
		private Sleeper ultSleeper = new Sleeper();
	}
}
