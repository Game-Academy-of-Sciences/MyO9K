using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Pugna
{
	// Token: 0x020001EE RID: 494
	[AbilityId(AbilityId.pugna_nether_ward)]
	public class NetherWard : CircleAbility, IDebuff, IActiveAbility
	{
		// Token: 0x060009B6 RID: 2486 RVA: 0x00008C09 File Offset: 0x00006E09
		public NetherWard(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.manaMultiplierData = new SpecialData(baseAbility, "mana_multiplier");
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00008C3F File Offset: 0x00006E3F
		public string DebuffModifierName { get; } = "modifier_pugna_nether_ward_aura";

		// Token: 0x060009B8 RID: 2488 RVA: 0x000236AC File Offset: 0x000218AC
		public int GetDamage(Unit9 unit, float manaCost)
		{
			float num = manaCost * this.manaMultiplierData.GetValue(this.Level);
			float damageAmplification = unit.GetDamageAmplification(base.Owner, this.DamageType, false);
			float damageBlock = unit.GetDamageBlock(this.DamageType);
			return (int)((num - damageBlock) * damageAmplification);
		}

		// Token: 0x040004E7 RID: 1255
		private readonly SpecialData manaMultiplierData;
	}
}
