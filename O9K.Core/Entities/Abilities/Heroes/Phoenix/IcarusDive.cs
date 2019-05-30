using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Abilities.Heroes.Phoenix
{
	// Token: 0x020002F8 RID: 760
	[AbilityId(AbilityId.phoenix_icarus_dive)]
	public class IcarusDive : LineAbility
	{
		// Token: 0x06000D27 RID: 3367 RVA: 0x000264E8 File Offset: 0x000246E8
		public IcarusDive(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "dash_width");
			this.castRangeData = new SpecialData(baseAbility, "dash_length");
			this.DamageData = new SpecialData(baseAbility, "damage_per_second");
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x0000BBEA File Offset: 0x00009DEA
		public override bool HasAreaOfEffect { get; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0000BBF2 File Offset: 0x00009DF2
		public override float Speed { get; } = 1500f;

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0000BBFA File Offset: 0x00009DFA
		public bool IsFlying
		{
			get
			{
				return this.icarusDiveStop.IsUsable;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0000BC07 File Offset: 0x00009E07
		protected override float BaseCastRange
		{
			get
			{
				return this.castRangeData.GetValue(this.Level);
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0000BC1A File Offset: 0x00009E1A
		public override bool UseAbility(bool queue = false, bool bypass = false)
		{
			return this.icarusDiveStop.UseAbility(queue, bypass);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0002653C File Offset: 0x0002473C
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.phoenix_icarus_dive_stop)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				throw new ArgumentNullException("icarusDiveStop");
			}
			this.icarusDiveStop = (IcarusDiveStop)EntityManager9.AddAbility(ability);
		}

		// Token: 0x040006CB RID: 1739
		private readonly SpecialData castRangeData;

		// Token: 0x040006CC RID: 1740
		private IcarusDiveStop icarusDiveStop;
	}
}
