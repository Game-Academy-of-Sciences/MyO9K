using System;
using O9K.AIO.Heroes.Base;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Specials
{
	// Token: 0x020001A0 RID: 416
	internal class SpecialAbilityGroup : OldAbilityGroup<IActiveAbility, OldSpecialAbility>
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x00006345 File Offset: 0x00004545
		public SpecialAbilityGroup(BaseHero baseHero) : base(baseHero)
		{
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000263F0 File Offset: 0x000245F0
		public override bool AddAbility(Ability9 ability)
		{
			IActiveAbility activeAbility;
			if ((activeAbility = (ability as IActiveAbility)) == null)
			{
				return false;
			}
			Type type;
			if (!base.UniqueAbilities.TryGetValue(ability.Id, out type))
			{
				return false;
			}
			OldSpecialAbility oldSpecialAbility = (OldSpecialAbility)Activator.CreateInstance(type, new object[]
			{
				activeAbility
			});
			oldSpecialAbility.AbilitySleeper = base.AbilitySleeper;
			oldSpecialAbility.OrbwalkSleeper = base.OrbwalkSleeper;
			base.Abilities.Add(oldSpecialAbility);
			this.OrderAbilities();
			return true;
		}
	}
}
