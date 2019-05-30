using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.DarkWillow.Abilities
{
	// Token: 0x02000157 RID: 343
	internal class EulsScepterOfDivinityDarkWillow : EulsScepterOfDivinity
	{
		// Token: 0x060006C1 RID: 1729 RVA: 0x00005650 File Offset: 0x00003850
		public EulsScepterOfDivinityDarkWillow(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000209F0 File Offset: 0x0001EBF0
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			if (usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.dark_willow_cursed_crown) != null)
			{
				return true;
			}
			Modifier modifier = targetManager.Target.GetModifier("modifier_dark_willow_cursed_crown");
			return !(modifier == null) && modifier.RemainingTime >= base.Ability.Duration + 0.1f;
		}
	}
}
