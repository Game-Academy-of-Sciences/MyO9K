using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Tusk
{
	// Token: 0x02000295 RID: 661
	[AbilityId(AbilityId.tusk_walrus_kick)]
	public class WalrusKick : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000BBD RID: 3005 RVA: 0x0000A97B File Offset: 0x00008B7B
		public WalrusKick(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0000720E File Offset: 0x0000540E
		public override float CastRange
		{
			get
			{
				return base.CastRange + 100f;
			}
		}
	}
}
