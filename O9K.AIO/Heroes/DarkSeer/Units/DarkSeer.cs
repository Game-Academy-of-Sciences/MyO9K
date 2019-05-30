using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.DarkSeer.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.DarkSeer.Units
{
	// Token: 0x020001C8 RID: 456
	[UnitName("npc_dota_hero_dark_seer")]
	internal class DarkSeer : ControllableUnit
	{
		// Token: 0x06000913 RID: 2323 RVA: 0x000288CC File Offset: 0x00026ACC
		public DarkSeer(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.dark_seer_vacuum,
					delegate(ActiveAbility x)
					{
						this.vacuum = new Vacuum(x);
						if (this.wall != null)
						{
							this.wall.Vacuum = this.vacuum;
						}
						return this.vacuum;
					}
				},
				{
					AbilityId.dark_seer_ion_shell,
					(ActiveAbility x) => this.shell = new IonShell(x)
				},
				{
					AbilityId.dark_seer_surge,
					(ActiveAbility x) => this.surge = new BuffAbility(x)
				},
				{
					AbilityId.dark_seer_wall_of_replica,
					delegate(ActiveAbility x)
					{
						this.wall = new WallOfReplica(x);
						if (this.vacuum != null)
						{
							this.wall.Vacuum = this.vacuum;
						}
						return this.wall;
					}
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				},
				{
					AbilityId.item_blade_mail,
					(ActiveAbility x) => this.bladeMail = new ShieldAbility(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.dark_seer_surge, (ActiveAbility _) => this.surge);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000289B8 File Offset: 0x00026BB8
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.shiva, true) || abilityHelper.UseAbility(this.bladeMail, 400f) || abilityHelper.UseAbility(this.vacuum, true) || abilityHelper.UseAbilityIfNone(this.wall, new UsableAbility[]
			{
				this.vacuum
			}) || abilityHelper.UseAbility(this.blink, 600f, 400f) || abilityHelper.UseAbility(this.force, 600f, 400f) || abilityHelper.UseAbility(this.shell, true) || (abilityHelper.CanBeCasted(this.surge, false, false, true, true) && abilityHelper.ForceUseAbility(this.surge, false, true));
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0000691B File Offset: 0x00004B1B
		protected override bool MoveComboUseBuffs(AbilityHelper abilityHelper)
		{
			if (base.MoveComboUseBuffs(abilityHelper))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.surge, false, false, true, true))
			{
				abilityHelper.ForceUseAbility(this.surge, false, true);
				return true;
			}
			return false;
		}

		// Token: 0x040004D2 RID: 1234
		private ShieldAbility bladeMail;

		// Token: 0x040004D3 RID: 1235
		private BlinkAbility blink;

		// Token: 0x040004D4 RID: 1236
		private ForceStaff force;

		// Token: 0x040004D5 RID: 1237
		private BuffAbility shell;

		// Token: 0x040004D6 RID: 1238
		private DebuffAbility shiva;

		// Token: 0x040004D7 RID: 1239
		private BuffAbility surge;

		// Token: 0x040004D8 RID: 1240
		private Vacuum vacuum;

		// Token: 0x040004D9 RID: 1241
		private WallOfReplica wall;
	}
}
