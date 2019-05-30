using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Morphling.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Heroes.Unique;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;

namespace O9K.AIO.Heroes.Morphling.Units
{
	// Token: 0x020000F5 RID: 245
	[UnitName("npc_dota_hero_morphling")]
	internal class Morphling : ControllableUnit, IDisposable
	{
		// Token: 0x060004E2 RID: 1250 RVA: 0x00019A30 File Offset: 0x00017C30
		public Morphling(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			this.morphling = (Morphling)owner;
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.morphling_waveform,
					(ActiveAbility x) => this.wave = new Wave(x)
				},
				{
					AbilityId.morphling_adaptive_strike_str,
					(ActiveAbility x) => this.str = new DisableAbility(x)
				},
				{
					AbilityId.morphling_adaptive_strike_agi,
					(ActiveAbility x) => this.agi = new NukeAbility(x)
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
					AbilityId.item_ethereal_blade,
					(ActiveAbility x) => this.ethereal = new NukeAbility(x)
				},
				{
					AbilityId.item_manta,
					(ActiveAbility x) => this.manta = new MantaStyle(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.morphling_waveform, (ActiveAbility x) => this.waveBlink = new BlinkAbility(x));
			Player.OnExecuteOrder += this.OnExecuteOrder;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00019B38 File Offset: 0x00017D38
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			if (this.morphling.IsMorphed)
			{
				return false;
			}
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbility(this.orchid, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.bloodthorn, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.ethereal, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.wave, true))
			{
				return true;
			}
			if (targetManager.Target.IsChanneling && abilityHelper.UseAbility(this.str, true))
			{
				return true;
			}
			if (base.Owner.TotalAgility > base.Owner.TotalStrength)
			{
				if (abilityHelper.UseAbility(this.agi, true))
				{
					return true;
				}
			}
			else if (abilityHelper.UseAbility(this.str, true))
			{
				return true;
			}
			return abilityHelper.UseAbility(this.manta, true);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000048B2 File Offset: 0x00002AB2
		public void Dispose()
		{
			Player.OnExecuteOrder -= this.OnExecuteOrder;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000048C5 File Offset: 0x00002AC5
		public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			return !this.morphling.IsMorphed && base.Orbwalk(target, attack, move, comboMenu);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000048E1 File Offset: 0x00002AE1
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBlinks(abilityHelper) || abilityHelper.UseMoveAbility(this.waveBlink);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00019C0C File Offset: 0x00017E0C
		private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
		{
			try
			{
				if (this.morphling.IsMorphed)
				{
					if (args.Process && !args.IsQueued && args.Entities.Contains(base.Owner.BaseUnit))
					{
						OrderId orderId = args.OrderId;
						if (orderId == OrderId.Ability || orderId == OrderId.AbilityLocation || orderId == OrderId.AbilityTarget)
						{
							Ability ability = args.Ability;
							this.morphlingAbilitySleeper.Sleep(ability.Handle, ability.CooldownLength);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x040002B1 RID: 689
		private readonly Morphling morphling;

		// Token: 0x040002B2 RID: 690
		private readonly MultiSleeper morphlingAbilitySleeper = new MultiSleeper();

		// Token: 0x040002B3 RID: 691
		private NukeAbility agi;

		// Token: 0x040002B4 RID: 692
		private DisableAbility bloodthorn;

		// Token: 0x040002B5 RID: 693
		private NukeAbility ethereal;

		// Token: 0x040002B6 RID: 694
		private BuffAbility manta;

		// Token: 0x040002B7 RID: 695
		private Morph morph;

		// Token: 0x040002B8 RID: 696
		private DisableAbility orchid;

		// Token: 0x040002B9 RID: 697
		private Replicate replicate;

		// Token: 0x040002BA RID: 698
		private DisableAbility str;

		// Token: 0x040002BB RID: 699
		private NukeAbility wave;

		// Token: 0x040002BC RID: 700
		private BlinkAbility waveBlink;
	}
}
