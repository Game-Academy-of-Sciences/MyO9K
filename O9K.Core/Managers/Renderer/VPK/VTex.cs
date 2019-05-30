using System;
using System.Drawing;
using System.IO;
using System.Text;
using Ensage.SDK.VPK.Content;

namespace O9K.Core.Managers.Renderer.VPK
{
	// Token: 0x02000037 RID: 55
	internal class VTex : ResourceEntry
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00013188 File Offset: 0x00011388
		public VTex(BinaryReader reader) : base(reader)
		{
			reader.BaseStream.Position = 608L;
			bool yCoCg = false;
			for (int i = 0; i < 15; i++)
			{
				Encoding utf = Encoding.UTF8;
				int byteCount = utf.GetByteCount("e");
				string @string;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					for (;;)
					{
						byte[] array = new byte[byteCount];
						reader.Read(array, 0, byteCount);
						if (utf.GetString(array, 0, byteCount) == "\0")
						{
							break;
						}
						memoryStream.Write(array, 0, array.Length);
					}
					@string = utf.GetString(memoryStream.ToArray());
				}
				if (string.IsNullOrEmpty(@string))
				{
					reader.BaseStream.Position += 32L;
				}
				else if (!(@string == "CompileTexture") && @string == "Texture Compiler Version Image YCoCg Conversion")
				{
					yCoCg = true;
					break;
				}
			}
			reader.BaseStream.Position = base.StreamStartPosition + (long)((ulong)base.Offset);
			this.Version = reader.ReadUInt16();
			if (this.Version != 1)
			{
				throw new InvalidDataException(string.Format("invalid version {0}", this.Version));
			}
			this.Flags = reader.ReadUInt16();
			this.Reflectivity = new float[]
			{
				reader.ReadSingle(),
				reader.ReadSingle(),
				reader.ReadSingle(),
				reader.ReadSingle()
			};
			this.Width = reader.ReadUInt16();
			this.Height = reader.ReadUInt16();
			this.Depth = reader.ReadUInt16();
			this.Format = reader.ReadByte();
			this.NumMipLevels = reader.ReadByte();
			reader.BaseStream.Position = (long)((ulong)base.Data);
			VTexFormat format = this.Format;
			switch (format)
			{
			case VTexFormat.IMAGE_FORMAT_DXT1:
				for (ushort num = 0; num < this.Depth; num += 1)
				{
					if (num >= 255)
					{
						break;
					}
					byte b = this.NumMipLevels;
					while (b > 0 && b != 1)
					{
						int num2 = 0;
						while ((double)num2 < (double)this.Height / Math.Pow(2.0, (double)(b + 1)))
						{
							int num3 = 0;
							while ((double)num3 < (double)this.Width / Math.Pow(2.0, (double)(b + 1)))
							{
								reader.BaseStream.Position += 8L;
								num3++;
							}
							num2++;
						}
						b -= 1;
					}
					this.Bitmap = DDSImage.UncompressDXT1(reader, (int)this.Width, (int)this.Height);
				}
				goto IL_480;
			case VTexFormat.IMAGE_FORMAT_DXT5:
			{
				ushort num4 = 0;
				while (num4 < this.Depth && num4 < 255)
				{
					byte b2 = this.NumMipLevels;
					while (b2 > 0 && b2 != 1)
					{
						int num5 = 0;
						while ((double)num5 < (double)this.Height / Math.Pow(2.0, (double)(b2 + 1)))
						{
							int num6 = 0;
							while ((double)num6 < (double)this.Width / Math.Pow(2.0, (double)(b2 + 1)))
							{
								reader.BaseStream.Position += 16L;
								num6++;
							}
							num5++;
						}
						b2 -= 1;
					}
					this.Bitmap = DDSImage.UncompressDXT5(reader, (int)this.Width, (int)this.Height, yCoCg);
					num4 += 1;
				}
				goto IL_480;
			}
			case (VTexFormat)3:
				goto IL_480;
			case VTexFormat.IMAGE_FORMAT_RGBA8888:
				break;
			default:
				if (format == VTexFormat.IMAGE_FORMAT_RGBA16161616F)
				{
					this.Bitmap = DDSImage.ReadRGBA16161616F(reader, (int)this.Width, (int)this.Height);
					goto IL_480;
				}
				if (format != VTexFormat.IMAGE_FORMAT_PNG)
				{
					goto IL_480;
				}
				using (MemoryStream memoryStream2 = new MemoryStream(reader.ReadBytes((int)reader.BaseStream.Length)))
				{
					this.Bitmap = new Bitmap(memoryStream2);
					goto IL_480;
				}
				break;
			}
			for (ushort num7 = 0; num7 < this.Depth; num7 += 1)
			{
				if (num7 >= 255)
				{
					break;
				}
				byte b3 = this.NumMipLevels;
				while (b3 > 0 && b3 != 1)
				{
					int num8 = 0;
					while ((double)num8 < (double)this.Height / Math.Pow(2.0, (double)(b3 - 1)))
					{
						reader.BaseStream.Position += (long)((int)((double)(4 * this.Width) / Math.Pow(2.0, (double)(b3 - 1))));
						num8++;
					}
					b3 -= 1;
				}
				this.Bitmap = DDSImage.ReadRGBA8888(reader, (int)this.Width, (int)this.Height);
			}
			IL_480:
			reader.BaseStream.Position = base.StreamEndPosition;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00002C43 File Offset: 0x00000E43
		public Bitmap Bitmap { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00002C4B File Offset: 0x00000E4B
		public ushort Depth { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00002C53 File Offset: 0x00000E53
		public ushort Flags { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00002C5B File Offset: 0x00000E5B
		public VTexFormat Format { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00002C63 File Offset: 0x00000E63
		public byte NumMipLevels { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00002C6B File Offset: 0x00000E6B
		public ushort Height { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00002C73 File Offset: 0x00000E73
		public float[] Reflectivity { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00002C7B File Offset: 0x00000E7B
		public ushort Version { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00002C83 File Offset: 0x00000E83
		public ushort Width { get; }
	}
}
