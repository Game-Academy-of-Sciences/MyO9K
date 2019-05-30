using System;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.Ursa.Abilities
{
	// Token: 0x02000060 RID: 96
	internal class Enrage : ShieldAbility
	{
		// Token: 0x0600020B RID: 523 RVA: 0x000030D7 File Offset: 0x000012D7
		public Enrage(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000EE90 File Offset: 0x0000D090
		public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
		{
			if (!base.CanBeCasted(targetManager, channelingCheck, comboMenu))
			{
				return false;
			}
			if (!base.Owner.CanAttack(null, 0f))
			{
				return false;
			}
			EnrageMenu abilitySettingsMenu = comboMenu.GetAbilitySettingsMenu<EnrageMenu>(this);
			return abilitySettingsMenu.StacksCount > 0 && abilitySettingsMenu.StacksCount <= targetManager.Target.GetModifierStacks("modifier_ursa_fury_swipes_damage_increase");
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000035CA File Offset: 0x000017CA
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new EnrageMenu(base.Ability, simplifiedName);
		}
	}
}
