using System;
using Ensage;

namespace O9K.Core.Entities.Abilities.Base
{
	// Token: 0x020003E2 RID: 994
	public abstract class Talent : Ability9
	{
		// Token: 0x06001091 RID: 4241 RVA: 0x0000E8CA File Offset: 0x0000CACA
		protected Talent(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x0000E8DA File Offset: 0x0000CADA
		public override bool IsTalent { get; } = 1;

		// Token: 0x06001093 RID: 4243 RVA: 0x0000E8E2 File Offset: 0x0000CAE2
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return true;
		}
	}
}
