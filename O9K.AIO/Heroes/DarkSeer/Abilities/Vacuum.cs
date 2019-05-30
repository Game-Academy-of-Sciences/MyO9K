using System;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.DarkSeer.Abilities
{
	// Token: 0x020001CC RID: 460
	internal class Vacuum : DisableAbility
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x00003482 File Offset: 0x00001682
		public Vacuum(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x000069CB File Offset: 0x00004BCB
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x000069D3 File Offset: 0x00004BD3
		public Vector3 CastPosition { get; protected set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x000069DC File Offset: 0x00004BDC
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x000069E4 File Offset: 0x00004BE4
		public float CastTime { get; protected set; }

		// Token: 0x0600092C RID: 2348 RVA: 0x000069ED File Offset: 0x00004BED
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return base.CanHit(targetManager, comboMenu) && base.Ability.CanHit(targetManager.Target, targetManager.EnemyHeroes, this.TargetsToHit(comboMenu));
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x000034BC File Offset: 0x000016BC
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new UsableAbilityHitCountMenu(base.Ability, simplifiedName);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000034CA File Offset: 0x000016CA
		public int TargetsToHit(IComboModeMenu comboMenu)
		{
			return comboMenu.GetAbilitySettingsMenu<UsableAbilityHitCountMenu>(this).HitCount;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00028C70 File Offset: 0x00026E70
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			if (!base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
			{
				return false;
			}
			this.CastPosition = predictionOutput.CastPosition;
			this.CastTime = Game.RawGameTime;
			float num = base.Ability.GetHitTime(targetManager.Target) + 0.5f;
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			targetManager.Target.SetExpectedUnitState(base.Disable.AppliesUnitState, num);
			comboSleeper.Sleep(castDelay);
			base.OrbwalkSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(num);
			return true;
		}
	}
}
