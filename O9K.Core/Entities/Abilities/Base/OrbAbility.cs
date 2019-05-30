using System;
using Ensage;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Base
{
	// Token: 0x020003E5 RID: 997
	public abstract class OrbAbility : AutoCastAbility
	{
		// Token: 0x060010EA RID: 4330 RVA: 0x0000ECE3 File Offset: 0x0000CEE3
		protected OrbAbility(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0000ECF7 File Offset: 0x0000CEF7
		public virtual string OrbModifier { get; } = string.Empty;

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override float CastPoint
		{
			get
			{
				return base.Owner.GetAttackPoint(null);
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0000AF6D File Offset: 0x0000916D
		public override float CastRange
		{
			get
			{
				return base.Owner.GetAttackRange(null, 0f);
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x0000AF88 File Offset: 0x00009188
		public override float Speed
		{
			get
			{
				return (float)base.Owner.ProjectileSpeed;
			}
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0000ECFF File Offset: 0x0000CEFF
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.Owner.CanAttack(null, 0f) && base.CanBeCasted(checkChanneling);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0000ED1D File Offset: 0x0000CF1D
		public override bool CanHit(Unit9 target)
		{
			return (!target.IsMagicImmune || this.CanHitSpellImmuneEnemy) && base.Owner.Distance(target) <= base.Owner.GetAttackRange(null, target.HullRadius);
		}
	}
}
