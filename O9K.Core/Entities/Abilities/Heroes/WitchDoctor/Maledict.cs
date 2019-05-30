using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.WitchDoctor
{
	// Token: 0x020001BE RID: 446
	[AbilityId(AbilityId.witch_doctor_maledict)]
	public class Maledict : CircleAbility, IDebuff, IActiveAbility
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x00022C68 File Offset: 0x00020E68
		public Maledict(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.amplifierData = new SpecialData(baseAbility, "bonus_damage");
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00008209 File Offset: 0x00006409
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00008211 File Offset: 0x00006411
		public string AmplifierModifierName { get; } = "modifier_maledict";

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00008219 File Offset: 0x00006419
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00008221 File Offset: 0x00006421
		public string DebuffModifierName { get; } = "modifier_maledict";

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00008229 File Offset: 0x00006429
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00008231 File Offset: 0x00006431
		public bool IsAmplifierPermanent { get; }

		// Token: 0x060008FD RID: 2301 RVA: 0x00008239 File Offset: 0x00006439
		public float AmplifierValue(Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / 100f;
		}

		// Token: 0x04000471 RID: 1137
		private readonly SpecialData amplifierData;
	}
}
