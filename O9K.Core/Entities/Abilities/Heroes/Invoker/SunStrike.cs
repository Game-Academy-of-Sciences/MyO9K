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
	// Token: 0x02000364 RID: 868
	[AbilityId(AbilityId.invoker_sun_strike)]
	public class SunStrike : CircleAbility, IInvokableAbility, INuke, IActiveAbility
	{
		// Token: 0x06000EEC RID: 3820 RVA: 0x000279AC File Offset: 0x00025BAC
		public SunStrike(Ability baseAbility)
		{
			AbilityId[] array = new AbilityId[3];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.3245E38FAE82429686D914173EBA82D15C37E9C4).FieldHandle);
			this.RequiredOrbs = array;
			base..ctor(baseAbility);
			this.invokeHelper = new InvokeHelper<SunStrike>(this);
			this.ActivationDelayData = new SpecialData(baseAbility, "delay");
			this.RadiusData = new SpecialData(baseAbility, "area_of_effect");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0000D2D5 File Offset: 0x0000B4D5
		public bool CanBeInvoked
		{
			get
			{
				return this.IsInvoked || this.invokeHelper.CanInvoke(false);
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0000D2ED File Offset: 0x0000B4ED
		public override float CastRange { get; } = 9999999f;

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0000D2F5 File Offset: 0x0000B4F5
		public bool IsInvoked
		{
			get
			{
				return this.invokeHelper.IsInvoked;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0000CD43 File Offset: 0x0000AF43
		public override bool IsReady
		{
			get
			{
				return this.Level != 0u && base.RemainingCooldown <= 0f && base.Owner.Mana >= base.ManaCost;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0000D302 File Offset: 0x0000B502
		public AbilityId[] RequiredOrbs { get; }

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0000D30A File Offset: 0x0000B50A
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00027A24 File Offset: 0x00025C24
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = this.DamageData.GetValue(this.invokeHelper.Exort.Level);
			return damage;
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0000D32B File Offset: 0x0000B52B
		public bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false)
		{
			return this.invokeHelper.Invoke(currentOrbs, queue, bypass);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0000D33B File Offset: 0x0000B53B
		public override bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
		{
			return this.Invoke(null, false, bypass) && base.UseAbility(position, queue, bypass);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0000D353 File Offset: 0x0000B553
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			this.invokeHelper.SetOwner(owner);
		}

		// Token: 0x040007C1 RID: 1985
		private readonly InvokeHelper<SunStrike> invokeHelper;
	}
}
