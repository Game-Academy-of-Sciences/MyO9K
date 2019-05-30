using System;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Abilities.Items
{
	// Token: 0x0200020B RID: 523
	internal class Bloodthorn : DisableAbility
	{
		// Token: 0x06000A68 RID: 2664 RVA: 0x00003482 File Offset: 0x00001682
		public Bloodthorn(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00002E73 File Offset: 0x00001073
		protected override bool ChainStun(Unit9 target, bool invulnerability)
		{
			return true;
		}
	}
}
