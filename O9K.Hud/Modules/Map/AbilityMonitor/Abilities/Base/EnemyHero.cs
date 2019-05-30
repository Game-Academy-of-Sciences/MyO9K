using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base
{
	// Token: 0x02000098 RID: 152
	internal class EnemyHero
	{
		// Token: 0x0600035F RID: 863 RVA: 0x000042C4 File Offset: 0x000024C4
		public EnemyHero(Unit9 unit)
		{
			this.Unit = unit;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000042D3 File Offset: 0x000024D3
		public Unit9 Unit { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000361 RID: 865 RVA: 0x000042DB File Offset: 0x000024DB
		// (set) Token: 0x06000362 RID: 866 RVA: 0x000042E3 File Offset: 0x000024E3
		public uint ObserversCount { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000042EC File Offset: 0x000024EC
		// (set) Token: 0x06000364 RID: 868 RVA: 0x000042F4 File Offset: 0x000024F4
		public uint SentryCount { get; set; }

		// Token: 0x06000365 RID: 869 RVA: 0x0001C20C File Offset: 0x0001A40C
		public uint CountWards(AbilityId id)
		{
			try
			{
				List<Item> source = this.Unit.BaseInventory.Items.Concat(this.Unit.BaseInventory.Backpack).ToList<Item>();
				return (uint)((from x in source
				where x.Id == id
				select x).Sum((Item x) => (long)((ulong)x.CurrentCharges)) + (from x in source
				where x.Id == AbilityId.item_ward_dispenser
				select x).Sum((Item x) => (long)((ulong)((id == AbilityId.item_ward_observer) ? x.CurrentCharges : x.SecondaryCharges))));
			}
			catch (Exception exception)
			{
				Unit9 unit = this.Unit;
				string str = (unit != null) ? unit.Name : null;
				string str2 = " ";
				Unit9 unit2 = this.Unit;
				Logger.Error(exception, str + str2 + (((unit2 != null) ? unit2.BaseInventory : null) == null).ToString());
			}
			return 0u;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001C318 File Offset: 0x0001A518
		public bool DroppedWard(AbilityId id)
		{
			return ObjectManager.GetEntitiesFast<PhysicalItem>().Any((PhysicalItem x) => (x.Item.Id == id || x.Item.Id == AbilityId.item_ward_dispenser) && x.Distance2D(this.Unit.Position) < 300f);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000042FD File Offset: 0x000024FD
		public uint GetWardsCount(AbilityId id)
		{
			if (id != AbilityId.item_ward_observer)
			{
				return this.SentryCount;
			}
			return this.ObserversCount;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00004311 File Offset: 0x00002511
		public void SetWardsCount(AbilityId id, uint count)
		{
			if (id == AbilityId.item_ward_observer)
			{
				this.ObserversCount = count;
				return;
			}
			this.SentryCount = count;
		}

		// Token: 0x0400023B RID: 571
		private const AbilityId DispenserId = AbilityId.item_ward_dispenser;

		// Token: 0x0400023C RID: 572
		private const AbilityId ObserverId = AbilityId.item_ward_observer;

		// Token: 0x0400023D RID: 573
		private const AbilityId SentryId = AbilityId.item_ward_sentry;
	}
}
