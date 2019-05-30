using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.PhantomLancer.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.PhantomLancer.Units
{
	// Token: 0x020000DF RID: 223
	[UnitName("npc_dota_hero_phantom_lancer")]
	internal class PhantomLancer : ControllableUnit
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x00018184 File Offset: 0x00016384
		public PhantomLancer(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.phantom_lancer_spirit_lance,
					(ActiveAbility x) => this.lance = new NukeAbility(x)
				},
				{
					AbilityId.phantom_lancer_doppelwalk,
					(ActiveAbility x) => this.doppel = new Doppelganger(x)
				},
				{
					AbilityId.item_diffusal_blade,
					(ActiveAbility x) => this.diffusal = new DebuffAbility(x)
				},
				{
					AbilityId.item_abyssal_blade,
					(ActiveAbility x) => this.abyssal = new DisableAbility(x)
				},
				{
					AbilityId.item_manta,
					(ActiveAbility x) => this.manta = new BuffAbility(x)
				},
				{
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.bloodthorn = new Bloodthorn(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.phantom_lancer_doppelwalk, (ActiveAbility x) => this.doppelBlink = new BlinkAbility(x));
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000466B File Offset: 0x0000286B
		protected override int BodyBlockRange { get; } = 80;

		// Token: 0x06000484 RID: 1156 RVA: 0x00018258 File Offset: 0x00016458
		public override bool CanAttack(Unit9 target, float additionalRange = 0f)
		{
			ToggleAbility toggleAbility = base.Owner.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.phantom_lancer_phantom_edge) as ToggleAbility;
			if (toggleAbility != null && !toggleAbility.Enabled && toggleAbility.CanBeCasted(true))
			{
				additionalRange = toggleAbility.Range;
			}
			return base.CanAttack(target, additionalRange);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00004673 File Offset: 0x00002873
		public override bool CanMove()
		{
			return base.CanMove() && !base.Owner.HasModifier("modifier_phantom_lancer_phantom_edge_boost");
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000182C0 File Offset: 0x000164C0
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			if (base.Owner.HasModifier("modifier_phantom_lancer_phantom_edge_boost"))
			{
				return false;
			}
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.abyssal, true) || abilityHelper.UseAbility(this.bloodthorn, true) || abilityHelper.UseAbility(this.diffusal, true) || abilityHelper.UseAbility(this.lance, true) || abilityHelper.UseAbility(this.doppel, true) || abilityHelper.UseAbility(this.manta, base.Owner.GetAttackRange(null, 0f));
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00018364 File Offset: 0x00016564
		protected override bool Attack(Unit9 target, ComboModeMenu comboMenu)
		{
			if (base.Owner.Distance(target) > base.Owner.GetAttackRange(target, 200f) && base.Owner.BaseUnit.Attack(target.BaseUnit))
			{
				base.AttackSleeper.Sleep(0.5f);
				base.MoveSleeper.Sleep(0.5f);
				return true;
			}
			return base.Attack(target, comboMenu);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00004694 File Offset: 0x00002894
		protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBlinks(abilityHelper) || abilityHelper.UseMoveAbility(this.doppelBlink);
		}

		// Token: 0x0400027B RID: 635
		private DisableAbility abyssal;

		// Token: 0x0400027C RID: 636
		private DisableAbility bloodthorn;

		// Token: 0x0400027D RID: 637
		private DebuffAbility diffusal;

		// Token: 0x0400027E RID: 638
		private Doppelganger doppel;

		// Token: 0x0400027F RID: 639
		private BlinkAbility doppelBlink;

		// Token: 0x04000280 RID: 640
		private NukeAbility lance;

		// Token: 0x04000281 RID: 641
		private BuffAbility manta;
	}
}
