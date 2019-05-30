using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Range;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000172 RID: 370
	[AbilityId(AbilityId.item_hurricane_pike)]
	public class HurricanePike : RangedAbility, IBlink, IHasRangeIncrease, IActiveAbility
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x00021A4C File Offset: 0x0001FC4C
		public HurricanePike(Ability baseAbility) : base(baseAbility)
		{
			this.attackRange = new SpecialData(baseAbility, "base_attack_range");
			this.RangeData = new SpecialData(baseAbility, "push_length");
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x00006E98 File Offset: 0x00005098
		public override float Range
		{
			get
			{
				return this.RangeData.GetValue(this.Level);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00006EE1 File Offset: 0x000050E1
		public override float Speed { get; } = 1200f;

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00006EE9 File Offset: 0x000050E9
		public BlinkType BlinkType { get; } = 1;

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x00006EF1 File Offset: 0x000050F1
		public bool IsRangeIncreasePermanent { get; } = 1;

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00006EF9 File Offset: 0x000050F9
		public float RangeIncrease
		{
			get
			{
				if (!base.IsActive || !base.Owner.IsRanged)
				{
					return 0f;
				}
				return this.attackRange.GetValue(this.Level);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x00006F27 File Offset: 0x00005127
		public RangeIncreaseType RangeIncreaseType { get; } = 2;

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00006F2F File Offset: 0x0000512F
		public string RangeModifierName { get; } = "modifier_item_hurricane_pike";

		// Token: 0x0600076E RID: 1902 RVA: 0x00006F37 File Offset: 0x00005137
		public override bool CanHit(Unit9 target)
		{
			return base.CanHit(target) && (base.Owner.IsAlly(target) || base.Owner.Distance(target) <= this.CastRange / 2f);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00006F6F File Offset: 0x0000516F
		public override void Dispose()
		{
			base.Dispose();
			Player.OnExecuteOrder -= this.OnExecuteOrder;
			Unit.OnModifierRemoved -= this.OnModifierRemoved;
			Unit.OnModifierAdded -= this.OnModifierAdded;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00006EBB File Offset: 0x000050BB
		public override float GetHitTime(Vector3 position)
		{
			return this.GetCastDelay(position) + this.ActivationDelay + this.Range / this.Speed;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00006FAA File Offset: 0x000051AA
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			if (base.IsControllable)
			{
				Player.OnExecuteOrder += this.OnExecuteOrder;
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00021AB0 File Offset: 0x0001FCB0
		private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
		{
			try
			{
				if (!this.subbed && args.OrderId == OrderId.AbilityTarget && args.Ability.Id == base.Id && args.Process)
				{
					this.pikeTarget = EntityManager9.GetUnit(args.Target.Handle);
					if (!(this.pikeTarget == null) && !this.pikeTarget.IsLinkensProtected && !this.pikeTarget.IsSpellShieldProtected)
					{
						Unit.OnModifierAdded += this.OnModifierAdded;
						this.subbed = true;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00021B64 File Offset: 0x0001FD64
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Handle == base.Owner.Handle && !(args.Modifier.Name != "modifier_item_hurricane_pike_range"))
				{
					base.Owner.HurricanePikeTarget = this.pikeTarget;
					Unit.OnModifierAdded -= this.OnModifierAdded;
					Unit.OnModifierRemoved += this.OnModifierRemoved;
				}
			}
			catch (Exception exception)
			{
				Unit.OnModifierAdded -= this.OnModifierAdded;
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00021C00 File Offset: 0x0001FE00
		private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				if (sender.Handle == base.Owner.Handle && !(args.Modifier.Name != "modifier_item_hurricane_pike_range"))
				{
					base.Owner.HurricanePikeTarget = null;
					Unit.OnModifierRemoved -= this.OnModifierRemoved;
					this.subbed = false;
				}
			}
			catch (Exception exception)
			{
				Unit.OnModifierRemoved -= this.OnModifierRemoved;
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0400034E RID: 846
		private readonly SpecialData attackRange;

		// Token: 0x0400034F RID: 847
		private Unit9 pikeTarget;

		// Token: 0x04000350 RID: 848
		private bool subbed;
	}
}
