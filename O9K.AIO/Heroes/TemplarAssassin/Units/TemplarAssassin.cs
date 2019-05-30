using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.TemplarAssassin.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.TemplarAssassin;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using SharpDX;

namespace O9K.AIO.Heroes.TemplarAssassin.Units
{
	// Token: 0x0200008C RID: 140
	[UnitName("npc_dota_hero_templar_assassin")]
	internal class TemplarAssassin : ControllableUnit
	{
		// Token: 0x060002BF RID: 703 RVA: 0x0001134C File Offset: 0x0000F54C
		public TemplarAssassin(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.templar_assassin_refraction,
					(ActiveAbility x) => this.refraction = new BuffAbility(x)
				},
				{
					AbilityId.templar_assassin_meld,
					(ActiveAbility x) => this.meld = new NukeAbility(x)
				},
				{
					AbilityId.templar_assassin_psionic_trap,
					(ActiveAbility x) => this.trap = new PsionicTrap(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
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
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_hurricane_pike,
					(ActiveAbility x) => this.pike = new HurricanePike(x)
				},
				{
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				},
				{
					AbilityId.item_solar_crest,
					(ActiveAbility x) => this.solar = new DebuffAbility(x)
				},
				{
					AbilityId.item_medallion_of_courage,
					(ActiveAbility x) => this.medallion = new DebuffAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.templar_assassin_refraction, (ActiveAbility _) => this.refraction);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000114A0 File Offset: 0x0000F6A0
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			if (comboModeMenu.IsHarassCombo)
			{
				return false;
			}
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.refraction, 1300f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.blink, 500f, 0f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.force, 500f, 0f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.pike, 500f, 0f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.hex, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.solar, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.medallion, true))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.meld, false, true, true, true))
			{
				this.preventAttackSleeper.Sleep(0.05f);
				if (!base.AttackSleeper.IsSleeping && abilityHelper.UseAbility(this.meld, true))
				{
					float num = base.Owner.GetAttackPoint(null) + 0.1f;
					base.ComboSleeper.ExtendSleep(num);
					base.MoveSleeper.Sleep(num);
					base.AttackSleeper.Sleep(num);
					return true;
				}
			}
			return abilityHelper.UseAbility(this.orchid, true) || abilityHelper.UseAbility(this.bloodthorn, true) || abilityHelper.UseAbility(this.nullifier, true) || ((!abilityHelper.CanBeCasted(this.blink, true, true, true, true) || base.Owner.Distance(targetManager.Target) < 1000f) && abilityHelper.UseAbility(this.trap, true));
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0001163C File Offset: 0x0000F83C
		public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			if (base.OrbwalkSleeper.IsSleeping)
			{
				return false;
			}
			if (target != null && comboMenu != null && comboMenu.IsHarassCombo && target.Distance(base.Owner) > base.Owner.GetAttackRange(target, 0f))
			{
				if (base.MoveSleeper.IsSleeping)
				{
					return false;
				}
				if (this.MoveToProjectile(target))
				{
					return true;
				}
				PsiBlades psi = (PsiBlades)base.Owner.Abilities.First((Ability9 x) => x.Id == AbilityId.templar_assassin_psi_blades);
				Vector3 ownerPosition = base.Owner.Position;
				float num = base.Owner.GetAttackPoint(null) + Game.Ping / 1000f + 0.3f;
				Vector3 targetPredictedPosition = target.GetPredictedPosition(num);
				Unit9 unit = (from x in EntityManager9.Units
				where x.IsUnit && !x.Equals(target) && x.IsAlive && x.IsVisible && !x.IsInvulnerable && (!x.IsAlly(this.Owner) || (x.IsCreep && x.HealthPercentage < 50f)) && x.Distance(target) < psi.SplitRange - 75f
				orderby Vector3Extensions.AngleBetween(ownerPosition, x.Position, targetPredictedPosition)
				select x).FirstOrDefault<Unit9>();
				if (unit != null)
				{
					Vector3 position = unit.Position;
					if (this.CanAttack(unit, 0f) && Vector3Extensions.AngleBetween(ownerPosition, position, targetPredictedPosition) < 15f)
					{
						base.LastMovePosition = Vector3.Zero;
						base.LastTarget = unit;
						base.OrbwalkSleeper.Sleep(0.05f);
						return this.Attack(unit, comboMenu);
					}
					float num2 = Math.Min(Math.Max(unit.Distance(ownerPosition), 150f), base.Owner.GetAttackRange(null, 0f));
					Vector3 movePosition = Vector3Extensions.Extend2D(position, targetPredictedPosition, -num2);
					base.OrbwalkSleeper.Sleep(0.05f);
					return base.Move(movePosition);
				}
				else
				{
					attack = false;
				}
			}
			return base.Orbwalk(target, attack, move, comboMenu);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00003C24 File Offset: 0x00001E24
		protected override bool Attack(Unit9 target, ComboModeMenu comboMenu)
		{
			return !this.preventAttackSleeper.IsSleeping && base.Attack(target, comboMenu);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00003C3D File Offset: 0x00001E3D
		protected override bool MoveComboUseShields(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseShields(abilityHelper) || abilityHelper.UseMoveAbility(this.refraction);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00011870 File Offset: 0x0000FA70
		private bool MoveToProjectile(Unit9 target)
		{
			if (!this.CanMove())
			{
				return false;
			}
			TrackingProjectile trackingProjectile = ObjectManager.TrackingProjectiles.FirstOrDefault(delegate(TrackingProjectile x)
			{
				if (x.IsValid)
				{
					Entity source = x.Source;
					EntityHandle? entityHandle = (source != null) ? new EntityHandle?(source.Handle) : null;
					if (((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == base.Owner.Handle)
					{
						Entity target3 = x.Target;
						return target3 != null && target3.IsValid;
					}
				}
				return false;
			});
			if (trackingProjectile == null)
			{
				return false;
			}
			Entity target2 = trackingProjectile.Target;
			Vector3 predictedPosition = target.GetPredictedPosition(trackingProjectile.Position.Distance2D(target2.Position, false) / (float)trackingProjectile.Speed + Game.Ping / 1000f);
			Vector3 position = Vector3Extensions.Extend2D(target2.Position, predictedPosition, -base.Owner.Distance(target2.Position));
			base.Owner.BaseUnit.Move(position);
			return true;
		}

		// Token: 0x0400017E RID: 382
		private readonly Sleeper preventAttackSleeper = new Sleeper();

		// Token: 0x0400017F RID: 383
		private BlinkAbility blink;

		// Token: 0x04000180 RID: 384
		private DisableAbility bloodthorn;

		// Token: 0x04000181 RID: 385
		private ForceStaff force;

		// Token: 0x04000182 RID: 386
		private DisableAbility hex;

		// Token: 0x04000183 RID: 387
		private DebuffAbility medallion;

		// Token: 0x04000184 RID: 388
		private NukeAbility meld;

		// Token: 0x04000185 RID: 389
		private Nullifier nullifier;

		// Token: 0x04000186 RID: 390
		private DisableAbility orchid;

		// Token: 0x04000187 RID: 391
		private HurricanePike pike;

		// Token: 0x04000188 RID: 392
		private BuffAbility refraction;

		// Token: 0x04000189 RID: 393
		private DebuffAbility solar;

		// Token: 0x0400018A RID: 394
		private DebuffAbility trap;
	}
}
