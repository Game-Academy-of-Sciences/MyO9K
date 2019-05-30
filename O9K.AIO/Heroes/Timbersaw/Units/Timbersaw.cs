using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Timbersaw.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Timbersaw.Units
{
	// Token: 0x02000078 RID: 120
	[UnitName("npc_dota_hero_shredder")]
	internal class Timbersaw : ControllableUnit
	{
		// Token: 0x06000273 RID: 627 RVA: 0x000103EC File Offset: 0x0000E5EC
		public Timbersaw(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.shredder_chakram,
					delegate(ActiveAbility x)
					{
						Chakram chakram = new Chakram(x);
						this.chakrams.Add(chakram);
						return chakram;
					}
				},
				{
					AbilityId.shredder_chakram_2,
					delegate(ActiveAbility x)
					{
						Chakram chakram = new Chakram(x);
						this.chakrams.Add(chakram);
						return chakram;
					}
				},
				{
					AbilityId.shredder_whirling_death,
					(ActiveAbility x) => this.whirlingDeath = new NukeAbility(x)
				},
				{
					AbilityId.shredder_timber_chain,
					(ActiveAbility x) => this.timberChain = new TimberChain(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkDaggerTimbersaw(x)
				},
				{
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				},
				{
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				},
				{
					AbilityId.item_blade_mail,
					(ActiveAbility x) => this.bladeMail = new ShieldAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.shredder_timber_chain, (ActiveAbility x) => this.timberChainBlink = new TimberChainBlink(x));
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000104E4 File Offset: 0x0000E6E4
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			int damagingChakrams = this.chakrams.Count((Chakram x) => comboModeMenu.IsAbilityEnabled(x.Ability) && x.IsDamaging(targetManager));
			Chakram chakram = this.chakrams.Find((Chakram x) => comboModeMenu.IsAbilityEnabled(x.Ability) && x.ShouldReturnChakram(targetManager, damagingChakrams));
			if (chakram != null && chakram.Return())
			{
				return true;
			}
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.whirlingDeath, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.hex, true))
			{
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
			{
				this.timberChain,
				this.whirlingDeath
			}))
			{
				if (abilityHelper.CanBeCasted(this.whirlingDeath, false, false, true, true))
				{
					abilityHelper.ForceUseAbility(this.whirlingDeath, true, true);
				}
				return true;
			}
			if (abilityHelper.UseAbility(this.shiva, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.bladeMail, 400f))
			{
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.timberChain, new UsableAbility[]
			{
				this.blink
			}))
			{
				return true;
			}
			Chakram ability = this.chakrams.Find((Chakram x) => x.Ability.CanBeCasted(true));
			return abilityHelper.UseAbility(ability, true);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00010644 File Offset: 0x0000E844
		public override void EndCombo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			base.EndCombo(targetManager, comboModeMenu);
			IEnumerable<Chakram> source = this.chakrams;
			Func<Chakram, bool> <>9__0;
			Func<Chakram, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((Chakram x) => x.ReturnChakram.CanBeCasted(true) && comboModeMenu.IsAbilityEnabled(x.Ability)));
			}
			foreach (Chakram chakram in source.Where(predicate))
			{
				chakram.Return();
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000038CE File Offset: 0x00001ACE
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBlinks(abilityHelper) || abilityHelper.UseMoveAbility(this.timberChainBlink);
		}

		// Token: 0x0400014C RID: 332
		private readonly List<Chakram> chakrams = new List<Chakram>();

		// Token: 0x0400014D RID: 333
		private ShieldAbility bladeMail;

		// Token: 0x0400014E RID: 334
		private BlinkDaggerTimbersaw blink;

		// Token: 0x0400014F RID: 335
		private DisableAbility hex;

		// Token: 0x04000150 RID: 336
		private DebuffAbility shiva;

		// Token: 0x04000151 RID: 337
		private TimberChain timberChain;

		// Token: 0x04000152 RID: 338
		private TimberChainBlink timberChainBlink;

		// Token: 0x04000153 RID: 339
		private NukeAbility whirlingDeath;
	}
}
