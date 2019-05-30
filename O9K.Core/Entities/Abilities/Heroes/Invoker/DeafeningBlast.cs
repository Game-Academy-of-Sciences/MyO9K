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
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Heroes.Invoker
{
	// Token: 0x02000363 RID: 867
	[AbilityId(AbilityId.invoker_deafening_blast)]
	public class DeafeningBlast : ConeAbility, IInvokableAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000EE1 RID: 3809 RVA: 0x000278EC File Offset: 0x00025AEC
		public DeafeningBlast(Ability baseAbility)
		{
			AbilityId[] array = new AbilityId[3];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.91AA8DB270763F8F97D1596E149F299C04A0166F).FieldHandle);
			this.RequiredOrbs = array;
			base..ctor(baseAbility);
			this.invokeHelper = new InvokeHelper<DeafeningBlast>(this);
			this.RadiusData = new SpecialData(baseAbility, "radius_start");
			this.EndRadiusData = new SpecialData(baseAbility, "radius_end");
			this.SpeedData = new SpecialData(baseAbility, "travel_speed");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x0000D242 File Offset: 0x0000B442
		public UnitState AppliesUnitState { get; } = 2L;

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0000D24A File Offset: 0x0000B44A
		public bool CanBeInvoked
		{
			get
			{
				return this.IsInvoked || this.invokeHelper.CanInvoke(false);
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x0000D262 File Offset: 0x0000B462
		public bool IsInvoked
		{
			get
			{
				return this.invokeHelper.IsInvoked;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0000CD43 File Offset: 0x0000AF43
		public override bool IsReady
		{
			get
			{
				return this.Level != 0u && base.RemainingCooldown <= 0f && base.Owner.Mana >= base.ManaCost;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0000D26F File Offset: 0x0000B46F
		public AbilityId[] RequiredOrbs { get; }

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0000D277 File Offset: 0x0000B477
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00027970 File Offset: 0x00025B70
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = this.DamageData.GetValue(this.invokeHelper.Exort.Level);
			return damage;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0000D298 File Offset: 0x0000B498
		public bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false)
		{
			return this.invokeHelper.Invoke(currentOrbs, queue, bypass);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
		public override bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
		{
			return this.Invoke(null, false, bypass) && base.UseAbility(position, queue, bypass);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			this.invokeHelper.SetOwner(owner);
		}

		// Token: 0x040007BE RID: 1982
		private readonly InvokeHelper<DeafeningBlast> invokeHelper;
	}
}
