using System;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Debuffs
{
	// Token: 0x020001B9 RID: 441
	internal class OldDebuffAbility : OldUsableAbility
	{
		// Token: 0x060008D5 RID: 2261 RVA: 0x00006725 File Offset: 0x00004925
		public OldDebuffAbility(IDebuff ability) : base(ability)
		{
			this.Debuff = ability;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00006735 File Offset: 0x00004935
		public IDebuff Debuff { get; }

		// Token: 0x060008D7 RID: 2263 RVA: 0x00027C68 File Offset: 0x00025E68
		public override bool ShouldCast(Unit9 target)
		{
			if (base.Ability.UnitTargetCast && !target.IsVisible)
			{
				return false;
			}
			if (target.HasModifier(this.Debuff.DebuffModifierName) && !(this.Debuff is INuke))
			{
				return false;
			}
			if (base.Ability.BreaksLinkens && target.IsBlockingAbilities)
			{
				return false;
			}
			if (target.IsDarkPactProtected)
			{
				return false;
			}
			if (target.IsInvulnerable)
			{
				if (this.Debuff.UnitTargetCast)
				{
					return false;
				}
				float immobilityDuration = target.GetImmobilityDuration();
				if (immobilityDuration <= 0f || immobilityDuration + 0.05f > this.Debuff.GetHitTime(target))
				{
					return false;
				}
			}
			return !target.IsRooted || base.Ability.UnitTargetCast || target.GetImmobilityDuration() > 0f;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00026B38 File Offset: 0x00024D38
		public override bool Use(Unit9 target)
		{
			if (!base.Ability.UseAbility(target, EntityManager9.EnemyHeroes, 1, 0, false, false))
			{
				return false;
			}
			base.OrbwalkSleeper.Sleep(base.Ability.Owner.Handle, base.Ability.GetCastDelay(target));
			base.AbilitySleeper.Sleep(base.Ability.Handle, base.Ability.GetHitTime(target) + 0.5f);
			return true;
		}
	}
}
