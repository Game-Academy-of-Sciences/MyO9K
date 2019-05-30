using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Bristleback
{
	// Token: 0x020003AD RID: 941
	[AbilityId(AbilityId.bristleback_bristleback)]
	public class Bristleback : PassiveAbility, IHasDamageAmplify
	{
		// Token: 0x06000FDF RID: 4063 RVA: 0x000287BC File Offset: 0x000269BC
		public Bristleback(Ability baseAbility) : base(baseAbility)
		{
			this.sideAmplifierData = new SpecialData(baseAbility, "side_damage_reduction");
			this.backAmplifierData = new SpecialData(baseAbility, "back_damage_reduction");
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0000E018 File Offset: 0x0000C218
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0000E020 File Offset: 0x0000C220
		public string AmplifierModifierName { get; } = "modifier_bristleback_bristleback";

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x0000E028 File Offset: 0x0000C228
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0000E030 File Offset: 0x0000C230
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0000E038 File Offset: 0x0000C238
		public bool IsAmplifierPermanent { get; } = 1;

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00028814 File Offset: 0x00026A14
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			if (!this.CanBeCasted(true))
			{
				return 0f;
			}
			float angle = target.GetAngle(source.Position, false);
			if ((double)angle <= 1.1)
			{
				return 0f;
			}
			if ((double)angle <= 1.9)
			{
				return this.sideAmplifierData.GetValue(this.Level) / -100f;
			}
			return this.backAmplifierData.GetValue(this.Level) / -100f;
		}

		// Token: 0x0400083D RID: 2109
		private readonly SpecialData backAmplifierData;

		// Token: 0x0400083E RID: 2110
		private readonly SpecialData sideAmplifierData;
	}
}
