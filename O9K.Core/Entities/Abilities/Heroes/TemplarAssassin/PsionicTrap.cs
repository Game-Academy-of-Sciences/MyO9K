using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.TemplarAssassin
{
	// Token: 0x020002B1 RID: 689
	[AbilityId(AbilityId.templar_assassin_psionic_trap)]
	public class PsionicTrap : CircleAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000C27 RID: 3111 RVA: 0x0000AED7 File Offset: 0x000090D7
		public PsionicTrap(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "trap_bonus_damage");
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0000AF07 File Offset: 0x00009107
		public string DebuffModifierName { get; } = "modifier_templar_assassin_trap_slow";

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0000AF0F File Offset: 0x0000910F
		public override float Radius { get; } = 400f;
	}
}
