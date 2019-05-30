using System;
using System.IO;
using System.Text;

namespace O9K.Core.Managers.Renderer.VPK
{
	// Token: 0x02000035 RID: 53
	internal static class StreamHelpers
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00013024 File Offset: 0x00011224
		public static string ReadNullTermString(this BinaryReader stream, Encoding encoding)
		{
			int byteCount = encoding.GetByteCount("e");
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				for (;;)
				{
					byte[] array = new byte[byteCount];
					stream.Read(array, 0, byteCount);
					if (encoding.GetString(array, 0, byteCount) == "\0")
					{
						break;
					}
					memoryStream.Write(array, 0, array.Length);
				}
				@string = encoding.GetString(memoryStream.ToArray());
			}
			return @string;
		}
	}
}
