using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.WinterWyvern
{
	// Token: 0x020001C4 RID: 452
	[AbilityId(AbilityId.winter_wyvern_winters_curse)]
	public class WintersCurse : RangedAreaOfEffectAbility, IDisable, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06000917 RID: 2327 RVA: 0x0000839A File Offset: 0x0000659A
		public WintersCurse(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x000083D6 File Offset: 0x000065D6
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x000083DE File Offset: 0x000065DE
		public string AmplifierModifierName { get; } = "modifier_winter_wyvern_winters_curse";

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x000083E6 File Offset: 0x000065E6
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x000083EE File Offset: 0x000065EE
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x000083F6 File Offset: 0x000065F6
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x000083FE File Offset: 0x000065FE
		public bool IsAmplifierPermanent { get; }

		// Token: 0x0600091E RID: 2334 RVA: 0x00006D18 File Offset: 0x00004F18
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return -1f;
		}
	}
}
