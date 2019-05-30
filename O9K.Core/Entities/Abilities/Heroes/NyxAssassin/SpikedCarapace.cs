using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.NyxAssassin
{
	// Token: 0x0200031A RID: 794
	[AbilityId(AbilityId.nyx_assassin_spiked_carapace)]
	public class SpikedCarapace : ActiveAbility, IShield, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06000DB4 RID: 3508 RVA: 0x0000C227 File Offset: 0x0000A427
		public SpikedCarapace(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x0000C25B File Offset: 0x0000A45B
		public UnitState AppliesUnitState { get; }

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0000C263 File Offset: 0x0000A463
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0000C26B File Offset: 0x0000A46B
		public string AmplifierModifierName { get; } = "modifier_nyx_assassin_spiked_carapace";

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0000C273 File Offset: 0x0000A473
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0000C27B File Offset: 0x0000A47B
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0000C283 File Offset: 0x0000A483
		public bool IsAmplifierPermanent { get; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0000C28B File Offset: 0x0000A48B
		public string ShieldModifierName { get; } = "modifier_nyx_assassin_spiked_carapace";

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x0000C293 File Offset: 0x0000A493
		public bool ShieldsAlly { get; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x0000C29B File Offset: 0x0000A49B
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x06000DBE RID: 3518 RVA: 0x00006D18 File Offset: 0x00004F18
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return -1f;
		}
	}
}
