using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.CentaurWarrunner
{
	// Token: 0x020003A5 RID: 933
	[AbilityId(AbilityId.centaur_stampede)]
	public class Stampede : ActiveAbility, IHasDamageAmplify
	{
		// Token: 0x06000FCB RID: 4043 RVA: 0x00028714 File Offset: 0x00026914
		public Stampede(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "damage_reduction");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0000DEFF File Offset: 0x0000C0FF
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x0000DF07 File Offset: 0x0000C107
		public string AmplifierModifierName { get; } = "modifier_centaur_stampede";

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x0000DF0F File Offset: 0x0000C10F
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0000DF17 File Offset: 0x0000C117
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x0000DF1F File Offset: 0x0000C11F
		public bool IsAmplifierPermanent { get; }

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0000DF27 File Offset: 0x0000C127
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			if (!base.Owner.HasAghanimsScepter)
			{
				return 0f;
			}
			return this.amplifierData.GetValue(this.Level) / -100f;
		}

		// Token: 0x04000832 RID: 2098
		private readonly SpecialData amplifierData;
	}
}
