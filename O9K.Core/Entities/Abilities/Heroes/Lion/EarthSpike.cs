using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Heroes.Lion
{
	// Token: 0x0200020D RID: 525
	[AbilityId(AbilityId.lion_impale)]
	public class EarthSpike : LineAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000A0F RID: 2575 RVA: 0x00023A2C File Offset: 0x00021C2C
		public EarthSpike(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "width");
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.RangeData = new SpecialData(baseAbility, "length_buffer");
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00009167 File Offset: 0x00007367
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0000916F File Offset: 0x0000736F
		public override float Range
		{
			get
			{
				return this.RangeData.GetValue(this.Level) + this.CastRange;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00009189 File Offset: 0x00007389
		public override bool BreaksLinkens { get; } = 1;

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00009191 File Offset: 0x00007391
		public override bool UnitTargetCast { get; }

		// Token: 0x06000A14 RID: 2580 RVA: 0x000235E0 File Offset: 0x000217E0
		public override bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
		{
			position = base.Owner.Position.Extend2D(position, Math.Min(this.CastRange, base.Owner.Distance(position)));
			bool flag = base.BaseAbility.UseAbility(position, queue, bypass);
			if (flag)
			{
				base.ActionSleeper.Sleep(0.1f);
			}
			return flag;
		}
	}
}
