using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Invoker.Helpers;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.Invoker
{
	// Token: 0x0200035F RID: 863
	[AbilityId(AbilityId.invoker_ghost_walk)]
	public class GhostWalk : ActiveAbility, IInvokableAbility
	{
		// Token: 0x06000EB6 RID: 3766 RVA: 0x0000CF46 File Offset: 0x0000B146
		public GhostWalk(Ability baseAbility)
		{
			AbilityId[] array = new AbilityId[3];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.E3E33884B4D5DBE5E296A8F85BD91D9EF88B779D).FieldHandle);
			this.RequiredOrbs = array;
			base..ctor(baseAbility);
			base.IsInvisibility = true;
			this.invokeHelper = new InvokeHelper<GhostWalk>(this);
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0000CF79 File Offset: 0x0000B179
		public bool CanBeInvoked
		{
			get
			{
				return this.IsInvoked || this.invokeHelper.CanInvoke(false);
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0000CF91 File Offset: 0x0000B191
		public bool IsInvoked
		{
			get
			{
				return this.invokeHelper.IsInvoked;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0000CD43 File Offset: 0x0000AF43
		public override bool IsReady
		{
			get
			{
				return this.Level != 0u && base.RemainingCooldown <= 0f && base.Owner.Mana >= base.ManaCost;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0000CF9E File Offset: 0x0000B19E
		public AbilityId[] RequiredOrbs { get; }

		// Token: 0x06000EBB RID: 3771 RVA: 0x0000CFA6 File Offset: 0x0000B1A6
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0000CFC7 File Offset: 0x0000B1C7
		public bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false)
		{
			return this.invokeHelper.Invoke(currentOrbs, queue, bypass);
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0000CFD7 File Offset: 0x0000B1D7
		public override bool UseAbility(Unit9 target, bool queue = false, bool bypass = false)
		{
			return this.Invoke(null, false, bypass) && base.UseAbility(queue, bypass);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0000CFEE File Offset: 0x0000B1EE
		public override bool UseAbility(bool queue = false, bool bypass = false)
		{
			return this.Invoke(null, false, bypass) && base.UseAbility(queue, bypass);
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0000D005 File Offset: 0x0000B205
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			this.invokeHelper.SetOwner(owner);
		}

		// Token: 0x040007AF RID: 1967
		private readonly InvokeHelper<GhostWalk> invokeHelper;
	}
}
