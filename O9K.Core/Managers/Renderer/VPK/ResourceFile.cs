using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ensage.SDK.VPK.Content;

namespace O9K.Core.Managers.Renderer.VPK
{
	// Token: 0x02000034 RID: 52
	internal class ResourceFile
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00002BB3 File Offset: 0x00000DB3
		public ResourceFile(Stream stream) : this(new BinaryReader(stream))
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00012F64 File Offset: 0x00011164
		public ResourceFile(BinaryReader reader)
		{
			this.FileSize = reader.ReadUInt32();
			this.HeaderVersion = reader.ReadUInt16();
			this.Version = reader.ReadUInt16();
			long position = reader.BaseStream.Position;
			uint num = reader.ReadUInt32();
			uint num2 = reader.ReadUInt32();
			reader.BaseStream.Position = position + (long)((ulong)num);
			int num3 = 0;
			while ((long)num3 < (long)((ulong)num2))
			{
				string @string = Encoding.UTF8.GetString(reader.ReadBytes(4));
				if (@string == "DATA")
				{
					this.resourceEntries.Add(new VTex(reader));
				}
				else
				{
					this.resourceEntries.Add(new ResourceEntry(reader));
				}
				num3++;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00002BC1 File Offset: 0x00000DC1
		public uint FileSize { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00002BC9 File Offset: 0x00000DC9
		public ushort HeaderVersion { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00002BD1 File Offset: 0x00000DD1
		public IReadOnlyCollection<ResourceEntry> ResourceEntries
		{
			get
			{
				return this.resourceEntries.AsReadOnly();
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00002BDE File Offset: 0x00000DDE
		public ushort Version { get; }

		// Token: 0x0400008B RID: 139
		private readonly List<ResourceEntry> resourceEntries = new List<ResourceEntry>();
	}
}
