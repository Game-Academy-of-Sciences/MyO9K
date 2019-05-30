using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.NagaSiren
{
	// Token: 0x02000323 RID: 803
	[AbilityId(AbilityId.naga_siren_song_of_the_siren)]
	public class SongOfTheSiren : AreaOfEffectAbility, IShield, IActiveAbility
	{
		// Token: 0x06000DD4 RID: 3540 RVA: 0x0000C3B5 File Offset: 0x0000A5B5
		public SongOfTheSiren(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
		public UnitState AppliesUnitState { get; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		public string ShieldModifierName { get; } = "modifier_naga_siren_song_of_the_siren_aura";

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0000C400 File Offset: 0x0000A600
		public bool ShieldsOwner { get; } = 1;
	}
}
