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

namespace O9K.AIO.Heroes.ChaosKnight.Units
{
	// Token: 0x020001D4 RID: 468
	[UnitName("npc_dota_hero_chaos_knight")]
	internal class ChaosKnight : ControllableUnit
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x000291C0 File Offset: 0x000273C0
		public ChaosKnight(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.chaos_knight_chaos_bolt,
					(ActiveAbility x) => this.bolt = new DisableAbility(x)
				},
				{
					AbilityId.chaos_knight_reality_rift,
					(ActiveAbility x) => this.rift = new TargetableAbility(x)
				},
				{
					AbilityId.chaos_knight_phantasm,
					(ActiveAbility x) => this.phantasm = new UntargetableAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_black_king_bar,
					(ActiveAbility x) => this.bkb = new ShieldAbility(x)
				},
				{
					AbilityId.item_manta,
					(ActiveAbility x) => this.manta = new BuffAbility(x)
				},
				{
					AbilityId.item_armlet,
					(ActiveAbility x) => this.armlet = new BuffAbility(x)
				},
				{
					AbilityId.item_heavens_halberd,
					(ActiveAbility x) => this.halberd = new DisableAbility(x)
				},
				{
					AbilityId.item_orchid,
					(ActiveAbility x) => this.orchid = new DisableAbility(x)
				},
				{
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.bloodthorn = new Bloodthorn(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.slardar_sprint, (ActiveAbility _) => this.bolt);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000292DC File Offset: 0x000274DC
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.bloodthorn, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.orchid, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.armlet, 600f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.bkb, 600f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.bolt, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.rift, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.blink, 500f, 0f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.halberd, true))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.phantasm, true, true, true, true))
			{
				if (abilityHelper.UseAbility(this.armlet, true))
				{
					base.ComboSleeper.ExtendSleep(0.5f);
					return true;
				}
				if (abilityHelper.UseAbility(this.phantasm, true))
				{
					return true;
				}
			}
			return abilityHelper.UseAbility(this.manta, 600f);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00006AEF File Offset: 0x00004CEF
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseAbility(this.bolt, true);
		}

		// Token: 0x040004EC RID: 1260
		private BuffAbility armlet;

		// Token: 0x040004ED RID: 1261
		private DisableAbility bloodthorn;

		// Token: 0x040004EE RID: 1262
		private DisableAbility orchid;

		// Token: 0x040004EF RID: 1263
		private ShieldAbility bkb;

		// Token: 0x040004F0 RID: 1264
		private BlinkAbility blink;

		// Token: 0x040004F1 RID: 1265
		private DisableAbility bolt;

		// Token: 0x040004F2 RID: 1266
		private DisableAbility halberd;

		// Token: 0x040004F3 RID: 1267
		private BuffAbility manta;

		// Token: 0x040004F4 RID: 1268
		private UntargetableAbility phantasm;

		// Token: 0x040004F5 RID: 1269
		private TargetableAbility rift;
	}
}
