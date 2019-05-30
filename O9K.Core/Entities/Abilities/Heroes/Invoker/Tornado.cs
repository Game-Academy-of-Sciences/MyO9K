using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
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
	// Token: 0x02000365 RID: 869
	[AbilityId(AbilityId.invoker_tornado)]
	public class Tornado : LineAbility, IInvokableAbility, IDisable, INuke, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000EF7 RID: 3831 RVA: 0x00027A60 File Offset: 0x00025C60
		public Tornado(Ability baseAbility)
		{
			AbilityId[] array = new AbilityId[3];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.28A296D511E5D75851CEBA91C0F9EB555A948E46).FieldHandle);
			this.RequiredOrbs = array;
			base..ctor(baseAbility);
			this.invokeHelper = new InvokeHelper<Tornado>(this);
			this.RadiusData = new SpecialData(baseAbility, "area_of_effect");
			this.DamageData = new SpecialData(baseAbility, "wex_damage");
			this.RangeData = new SpecialData(baseAbility, "travel_distance");
			this.SpeedData = new SpecialData(baseAbility, "travel_speed");
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0000D368 File Offset: 0x0000B568
		public UnitState AppliesUnitState { get; } = 256L;

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x0000D370 File Offset: 0x0000B570
		public bool CanBeInvoked
		{
			get
			{
				return this.IsInvoked || this.invokeHelper.CanInvoke(false);
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x0000D388 File Offset: 0x0000B588
		public string ImmobilityModifierName { get; } = "modifier_invoker_tornado";

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x0000D390 File Offset: 0x0000B590
		public bool IsInvoked
		{
			get
			{
				return this.invokeHelper.IsInvoked;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0000CD43 File Offset: 0x0000AF43
		public override bool IsReady
		{
			get
			{
				return this.Level != 0u && base.RemainingCooldown <= 0f && base.Owner.Mana >= base.ManaCost;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0000D39D File Offset: 0x0000B59D
		public override float Range
		{
			get
			{
				return this.RangeData.GetValue(this.invokeHelper.Wex.Level) + this.Radius;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x0000D3C1 File Offset: 0x0000B5C1
		public override float CastRange
		{
			get
			{
				return Math.Min(this.Range - this.Radius, base.CastRange);
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0000D3DB File Offset: 0x0000B5DB
		public AbilityId[] RequiredOrbs { get; }

		// Token: 0x06000F00 RID: 3840 RVA: 0x0000D3E3 File Offset: 0x0000B5E3
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00027AF4 File Offset: 0x00025CF4
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = this.DamageData.GetValue(this.invokeHelper.Wex.Level);
			return damage;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0000D404 File Offset: 0x0000B604
		public bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false)
		{
			return this.invokeHelper.Invoke(currentOrbs, queue, bypass);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0000D414 File Offset: 0x0000B614
		public override bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
		{
			return this.Invoke(null, false, bypass) && base.UseAbility(position, queue, bypass);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0000D42C File Offset: 0x0000B62C
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			this.invokeHelper.SetOwner(owner);
		}

		// Token: 0x040007C4 RID: 1988
		private readonly InvokeHelper<Tornado> invokeHelper;
	}
}
