using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.NightStalker
{
	// Token: 0x020001F7 RID: 503
	[AbilityId(AbilityId.night_stalker_void)]
	public class Void : RangedAbility, IDebuff, IDisable, INuke, IActiveAbility
	{
		// Token: 0x060009D1 RID: 2513 RVA: 0x00008DAF File Offset: 0x00006FAF
		public Void(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00008DC3 File Offset: 0x00006FC3
		public UnitState AppliesUnitState
		{
			get
			{
				if (Game.IsNight)
				{
					return UnitState.Stunned;
				}
				return (UnitState)0UL;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00008DD2 File Offset: 0x00006FD2
		public string DebuffModifierName { get; } = "modifier_night_stalker_void";

		// Token: 0x060009D4 RID: 2516 RVA: 0x00023744 File Offset: 0x00021944
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = base.GetRawDamage(unit, remainingHealth);
			if (!Game.IsNight)
			{
				damage *= 0.5f;
			}
			return damage;
		}
	}
}
