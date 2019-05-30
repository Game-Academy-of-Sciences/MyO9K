using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Abilities.Heroes.Riki
{
	// Token: 0x020002E5 RID: 741
	[AbilityId(AbilityId.riki_blink_strike)]
	public class BlinkStrike : RangedAbility, IBlink, INuke, IActiveAbility
	{
		// Token: 0x06000CE9 RID: 3305 RVA: 0x0000B9A0 File Offset: 0x00009BA0
		public BlinkStrike(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "bonus_damage");
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0000B9C1 File Offset: 0x00009BC1
		public BlinkType BlinkType { get; } = 2;

		// Token: 0x06000CEB RID: 3307 RVA: 0x00025EDC File Offset: 0x000240DC
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage rawDamage = base.GetRawDamage(unit, remainingHealth);
			Damage rawAttackDamage = base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
			Damage left = rawDamage + rawAttackDamage;
			CloakAndDagger cloakAndDagger = this.cloakAndDagger;
			return left + ((cloakAndDagger != null) ? cloakAndDagger.GetRawDamage(unit, null) : null);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00025F30 File Offset: 0x00024130
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.riki_permanent_invisibility)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				return;
			}
			this.cloakAndDagger = (CloakAndDagger)EntityManager9.AddAbility(ability);
		}

		// Token: 0x040006AE RID: 1710
		private CloakAndDagger cloakAndDagger;
	}
}
