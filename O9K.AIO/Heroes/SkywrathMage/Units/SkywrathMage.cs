using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.SkywrathMage.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.SkywrathMage.Units
{
	// Token: 0x020000A9 RID: 169
	[UnitName("npc_dota_hero_skywrath_mage")]
	internal class SkywrathMage : ControllableUnit, IDisposable
	{
		// Token: 0x0600035E RID: 862 RVA: 0x0001374C File Offset: 0x0001194C
		public SkywrathMage(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.skywrath_mage_arcane_bolt,
					(ActiveAbility x) => this.bolt = new NukeAbility(x)
				},
				{
					AbilityId.skywrath_mage_ancient_seal,
					(ActiveAbility x) => this.seal = new DebuffAbility(x)
				},
				{
					AbilityId.skywrath_mage_concussive_shot,
					(ActiveAbility x) => this.concussive = new DebuffAbility(x)
				},
				{
					AbilityId.skywrath_mage_mystic_flare,
					(ActiveAbility x) => this.flare = new MysticFlare(x)
				},
				{
					AbilityId.item_rod_of_atos,
					(ActiveAbility x) => this.atos = new DisableAbility(x)
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
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.hex = new HexSkywrath(x)
				},
				{
					AbilityId.item_ethereal_blade,
					(ActiveAbility x) => this.ethereal = new EtherealBlade(x)
				},
				{
					AbilityId.item_nullifier,
					(ActiveAbility x) => this.nullifier = new Nullifier(x)
				},
				{
					AbilityId.item_dagon_5,
					(ActiveAbility x) => this.dagon = new NukeAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.skywrath_mage_ancient_seal, (ActiveAbility _) => this.seal);
			base.MoveComboAbilities.Add(AbilityId.skywrath_mage_concussive_shot, (ActiveAbility _) => this.concussive);
			Player.OnExecuteOrder += this.OnExecuteOrder;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000138C4 File Offset: 0x00011AC4
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.atos, true) || abilityHelper.UseAbility(this.hex, true) || abilityHelper.UseAbility(this.nullifier, true) || abilityHelper.UseAbility(this.seal, true) || abilityHelper.UseAbility(this.ethereal, true) || abilityHelper.UseAbility(this.dagon, true) || abilityHelper.UseAbility(this.concussive, true) || abilityHelper.UseAbility(this.veil, true) || abilityHelper.UseAbility(this.blink, 850f, 600f) || abilityHelper.UseAbility(this.force, 850f, 600f) || abilityHelper.UseAbilityIfNone(this.flare, new UsableAbility[]
			{
				this.atos
			}) || abilityHelper.UseAbility(this.bolt, true);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00004042 File Offset: 0x00002242
		public void Dispose()
		{
			Player.OnExecuteOrder -= this.OnExecuteOrder;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00004055 File Offset: 0x00002255
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseAbility(this.seal, true) || abilityHelper.UseAbility(this.concussive, true);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000139C8 File Offset: 0x00011BC8
		private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
		{
			if (!args.Process || args.IsQueued || !args.IsPlayerInput || args.OrderId != OrderId.Ability)
			{
				return;
			}
			uint num = args.Ability.Handle;
			DebuffAbility debuffAbility = this.concussive;
			if (num != ((debuffAbility != null) ? new uint?(debuffAbility.Ability.Handle) : null))
			{
				return;
			}
			foreach (Unit9 unit in EntityManager9.EnemyHeroes)
			{
				if (this.concussive.Ability.CanHit(unit))
				{
					return;
				}
			}
			args.Process = false;
		}

		// Token: 0x040001DD RID: 477
		private DisableAbility atos;

		// Token: 0x040001DE RID: 478
		private BlinkAbility blink;

		// Token: 0x040001DF RID: 479
		private NukeAbility bolt;

		// Token: 0x040001E0 RID: 480
		private NukeAbility dagon;

		// Token: 0x040001E1 RID: 481
		private DebuffAbility concussive;

		// Token: 0x040001E2 RID: 482
		private EtherealBlade ethereal;

		// Token: 0x040001E3 RID: 483
		private MysticFlare flare;

		// Token: 0x040001E4 RID: 484
		private ForceStaff force;

		// Token: 0x040001E5 RID: 485
		private HexSkywrath hex;

		// Token: 0x040001E6 RID: 486
		private Nullifier nullifier;

		// Token: 0x040001E7 RID: 487
		private DebuffAbility seal;

		// Token: 0x040001E8 RID: 488
		private DebuffAbility veil;
	}
}
