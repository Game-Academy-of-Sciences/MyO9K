using System;
using System.Collections.Generic;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Abilities
{
	// Token: 0x02000207 RID: 519
	internal abstract class UsableAbility
	{
		// Token: 0x06000A52 RID: 2642 RVA: 0x0000730D File Offset: 0x0000550D
		protected UsableAbility(ActiveAbility ability)
		{
			this.Ability = ability;
			this.Owner = ability.Owner;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00007328 File Offset: 0x00005528
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x00007330 File Offset: 0x00005530
		public Sleeper Sleeper { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00007339 File Offset: 0x00005539
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x00007341 File Offset: 0x00005541
		public Sleeper OrbwalkSleeper { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0000734A File Offset: 0x0000554A
		public ActiveAbility Ability { get; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00007352 File Offset: 0x00005552
		protected Unit9 Owner { get; }

		// Token: 0x06000A59 RID: 2649 RVA: 0x0000735A File Offset: 0x0000555A
		public virtual bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
		{
			return !this.Sleeper.IsSleeping && this.Ability.CanBeCasted(channelingCheck);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00007377 File Offset: 0x00005577
		public virtual bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return this.Ability.CanHit(targetManager.Target);
		}

		// Token: 0x06000A5B RID: 2651
		public abstract bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper);

		// Token: 0x06000A5C RID: 2652 RVA: 0x0000738A File Offset: 0x0000558A
		public virtual UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return null;
		}

		// Token: 0x06000A5D RID: 2653
		public abstract bool ShouldCast(TargetManager targetManager);

		// Token: 0x06000A5E RID: 2654 RVA: 0x00002E73 File Offset: 0x00001073
		public virtual bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			return true;
		}

		// Token: 0x06000A5F RID: 2655
		public abstract bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe);

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002CB78 File Offset: 0x0002AD78
		protected virtual bool ChainStun(Unit9 target, bool invulnerability)
		{
			float num = invulnerability ? target.GetInvulnerabilityDuration() : target.GetImmobilityDuration();
			if (num <= 0f)
			{
				return false;
			}
			float num2 = this.Ability.GetHitTime(target);
			if (target.IsInvulnerable)
			{
				num2 -= 0.1f;
			}
			return num2 > num;
		}
	}
}
