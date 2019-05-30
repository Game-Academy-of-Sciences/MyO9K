using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.DarkWillow.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.DarkWillow.Units
{
	// Token: 0x02000155 RID: 341
	[UnitName("npc_dota_hero_dark_willow")]
	internal class DarkWillow : ControllableUnit
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x00020268 File Offset: 0x0001E468
		public DarkWillow(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.dark_willow_bramble_maze,
					(ActiveAbility x) => this.maze = new BrambleMaze(x)
				},
				{
					AbilityId.dark_willow_shadow_realm,
					(ActiveAbility x) => this.realm = new ShadowRealm(x)
				},
				{
					AbilityId.dark_willow_cursed_crown,
					(ActiveAbility x) => this.crown = new DebuffAbility(x)
				},
				{
					AbilityId.dark_willow_bedlam,
					(ActiveAbility x) => this.bedlam = new NukeAbility(x)
				},
				{
					AbilityId.dark_willow_terrorize,
					(ActiveAbility x) => this.terror = new DisableAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_cyclone,
					(ActiveAbility x) => this.eul = new EulsScepterOfDivinityDarkWillow(x)
				},
				{
					AbilityId.item_veil_of_discord,
					(ActiveAbility x) => this.veil = new DebuffAbility(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_spirit_vessel,
					(ActiveAbility x) => this.vessel = new DebuffAbility(x)
				},
				{
					AbilityId.item_urn_of_shadows,
					(ActiveAbility x) => this.urn = new DebuffAbility(x)
				},
				{
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				},
				{
					AbilityId.item_nullifier,
					(ActiveAbility x) => this.nullifier = new Nullifier(x)
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
			base.MoveComboAbilities.Add(AbilityId.dark_willow_shadow_realm, (ActiveAbility x) => this.moveRealm = new ShieldAbility(x));
			base.MoveComboAbilities.Add(AbilityId.dark_willow_cursed_crown, (ActiveAbility x) => this.moveCrown = new DisableAbility(x));
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00020420 File Offset: 0x0001E620
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.blink, 600f, 450f) || abilityHelper.UseAbility(this.force, 600f, 450f) || abilityHelper.UseAbility(this.veil, true) || abilityHelper.UseAbility(this.hex, true) || abilityHelper.UseAbility(this.atos, true) || abilityHelper.UseAbility(this.orchid, true) || abilityHelper.UseAbility(this.bloodthorn, true) || abilityHelper.UseAbility(this.nullifier, true) || abilityHelper.UseAbility(this.crown, true) || abilityHelper.UseAbilityIfCondition(this.eul, new UsableAbility[]
			{
				this.crown
			}) || abilityHelper.UseAbility(this.realm, true) || abilityHelper.UseAbility(this.maze, true) || abilityHelper.UseAbility(this.bedlam, true) || abilityHelper.UseAbility(this.vessel, true) || abilityHelper.UseAbility(this.urn, true) || abilityHelper.UseAbility(this.terror, true);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00020568 File Offset: 0x0001E768
		public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			if (target != null)
			{
				if (this.realm.Casted)
				{
					return base.Orbwalk(target, this.realm.ShouldAttack(target), move, comboMenu);
				}
				if (base.Owner.HasModifier("modifier_dark_willow_bedlam") && base.Owner.Distance(target) > this.bedlam.Ability.Radius * 0.5f)
				{
					return this.ForceMove(target, true);
				}
			}
			return base.Orbwalk(target, attack, move, comboMenu);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00005619 File Offset: 0x00003819
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseMoveAbility(this.moveCrown);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00005637 File Offset: 0x00003837
		protected override bool MoveComboUseShields(AbilityHelper abilityHelper)
		{
			return abilityHelper.UseMoveAbility(this.moveRealm) || base.MoveComboUseShields(abilityHelper);
		}

		// Token: 0x040003A1 RID: 929
		private DisableAbility atos;

		// Token: 0x040003A2 RID: 930
		private NukeAbility bedlam;

		// Token: 0x040003A3 RID: 931
		private BlinkAbility blink;

		// Token: 0x040003A4 RID: 932
		private DisableAbility bloodthorn;

		// Token: 0x040003A5 RID: 933
		private DebuffAbility crown;

		// Token: 0x040003A6 RID: 934
		private EulsScepterOfDivinityDarkWillow eul;

		// Token: 0x040003A7 RID: 935
		private ForceStaff force;

		// Token: 0x040003A8 RID: 936
		private DisableAbility hex;

		// Token: 0x040003A9 RID: 937
		private BrambleMaze maze;

		// Token: 0x040003AA RID: 938
		private DisableAbility moveCrown;

		// Token: 0x040003AB RID: 939
		private ShieldAbility moveRealm;

		// Token: 0x040003AC RID: 940
		private DisableAbility nullifier;

		// Token: 0x040003AD RID: 941
		private DisableAbility orchid;

		// Token: 0x040003AE RID: 942
		private ShadowRealm realm;

		// Token: 0x040003AF RID: 943
		private DisableAbility terror;

		// Token: 0x040003B0 RID: 944
		private DebuffAbility urn;

		// Token: 0x040003B1 RID: 945
		private DebuffAbility veil;

		// Token: 0x040003B2 RID: 946
		private DebuffAbility vessel;
	}
}
