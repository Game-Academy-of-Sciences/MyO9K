using System;
using System.Linq;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Abilities
{
	// Token: 0x020001F9 RID: 505
	internal class AbilityHelper
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00006F13 File Offset: 0x00005113
		public TargetManager TargetManager { get; }

		// Token: 0x06000A02 RID: 2562 RVA: 0x00006F1B File Offset: 0x0000511B
		public AbilityHelper(TargetManager targetManager, IComboModeMenu comboModeMenu, ControllableUnit controllableUnit)
		{
			this.unit = controllableUnit.Owner;
			this.TargetManager = targetManager;
			this.comboSleeper = controllableUnit.ComboSleeper;
			this.orbwalkSleeper = controllableUnit.OrbwalkSleeper;
			this.menu = comboModeMenu;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0002BB30 File Offset: 0x00029D30
		public bool CanBeCasted(UsableAbility ability, bool canHit = true, bool shouldCast = true, bool channelingCheck = true, bool canBeCastedCheck = true)
		{
			if (ability == null || !ability.Ability.IsValid)
			{
				return false;
			}
			IComboModeMenu comboModeMenu = this.menu;
			return (comboModeMenu == null || comboModeMenu.IsAbilityEnabled(ability.Ability)) && (!canBeCastedCheck || ability.CanBeCasted(this.TargetManager, channelingCheck, this.menu)) && (!canHit || ability.CanHit(this.TargetManager, this.menu)) && (!shouldCast || ability.ShouldCast(this.TargetManager));
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002BBC0 File Offset: 0x00029DC0
		public bool CanBeCastedHidden(UsableAbility ability)
		{
			return ability != null && ability.Ability.IsValid && this.menu.IsAbilityEnabled(ability.Ability) && !ability.Ability.Owner.IsChanneling && ability.Ability.Level > 0u && ability.Ability.RemainingCooldown <= 0f && ability.Ability.Owner.Mana >= ability.Ability.ManaCost;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00006F55 File Offset: 0x00005155
		public bool CanBeCastedIfCondition(UsableAbility ability, params UsableAbility[] checkAbilities)
		{
			return this.CanBeCasted(ability, true, true, true, true) && ability.ShouldConditionCast(this.TargetManager, this.menu, (from x in checkAbilities
			where this.CanBeCasted(x, false, false, true, true)
			select x).ToList<UsableAbility>());
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00006F94 File Offset: 0x00005194
		public bool ForceUseAbility(UsableAbility ability, bool ignorePrediction = false, bool aoe = true)
		{
			if (ignorePrediction)
			{
				return ability.ForceUseAbility(this.TargetManager, this.comboSleeper);
			}
			return ability.UseAbility(this.TargetManager, this.comboSleeper, aoe);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00006FBF File Offset: 0x000051BF
		public bool HasMana(params UsableAbility[] abilities)
		{
			return this.MissingMana(abilities) <= 0f;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0002BC54 File Offset: 0x00029E54
		public float MissingMana(params UsableAbility[] abilities)
		{
			return (from x in abilities
			where x != null && x.Ability.IsValid
			select x).Sum((UsableAbility x) => x.Ability.ManaCost) - this.unit.Mana;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00006FD2 File Offset: 0x000051D2
		public bool UseAbility(BuffAbility ability, float distance)
		{
			return this.CanBeCasted(ability, true, true, true, true) && ability.UseAbility(this.TargetManager, this.comboSleeper, distance);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00006FF6 File Offset: 0x000051F6
		public bool UseAbility(ShieldAbility ability, float distance)
		{
			return this.CanBeCasted(ability, true, true, true, true) && ability.UseAbility(this.TargetManager, this.comboSleeper, distance);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0000701A File Offset: 0x0000521A
		public bool UseAbility(UsableAbility ability, bool aoe = true)
		{
			return this.CanBeCasted(ability, true, true, true, true) && ability.UseAbility(this.TargetManager, this.comboSleeper, aoe);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0000703E File Offset: 0x0000523E
		public bool UseAbility(BlinkAbility ability, float minUseRange, float blinkToEnemyRange)
		{
			return this.CanBeCasted(ability, true, true, true, true) && ability.UseAbility(this.TargetManager, this.comboSleeper, minUseRange, blinkToEnemyRange);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00007063 File Offset: 0x00005263
		public bool UseAbility(BlinkAbility ability, Vector3 position)
		{
			return this.CanBeCasted(ability, true, true, true, true) && ability.UseAbility(this.TargetManager, this.comboSleeper, position);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00007087 File Offset: 0x00005287
		public bool UseAbilityIfAny(UsableAbility ability, params UsableAbility[] checkAbilities)
		{
			return this.CanBeCasted(ability, true, true, true, true) && checkAbilities.Any((UsableAbility x) => this.CanBeCasted(x, false, true, true, true)) && ability.UseAbility(this.TargetManager, this.comboSleeper, true);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002BCB8 File Offset: 0x00029EB8
		public bool UseAbilityIfCondition(UsableAbility ability, params UsableAbility[] checkAbilities)
		{
			return this.CanBeCasted(ability, true, true, true, true) && ability.ShouldConditionCast(this.TargetManager, this.menu, (from x in checkAbilities
			where this.CanBeCasted(x, false, false, true, true)
			select x).ToList<UsableAbility>()) && ability.UseAbility(this.TargetManager, this.comboSleeper, true);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000070C1 File Offset: 0x000052C1
		public bool UseAbilityIfNone(UsableAbility ability, params UsableAbility[] checkAbilities)
		{
			return this.CanBeCasted(ability, true, true, true, true) && checkAbilities.All((UsableAbility x) => !this.CanBeCasted(x, false, true, true, true)) && ability.UseAbility(this.TargetManager, this.comboSleeper, true);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002BD14 File Offset: 0x00029F14
		public bool UseBlinkLineCombo(BlinkAbility blink, UsableAbility ability)
		{
			if (!this.CanBeCasted(ability, false, true, true, true) || !this.CanBeCasted(blink, true, true, true, true))
			{
				return false;
			}
			LineAbility lineAbility;
			if ((lineAbility = (ability.Ability as LineAbility)) == null)
			{
				return false;
			}
			Unit9 target = this.TargetManager.Target;
			float num = lineAbility.CastRange;
			if (blink.Ability.CastRange < num)
			{
				num = blink.Ability.CastRange - 100f;
			}
			if (lineAbility.Owner.Distance(target) < num)
			{
				return false;
			}
			PredictionInput9 predictionInput = lineAbility.GetPredictionInput(target, this.TargetManager.EnemyHeroes);
			predictionInput.CastRange = blink.Ability.Range;
			predictionInput.Range = lineAbility.CastRange;
			predictionInput.UseBlink = true;
			PredictionOutput9 predictionOutput = lineAbility.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			Vector3 blinkLinePosition = predictionOutput.BlinkLinePosition;
			if (blink.Ability.UseAbility(blinkLinePosition, false, false) && lineAbility.UseAbility(predictionOutput.CastPosition, false, false))
			{
				float castDelay = ability.Ability.GetCastDelay(predictionOutput.CastPosition);
				this.comboSleeper.Sleep(castDelay + 0.3f);
				this.orbwalkSleeper.Sleep(castDelay + 0.5f);
				ability.Sleeper.Sleep(castDelay + 0.5f);
				blink.Sleeper.Sleep(castDelay + 0.5f);
				return true;
			}
			return false;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002BE6C File Offset: 0x0002A06C
		public bool UseDoubleBlinkCombo(ForceStaff force, BlinkAbility blink, float minDistance = 0f)
		{
			if (!this.CanBeCasted(force, false, true, true, true) || !this.CanBeCasted(blink, false, true, true, true))
			{
				return false;
			}
			Unit9 target = this.TargetManager.Target;
			Unit9 owner = force.Ability.Owner;
			if (owner.Distance(target) < minDistance || owner.Distance(target) < blink.Ability.Range)
			{
				return false;
			}
			float num = blink.Ability.Range + force.Ability.Range;
			if (owner.Distance(target) > num)
			{
				return false;
			}
			if (owner.GetAngle(target.Position, false) > 0.5f)
			{
				owner.BaseUnit.Move(target.Position);
				this.comboSleeper.Sleep(0.1f);
				return false;
			}
			force.Ability.UseAbility(owner, false, false);
			this.comboSleeper.Sleep(force.Ability.GetCastDelay() + 0.3f);
			return false;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002BF58 File Offset: 0x0002A158
		public bool UseForceStaffAway(ForceStaff force, int range)
		{
			if (!this.CanBeCasted(force, true, true, true, true))
			{
				return false;
			}
			Unit9 target = this.TargetManager.Target;
			if (target.IsRanged || target.IsStunned || target.IsRooted || target.IsHexed || target.IsDisarmed)
			{
				return false;
			}
			Unit9 owner = force.Ability.Owner;
			if (target.Distance(owner) > (float)range)
			{
				return false;
			}
			Vector3 mousePosition = Game.MousePosition;
			if (owner.GetAngle(mousePosition, false) > 1f)
			{
				owner.BaseUnit.Move(mousePosition);
				UpdateManager.BeginInvoke(delegate
				{
					force.Ability.UseAbility(owner, false, false);
				}, 200);
				return true;
			}
			return false;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002C02C File Offset: 0x0002A22C
		public bool UseKillStealAbility(NukeAbility ability, bool aoe = true)
		{
			return this.CanBeCasted(ability, true, true, true, true) && (float)ability.Ability.GetDamage(this.TargetManager.Target) >= this.TargetManager.Target.Health && ability.UseAbility(this.TargetManager, this.comboSleeper, aoe);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000070FB File Offset: 0x000052FB
		public bool UseMoveAbility(BlinkAbility ability)
		{
			return this.CanBeCasted(ability, false, true, true, true) && ability.UseAbility(this.TargetManager, this.comboSleeper, Game.MousePosition);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00007123 File Offset: 0x00005323
		public bool UseMoveAbility(UsableAbility ability)
		{
			return this.CanBeCasted(ability, true, true, true, true) && ability.UseAbility(this.TargetManager, this.comboSleeper, true);
		}

		// Token: 0x04000548 RID: 1352
		private readonly Sleeper comboSleeper;

		// Token: 0x04000549 RID: 1353
		private readonly IComboModeMenu menu;

		// Token: 0x0400054A RID: 1354
		private readonly Sleeper orbwalkSleeper;

		// Token: 0x0400054C RID: 1356
		private readonly Unit9 unit;
	}
}
