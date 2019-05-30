using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Magnus.Abilities;
using O9K.AIO.Heroes.Magnus.Modes;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Magnus.Units
{
	// Token: 0x0200010B RID: 267
	[UnitName("npc_dota_hero_magnataur")]
	internal class Magnus : ControllableUnit
	{
		// Token: 0x06000548 RID: 1352 RVA: 0x0001B1C8 File Offset: 0x000193C8
		public Magnus(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.magnataur_shockwave,
					(ActiveAbility x) => this.shockwave = new Shockwave(x)
				},
				{
					AbilityId.magnataur_skewer,
					delegate(ActiveAbility x)
					{
						this.skewer = new Skewer(x);
						ReversePolarity reversePolarity = this.polarity;
						if (reversePolarity != null)
						{
							reversePolarity.AddSkewer(this.skewer);
						}
						return this.skewer;
					}
				},
				{
					AbilityId.magnataur_reverse_polarity,
					delegate(ActiveAbility x)
					{
						this.polarity = new ReversePolarity(x);
						if (this.skewer != null)
						{
							this.polarity.AddSkewer(this.skewer);
						}
						return this.polarity;
					}
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkDaggerMagnus(x)
				},
				{
					AbilityId.item_force_staff,
					(ActiveAbility x) => this.force = new ForceStaff(x)
				},
				{
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new ShivasGuard(x)
				},
				{
					AbilityId.item_refresher,
					(ActiveAbility x) => this.refresher = new UntargetableAbility(x)
				},
				{
					AbilityId.item_refresher_shard,
					(ActiveAbility x) => this.refresherShard = new UntargetableAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.magnataur_skewer, (ActiveAbility x) => this.moveSkewer = new BlinkAbility(x));
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001B2B4 File Offset: 0x000194B4
		public void BlinkSkewerCombo(TargetManager targetManager, BlinkSkewerModeMenu menu)
		{
			if (base.ComboSleeper.IsSleeping)
			{
				return;
			}
			BlinkDaggerMagnus blinkDaggerMagnus = this.blink;
			if (blinkDaggerMagnus != null && blinkDaggerMagnus.Ability.CanBeCasted(true))
			{
				Skewer skewer = this.skewer;
				if (skewer != null && skewer.Ability.CanBeCasted(true))
				{
					Vector3 predictedPosition = targetManager.Target.GetPredictedPosition(this.skewer.Ability.CastPoint);
					Vector3 blinkPosition = Vector3Extensions.Extend2D(base.Owner.Position, predictedPosition, base.Owner.Distance(predictedPosition) + 100f);
					float num = base.Owner.Distance(blinkPosition);
					if (this.blink.Ability.CastRange < num)
					{
						return;
					}
					Unit9 unit = (from x in targetManager.AllyHeroes
					where !x.Equals(this.Owner) && menu.IsAllyEnabled(x.Name) && x.Distance(blinkPosition) < this.skewer.Ability.CastRange + 800f
					orderby x.Distance(blinkPosition)
					select x).FirstOrDefault<Unit9>();
					if (unit == null)
					{
						return;
					}
					base.OrbwalkSleeper.Sleep(this.skewer.Ability.CastPoint + 0.3f);
					this.blink.Ability.UseAbility(blinkPosition, false, false);
					this.skewer.Ability.UseAbility(unit.Position, false, false);
					base.ComboSleeper.Sleep(0.3f);
					return;
				}
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001B424 File Offset: 0x00019624
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.polarity, true))
			{
				return true;
			}
			if (!abilityHelper.CanBeCasted(this.polarity, false, false, false, true) && !abilityHelper.CanBeCasted(this.skewer, true, true, true, true) && abilityHelper.UseAbility(this.shiva, true))
			{
				return true;
			}
			if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink, 0f))
			{
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.blink, new UsableAbility[]
			{
				this.polarity
			}))
			{
				UpdateManager.BeginInvoke(delegate
				{
					this.polarity.ForceUseAbility(targetManager, this.ComboSleeper, comboModeMenu);
				}, 50);
				return true;
			}
			if (abilityHelper.UseAbility(this.force, 500f, 100f))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.skewer, true))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.skewer, false, false, true, true))
			{
				bool flag = abilityHelper.CanBeCasted(this.blink, false, false, true, true);
				bool flag2 = abilityHelper.CanBeCasted(this.force, false, false, true, true);
				if (this.skewer.UseAbilityIfCondition(targetManager, base.ComboSleeper, comboModeMenu, this.polarity, flag, flag2))
				{
					return true;
				}
			}
			if (abilityHelper.UseAbilityIfCondition(this.skewer, new UsableAbility[]
			{
				this.blink,
				this.force
			}))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.skewer, false, false, true, true) && abilityHelper.CanBeCasted(this.shockwave, false, true, true, true) && !abilityHelper.CanBeCasted(this.polarity, false, false, true, true) && this.skewer.UseAbilityOnTarget(targetManager, base.ComboSleeper))
			{
				return true;
			}
			if (abilityHelper.UseAbilityIfCondition(this.shockwave, new UsableAbility[]
			{
				this.blink,
				this.force,
				this.skewer,
				this.polarity
			}))
			{
				return true;
			}
			if ((abilityHelper.CanBeCasted(this.refresher, true, true, true, true) || abilityHelper.CanBeCasted(this.refresherShard, true, true, true, true)) && abilityHelper.CanBeCasted(this.polarity, true, true, true, false) && !this.polarity.Ability.IsReady)
			{
				UntargetableAbility untargetableAbility = abilityHelper.CanBeCasted(this.refresherShard, true, true, true, true) ? this.refresherShard : this.refresher;
				if (abilityHelper.HasMana(new UsableAbility[]
				{
					this.polarity,
					untargetableAbility
				}) && abilityHelper.UseAbility(untargetableAbility, true))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00004BEA File Offset: 0x00002DEA
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBlinks(abilityHelper) || abilityHelper.UseMoveAbility(this.moveSkewer);
		}

		// Token: 0x040002EF RID: 751
		private BlinkDaggerMagnus blink;

		// Token: 0x040002F0 RID: 752
		private ForceStaff force;

		// Token: 0x040002F1 RID: 753
		private BlinkAbility moveSkewer;

		// Token: 0x040002F2 RID: 754
		private ReversePolarity polarity;

		// Token: 0x040002F3 RID: 755
		private UntargetableAbility refresher;

		// Token: 0x040002F4 RID: 756
		private UntargetableAbility refresherShard;

		// Token: 0x040002F5 RID: 757
		private ShivasGuard shiva;

		// Token: 0x040002F6 RID: 758
		private Shockwave shockwave;

		// Token: 0x040002F7 RID: 759
		private Skewer skewer;
	}
}
