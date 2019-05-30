using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Dynamic.Abilities.Blinks;
using O9K.AIO.Heroes.Dynamic.Abilities.Specials;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Managers.Entity;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Disables
{
	// Token: 0x020001BF RID: 447
	internal class DisableAbilityGroup : OldAbilityGroup<IDisable, OldDisableAbility>
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x000067FE File Offset: 0x000049FE
		public DisableAbilityGroup(BaseHero baseHero) : base(baseHero)
		{
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0000683D File Offset: 0x00004A3D
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x00006845 File Offset: 0x00004A45
		public BlinkAbilityGroup Blinks { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0000684E File Offset: 0x00004A4E
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x00006856 File Offset: 0x00004A56
		public SpecialAbilityGroup Specials { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0000685F File Offset: 0x00004A5F
		protected override HashSet<AbilityId> Ignored { get; } = new HashSet<AbilityId>
		{
			AbilityId.pudge_meat_hook,
			AbilityId.item_ethereal_blade
		};

		// Token: 0x060008F2 RID: 2290 RVA: 0x00028164 File Offset: 0x00026364
		public override bool Use(Unit9 target, ComboModeMenu menu, params AbilityId[] except)
		{
			foreach (OldDisableAbility oldDisableAbility in base.Abilities)
			{
				if (oldDisableAbility.Ability.IsValid && !except.Contains(oldDisableAbility.Ability.Id) && oldDisableAbility.CanBeCasted(target, base.Abilities, this.Specials.Abilities, menu) && oldDisableAbility.Use(target))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x000281FC File Offset: 0x000263FC
		public bool UseBlinkDisable(Unit9 target, ComboModeMenu menu)
		{
			foreach (IGrouping<Unit9, OldDisableAbility> grouping in from x in base.Abilities
			where x.Ability.IsValid
			group x by x.Ability.Owner)
			{
				Unit9 key = grouping.Key;
				List<OldBlinkAbility> list = this.Blinks.GetBlinkAbilities(key, menu).ToList<OldBlinkAbility>();
				if (list.Count != 0)
				{
					float range = list.Sum((OldBlinkAbility x) => x.Blink.Range);
					foreach (OldDisableAbility oldDisableAbility in grouping)
					{
						if (oldDisableAbility.CanBeCasted(target, menu, false) && oldDisableAbility.ShouldCast(target) && !oldDisableAbility.CanHit(target) && (!target.IsMagicImmune || oldDisableAbility.Disable.PiercesMagicImmunity(target)))
						{
							if (this.UseTargetable(oldDisableAbility, key, target, list, range))
							{
								return true;
							}
							if (this.UseAoe(oldDisableAbility, key, target, list, range))
							{
								return true;
							}
							if (this.UseLine(oldDisableAbility, key, target, list, range))
							{
								return true;
							}
							if (this.UseCircle(oldDisableAbility, key, target, list, range))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000283BC File Offset: 0x000265BC
		public bool UseInstantDisable(Unit9 target, ComboModeMenu menu)
		{
			foreach (OldDisableAbility oldDisableAbility in base.Abilities)
			{
				if (oldDisableAbility.Ability.IsValid && !AbilityExtensions.IsDisarm(oldDisableAbility.Disable, true) && oldDisableAbility.Ability.CastPoint <= 0.1f && oldDisableAbility.CanBeCasted(target, menu, true) && oldDisableAbility.Use(target))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00028450 File Offset: 0x00026650
		public bool UseOnly(Unit9 target, AbilityId abilityId, ComboModeMenu menu)
		{
			OldDisableAbility oldDisableAbility = base.Abilities.Find((OldDisableAbility x) => x.Ability.Id == abilityId);
			return oldDisableAbility != null && oldDisableAbility.CanBeCasted(target, menu, true) && oldDisableAbility.Use(target);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002849C File Offset: 0x0002669C
		protected override void OrderAbilities()
		{
			base.Abilities = (from x in base.Abilities
			orderby x.Ability is IChanneled, this.castOrder.IndexOf(x.Ability.Id) descending, x.Ability.CastPoint
			select x).ToList<OldDisableAbility>();
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00028514 File Offset: 0x00026714
		private bool UseAoe(OldDisableAbility ability, Unit9 owner, Unit9 target, List<OldBlinkAbility> blinkAbilities, float range)
		{
			AreaOfEffectAbility areaOfEffectAbility;
			if ((areaOfEffectAbility = (ability.Disable as AreaOfEffectAbility)) != null)
			{
				PredictionInput9 predictionInput = areaOfEffectAbility.GetPredictionInput(target, EntityManager9.EnemyHeroes);
				predictionInput.Range += range;
				predictionInput.CastRange = range;
				predictionInput.SkillShotType = 4;
				PredictionOutput9 predictionOutput = areaOfEffectAbility.GetPredictionOutput(predictionInput);
				if (predictionOutput.HitChance < 1)
				{
					return false;
				}
				if (this.Blinks.Use(owner, blinkAbilities, predictionOutput.CastPosition, target))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00028588 File Offset: 0x00026788
		private bool UseCircle(OldDisableAbility ability, Unit9 owner, Unit9 target, List<OldBlinkAbility> blinkAbilities, float range)
		{
			CircleAbility circleAbility;
			if ((circleAbility = (ability.Disable as CircleAbility)) != null)
			{
				PredictionInput9 predictionInput = circleAbility.GetPredictionInput(target, EntityManager9.EnemyHeroes);
				predictionInput.CastRange += range;
				PredictionOutput9 predictionOutput = circleAbility.GetPredictionOutput(predictionInput);
				if (predictionOutput.HitChance < 1)
				{
					return false;
				}
				Vector3 position = owner.IsRanged ? Vector3Extensions.Extend2D(owner.Position, predictionOutput.CastPosition, Math.Min(range, owner.Position.Distance2D(predictionOutput.CastPosition, false) - owner.GetAttackRange(null, 0f) / 2f)) : predictionOutput.CastPosition;
				if (this.Blinks.Use(owner, blinkAbilities, position, target))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002863C File Offset: 0x0002683C
		private bool UseLine(OldDisableAbility ability, Unit9 owner, Unit9 target, List<OldBlinkAbility> blinkAbilities, float range)
		{
			LineAbility lineAbility;
			if ((lineAbility = (ability.Disable as LineAbility)) != null)
			{
				PredictionInput9 predictionInput = lineAbility.GetPredictionInput(target, EntityManager9.EnemyHeroes);
				predictionInput.CastRange = range;
				predictionInput.Range = lineAbility.CastRange;
				predictionInput.UseBlink = true;
				PredictionOutput9 predictionOutput = lineAbility.GetPredictionOutput(predictionInput);
				if (predictionOutput.HitChance < 1)
				{
					return false;
				}
				Vector3 blinkLinePosition = predictionOutput.BlinkLinePosition;
				if (this.Blinks.Use(owner, blinkAbilities, blinkLinePosition, target))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000286B0 File Offset: 0x000268B0
		private bool UseTargetable(OldDisableAbility ability, Unit9 owner, Unit9 target, List<OldBlinkAbility> blinkAbilities, float range)
		{
			if (ability.Disable.UnitTargetCast)
			{
				Vector3 position = Vector3Extensions.Extend2D(target.Position, owner.Position, ability.Disable.CastRange / 2f);
				if (this.Blinks.Use(owner, blinkAbilities, position, target))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040004C3 RID: 1219
		private readonly List<AbilityId> castOrder = new List<AbilityId>
		{
			AbilityId.item_sheepstick
		};
	}
}
