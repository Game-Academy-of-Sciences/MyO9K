using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Leshrac.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Leshrac.Units
{
	// Token: 0x0200011D RID: 285
	[UnitName("npc_dota_hero_leshrac")]
	internal class Leshrac : ControllableUnit
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		public Leshrac(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.leshrac_split_earth,
					delegate(ActiveAbility x)
					{
						this.splitEarth = new SplitEarth(x);
						if (this.lightning != null)
						{
							this.splitEarth.Storm = this.lightning;
						}
						this.splitEarth.FailSafe = base.FailSafe;
						return this.splitEarth;
					}
				},
				{
					AbilityId.leshrac_diabolic_edict,
					(ActiveAbility x) => this.diabolic = new DiabolicEdict(x)
				},
				{
					AbilityId.leshrac_lightning_storm,
					delegate(ActiveAbility x)
					{
						this.lightning = new NukeAbility(x);
						if (this.splitEarth != null)
						{
							this.splitEarth.Storm = this.lightning;
						}
						return this.lightning;
					}
				},
				{
					AbilityId.leshrac_pulse_nova,
					(ActiveAbility x) => this.nova = new PulseNova(x)
				},
				{
					AbilityId.item_ethereal_blade,
					(ActiveAbility x) => this.ethereal = new EtherealBlade(x)
				},
				{
					AbilityId.item_veil_of_discord,
					(ActiveAbility x) => this.veil = new DebuffAbility(x)
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
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				},
				{
					AbilityId.item_rod_of_atos,
					(ActiveAbility x) => this.atos = new DisableAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.leshrac_split_earth, (ActiveAbility _) => this.splitEarth);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001C918 File Offset: 0x0001AB18
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.blink, 550f, 350f) || abilityHelper.UseAbility(this.veil, true) || abilityHelper.UseAbility(this.shiva, true) || abilityHelper.UseAbility(this.ethereal, true) || abilityHelper.UseAbility(this.hex, true) || abilityHelper.UseAbility(this.atos, true) || abilityHelper.UseAbilityIfAny(this.euls, new UsableAbility[]
			{
				this.splitEarth
			}) || (abilityHelper.CanBeCasted(this.nova, false, false, true, true) && this.nova.AutoToggle(targetManager)) || abilityHelper.UseAbilityIfCondition(this.diabolic, new UsableAbility[]
			{
				this.splitEarth
			}) || abilityHelper.UseAbility(this.lightning, true) || abilityHelper.UseAbilityIfNone(this.splitEarth, new UsableAbility[]
			{
				this.lightning,
				this.euls,
				this.atos
			});
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001CA40 File Offset: 0x0001AC40
		public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			if ((target == null || !target.IsMagicImmune) && ((comboMenu != null && comboMenu.IsAbilityEnabled(this.lightning.Ability) && this.lightning.Ability.CanBeCasted(true)) || (comboMenu != null && comboMenu.IsAbilityEnabled(this.splitEarth.Ability) && this.splitEarth.Ability.CanBeCasted(true))))
			{
				attack = false;
			}
			return base.Orbwalk(target, attack, move, comboMenu);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00004F31 File Offset: 0x00003131
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseMoveAbility(this.splitEarth);
		}

		// Token: 0x04000324 RID: 804
		private DisableAbility atos;

		// Token: 0x04000325 RID: 805
		private BlinkAbility blink;

		// Token: 0x04000326 RID: 806
		private DiabolicEdict diabolic;

		// Token: 0x04000327 RID: 807
		private EtherealBlade ethereal;

		// Token: 0x04000328 RID: 808
		private EulsScepterOfDivinity euls;

		// Token: 0x04000329 RID: 809
		private DisableAbility hex;

		// Token: 0x0400032A RID: 810
		private NukeAbility lightning;

		// Token: 0x0400032B RID: 811
		private PulseNova nova;

		// Token: 0x0400032C RID: 812
		private DebuffAbility shiva;

		// Token: 0x0400032D RID: 813
		private SplitEarth splitEarth;

		// Token: 0x0400032E RID: 814
		private DebuffAbility veil;
	}
}
