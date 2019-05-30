using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Range;

namespace O9K.Core.Entities.Abilities.Heroes.Terrorblade
{
	// Token: 0x020001CE RID: 462
	[AbilityId(AbilityId.terrorblade_metamorphosis)]
	public class Metamorphosis : ActiveAbility, IBuff, IHasRangeIncrease, IActiveAbility
	{
		// Token: 0x06000946 RID: 2374 RVA: 0x000085AF File Offset: 0x000067AF
		public Metamorphosis(Ability baseAbility) : base(baseAbility)
		{
			this.attackRange = new SpecialData(baseAbility, "bonus_range");
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x000085ED File Offset: 0x000067ED
		public bool IsRangeIncreasePermanent { get; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x000085F5 File Offset: 0x000067F5
		public float RangeIncrease
		{
			get
			{
				return this.attackRange.GetValue(this.Level);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00008608 File Offset: 0x00006808
		public RangeIncreaseType RangeIncreaseType { get; } = 2;

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00008610 File Offset: 0x00006810
		public string RangeModifierName { get; } = "modifier_terrorblade_metamorphosis";

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00008618 File Offset: 0x00006818
		public string BuffModifierName { get; } = "modifier_terrorblade_metamorphosis";

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00008620 File Offset: 0x00006820
		public bool BuffsAlly { get; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00008628 File Offset: 0x00006828
		public bool BuffsOwner { get; } = 1;

		// Token: 0x040004A4 RID: 1188
		private readonly SpecialData attackRange;
	}
}
