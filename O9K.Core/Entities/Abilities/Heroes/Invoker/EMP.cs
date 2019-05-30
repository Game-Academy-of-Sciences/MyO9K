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
	// Token: 0x0200035D RID: 861
	[AbilityId(AbilityId.invoker_emp)]
	public class EMP : CircleAbility, IInvokableAbility, INuke, IActiveAbility
	{
		// Token: 0x06000E9F RID: 3743 RVA: 0x000276FC File Offset: 0x000258FC
		public EMP(Ability baseAbility)
		{
			AbilityId[] array = new AbilityId[3];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.0F27F51560F4FDC1B93D4F490FE0DC67AAB5FA39).FieldHandle);
			this.RequiredOrbs = array;
			base..ctor(baseAbility);
			this.invokeHelper = new InvokeHelper<EMP>(this);
			this.ActivationDelayData = new SpecialData(baseAbility, "delay");
			this.RadiusData = new SpecialData(baseAbility, "area_of_effect");
			this.DamageData = new SpecialData(baseAbility, "mana_burned");
			this.damagePctData = new SpecialData(baseAbility, "damage_per_mana_pct");
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		public bool CanBeInvoked
		{
			get
			{
				return this.IsInvoked || this.invokeHelper.CanInvoke(false);
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0000CE14 File Offset: 0x0000B014
		public bool IsInvoked
		{
			get
			{
				return this.invokeHelper.IsInvoked;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x0000CD43 File Offset: 0x0000AF43
		public override bool IsReady
		{
			get
			{
				return this.Level != 0u && base.RemainingCooldown <= 0f && base.Owner.Mana >= base.ManaCost;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0000CE21 File Offset: 0x0000B021
		public AbilityId[] RequiredOrbs { get; }

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0000CE29 File Offset: 0x0000B029
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00027778 File Offset: 0x00025978
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			float value = this.DamageData.GetValue(this.invokeHelper.Wex.Level);
			float num = this.damagePctData.GetValue(this.Level) / 100f;
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = (float)((int)(Math.Min(unit.Mana, value) * num));
			return damage;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0000CE4A File Offset: 0x0000B04A
		public bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false)
		{
			return this.invokeHelper.Invoke(currentOrbs, queue, bypass);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0000CE5A File Offset: 0x0000B05A
		public override bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
		{
			return this.Invoke(null, false, bypass) && base.UseAbility(position, queue, bypass);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0000CE72 File Offset: 0x0000B072
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			this.invokeHelper.SetOwner(owner);
		}

		// Token: 0x040007A6 RID: 1958
		private readonly SpecialData damagePctData;

		// Token: 0x040007A7 RID: 1959
		private readonly InvokeHelper<EMP> invokeHelper;
	}
}
