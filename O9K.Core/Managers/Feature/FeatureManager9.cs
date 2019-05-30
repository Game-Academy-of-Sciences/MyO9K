using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;

namespace O9K.Core.Managers.Feature
{
	// Token: 0x02000023 RID: 35
	[Export(typeof(IFeatureManager9))]
	public sealed class FeatureManager9 : IFeatureManager9
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00010BC8 File Offset: 0x0000EDC8
		[ImportingConstructor]
		public FeatureManager9([ImportMany] IEnumerable<IO9KFeature> features)
		{
			foreach (IO9KFeature io9KFeature in features)
			{
				uint num;
				this.assemblyFeatures.TryGetValue(io9KFeature.AssemblyName, out num);
				this.assemblyFeatures[io9KFeature.AssemblyName] = (num | io9KFeature.FeatureFlags);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00010C48 File Offset: 0x0000EE48
		public bool IsFeatureActive(int feature)
		{
			uint num;
			this.assemblyFeatures.TryGetValue(Assembly.GetCallingAssembly().GetName().Name, out num);
			return ((ulong)num & (ulong)(1L << (feature & 31))) == (ulong)(1L << (feature & 31));
		}

		// Token: 0x04000057 RID: 87
		private readonly Dictionary<string, uint> assemblyFeatures = new Dictionary<string, uint>();
	}
}
