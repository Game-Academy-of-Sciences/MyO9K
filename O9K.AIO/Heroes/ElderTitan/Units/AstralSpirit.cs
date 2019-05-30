using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.ElderTitan.Units
{
	// Token: 0x0200013D RID: 317
	[UnitName("npc_dota_elder_titan_ancestral_spirit")]
	internal class AstralSpirit : ControllableUnit
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x0001E9C0 File Offset: 0x0001CBC0
		public AstralSpirit(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.elder_titan_echo_stomp_spirit,
					(ActiveAbility x) => this.stomp = new DisableAbility(x)
				}
			};
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00005403 File Offset: 0x00003603
		public override bool CanMove()
		{
			return !base.MoveSleeper.IsSleeping;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001EA10 File Offset: 0x0001CC10
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			foreach (Unit9 unit in targetManager.EnemyUnits)
			{
				if (base.Owner.Distance(unit) < 275f)
				{
					if (!this.damagedUnits.Contains(unit))
					{
						this.damagedUnits.Add(unit);
					}
					if (this.moveUnits.Contains(unit))
					{
						this.moveUnits.Remove(unit);
					}
				}
				else if (!this.damagedUnits.Contains(unit) && !this.moveUnits.Contains(unit))
				{
					this.moveUnits.Add(unit);
				}
			}
			return abilityHelper.UseAbility(this.stomp, true);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00005413 File Offset: 0x00003613
		public override void EndCombo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			base.EndCombo(targetManager, comboModeMenu);
			this.damagedUnits.Clear();
			this.moveUnits.Clear();
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001EAEC File Offset: 0x0001CCEC
		public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			if (target != null)
			{
				if (comboMenu != null && comboMenu.IsAbilityEnabled(this.stomp.Ability) && this.stomp.Ability.CanBeCasted(true))
				{
					return base.Move(target.GetPredictedPosition(1f));
				}
				if (!this.damagedUnits.Contains(target))
				{
					return base.Move(target.Position);
				}
				Unit9 unit = (from x in this.moveUnits
				where x.IsValid && x.IsAlive && x.Distance(base.Owner) < 1000f
				orderby x.IsHero descending, x.Distance(base.Owner)
				select x).FirstOrDefault<Unit9>();
				if (unit != null)
				{
					return base.Move(unit.Position);
				}
				ActiveAbility activeAbility = base.Owner.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.elder_titan_return_spirit) as ActiveAbility;
				if (activeAbility != null && activeAbility.CanBeCasted(true) && activeAbility.UseAbility(false, false))
				{
					base.OrbwalkSleeper.Sleep(3f);
					return true;
				}
			}
			return base.Orbwalk(target, attack, move, comboMenu);
		}

		// Token: 0x04000372 RID: 882
		private readonly List<Unit9> damagedUnits = new List<Unit9>();

		// Token: 0x04000373 RID: 883
		private readonly List<Unit9> moveUnits = new List<Unit9>();

		// Token: 0x04000374 RID: 884
		private DisableAbility stomp;
	}
}
