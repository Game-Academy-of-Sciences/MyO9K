using System;
using System.Collections.Generic;
using System.Linq;
using Ensage.SDK.Extensions;
using Ensage.SDK.Geometry;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Managers.Entity;
using O9K.Core.Prediction.Collision;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.Core.Prediction
{
	// Token: 0x0200000F RID: 15
	public class PredictionManager9 : IPredictionManager9
	{
		// Token: 0x06000045 RID: 69 RVA: 0x0000F570 File Offset: 0x0000D770
		public PredictionOutput9 GetPrediction(PredictionInput9 input)
		{
			PredictionOutput9 simplePrediction = this.GetSimplePrediction(input);
			if (input.AreaOfEffect)
			{
				this.GetAreaOfEffectPrediction(input, simplePrediction);
			}
			else if (input.SkillShotType == SkillShotType.AreaOfEffect)
			{
				simplePrediction.CastPosition = ((input.CastRange > 0f) ? input.Caster.InFront(input.CastRange, 0f, true) : input.Caster.Position);
			}
			PredictionManager9.GetProperCastPosition(input, simplePrediction);
			if (input.SkillShotType == SkillShotType.Line && !input.AreaOfEffect && input.UseBlink)
			{
				simplePrediction.BlinkLinePosition = simplePrediction.TargetPosition.Extend2D(input.Caster.Position, 200f);
				simplePrediction.CastPosition = simplePrediction.TargetPosition;
			}
			if (!PredictionManager9.CheckRange(input, simplePrediction))
			{
				return simplePrediction;
			}
			this.CheckCollision(input, simplePrediction);
			return simplePrediction;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000F638 File Offset: 0x0000D838
		public PredictionOutput9 GetSimplePrediction(PredictionInput9 input)
		{
			Unit9 target = input.Target;
			Vector3 position = input.Target.Position;
			Unit9 caster = input.Caster;
			bool isVisible = target.IsVisible;
			float num = input.Delay;
			PredictionOutput9 predictionOutput = new PredictionOutput9
			{
				Target = target
			};
			if (target.Equals(caster))
			{
				predictionOutput.HitChance = HitChance.High;
				predictionOutput.TargetPosition = position;
				predictionOutput.CastPosition = position;
				return predictionOutput;
			}
			if (input.RequiresToTurn)
			{
				num += caster.GetTurnTime(position);
			}
			if (input.Speed > 0f)
			{
				num += caster.Distance(position) / input.Speed;
			}
			Vector3 predictedPosition = target.GetPredictedPosition(num);
			predictionOutput.TargetPosition = predictedPosition;
			predictionOutput.CastPosition = predictedPosition;
			if (!isVisible)
			{
				predictionOutput.HitChance = HitChance.Low;
				return predictionOutput;
			}
			if (target.IsStunned || target.IsRooted || target.IsHexed)
			{
				predictionOutput.HitChance = HitChance.Immobile;
				return predictionOutput;
			}
			if (!target.IsMoving && !caster.IsVisibleToEnemies)
			{
				predictionOutput.HitChance = HitChance.High;
				return predictionOutput;
			}
			predictionOutput.HitChance = ((num > 0.5f) ? HitChance.Medium : HitChance.High);
			return predictionOutput;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000F754 File Offset: 0x0000D954
		private static bool CheckRange(PredictionInput9 input, PredictionOutput9 output)
		{
			if (input.Radius >= 9999999f || input.Range >= 9999999f)
			{
				return true;
			}
			if (input.SkillShotType == SkillShotType.AreaOfEffect)
			{
				if (output.TargetPosition.Distance2D(output.CastPosition, false) > input.Radius)
				{
					output.HitChance = HitChance.Impossible;
					return false;
				}
				return true;
			}
			else if (input.UseBlink && input.SkillShotType == SkillShotType.Line)
			{
				if (input.Caster.Distance(output.CastPosition) > input.CastRange + input.Range)
				{
					output.HitChance = HitChance.Impossible;
					return false;
				}
				return true;
			}
			else
			{
				if (input.Caster.Distance(output.CastPosition) > input.CastRange && (input.SkillShotType == SkillShotType.RangedAreaOfEffect || input.Caster.Distance(output.TargetPosition) > input.Range))
				{
					output.HitChance = HitChance.Impossible;
					return false;
				}
				return true;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000F82C File Offset: 0x0000DA2C
		private static void GetProperCastPosition(PredictionInput9 input, PredictionOutput9 output)
		{
			if (input.SkillShotType == SkillShotType.RangedAreaOfEffect || input.SkillShotType == SkillShotType.AreaOfEffect)
			{
				return;
			}
			if (input.SkillShotType == SkillShotType.Line && input.UseBlink)
			{
				return;
			}
			float radius = input.Radius;
			if (radius <= 0f)
			{
				return;
			}
			Vector3 position = input.Caster.Position;
			Vector3 castPosition = output.CastPosition;
			float num = position.Distance2D(castPosition, false);
			float castRange = input.CastRange;
			if (castRange >= num)
			{
				return;
			}
			castPosition = castPosition.Extend2D(position, Math.Min(num - castRange, radius));
			if (output.AoeTargetsHit.Count > 1)
			{
				float num2 = output.AoeTargetsHit.Max((PredictionOutput9 x) => x.TargetPosition.Distance2D(castPosition, false));
				if (num2 > radius)
				{
					num = position.Distance2D(castPosition, false);
					castPosition = position.Extend2D(castPosition, num + (num2 - radius));
				}
			}
			output.CastPosition = castPosition;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000F924 File Offset: 0x0000DB24
		private void CheckCollision(PredictionInput9 input, PredictionOutput9 output)
		{
			if (input.CollisionTypes != CollisionTypes.None)
			{
				Unit9 caster = input.Caster;
				float scanRange = caster.Distance(output.CastPosition);
				List<Unit9> list = new List<Unit9>();
				List<CollisionObject> list2 = new List<CollisionObject>();
				List<Unit9> source = (from x in EntityManager9.Units
				where x.IsUnit && !x.Equals(caster) && !x.Equals(input.Target) && x.IsAlive && x.IsVisible && x.Distance(caster) < scanRange
				select x).ToList<Unit9>();
				if ((input.CollisionTypes & CollisionTypes.AllyCreeps) == CollisionTypes.AllyCreeps)
				{
					list.AddRange(from x in source
					where x.IsAlly(caster)
					select x);
				}
				if ((input.CollisionTypes & CollisionTypes.EnemyCreeps) == CollisionTypes.EnemyCreeps)
				{
					list.AddRange(from x in source
					where !x.IsAlly(caster)
					select x);
				}
				if ((input.CollisionTypes & CollisionTypes.AllyHeroes) == CollisionTypes.AllyHeroes)
				{
					list.AddRange(from x in source
					where x.IsHero && x.IsAlly(caster)
					select x);
				}
				if ((input.CollisionTypes & CollisionTypes.EnemyHeroes) == CollisionTypes.EnemyHeroes)
				{
					list.AddRange(from x in source
					where x.IsHero && !x.IsAlly(caster)
					select x);
				}
				foreach (Unit9 unit in list)
				{
					PredictionInput9 input2 = new PredictionInput9
					{
						Target = unit,
						Caster = input.Caster,
						Delay = input.Delay,
						Speed = input.Speed,
						CastRange = input.CastRange,
						Radius = input.Radius,
						RequiresToTurn = input.RequiresToTurn
					};
					PredictionOutput9 simplePrediction = this.GetSimplePrediction(input2);
					list2.Add(new CollisionObject(unit, simplePrediction.TargetPosition, unit.HullRadius + 10f));
				}
				if (Collision.GetCollision(caster.Position.ToVector2(), output.CastPosition.ToVector2(), input.Radius, list2).Collides)
				{
					output.HitChance = HitChance.Impossible;
				}
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		private void GetAreaOfEffectPrediction(PredictionInput9 input, PredictionOutput9 output)
		{
			List<PredictionOutput9> targets = new List<PredictionOutput9>();
			IEnumerable<Unit9> areaOfEffectTargets = input.AreaOfEffectTargets;
			Func<Unit9, bool> <>9__2;
			Func<Unit9, bool> predicate;
			if ((predicate = <>9__2) == null)
			{
				predicate = (<>9__2 = ((Unit9 x) => !x.Equals(output.Target)));
			}
			foreach (Unit9 target in areaOfEffectTargets.Where(predicate))
			{
				PredictionInput9 input2 = new PredictionInput9
				{
					Target = target,
					Caster = input.Caster,
					Delay = input.Delay,
					Speed = input.Speed,
					CastRange = input.CastRange,
					Radius = input.Radius,
					RequiresToTurn = input.RequiresToTurn
				};
				PredictionOutput9 simplePrediction = this.GetSimplePrediction(input2);
				float num = (input.SkillShotType == SkillShotType.Line) ? (input.Range + input.CastRange) : input.Range;
				if (input.Caster.Distance(simplePrediction.CastPosition) < num)
				{
					targets.Add(simplePrediction);
				}
			}
			switch (input.SkillShotType)
			{
			case SkillShotType.AreaOfEffect:
				targets.Insert(0, output);
				output.CastPosition = ((input.CastRange > 0f) ? input.Caster.InFront(input.CastRange, 0f, true) : input.Caster.Position);
				output.AoeTargetsHit = (from x in targets
				where output.CastPosition.IsInRange(x.TargetPosition, input.Radius)
				select x).ToList<PredictionOutput9>();
				return;
			case SkillShotType.RangedAreaOfEffect:
				targets.Insert(0, output);
				output.CastPosition = input.Target.Position;
				output.AoeTargetsHit = (from x in targets
				where output.CastPosition.IsInRange(x.TargetPosition, input.Radius)
				select x).ToList<PredictionOutput9>();
				if (!output.AoeTargetsHit.Contains(output))
				{
					output.AoeTargetsHit.Add(output);
					return;
				}
				break;
			case SkillShotType.Line:
				targets.Insert(0, output);
				if (targets.Count > 1)
				{
					Dictionary<Polygon.Rectangle, List<PredictionOutput9>> dictionary = new Dictionary<Polygon.Rectangle, List<PredictionOutput9>>();
					if (input.UseBlink)
					{
						Vector3 targetPosition = output.TargetPosition;
						List<PredictionOutput9> list = targets.Skip(1).ToList<PredictionOutput9>();
						using (List<PredictionOutput9>.Enumerator enumerator2 = list.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								PredictionOutput9 predictionOutput = enumerator2.Current;
								Vector3 targetPosition2 = predictionOutput.TargetPosition;
								Vector3 vector = (targetPosition + targetPosition2) / 2f;
								Vector3 vector2 = targetPosition.Extend2D(targetPosition2, -100f);
								Vector3 end = vector2.Extend2D(targetPosition2, input.Range);
								Polygon.Rectangle rec = new Polygon.Rectangle(vector2, end, input.Radius);
								foreach (PredictionOutput9 predictionOutput2 in list)
								{
									if (!(predictionOutput2.Target == predictionOutput.Target))
									{
										Vector3 to = (vector + predictionOutput2.TargetPosition) / 2f;
										Vector3 vector3 = targetPosition.Extend2D(to, -100f);
										Vector3 end2 = vector3.Extend2D(to, input.Range);
										Polygon.Rectangle rectangle = new Polygon.Rectangle(vector3, end2, input.Radius + 50f);
										if (rectangle.IsInside(targetPosition2) && rectangle.IsInside(predictionOutput2.TargetPosition))
										{
											rec = rectangle;
										}
									}
								}
								dictionary[rec] = (from x in targets
								where rec.IsInside(x.TargetPosition)
								select x).ToList<PredictionOutput9>();
							}
							goto IL_C36;
						}
					}
					Vector3 position = input.Caster.Position;
					foreach (PredictionOutput9 predictionOutput3 in targets)
					{
						Vector3 end3 = position.Extend2D(predictionOutput3.TargetPosition, input.Range);
						Polygon.Rectangle rec = new Polygon.Rectangle(position, end3, input.Radius * 1.3f);
						if (!rec.IsOutside(output.TargetPosition.To2D()))
						{
							dictionary[rec] = (from x in targets
							where rec.IsInside(x.TargetPosition)
							select x).ToList<PredictionOutput9>();
						}
					}
					IL_C36:
					KeyValuePair<Polygon.Rectangle, List<PredictionOutput9>> keyValuePair = dictionary.MaxOrDefault((KeyValuePair<Polygon.Rectangle, List<PredictionOutput9>> x) => x.Value.Count);
					if (keyValuePair.Key != null)
					{
						List<PredictionOutput9> list2 = keyValuePair.Value.ToList<PredictionOutput9>();
						Vector3 to2 = list2.Aggregate(default(Vector3), (Vector3 sum, PredictionOutput9 pos) => sum + pos.TargetPosition) / (float)list2.Count;
						if (list2.Count == 0)
						{
							output.HitChance = HitChance.Impossible;
							return;
						}
						float val = list2.Max(delegate(PredictionOutput9 x)
						{
							if (!input.UseBlink)
							{
								return input.Caster.Distance(x.TargetPosition);
							}
							return output.TargetPosition.Distance(x.TargetPosition);
						});
						float distance = Math.Min(input.UseBlink ? input.Range : input.CastRange, val);
						output.CastPosition = (input.UseBlink ? output.TargetPosition.Extend2D(to2, distance) : input.Caster.Position.Extend2D(to2, distance));
						output.AoeTargetsHit = keyValuePair.Value;
					}
				}
				else
				{
					output.AoeTargetsHit.Add(output);
					if (input.UseBlink)
					{
						input.AreaOfEffect = false;
					}
				}
				if (input.UseBlink)
				{
					output.BlinkLinePosition = ((input.Caster.Distance(output.TargetPosition) > input.CastRange) ? input.Caster.Position.Extend2D(output.TargetPosition, input.CastRange) : output.TargetPosition.Extend2D(output.CastPosition, -100f));
					if (input.Caster.Distance(output.BlinkLinePosition) > input.CastRange)
					{
						output.HitChance = HitChance.Impossible;
					}
				}
				break;
			case SkillShotType.Circle:
			{
				targets.Insert(0, output);
				if (targets.Count == 1)
				{
					output.AoeTargetsHit.Add(output);
					return;
				}
				Func<PredictionOutput9, bool> <>9__4;
				Func<PredictionOutput9, float> <>9__5;
				while (targets.Count > 1)
				{
					MEC.MecCircle mec = MEC.GetMec((from x in targets
					select x.TargetPosition.ToVector2()).ToList<Vector2>());
					if (mec.Radius > 0f && mec.Radius < input.Radius && input.Caster.Distance(mec.Center.ToVector3(0f)) < input.Range)
					{
						output.CastPosition = new Vector3((targets.Count <= 2) ? ((targets[0].TargetPosition.ToVector2() + targets[1].TargetPosition.ToVector2()) / 2f) : mec.Center, output.CastPosition.Z);
						PredictionOutput9 output2 = output;
						IEnumerable<PredictionOutput9> targets3 = targets;
						Func<PredictionOutput9, bool> predicate2;
						if ((predicate2 = <>9__4) == null)
						{
							predicate2 = (<>9__4 = ((PredictionOutput9 x) => output.CastPosition.IsInRange(x.TargetPosition, input.Radius)));
						}
						output2.AoeTargetsHit = targets3.Where(predicate2).ToList<PredictionOutput9>();
						return;
					}
					IEnumerable<PredictionOutput9> targets2 = targets;
					Func<PredictionOutput9, float> comparer;
					if ((comparer = <>9__5) == null)
					{
						comparer = (<>9__5 = ((PredictionOutput9 x) => targets[0].TargetPosition.DistanceSquared(x.TargetPosition)));
					}
					PredictionOutput9 item = targets2.MaxOrDefault(comparer);
					targets.Remove(item);
					output.AoeTargetsHit.Add(output);
				}
				return;
			}
			case SkillShotType.Cone:
				targets.Insert(0, output);
				if (targets.Count > 1)
				{
					Dictionary<Polygon.Trapezoid, List<PredictionOutput9>> dictionary2 = new Dictionary<Polygon.Trapezoid, List<PredictionOutput9>>();
					if (input.UseBlink)
					{
						Vector3 targetPosition3 = output.TargetPosition;
						List<PredictionOutput9> list3 = targets.Skip(1).ToList<PredictionOutput9>();
						using (List<PredictionOutput9>.Enumerator enumerator2 = list3.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								PredictionOutput9 predictionOutput4 = enumerator2.Current;
								Vector3 targetPosition4 = predictionOutput4.TargetPosition;
								Vector3 vector4 = (targetPosition3 + targetPosition4) / 2f;
								Vector3 vector5 = targetPosition3.Extend2D(targetPosition4, -100f);
								Vector3 end4 = vector5.Extend2D(targetPosition4, input.Range);
								Polygon.Trapezoid rec = new Polygon.Trapezoid(vector5, end4, input.Radius, input.EndRadius);
								foreach (PredictionOutput9 predictionOutput5 in list3)
								{
									if (!(predictionOutput5.Target == predictionOutput4.Target))
									{
										Vector3 to3 = (vector4 + predictionOutput5.TargetPosition) / 2f;
										Vector3 vector6 = targetPosition3.Extend2D(to3, -100f);
										Vector3 end5 = vector6.Extend2D(to3, input.Range);
										Polygon.Trapezoid trapezoid = new Polygon.Trapezoid(vector6, end5, input.Radius + 50f, input.EndRadius + 50f);
										if (trapezoid.IsInside(targetPosition4) && trapezoid.IsInside(predictionOutput5.TargetPosition))
										{
											rec = trapezoid;
										}
									}
								}
								dictionary2[rec] = (from x in targets
								where rec.IsInside(x.TargetPosition)
								select x).ToList<PredictionOutput9>();
							}
							goto IL_751;
						}
					}
					Vector3 position2 = input.Caster.Position;
					foreach (PredictionOutput9 predictionOutput6 in targets)
					{
						Vector3 end6 = position2.Extend2D(predictionOutput6.TargetPosition, input.Range);
						Polygon.Trapezoid rec = new Polygon.Trapezoid(position2, end6, input.Radius * 1.4f, input.EndRadius * 1.8f);
						if (!rec.IsOutside(output.TargetPosition.To2D()))
						{
							dictionary2[rec] = (from x in targets
							where rec.IsInside(x.TargetPosition)
							select x).ToList<PredictionOutput9>();
						}
					}
					IL_751:
					KeyValuePair<Polygon.Trapezoid, List<PredictionOutput9>> keyValuePair2 = dictionary2.MaxOrDefault((KeyValuePair<Polygon.Trapezoid, List<PredictionOutput9>> x) => x.Value.Count);
					if (keyValuePair2.Key != null)
					{
						List<PredictionOutput9> list4 = keyValuePair2.Value.ToList<PredictionOutput9>();
						Vector3 to4 = list4.Aggregate(default(Vector3), (Vector3 sum, PredictionOutput9 pos) => sum + pos.TargetPosition) / (float)list4.Count;
						if (list4.Count == 0)
						{
							output.HitChance = HitChance.Impossible;
							return;
						}
						float val2 = list4.Max(delegate(PredictionOutput9 x)
						{
							if (!input.UseBlink)
							{
								return input.Caster.Distance(x.TargetPosition);
							}
							return output.TargetPosition.Distance(x.TargetPosition);
						});
						float distance2 = Math.Min(input.UseBlink ? input.Range : input.CastRange, val2);
						output.CastPosition = (input.UseBlink ? output.TargetPosition.Extend2D(to4, distance2) : input.Caster.Position.Extend2D(to4, distance2));
						output.AoeTargetsHit = keyValuePair2.Value;
					}
				}
				else
				{
					output.AoeTargetsHit.Add(output);
					if (input.UseBlink)
					{
						input.AreaOfEffect = false;
					}
				}
				if (input.UseBlink)
				{
					output.BlinkLinePosition = ((input.Caster.Distance(output.TargetPosition) > input.CastRange) ? input.Caster.Position.Extend2D(output.TargetPosition, input.CastRange) : output.TargetPosition.Extend2D(output.CastPosition, -100f));
					if (input.Caster.Distance(output.BlinkLinePosition) > input.CastRange)
					{
						output.HitChance = HitChance.Impossible;
						return;
					}
				}
				break;
			default:
				return;
			}
		}
	}
}
