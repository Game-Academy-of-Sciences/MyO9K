using System;
using System.Linq;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Permanent;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Heroes;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;

namespace O9K.AIO.Heroes.EmberSpirit.Modes
{
	// Token: 0x0200017B RID: 379
	internal class AutoChainsMode : PermanentMode
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x00005EE4 File Offset: 0x000040E4
		public AutoChainsMode(BaseHero baseHero, AutoChainsModeMenu menu) : base(baseHero, menu)
		{
			this.menu = menu;
			Player.OnExecuteOrder += this.OnExecuteOrder;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00005F06 File Offset: 0x00004106
		public override void Dispose()
		{
			base.Dispose();
			Player.OnExecuteOrder -= this.OnExecuteOrder;
			Unit.OnModifierAdded -= this.OnModifierAdded;
			Unit.OnModifierRemoved -= this.OnModifierRemoved;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00023DD0 File Offset: 0x00021FD0
		protected override void Execute()
		{
			if (!this.useChains)
			{
				return;
			}
			Hero9 hero = base.Owner.Hero;
			if (!hero.IsValid || !hero.IsAlive)
			{
				return;
			}
			ActiveAbility chains = hero.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.ember_spirit_searing_chains) as ActiveAbility;
			ActiveAbility chains2 = chains;
			if (chains2 == null || !chains2.IsValid || !chains.CanBeCasted(true))
			{
				return;
			}
			if (!base.TargetManager.EnemyHeroes.Any((Unit9 x) => chains.CanHit(x)))
			{
				return;
			}
			if (this.menu.fistKey)
			{
				chains.UseAbility(false, false);
			}
			this.useChains = false;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00023EAC File Offset: 0x000220AC
		private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
		{
			try
			{
				if (args.IsPlayerInput && args.Process && args.OrderId == OrderId.AbilityLocation)
				{
					if (args.Ability.Id == AbilityId.ember_spirit_sleight_of_fist)
					{
						Unit.OnModifierAdded += this.OnModifierAdded;
						Unit.OnModifierRemoved += this.OnModifierRemoved;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00023F24 File Offset: 0x00022124
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Handle == base.Owner.HeroHandle)
				{
					if (args.Modifier.Name == "modifier_ember_spirit_sleight_of_fist_caster")
					{
						this.useChains = true;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00023F84 File Offset: 0x00022184
		private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Handle == base.Owner.HeroHandle)
				{
					if (args.Modifier.Name == "modifier_ember_spirit_sleight_of_fist_caster")
					{
						Unit.OnModifierAdded -= this.OnModifierAdded;
						Unit.OnModifierRemoved -= this.OnModifierRemoved;
						this.useChains = false;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x0400044E RID: 1102
		private readonly AutoChainsModeMenu menu;

		// Token: 0x0400044F RID: 1103
		private bool useChains;
	}
}
