using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Heroes.Unique;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using O9K.Core.Helpers.Range;
using O9K.Core.Logger;

namespace O9K.Core.Managers.Entity.Monitors
{
	// Token: 0x02000073 RID: 115
	public sealed class UnitMonitor : IDisposable
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x0001C404 File Offset: 0x0001A604
		public UnitMonitor()
		{
			Dictionary<string, Action<Unit9, bool>> dictionary = new Dictionary<string, Action<Unit9, bool>>();
			dictionary.Add("modifier_teleporting", delegate(Unit9 x, bool value)
			{
				x.IsTeleporting = value;
			});
			dictionary.Add("modifier_treant_natures_guise_invis", delegate(Unit9 x, bool value)
			{
				x.CanUseAbilitiesInInvisibility = value;
			});
			dictionary.Add("modifier_riki_permanent_invisibility", delegate(Unit9 x, bool value)
			{
				x.CanUseAbilitiesInInvisibility = value;
			});
			dictionary.Add("modifier_ice_blast", delegate(Unit9 x, bool value)
			{
				x.CanBeHealed = !value;
			});
			dictionary.Add("modifier_item_aegis", delegate(Unit9 x, bool value)
			{
				x.HasAegis = value;
			});
			dictionary.Add("modifier_necrolyte_sadist_active", delegate(Unit9 x, bool value)
			{
				x.IsEthereal = value;
			});
			dictionary.Add("modifier_pugna_decrepify", delegate(Unit9 x, bool value)
			{
				x.IsEthereal = value;
			});
			dictionary.Add("modifier_item_ethereal_blade_ethereal", delegate(Unit9 x, bool value)
			{
				x.IsEthereal = value;
			});
			dictionary.Add("modifier_ghost_state", delegate(Unit9 x, bool value)
			{
				x.IsEthereal = value;
			});
			dictionary.Add("modifier_item_lotus_orb_active", delegate(Unit9 x, bool value)
			{
				x.IsLotusProtected = value;
			});
			dictionary.Add("modifier_antimage_counterspell", delegate(Unit9 x, bool value)
			{
				x.IsSpellShieldProtected = value;
			});
			dictionary.Add("modifier_item_sphere_target", delegate(Unit9 x, bool value)
			{
				x.IsLinkensTargetProtected = value;
			});
			dictionary.Add("modifier_item_blade_mail_reflect", delegate(Unit9 x, bool value)
			{
				x.IsReflectingDamage = value;
			});
			dictionary.Add("modifier_item_ultimate_scepter", delegate(Unit9 x, bool value)
			{
				x.HasAghanimsScepter = value;
			});
			dictionary.Add("modifier_item_ultimate_scepter_consumed", delegate(Unit9 x, bool value)
			{
				x.HasAghanimsScepter = value;
			});
			dictionary.Add("modifier_wisp_tether_scepter", delegate(Unit9 x, bool value)
			{
				x.HasAghanimsScepter = value;
			});
			dictionary.Add("modifier_slark_dark_pact", delegate(Unit9 x, bool value)
			{
				x.IsDarkPactProtected = value;
			});
			dictionary.Add("modifier_bloodseeker_rupture", delegate(Unit9 x, bool value)
			{
				x.IsRuptured = value;
			});
			dictionary.Add("modifier_spirit_breaker_charge_of_darkness", delegate(Unit9 x, bool value)
			{
				x.IsCharging = value;
			});
			dictionary.Add("modifier_dragon_knight_dragon_form", delegate(Unit9 x, bool value)
			{
				x.IsRanged = (value || x.BaseUnit.IsRanged);
			});
			dictionary.Add("modifier_terrorblade_metamorphosis", delegate(Unit9 x, bool value)
			{
				x.IsRanged = (value || x.BaseUnit.IsRanged);
			});
			dictionary.Add("modifier_troll_warlord_berserkers_rage", delegate(Unit9 x, bool value)
			{
				x.IsRanged = (!value || x.BaseUnit.IsRanged);
			});
			dictionary.Add("modifier_lone_druid_true_form", delegate(Unit9 x, bool value)
			{
				x.IsRanged = (!value || x.BaseUnit.IsRanged);
			});
			dictionary.Add("modifier_slark_shadow_dance_visual", delegate(Unit9 x, bool value)
			{
				Slark slark = x.Owner as Slark;
				if (slark == null)
				{
					return;
				}
				slark.ShadowDanced(value);
			});
			dictionary.Add("modifier_morphling_replicate", delegate(Unit9 x, bool value)
			{
				Morphling morphling = x as Morphling;
				if (morphling == null)
				{
					return;
				}
				morphling.Morphed(value);
			});
			dictionary.Add("modifier_morphling_replicate_manager", delegate(Unit9 x, bool value)
			{
				Morphling morphling = x as Morphling;
				if (morphling == null)
				{
					return;
				}
				morphling.Replicated(value);
			});
			dictionary.Add("modifier_alchemist_chemical_rage", delegate(Unit9 x, bool value)
			{
				Alchemist alchemist = x as Alchemist;
				if (alchemist == null)
				{
					return;
				}
				alchemist.Raged(value);
			});
			this.specialModifiers = dictionary;
			base..ctor();
			this.damageFactory = new DamageFactory();
			this.rangeFactory = new RangeFactory();
			Entity.OnInt64PropertyChange += this.OnInt64PropertyChange;
			Entity.OnInt32PropertyChange += this.OnInt32PropertyChange;
			Entity.OnAnimationChanged += this.OnAnimationChanged;
			Unit.OnModifierAdded += this.OnModifierAdded;
			Unit.OnModifierRemoved += this.OnModifierRemoved;
			Player.OnExecuteOrder += this.OnExecuteOrder;
			Drawing.OnDraw += UnitMonitor.OnUpdate;
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060003A5 RID: 933 RVA: 0x0001C9DC File Offset: 0x0001ABDC
		// (remove) Token: 0x060003A6 RID: 934 RVA: 0x0001CA14 File Offset: 0x0001AC14
		public event UnitMonitor.EventHandler AttackEnd;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060003A7 RID: 935 RVA: 0x0001CA4C File Offset: 0x0001AC4C
		// (remove) Token: 0x060003A8 RID: 936 RVA: 0x0001CA84 File Offset: 0x0001AC84
		public event UnitMonitor.EventHandler AttackStart;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060003A9 RID: 937 RVA: 0x0001CABC File Offset: 0x0001ACBC
		// (remove) Token: 0x060003AA RID: 938 RVA: 0x0001CAF4 File Offset: 0x0001ACF4
		public event UnitMonitor.EventHandler UnitDied;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060003AB RID: 939 RVA: 0x0001CB2C File Offset: 0x0001AD2C
		// (remove) Token: 0x060003AC RID: 940 RVA: 0x0001CB64 File Offset: 0x0001AD64
		public event UnitMonitor.HealthEventHandler UnitHealthChange;

		// Token: 0x060003AD RID: 941 RVA: 0x0001CB9C File Offset: 0x0001AD9C
		public void Dispose()
		{
			this.damageFactory.Dispose();
			this.rangeFactory.Dispose();
			Entity.OnInt64PropertyChange -= this.OnInt64PropertyChange;
			Entity.OnInt32PropertyChange -= this.OnInt32PropertyChange;
			Entity.OnAnimationChanged -= this.OnAnimationChanged;
			Unit.OnModifierAdded -= this.OnModifierAdded;
			Unit.OnModifierRemoved -= this.OnModifierRemoved;
			Player.OnExecuteOrder -= this.OnExecuteOrder;
			Drawing.OnDraw -= UnitMonitor.OnUpdate;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001CC38 File Offset: 0x0001AE38
		internal void CheckModifiers(Unit9 unit)
		{
			try
			{
				foreach (Modifier modifier in unit.BaseModifiers)
				{
					this.CheckModifier(unit.Handle, modifier, true);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001CCA4 File Offset: 0x0001AEA4
		private static void DropTarget(IEnumerable<Entity> entities)
		{
			foreach (Entity entity in entities)
			{
				Unit9 unitFast = EntityManager9.GetUnitFast(entity.Handle);
				if (unitFast != null)
				{
					unitFast.Target = null;
					unitFast.IsAttacking = false;
				}
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001CD0C File Offset: 0x0001AF0C
		private static void OnUpdate(EventArgs args)
		{
			try
			{
				float rawGameTime = Game.RawGameTime;
				foreach (Unit9 unit in EntityManager9.Units)
				{
					if (!unit.IsBuilding && (unit.IsVisible = unit.BaseUnit.IsVisible))
					{
						unit.LastVisibleTime = rawGameTime;
						unit.Speed = (float)unit.BaseUnit.MovementSpeed;
						unit.CachedPosition = unit.BaseUnit.Position;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001CDB8 File Offset: 0x0001AFB8
		private static void SetTarget(IEnumerable<Entity> entities, uint targetHandle)
		{
			Unit9 unit = EntityManager9.GetUnit(targetHandle);
			if (unit == null || !unit.IsAlive)
			{
				return;
			}
			foreach (Entity entity in entities)
			{
				Unit9 unitFast = EntityManager9.GetUnitFast(entity.Handle);
				if (unitFast != null && unitFast.IsEnemy(unit))
				{
					unitFast.Target = unit;
					unitFast.IsAttacking = true;
				}
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001CE3C File Offset: 0x0001B03C
		private static void StartChanneling(uint abilityHandle)
		{
			IChanneled channeled;
			if ((channeled = (EntityManager9.GetAbilityFast(abilityHandle) as IChanneled)) == null)
			{
				return;
			}
			channeled.Owner.ChannelEndTime = channeled.GetCastDelay() + 1f;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001CE70 File Offset: 0x0001B070
		private static void StopChanneling(IEnumerable<Entity> entities)
		{
			foreach (Entity entity in entities)
			{
				Unit9 unitFast = EntityManager9.GetUnitFast(entity.Handle);
				if (!(unitFast == null))
				{
					unitFast.ChannelEndTime = 0f;
					unitFast.ChannelActivatesOnCast = false;
				}
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001CEDC File Offset: 0x0001B0DC
		private void AttackStarted(Unit9 unit)
		{
			if (this.attackSleeper.IsSleeping(unit.Handle) || !unit.IsAlive)
			{
				return;
			}
			if (!unit.IsControllable && unit.IsHero)
			{
				unit.Target = (from x in EntityManager9.Units
				where x.IsAlive && !x.IsAlly(unit) && x.Distance(unit) <= unit.GetAttackRange(x, 25f)
				orderby unit.GetAngle(x.Position, false)
				select x).FirstOrDefault((Unit9 x) => unit.GetAngle(x.Position, false) < 0.35f);
			}
			unit.IsAttacking = true;
			this.attackSleeper.Sleep(unit.Handle, unit.GetAttackPoint(null));
			UnitMonitor.EventHandler attackStart = this.AttackStart;
			if (attackStart == null)
			{
				return;
			}
			attackStart(unit);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001CFC0 File Offset: 0x0001B1C0
		private void AttackStopped(Unit9 unit)
		{
			if (!this.attackSleeper.IsSleeping(unit.Handle))
			{
				return;
			}
			if (!unit.IsControllable && !unit.IsTower && unit.IsHero)
			{
				unit.Target = null;
			}
			unit.IsAttacking = false;
			this.attackSleeper.Reset(unit.Handle);
			UnitMonitor.EventHandler attackEnd = this.AttackEnd;
			if (attackEnd == null)
			{
				return;
			}
			attackEnd(unit);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001D02C File Offset: 0x0001B22C
		private void CheckModifier(uint senderHandle, Modifier modifier, bool added)
		{
			string name = modifier.Name;
			Unit9 unitFast = EntityManager9.GetUnitFast(senderHandle);
			if (unitFast == null)
			{
				return;
			}
			Action<Unit9, bool> action;
			if (this.specialModifiers.TryGetValue(name, out action))
			{
				action(unitFast, added);
			}
			IHasRangeIncrease range = this.rangeFactory.GetRange(name);
			if (range != null)
			{
				unitFast.Range(range, added);
				return;
			}
			if (modifier.IsHidden)
			{
				return;
			}
			IAppliesImmobility disable = this.damageFactory.GetDisable(name);
			if (disable != null)
			{
				IDisable disable2;
				bool invulnerability = (disable2 = (disable as IDisable)) != null && (disable2.AppliesUnitState & UnitState.Invulnerable) > (UnitState)0UL;
				unitFast.Disabler(modifier, added, invulnerability);
				return;
			}
			if (modifier.IsStunDebuff)
			{
				unitFast.Disabler(modifier, added, false);
				return;
			}
			IHasDamageAmplify amplifier = this.damageFactory.GetAmplifier(name);
			if (amplifier != null)
			{
				unitFast.Amplifier(amplifier, added);
				return;
			}
			IHasPassiveDamageIncrease passive = this.damageFactory.GetPassive(name);
			if (passive != null)
			{
				unitFast.Passive(passive, added);
				return;
			}
			IHasDamageBlock blocker = this.damageFactory.GetBlocker(name);
			if (blocker != null)
			{
				unitFast.Blocker(blocker, added);
				return;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001D134 File Offset: 0x0001B334
		private bool IsAttackAnimation(Animation animation)
		{
			string name = animation.Name;
			if (this.notAttackAnimation.Contains(name))
			{
				return false;
			}
			if (this.attackAnimation.Contains(name))
			{
				return true;
			}
			if (name.IndexOf("attack", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				this.attackAnimation.Add(name);
				return true;
			}
			this.notAttackAnimation.Add(name);
			return false;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001D194 File Offset: 0x0001B394
		private void OnAnimationChanged(Entity sender, EventArgs args)
		{
			try
			{
				Unit9 unit = EntityManager9.GetUnit(sender.Handle);
				if (!(unit == null))
				{
					if (this.IsAttackAnimation(sender.Animation))
					{
						this.AttackStarted(unit);
					}
					else
					{
						this.AttackStopped(unit);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
		private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
		{
			if (args.IsQueued)
			{
				return;
			}
			try
			{
				OrderId orderId = args.OrderId;
				switch (orderId)
				{
				case OrderId.MoveLocation:
				case OrderId.MoveTarget:
					UnitMonitor.DropTarget(args.Entities);
					goto IL_DE;
				case OrderId.AttackLocation:
				case OrderId.AbilityTargetTree:
				case OrderId.ToggleAbility:
					goto IL_DE;
				case OrderId.AttackTarget:
					UnitMonitor.SetTarget(args.Entities, args.Target.Handle);
					goto IL_DE;
				case OrderId.AbilityLocation:
				case OrderId.Ability:
					UnitMonitor.StartChanneling(args.Ability.Handle);
					goto IL_DE;
				case OrderId.AbilityTarget:
				{
					Unit9 unitFast = EntityManager9.GetUnitFast(args.Target.Handle);
					if (unitFast != null && unitFast.IsLinkensProtected)
					{
						return;
					}
					UnitMonitor.StartChanneling(args.Ability.Handle);
					goto IL_DE;
				}
				case OrderId.Hold:
					break;
				default:
					if (orderId != OrderId.Stop)
					{
						goto IL_DE;
					}
					break;
				}
				UnitMonitor.DropTarget(args.Entities);
				UnitMonitor.StopChanneling(args.Entities);
				IL_DE:;
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001D300 File Offset: 0x0001B500
		private void OnInt32PropertyChange(Entity sender, Int32PropertyChangeEventArgs args)
		{
			int newValue = args.NewValue;
			if (newValue == args.OldValue)
			{
				return;
			}
			try
			{
				Unit9 unitFast = EntityManager9.GetUnitFast(sender.Handle);
				if (!(unitFast == null))
				{
					string propertyName = args.PropertyName;
					if (!(propertyName == "m_iHealth"))
					{
						if (propertyName == "m_NetworkActivity")
						{
							if (unitFast.IsValid)
							{
								if (this.attackActivities.Contains(newValue))
								{
									this.AttackStarted(unitFast);
								}
								else
								{
									this.AttackStopped(unitFast);
								}
							}
						}
					}
					else if (newValue > 0)
					{
						UnitMonitor.HealthEventHandler unitHealthChange = this.UnitHealthChange;
						if (unitHealthChange != null)
						{
							unitHealthChange(unitFast, (float)newValue);
						}
					}
					else
					{
						unitFast.DeathTime = Game.RawGameTime;
						this.AttackStopped(unitFast);
						this.attackSleeper.Remove(unitFast.Handle);
						UnitMonitor.EventHandler unitDied = this.UnitDied;
						if (unitDied != null)
						{
							unitDied(unitFast);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, sender, null);
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001D3F4 File Offset: 0x0001B5F4
		private void OnInt64PropertyChange(Entity sender, Int64PropertyChangeEventArgs args)
		{
			if (args.NewValue == args.OldValue)
			{
				return;
			}
			try
			{
				string propertyName = args.PropertyName;
				if (propertyName == "m_iIsControllableByPlayer64")
				{
					UpdateManager.BeginInvoke(delegate
					{
						EntityManager9.ChangeEntityControl(sender);
					}, 0);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001D460 File Offset: 0x0001B660
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				this.CheckModifier(sender.Handle, args.Modifier, true);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001D4A0 File Offset: 0x0001B6A0
		private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				this.CheckModifier(sender.Handle, args.Modifier, false);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x040001A0 RID: 416
		private readonly HashSet<int> attackActivities = new HashSet<int>
		{
			1503,
			1504,
			1505,
			1505,
			1705
		};

		// Token: 0x040001A1 RID: 417
		private readonly HashSet<string> attackAnimation = new HashSet<string>
		{
			"tidebringer"
		};

		// Token: 0x040001A2 RID: 418
		private readonly MultiSleeper attackSleeper = new MultiSleeper();

		// Token: 0x040001A3 RID: 419
		private readonly DamageFactory damageFactory;

		// Token: 0x040001A4 RID: 420
		private readonly HashSet<string> notAttackAnimation = new HashSet<string>
		{
			"sniper_attack_schrapnel_cast1_aggressive",
			"sniper_attack_schrapnel_cast1_aggressive_anim",
			"attack_omni_cast",
			"lotfl_dualwield_press_the_attack",
			"lotfl_press_the_attack",
			"sniper_attack_assassinate_dreamleague"
		};

		// Token: 0x040001A5 RID: 421
		private readonly RangeFactory rangeFactory;

		// Token: 0x040001A6 RID: 422
		private readonly Dictionary<string, Action<Unit9, bool>> specialModifiers;

		// Token: 0x02000074 RID: 116
		// (Invoke) Token: 0x060003BF RID: 959
		public delegate void EventHandler(Unit9 unit);

		// Token: 0x02000075 RID: 117
		// (Invoke) Token: 0x060003C3 RID: 963
		public delegate void HealthEventHandler(Unit9 unit, float health);
	}
}
