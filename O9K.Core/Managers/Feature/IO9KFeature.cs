using System;
using System.ComponentModel.Composition;

namespace O9K.Core.Managers.Feature
{
	// Token: 0x02000024 RID: 36
	[InheritedExport]
	public interface IO9KFeature
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A5 RID: 165
		string AssemblyName { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A6 RID: 166
		uint FeatureFlags { get; }
	}
}
