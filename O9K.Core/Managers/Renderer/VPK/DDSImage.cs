using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace O9K.Core.Managers.Renderer.VPK
{
	// Token: 0x02000030 RID: 48
	internal static class DDSImage
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00011C80 File Offset: 0x0000FE80
		public static Bitmap ReadRGBA16161616F(BinaryReader r, int w, int h)
		{
			Bitmap bitmap = new Bitmap(w, h);
			while (h-- > 0)
			{
				while (w-- > 0)
				{
					int red = (int)r.ReadDouble();
					int green = (int)r.ReadDouble();
					int blue = (int)r.ReadDouble();
					int alpha = (int)r.ReadDouble();
					bitmap.SetPixel(w, h, Color.FromArgb(alpha, red, green, blue));
				}
			}
			return bitmap;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00011CE0 File Offset: 0x0000FEE0
		public static Bitmap ReadRGBA8888(BinaryReader r, int w, int h)
		{
			Bitmap bitmap = new Bitmap(w, h);
			for (int i = 0; i < h; i++)
			{
				for (int j = 0; j < w; j++)
				{
					int num = r.ReadInt32();
					Color color = Color.FromArgb(num >> 24 & 255, num & 255, num >> 8 & 255, num >> 16 & 255);
					bitmap.SetPixel(j, i, color);
				}
			}
			return bitmap;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00011D4C File Offset: 0x0000FF4C
		public static Bitmap UncompressDXT1(BinaryReader r, int w, int h)
		{
			Rectangle rect = new Rectangle(0, 0, w, h);
			Bitmap bitmap = new Bitmap(w, h, PixelFormat.Format32bppRgb);
			int num = (w + 3) / 4;
			int num2 = (h + 3) / 4;
			BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
			byte[] array = new byte[bitmapData.Stride * bitmapData.Height];
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					byte[] blockStorage = r.ReadBytes(8);
					DDSImage.DecompressBlockDXT1(j * 4, i * 4, w, blockStorage, ref array, bitmapData.Stride);
				}
			}
			Marshal.Copy(array, 0, bitmapData.Scan0, array.Length);
			bitmap.UnlockBits(bitmapData);
			return bitmap;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00011E04 File Offset: 0x00010004
		public static Bitmap UncompressDXT5(BinaryReader r, int w, int h, bool yCoCg)
		{
			Rectangle rect = new Rectangle(0, 0, w, h);
			Bitmap bitmap = new Bitmap(w, h, PixelFormat.Format32bppArgb);
			int num = (w + 3) / 4;
			int num2 = (h + 3) / 4;
			BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
			byte[] array = new byte[bitmapData.Stride * bitmapData.Height];
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					byte[] blockStorage = r.ReadBytes(16);
					DDSImage.DecompressBlockDXT5(j * 4, i * 4, w, blockStorage, ref array, bitmapData.Stride, yCoCg);
				}
			}
			Marshal.Copy(array, 0, bitmapData.Scan0, array.Length);
			bitmap.UnlockBits(bitmapData);
			return bitmap;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000029D8 File Offset: 0x00000BD8
		private static byte ClampColor(int a)
		{
			if (a > 255)
			{
				return byte.MaxValue;
			}
			if (a >= 0)
			{
				return (byte)a;
			}
			return 0;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00011EBC File Offset: 0x000100BC
		private static void DecompressBlockDXT1(int x, int y, int width, byte[] blockStorage, ref byte[] pixels, int stride)
		{
			ushort num = (ushort)((int)blockStorage[0] | (int)blockStorage[1] << 8);
			ushort num2 = (ushort)((int)blockStorage[2] | (int)blockStorage[3] << 8);
			int num3 = (num >> 11) * 255 + 16;
			byte b = (byte)((num3 / 32 + num3) / 32);
			num3 = ((num & 2016) >> 5) * 255 + 32;
			byte b2 = (byte)((num3 / 64 + num3) / 64);
			num3 = (int)((num & 31) * 255 + 16);
			byte b3 = (byte)((num3 / 32 + num3) / 32);
			num3 = (num2 >> 11) * 255 + 16;
			byte b4 = (byte)((num3 / 32 + num3) / 32);
			num3 = ((num2 & 2016) >> 5) * 255 + 32;
			byte b5 = (byte)((num3 / 64 + num3) / 64);
			num3 = (int)((num2 & 31) * 255 + 16);
			byte b6 = (byte)((num3 / 32 + num3) / 32);
			uint num4 = (uint)blockStorage[4];
			uint num5 = (uint)((uint)blockStorage[5] << 8);
			uint num6 = (uint)((uint)blockStorage[6] << 16);
			uint num7 = (uint)((uint)blockStorage[7] << 24);
			uint num8 = num4 | num5 | num6 | num7;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					byte b7 = (byte)(num8 >> 2 * (4 * i + j) & 3u);
					byte b8 = 0;
					byte b9 = 0;
					byte b10 = 0;
					switch (b7)
					{
					case 0:
						b8 = b;
						b9 = b2;
						b10 = b3;
						break;
					case 1:
						b8 = b4;
						b9 = b5;
						b10 = b6;
						break;
					case 2:
						if (num > num2)
						{
							b8 = (2 * b + b4) / 3;
							b9 = (2 * b2 + b5) / 3;
							b10 = (2 * b3 + b6) / 3;
						}
						else
						{
							b8 = (b + b4) / 2;
							b9 = (b2 + b5) / 2;
							b10 = (b3 + b6) / 2;
						}
						break;
					case 3:
						if (num >= num2)
						{
							b8 = (2 * b4 + b) / 3;
							b9 = (2 * b5 + b2) / 3;
							b10 = (2 * b6 + b3) / 3;
						}
						break;
					}
					if (x + j < width)
					{
						int num9 = (y + i) * stride + (x + j) * 4;
						pixels[num9] = b10;
						pixels[num9 + 1] = b9;
						pixels[num9 + 2] = b8;
					}
				}
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000120CC File Offset: 0x000102CC
		private static void DecompressBlockDXT5(int x, int y, int width, byte[] blockStorage, ref byte[] pixels, int stride, bool yCoCg)
		{
			byte b = blockStorage[0];
			byte b2 = blockStorage[1];
			uint num = (uint)blockStorage[4];
			uint num2 = (uint)((uint)blockStorage[5] << 8);
			uint num3 = (uint)((uint)blockStorage[6] << 16);
			uint num4 = (uint)((uint)blockStorage[7] << 24);
			uint num5 = num | num2 | num3 | num4;
			ushort num6 = (ushort)((int)blockStorage[2] | (int)blockStorage[3] << 8);
			ushort num7 = (ushort)((int)blockStorage[8] | (int)blockStorage[9] << 8);
			ushort num8 = (ushort)((int)blockStorage[10] | (int)blockStorage[11] << 8);
			int num9 = (num7 >> 11) * 255 + 16;
			byte b3 = (byte)((num9 / 32 + num9) / 32);
			num9 = ((num7 & 2016) >> 5) * 255 + 32;
			byte b4 = (byte)((num9 / 64 + num9) / 64);
			num9 = (int)((num7 & 31) * 255 + 16);
			byte b5 = (byte)((num9 / 32 + num9) / 32);
			num9 = (num8 >> 11) * 255 + 16;
			byte b6 = (byte)((num9 / 32 + num9) / 32);
			num9 = ((num8 & 2016) >> 5) * 255 + 32;
			byte b7 = (byte)((num9 / 64 + num9) / 64);
			num9 = (int)((num8 & 31) * 255 + 16);
			byte b8 = (byte)((num9 / 32 + num9) / 32);
			uint num10 = (uint)blockStorage[12];
			uint num11 = (uint)((uint)blockStorage[13] << 8);
			uint num12 = (uint)((uint)blockStorage[14] << 16);
			uint num13 = (uint)((uint)blockStorage[15] << 24);
			uint num14 = num10 | num11 | num12 | num13;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					int num15 = 3 * (4 * i + j);
					int num16;
					if (num15 <= 12)
					{
						num16 = (num6 >> num15 & 7);
					}
					else if (num15 == 15)
					{
						num16 = (int)((uint)num6 >> 15 | (num5 << 1 & 6u));
					}
					else
					{
						num16 = (int)(num5 >> num15 - 16 & 7u);
					}
					byte b9;
					if (num16 == 0)
					{
						b9 = b;
					}
					else if (num16 == 1)
					{
						b9 = b2;
					}
					else if (b > b2)
					{
						b9 = (byte)(((8 - num16) * (int)b + (num16 - 1) * (int)b2) / 7);
					}
					else if (num16 == 6)
					{
						b9 = 0;
					}
					else if (num16 == 7)
					{
						b9 = byte.MaxValue;
					}
					else
					{
						b9 = (byte)(((6 - num16) * (int)b + (num16 - 1) * (int)b2) / 5);
					}
					byte b10 = (byte)(num14 >> 2 * (4 * i + j) & 3u);
					byte b11 = 0;
					byte b12 = 0;
					byte b13 = 0;
					switch (b10)
					{
					case 0:
						b11 = b3;
						b12 = b4;
						b13 = b5;
						break;
					case 1:
						b11 = b6;
						b12 = b7;
						b13 = b8;
						break;
					case 2:
						b11 = (2 * b3 + b6) / 3;
						b12 = (2 * b4 + b7) / 3;
						b13 = (2 * b5 + b8) / 3;
						break;
					case 3:
						b11 = (2 * b6 + b3) / 3;
						b12 = (2 * b7 + b4) / 3;
						b13 = (2 * b8 + b5) / 3;
						break;
					}
					if (x + j < width)
					{
						if (yCoCg)
						{
							int num17 = (b13 >> 3) + 1;
							int num18 = (int)(b11 - 128) / num17;
							int num19 = (int)(b12 - 128) / num17;
							b11 = DDSImage.ClampColor((int)b9 + num18 - num19);
							b12 = DDSImage.ClampColor((int)b9 + num19);
							b13 = DDSImage.ClampColor((int)b9 - num18 - num19);
						}
						int num20 = (y + i) * stride + (x + j) * 4;
						pixels[num20] = b13;
						pixels[num20 + 1] = b12;
						pixels[num20 + 2] = b11;
						pixels[num20 + 3] = byte.MaxValue;
					}
				}
			}
		}
	}
}
