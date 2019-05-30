using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Heroes.Unique
{
	// Token: 0x020000D4 RID: 212
	[HeroId(HeroId.npc_dota_hero_windrunner)]
	public class Windranger : Hero9, IDisposable
	{
		// Token: 0x0600064A RID: 1610 RVA: 0x0002155C File Offset: 0x0001F75C
		public Windranger(Hero baseHero) : base(baseHero)
		{
			this.focusFireAttackSpeed = Ensage.Ability.GetAbilityDataById(AbilityId.windrunner_focusfire).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "bonus_attack_speed").Value;
			if (base.IsControllable)
			{
				Player.OnExecuteOrder += this.OnExecuteOrder;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x000063E8 File Offset: 0x000045E8
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x000063F0 File Offset: 0x000045F0
		public bool FocusFireActive { get; private set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x000063F9 File Offset: 0x000045F9
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x00006401 File Offset: 0x00004601
		public Unit9 FocusFireTarget { get; private set; }

		// Token: 0x0600064F RID: 1615 RVA: 0x0000640A File Offset: 0x0000460A
		public void Dispose()
		{
			Player.OnExecuteOrder -= this.OnExecuteOrder;
			Unit.OnModifierAdded -= this.OnModifierAdded;
			Unit.OnModifierRemoved -= this.OnModifierRemoved;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000215C8 File Offset: 0x0001F7C8
		internal override float GetAttackSpeed(Unit9 target = null)
		{
			float attackSpeed = base.GetAttackSpeed(target);
			if (!(target == null) && this.FocusFireActive)
			{
				Unit9 focusFireTarget = this.FocusFireTarget;
				if (!(((focusFireTarget != null) ? new uint?(focusFireTarget.Handle) : null) == target.Handle))
				{
					return attackSpeed - this.focusFireAttackSpeed;
				}
			}
			return attackSpeed;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00021638 File Offset: 0x0001F838
		private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
		{
			try
			{
				if (args.OrderId == OrderId.AbilityTarget && args.Ability.Id == AbilityId.windrunner_focusfire && args.Process)
				{
					this.FocusFireTarget = EntityManager9.GetUnit(args.Target.Handle);
					if (!(this.FocusFireTarget == null) && !this.FocusFireTarget.IsLinkensProtected && !this.FocusFireTarget.IsSpellShieldProtected)
					{
						Unit.OnModifierAdded += this.OnModifierAdded;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000216DC File Offset: 0x0001F8DC
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Handle == base.Handle && !(args.Modifier.Name != "modifier_windrunner_focusfire"))
				{
					this.FocusFireActive = true;
					Unit.OnModifierAdded -= this.OnModifierAdded;
					Unit.OnModifierRemoved += this.OnModifierRemoved;
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00021758 File Offset: 0x0001F958
		private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Handle == base.Handle && !(args.Modifier.Name != "modifier_windrunner_focusfire"))
				{
					this.FocusFireActive = false;
					Unit.OnModifierRemoved -= this.OnModifierRemoved;
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x040002DE RID: 734
		private readonly float focusFireAttackSpeed;
	}
}
