using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000186 RID: 390
	[AbilityId(AbilityId.item_ancient_janggo)]
	public class DrumOfEndurance : AreaOfEffectAbility, IBuff, IActiveAbility
	{
		// Token: 0x060007D0 RID: 2000 RVA: 0x00007501 File Offset: 0x00005701
		public DrumOfEndurance(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00007534 File Offset: 0x00005734
		public string BuffModifierName { get; } = "modifier_item_ancient_janggo_active";

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0000753C File Offset: 0x0000573C
		public bool BuffsAlly { get; } = 1;

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00007544 File Offset: 0x00005744
		public bool BuffsOwner { get; } = 1;

		// Token: 0x060007D4 RID: 2004 RVA: 0x0000754C File Offset: 0x0000574C
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.BaseItem.CurrentCharges > 0u && base.CanBeCasted(checkChanneling);
		}
	}
}
