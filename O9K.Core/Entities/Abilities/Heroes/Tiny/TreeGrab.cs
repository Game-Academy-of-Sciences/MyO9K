using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Range;

namespace O9K.Core.Entities.Abilities.Heroes.Tiny
{
	// Token: 0x020002A3 RID: 675
	[AbilityId(AbilityId.tiny_craggy_exterior)]
	public class TreeGrab : RangedAbility, IHasRangeIncrease
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x0000AC07 File Offset: 0x00008E07
		public TreeGrab(Ability baseAbility) : base(baseAbility)
		{
			this.attackRange = new SpecialData(baseAbility, "splash_width");
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0000720E File Offset: 0x0000540E
		public override float CastRange
		{
			get
			{
				return base.CastRange + 100f;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0000AC33 File Offset: 0x00008E33
		public override bool TargetsEnemy { get; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0000AC3B File Offset: 0x00008E3B
		public bool IsRangeIncreasePermanent { get; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0000AC43 File Offset: 0x00008E43
		public float RangeIncrease
		{
			get
			{
				return this.attackRange.GetValue(this.Level);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0000AC56 File Offset: 0x00008E56
		public RangeIncreaseType RangeIncreaseType { get; } = 2;

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0000AC5E File Offset: 0x00008E5E
		public string RangeModifierName { get; } = "modifier_tiny_craggy_exterior";

		// Token: 0x04000627 RID: 1575
		private readonly SpecialData attackRange;
	}
}
