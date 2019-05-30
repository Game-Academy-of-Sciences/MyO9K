using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Leshrac.Abilities;
using O9K.AIO.Heroes.SandKing.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.SandKing.Units
{
	// Token: 0x020000B5 RID: 181
	[UnitName("npc_dota_hero_sand_king")]
	internal class SandKing : ControllableUnit
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x00014AAC File Offset: 0x00012CAC
		public SandKing(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.sandking_burrowstrike,
					(ActiveAbility x) => this.burrow = new Burrowstrike(x)
				},
				{
					AbilityId.sandking_sand_storm,
					(ActiveAbility x) => this.sandstorm = new UntargetableAbility(x)
				},
				{
					AbilityId.sandking_epicenter,
					(ActiveAbility x) => this.epicenter = new Epicenter(x)
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
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.sandking_burrowstrike, (ActiveAbility x) => this.burrowBlink = new BlinkAbility(x));
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00014B84 File Offset: 0x00012D84
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.CanBeCastedIfCondition(this.epicenter, new UsableAbility[]
			{
				this.blink,
				this.burrow
			}))
			{
				if (abilityHelper.UseAbility(this.sandstorm, true))
				{
					return true;
				}
				if (abilityHelper.UseAbility(this.epicenter, true))
				{
					return true;
				}
			}
			if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink, 300f))
			{
				return true;
			}
			if (abilityHelper.UseBlinkLineCombo(this.blink, this.burrow))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.burrow, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.force, 800f, this.burrow.Ability.CastRange))
			{
				return true;
			}
			float num = base.Owner.Distance(targetManager.Target);
			if (num < 450f && abilityHelper.UseAbility(this.veil, true))
			{
				return true;
			}
			if (num < 450f && abilityHelper.UseAbility(this.shiva, true))
			{
				return true;
			}
			if (base.Owner.HasModifier("modifier_sand_king_epicenter") && num < 450f && abilityHelper.UseAbilityIfNone(this.sandstorm, new UsableAbility[]
			{
				this.epicenter,
				this.burrow
			}))
			{
				base.ComboSleeper.Sleep(0.2f);
				return true;
			}
			return false;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00004129 File Offset: 0x00002329
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBlinks(abilityHelper) || abilityHelper.UseMoveAbility(this.burrowBlink);
		}

		// Token: 0x04000203 RID: 515
		private BlinkAbility blink;

		// Token: 0x04000204 RID: 516
		private Burrowstrike burrow;

		// Token: 0x04000205 RID: 517
		private BlinkAbility burrowBlink;

		// Token: 0x04000206 RID: 518
		private Epicenter epicenter;

		// Token: 0x04000207 RID: 519
		private ForceStaff force;

		// Token: 0x04000208 RID: 520
		private UntargetableAbility sandstorm;

		// Token: 0x04000209 RID: 521
		private DebuffAbility shiva;

		// Token: 0x0400020A RID: 522
		private DebuffAbility veil;
	}
}
