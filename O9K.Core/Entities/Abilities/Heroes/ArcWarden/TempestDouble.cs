using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.ArcWarden
{
	// Token: 0x0200025F RID: 607
	[AbilityId(AbilityId.arc_warden_tempest_double)]
	public class TempestDouble : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000B0A RID: 2826 RVA: 0x00009FC0 File Offset: 0x000081C0
		public TempestDouble(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00009FDB File Offset: 0x000081DB
		public string BuffModifierName { get; } = string.Empty;

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00009FE3 File Offset: 0x000081E3
		public bool BuffsAlly { get; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00009FEB File Offset: 0x000081EB
		public bool BuffsOwner { get; } = 1;
	}
}
