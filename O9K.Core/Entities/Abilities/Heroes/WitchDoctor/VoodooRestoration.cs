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
	// Token: 0x020001C0 RID: 448
	[AbilityId(AbilityId.witch_doctor_voodoo_restoration)]
	public class VoodooRestoration : ToggleAbility, IHealthRestore, IHasRadius, IActiveAbility
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x0000827D File Offset: 0x0000647D
		public VoodooRestoration(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x000082B0 File Offset: 0x000064B0
		public bool InstantHealthRestore { get; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x000082B8 File Offset: 0x000064B8
		public string HealModifierName { get; } = "modifier_voodoo_restoration_heal";

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x000082C0 File Offset: 0x000064C0
		public bool RestoresAlly { get; } = 1;

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x000082C8 File Offset: 0x000064C8
		public bool RestoresOwner { get; } = 1;

		// Token: 0x06000905 RID: 2309 RVA: 0x0000372C File Offset: 0x0000192C
		public int HealthRestoreValue(Unit9 unit)
		{
			return 0;
		}
	}
}
