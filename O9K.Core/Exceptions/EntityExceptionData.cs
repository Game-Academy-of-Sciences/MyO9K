using System;
using Ensage;

namespace O9K.Core.Exceptions
{
	// Token: 0x020000A8 RID: 168
	[Serializable]
	public class EntityExceptionData
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x0001F854 File Offset: 0x0001DA54
		public EntityExceptionData(Entity entity)
		{
			try
			{
				this.Name = entity.Name;
				this.Team = entity.Team.ToString();
				Entity owner = entity.Owner;
				Entity owner2 = entity.Owner;
				if (owner2 != null && owner2.IsValid)
				{
					this.Owner = ((owner is Player) ? "Player" : owner.Name);
				}
				else
				{
					this.Owner = "null";
				}
			}
			catch
			{
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00004FED File Offset: 0x000031ED
		public string Name { get; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00004FF5 File Offset: 0x000031F5
		public string Owner { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00004FFD File Offset: 0x000031FD
		public string Team { get; }
	}
}
