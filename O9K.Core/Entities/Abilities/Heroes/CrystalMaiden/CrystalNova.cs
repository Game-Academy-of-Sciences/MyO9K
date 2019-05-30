using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.CrystalMaiden
{
	// Token: 0x0200039E RID: 926
	[AbilityId(AbilityId.crystal_maiden_crystal_nova)]
	public class CrystalNova : CircleAbility, IDebuff, INuke, IActiveAbility
	{
		// Token: 0x06000FBB RID: 4027 RVA: 0x0000DE2F File Offset: 0x0000C02F
		public CrystalNova(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "nova_damage");
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x0000DE65 File Offset: 0x0000C065
		public string DebuffModifierName { get; } = "modifier_crystal_maiden_crystal_nova";
	}
}
