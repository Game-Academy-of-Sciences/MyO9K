using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.PhantomLancer
{
	// Token: 0x020002FF RID: 767
	[AbilityId(AbilityId.phantom_lancer_doppelwalk)]
	public class Doppelganger : CircleAbility, IShield, IActiveAbility
	{
		// Token: 0x06000D49 RID: 3401 RVA: 0x0000BD1E File Offset: 0x00009F1E
		public Doppelganger(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "target_aoe");
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0000BD56 File Offset: 0x00009F56
		public UnitState AppliesUnitState { get; } = 256L;

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x0000BD5E File Offset: 0x00009F5E
		public string ShieldModifierName { get; } = "modifier_phantomlancer_dopplewalk_phase";

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x0000BD66 File Offset: 0x00009F66
		public bool ShieldsAlly { get; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x0000BD6E File Offset: 0x00009F6E
		public bool ShieldsOwner { get; } = 1;
	}
}
