using System;
using Ensage;

namespace O9K.Core.Entities.Metadata
{
	// Token: 0x020000C5 RID: 197
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class AbilityIdAttribute : System.Attribute
	{
		// Token: 0x060005FA RID: 1530 RVA: 0x00005FD4 File Offset: 0x000041D4
		public AbilityIdAttribute(AbilityId abilityId)
		{
			this.AbilityId = abilityId;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00005FE3 File Offset: 0x000041E3
		public AbilityId AbilityId { get; }
	}
}
