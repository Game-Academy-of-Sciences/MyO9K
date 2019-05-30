using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Omniknight
{
	// Token: 0x0200030E RID: 782
	[AbilityId(AbilityId.omniknight_guardian_angel)]
	public class GuardianAngel : AreaOfEffectAbility, IShield, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06000D84 RID: 3460 RVA: 0x00026D9C File Offset: 0x00024F9C
		public GuardianAngel(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0000BFC9 File Offset: 0x0000A1C9
		public UnitState AppliesUnitState { get; } = 4L;

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0000BFD1 File Offset: 0x0000A1D1
		public override bool TargetsEnemy { get; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0000BFD9 File Offset: 0x0000A1D9
		public DamageType AmplifierDamageType { get; } = 1;

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0000BFE1 File Offset: 0x0000A1E1
		public string AmplifierModifierName { get; } = "modifier_omninight_guardian_angel";

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0000BFE9 File Offset: 0x0000A1E9
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0000BFF1 File Offset: 0x0000A1F1
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x0000BFF9 File Offset: 0x0000A1F9
		public bool IsAmplifierPermanent { get; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0000C001 File Offset: 0x0000A201
		public string ShieldModifierName { get; } = "modifier_omninight_guardian_angel";

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x0000C009 File Offset: 0x0000A209
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0000C011 File Offset: 0x0000A211
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x06000D8F RID: 3471 RVA: 0x00006D18 File Offset: 0x00004F18
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return -1f;
		}
	}
}
