using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Doom
{
	// Token: 0x02000396 RID: 918
	[AbilityId(AbilityId.doom_bringer_scorched_earth)]
	public class ScorchedEarth : AreaOfEffectAbility, ISpeedBuff, IBuff, IActiveAbility
	{
		// Token: 0x06000F9A RID: 3994 RVA: 0x0000DC17 File Offset: 0x0000BE17
		public ScorchedEarth(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.bonusMoveSpeedData = new SpecialData(baseAbility, "bonus_movement_speed_pct");
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0000DC54 File Offset: 0x0000BE54
		public string BuffModifierName { get; } = "modifier_doom_bringer_scorched_earth_effect";

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0000DC5C File Offset: 0x0000BE5C
		public bool BuffsAlly { get; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0000DC64 File Offset: 0x0000BE64
		public bool BuffsOwner { get; } = 1;

		// Token: 0x06000F9E RID: 3998 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		public float GetSpeedBuff(Unit9 unit)
		{
			return unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level) / 100f;
		}

		// Token: 0x04000814 RID: 2068
		private readonly SpecialData bonusMoveSpeedData;
	}
}
