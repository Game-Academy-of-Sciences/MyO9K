using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace O9K.Hud.Modules.Map.AbilityMonitor
{
	// Token: 0x02000072 RID: 114
	[Serializable]
	internal class UnknownHeroParticleException : Exception
	{
		// Token: 0x06000260 RID: 608 RVA: 0x000037CB File Offset: 0x000019CB
		public UnknownHeroParticleException()
		{
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000037DE File Offset: 0x000019DE
		public UnknownHeroParticleException(string message) : base(message)
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000037F2 File Offset: 0x000019F2
		public UnknownHeroParticleException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00003807 File Offset: 0x00001A07
		protected UnknownHeroParticleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000381C File Offset: 0x00001A1C
		public override IDictionary Data { get; } = new Dictionary<string, object>();
	}
}
