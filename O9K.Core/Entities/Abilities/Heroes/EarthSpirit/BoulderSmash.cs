using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.EarthSpirit
{
	// Token: 0x02000233 RID: 563
	[AbilityId(AbilityId.earth_spirit_boulder_smash)]
	public class BoulderSmash : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x000240B0 File Offset: 0x000222B0
		public BoulderSmash(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.RangeData = new SpecialData(baseAbility, "rock_distance");
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.DamageData = new SpecialData(baseAbility, "rock_damage");
			this.castRange = new SpecialData(baseAbility, "rock_search_aoe");
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x000097D9 File Offset: 0x000079D9
		public override float Range
		{
			get
			{
				return this.RangeData.GetValue(this.Level) + this.Radius;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x000097F3 File Offset: 0x000079F3
		protected override float BaseCastRange
		{
			get
			{
				return this.castRange.GetValue(this.Level);
			}
		}

		// Token: 0x0400054E RID: 1358
		private readonly SpecialData castRange;
	}
}
