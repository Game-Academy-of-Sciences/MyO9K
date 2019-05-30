using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Kunkka.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using SharpDX;

namespace O9K.AIO.Heroes.Kunkka.Units
{
	// Token: 0x02000129 RID: 297
	[UnitName("npc_dota_hero_kunkka")]
	internal class Kunkka : ControllableUnit, IDisposable
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x0001D54C File Offset: 0x0001B74C
		public Kunkka(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			this.playerHandle = owner.BaseOwner.Handle;
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.kunkka_torrent,
					(ActiveAbility x) => this.torrent = new Torrent(x)
				},
				{
					AbilityId.kunkka_tidebringer,
					(ActiveAbility x) => this.tidebringer = new TargetableAbility(x)
				},
				{
					AbilityId.kunkka_x_marks_the_spot,
					(ActiveAbility x) => this.xMark = new XMark(x)
				},
				{
					AbilityId.kunkka_return,
					(ActiveAbility x) => this.xReturn = new UntargetableAbility(x)
				},
				{
					AbilityId.kunkka_ghostship,
					(ActiveAbility x) => this.ship = new Ghostship(x)
				},
				{
					AbilityId.item_phase_boots,
					(ActiveAbility x) => this.phase = new SpeedBuffAbility(x)
				},
				{
					AbilityId.item_armlet,
					(ActiveAbility x) => this.armlet = new BuffAbility(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				}
			};
			Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;
			Unit.OnModifierAdded += this.OnModifierAdded;
			Player.OnExecuteOrder += this.OnExecuteOrder;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001D66C File Offset: 0x0001B86C
		public void AutoReturn()
		{
			if (base.ComboSleeper.IsSleeping)
			{
				return;
			}
			if (this.xMark.Position.IsZero)
			{
				return;
			}
			ActiveAbility ability = this.xReturn.Ability;
			if (!ability.CanBeCasted(true))
			{
				return;
			}
			if (!this.torrent.ShouldReturn(this.xReturn.Ability, this.xMark.Position) && !this.ship.ShouldReturn(this.xReturn.Ability, this.xMark.Position))
			{
				return;
			}
			ability.UseAbility(false, false);
			base.ComboSleeper.Sleep(ability.GetCastDelay());
			base.OrbwalkSleeper.Sleep(ability.GetCastDelay());
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001D728 File Offset: 0x0001B928
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			if (comboModeMenu.IsHarassCombo)
			{
				return false;
			}
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.CanBeCasted(this.blink, true, true, true, true) && !abilityHelper.CanBeCasted(this.xReturn, true, true, true, true))
			{
				float blinkToEnemyRange = 0f;
				if (!abilityHelper.CanBeCasted(this.xMark, true, true, true, true))
				{
					if (abilityHelper.CanBeCasted(this.xMark, false, true, true, true))
					{
						blinkToEnemyRange = Math.Min(this.xMark.Ability.CastRange - 100f, Math.Max(base.Owner.Distance(targetManager.Target) - this.xMark.Ability.CastRange, 0f));
					}
					if (abilityHelper.UseAbility(this.blink, 500f, blinkToEnemyRange))
					{
						return true;
					}
				}
			}
			if (abilityHelper.UseAbilityIfAny(this.xMark, new UsableAbility[]
			{
				this.torrent,
				this.ship
			}))
			{
				base.ComboSleeper.ExtendSleep(0.1f);
				base.OrbwalkSleeper.ExtendSleep(0.1f);
				return true;
			}
			if (abilityHelper.CanBeCasted(this.xReturn, true, true, true, true))
			{
				if (!this.xMark.Position.IsZero)
				{
					if (abilityHelper.CanBeCasted(this.ship, false, true, true, true) && this.ship.UseAbility(this.xMark.Position, targetManager, base.ComboSleeper))
					{
						return true;
					}
					if (abilityHelper.CanBeCasted(this.torrent, false, true, true, true) && this.torrent.UseAbility(this.xMark.Position, targetManager, base.ComboSleeper))
					{
						return true;
					}
					if (!this.torrent.ShouldReturn(this.xReturn.Ability, this.xMark.Position))
					{
						Ghostship ghostship = this.ship;
						if (ghostship == null || !ghostship.ShouldReturn(this.xReturn.Ability, this.xMark.Position))
						{
							goto IL_220;
						}
					}
					if (abilityHelper.UseAbility(this.xReturn, true))
					{
						return true;
					}
				}
			}
			else
			{
				if (abilityHelper.UseAbility(this.torrent, true))
				{
					return true;
				}
				if (abilityHelper.UseAbility(this.ship, true))
				{
					return true;
				}
			}
			IL_220:
			return abilityHelper.UseAbility(this.armlet, 400f) || abilityHelper.UseAbility(this.phase, true);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000513D File Offset: 0x0000333D
		public void Dispose()
		{
			Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
			Unit.OnModifierAdded -= this.OnModifierAdded;
			Player.OnExecuteOrder -= this.OnExecuteOrder;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001D97C File Offset: 0x0001BB7C
		public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			if (comboMenu != null && target != null && comboMenu.IsHarassCombo)
			{
				ActiveAbility ability = this.tidebringer.Ability;
				if (ability.CanBeCasted(true))
				{
					Vector3 ownerPosition = base.Owner.Position;
					float num = base.Owner.GetAttackPoint(null) + Game.Ping / 1000f + 0.3f;
					Vector3 targetPredictedPosition = target.GetPredictedPosition(num);
					Unit9 unit = (from x in EntityManager9.Units
					where x.IsUnit && !x.Equals(target) && x.IsAlive && x.IsVisible && !x.IsInvulnerable && !x.IsAlly(this.Owner) && x.Distance(target) < ability.Range
					orderby Vector3Extensions.AngleBetween(ownerPosition, x.Position, targetPredictedPosition)
					select x).FirstOrDefault<Unit9>();
					if (unit != null)
					{
						Vector3 position = unit.Position;
						if (this.CanAttack(unit, 0f) && Vector3Extensions.AngleBetween(ownerPosition, position, targetPredictedPosition) < 45f)
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
				}
			}
			return base.Orbwalk(target, attack, move, comboMenu);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001DB44 File Offset: 0x0001BD44
		protected override bool UseOrbAbility(Unit9 target, ComboModeMenu comboMenu)
		{
			if (!base.Owner.CanUseAbilities)
			{
				return false;
			}
			ActiveAbility ability = this.tidebringer.Ability;
			return comboMenu != null && comboMenu.IsAbilityEnabled(ability) && (ability.CanBeCasted(true) && ability.CanHit(target) && ability.UseAbility(target, false, false));
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001DBA0 File Offset: 0x0001BDA0
		private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
		{
			try
			{
				if (args.Process && args.OrderId == OrderId.Ability)
				{
					if (args.Ability.Handle == this.xReturn.Ability.Handle)
					{
						this.torrent.Modifier = null;
						this.xMark.Position = Vector3.Zero;
						if (this.ship != null)
						{
							this.ship.Position = Vector3.Zero;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001DC34 File Offset: 0x0001BE34
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				Modifier modifier = args.Modifier;
				if (modifier.IsHidden && modifier.Team == base.Owner.Team)
				{
					Unit owner = modifier.Owner;
					EntityHandle? entityHandle;
					if (owner == null)
					{
						entityHandle = null;
					}
					else
					{
						Entity owner2 = owner.Owner;
						entityHandle = ((owner2 != null) ? new EntityHandle?(owner2.Handle) : null);
					}
					EntityHandle? entityHandle2 = entityHandle;
					if (!(((entityHandle2 != null) ? new uint?(entityHandle2.GetValueOrDefault()) : null) != base.Owner.Handle))
					{
						if (modifier.Name == "modifier_kunkka_torrent_thinker")
						{
							this.torrent.Modifier = modifier;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, EntityManager9.Abilities.Count((Ability9 x) => base.Owner.Equals(x.Owner)).ToString());
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001DD44 File Offset: 0x0001BF44
		private void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
		{
			try
			{
				ParticleEffect particle = args.ParticleEffect;
				Entity owner = particle.Owner;
				EntityHandle? entityHandle = (owner != null) ? new EntityHandle?(owner.Handle) : null;
				if (!(((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) != this.playerHandle))
				{
					string name = args.Name;
					if (!(name == "particles/units/heroes/hero_kunkka/kunkka_spell_x_spot.vpcf") && !(name == "particles/econ/items/kunkka/divine_anchor/hero_kunkka_dafx_skills/kunkka_spell_x_spot_fxset.vpcf"))
					{
						if (name == "particles/units/heroes/hero_kunkka/kunkka_ghostship_marker.vpcf")
						{
							float time = Game.RawGameTime - Game.Ping / 2000f;
							UpdateManager.BeginInvoke(delegate
							{
								this.ship.CalculateTimings(particle.GetControlPoint(0u), time);
							}, 0);
						}
					}
					else
					{
						UpdateManager.BeginInvoke(delegate
						{
							this.xMark.Position = particle.GetControlPoint(0u);
						}, 0);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x04000343 RID: 835
		private readonly uint playerHandle;

		// Token: 0x04000344 RID: 836
		private BuffAbility armlet;

		// Token: 0x04000345 RID: 837
		private BlinkAbility blink;

		// Token: 0x04000346 RID: 838
		private SpeedBuffAbility phase;

		// Token: 0x04000347 RID: 839
		private Ghostship ship;

		// Token: 0x04000348 RID: 840
		private TargetableAbility tidebringer;

		// Token: 0x04000349 RID: 841
		private Torrent torrent;

		// Token: 0x0400034A RID: 842
		private XMark xMark;

		// Token: 0x0400034B RID: 843
		private UntargetableAbility xReturn;
	}
}
