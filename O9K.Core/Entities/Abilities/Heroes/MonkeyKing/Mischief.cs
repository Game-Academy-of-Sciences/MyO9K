using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Abilities.Heroes.MonkeyKing
{
	// Token: 0x02000326 RID: 806
	[AbilityId(AbilityId.monkey_king_mischief)]
	public class Mischief : ActiveAbility
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x00006683 File Offset: 0x00004883
		public Mischief(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0000C462 File Offset: 0x0000A662
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			if (!this.IsUsable)
			{
				return this.revertForm.CanBeCasted(checkChanneling);
			}
			return base.CanBeCasted(checkChanneling);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0000C480 File Offset: 0x0000A680
		public override bool UseAbility(Unit9 target, bool queue = false, bool bypass = false)
		{
			if (!this.IsUsable)
			{
				return this.revertForm.UseAbility(target, queue, bypass);
			}
			return base.UseAbility(target, queue, bypass);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0000C4A2 File Offset: 0x0000A6A2
		public override bool UseAbility(bool queue = false, bool bypass = false)
		{
			if (!this.IsUsable)
			{
				return this.revertForm.UseAbility(queue, bypass);
			}
			return base.UseAbility(queue, bypass);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00026FFC File Offset: 0x000251FC
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.monkey_king_untransform)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				throw new ArgumentNullException("revertForm");
			}
			this.revertForm = (RevertForm)EntityManager9.AddAbility(ability);
		}

		// Token: 0x04000737 RID: 1847
		private RevertForm revertForm;
	}
}
