using System;
using Ensage;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Abilities.Heroes.DarkWillow;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes.Unique
{
	// Token: 0x020001AC RID: 428
	[AbilityId(AbilityId.dark_willow_shadow_realm)]
	internal class ShadowRealmNukeAbility : OldNukeAbility
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x000064F6 File Offset: 0x000046F6
		public ShadowRealmNukeAbility(INuke ability) : base(ability)
		{
			this.shadowRealm = (ShadowRealm)ability;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00026F68 File Offset: 0x00025168
		public override bool CanBeCasted(ComboModeMenu menu)
		{
			if (menu.IsAbilityEnabled(base.Ability))
			{
				return false;
			}
			if (base.AbilitySleeper.IsSleeping(base.Ability.Handle) || base.OrbwalkSleeper.IsSleeping(base.Ability.Owner.Handle))
			{
				return false;
			}
			if (this.shadowRealm.Casted)
			{
				return base.Ability.Owner.CanAttack(null, 0f);
			}
			return base.Ability.CanBeCasted(true);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00026FEC File Offset: 0x000251EC
		public override bool ShouldCast(Unit9 target)
		{
			if (base.Ability.UnitTargetCast && !target.IsVisible)
			{
				return false;
			}
			if (!this.shadowRealm.Casted)
			{
				return true;
			}
			if (target.IsReflectingDamage)
			{
				return false;
			}
			if (target.IsInvulnerable)
			{
				return false;
			}
			int damage = base.Nuke.GetDamage(target);
			return damage > 0 && (this.shadowRealm.DamageMaxed || (float)damage >= target.Health);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00027060 File Offset: 0x00025260
		public override bool Use(Unit9 target)
		{
			if (this.shadowRealm.Casted)
			{
				if (!base.Ability.Owner.BaseUnit.Attack(target.BaseUnit))
				{
					return false;
				}
				base.AbilitySleeper.Sleep(base.Ability.Handle, base.Ability.GetHitTime(target) + 0.5f);
				base.OrbwalkSleeper.Sleep(base.Ability.Owner.Handle, base.Ability.Owner.GetAttackPoint(null));
				return true;
			}
			else
			{
				if (!base.Ability.UseAbility(false, false))
				{
					return false;
				}
				base.OrbwalkSleeper.Sleep(base.Ability.Owner.Handle, base.Ability.GetCastDelay(base.Ability.Owner));
				base.AbilitySleeper.Sleep(base.Ability.Handle, base.Ability.GetHitTime(base.Ability.Owner) + 0.5f);
				return true;
			}
		}

		// Token: 0x040004A6 RID: 1190
		private readonly ShadowRealm shadowRealm;
	}
}
