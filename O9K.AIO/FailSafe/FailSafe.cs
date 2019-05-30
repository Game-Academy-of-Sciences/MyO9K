using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using Ensage.SDK.Handlers;
using Ensage.SDK.Helpers;
using O9K.AIO.Heroes.Base;
using O9K.AIO.KillStealer;
using O9K.AIO.Modes.Base;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.FailSafe
{
	// Token: 0x020001F5 RID: 501
	internal class FailSafe : BaseMode
	{
		// Token: 0x060009EE RID: 2542 RVA: 0x0002B2E0 File Offset: 0x000294E0
		public FailSafe(BaseHero baseHero) : base(baseHero)
		{
			this.killSteal = baseHero.KillSteal;
			this.AbilitySleeper = baseHero.AbilitySleeper;
			this.OrbwalkSleeper = baseHero.OrbwalkSleeper;
			this.menu = new FailSafeMenu(baseHero.Menu.GeneralSettingsMenu);
			this.failSafeHandler = UpdateManager.Subscribe(new Action(this.OnUpdate), 0, false);
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00006E04 File Offset: 0x00005004
		public Sleeper Sleeper { get; } = new Sleeper();

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00006E0C File Offset: 0x0000500C
		public MultiSleeper AbilitySleeper { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00006E14 File Offset: 0x00005014
		public MultiSleeper OrbwalkSleeper { get; }

		// Token: 0x060009F2 RID: 2546 RVA: 0x00006E1C File Offset: 0x0000501C
		public void Disable()
		{
			this.menu.FailSafeEnabled.ValueChange -= this.FailSafeEnabledOnValueChanged;
			this.FailSafeEnabledOnValueChanged(null, new SwitcherEventArgs(false, false));
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002B458 File Offset: 0x00029658
		public override void Dispose()
		{
			UpdateManager.Unsubscribe(this.failSafeHandler);
			Entity.OnBoolPropertyChange -= this.OnBoolPropertyChange;
			Player.OnExecuteOrder -= this.OnExecuteOrder;
			this.menu.FailSafeEnabled.ValueChange -= this.FailSafeEnabledOnValueChanged;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00006E48 File Offset: 0x00005048
		public void Enable()
		{
			this.menu.FailSafeEnabled.ValueChange += this.FailSafeEnabledOnValueChanged;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002B4B0 File Offset: 0x000296B0
		private void FailSafeEnabledOnValueChanged(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				Entity.OnBoolPropertyChange += this.OnBoolPropertyChange;
				Player.OnExecuteOrder += this.OnExecuteOrder;
				return;
			}
			this.failSafeHandler.IsEnabled = false;
			Entity.OnBoolPropertyChange -= this.OnBoolPropertyChange;
			Player.OnExecuteOrder -= this.OnExecuteOrder;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00006E66 File Offset: 0x00005066
		private bool IsIgnored(Ability9 ability)
		{
			return this.ignoredAbilities.Contains(ability.Id) || ability is IBlink;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002B518 File Offset: 0x00029718
		private void OnBoolPropertyChange(Entity sender, BoolPropertyChangeEventArgs args)
		{
			try
			{
				if (args.NewValue != args.OldValue && !(args.PropertyName != "m_bInAbilityPhase"))
				{
					ActiveAbility ability = EntityManager9.GetAbility(sender.Handle) as ActiveAbility;
					ActiveAbility ability2 = ability;
					if (ability2 != null && ability2.IsControllable)
					{
						if (!this.IsIgnored(ability))
						{
							if (ability is AreaOfEffectAbility || ability is PredictionAbility)
							{
								if (args.NewValue)
								{
									if (ability is AreaOfEffectAbility)
									{
										if (ability.CastRange > 0f)
										{
											UpdateManager.BeginInvoke(delegate
											{
												this.abilityPositions[ability.Handle] = ability.Owner.InFront(ability.CastRange, 0f, true);
											}, 10);
										}
										else
										{
											UpdateManager.BeginInvoke(delegate
											{
												this.abilityPositions[ability.Handle] = ability.Owner.Position;
											}, 10);
										}
									}
									this.abilityTimings[ability] = Game.RawGameTime + ability.CastPoint;
									this.failSafeHandler.IsEnabled = true;
								}
								else
								{
									this.abilityTimings.Remove(ability);
									this.abilityPositions.Remove(ability.Handle);
									if (this.abilityTimings.Count <= 0)
									{
										this.failSafeHandler.IsEnabled = false;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002B6A8 File Offset: 0x000298A8
		private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
		{
			try
			{
				if (args.Process && !args.IsPlayerInput && args.OrderId == OrderId.AbilityLocation)
				{
					Ability9 ability = EntityManager9.GetAbility(args.Ability.Handle);
					if (!(ability == null))
					{
						if (!this.IsIgnored(ability))
						{
							this.abilityPositions[ability.Handle] = args.TargetPosition;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002B730 File Offset: 0x00029930
		private void OnUpdate()
		{
			try
			{
				if (!this.Sleeper.IsSleeping)
				{
					Unit9 unit = base.TargetManager.TargetLocked ? base.TargetManager.Target : this.killSteal.Target;
					if (unit != null && unit.IsValid && unit.IsVisible)
					{
						foreach (KeyValuePair<ActiveAbility, float> keyValuePair in this.abilityTimings.ToList<KeyValuePair<ActiveAbility, float>>())
						{
							ActiveAbility ability = keyValuePair.Key;
							if (ability.IsValid && ability.BaseAbility.IsInAbilityPhase)
							{
								Unit9 owner = ability.Owner;
								float value = keyValuePair.Value;
								PredictionInput9 predictionInput = ability.GetPredictionInput(unit, null);
								predictionInput.Delay = Math.Max(value - Game.RawGameTime, 0f) + ability.ActivationDelay;
								PredictionOutput9 predictionOutput = ability.GetPredictionOutput(predictionInput);
								Vector3 vector;
								if (ability is IHasRadius && this.abilityPositions.TryGetValue(ability.Handle, out vector))
								{
									Polygon polygon = null;
									ActiveAbility ability2 = ability;
									if (ability2 != null)
									{
										AreaOfEffectAbility areaOfEffectAbility;
										if ((areaOfEffectAbility = (ability2 as AreaOfEffectAbility)) == null)
										{
											CircleAbility circleAbility;
											if ((circleAbility = (ability2 as CircleAbility)) == null)
											{
												ConeAbility coneAbility;
												if ((coneAbility = (ability2 as ConeAbility)) == null)
												{
													LineAbility lineAbility;
													if ((lineAbility = (ability2 as LineAbility)) != null)
													{
														LineAbility lineAbility2 = lineAbility;
														polygon = new Polygon.Rectangle(owner.Position, Vector3Extensions.Extend2D(owner.Position, vector, lineAbility2.Range), lineAbility2.Radius + 50f);
													}
												}
												else
												{
													ConeAbility coneAbility2 = coneAbility;
													polygon = new Polygon.Trapezoid(Vector3Extensions.Extend2D(owner.Position, vector, -coneAbility2.Radius / 2f), Vector3Extensions.Extend2D(owner.Position, vector, coneAbility2.Range), coneAbility2.Radius + 50f, coneAbility2.EndRadius + 100f);
												}
											}
											else
											{
												CircleAbility circleAbility2 = circleAbility;
												polygon = new Polygon.Circle(vector, circleAbility2.Radius + 50f, 20);
											}
										}
										else
										{
											AreaOfEffectAbility areaOfEffectAbility2 = areaOfEffectAbility;
											polygon = new Polygon.Circle(vector, areaOfEffectAbility2.Radius + 50f, 20);
										}
									}
									if (polygon != null && (!unit.IsAlive || predictionOutput.HitChance == null || !polygon.IsInside(predictionOutput.TargetPosition)))
									{
										this.Sleeper.Sleep(0.15f);
										this.abilityTimings.Remove(ability);
										this.abilityPositions.Remove(ability.Handle);
										this.OrbwalkSleeper.Reset(ability.Owner.Handle);
										this.AbilitySleeper.Reset(ability.Handle);
										this.killSteal.KillStealSleeper.Reset();
										unit.RefreshUnitState();
										ability.Owner.BaseUnit.Stop();
										ControllableUnit controllableUnit = base.BaseHero.UnitManager.AllControllableUnits.FirstOrDefault((ControllableUnit x) => x.Handle == ability.Owner.Handle);
										if (controllableUnit != null)
										{
											controllableUnit.ComboSleeper.Reset();
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x0400053B RID: 1339
		private readonly Dictionary<uint, Vector3> abilityPositions = new Dictionary<uint, Vector3>();

		// Token: 0x0400053C RID: 1340
		private readonly Dictionary<ActiveAbility, float> abilityTimings = new Dictionary<ActiveAbility, float>();

		// Token: 0x0400053D RID: 1341
		private readonly IUpdateHandler failSafeHandler;

		// Token: 0x0400053E RID: 1342
		private readonly HashSet<AbilityId> ignoredAbilities = new HashSet<AbilityId>
		{
			AbilityId.arc_warden_magnetic_field,
			AbilityId.storm_spirit_ball_lightning,
			AbilityId.leshrac_diabolic_edict,
			AbilityId.shredder_timber_chain,
			AbilityId.magnataur_skewer,
			AbilityId.disruptor_kinetic_field,
			AbilityId.nevermore_requiem,
			AbilityId.disruptor_static_storm,
			AbilityId.crystal_maiden_freezing_field,
			AbilityId.skywrath_mage_mystic_flare,
			AbilityId.phoenix_icarus_dive,
			AbilityId.kunkka_torrent,
			AbilityId.kunkka_ghostship,
			AbilityId.elder_titan_echo_stomp_spirit,
			AbilityId.elder_titan_echo_stomp,
			AbilityId.bloodseeker_blood_bath,
			AbilityId.phantom_lancer_doppelwalk,
			AbilityId.mars_gods_rebuke,
			AbilityId.beastmaster_wild_axes
		};

		// Token: 0x0400053F RID: 1343
		private readonly KillSteal killSteal;

		// Token: 0x04000540 RID: 1344
		private readonly FailSafeMenu menu;
	}
}
