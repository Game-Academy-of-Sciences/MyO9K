using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace O9K.Core.Exceptions
{
	// Token: 0x020000AC RID: 172
	[Serializable]
	internal class TextureNotFoundException : Exception
	{
		// Token: 0x060004D6 RID: 1238 RVA: 0x000050E7 File Offset: 0x000032E7
		public TextureNotFoundException()
		{
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000050FA File Offset: 0x000032FA
		public TextureNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000510E File Offset: 0x0000330E
		public TextureNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00005123 File Offset: 0x00003323
		protected TextureNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00005138 File Offset: 0x00003338
		public override IDictionary Data { get; } = new Dictionary<string, object>();
	}
}
