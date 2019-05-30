using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Range;

namespace O9K.Core.Entities.Abilities.Heroes.DragonKnight
{
	// Token: 0x02000390 RID: 912
	[AbilityId(AbilityId.dragon_knight_elder_dragon_form)]
	public class ElderDragonForm : ActiveAbility, IHasRangeIncrease
	{
		// Token: 0x06000F8A RID: 3978 RVA: 0x0000DB20 File Offset: 0x0000BD20
		public ElderDragonForm(Ability baseAbility) : base(baseAbility)
		{
			this.attackRange = new SpecialData(baseAbility, "bonus_attack_range");
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0000DB4C File Offset: 0x0000BD4C
		public bool IsRangeIncreasePermanent { get; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x0000DB54 File Offset: 0x0000BD54
		public float RangeIncrease
		{
			get
			{
				return this.attackRange.GetValue(this.Level);
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0000DB67 File Offset: 0x0000BD67
		public RangeIncreaseType RangeIncreaseType { get; } = 2;

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0000DB6F File Offset: 0x0000BD6F
		public string RangeModifierName { get; } = "modifier_dragon_knight_dragon_form";

		// Token: 0x0400080A RID: 2058
		private readonly SpecialData attackRange;
	}
}
