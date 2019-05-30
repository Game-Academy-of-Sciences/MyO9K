using System;
using Ensage;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Base.Components
{
	// Token: 0x020003FA RID: 1018
	public interface IHasDamageAmplify
	{
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001128 RID: 4392
		DamageType AmplifierDamageType { get; }

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001129 RID: 4393
		string AmplifierModifierName { get; }

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x0600112A RID: 4394
		AmplifiesDamage AmplifiesDamage { get; }

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600112B RID: 4395
		bool IsAmplifierAddedToStats { get; }

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600112C RID: 4396
		bool IsAmplifierPermanent { get; }

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600112D RID: 4397
		bool IsValid { get; }

		// Token: 0x0600112E RID: 4398
		float AmplifierValue(Unit9 source, Unit9 target);
	}
}
