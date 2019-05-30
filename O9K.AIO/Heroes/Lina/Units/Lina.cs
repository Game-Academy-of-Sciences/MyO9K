using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Lina.Units
{
	// Token: 0x0200011B RID: 283
	[UnitName("npc_dota_hero_lina")]
	internal class Lina : ControllableUnit
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x0001C364 File Offset: 0x0001A564
		public Lina(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.lina_dragon_slave,
					(ActiveAbility x) => this.dragon = new NukeAbility(x)
				},
				{
					AbilityId.lina_light_strike_array,
					(ActiveAbility x) => this.array = new DisableAbility(x)
				},
				{
					AbilityId.lina_laguna_blade,
					(ActiveAbility x) => this.laguna = new NukeAbility(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_hurricane_pike,
					(ActiveAbility x) => this.pike = new HurricanePike(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_cyclone,
					(ActiveAbility x) => this.euls = new EulsScepterOfDivinity(x)
				},
				{
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				},
				{
					AbilityId.item_orchid,
					(ActiveAbility x) => this.orchid = new DisableAbility(x)
				},
				{
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.bloodthorn = new Bloodthorn(x)
				},
				{
					AbilityId.item_rod_of_atos,
					(ActiveAbility x) => this.atos = new DisableAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.lina_light_strike_array, (ActiveAbility _) => this.array);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001C4B0 File Offset: 0x0001A6B0
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			if (targetManager.TargetSleeper.IsSleeping)
			{
				return false;
			}
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.blink, 550f, 400f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.force, 550f, 400f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.pike, 550f, 400f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.hex, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.atos, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.orchid, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.bloodthorn, true))
			{
				return true;
			}
			if (abilityHelper.UseKillStealAbility(this.dragon, false))
			{
				return true;
			}
			if (abilityHelper.UseKillStealAbility(this.laguna, true))
			{
				base.ComboSleeper.ExtendSleep(0.2f);
				return true;
			}
			if (abilityHelper.UseAbilityIfAny(this.euls, new UsableAbility[]
			{
				this.array
			}))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.euls, false, true, true, true) && abilityHelper.CanBeCasted(this.array, false, true, true, true) && base.Owner.Speed > targetManager.Target.Speed + 50f)
			{
				this.preventAttackSleeper.Sleep(0.5f);
				return true;
			}
			if (abilityHelper.UseAbility(this.array, false))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.laguna, true))
			{
				base.ComboSleeper.ExtendSleep(0.2f);
				return true;
			}
			return abilityHelper.UseAbility(this.dragon, false) || (abilityHelper.CanBeCasted(this.pike, true, true, true, true) && !base.MoveSleeper.IsSleeping && this.pike.UseAbilityOnTarget(targetManager, base.ComboSleeper)) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00004EF2 File Offset: 0x000030F2
		protected override bool Attack(Unit9 target, ComboModeMenu comboMenu)
		{
			return !this.preventAttackSleeper.IsSleeping && base.Attack(target, comboMenu);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00004F0B File Offset: 0x0000310B
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseMoveAbility(this.array);
		}

		// Token: 0x04000317 RID: 791
		private readonly Sleeper preventAttackSleeper = new Sleeper();

		// Token: 0x04000318 RID: 792
		private DisableAbility array;

		// Token: 0x04000319 RID: 793
		private DisableAbility atos;

		// Token: 0x0400031A RID: 794
		private BlinkAbility blink;

		// Token: 0x0400031B RID: 795
		private DisableAbility bloodthorn;

		// Token: 0x0400031C RID: 796
		private NukeAbility dragon;

		// Token: 0x0400031D RID: 797
		private EulsScepterOfDivinity euls;

		// Token: 0x0400031E RID: 798
		private ForceStaff force;

		// Token: 0x0400031F RID: 799
		private DisableAbility hex;

		// Token: 0x04000320 RID: 800
		private NukeAbility laguna;

		// Token: 0x04000321 RID: 801
		private DisableAbility orchid;

		// Token: 0x04000322 RID: 802
		private SpeedBuffAbility phase;

		// Token: 0x04000323 RID: 803
		private HurricanePike pike;
	}
}
