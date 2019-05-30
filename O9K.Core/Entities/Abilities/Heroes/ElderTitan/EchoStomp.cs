using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.ElderTitan
{
	// Token: 0x0200038A RID: 906
	[AbilityId(AbilityId.elder_titan_echo_stomp)]
	public class EchoStomp : AreaOfEffectAbility, IDisable, IChanneled, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000F7D RID: 3965 RVA: 0x000283F8 File Offset: 0x000265F8
		public EchoStomp(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.ActivationDelayData = new SpecialData(baseAbility, "cast_time");
			this.DamageData = new SpecialData(baseAbility, "stomp_damage");
			this.ChannelTime = baseAbility.GetChannelTime(0u);
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0000DA9A File Offset: 0x0000BC9A
		public override float ActivationDelay
		{
			get
			{
				return base.ActivationDelay - this.CastPoint;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0000DAA9 File Offset: 0x0000BCA9
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0000DAB1 File Offset: 0x0000BCB1
		public float ChannelTime { get; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0000DAB9 File Offset: 0x0000BCB9
		public bool IsActivatesOnChannelStart { get; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0000DAC1 File Offset: 0x0000BCC1
		public string ImmobilityModifierName { get; } = "modifier_elder_titan_echo_stomp";
	}
}
