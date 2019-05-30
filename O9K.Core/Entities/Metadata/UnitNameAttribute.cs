using System;

namespace O9K.Core.Entities.Metadata
{
	// Token: 0x020000C4 RID: 196
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class UnitNameAttribute : Attribute
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x00005FBD File Offset: 0x000041BD
		public UnitNameAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00005FCC File Offset: 0x000041CC
		public string Name { get; }
	}
}
