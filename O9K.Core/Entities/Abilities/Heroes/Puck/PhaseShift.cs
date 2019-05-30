using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Puck
{
	// Token: 0x020001F2 RID: 498
	[AbilityId(AbilityId.puck_phase_shift)]
	public class PhaseShift : ActiveAbility, IShield, IChanneled, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x060009C0 RID: 2496 RVA: 0x000236F4 File Offset: 0x000218F4
		public PhaseShift(Ability baseAbility) : base(baseAbility)
		{
			this.channelTimeData = new SpecialData(baseAbility, "duration");
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00008CD4 File Offset: 0x00006ED4
		public UnitState AppliesUnitState { get; } = 256L;

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00008CDC File Offset: 0x00006EDC
		public float ChannelTime
		{
			get
			{
				return this.channelTimeData.GetValue(this.Level);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00008CEF File Offset: 0x00006EEF
		public bool IsActivatesOnChannelStart { get; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00008CF7 File Offset: 0x00006EF7
		public string ShieldModifierName { get; } = "modifier_puck_phase_shift";

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00008CFF File Offset: 0x00006EFF
		public bool ShieldsAlly { get; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00008D07 File Offset: 0x00006F07
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00008D0F File Offset: 0x00006F0F
		public string ImmobilityModifierName { get; } = "modifier_puck_phase_shift";

		// Token: 0x040004EB RID: 1259
		private readonly SpecialData channelTimeData;
	}
}
