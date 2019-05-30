using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Abilities.Heroes.Phoenix;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Phoenix.Abilities
{
	// Token: 0x020000D7 RID: 215
	internal class IcarusDive : NukeAbility
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x000044F1 File Offset: 0x000026F1
		public IcarusDive(ActiveAbility ability) : base(ability)
		{
			this.icarusDive = (IcarusDive)ability;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00004506 File Offset: 0x00002706
		public bool IsFlying
		{
			get
			{
				return this.icarusDive.IsFlying;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00004513 File Offset: 0x00002713
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0000451B File Offset: 0x0000271B
		public Vector3 CastPosition { get; private set; }

		// Token: 0x0600046A RID: 1130 RVA: 0x00017C98 File Offset: 0x00015E98
		public bool AutoStop(TargetManager targetManager)
		{
			if (!this.icarusDive.IsFlying || this.CastPosition != Vector3.Zero)
			{
				return false;
			}
			Modifier modifier = base.Owner.GetModifier("modifier_phoenix_icarus_dive");
			if (modifier == null)
			{
				return false;
			}
			if (targetManager != null)
			{
				if (modifier.ElapsedTime < 1f)
				{
					return false;
				}
				float num = base.Owner.Distance(targetManager.Target);
				if (targetManager.Target.Distance(this.startPosition) > base.Ability.CastRange - 200f && num > 300f && base.Ability.UseAbility(false, false))
				{
					return true;
				}
				if (num > 300f && num < 500f && base.Ability.UseAbility(false, false))
				{
					return true;
				}
			}
			else if (modifier.ElapsedTime > 1f && base.Ability.UseAbility(false, false))
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00004524 File Offset: 0x00002724
		public override bool ShouldCast(TargetManager targetManager)
		{
			return !(this.CastPosition == Vector3.Zero) || base.Owner.Distance(targetManager.Target) >= 600f;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00017D84 File Offset: 0x00015F84
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			if (usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.phoenix_supernova) != null && base.Owner.HasAghanimsScepter)
			{
				Vector3 mouse = Game.MousePosition;
				Unit9 unit = (from x in targetManager.AllyHeroes
				where !x.Equals(base.Owner) && x.HealthPercentage < 35f && x.Distance(base.Owner) < base.Ability.CastRange
				orderby x.Distance(mouse)
				select x).FirstOrDefault<Unit9>();
				if (unit != null)
				{
					this.CastPosition = unit.Position;
					return true;
				}
			}
			this.CastPosition = Vector3.Zero;
			SunRay sunRay = base.Owner.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.phoenix_sun_ray) as SunRay;
			return sunRay == null || !sunRay.IsSunRayActive;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00017E6C File Offset: 0x0001606C
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			this.startPosition = base.Owner.Position;
			if (!this.CastPosition.IsZero)
			{
				if (!base.Ability.UseAbility(this.CastPosition, false, false))
				{
					return false;
				}
			}
			else if (!base.Ability.UseAbility(targetManager.Target, targetManager.EnemyHeroes, 1, 0, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			IDisable disable;
			if ((disable = (base.Ability as IDisable)) != null)
			{
				targetManager.Target.SetExpectedUnitState(disable.AppliesUnitState, base.Ability.GetHitTime(targetManager.Target));
			}
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x04000270 RID: 624
		private readonly IcarusDive icarusDive;

		// Token: 0x04000271 RID: 625
		private Vector3 startPosition;
	}
}
