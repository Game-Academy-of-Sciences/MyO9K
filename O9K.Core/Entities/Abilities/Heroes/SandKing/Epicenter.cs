using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.SandKing
{
	// Token: 0x020002DA RID: 730
	[AbilityId(AbilityId.sandking_epicenter)]
	public class Epicenter : AreaOfEffectAbility, IChanneled, IActiveAbility
	{
		// Token: 0x06000CC1 RID: 3265 RVA: 0x0000B74D File Offset: 0x0000994D
		public Epicenter(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "epicenter_damage");
			this.ChannelTime = baseAbility.GetChannelTime(0u);
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0000B77F File Offset: 0x0000997F
		public float ChannelTime { get; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0000B787 File Offset: 0x00009987
		public bool IsActivatesOnChannelStart { get; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0000B78F File Offset: 0x0000998F
		public override float Radius { get; } = 350f;
	}
}
