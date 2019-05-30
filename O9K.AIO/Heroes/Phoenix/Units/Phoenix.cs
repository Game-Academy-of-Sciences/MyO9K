using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Phoenix.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using SharpDX;

namespace O9K.AIO.Heroes.Phoenix.Units
{
	// Token: 0x020000D5 RID: 213
	[UnitName("npc_dota_hero_phoenix")]
	internal class Phoenix : ControllableUnit
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x000175BC File Offset: 0x000157BC
		public Phoenix(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.phoenix_icarus_dive,
					(ActiveAbility x) => this.dive = new IcarusDive(x)
				},
				{
					AbilityId.phoenix_launch_fire_spirit,
					(ActiveAbility x) => this.spirits = new FireSpirits(x)
				},
				{
					AbilityId.phoenix_sun_ray,
					(ActiveAbility x) => this.ray = new SunRay(x)
				},
				{
					AbilityId.phoenix_supernova,
					(ActiveAbility x) => this.nova = new Supernova(x)
				},
				{
					AbilityId.item_veil_of_discord,
					(ActiveAbility x) => this.veil = new DebuffAbility(x)
				},
				{
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new ShivasGuard(x)
				},
				{
					AbilityId.item_rod_of_atos,
					(ActiveAbility x) => this.atos = new DisableAbility(x)
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
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				},
				{
					AbilityId.item_heavens_halberd,
					(ActiveAbility x) => this.halberd = new DisableAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.phoenix_icarus_dive, (ActiveAbility _) => this.dive);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000176F0 File Offset: 0x000158F0
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			if (abilityHelper.UseAbilityIfCondition(this.dive, new UsableAbility[]
			{
				this.nova
			}))
			{
				if (abilityHelper.CanBeCasted(this.shiva, false, true, true, true))
				{
					this.shiva.ForceUseAbility(targetManager, base.ComboSleeper);
				}
				return true;
			}
			if (!comboModeMenu.IsHarassCombo && this.dive.AutoStop(targetManager))
			{
				base.ComboSleeper.Sleep(0.1f);
				return true;
			}
			if (abilityHelper.UseAbility(this.veil, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.hex, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.halberd, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.atos, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.urn, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.vessel, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.spirits, true))
			{
				return true;
			}
			if (abilityHelper.UseAbility(this.shiva, true))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.nova, false, true, true, true) && (this.dive.IsFlying || base.Owner.Distance(targetManager.Target) < 600f))
			{
				Modifier modifier = base.Owner.GetModifier("modifier_phoenix_fire_spirit_count");
				int val = (modifier != null) ? modifier.StackCount : 0;
				List<Unit9> list = (from x in targetManager.EnemyHeroes
				where x.Distance(base.Owner) < this.spirits.Ability.CastRange
				select x).ToList<Unit9>();
				int num = Math.Min(val, list.Count);
				for (int i = 0; i < num; i++)
				{
					this.spirits.Ability.UseAbility(list[i], 1, i != 0, false);
				}
				if (num > 0)
				{
					base.ComboSleeper.Sleep(0.2f);
					return true;
				}
				if ((base.Owner.Distance(targetManager.Target) < 600f || (this.dive.CastPosition != Vector3.Zero && base.Owner.Distance(this.dive.CastPosition) < this.nova.Ability.CastRange)) && abilityHelper.UseAbility(this.nova, true))
				{
					return true;
				}
			}
			return !this.dive.IsFlying && (!abilityHelper.CanBeCasted(this.spirits, true, true, true, true) || base.Owner.HasModifier("modifier_phoenix_fire_spirit_count")) && comboModeMenu.IsAbilityEnabled(this.ray.Ability) && this.ray.AutoControl(targetManager, base.ComboSleeper, 0.6f);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00017984 File Offset: 0x00015B84
		public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			bool result;
			try
			{
				if (this.ray.IsActive)
				{
					result = false;
				}
				else
				{
					result = base.Orbwalk(target, attack, move, comboMenu);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
				result = false;
			}
			return result;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000179CC File Offset: 0x00015BCC
		public void SunRayAllyCombo(TargetManager targetManager)
		{
			if (base.ComboSleeper.IsSleeping)
			{
				return;
			}
			if (this.ray.Ability.CanBeCasted(true))
			{
				this.ray.UseAbility(targetManager, base.ComboSleeper, true);
				return;
			}
			if (this.ray.IsActive)
			{
				this.ray.AutoControl(targetManager, base.ComboSleeper, 0.85f);
				return;
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00017A38 File Offset: 0x00015C38
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			if (base.MoveComboUseBlinks(abilityHelper))
			{
				return true;
			}
			if (abilityHelper.CanBeCasted(this.dive, false, false, true, true))
			{
				this.dive.Ability.UseAbility(Game.MousePosition, false, false);
				base.ComboSleeper.Sleep(0.3f);
				this.dive.Sleeper.Sleep(0.3f);
				return true;
			}
			if (this.dive.AutoStop(null))
			{
				base.ComboSleeper.Sleep(0.1f);
				return true;
			}
			return false;
		}

		// Token: 0x04000265 RID: 613
		private DisableAbility atos;

		// Token: 0x04000266 RID: 614
		private IcarusDive dive;

		// Token: 0x04000267 RID: 615
		private DisableAbility halberd;

		// Token: 0x04000268 RID: 616
		private DisableAbility hex;

		// Token: 0x04000269 RID: 617
		private Supernova nova;

		// Token: 0x0400026A RID: 618
		private SunRay ray;

		// Token: 0x0400026B RID: 619
		private ShivasGuard shiva;

		// Token: 0x0400026C RID: 620
		private FireSpirits spirits;

		// Token: 0x0400026D RID: 621
		private DebuffAbility urn;

		// Token: 0x0400026E RID: 622
		private DebuffAbility veil;

		// Token: 0x0400026F RID: 623
		private DebuffAbility vessel;
	}
}
