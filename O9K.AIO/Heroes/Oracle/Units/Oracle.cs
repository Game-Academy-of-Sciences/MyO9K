using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Oracle.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Oracle.Units
{
	// Token: 0x0200004A RID: 74
	[UnitName("npc_dota_hero_oracle")]
	internal class Oracle : ControllableUnit
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		public Oracle(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.oracle_fortunes_end,
					(ActiveAbility x) => this.end = new FortunesEnd(x)
				},
				{
					AbilityId.oracle_fates_edict,
					(ActiveAbility x) => this.edict = new ShieldAbility(x)
				},
				{
					AbilityId.oracle_purifying_flames,
					(ActiveAbility x) => this.flames = new PurifyingFlames(x)
				},
				{
					AbilityId.oracle_false_promise,
					(ActiveAbility x) => this.promise = new ShieldAbility(x)
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
					AbilityId.item_dagon_5,
					(ActiveAbility x) => this.dagon = new NukeAbility(x)
				}
			};
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000DB78 File Offset: 0x0000BD78
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.dagon, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.end, true))
			{
				return true;
			}
			if (this.end.Ability.IsChanneling && targetManager.Target.HasModifier("modifier_oracle_purifying_flames") && base.Owner.BaseUnit.Stop())
			{
				base.ComboSleeper.Sleep(0.1f);
				return true;
			}
			return (abilityHelper.CanBeCasted(this.flames, true, true, false, true) && this.end.Ability.IsChanneling && !this.end.FullChannelTime(comboModeMenu) && abilityHelper.ForceUseAbility(this.flames, false, true)) || abilityHelper.UseAbility(this.flames, true) || abilityHelper.UseAbility(this.urn, true) || abilityHelper.UseAbility(this.vessel, true);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000DC70 File Offset: 0x0000BE70
		public void HealAllyCombo(TargetManager targetManager)
		{
			if (base.ComboSleeper.IsSleeping)
			{
				return;
			}
			if (!this.flames.Ability.CanBeCasted(true))
			{
				return;
			}
			Unit9 target = targetManager.Target;
			if (target.HasModifier(new string[]
			{
				this.edict.Shield.ShieldModifierName,
				this.promise.Shield.ShieldModifierName
			}))
			{
				this.flames.Ability.UseAbility(target, false, false);
				base.ComboSleeper.Sleep(this.flames.Ability.GetCastDelay(target));
				return;
			}
			if (this.edict.Ability.CanBeCasted(true))
			{
				this.edict.Ability.UseAbility(target, false, false);
				base.ComboSleeper.Sleep(this.edict.Ability.GetCastDelay(target));
			}
		}

		// Token: 0x040000E9 RID: 233
		private ShieldAbility edict;

		// Token: 0x040000EA RID: 234
		private FortunesEnd end;

		// Token: 0x040000EB RID: 235
		private PurifyingFlames flames;

		// Token: 0x040000EC RID: 236
		private ShieldAbility promise;

		// Token: 0x040000ED RID: 237
		private DebuffAbility urn;

		// Token: 0x040000EE RID: 238
		private NukeAbility dagon;

		// Token: 0x040000EF RID: 239
		private DebuffAbility vessel;
	}
}
