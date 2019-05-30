using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Dynamic.Abilities.Disables;
using O9K.AIO.Heroes.Dynamic.Abilities.Specials;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Heroes;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using SharpDX;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Blinks
{
	// Token: 0x020001B5 RID: 437
	internal class BlinkAbilityGroup : OldAbilityGroup<IBlink, OldBlinkAbility>
	{
		// Token: 0x060008B9 RID: 2233 RVA: 0x000274C4 File Offset: 0x000256C4
		public BlinkAbilityGroup(BaseHero baseHero) : base(baseHero)
		{
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0000664C File Offset: 0x0000484C
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x00006654 File Offset: 0x00004854
		public DisableAbilityGroup Disables { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0000665D File Offset: 0x0000485D
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x00006665 File Offset: 0x00004865
		public SpecialAbilityGroup Specials { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0000666E File Offset: 0x0000486E
		protected override HashSet<AbilityId> Ignored { get; } = new HashSet<AbilityId>
		{
			AbilityId.ember_spirit_activate_fire_remnant,
			AbilityId.item_hurricane_pike
		};

		// Token: 0x060008BF RID: 2239 RVA: 0x00027544 File Offset: 0x00025744
		public IEnumerable<OldBlinkAbility> GetBlinkAbilities(Unit9 owner, ComboModeMenu menu)
		{
			return from x in base.Abilities
			where x.Ability.IsValid && x.Ability.Owner.Equals(owner) && x.CanBeCasted(menu)
			select x;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0002757C File Offset: 0x0002577C
		public bool Use(Unit9 owner, List<OldBlinkAbility> blinkAbilities, Vector3 position, Unit9 target)
		{
			float distance = owner.Distance(position);
			bool? flag = this.UseSingleBlink(owner, blinkAbilities, position, distance);
			if (flag == null)
			{
				return false;
			}
			bool valueOrDefault = flag.GetValueOrDefault();
			if (valueOrDefault)
			{
				return valueOrDefault;
			}
			return this.UseComboBlink(owner, blinkAbilities, distance, position, target);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000275C8 File Offset: 0x000257C8
		public override bool Use(Unit9 target, ComboModeMenu menu, params AbilityId[] except)
		{
			foreach (IGrouping<Unit9, OldBlinkAbility> grouping in from x in base.Abilities
			where x.Ability.IsValid
			group x by x.Ability.Owner)
			{
				Unit9 key = grouping.Key;
				foreach (OldBlinkAbility oldBlinkAbility in grouping)
				{
					if (!except.Contains(oldBlinkAbility.Ability.Id) && oldBlinkAbility.CanBeCasted(menu))
					{
						Vector3 blinkPosition = this.GetBlinkPosition(key, target, menu);
						if (!blinkPosition.IsZero && key.Distance(blinkPosition) <= oldBlinkAbility.Blink.Range)
						{
							if (oldBlinkAbility.Blink.PositionCast && oldBlinkAbility.Use(blinkPosition))
							{
								return true;
							}
							if ((double)key.GetAngle(blinkPosition, false) < 0.2 && oldBlinkAbility.Use(key))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00027720 File Offset: 0x00025920
		public bool Use(Hero9 target, ComboModeMenu menu, float useRange, float blinkRange)
		{
			foreach (IGrouping<Unit9, OldBlinkAbility> grouping in from x in base.Abilities
			where x.Ability.IsValid
			group x by x.Ability.Owner)
			{
				Unit9 key = grouping.Key;
				if (key.Distance(target) >= useRange)
				{
					foreach (OldBlinkAbility oldBlinkAbility in grouping)
					{
						if (oldBlinkAbility.CanBeCasted(menu))
						{
							Vector3 vector = Vector3Extensions.Extend2D(target.Position, key.Position, blinkRange);
							if (key.Distance(vector) <= oldBlinkAbility.Blink.Range)
							{
								if (oldBlinkAbility.Blink.PositionCast && oldBlinkAbility.Use(vector))
								{
									return true;
								}
								if ((double)key.GetAngle(vector, false) < 0.2 && oldBlinkAbility.Use(key))
								{
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00006676 File Offset: 0x00004876
		protected override bool IsIgnored(Ability9 ability)
		{
			return base.IsIgnored(ability) || ability is IDisable || ability is INuke;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00027878 File Offset: 0x00025A78
		protected override void OrderAbilities()
		{
			base.Abilities = (from x in base.Abilities
			orderby this.castOrder.IndexOf(x.Ability.Id) descending, x.Ability.CastPoint
			select x).ToList<OldBlinkAbility>();
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x000278CC File Offset: 0x00025ACC
		private Vector3 GetBlinkPosition(Unit9 owner, Unit9 target, ComboModeMenu menu)
		{
			if (this.Disables.CanBeCasted(this.forceBlinkAbilities, menu, target))
			{
				if (target.IsMoving)
				{
					return target.Position;
				}
				return Vector3Extensions.Extend2D(target.Position, owner.Position, 25f);
			}
			else
			{
				if (this.Disables.CanBeCasted(new HashSet<AbilityId>
				{
					AbilityId.item_cyclone
				}, menu, target) && this.Specials.CanBeCasted(new HashSet<AbilityId>
				{
					AbilityId.item_cyclone
				}, menu, target))
				{
					return Vector3Extensions.Extend2D(target.Position, owner.Position, 25f);
				}
				Vector3 result = owner.IsRanged ? Vector3Extensions.Extend2D(target.Position, owner.Position, owner.GetAttackRange(null, 0f) * 0.75f) : target.InFront(25f, 0f, true);
				if (owner.Distance(target) < owner.GetAttackRange(target, 100f))
				{
					return Vector3.Zero;
				}
				return result;
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x000279C0 File Offset: 0x00025BC0
		private bool UseComboBlink(Unit9 owner, IEnumerable<OldBlinkAbility> blinkAbilities, float distance, Vector3 position, Unit9 target)
		{
			bool flag = false;
			List<OldBlinkAbility> list = new List<OldBlinkAbility>();
			foreach (OldBlinkAbility oldBlinkAbility in blinkAbilities)
			{
				distance -= oldBlinkAbility.Blink.Range;
				list.Add(oldBlinkAbility);
				if (distance < 0f)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				OldBlinkAbility oldBlinkAbility2 = list.LastOrDefault<OldBlinkAbility>();
				if (oldBlinkAbility2 == null)
				{
					return false;
				}
				float num = Math.Min(owner.Distance(position), oldBlinkAbility2.Blink.Range);
				Vector3 position2 = Vector3Extensions.Extend2D(owner.Position, position, num);
				if (this.UseSingleBlink(owner, new OldBlinkAbility[]
				{
					oldBlinkAbility2
				}, position2, distance) == true)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00027A9C File Offset: 0x00025C9C
		private bool? UseSingleBlink(Unit9 owner, IEnumerable<OldBlinkAbility> blinkAbilities, Vector3 position, float distance)
		{
			foreach (OldBlinkAbility oldBlinkAbility in blinkAbilities)
			{
				if (oldBlinkAbility.Blink.Range >= distance)
				{
					BlinkType blinkType = oldBlinkAbility.Blink.BlinkType;
					if (blinkType != null)
					{
						if (blinkType == 1)
						{
							if ((double)owner.GetAngle(position, false) > 0.2)
							{
								owner.BaseUnit.Move(Vector3Extensions.Extend2D(owner.Position, position, 25f));
								base.OrbwalkSleeper.Sleep(owner.Handle, owner.GetTurnTime(position));
								return null;
							}
							if (!oldBlinkAbility.Use(owner))
							{
								return new bool?(false);
							}
							if (oldBlinkAbility.Ability.Speed <= 0f)
							{
								return new bool?(true);
							}
							return null;
						}
					}
					else
					{
						if (!oldBlinkAbility.Use(position))
						{
							return new bool?(false);
						}
						if (oldBlinkAbility.Ability.Speed <= 0f)
						{
							return new bool?(true);
						}
						return null;
					}
				}
			}
			return new bool?(false);
		}

		// Token: 0x040004AF RID: 1199
		private readonly List<AbilityId> castOrder = new List<AbilityId>
		{
			AbilityId.item_force_staff,
			AbilityId.item_hurricane_pike,
			AbilityId.item_blink
		};

		// Token: 0x040004B0 RID: 1200
		private readonly HashSet<AbilityId> forceBlinkAbilities = new HashSet<AbilityId>
		{
			AbilityId.legion_commander_duel,
			AbilityId.batrider_flaming_lasso
		};
	}
}
