using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace O9K.Core.Exceptions
{
	// Token: 0x020000AA RID: 170
	[Serializable]
	public class O9KException : Exception
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x00005035 File Offset: 0x00003235
		public O9KException()
		{
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00005048 File Offset: 0x00003248
		public O9KException(string message) : base(message)
		{
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000505C File Offset: 0x0000325C
		public O9KException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00005071 File Offset: 0x00003271
		protected O9KException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00005086 File Offset: 0x00003286
		public override IDictionary Data { get; } = new Dictionary<string, object>();
	}
}
