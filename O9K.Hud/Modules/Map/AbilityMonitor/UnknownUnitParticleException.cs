using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace O9K.Hud.Modules.Map.AbilityMonitor
{
	// Token: 0x02000071 RID: 113
	[Serializable]
	internal class UnknownUnitParticleException : Exception
	{
		// Token: 0x0600025B RID: 603 RVA: 0x00003772 File Offset: 0x00001972
		public UnknownUnitParticleException()
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00003785 File Offset: 0x00001985
		public UnknownUnitParticleException(string message) : base(message)
		{
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00003799 File Offset: 0x00001999
		public UnknownUnitParticleException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000037AE File Offset: 0x000019AE
		protected UnknownUnitParticleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600025F RID: 607 RVA: 0x000037C3 File Offset: 0x000019C3
		public override IDictionary Data { get; } = new Dictionary<string, object>();
	}
}
