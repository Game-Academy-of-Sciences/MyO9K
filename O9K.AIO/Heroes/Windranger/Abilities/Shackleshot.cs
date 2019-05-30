using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Windranger;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Windranger.Abilities
{
	// Token: 0x02000040 RID: 64
	internal class Shackleshot : DisableAbility
	{
		// Token: 0x0600016E RID: 366 RVA: 0x0000307A File Offset: 0x0000127A
		public Shackleshot(ActiveAbility ability) : base(ability)
		{
			this.shackleshot = (Shackleshot)ability;
			this.trees = ObjectManager.GetEntities<Tree>().ToArray<Tree>();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			Unit9 target = targetManager.Target;
			if (target.IsMagicImmune && !this.shackleshot.CanHitSpellImmuneEnemy)
			{
				return false;
			}
			if (base.Owner.Distance(target) > this.shackleshot.CastRange + this.shackleshot.ShackleRange)
			{
				return false;
			}
			PredictionInput9 predictionInput = this.shackleshot.GetPredictionInput(target, null);
			predictionInput.Range = this.shackleshot.CastRange;
			Vector3 targetPosition = this.shackleshot.PredictionManager.GetSimplePrediction(predictionInput).TargetPosition;
			Vector3 position = base.Owner.Position;
			Vector3 position2 = target.Position;
			if (!target.IsBlockingAbilities)
			{
				foreach (Unit9 unit in targetManager.AllEnemyUnits)
				{
					if (!unit.Equals(target) && !unit.IsMagicImmune && unit.Distance(target) <= this.shackleshot.ShackleRange)
					{
						PredictionInput9 predictionInput2 = this.shackleshot.GetPredictionInput(unit, null);
						predictionInput2.Delay -= target.Distance(unit) / this.shackleshot.Speed;
						Vector3 targetPosition2 = this.shackleshot.PredictionManager.GetSimplePrediction(predictionInput2).TargetPosition;
						Vector3 position3 = unit.Position;
						if (targetPosition2.Distance(targetPosition) <= this.shackleshot.ShackleRange)
						{
							float num = (targetPosition - position).AngleBetween(targetPosition2 - targetPosition);
							float num2 = (position2 - position).AngleBetween(position3 - position2);
							if ((num < this.shackleshot.Angle && num2 < this.shackleshot.Angle) || num < this.shackleshot.Angle / 2f)
							{
								this.shackleTarget = target;
								return true;
							}
						}
					}
				}
			}
			foreach (Unit9 unit2 in targetManager.AllEnemyUnits)
			{
				if (!unit2.Equals(target) && !unit2.IsMagicImmune && unit2.Distance(target) <= this.shackleshot.ShackleRange && unit2.Distance(targetPosition) >= 50f)
				{
					PredictionInput9 predictionInput3 = this.shackleshot.GetPredictionInput(unit2, null);
					Vector3 targetPosition3 = this.shackleshot.PredictionManager.GetSimplePrediction(predictionInput3).TargetPosition;
					Vector3 position4 = unit2.Position;
					if (targetPosition3.Distance(targetPosition) <= this.shackleshot.ShackleRange)
					{
						predictionInput = this.shackleshot.GetPredictionInput(target, null);
						predictionInput.Range = this.shackleshot.CastRange;
						predictionInput.Delay -= target.Distance(unit2) / this.shackleshot.Speed;
						targetPosition = this.shackleshot.PredictionManager.GetSimplePrediction(predictionInput).TargetPosition;
						float num3 = (targetPosition3 - position).AngleBetween(targetPosition - targetPosition3);
						float num4 = (position4 - position).AngleBetween(position2 - position4);
						if ((num3 < this.shackleshot.Angle && num4 < this.shackleshot.Angle) || num3 < this.shackleshot.Angle / 2f)
						{
							this.shackleTarget = unit2;
							return true;
						}
					}
				}
			}
			if (!target.IsBlockingAbilities)
			{
				foreach (Tree tree in this.trees)
				{
					if (tree.IsValid && tree.IsAlive && tree.Distance2D(targetPosition) <= this.shackleshot.ShackleRange)
					{
						float num5 = (targetPosition - position).AngleBetween(tree.Position - targetPosition);
						float num6 = (position2 - position).AngleBetween(tree.Position - position2);
						if ((num5 < this.shackleshot.Angle && num6 < this.shackleshot.Angle) || num5 < this.shackleshot.Angle / 2f)
						{
							this.shackleTarget = target;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000309F File Offset: 0x0000129F
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new ShackleshotMenu(base.Ability, simplifiedName);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000CF5C File Offset: 0x0000B15C
		public Vector3 GetBlinkPosition(TargetManager targetManager, float range)
		{
			Unit9 target = targetManager.Target;
			if (target.IsMagicImmune || target.IsEthereal || target.IsInvulnerable || !target.IsVisible || target.IsBlockingAbilities)
			{
				return Vector3.Zero;
			}
			PredictionInput9 predictionInput = this.shackleshot.GetPredictionInput(target, null);
			predictionInput.Range = this.shackleshot.CastRange;
			predictionInput.Delay -= Math.Max((base.Owner.Distance(target) - 200f) / base.Ability.Speed, 0f);
			Vector3 targetPosition = this.shackleshot.PredictionManager.GetSimplePrediction(predictionInput).TargetPosition;
			foreach (Unit9 unit in from x in targetManager.EnemyUnits
			orderby x.IsHero descending
			select x)
			{
				if (unit.Distance(targetPosition) >= 50f && (!unit.IsMagicImmune || this.shackleshot.CanHitSpellImmuneEnemy) && !unit.Equals(target) && !unit.IsInvulnerable && unit.Distance(targetPosition) <= this.shackleshot.ShackleRange)
				{
					PredictionInput9 predictionInput2 = base.Ability.GetPredictionInput(unit, null);
					predictionInput2.Delay = predictionInput.Delay;
					PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput2);
					Vector3 vector = Vector3Extensions.Extend2D(predictionOutput.TargetPosition, targetPosition, predictionOutput.TargetPosition.Distance2D(targetPosition, false) + 200f);
					if (base.Owner.Distance(vector) < range)
					{
						return vector;
					}
				}
			}
			if (!target.IsBlockingAbilities)
			{
				foreach (Tree tree in this.trees)
				{
					if (tree.IsValid && tree.IsAlive && tree.Distance2D(targetPosition) <= this.shackleshot.ShackleRange)
					{
						Vector3 vector2 = Vector3Extensions.Extend2D(tree.Position, targetPosition, tree.Distance2D(targetPosition) + 200f);
						if (base.Owner.Distance(vector2) < range)
						{
							return vector2;
						}
					}
				}
			}
			return Vector3.Zero;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
		public Vector3 GetMovePosition(TargetManager targetManager, ComboModeMenu comboModeMenu, bool windrun)
		{
			if (!comboModeMenu.GetAbilitySettingsMenu<ShackleshotMenu>(this).MoveToShackle)
			{
				return Vector3.Zero;
			}
			Unit9 target = targetManager.Target;
			if (target.IsMoving && base.Owner.Speed * (windrun ? 1.5f : 0.9f) < target.Speed)
			{
				return Vector3.Zero;
			}
			List<Vector3> list = new List<Vector3>();
			Vector3 position = target.Position;
			foreach (Unit9 unit in targetManager.EnemyUnits)
			{
				if (!unit.Equals(target) && unit.Distance(target) <= this.shackleshot.ShackleRange)
				{
					Vector3 vector = Vector3Extensions.Extend2D(unit.Position, position, -200f);
					Vector3 vector2 = Vector3Extensions.Extend2D(position, unit.Position, -200f);
					if (base.Owner.Distance(vector) < 500f && !unit.IsBlockingAbilities)
					{
						list.Add(vector);
					}
					if (base.Owner.Distance(vector2) < 500f && !target.IsBlockingAbilities)
					{
						list.Add(vector2);
					}
				}
			}
			if (!target.IsBlockingAbilities)
			{
				foreach (Tree tree in this.trees)
				{
					if (tree.IsValid && tree.IsAlive && position.Distance2D(tree.Position, false) <= this.shackleshot.ShackleRange)
					{
						Vector3 vector3 = Vector3Extensions.Extend2D(position, tree.Position, -200f);
						if (base.Owner.Distance(vector3) < 500f)
						{
							list.Add(vector3);
						}
					}
				}
			}
			if (list.Count > 0)
			{
				return (from x in list
				orderby base.Owner.Distance(x)
				select x).First<Vector3>();
			}
			return Vector3.Zero;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			if (base.Ability.UnitTargetCast && !target.IsVisible)
			{
				return false;
			}
			if (target.IsDarkPactProtected)
			{
				return false;
			}
			if (target.IsInvulnerable)
			{
				return false;
			}
			if (target.IsStunned)
			{
				return this.ChainStun(target, false);
			}
			if (target.IsHexed)
			{
				return this.ChainStun(target, false);
			}
			if (target.IsSilenced)
			{
				return !AbilityExtensions.IsSilence(base.Disable, false) || this.ChainStun(target, false);
			}
			if (target.IsRooted)
			{
				return !AbilityExtensions.IsRoot(base.Disable) || this.ChainStun(target, false);
			}
			return !target.IsRooted || base.Ability.UnitTargetCast || target.GetImmobilityDuration() > 0f;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000D470 File Offset: 0x0000B670
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 unit = this.shackleTarget ?? targetManager.Target;
			if (!base.Ability.UseAbility(unit, false, false))
			{
				return false;
			}
			float num = base.Ability.GetHitTime(unit) + 0.5f;
			float castDelay = base.Ability.GetCastDelay(unit);
			unit.SetExpectedUnitState(base.Disable.AppliesUnitState, num);
			Unit9 unit2 = this.shackleTarget;
			if (unit2 != null && !unit2.Equals(targetManager.Target))
			{
				targetManager.Target.SetExpectedUnitState(base.Disable.AppliesUnitState, num);
			}
			comboSleeper.Sleep(castDelay);
			base.OrbwalkSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(num);
			this.shackleTarget = null;
			return true;
		}

		// Token: 0x040000D2 RID: 210
		private readonly Shackleshot shackleshot;

		// Token: 0x040000D3 RID: 211
		private readonly Tree[] trees;

		// Token: 0x040000D4 RID: 212
		private Unit9 shackleTarget;
	}
}
