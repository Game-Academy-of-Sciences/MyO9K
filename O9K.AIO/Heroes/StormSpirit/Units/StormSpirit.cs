using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.StormSpirit.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.StormSpirit.Units
{
	// Token: 0x02000094 RID: 148
	[UnitName("npc_dota_hero_storm_spirit")]
	internal class StormSpirit : ControllableUnit
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x00011D0C File Offset: 0x0000FF0C
		public StormSpirit(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.storm_spirit_static_remnant,
					(ActiveAbility x) => this.remnant = new NukeAbility(x)
				},
				{
					AbilityId.storm_spirit_electric_vortex,
					(ActiveAbility x) => this.vortex = new DisableAbility(x)
				},
				{
					AbilityId.storm_spirit_ball_lightning,
					(ActiveAbility x) => this.ball = new BallLightning(x)
				},
				{
					AbilityId.item_orchid,
					(ActiveAbility x) => this.orchid = new DisableAbility(x)
				},
				{
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				},
				{
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.bloodthorn = new Bloodthorn(x)
				},
				{
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				},
				{
					AbilityId.item_nullifier,
					(ActiveAbility x) => this.nullifier = new Nullifier(x)
				},
				{
					AbilityId.item_dagon_5,
					(ActiveAbility x) => this.dagon = new NukeAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.storm_spirit_ball_lightning, (ActiveAbility _) => this.ball);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00011E14 File Offset: 0x00010014
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			Unit9 target = targetManager.Target;
			if (abilityHelper.UseAbility(this.hex, true))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.vortex, true, true, true, true) && (target.CanBecomeMagicImmune || target.CanBecomeInvisible) && abilityHelper.UseAbility(this.vortex, true))
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
			if (abilityHelper.UseAbility(this.dagon, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.shiva, true))
			{
				return true;
			}
			bool flag = base.Owner.CanAttack(target, 25f) && base.Owner.HasModifier("modifier_storm_spirit_overload");
			TrackingProjectile trackingProjectile = ObjectManager.TrackingProjectiles.FirstOrDefault(delegate(TrackingProjectile x)
			{
				Entity source = x.Source;
				EntityHandle? entityHandle = (source != null) ? new EntityHandle?(source.Handle) : null;
				if (((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == this.Handle)
				{
					Entity target = x.Target;
					entityHandle = ((target != null) ? new EntityHandle?(target.Handle) : null);
					if (((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == target.Handle)
					{
						return ProjectileExtensions.IsAutoAttackProjectile(x);
					}
				}
				return false;
			});
			if (flag)
			{
				if (trackingProjectile == null)
				{
					return false;
				}
				int num = (target.IsMoving && target.GetAngle(trackingProjectile.Position, false) > 1.5f) ? 250 : 450;
				if (trackingProjectile.Position.Distance2D(trackingProjectile.TargetPosition, false) > (float)num)
				{
					return false;
				}
			}
			else
			{
				if (trackingProjectile != null)
				{
					Ability9 ability = base.Owner.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.storm_spirit_overload);
					if (ability != null)
					{
						int attackDamage = base.Owner.GetAttackDamage(target, 0, 0f);
						int damage = ability.GetDamage(target);
						float health = target.Health;
						if ((float)attackDamage < health && (float)(attackDamage + damage) > health)
						{
							if (abilityHelper.CanBeCasted(this.remnant, false, false, true, true) && abilityHelper.ForceUseAbility(this.remnant, true, true))
							{
								return true;
							}
							if (abilityHelper.CanBeCasted(this.ball, false, false, true, true) && trackingProjectile.Position.Distance2D(trackingProjectile.TargetPosition, false) / (float)trackingProjectile.Speed > this.ball.Ability.CastPoint && abilityHelper.ForceUseAbility(this.ball, true, true))
							{
								return true;
							}
						}
					}
				}
				if (abilityHelper.UseAbility(this.vortex, true))
				{
					base.ComboSleeper.ExtendSleep(0.1f);
					BallLightning ballLightning = this.ball;
					if (ballLightning != null)
					{
						ballLightning.Sleeper.Sleep(1f);
					}
					return true;
				}
				if (abilityHelper.UseAbility(this.remnant, true))
				{
					base.ComboSleeper.ExtendSleep(0.1f);
					BallLightning ballLightning2 = this.ball;
					if (ballLightning2 != null)
					{
						ballLightning2.Sleeper.Sleep(1f);
					}
					return true;
				}
			}
			return abilityHelper.UseAbility(this.ball, true);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00003D2A File Offset: 0x00001F2A
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBlinks(abilityHelper) || abilityHelper.UseMoveAbility(this.ball);
		}

		// Token: 0x04000197 RID: 407
		private BallLightning ball;

		// Token: 0x04000198 RID: 408
		private DisableAbility bloodthorn;

		// Token: 0x04000199 RID: 409
		private DisableAbility hex;

		// Token: 0x0400019A RID: 410
		private Nullifier nullifier;

		// Token: 0x0400019B RID: 411
		private DisableAbility orchid;

		// Token: 0x0400019C RID: 412
		private NukeAbility remnant;

		// Token: 0x0400019D RID: 413
		private DebuffAbility shiva;

		// Token: 0x0400019E RID: 414
		private DisableAbility vortex;

		// Token: 0x0400019F RID: 415
		private NukeAbility dagon;
	}
}
