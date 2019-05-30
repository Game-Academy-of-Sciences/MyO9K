using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Tiny.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.Tiny.Units
{
	// Token: 0x0200006D RID: 109
	[UnitName("npc_dota_hero_tiny")]
	internal class Tiny : ControllableUnit
	{
		// Token: 0x06000243 RID: 579 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
		public Tiny(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.tiny_avalanche,
					(ActiveAbility x) => this.avalanche = new DisableAbility(x)
				},
				{
					AbilityId.tiny_toss,
					(ActiveAbility x) => this.toss = new Toss(x)
				},
				{
					AbilityId.tiny_craggy_exterior,
					(ActiveAbility x) => this.treeGrab = new TreeGrab(x)
				},
				{
					AbilityId.tiny_toss_tree,
					(ActiveAbility x) => this.treeThrow = new TreeThrow(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.leshrac_split_earth, (ActiveAbility _) => this.avalanche);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000FC64 File Offset: 0x0000DE64
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.blink, 400f, 0f) || abilityHelper.UseAbility(this.avalanche, true) || abilityHelper.UseAbility(this.toss, true) || abilityHelper.UseAbility(this.treeGrab, true) || abilityHelper.UseAbility(this.treeThrow, true) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000FCEC File Offset: 0x0000DEEC
		public void Toss()
		{
			ActiveAbility tossAbility = base.Owner.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.tiny_toss) as ActiveAbility;
			ActiveAbility tossAbility2 = tossAbility;
			if (tossAbility2 == null || !tossAbility2.CanBeCasted(true))
			{
				return;
			}
			Unit9 tower = (from x in EntityManager9.Units
			where x.IsTower && x.IsAlly(this.Owner) && x.IsAlive
			orderby x.Distance(this.Owner)
			select x).FirstOrDefault((Unit9 x) => x.Distance(this.Owner) < 2000f);
			if (tower == null)
			{
				return;
			}
			Unit9 unit = (from x in EntityManager9.Units
			where x.IsUnit && !x.IsInvulnerable && !x.IsMagicImmune && x.IsAlive && x.IsVisible && x.Distance(this.Owner) < tossAbility.CastRange && x.Distance(tower) < tower.GetAttackRange(null, 0f)
			orderby x.Distance(tower)
			select x).FirstOrDefault<Unit9>();
			if (unit == null)
			{
				return;
			}
			Unit9 unit2 = (from x in EntityManager9.Units
			where x.IsUnit && !x.Equals(this.Owner) && !x.IsInvulnerable && !x.IsMagicImmune && x.IsAlive && x.IsVisible && x.Distance(this.Owner) < tossAbility.Radius
			orderby x.Distance(this.Owner)
			select x).FirstOrDefault<Unit9>();
			if (unit2 == null || !unit2.IsHero || unit2.IsIllusion || unit2.IsAlly(base.Owner))
			{
				return;
			}
			tossAbility.UseAbility(unit, false, false);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00003735 File Offset: 0x00001935
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseMoveAbility(this.avalanche);
		}

		// Token: 0x04000138 RID: 312
		private DisableAbility avalanche;

		// Token: 0x04000139 RID: 313
		private BlinkAbility blink;

		// Token: 0x0400013A RID: 314
		private SpeedBuffAbility phase;

		// Token: 0x0400013B RID: 315
		private NukeAbility toss;

		// Token: 0x0400013C RID: 316
		private TreeGrab treeGrab;

		// Token: 0x0400013D RID: 317
		private TreeThrow treeThrow;
	}
}
