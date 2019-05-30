using System;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.DarkSeer.Abilities
{
	// Token: 0x020001CD RID: 461
	internal class WallOfReplica : AoeAbility
	{
		// Token: 0x06000930 RID: 2352 RVA: 0x0000356A File Offset: 0x0000176A
		public WallOfReplica(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00006A1E File Offset: 0x00004C1E
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x00006A26 File Offset: 0x00004C26
		public Vacuum Vacuum { get; set; }

		// Token: 0x06000933 RID: 2355 RVA: 0x00006A2F File Offset: 0x00004C2F
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return base.CanHit(targetManager, comboMenu);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x000034BC File Offset: 0x000016BC
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new UsableAbilityHitCountMenu(base.Ability, simplifiedName);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000034CA File Offset: 0x000016CA
		public int TargetsToHit(IComboModeMenu comboMenu)
		{
			return comboMenu.GetAbilitySettingsMenu<UsableAbilityHitCountMenu>(this).HitCount;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00028D3C File Offset: 0x00026F3C
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (this.Vacuum != null && this.Vacuum.CastTime + 1f > Game.RawGameTime)
			{
				return base.Ability.UseAbility(this.Vacuum.CastPosition, false, false);
			}
			return base.UseAbility(targetManager, comboSleeper, aoe);
		}
	}
}
