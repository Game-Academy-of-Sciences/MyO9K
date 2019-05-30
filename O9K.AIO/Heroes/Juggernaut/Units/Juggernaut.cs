using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Juggernaut.Abililities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Juggernaut.Units
{
	// Token: 0x02000160 RID: 352
	[UnitName("npc_dota_hero_juggernaut")]
	internal class Juggernaut : ControllableUnit
	{
		// Token: 0x06000740 RID: 1856 RVA: 0x00022084 File Offset: 0x00020284
		public Juggernaut(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.juggernaut_blade_fury,
					(ActiveAbility x) => this.bladeFury = new NukeAbility(x)
				},
				{
					AbilityId.juggernaut_healing_ward,
					(ActiveAbility x) => this.ward = new HealingWard(x)
				},
				{
					AbilityId.juggernaut_omni_slash,
					(ActiveAbility x) => this.omni = new Omnislash(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
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
					AbilityId.item_nullifier,
					(ActiveAbility x) => this.nullifier = new Nullifier(x)
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
					AbilityId.item_manta,
					(ActiveAbility x) => this.manta = new BuffAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_mjollnir,
					(ActiveAbility x) => this.mjollnir = new ShieldAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.juggernaut_blade_fury, (ActiveAbility x) => this.bladeFuryShield = new ShieldAbility(x));
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000221D0 File Offset: 0x000203D0
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.abyssal, true) || abilityHelper.UseAbility(this.orchid, true) || abilityHelper.UseAbility(this.bloodthorn, true) || abilityHelper.UseAbility(this.nullifier, true) || abilityHelper.UseAbility(this.diffusal, true) || abilityHelper.UseAbility(this.omni, true) || abilityHelper.UseAbility(this.blink, 400f, 0f) || abilityHelper.UseAbility(this.ward, true) || abilityHelper.UseAbility(this.bladeFury, true) || abilityHelper.UseAbility(this.manta, base.Owner.GetAttackRange(null, 0f)) || abilityHelper.UseAbility(this.mjollnir, 600f) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00005AF5 File Offset: 0x00003CF5
		public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			if (target != null && base.Owner.HasModifier("modifier_juggernaut_blade_fury"))
			{
				return base.Move(target.InFront(100f, 0f, true));
			}
			return base.Orbwalk(target, attack, move, comboMenu);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00005B35 File Offset: 0x00003D35
		protected override bool MoveComboUseShields(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseShields(abilityHelper) || abilityHelper.UseMoveAbility(this.bladeFuryShield);
		}

		// Token: 0x040003F5 RID: 1013
		private DisableAbility abyssal;

		// Token: 0x040003F6 RID: 1014
		private NukeAbility bladeFury;

		// Token: 0x040003F7 RID: 1015
		private ShieldAbility bladeFuryShield;

		// Token: 0x040003F8 RID: 1016
		private BlinkAbility blink;

		// Token: 0x040003F9 RID: 1017
		private DisableAbility bloodthorn;

		// Token: 0x040003FA RID: 1018
		private DebuffAbility diffusal;

		// Token: 0x040003FB RID: 1019
		private BuffAbility manta;

		// Token: 0x040003FC RID: 1020
		private ShieldAbility mjollnir;

		// Token: 0x040003FD RID: 1021
		private Nullifier nullifier;

		// Token: 0x040003FE RID: 1022
		private Omnislash omni;

		// Token: 0x040003FF RID: 1023
		private DisableAbility orchid;

		// Token: 0x04000400 RID: 1024
		private SpeedBuffAbility phase;

		// Token: 0x04000401 RID: 1025
		private HealingWard ward;
	}
}
