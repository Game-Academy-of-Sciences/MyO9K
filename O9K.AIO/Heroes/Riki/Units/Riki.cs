using System;
using System.Collections.Generic;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Riki.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Riki.Units
{
	// Token: 0x020000B9 RID: 185
	[UnitName("npc_dota_hero_riki")]
	internal class Riki : ControllableUnit
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x00014F0C File Offset: 0x0001310C
		public Riki(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.riki_smoke_screen,
					(ActiveAbility x) => this.smoke = new SmokeScreen(x)
				},
				{
					AbilityId.riki_blink_strike,
					(ActiveAbility x) => this.blinkStrike = new NukeAbility(x)
				},
				{
					AbilityId.riki_tricks_of_the_trade,
					(ActiveAbility x) => this.tricks = new TricksOfTheTrade(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
				},
				{
					AbilityId.item_abyssal_blade,
					(ActiveAbility x) => this.abyssal = new DisableAbility(x)
				},
				{
					AbilityId.item_diffusal_blade,
					(ActiveAbility x) => this.diffusal = new DebuffAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_nullifier,
					(ActiveAbility x) => this.nullifier = new Nullifier(x)
				},
				{
					AbilityId.item_solar_crest,
					(ActiveAbility x) => this.solar = new DebuffAbility(x)
				},
				{
					AbilityId.item_medallion_of_courage,
					(ActiveAbility x) => this.medallion = new DebuffAbility(x)
				},
				{
					AbilityId.item_orchid,
					(ActiveAbility x) => this.orchid = new DisableAbility(x)
				},
				{
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.bloodthorn = new Bloodthorn(x)
				}
			};
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001503C File Offset: 0x0001323C
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			TricksOfTheTrade tricksOfTheTrade = this.tricks;
			if (tricksOfTheTrade != null && tricksOfTheTrade.CancelChanneling(targetManager))
			{
				base.ComboSleeper.Sleep(0.1f);
				return true;
			}
			return abilityHelper.UseAbility(this.blinkStrike, true) || abilityHelper.UseAbility(this.abyssal, true) || abilityHelper.UseAbility(this.nullifier, true) || abilityHelper.UseAbility(this.bloodthorn, true) || abilityHelper.UseAbility(this.orchid, true) || abilityHelper.UseAbility(this.diffusal, true) || abilityHelper.UseAbility(this.solar, true) || abilityHelper.UseAbility(this.medallion, true) || (!abilityHelper.CanBeCasted(this.blinkStrike, true, true, true, true) && abilityHelper.UseAbility(this.blink, 500f, 0f)) || abilityHelper.UseAbilityIfNone(this.smoke, new UsableAbility[]
			{
				this.blinkStrike
			}) || abilityHelper.UseAbility(this.tricks, true) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00015170 File Offset: 0x00013370
		protected override bool ForceMove(Unit9 target, bool attack)
		{
			Vector3 mousePosition = Game.MousePosition;
			Vector3 vector = mousePosition;
			if (target != null && attack)
			{
				Vector3 position = target.Position;
				vector = target.InFront(100f, 180f, false);
				if (base.Menu.OrbwalkingMode == "Move to target" || this.CanAttack(target, 400f))
				{
					vector = position;
				}
				if (base.Menu.DangerRange > 0)
				{
					int num = Math.Min((int)base.Owner.GetAttackRange(null, 0f), base.Menu.DangerRange);
					float num2 = base.Owner.Distance(target);
					if (base.Menu.DangerMoveToMouse)
					{
						if (num2 < (float)num)
						{
							vector = mousePosition;
						}
					}
					else if (num2 < (float)num)
					{
						float num3 = (position - base.Owner.Position).AngleBetween(vector - position);
						if (num3 < 90f)
						{
							if (num3 < 30f)
							{
								vector = Vector3Extensions.Extend2D(position, vector, (float)((num - 25) * -1));
							}
							else
							{
								Vector3 vector2 = (mousePosition - position).Rotated(MathUtil.DegreesToRadians(90f)).Normalized() * (float)(num - 25);
								Vector3 vector3 = position + vector2;
								Vector3 vector4 = position - vector2;
								vector = ((base.Owner.Distance(vector3) < base.Owner.Distance(vector4)) ? vector3 : vector4);
							}
						}
						else if (target.Distance(vector) < (float)num)
						{
							vector = Vector3Extensions.Extend2D(position, vector, (float)(num - 25));
						}
					}
				}
			}
			if (vector == base.LastMovePosition)
			{
				return false;
			}
			if (!base.Owner.BaseUnit.Move(vector))
			{
				return false;
			}
			base.LastMovePosition = vector;
			return true;
		}

		// Token: 0x0400020E RID: 526
		private DisableAbility abyssal;

		// Token: 0x0400020F RID: 527
		private BlinkAbility blink;

		// Token: 0x04000210 RID: 528
		private NukeAbility blinkStrike;

		// Token: 0x04000211 RID: 529
		private DebuffAbility diffusal;

		// Token: 0x04000212 RID: 530
		private DebuffAbility medallion;

		// Token: 0x04000213 RID: 531
		private Nullifier nullifier;

		// Token: 0x04000214 RID: 532
		private DisableAbility bloodthorn;

		// Token: 0x04000215 RID: 533
		private DisableAbility orchid;

		// Token: 0x04000216 RID: 534
		private SpeedBuffAbility phase;

		// Token: 0x04000217 RID: 535
		private DisableAbility smoke;

		// Token: 0x04000218 RID: 536
		private DebuffAbility solar;

		// Token: 0x04000219 RID: 537
		private TricksOfTheTrade tricks;
	}
}
