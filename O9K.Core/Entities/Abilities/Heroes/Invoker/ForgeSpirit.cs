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

namespace O9K.Core.Entities.Abilities.Heroes.Invoker
{
	// Token: 0x0200035E RID: 862
	[AbilityId(AbilityId.invoker_forge_spirit)]
	public class ForgeSpirit : ActiveAbility, IInvokableAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000EA9 RID: 3753 RVA: 0x000277DC File Offset: 0x000259DC
		public ForgeSpirit(Ability baseAbility)
		{
			AbilityId[] array = new AbilityId[3];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.92D36661F395E36E0A7FCBF0AF774AC735C359A1).FieldHandle);
			this.RequiredOrbs = array;
			this.BuffModifierName = string.Empty;
			this.BuffsOwner = 1;
			base..ctor(baseAbility);
			this.invokeHelper = new InvokeHelper<ForgeSpirit>(this);
			this.forgeSpiritAttackRangeData = new SpecialData(baseAbility, "spirit_attack_range");
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0000CE87 File Offset: 0x0000B087
		public float ForgeSpiritAttackRange
		{
			get
			{
				return this.forgeSpiritAttackRangeData.GetValue(this.invokeHelper.Quas.Level);
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x0000CEA4 File Offset: 0x0000B0A4
		public bool CanBeInvoked
		{
			get
			{
				return this.IsInvoked || this.invokeHelper.CanInvoke(false);
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		public bool IsInvoked
		{
			get
			{
				return this.invokeHelper.IsInvoked;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x0000CD43 File Offset: 0x0000AF43
		public override bool IsReady
		{
			get
			{
				return this.Level != 0u && base.RemainingCooldown <= 0f && base.Owner.Mana >= base.ManaCost;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0000CEC9 File Offset: 0x0000B0C9
		public AbilityId[] RequiredOrbs { get; }

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x0000CED1 File Offset: 0x0000B0D1
		public string BuffModifierName { get; }

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0000CED9 File Offset: 0x0000B0D9
		public bool BuffsAlly { get; }

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x0000CEE1 File Offset: 0x0000B0E1
		public bool BuffsOwner { get; }

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0000CEE9 File Offset: 0x0000B0E9
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0000CF0A File Offset: 0x0000B10A
		public bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false)
		{
			return this.invokeHelper.Invoke(currentOrbs, queue, bypass);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0000CF1A File Offset: 0x0000B11A
		public override bool UseAbility(bool queue = false, bool bypass = false)
		{
			return this.Invoke(null, false, bypass) && base.UseAbility(queue, bypass);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0000CF31 File Offset: 0x0000B131
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			this.invokeHelper.SetOwner(owner);
		}

		// Token: 0x040007A9 RID: 1961
		private readonly SpecialData forgeSpiritAttackRangeData;

		// Token: 0x040007AA RID: 1962
		private readonly InvokeHelper<ForgeSpirit> invokeHelper;
	}
}
