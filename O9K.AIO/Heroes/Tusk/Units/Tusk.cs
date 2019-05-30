using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Tusk.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Heroes;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using SharpDX;

namespace O9K.AIO.Heroes.Tusk.Units
{
	// Token: 0x02000063 RID: 99
	[UnitName("npc_dota_hero_tusk")]
	internal class Tusk : ControllableUnit
	{
		// Token: 0x06000212 RID: 530 RVA: 0x0000EF58 File Offset: 0x0000D158
		public Tusk(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.tusk_ice_shards,
					(ActiveAbility x) => this.shards = new IceShards(x)
				},
				{
					AbilityId.tusk_snowball,
					(ActiveAbility x) => this.snowball = new Snowball(x)
				},
				{
					AbilityId.tusk_tag_team,
					(ActiveAbility x) => this.tag = new DebuffAbility(x)
				},
				{
					AbilityId.tusk_walrus_punch,
					(ActiveAbility x) => this.punch = new NukeAbility(x)
				},
				{
					AbilityId.tusk_walrus_kick,
					(ActiveAbility x) => this.kick = new Kick(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
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
					AbilityId.item_solar_crest,
					(ActiveAbility x) => this.solar = new DebuffAbility(x)
				},
				{
					AbilityId.item_medallion_of_courage,
					(ActiveAbility x) => this.medallion = new DebuffAbility(x)
				},
				{
					AbilityId.item_blade_mail,
					(ActiveAbility x) => this.bladeMail = new ShieldAbility(x)
				}
			};
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000F070 File Offset: 0x0000D270
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			if (comboModeMenu.SimplifiedName == "kicktoallycombo")
			{
				return this.KickToAllyCombo(targetManager, comboModeMenu);
			}
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.blink, true))
			{
				base.ComboSleeper.ExtendSleep(0.2f);
				return true;
			}
			return abilityHelper.UseAbility(this.solar, true) || abilityHelper.UseAbility(this.medallion, true) || abilityHelper.UseAbility(this.tag, true) || abilityHelper.UseAbility(this.punch, true) || abilityHelper.UseAbility(this.snowball, true) || abilityHelper.UseAbility(this.shards, true) || abilityHelper.UseAbility(this.vessel, true) || abilityHelper.UseAbility(this.urn, true) || abilityHelper.UseAbility(this.bladeMail, 400f) || abilityHelper.UseAbility(this.kick, true);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000F174 File Offset: 0x0000D374
		public bool KickToAllyCombo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (base.Owner.HasModifier("modifier_tusk_snowball_movement"))
			{
				Hero9 hero = EntityManager9.Heroes.FirstOrDefault((Hero9 x) => !x.Equals(base.Owner) && x.IsAlly(base.Owner) && x.IsAlive && !x.IsInvulnerable && x.Distance(base.Owner) < 200f);
				if (hero != null && base.Owner.Attack(hero))
				{
					base.ComboSleeper.Sleep(0.1f);
					return true;
				}
			}
			if (abilityHelper.UseAbility(this.punch, true))
			{
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.kick, new UsableAbility[0]))
			{
				return true;
			}
			if (targetManager.Target.HasModifier("modifier_tusk_walrus_kick_slow") && abilityHelper.UseAbilityIfNone(this.snowball, new UsableAbility[0]))
			{
				return true;
			}
			if (!base.Owner.IsInvulnerable)
			{
				if (abilityHelper.UseAbilityIfNone(this.shards, new UsableAbility[]
				{
					this.snowball,
					this.punch,
					this.kick
				}))
				{
					return true;
				}
				if (abilityHelper.UseAbilityIfNone(this.tag, new UsableAbility[]
				{
					this.snowball,
					this.punch,
					this.kick
				}))
				{
					return true;
				}
			}
			if (abilityHelper.CanBeCasted(this.blink, true, true, true, true) && abilityHelper.CanBeCasted(this.kick, false, true, true, true) && !abilityHelper.CanBeCasted(this.kick, true, true, true, true))
			{
				Vector3 vector = Vector3Extensions.Extend2D(targetManager.Target.Position, EntityManager9.EnemyFountain, 100f);
				if (base.Owner.Distance(vector) > this.blink.Ability.Range)
				{
					return false;
				}
				if (abilityHelper.UseAbility(this.blink, vector))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400011E RID: 286
		private ShieldAbility bladeMail;

		// Token: 0x0400011F RID: 287
		private BlinkAbility blink;

		// Token: 0x04000120 RID: 288
		private NukeAbility kick;

		// Token: 0x04000121 RID: 289
		private DebuffAbility medallion;

		// Token: 0x04000122 RID: 290
		private NukeAbility punch;

		// Token: 0x04000123 RID: 291
		private IceShards shards;

		// Token: 0x04000124 RID: 292
		private TargetableAbility snowball;

		// Token: 0x04000125 RID: 293
		private DebuffAbility solar;

		// Token: 0x04000126 RID: 294
		private DebuffAbility tag;

		// Token: 0x04000127 RID: 295
		private DebuffAbility urn;

		// Token: 0x04000128 RID: 296
		private DebuffAbility vessel;
	}
}
