using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.VengefulSpirit
{
	// Token: 0x02000284 RID: 644
	[AbilityId(AbilityId.vengefulspirit_magic_missile)]
	public class MagicMissile : RangedAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000B89 RID: 2953 RVA: 0x0000A62B File Offset: 0x0000882B
		public MagicMissile(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "magic_missile_speed");
			this.DamageData = new SpecialData(baseAbility, "magic_missile_damage");
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0000A65F File Offset: 0x0000885F
		public override bool CanHitSpellImmuneEnemy
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_vengeful_spirit_3);
				return abilityById != null && abilityById.Level > 0u;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0000A685 File Offset: 0x00008885
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
