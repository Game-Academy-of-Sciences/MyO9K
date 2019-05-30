using System;
using System.ComponentModel.Composition;
using Ensage;
using Ensage.SDK.Renderer.Metadata;

namespace O9K.Core.Managers.Renderer.Metadata
{
	// Token: 0x02000039 RID: 57
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ExportRendererAttribute : ExportAttribute, IRendererMetadata
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00002FBF File Offset: 0x000011BF
		public ExportRendererAttribute(RenderMode mode) : base(typeof(IRenderer))
		{
			this.Mode = mode;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00002FD8 File Offset: 0x000011D8
		public RenderMode Mode { get; }
	}
}
