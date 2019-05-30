using System;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Harasses
{
	// Token: 0x020001AE RID: 430
	internal class OldHarassAbility : OldUsableAbility
	{
		// Token: 0x060008A1 RID: 2209 RVA: 0x00006552 File Offset: 0x00004752
		public OldHarassAbility(IHarass ability) : base(ability)
		{
			this.Harass = ability;
			this.isOrb = (ability is OrbAbility);
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00006571 File Offset: 0x00004771
		public IHarass Harass { get; }

		// Token: 0x060008A3 RID: 2211 RVA: 0x00027164 File Offset: 0x00025364
		public override bool ShouldCast(Unit9 target)
		{
			ToggleAbility toggleAbility;
			return !this.isOrb && !target.IsInvulnerable && (!target.IsRooted || base.Ability.UnitTargetCast || target.GetImmobilityDuration() > 0f) && (!base.Ability.BreaksLinkens || !target.IsBlockingAbilities) && ((toggleAbility = (this.Harass as ToggleAbility)) == null || !toggleAbility.Enabled);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00026B38 File Offset: 0x00024D38
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

		// Token: 0x040004A8 RID: 1192
		private readonly bool isOrb;
	}
}
