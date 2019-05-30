using System;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Pudge
{
	// Token: 0x020002F0 RID: 752
	[AbilityId(AbilityId.pudge_dismember)]
	public class Dismember : RangedAbility, IDisable, IChanneled, IActiveAbility
	{
		// Token: 0x06000D10 RID: 3344 RVA: 0x0000BAD3 File Offset: 0x00009CD3
		public Dismember(Ability baseAbility) : base(baseAbility)
		{
			this.channelTime = baseAbility.GetChannelTime(0u);
			this.DamageData = new SpecialData(baseAbility, "dismember_damage");
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0000BB0A File Offset: 0x00009D0A
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0000720E File Offset: 0x0000540E
		public override float CastRange
		{
			get
			{
				return base.CastRange + 100f;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0002631C File Offset: 0x0002451C
		public float ChannelTime
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_pudge_3);
				if (abilityById != null && abilityById.Level > 0u)
				{
					return this.channelTime + abilityById.GetAbilitySpecialData("value", 0u);
				}
				return this.channelTime;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0000BB12 File Offset: 0x00009D12
		public bool IsActivatesOnChannelStart { get; } = 1;

		// Token: 0x040006C1 RID: 1729
		private readonly float channelTime;
	}
}
