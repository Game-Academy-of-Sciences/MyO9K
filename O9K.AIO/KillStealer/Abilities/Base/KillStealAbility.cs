using System;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.KillStealer.Abilities.Base
{
	// Token: 0x02000038 RID: 56
	internal class KillStealAbility
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00002E4E File Offset: 0x0000104E
		public KillStealAbility(ActiveAbility ability)
		{
			this.Ability = ability;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00002E5D File Offset: 0x0000105D
		public ActiveAbility Ability { get; }

		// Token: 0x06000144 RID: 324 RVA: 0x00002E65 File Offset: 0x00001065
		public virtual bool CanHit(Unit9 target)
		{
			return this.Ability.CanHit(target);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00002E73 File Offset: 0x00001073
		public virtual bool ShouldCast(Unit9 target)
		{
			return true;
		}
	}
}
