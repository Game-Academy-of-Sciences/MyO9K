using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace O9K.Core.Exceptions
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	public class BrokenAbilityException : Exception
	{
		// Token: 0x060004D1 RID: 1233 RVA: 0x0000508E File Offset: 0x0000328E
		public BrokenAbilityException()
		{
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000050A1 File Offset: 0x000032A1
		public BrokenAbilityException(string message) : base(message)
		{
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000050B5 File Offset: 0x000032B5
		public BrokenAbilityException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000050CA File Offset: 0x000032CA
		protected BrokenAbilityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x000050DF File Offset: 0x000032DF
		public override IDictionary Data { get; } = new Dictionary<string, object>();
	}
}
