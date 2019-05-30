using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Windranger.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Heroes.Unique;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Windranger.Units
{
	// Token: 0x0200003A RID: 58
	[UnitName("npc_dota_hero_windrunner")]
	internal class Windranger : ControllableUnit, IDisposable
	{
		// Token: 0x06000147 RID: 327 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		public Windranger(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			this.windranger = (owner as Windranger);
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.windrunner_shackleshot,
					delegate(ActiveAbility x)
					{
						this.shackleshot = new Shackleshot(x);
						if (this.powershot != null)
						{
							this.powershot.Shackleshot = this.shackleshot;
						}
						return this.shackleshot;
					}
				},
				{
					AbilityId.windrunner_powershot,
					delegate(ActiveAbility x)
					{
						this.powershot = new Powershot(x);
						if (this.shackleshot != null)
						{
							this.powershot.Shackleshot = this.shackleshot;
						}
						return this.powershot;
					}
				},
				{
					AbilityId.windrunner_windrun,
					(ActiveAbility x) => this.windrun = new Windrun(x)
				},
				{
					AbilityId.windrunner_focusfire,
					(ActiveAbility x) => this.focusFire = new FocusFire(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkDaggerWindranger(x)
				},
				{
					AbilityId.item_orchid,
					(ActiveAbility x) => this.orchid = new DisableAbility(x)
				},
				{
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.bloodthorn = new Bloodthorn(x)
				},
				{
					AbilityId.item_nullifier,
					(ActiveAbility x) => this.nullifier = new Nullifier(x)
				},
				{
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.windrunner_shackleshot, (ActiveAbility _) => this.shackleshot);
			base.MoveComboAbilities.Add(AbilityId.windrunner_windrun, (ActiveAbility x) => this.windrunMove = new MoveBuffAbility(x));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000C418 File Offset: 0x0000A618
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (this.powershot.CancelChanneling(targetManager))
			{
				base.ComboSleeper.Sleep(0.1f);
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
			{
				this.shackleshot
			}))
			{
				abilityHelper.ForceUseAbility(this.shackleshot, false, true);
				base.OrbwalkSleeper.Sleep(0.5f);
				return true;
			}
			if (!abilityHelper.CanBeCasted(this.shackleshot, false, false, true, true) && abilityHelper.UseAbility(this.blink, base.Owner.GetAttackRange(targetManager.Target, 0f) + 100f, 400f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.hex, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.bloodthorn, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.orchid, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.nullifier, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.shackleshot, true))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.shackleshot, false, true, true, true))
			{
				bool flag = abilityHelper.CanBeCasted(this.windrun, false, false, true, true);
				Vector3 movePosition = this.shackleshot.GetMovePosition(targetManager, comboModeMenu, flag);
				if (!movePosition.IsZero && base.Move(movePosition))
				{
					if ((base.Owner.Distance(movePosition) > 100f || targetManager.Target.IsMoving) && flag)
					{
						abilityHelper.ForceUseAbility(this.windrun, false, true);
					}
					base.OrbwalkSleeper.Sleep(0.1f);
					base.ComboSleeper.Sleep(0.1f);
					return true;
				}
			}
			if (abilityHelper.UseAbilityIfCondition(this.powershot, new UsableAbility[]
			{
				this.shackleshot,
				this.blink
			}))
			{
				base.ComboSleeper.ExtendSleep(0.2f);
				base.OrbwalkSleeper.ExtendSleep(0.2f);
				return true;
			}
			return abilityHelper.UseAbilityIfCondition(this.focusFire, new UsableAbility[]
			{
				this.powershot
			}) || abilityHelper.UseAbility(this.windrun, true) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00002E7F File Offset: 0x0000107F
		public virtual bool DirectionalMove()
		{
			if (base.OrbwalkSleeper.IsSleeping)
			{
				return false;
			}
			if (this.CanMove())
			{
				base.OrbwalkSleeper.Sleep(0.2f);
				return base.Owner.BaseUnit.MoveToDirection(Game.MousePosition);
			}
			return false;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00002EBF File Offset: 0x000010BF
		public void Dispose()
		{
			this.powershot.Dispose();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000C64C File Offset: 0x0000A84C
		public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			Windranger windranger = this.windranger;
			bool flag;
			if (windranger != null && windranger.FocusFireActive)
			{
				Unit9 focusFireTarget = this.windranger.FocusFireTarget;
				flag = (focusFireTarget != null && focusFireTarget.Equals(target));
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2 && target.IsReflectingDamage)
			{
				return this.DirectionalMove();
			}
			if (flag2 && base.Owner.HasModifier("modifier_windrunner_windrun_invis"))
			{
				flag2 = false;
			}
			return base.Orbwalk(target, !flag2, move, comboMenu);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00002ECC File Offset: 0x000010CC
		protected override bool MoveComboUseBuffs(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBuffs(abilityHelper) || abilityHelper.UseMoveAbility(this.windrunMove);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00002EEA File Offset: 0x000010EA
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseAbility(this.shackleshot, true);
		}

		// Token: 0x040000BE RID: 190
		private readonly Windranger windranger;

		// Token: 0x040000BF RID: 191
		private BlinkDaggerWindranger blink;

		// Token: 0x040000C0 RID: 192
		private DisableAbility bloodthorn;

		// Token: 0x040000C1 RID: 193
		private FocusFire focusFire;

		// Token: 0x040000C2 RID: 194
		private DisableAbility hex;

		// Token: 0x040000C3 RID: 195
		private Nullifier nullifier;

		// Token: 0x040000C4 RID: 196
		private DisableAbility orchid;

		// Token: 0x040000C5 RID: 197
		private SpeedBuffAbility phase;

		// Token: 0x040000C6 RID: 198
		private Powershot powershot;

		// Token: 0x040000C7 RID: 199
		private Shackleshot shackleshot;

		// Token: 0x040000C8 RID: 200
		private Windrun windrun;

		// Token: 0x040000C9 RID: 201
		private MoveBuffAbility windrunMove;
	}
}
