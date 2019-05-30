using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Invoker.Helpers;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Invoker
{
	// Token: 0x02000360 RID: 864
	[AbilityId(AbilityId.invoker_ice_wall)]
	public class IceWall : ActiveAbility, IInvokableAbility
	{
		// Token: 0x06000EC0 RID: 3776 RVA: 0x00027838 File Offset: 0x00025A38
		public IceWall(Ability baseAbility)
		{
			AbilityId[] array = new AbilityId[3];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.B5390FAF1C60F70E75475651C37B17BF2A95CA56).FieldHandle);
			this.RequiredOrbs = array;
			this.DebuffModifierName = "modifier_invoker_ice_wall_slow_debuff";
			base..ctor(baseAbility);
			this.invokeHelper = new InvokeHelper<IceWall>(this);
			this.spacing = new SpecialData(baseAbility, "wall_element_spacing");
			this.count = new SpecialData(baseAbility, "num_wall_elements");
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0000D01A File Offset: 0x0000B21A
		public override float Radius
		{
			get
			{
				return this.spacing.GetValue(this.Level) * this.count.GetValue(this.Level) / 2f;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x0000D045 File Offset: 0x0000B245
		public bool CanBeInvoked
		{
			get
			{
				return this.IsInvoked || this.invokeHelper.CanInvoke(false);
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0000D05D File Offset: 0x0000B25D
		public bool IsInvoked
		{
			get
			{
				return this.invokeHelper.IsInvoked;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x0000CD43 File Offset: 0x0000AF43
		public override bool IsReady
		{
			get
			{
				return this.Level != 0u && base.RemainingCooldown <= 0f && base.Owner.Mana >= base.ManaCost;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x0000D06A File Offset: 0x0000B26A
		public AbilityId[] RequiredOrbs { get; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0000D072 File Offset: 0x0000B272
		public string DebuffModifierName { get; }

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0000D07A File Offset: 0x0000B27A
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0000D09B File Offset: 0x0000B29B
		public bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false)
		{
			return this.invokeHelper.Invoke(currentOrbs, queue, bypass);
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0000D0AB File Offset: 0x0000B2AB
		public override bool UseAbility(bool queue = false, bool bypass = false)
		{
			return this.Invoke(null, false, bypass) && base.UseAbility(queue, bypass);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0000D0C2 File Offset: 0x0000B2C2
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			this.invokeHelper.SetOwner(owner);
		}

		// Token: 0x040007B1 RID: 1969
		private readonly SpecialData count;

		// Token: 0x040007B2 RID: 1970
		private readonly InvokeHelper<IceWall> invokeHelper;

		// Token: 0x040007B3 RID: 1971
		private readonly SpecialData spacing;
	}
}
