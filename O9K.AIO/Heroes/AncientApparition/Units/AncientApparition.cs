using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Heroes.AncientApparition.Abilities;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.AncientApparition.Units
{
	// Token: 0x020001EE RID: 494
	[UnitName("npc_dota_hero_ancient_apparition")]
	internal class AncientApparition : ControllableUnit
	{
		// Token: 0x060009C8 RID: 2504 RVA: 0x0002A908 File Offset: 0x00028B08
		public AncientApparition(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.ancient_apparition_cold_feet,
					(ActiveAbility x) => this.coldFeet = new DebuffAbility(x)
				},
				{
					AbilityId.ancient_apparition_ice_vortex,
					(ActiveAbility x) => this.vortex = new DebuffAbility(x)
				},
				{
					AbilityId.ancient_apparition_chilling_touch,
					(ActiveAbility x) => this.touch = new ChillingTouch(x)
				},
				{
					AbilityId.ancient_apparition_ice_blast,
					(ActiveAbility x) => this.blast = new IceBlast(x)
				},
				{
					AbilityId.item_cyclone,
					(ActiveAbility x) => this.eul = new DisableAbility(x)
				},
				{
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				},
				{
					AbilityId.item_rod_of_atos,
					(ActiveAbility x) => this.atos = new DisableAbility(x)
				},
				{
					AbilityId.item_spirit_vessel,
					(ActiveAbility x) => this.vessel = new DebuffAbility(x)
				},
				{
					AbilityId.item_urn_of_shadows,
					(ActiveAbility x) => this.urn = new DebuffAbility(x)
				}
			};
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002A9F4 File Offset: 0x00028BF4
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			IceBlast iceBlast = this.blast;
			if (iceBlast != null && iceBlast.Release(targetManager, base.ComboSleeper))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.hex, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.coldFeet, true))
			{
				base.ComboSleeper.ExtendSleep(0.3f);
				return true;
			}
			if (abilityHelper.UseAbility(this.vortex, true))
			{
				return true;
			}
			Modifier modifier = targetManager.Target.GetModifier("modifier_cold_feet");
			if (modifier != null && modifier.ElapsedTime < (float)1 && abilityHelper.UseAbility(this.eul, true))
			{
				base.ComboSleeper.ExtendSleep(0.5f);
				return true;
			}
			if (abilityHelper.UseAbility(this.touch, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.atos, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.vessel, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.urn, true))
			{
				return true;
			}
			abilityHelper.UseAbility(this.blast, true);
			return true;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00003880 File Offset: 0x00001A80
		protected override bool UseOrbAbility(Unit9 target, ComboModeMenu comboMenu)
		{
			return false;
		}

		// Token: 0x04000527 RID: 1319
		private DisableAbility atos;

		// Token: 0x04000528 RID: 1320
		private IceBlast blast;

		// Token: 0x04000529 RID: 1321
		private DebuffAbility coldFeet;

		// Token: 0x0400052A RID: 1322
		private DisableAbility eul;

		// Token: 0x0400052B RID: 1323
		private DisableAbility hex;

		// Token: 0x0400052C RID: 1324
		private TargetableAbility touch;

		// Token: 0x0400052D RID: 1325
		private DebuffAbility urn;

		// Token: 0x0400052E RID: 1326
		private DebuffAbility vessel;

		// Token: 0x0400052F RID: 1327
		private DebuffAbility vortex;
	}
}
