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
	// Token: 0x02000362 RID: 866
	[AbilityId(AbilityId.invoker_cold_snap)]
	public class ColdSnap : RangedAbility, IInvokableAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000ED7 RID: 3799 RVA: 0x0000D17A File Offset: 0x0000B37A
		public ColdSnap(Ability baseAbility)
		{
			AbilityId[] array = new AbilityId[3];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.D2E837ABB7592BEF49070A3169FE7133D3E84FA3).FieldHandle);
			this.RequiredOrbs = array;
			base..ctor(baseAbility);
			this.invokeHelper = new InvokeHelper<ColdSnap>(this);
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x0000D1AF File Offset: 0x0000B3AF
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0000D1B7 File Offset: 0x0000B3B7
		public bool CanBeInvoked
		{
			get
			{
				return this.IsInvoked || this.invokeHelper.CanInvoke(false);
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0000D1CF File Offset: 0x0000B3CF
		public bool IsInvoked
		{
			get
			{
				return this.invokeHelper.IsInvoked;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0000CD43 File Offset: 0x0000AF43
		public override bool IsReady
		{
			get
			{
				return this.Level != 0u && base.RemainingCooldown <= 0f && base.Owner.Mana >= base.ManaCost;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0000D1DC File Offset: 0x0000B3DC
		public AbilityId[] RequiredOrbs { get; }

		// Token: 0x06000EDD RID: 3805 RVA: 0x0000D1E4 File Offset: 0x0000B3E4
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0000D205 File Offset: 0x0000B405
		public bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false)
		{
			return this.invokeHelper.Invoke(currentOrbs, queue, bypass);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0000D215 File Offset: 0x0000B415
		public override bool UseAbility(Unit9 target, bool queue = false, bool bypass = false)
		{
			return this.Invoke(null, false, bypass) && base.UseAbility(target, queue, bypass);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0000D22D File Offset: 0x0000B42D
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			this.invokeHelper.SetOwner(owner);
		}

		// Token: 0x040007BB RID: 1979
		private readonly InvokeHelper<ColdSnap> invokeHelper;
	}
}
