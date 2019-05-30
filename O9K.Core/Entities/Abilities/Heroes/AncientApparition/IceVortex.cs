using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.AncientApparition
{
	// Token: 0x02000266 RID: 614
	[AbilityId(AbilityId.ancient_apparition_ice_vortex)]
	public class IceVortex : CircleAbility, IDebuff, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06000B25 RID: 2853 RVA: 0x00024C94 File Offset: 0x00022E94
		public IceVortex(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.amplifierData = new SpecialData(baseAbility, "spell_resist_pct");
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0000A13B File Offset: 0x0000833B
		public DamageType AmplifierDamageType { get; } = 2;

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0000A143 File Offset: 0x00008343
		public string AmplifierModifierName { get; } = "modifier_ice_vortex";

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0000A14B File Offset: 0x0000834B
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0000A153 File Offset: 0x00008353
		public string DebuffModifierName { get; } = "modifier_ice_vortex";

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0000A15B File Offset: 0x0000835B
		public bool IsAmplifierAddedToStats { get; } = 1;

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0000A163 File Offset: 0x00008363
		public bool IsAmplifierPermanent { get; }

		// Token: 0x06000B2C RID: 2860 RVA: 0x0000A16B File Offset: 0x0000836B
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / -100f;
		}

		// Token: 0x040005AC RID: 1452
		private readonly SpecialData amplifierData;
	}
}
