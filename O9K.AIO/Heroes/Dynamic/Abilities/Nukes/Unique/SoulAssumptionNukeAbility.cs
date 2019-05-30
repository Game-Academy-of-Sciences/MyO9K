using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Abilities.Heroes.Visage;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes.Unique
{
	// Token: 0x020001AD RID: 429
	[AbilityId(AbilityId.visage_soul_assumption)]
	internal class SoulAssumptionNukeAbility : OldNukeAbility
	{
		// Token: 0x0600089F RID: 2207 RVA: 0x0000650B File Offset: 0x0000470B
		public SoulAssumptionNukeAbility(INuke ability) : base(ability)
		{
			this.soulAssumption = (SoulAssumption)ability;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00006520 File Offset: 0x00004720
		public override bool ShouldCast(Unit9 target)
		{
			return base.ShouldCast(target) && (this.soulAssumption.MaxCharges || (float)this.soulAssumption.GetDamage(target) >= target.Health);
		}

		// Token: 0x040004A7 RID: 1191
		private readonly SoulAssumption soulAssumption;
	}
}
