using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Specials.Unique
{
	// Token: 0x020001A1 RID: 417
	[AbilityId(AbilityId.item_hurricane_pike)]
	internal class HurricanePikeSpecial : OldSpecialAbility, IDisposable
	{
		// Token: 0x06000871 RID: 2161 RVA: 0x0000634E File Offset: 0x0000454E
		public HurricanePikeSpecial(IActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00006362 File Offset: 0x00004562
		public void Dispose()
		{
			Unit.OnModifierAdded -= this.OnModifierAdded;
			Unit.OnModifierRemoved -= this.OnModifierRemoved;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00006386 File Offset: 0x00004586
		public override bool ShouldCast(Unit9 target)
		{
			return !target.IsLinkensProtected && !target.IsSpellShieldProtected && base.Ability.Owner.Distance(target) < target.GetAttackRange(null, 0f) + 100f;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00026464 File Offset: 0x00024664
		public override bool Use(Unit9 target)
		{
			if (!base.Ability.UseAbility(target, false, false))
			{
				return false;
			}
			Unit.OnModifierAdded += this.OnModifierAdded;
			base.OrbwalkSleeper.Sleep(base.Ability.Owner.Handle, base.Ability.GetCastDelay(target));
			base.AbilitySleeper.Sleep(base.Ability.Handle, base.Ability.GetHitTime(target) + 0.5f);
			return true;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000264E4 File Offset: 0x000246E4
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Handle == base.Ability.Owner.Handle && !(args.Modifier.Name != "modifier_item_hurricane_pike_range"))
				{
					foreach (OrbAbility orbAbility in base.Ability.Owner.Abilities.OfType<OrbAbility>())
					{
						if (!orbAbility.Enabled && orbAbility.CanBeCasted(true))
						{
							this.enabledOrbs.Add(orbAbility.Id);
							orbAbility.Enabled = true;
						}
					}
					Unit.OnModifierRemoved += this.OnModifierRemoved;
					Unit.OnModifierAdded -= this.OnModifierAdded;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
				Unit.OnModifierAdded -= this.OnModifierAdded;
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x000265E4 File Offset: 0x000247E4
		private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Handle == base.Ability.Owner.Handle && !(args.Modifier.Name != "modifier_item_hurricane_pike_range"))
				{
					foreach (OrbAbility orbAbility in from x in base.Ability.Owner.Abilities.OfType<OrbAbility>()
					where this.enabledOrbs.Contains(x.Id)
					select x)
					{
						orbAbility.Enabled = false;
					}
					Unit.OnModifierRemoved -= this.OnModifierRemoved;
				}
			}
			catch (Exception ex)
			{
				Unit.OnModifierRemoved -= this.OnModifierRemoved;
				Logger.Error(ex, null);
			}
		}

		// Token: 0x0400049A RID: 1178
		private readonly List<AbilityId> enabledOrbs = new List<AbilityId>();
	}
}
