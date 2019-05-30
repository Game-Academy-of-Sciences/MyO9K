using System;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Spectre.Abilities
{
	// Token: 0x0200009E RID: 158
	internal class DaggerMove : UsableAbility
	{
		// Token: 0x0600031E RID: 798 RVA: 0x00003F23 File Offset: 0x00002123
		public DaggerMove(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00002E73 File Offset: 0x00001073
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return true;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00003880 File Offset: 0x00001A80
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			return false;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00002E73 File Offset: 0x00001073
		public override bool ShouldCast(TargetManager targetManager)
		{
			return true;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00012B70 File Offset: 0x00010D70
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Vector3 vector = Vector3Extensions.Extend2D(base.Owner.Position, Game.MousePosition, Math.Max(base.Ability.CastRange, base.Owner.Distance(Game.MousePosition)));
			if (!base.Ability.UseAbility(vector, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(vector);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
