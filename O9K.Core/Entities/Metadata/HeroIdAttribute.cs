using System;
using Ensage;

namespace O9K.Core.Entities.Metadata
{
	// Token: 0x020000C6 RID: 198
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class HeroIdAttribute : System.Attribute
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x00005FEB File Offset: 0x000041EB
		public HeroIdAttribute(HeroId heroId)
		{
			this.HeroId = heroId;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00005FFA File Offset: 0x000041FA
		public HeroId HeroId { get; }
	}
}
