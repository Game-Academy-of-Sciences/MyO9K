using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Base
{
	// Token: 0x020003E9 RID: 1001
	public abstract class ToggleAbility : ActiveAbility, IToggleable
	{
		// Token: 0x060010FE RID: 4350 RVA: 0x00006683 File Offset: 0x00004883
		protected ToggleAbility(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x00029604 File Offset: 0x00027804
		public virtual bool Enabled
		{
			get
			{
				return base.BaseAbility.IsToggled;
			}
			set
			{
				bool flag = false;
				if (value)
				{
					if (!this.Enabled)
					{
						flag = base.BaseAbility.ToggleAbility();
					}
				}
				else if (this.Enabled)
				{
					flag = base.BaseAbility.ToggleAbility();
				}
				if (flag)
				{
					base.ActionSleeper.Sleep(0.1f);
				}
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0000EDE9 File Offset: 0x0000CFE9
		public override bool UseAbility(Unit9 target, bool queue = false, bool bypass = false)
		{
			return this.UseAbility(queue, bypass);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0000EDF3 File Offset: 0x0000CFF3
		public override bool UseAbility(bool queue = false, bool bypass = false)
		{
			bool flag = base.BaseAbility.ToggleAbility(queue, bypass);
			if (flag)
			{
				base.ActionSleeper.Sleep(0.1f);
			}
			return flag;
		}
	}
}
