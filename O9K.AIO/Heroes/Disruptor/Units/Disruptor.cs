using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Disruptor.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Disruptor.Units
{
	// Token: 0x0200014E RID: 334
	[UnitName("npc_dota_hero_disruptor")]
	internal class Disruptor : ControllableUnit, IDisposable
	{
		// Token: 0x06000689 RID: 1673 RVA: 0x0001F9A8 File Offset: 0x0001DBA8
		public Disruptor(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.disruptor_thunder_strike,
					(ActiveAbility x) => this.thunder = new NukeAbility(x)
				},
				{
					AbilityId.disruptor_glimpse,
					(ActiveAbility x) => this.glimpse = new TargetableAbility(x)
				},
				{
					AbilityId.disruptor_kinetic_field,
					(ActiveAbility x) => this.field = new KineticField(x)
				},
				{
					AbilityId.disruptor_static_storm,
					(ActiveAbility x) => this.storm = new StaticStorm(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_veil_of_discord,
					(ActiveAbility x) => this.veil = new DebuffAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.disruptor_kinetic_field, (ActiveAbility _) => this.field);
			Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001FA94 File Offset: 0x0001DC94
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.glimpse, true))
			{
				BlinkAbility blinkAbility = this.blink;
				if (blinkAbility != null)
				{
					blinkAbility.Sleeper.Sleep(3f);
				}
				ForceStaff forceStaff = this.force;
				if (forceStaff != null)
				{
					forceStaff.Sleeper.Sleep(3f);
				}
				return true;
			}
			if (abilityHelper.CanBeCasted(this.glimpse, false, true, true, true))
			{
				if (abilityHelper.UseAbility(this.blink, this.glimpse.Ability.CastRange, this.glimpse.Ability.CastRange - 500f))
				{
					return true;
				}
			}
			else if (abilityHelper.UseAbility(this.blink, 800f, 500f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.force, this.glimpse.Ability.CastRange, this.glimpse.Ability.CastRange - 500f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.veil, true))
			{
				return true;
			}
			float timeSinceCasted = this.glimpse.Ability.TimeSinceCasted;
			if ((double)timeSinceCasted < 1.8)
			{
				ParticleEffect particleEffect = this.glimpseParticle;
				if (particleEffect != null && particleEffect.IsValid && !targetManager.Target.IsMagicImmune)
				{
					Vector3 controlPoint = this.glimpseParticle.GetControlPoint(1u);
					if (abilityHelper.CanBeCasted(this.field, false, true, true, true) && this.field.UseAbility(controlPoint, base.ComboSleeper))
					{
						return true;
					}
					float x = this.glimpseParticle.GetControlPoint(2u).X;
					if (timeSinceCasted + 0.35f > x && abilityHelper.CanBeCasted(this.storm, false, true, true, true) && this.storm.UseAbility(controlPoint, targetManager, base.ComboSleeper))
					{
						return true;
					}
					if (base.Owner.Distance(controlPoint) > this.storm.Ability.CastRange - 100f)
					{
						base.Owner.BaseUnit.Move(Vector3Extensions.Extend2D(controlPoint, base.Owner.Position, 500f));
						return true;
					}
				}
				base.OrbwalkSleeper.Sleep(0.1f);
				return true;
			}
			if (timeSinceCasted > 2f && abilityHelper.UseAbilityIfNone(this.field, new UsableAbility[]
			{
				this.glimpse
			}))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.storm, true, true, true, true))
			{
				if (this.field.Ability.TimeSinceCasted <= 4f)
				{
					if (this.storm.UseAbility(this.field.CastPosition, targetManager, base.ComboSleeper))
					{
						return true;
					}
				}
				else if (abilityHelper.UseAbility(this.storm, true))
				{
					return true;
				}
			}
			if (abilityHelper.CanBeCasted(this.thunder, true, true, true, true))
			{
				float num = base.Owner.Mana - this.thunder.Ability.ManaCost;
				if (abilityHelper.CanBeCasted(this.field, true, true, true, true))
				{
					num -= this.field.Ability.ManaCost;
				}
				if (abilityHelper.CanBeCasted(this.storm, true, true, true, true))
				{
					num -= this.storm.Ability.ManaCost;
				}
				if (num > 0f)
				{
					abilityHelper.UseAbility(this.thunder, true);
				}
			}
			return false;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0000555F File Offset: 0x0000375F
		public void Dispose()
		{
			Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00005572 File Offset: 0x00003772
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseAbility(this.field, true);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00005591 File Offset: 0x00003791
		private void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
		{
			if (args.Name == "particles/units/heroes/hero_disruptor/disruptor_glimpse_targetend.vpcf")
			{
				this.glimpseParticle = args.ParticleEffect;
			}
		}

		// Token: 0x04000392 RID: 914
		private BlinkAbility blink;

		// Token: 0x04000393 RID: 915
		private KineticField field;

		// Token: 0x04000394 RID: 916
		private ForceStaff force;

		// Token: 0x04000395 RID: 917
		private TargetableAbility glimpse;

		// Token: 0x04000396 RID: 918
		private ParticleEffect glimpseParticle;

		// Token: 0x04000397 RID: 919
		private StaticStorm storm;

		// Token: 0x04000398 RID: 920
		private NukeAbility thunder;

		// Token: 0x04000399 RID: 921
		private DebuffAbility veil;
	}
}
