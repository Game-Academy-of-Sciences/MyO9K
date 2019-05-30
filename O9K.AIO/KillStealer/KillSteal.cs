using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ensage;
using Ensage.SDK.Handlers;
using Ensage.SDK.Helpers;
using O9K.AIO.Heroes.Base;
using O9K.AIO.KillStealer.Abilities.Base;
using O9K.AIO.Modes.Base;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Exceptions;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using SharpDX;

namespace O9K.AIO.KillStealer
{
	// Token: 0x0200002F RID: 47
	internal class KillSteal : BaseMode
	{
		// Token: 0x06000102 RID: 258 RVA: 0x0000AC40 File Offset: 0x00008E40
		public KillSteal(BaseHero baseHero) : base(baseHero)
		{
			foreach (Type type in from x in Assembly.GetExecutingAssembly().GetTypes()
			where !x.IsAbstract && x.IsClass && typeof(KillStealAbility).IsAssignableFrom(x)
			select x)
			{
				foreach (AbilityIdAttribute abilityIdAttribute in type.GetCustomAttributes<AbilityIdAttribute>())
				{
					this.abilityTypes.Add(abilityIdAttribute.AbilityId, type);
				}
			}
			this.KillStealMenu = new KillStealMenu(baseHero.Menu.RootMenu);
			this.orbwalkSleeper = baseHero.OrbwalkSleeper;
			EntityManager9.AbilityAdded += new EntityManager9.EventHandler<Ability9>(this.OnAbilityAdded);
			EntityManager9.AbilityRemoved += new EntityManager9.EventHandler<Ability9>(this.OnAbilityRemoved);
			this.damageHandler = UpdateManager.Subscribe(new Action(this.OnUpdateDamage), 200, false);
			this.killStealHandler = UpdateManager.Subscribe(new Action(this.OnUpdateKillSteal), 0, false);
			this.KillStealMenu.OverlayX.ValueChange += this.OverlayXOnValueChanged;
			this.KillStealMenu.OverlayY.ValueChange += this.OverlayYOnValueChanged;
			this.KillStealMenu.OverlaySizeX.ValueChange += this.OverlaySizeXOnValueChanged;
			this.KillStealMenu.OverlaySizeY.ValueChange += this.OverlaySizeYOnValueChanged;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00002B85 File Offset: 0x00000D85
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00002B8D File Offset: 0x00000D8D
		public Vector2 AdditionalOverlayPosition { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00002B96 File Offset: 0x00000D96
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00002B9E File Offset: 0x00000D9E
		public Vector2 AdditionalOverlaySize { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00002BA7 File Offset: 0x00000DA7
		public Sleeper KillStealSleeper { get; } = new Sleeper();

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00002BAF File Offset: 0x00000DAF
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00002BB7 File Offset: 0x00000DB7
		public Unit9 Target { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public MultiSleeper AbilitySleeper { get; } = new MultiSleeper();

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public KillStealMenu KillStealMenu { get; }

		// Token: 0x0600010C RID: 268 RVA: 0x0000AE94 File Offset: 0x00009094
		public void Disable()
		{
			this.KillStealMenu.KillStealEnabled.ValueChange -= this.KillStealEnabledOnValueChanged;
			this.KillStealMenu.OverlayEnabled.ValueChange -= this.OverlayEnabledOnValueChanged;
			this.KillStealEnabledOnValueChanged(null, new SwitcherEventArgs(false, false));
			this.OverlayEnabledOnValueChanged(null, new SwitcherEventArgs(false, false));
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000AEF8 File Offset: 0x000090F8
		public override void Dispose()
		{
			Drawing.OnDraw -= this.OnDraw;
			UpdateManager.Unsubscribe(this.damageHandler);
			UpdateManager.Unsubscribe(new Action(this.OnUpdateKillSteal));
			EntityManager9.AbilityAdded -= new EntityManager9.EventHandler<Ability9>(this.OnAbilityAdded);
			EntityManager9.AbilityRemoved -= new EntityManager9.EventHandler<Ability9>(this.OnAbilityRemoved);
			this.KillStealMenu.KillStealEnabled.ValueChange -= this.KillStealEnabledOnValueChanged;
			this.KillStealMenu.OverlayEnabled.ValueChange -= this.OverlayEnabledOnValueChanged;
			this.KillStealMenu.OverlayX.ValueChange -= this.OverlayXOnValueChanged;
			this.KillStealMenu.OverlayY.ValueChange -= this.OverlayYOnValueChanged;
			this.KillStealMenu.OverlaySizeX.ValueChange -= this.OverlaySizeXOnValueChanged;
			this.KillStealMenu.OverlaySizeY.ValueChange -= this.OverlaySizeYOnValueChanged;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public void Enable()
		{
			this.KillStealMenu.KillStealEnabled.ValueChange += this.KillStealEnabledOnValueChanged;
			this.KillStealMenu.OverlayEnabled.ValueChange += this.OverlayEnabledOnValueChanged;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000AFFC File Offset: 0x000091FC
		private void AddAbility(ActiveAbility ability)
		{
			Type type;
			if (this.abilityTypes.TryGetValue(ability.Id, out type))
			{
				this.activeAbilities.Add((KillStealAbility)Activator.CreateInstance(type, new object[]
				{
					ability
				}));
			}
			else
			{
				this.activeAbilities.Add(new KillStealAbility(ability));
			}
			this.activeAbilities = (from x in this.activeAbilities
			orderby x.Ability is IHasDamageAmplify && !(x.Ability is INuke) descending, x.Ability is IHasDamageAmplify && x.Ability is INuke descending, this.highPriorityKillSteal.Contains(x.Ability.Id) descending, x.Ability.CastPoint
			select x).ToList<KillStealAbility>();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000B0E0 File Offset: 0x000092E0
		private Dictionary<KillStealAbility, int> GetDamage(Unit9 target)
		{
			List<KillStealAbility> list = new List<KillStealAbility>();
			DamageAmplifier damageAmplifier = new DamageAmplifier();
			Dictionary<IHasDamageBlock, float> dictionary = target.GetDamageBlockers().ToDictionary((IHasDamageBlock x) => x, (IHasDamageBlock x) => x.BlockValue(target));
			IEnumerable<KillStealAbility> source = this.activeAbilities;
			Func<KillStealAbility, bool> <>9__2;
			Func<KillStealAbility, bool> predicate;
			if ((predicate = <>9__2) == null)
			{
				predicate = (<>9__2 = ((KillStealAbility x) => this.KillStealMenu.IsAbilityEnabled(x.Ability.Name)));
			}
			foreach (KillStealAbility killStealAbility in source.Where(predicate))
			{
				if (killStealAbility.Ability.IsValid && (this.AbilitySleeper.IsSleeping(killStealAbility.Ability.Handle) || killStealAbility.Ability.CanBeCasted(false)))
				{
					IHasDamageAmplify hasDamageAmplify;
					if ((hasDamageAmplify = (killStealAbility.Ability as IHasDamageAmplify)) != null && ((AbilityExtensions.IsIncomingDamageAmplifier(hasDamageAmplify) && !target.HasModifier(hasDamageAmplify.AmplifierModifierName)) || (AbilityExtensions.IsOutgoingDamageAmplifier(hasDamageAmplify) && !killStealAbility.Ability.Owner.HasModifier(hasDamageAmplify.AmplifierModifierName))))
					{
						if (AbilityExtensions.IsPhysicalDamageAmplifier(hasDamageAmplify))
						{
							DamageAmplifier damageAmplifier2 = damageAmplifier;
							damageAmplifier2[DamageType.Physical] = damageAmplifier2[DamageType.Physical] * (1f + hasDamageAmplify.AmplifierValue(killStealAbility.Ability.Owner, target));
						}
						if (AbilityExtensions.IsMagicalDamageAmplifier(hasDamageAmplify))
						{
							DamageAmplifier damageAmplifier2 = damageAmplifier;
							damageAmplifier2[DamageType.Magical] = damageAmplifier2[DamageType.Magical] * (1f + hasDamageAmplify.AmplifierValue(killStealAbility.Ability.Owner, target));
						}
						if (AbilityExtensions.IsPureDamageAmplifier(hasDamageAmplify))
						{
							DamageAmplifier damageAmplifier2 = damageAmplifier;
							damageAmplifier2[DamageType.Pure] = damageAmplifier2[DamageType.Pure] * (1f + hasDamageAmplify.AmplifierValue(killStealAbility.Ability.Owner, target));
						}
					}
					list.Add(killStealAbility);
				}
			}
			float num = target.Health;
			Dictionary<KillStealAbility, int> dictionary2 = new Dictionary<KillStealAbility, int>();
			Dictionary<Unit9, float> dictionary3 = new Dictionary<Unit9, float>();
			using (List<KillStealAbility>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KillStealAbility ability = enumerator2.Current;
					float num2;
					if (!dictionary3.TryGetValue(ability.Ability.Owner, out num2))
					{
						num2 = (dictionary3[ability.Ability.Owner] = ability.Ability.Owner.Mana);
					}
					if (num2 >= ability.Ability.ManaCost)
					{
						Predicate<KillStealAbility> <>9__3;
						foreach (KeyValuePair<DamageType, float> keyValuePair in ability.Ability.GetRawDamage(target, new float?(num)))
						{
							DamageType key = keyValuePair.Key;
							float num3 = keyValuePair.Value;
							if (key == DamageType.None)
							{
								if (num3 > 0f)
								{
									Logger.Error(new BrokenAbilityException("DamageType"), ability.Ability.Name);
									List<KillStealAbility> list2 = this.activeAbilities;
									Predicate<KillStealAbility> match;
									if ((match = <>9__3) == null)
									{
										match = (<>9__3 = ((KillStealAbility x) => x.Ability.Handle == ability.Ability.Handle));
									}
									list2.RemoveAll(match);
								}
								dictionary2.Add(ability, 0);
							}
							else if (key == DamageType.HealthRemoval)
							{
								int num4;
								dictionary2.TryGetValue(ability, out num4);
								dictionary2[ability] = num4 + (int)num3;
								num -= num3;
							}
							else
							{
								float damageAmplification = target.GetDamageAmplification(ability.Ability.Owner, key, ability.Ability.IntelligenceAmplify);
								if (damageAmplification > 0f)
								{
									foreach (KeyValuePair<IHasDamageBlock, float> keyValuePair2 in dictionary.ToList<KeyValuePair<IHasDamageBlock, float>>())
									{
										IHasDamageBlock key2 = keyValuePair2.Key;
										if (AbilityExtensions.CanBlock(key2, key))
										{
											float num5 = Math.Min(num3, keyValuePair2.Value);
											Dictionary<IHasDamageBlock, float> dictionary4 = dictionary;
											IHasDamageBlock key3 = key2;
											dictionary4[key3] -= num5;
											if (key2.BlocksDamageAfterReduction)
											{
												num3 -= Math.Min(num3, num5 * (1f / (damageAmplification * damageAmplifier[key])));
											}
											else
											{
												num3 -= num5;
											}
										}
									}
									int num6 = (int)(num3 * damageAmplification * damageAmplifier[key]);
									if (num6 > 6000 || num6 < 0)
									{
										BrokenAbilityException ex = new BrokenAbilityException("Amplified");
										ex.Data["Ability"] = new
										{
											Ability = ability.Ability.Name,
											Target = target.Name,
											Damage = num3.ToString(),
											AmplifiedDamage = num6.ToString(),
											Type = key.ToString(),
											Amp = damageAmplification.ToString(),
											Amp2 = damageAmplifier[key].ToString(),
											TargetAmps = (from x in target.amplifiers
											where x.IsValid && AbilityExtensions.IsIncomingDamageAmplifier(x)
											select x.AmplifierModifierName).ToArray<string>(),
											OwnerAmps = (from x in ability.Ability.Owner.amplifiers
											where x.IsValid && AbilityExtensions.IsOutgoingDamageAmplifier(x)
											select x.AmplifierModifierName).ToArray<string>()
										};
										Logger.Error(ex, null);
									}
									else
									{
										Dictionary<Unit9, float> dictionary5 = dictionary3;
										Unit9 owner = ability.Ability.Owner;
										dictionary5[owner] -= ability.Ability.ManaCost;
										int num7;
										dictionary2.TryGetValue(ability, out num7);
										dictionary2[ability] = num7 + num6;
										num -= (float)num6;
									}
								}
							}
						}
					}
				}
			}
			return dictionary2;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000B7C0 File Offset: 0x000099C0
		private void Kill(Unit9 target, IReadOnlyList<KillStealAbility> abilities)
		{
			if (this.KillStealSleeper.IsSleeping)
			{
				return;
			}
			Func<KillStealAbility, int> <>9__0;
			Func<KillStealAbility, int> keySelector;
			if ((keySelector = <>9__0) == null)
			{
				keySelector = (<>9__0 = ((KillStealAbility x) => x.Ability.GetDamage(target)));
			}
			foreach (KillStealAbility killStealAbility in abilities.OrderBy(keySelector))
			{
				float hitTime = killStealAbility.Ability.GetHitTime(target);
				if (target.Health + target.HealthRegeneration * hitTime * 1.5f <= (float)killStealAbility.Ability.GetDamage(target) && killStealAbility.Ability.UseAbility(target, EntityManager9.EnemyHeroes, 2, 0, false, false))
				{
					float castDelay = killStealAbility.Ability.GetCastDelay(target);
					this.AbilitySleeper.Sleep(killStealAbility.Ability.Handle, hitTime);
					this.orbwalkSleeper.Sleep(killStealAbility.Ability.Owner.Handle, castDelay);
					this.KillStealSleeper.Sleep(hitTime - killStealAbility.Ability.ActivationDelay);
					return;
				}
			}
			for (int i = 0; i < abilities.Count; i++)
			{
				KillStealAbility killStealAbility2 = abilities[i];
				if (this.orbwalkSleeper.IsSleeping(killStealAbility2.Ability.Owner.Handle))
				{
					return;
				}
				if (killStealAbility2.Ability.CanBeCasted(true))
				{
					IHasDamageAmplify hasDamageAmplify = killStealAbility2.Ability as IHasDamageAmplify;
					bool flag = hasDamageAmplify != null && AbilityExtensions.IsIncomingDamageAmplifier(hasDamageAmplify) && target.HasModifier(hasDamageAmplify.AmplifierModifierName);
					if (!flag || killStealAbility2.Ability is INuke)
					{
						if (!killStealAbility2.Ability.UseAbility(target, EntityManager9.EnemyHeroes, 2, 0, false, false))
						{
							return;
						}
						float num;
						if (!flag)
						{
							KillStealAbility killStealAbility3 = (i < abilities.Count - 1) ? abilities[i + 1] : null;
							if (killStealAbility3 != null)
							{
								float hitTime2 = killStealAbility3.Ability.GetHitTime(target);
								num = killStealAbility2.Ability.GetHitTime(target) - hitTime2;
							}
							else
							{
								num = killStealAbility2.Ability.GetHitTime(target);
							}
						}
						else
						{
							num = killStealAbility2.Ability.GetCastDelay(target);
						}
						this.AbilitySleeper.Sleep(killStealAbility2.Ability.Handle, killStealAbility2.Ability.GetHitTime(target));
						this.orbwalkSleeper.Sleep(killStealAbility2.Ability.Owner.Handle, killStealAbility2.Ability.GetCastDelay(target));
						this.KillStealSleeper.Sleep(num - killStealAbility2.Ability.ActivationDelay);
						return;
					}
				}
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000BABC File Offset: 0x00009CBC
		private void KillStealEnabledOnValueChanged(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				this.killStealHandler.IsEnabled = true;
				this.damageHandler.IsEnabled = true;
				return;
			}
			this.killStealHandler.IsEnabled = false;
			if (!this.KillStealMenu.OverlayEnabled)
			{
				this.damageHandler.IsEnabled = false;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000BB14 File Offset: 0x00009D14
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.IsControllable && ability.Owner.CanUseAbilities && ability.Owner.IsAlly(base.Owner))
				{
					ActiveAbility activeAbility;
					if (!this.ignoredAmplifiers.Contains(ability.Id) && (activeAbility = (ability as ActiveAbility)) != null)
					{
						IHasDamageAmplify hasDamageAmplify;
						if ((hasDamageAmplify = (ability as IHasDamageAmplify)) != null && ((AbilityExtensions.IsIncomingDamageAmplifier(hasDamageAmplify) && activeAbility.TargetsEnemy) || (AbilityExtensions.IsOutgoingDamageAmplifier(hasDamageAmplify) && activeAbility.TargetsAlly)))
						{
							this.AddAbility(activeAbility);
							this.KillStealMenu.AddKillStealAbility(activeAbility);
						}
						else if (ability is INuke)
						{
							this.AddAbility(activeAbility);
							this.KillStealMenu.AddKillStealAbility(activeAbility);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000BBE8 File Offset: 0x00009DE8
		private void OnAbilityRemoved(Ability9 ability)
		{
			try
			{
				if (ability.IsControllable && ability.Owner.CanUseAbilities && ability.Owner.IsAlly(base.Owner))
				{
					if (ability is ActiveAbility)
					{
						this.activeAbilities.RemoveAll((KillStealAbility x) => x.Ability.Handle == ability.Handle);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000BC84 File Offset: 0x00009E84
		private void OnDraw(EventArgs args)
		{
			try
			{
				foreach (KeyValuePair<Unit9, Dictionary<KillStealAbility, int>> keyValuePair in this.targetDamage)
				{
					int num = keyValuePair.Value.Sum((KeyValuePair<KillStealAbility, int> x) => x.Value);
					if (num > 0)
					{
						Unit9 key = keyValuePair.Key;
						if (key.IsValid && key.IsAlive && !key.IsInvulnerable && key.IsVisible)
						{
							Vector2 healthBarPosition = key.HealthBarPosition;
							if (!healthBarPosition.IsZero)
							{
								float health = key.Health;
								float num2 = Math.Min((float)num, health) / key.MaximumHealth;
								Vector2 healthBarSize = key.HealthBarSize;
								Vector2 vector = healthBarPosition + new Vector2(0f, healthBarSize.Y * 0.7f) + this.AdditionalOverlayPosition;
								Vector2 vector2 = healthBarSize * new Vector2(num2, 0.4f) + this.AdditionalOverlaySize;
								Drawing.DrawRect(vector, vector2, ((float)num >= health) ? Color.LightGreen : Color.DarkOliveGreen);
								Drawing.DrawRect(vector - new Vector2(1f), vector2 + new Vector2(1f), Color.Black, true);
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

		// Token: 0x06000116 RID: 278 RVA: 0x0000BE30 File Offset: 0x0000A030
		private void OnUpdateDamage()
		{
			if (Game.IsPaused)
			{
				return;
			}
			try
			{
				this.targetDamage = EntityManager9.EnemyHeroes.ToDictionary((Unit9 x) => x, new Func<Unit9, Dictionary<KillStealAbility, int>>(this.GetDamage));
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000BE9C File Offset: 0x0000A09C
		private void OnUpdateKillSteal()
		{
			if (Game.IsPaused)
			{
				return;
			}
			try
			{
				foreach (KeyValuePair<Unit9, Dictionary<KillStealAbility, int>> keyValuePair in this.targetDamage)
				{
					Unit9 key = keyValuePair.Key;
					float num = 0f;
					if (key.IsValid && key.IsVisible && key.IsAlive && !key.IsReflectingDamage && !key.IsInvulnerable)
					{
						List<KillStealAbility> list = new List<KillStealAbility>();
						foreach (KeyValuePair<KillStealAbility, int> keyValuePair2 in keyValuePair.Value)
						{
							KillStealAbility key2 = keyValuePair2.Key;
							if (key2.Ability.IsValid)
							{
								int value = keyValuePair2.Value;
								if ((this.AbilitySleeper.IsSleeping(key2.Ability.Handle) || key2.Ability.CanBeCasted(true)) && key2.CanHit(key) && key2.ShouldCast(key) && (!key2.Ability.UnitTargetCast || !key.IsBlockingAbilities))
								{
									list.Add(key2);
									num += (float)value - key2.Ability.GetHitTime(key) * key.HealthRegeneration * 1.5f;
									if (num >= key.Health && (!base.TargetManager.TargetLocked || key.Equals(base.TargetManager.Target)))
									{
										this.Target = key;
										this.Kill(key, (from x in list
										where x.Ability.CanBeCasted(true)
										select x).ToList<KillStealAbility>());
										return;
									}
								}
							}
						}
					}
				}
				if (!this.KillStealSleeper.IsSleeping)
				{
					this.Target = null;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		private void OverlayEnabledOnValueChanged(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				this.damageHandler.IsEnabled = true;
				Drawing.OnDraw += this.OnDraw;
				return;
			}
			Drawing.OnDraw -= this.OnDraw;
			if (!this.KillStealMenu.KillStealEnabled)
			{
				this.damageHandler.IsEnabled = false;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00002C0A File Offset: 0x00000E0A
		private void OverlaySizeXOnValueChanged(object sender, SliderEventArgs e)
		{
			this.AdditionalOverlaySize = new Vector2((float)e.NewValue, this.AdditionalOverlaySize.Y);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00002C29 File Offset: 0x00000E29
		private void OverlaySizeYOnValueChanged(object sender, SliderEventArgs e)
		{
			this.AdditionalOverlaySize = new Vector2(this.AdditionalOverlaySize.X, (float)e.NewValue);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00002C48 File Offset: 0x00000E48
		private void OverlayXOnValueChanged(object sender, SliderEventArgs e)
		{
			this.AdditionalOverlayPosition = new Vector2((float)e.NewValue, this.AdditionalOverlayPosition.Y);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00002C67 File Offset: 0x00000E67
		private void OverlayYOnValueChanged(object sender, SliderEventArgs e)
		{
			this.AdditionalOverlayPosition = new Vector2(this.AdditionalOverlayPosition.X, (float)e.NewValue);
		}

		// Token: 0x04000093 RID: 147
		private readonly Dictionary<AbilityId, Type> abilityTypes = new Dictionary<AbilityId, Type>();

		// Token: 0x04000094 RID: 148
		private readonly IUpdateHandler damageHandler;

		// Token: 0x04000095 RID: 149
		private readonly HashSet<AbilityId> highPriorityKillSteal = new HashSet<AbilityId>
		{
			AbilityId.morphling_adaptive_strike_agi,
			AbilityId.tusk_walrus_punch
		};

		// Token: 0x04000096 RID: 150
		private readonly HashSet<AbilityId> ignoredAmplifiers = new HashSet<AbilityId>
		{
			AbilityId.winter_wyvern_winters_curse,
			AbilityId.centaur_stampede,
			AbilityId.kunkka_ghostship,
			AbilityId.legion_commander_duel,
			AbilityId.medusa_stone_gaze,
			AbilityId.nyx_assassin_spiked_carapace
		};

		// Token: 0x04000097 RID: 151
		private readonly IUpdateHandler killStealHandler;

		// Token: 0x04000098 RID: 152
		private readonly MultiSleeper orbwalkSleeper;

		// Token: 0x04000099 RID: 153
		private List<KillStealAbility> activeAbilities = new List<KillStealAbility>();

		// Token: 0x0400009A RID: 154
		private Dictionary<Unit9, Dictionary<KillStealAbility, int>> targetDamage = new Dictionary<Unit9, Dictionary<KillStealAbility, int>>();
	}
}
