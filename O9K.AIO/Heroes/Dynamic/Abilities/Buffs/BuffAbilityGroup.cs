using System;
using System.Linq;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Dynamic.Abilities.Blinks;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Buffs
{
	// Token: 0x020001B2 RID: 434
	internal class BuffAbilityGroup : OldAbilityGroup<IBuff, OldBuffAbility>
	{
		// Token: 0x060008AE RID: 2222 RVA: 0x000065E8 File Offset: 0x000047E8
		public BuffAbilityGroup(BaseHero baseHero) : base(baseHero)
		{
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x000065F1 File Offset: 0x000047F1
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x000065F9 File Offset: 0x000047F9
		public BlinkAbilityGroup Blinks { get; set; }

		// Token: 0x060008B1 RID: 2225 RVA: 0x000272F0 File Offset: 0x000254F0
		public override bool Use(Unit9 target, ComboModeMenu menu, params AbilityId[] except)
		{
			foreach (OldBuffAbility oldBuffAbility in base.Abilities)
			{
				if (oldBuffAbility.Ability.IsValid && !except.Contains(oldBuffAbility.Ability.Id) && oldBuffAbility.CanBeCasted(oldBuffAbility.Buff.Owner, target, menu, this.Blinks) && oldBuffAbility.Use(oldBuffAbility.Buff.Owner))
				{
					return true;
				}
			}
			return false;
		}
	}
}
