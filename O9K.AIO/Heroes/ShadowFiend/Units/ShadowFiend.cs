using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.ShadowFiend.Units
{
	// Token: 0x020000B2 RID: 178
	[UnitName("npc_dota_hero_nevermore")]
	internal class ShadowFiend : ControllableUnit
	{
		// Token: 0x0600038B RID: 907 RVA: 0x0001410C File Offset: 0x0001230C
		public ShadowFiend(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.nevermore_shadowraze1,
					delegate(ActiveAbility x)
					{
						NukeAbility nukeAbility = new NukeAbility(x);
						this.razes.Add(nukeAbility);
						return nukeAbility;
					}
				},
				{
					AbilityId.nevermore_shadowraze2,
					delegate(ActiveAbility x)
					{
						NukeAbility nukeAbility = new NukeAbility(x);
						this.razes.Add(nukeAbility);
						return nukeAbility;
					}
				},
				{
					AbilityId.nevermore_shadowraze3,
					delegate(ActiveAbility x)
					{
						NukeAbility nukeAbility = new NukeAbility(x);
						this.razes.Add(nukeAbility);
						return nukeAbility;
					}
				},
				{
					AbilityId.nevermore_requiem,
					(ActiveAbility x) => this.requiem = new NukeAbility(x)
				},
				{
					AbilityId.item_veil_of_discord,
					(ActiveAbility x) => this.veil = new DebuffAbility(x)
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
					AbilityId.item_ethereal_blade,
					(ActiveAbility x) => this.ethereal = new EtherealBlade(x)
				},
				{
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				},
				{
					AbilityId.item_manta,
					(ActiveAbility x) => this.manta = new BuffAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_cyclone,
					(ActiveAbility x) => this.euls = new EulsScepterOfDivinity(x)
				},
				{
					AbilityId.item_hurricane_pike,
					(ActiveAbility x) => this.pike = new HurricanePike(x)
				},
				{
					AbilityId.item_black_king_bar,
					(ActiveAbility x) => this.bkb = new ShieldAbility(x)
				}
			};
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00014284 File Offset: 0x00012484
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			Unit9 target = targetManager.Target;
			if (this.UltCombo(targetManager, abilityHelper))
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
			if (abilityHelper.UseAbility(this.veil, true))
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
			if (abilityHelper.UseAbility(this.pike, 800f, 400f))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.pike, true, true, true, true) && !base.MoveSleeper.IsSleeping && this.pike.UseAbilityOnTarget(targetManager, base.ComboSleeper))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.manta, base.Owner.GetAttackRange(null, 0f)))
			{
				return true;
			}
			IEnumerable<NukeAbility> enumerable;
			if (target.GetAngle(base.Owner.Position, false) <= 1f && target.IsMoving)
			{
				enumerable = from x in this.razes
				orderby x.Ability.Id descending
				select x;
			}
			else
			{
				enumerable = from x in this.razes
				orderby x.Ability.Id
				select x;
			}
			foreach (NukeAbility nukeAbility in enumerable)
			{
				if (abilityHelper.CanBeCasted(nukeAbility, true, true, true, true) && !this.RazeCanWaitAttack(nukeAbility, target) && abilityHelper.UseAbility(nukeAbility, true))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00014458 File Offset: 0x00012658
		private bool RazeCanWaitAttack(UsableAbility raze, Unit9 target)
		{
			if ((float)raze.Ability.GetDamage(target) > target.Health)
			{
				return false;
			}
			PredictionInput9 predictionInput = raze.Ability.GetPredictionInput(target, null);
			if (base.MoveSleeper.IsSleeping)
			{
				predictionInput.Delay += base.MoveSleeper.RemainingSleepTime;
			}
			else
			{
				if (!target.IsMoving || !base.Owner.CanAttack(target, -50f))
				{
					return false;
				}
				predictionInput.Delay += base.Owner.GetAttackPoint(target) + base.Owner.GetTurnTime(target.Position);
			}
			return raze.Ability.GetPredictionOutput(predictionInput).HitChance >= 1;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00014510 File Offset: 0x00012710
		private bool UltCombo(TargetManager targetManager, AbilityHelper abilityHelper)
		{
			if (!abilityHelper.CanBeCasted(this.requiem, false, false, true, true))
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			Vector3 vector = target.Position;
			float num = base.Owner.Distance(vector);
			float num2 = this.requiem.Ability.CastPoint + Game.Ping / 2000f;
			if (target.IsInvulnerable)
			{
				float invulnerabilityDuration = target.GetInvulnerabilityDuration();
				if (invulnerabilityDuration <= 0f)
				{
					return true;
				}
				Modifier modifier = target.GetModifier("modifier_eul_cyclone");
				if (modifier != null)
				{
					ParticleEffect particleEffect = modifier.ParticleEffects.FirstOrDefault<ParticleEffect>();
					if (particleEffect != null)
					{
						vector = particleEffect.GetControlPoint(0u);
						num = base.Owner.Distance(vector);
					}
				}
				float num3 = invulnerabilityDuration - num2;
				if (num3 <= -0.3f)
				{
					return false;
				}
				if (num < 70f)
				{
					if (abilityHelper.UseAbility(this.bkb, true))
					{
						return true;
					}
					if (num3 <= 0f && abilityHelper.ForceUseAbility(this.requiem, false, true))
					{
						return true;
					}
					if (!base.OrbwalkSleeper.IsSleeping)
					{
						base.OrbwalkSleeper.Sleep(0.1f);
						base.Owner.BaseUnit.Move(vector);
					}
					return true;
				}
				else if (num / base.Owner.Speed < num3 + 0.3f)
				{
					if (abilityHelper.UseAbility(this.bkb, true))
					{
						return true;
					}
					base.OrbwalkSleeper.Sleep(0.1f);
					base.ComboSleeper.Sleep(0.1f);
					return base.Owner.BaseUnit.Move(vector);
				}
				else if (abilityHelper.CanBeCasted(this.blink, true, true, true, true) && this.blink.Ability.CastRange + base.Owner.Speed * num3 > num && abilityHelper.UseAbility(this.blink, vector))
				{
					base.OrbwalkSleeper.Sleep(0.1f);
					return true;
				}
			}
			if (!abilityHelper.CanBeCasted(this.euls, false, false, true, true) || !this.euls.ShouldForceCast(targetManager) || target.IsMagicImmune)
			{
				return false;
			}
			float num4 = this.euls.Ability.Duration - num2;
			if (abilityHelper.CanBeCasted(this.blink, true, true, true, true) && this.blink.Ability.CastRange + base.Owner.Speed * num4 > num && abilityHelper.UseAbility(this.blink, vector))
			{
				base.OrbwalkSleeper.Sleep(0.1f);
				base.ComboSleeper.ExtendSleep(0.1f);
				return true;
			}
			if (num / base.Owner.Speed < num4)
			{
				if (abilityHelper.CanBeCasted(this.hex, true, true, true, true))
				{
					bool flag = false;
					foreach (ActiveAbility activeAbility in target.Abilities.OfType<ActiveAbility>())
					{
						if (activeAbility.IsValid && activeAbility.CastPoint <= 0f && activeAbility.CanBeCasted(false))
						{
							if (!base.Owner.IsMagicImmune || !abilityHelper.CanBeCasted(this.bkb, true, true, true, true))
							{
								if (activeAbility.Id == AbilityId.item_blade_mail)
								{
									flag = true;
									break;
								}
								IDisable disable;
								if ((disable = (activeAbility as IDisable)) != null && (disable.AppliesUnitState & (UnitState.Silenced | UnitState.Stunned | UnitState.Hexed)) != (UnitState)0UL)
								{
									flag = true;
									break;
								}
							}
							IShield shield;
							if ((shield = (activeAbility as IShield)) != null && (shield.AppliesUnitState & (UnitState.Invulnerable | UnitState.MagicImmune)) != (UnitState)0UL)
							{
								flag = true;
								break;
							}
							IBlink blink;
							if ((blink = (activeAbility as IBlink)) != null && blink.Id != AbilityId.item_blink)
							{
								flag = true;
								break;
							}
						}
					}
					if (flag && abilityHelper.ForceUseAbility(this.hex, false, true))
					{
						base.ComboSleeper.ExtendSleep(0.1f);
						base.OrbwalkSleeper.ExtendSleep(0.1f);
						return true;
					}
				}
				if (abilityHelper.UseAbility(this.veil, true))
				{
					return true;
				}
				if (abilityHelper.UseAbility(this.ethereal, true))
				{
					base.OrbwalkSleeper.Sleep(0.5f);
					return true;
				}
				if (abilityHelper.ForceUseAbility(this.euls, false, true))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040001F3 RID: 499
		private readonly List<NukeAbility> razes = new List<NukeAbility>();

		// Token: 0x040001F4 RID: 500
		private ShieldAbility bkb;

		// Token: 0x040001F5 RID: 501
		private BlinkAbility blink;

		// Token: 0x040001F6 RID: 502
		private DisableAbility bloodthorn;

		// Token: 0x040001F7 RID: 503
		private EtherealBlade ethereal;

		// Token: 0x040001F8 RID: 504
		private EulsScepterOfDivinity euls;

		// Token: 0x040001F9 RID: 505
		private DisableAbility hex;

		// Token: 0x040001FA RID: 506
		private BuffAbility manta;

		// Token: 0x040001FB RID: 507
		private Nullifier nullifier;

		// Token: 0x040001FC RID: 508
		private DisableAbility orchid;

		// Token: 0x040001FD RID: 509
		private HurricanePike pike;

		// Token: 0x040001FE RID: 510
		private NukeAbility requiem;

		// Token: 0x040001FF RID: 511
		private DebuffAbility veil;
	}
}
