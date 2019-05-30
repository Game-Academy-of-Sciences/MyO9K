using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace O9K.Hud.Modules.Map.AbilityMonitor
{
	// Token: 0x02000073 RID: 115
	[Serializable]
	internal class UnknownParticleException : Exception
	{
		// Token: 0x06000265 RID: 613 RVA: 0x00003824 File Offset: 0x00001A24
		public UnknownParticleException()
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00003837 File Offset: 0x00001A37
		public UnknownParticleException(string message) : base(message)
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000384B File Offset: 0x00001A4B
		public UnknownParticleException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00003860 File Offset: 0x00001A60
		protected UnknownParticleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00003875 File Offset: 0x00001A75
		public override IDictionary Data { get; } = new Dictionary<string, object>();
	}
}
