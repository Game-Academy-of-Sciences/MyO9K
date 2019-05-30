using System;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.DarkWillow;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.DarkWillow.Abilities
{
	// Token: 0x02000159 RID: 345
	internal class ShadowRealm : NukeAbility
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x00005679 File Offset: 0x00003879
		public ShadowRealm(ActiveAbility ability) : base(ability)
		{
			this.realm = (ShadowRealm)ability;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0000568E File Offset: 0x0000388E
		public bool Casted
		{
			get
			{
				return this.realm.Casted;
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000569B File Offset: 0x0000389B
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return base.Owner.Distance(targetManager.Target) <= 900f;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00020A5C File Offset: 0x0001EC5C
		public bool ShouldAttack(Unit9 target)
		{
			if (base.Owner.IsReflectingDamage && base.Owner.HealthPercentage < 50f)
			{
				return false;
			}
			if (base.Owner.IsMagicImmune && !base.Ability.PiercesMagicImmunity(target))
			{
				return false;
			}
			if ((float)this.realm.GetDamage(target) > target.Health)
			{
				return true;
			}
			if (base.Owner.Distance(target) < 700f || target.IsStunned || target.IsRooted || target.IsHexed)
			{
				return this.realm.DamageMaxed;
			}
			return this.realm.RealmTime >= 1f;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000056B8 File Offset: 0x000038B8
		public override bool ShouldCast(TargetManager targetManager)
		{
			return targetManager.Target.IsVisible;
		}

		// Token: 0x040003B5 RID: 949
		private readonly ShadowRealm realm;
	}
}
