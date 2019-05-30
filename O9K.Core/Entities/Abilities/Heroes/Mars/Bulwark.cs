using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Mars
{
	// Token: 0x02000335 RID: 821
	[AbilityId(AbilityId.mars_bulwark)]
	public class Bulwark : PassiveAbility, IHasDamageAmplify
	{
		// Token: 0x06000E11 RID: 3601 RVA: 0x0002720C File Offset: 0x0002540C
		public Bulwark(Ability baseAbility) : base(baseAbility)
		{
			this.sideAmplifierData = new SpecialData(baseAbility, "physical_damage_reduction_side");
			this.frontAmplifierData = new SpecialData(baseAbility, "physical_damage_reduction");
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0000C75E File Offset: 0x0000A95E
		public DamageType AmplifierDamageType { get; } = 1;

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0000C766 File Offset: 0x0000A966
		public string AmplifierModifierName { get; } = "modifier_mars_bulwark";

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0000C76E File Offset: 0x0000A96E
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0000C776 File Offset: 0x0000A976
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0000C77E File Offset: 0x0000A97E
		public bool IsAmplifierPermanent { get; } = 1;

		// Token: 0x06000E17 RID: 3607 RVA: 0x00027264 File Offset: 0x00025464
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			if (!this.CanBeCasted(true))
			{
				return 0f;
			}
			float angle = target.GetAngle(source.Position, false);
			if ((double)angle <= 1.1)
			{
				return this.frontAmplifierData.GetValue(this.Level) / -100f;
			}
			if ((double)angle <= 1.9)
			{
				return this.sideAmplifierData.GetValue(this.Level) / -100f;
			}
			return 0f;
		}

		// Token: 0x04000755 RID: 1877
		private readonly SpecialData frontAmplifierData;

		// Token: 0x04000756 RID: 1878
		private readonly SpecialData sideAmplifierData;
	}
}
