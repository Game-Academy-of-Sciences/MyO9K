using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Abilities.Heroes.Invoker.Helpers;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.Invoker
{
	// Token: 0x02000361 RID: 865
	[AbilityId(AbilityId.invoker_alacrity)]
	public class Alacrity : RangedAbility, IInvokableAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000ECB RID: 3787 RVA: 0x0002789C File Offset: 0x00025A9C
		public Alacrity(Ability baseAbility)
		{
			AbilityId[] array = new AbilityId[3];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.54CB4A334BD6A28780BD9A51B9F27C7F5A14F512).FieldHandle);
			this.RequiredOrbs = array;
			base..ctor(baseAbility);
			this.invokeHelper = new InvokeHelper<Alacrity>(this);
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0000D0D7 File Offset: 0x0000B2D7
		public string BuffModifierName { get; } = "modifier_invoker_alacrity";

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0000D0DF File Offset: 0x0000B2DF
		public bool BuffsAlly { get; } = 1;

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0000D0E7 File Offset: 0x0000B2E7
		public bool BuffsOwner { get; } = 1;

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0000D0EF File Offset: 0x0000B2EF
		public bool CanBeInvoked
		{
			get
			{
				return this.IsInvoked || this.invokeHelper.CanInvoke(false);
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0000D107 File Offset: 0x0000B307
		public bool IsInvoked
		{
			get
			{
				return this.invokeHelper.IsInvoked;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0000CD43 File Offset: 0x0000AF43
		public override bool IsReady
		{
			get
			{
				return this.Level != 0u && base.RemainingCooldown <= 0f && base.Owner.Mana >= base.ManaCost;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0000D114 File Offset: 0x0000B314
		public AbilityId[] RequiredOrbs { get; }

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0000D11C File Offset: 0x0000B31C
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0000D13D File Offset: 0x0000B33D
		public bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false)
		{
			return this.invokeHelper.Invoke(currentOrbs, queue, bypass);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0000D14D File Offset: 0x0000B34D
		public override bool UseAbility(Unit9 target, bool queue = false, bool bypass = false)
		{
			return this.Invoke(null, false, bypass) && base.UseAbility(target, queue, bypass);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0000D165 File Offset: 0x0000B365
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			this.invokeHelper.SetOwner(owner);
		}

		// Token: 0x040007B6 RID: 1974
		private readonly InvokeHelper<Alacrity> invokeHelper;
	}
}
