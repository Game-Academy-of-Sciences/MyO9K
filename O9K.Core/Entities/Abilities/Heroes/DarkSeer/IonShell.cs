using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.DarkSeer
{
	// Token: 0x0200024B RID: 587
	[AbilityId(AbilityId.dark_seer_ion_shell)]
	public class IonShell : RangedAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000AC1 RID: 2753 RVA: 0x00009B20 File Offset: 0x00007D20
		public IonShell(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00009B53 File Offset: 0x00007D53
		public string BuffModifierName { get; } = "modifier_dark_seer_ion_shell";

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00009B5B File Offset: 0x00007D5B
		public bool BuffsAlly { get; } = 1;

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00009B63 File Offset: 0x00007D63
		public bool BuffsOwner { get; } = 1;
	}
}
