using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Pudge.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Items;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Pudge.Units
{
	// Token: 0x020000CA RID: 202
	[UnitName("npc_dota_hero_pudge")]
	internal class Pudge : ControllableUnit
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x000164D4 File Offset: 0x000146D4
		public Pudge(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.pudge_meat_hook,
					(ActiveAbility x) => this.hook = new MeatHook(x)
				},
				{
					AbilityId.pudge_rot,
					(ActiveAbility x) => this.rot = new Rot(x)
				},
				{
					AbilityId.pudge_dismember,
					(ActiveAbility x) => this.dismember = new Dismember(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkDaggerPudge(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_blade_mail,
					(ActiveAbility x) => this.bladeMail = new ShieldAbility(x)
				},
				{
					AbilityId.item_spirit_vessel,
					(ActiveAbility x) => this.vessel = new DebuffAbility(x)
				},
				{
					AbilityId.item_urn_of_shadows,
					(ActiveAbility x) => this.urn = new DebuffAbility(x)
				},
				{
					AbilityId.item_rod_of_atos,
					(ActiveAbility x) => this.atos = new DisableAbility(x)
				},
				{
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				},
				{
					AbilityId.item_ethereal_blade,
					(ActiveAbility x) => this.ethereal = new DebuffAbility(x)
				},
				{
					AbilityId.item_dagon_5,
					(ActiveAbility x) => this.dagon = new NukeAbility(x)
				},
				{
					AbilityId.item_nullifier,
					(ActiveAbility x) => this.nullifier = new Nullifier(x)
				},
				{
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.bloodthorn = new Bloodthorn(x)
				}
			};
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001662C File Offset: 0x0001482C
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.bloodthorn, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.nullifier, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.ethereal, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.dagon, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.shiva, true))
			{
				return true;
			}
			if (targetManager.Target.HasModifier("modifier_pudge_meat_hook"))
			{
				if (abilityHelper.CanBeCasted(this.bloodthorn, true, false, true, true))
				{
					abilityHelper.ForceUseAbility(this.bloodthorn, false, true);
					return true;
				}
				if (abilityHelper.CanBeCasted(this.nullifier, true, false, true, true))
				{
					if (!targetManager.Target.Abilities.Any((Ability9 x) => x.Id == AbilityId.item_aeon_disk && x.IsReady))
					{
						abilityHelper.ForceUseAbility(this.nullifier, false, true);
						return true;
					}
				}
				if (abilityHelper.CanBeCasted(this.ethereal, true, false, true, true))
				{
					abilityHelper.ForceUseAbility(this.ethereal, false, true);
					return true;
				}
				if (abilityHelper.UseAbility(this.dagon, true))
				{
					return true;
				}
				if (abilityHelper.UseAbility(this.vessel, true))
				{
					return true;
				}
				if (abilityHelper.UseAbility(this.urn, true))
				{
					return true;
				}
			}
			if (!abilityHelper.CanBeCasted(this.dismember, true, true, true, true))
			{
				return abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
				{
					this.dismember
				}) || abilityHelper.UseAbility(this.blink, 800f, 25f) || abilityHelper.UseAbility(this.atos, true) || (abilityHelper.CanBeCasted(this.force, true, true, true, true) && this.force.UseAbilityOnTarget(targetManager, base.ComboSleeper)) || abilityHelper.UseAbility(this.force, 400f, 800f) || abilityHelper.UseAbility(this.hook, true) || (abilityHelper.CanBeCasted(this.rot, false, false, true, true) && this.rot.AutoToggle(targetManager));
			}
			if (abilityHelper.CanBeCasted(this.bloodthorn, true, false, true, true))
			{
				abilityHelper.ForceUseAbility(this.bloodthorn, false, true);
				return true;
			}
			if (abilityHelper.CanBeCasted(this.nullifier, true, false, true, true))
			{
				if (!targetManager.Target.Abilities.Any((Ability9 x) => x.Id == AbilityId.item_aeon_disk && x.IsReady))
				{
					abilityHelper.ForceUseAbility(this.nullifier, false, true);
					return true;
				}
			}
			if (abilityHelper.CanBeCasted(this.ethereal, true, false, true, true))
			{
				abilityHelper.ForceUseAbility(this.ethereal, false, true);
				return true;
			}
			if (abilityHelper.UseAbility(this.dagon, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.bladeMail, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.vessel, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.urn, true))
			{
				return true;
			}
			abilityHelper.ForceUseAbility(this.dismember, false, true);
			return true;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00016940 File Offset: 0x00014B40
		public void HookAlly(TargetManager targetManager)
		{
			MeatHook meatHook = this.hook;
			if (meatHook == null || !meatHook.Ability.CanBeCasted(true))
			{
				return;
			}
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = this.hook.Ability.GetPredictionInput(target, null);
			PredictionOutput9 predictionOutput = this.hook.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return;
			}
			this.hook.Ability.UseAbility(predictionOutput.CastPosition, false, false);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000169BC File Offset: 0x00014BBC
		public void SoulRingSuicide(Dictionary<Unit9, float> attacks, Dictionary<TrackingProjectile, int> projectiles)
		{
			if (base.Owner.HealthPercentage > 30f)
			{
				return;
			}
			Rot rot = this.rot;
			if (rot == null || !rot.Ability.CanBeCasted(true))
			{
				return;
			}
			Vector3 other = (base.Owner.IsMoving && Math.Abs(base.Owner.BaseUnit.RotationDifference) < 60f) ? base.Owner.InFront(55f, 0f, true) : base.Owner.Position;
			bool flag = true;
			foreach (KeyValuePair<TrackingProjectile, int> keyValuePair in projectiles)
			{
				if (keyValuePair.Key.IsValid && keyValuePair.Key.Position.Distance2D(other, false) < 200f)
				{
					flag = false;
					break;
				}
			}
			bool flag2 = true;
			foreach (KeyValuePair<Unit9, float> keyValuePair2 in from x in attacks
			where x.Key.IsValid && x.Key.IsAlive && x.Key.Distance(base.Owner) <= x.Key.GetAttackRange(base.Owner, 200f) && (double)x.Key.GetAngle(base.Owner.Position, false) < 0.5 && (!x.Key.IsRanged || x.Key.Distance(base.Owner) < 400f)
			select x)
			{
				Unit9 key = keyValuePair2.Key;
				float value = keyValuePair2.Value;
				float attackPoint = key.GetAttackPoint(base.Owner);
				float secondsPerAttack = key.BaseUnit.SecondsPerAttack;
				float rawGameTime = Game.RawGameTime;
				float num = value + attackPoint;
				if (key.IsRanged)
				{
					num += Math.Max(key.Distance(base.Owner) - base.Owner.HullRadius, 0f) / (float)key.ProjectileSpeed;
				}
				Ability9 ability = key.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.item_echo_sabre);
				if ((rawGameTime <= num && ((double)attackPoint < 0.35 || (double)rawGameTime + (double)attackPoint * 0.6 > (double)num)) || ((double)attackPoint < 0.25 && (double)rawGameTime > (double)num + (double)key.GetAttackBackswing(base.Owner) * 0.6 && (double)rawGameTime <= (double)(value + secondsPerAttack) + 0.12) || (ability != null && !key.IsRanged && ability.Cooldown - ability.RemainingCooldown <= attackPoint * 2f))
				{
					flag2 = false;
					break;
				}
			}
			if (!flag || !flag2)
			{
				return;
			}
			SoulRing soulRing = base.Owner.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.item_soul_ring) as SoulRing;
			if (soulRing != null && soulRing.CanBeCasted(true))
			{
				if (base.Owner.Health > (float)soulRing.HealthCost)
				{
					return;
				}
				soulRing.UseAbility(false, false);
				if (!this.rot.IsEnabled)
				{
					this.rot.Ability.UseAbility(false, false);
					return;
				}
			}
			else
			{
				if (this.rot.IsEnabled)
				{
					return;
				}
				float num2 = (float)this.rot.Ability.GetDamage(base.Owner) * 0.5f;
				if (base.Owner.Health > num2)
				{
					return;
				}
				this.rot.Ability.UseAbility(false, false);
			}
		}

		// Token: 0x04000246 RID: 582
		private DisableAbility atos;

		// Token: 0x04000247 RID: 583
		private ShieldAbility bladeMail;

		// Token: 0x04000248 RID: 584
		private BlinkDaggerPudge blink;

		// Token: 0x04000249 RID: 585
		private Dismember dismember;

		// Token: 0x0400024A RID: 586
		private DebuffAbility ethereal;

		// Token: 0x0400024B RID: 587
		private ForceStaff force;

		// Token: 0x0400024C RID: 588
		private MeatHook hook;

		// Token: 0x0400024D RID: 589
		private Rot rot;

		// Token: 0x0400024E RID: 590
		private NukeAbility dagon;

		// Token: 0x0400024F RID: 591
		private DebuffAbility shiva;

		// Token: 0x04000250 RID: 592
		private DebuffAbility urn;

		// Token: 0x04000251 RID: 593
		private DisableAbility bloodthorn;

		// Token: 0x04000252 RID: 594
		private DebuffAbility vessel;

		// Token: 0x04000253 RID: 595
		private Nullifier nullifier;
	}
}
