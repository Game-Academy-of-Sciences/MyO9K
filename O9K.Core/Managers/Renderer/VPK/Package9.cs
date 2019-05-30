using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O9K.Core.Logger;
using SteamDatabase.ValvePak;

namespace O9K.Core.Managers.Renderer.VPK
{
	// Token: 0x02000031 RID: 49
	internal class Package9
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000029F0 File Offset: 0x00000BF0
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000029F8 File Offset: 0x00000BF8
		public string FileName { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00002A01 File Offset: 0x00000C01
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00002A09 File Offset: 0x00000C09
		public uint Version { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00002A12 File Offset: 0x00000C12
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00002A1A File Offset: 0x00000C1A
		public uint TreeSize { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00002A23 File Offset: 0x00000C23
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00002A2B File Offset: 0x00000C2B
		public uint FileDataSectionSize { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00002A34 File Offset: 0x00000C34
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00002A3C File Offset: 0x00000C3C
		public uint ArchiveMD5SectionSize { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00002A45 File Offset: 0x00000C45
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00002A4D File Offset: 0x00000C4D
		public uint OtherMD5SectionSize { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00002A56 File Offset: 0x00000C56
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00002A5E File Offset: 0x00000C5E
		public uint SignatureSectionSize { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00002A67 File Offset: 0x00000C67
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00002A6F File Offset: 0x00000C6F
		public byte[] TreeChecksum { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00002A78 File Offset: 0x00000C78
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00002A80 File Offset: 0x00000C80
		public byte[] ArchiveMD5EntriesChecksum { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00002A89 File Offset: 0x00000C89
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00002A91 File Offset: 0x00000C91
		public byte[] WholeFileChecksum { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00002A9A File Offset: 0x00000C9A
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00002AA2 File Offset: 0x00000CA2
		public byte[] PublicKey { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00002AAB File Offset: 0x00000CAB
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00002AB3 File Offset: 0x00000CB3
		public byte[] Signature { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00002ABC File Offset: 0x00000CBC
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public Dictionary<string, List<PackageEntry>> Entries { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00002ACD File Offset: 0x00000CCD
		public Dictionary<string, PackageEntry> VtexEntries { get; } = new Dictionary<string, PackageEntry>();

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00002AD5 File Offset: 0x00000CD5
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00002ADD File Offset: 0x00000CDD
		public List<ArchiveMD5SectionEntry> ArchiveMD5Entries { get; private set; }

		// Token: 0x0600011E RID: 286 RVA: 0x00002AE6 File Offset: 0x00000CE6
		public PackageEntry FindEntry(string filePath)
		{
			filePath = ((filePath != null) ? filePath.Replace('\\', '/') : null);
			return this.FindEntry(Path.GetDirectoryName(filePath), filePath);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00002B07 File Offset: 0x00000D07
		public PackageEntry FindEntry(string directory, string fileName)
		{
			fileName = ((fileName != null) ? fileName.Replace('\\', '/') : null);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
			string extension = Path.GetExtension(fileName);
			return this.FindEntry(directory, fileNameWithoutExtension, (extension != null) ? extension.TrimStart(new char[]
			{
				'.'
			}) : null);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00012400 File Offset: 0x00010600
		public PackageEntry FindEntry(string directory, string fileName, string extension)
		{
			Package9.<>c__DisplayClass68_0 <>c__DisplayClass68_ = new Package9.<>c__DisplayClass68_0();
			<>c__DisplayClass68_.directory = directory;
			<>c__DisplayClass68_.fileName = fileName;
			if (extension == null)
			{
				extension = string.Empty;
			}
			if (!this.Entries.ContainsKey(extension))
			{
				return null;
			}
			Package9.<>c__DisplayClass68_0 <>c__DisplayClass68_2 = <>c__DisplayClass68_;
			string directory2 = <>c__DisplayClass68_.directory;
			<>c__DisplayClass68_2.directory = ((directory2 != null) ? directory2.Replace('\\', '/').Trim(new char[]
			{
				'/'
			}) : null);
			if (<>c__DisplayClass68_.directory == string.Empty)
			{
				<>c__DisplayClass68_.directory = null;
			}
			return this.Entries[extension].Find((PackageEntry x) => x.DirectoryName == <>c__DisplayClass68_.directory && x.FileName == <>c__DisplayClass68_.fileName);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000124A0 File Offset: 0x000106A0
		public PackageEntry FindVtexEntry(string filePath)
		{
			PackageEntry result;
			if (this.VtexEntries.TryGetValue(filePath, out result))
			{
				return result;
			}
			if (this.Entries == null)
			{
				return null;
			}
			return this.FindEntry(filePath);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000124D0 File Offset: 0x000106D0
		public void Read(string filename)
		{
			this.SetFileName(filename);
			FileStream input = new FileStream(string.Format("{0}{1}.vpk", this.FileName, this.IsDirVPK ? "_dir" : string.Empty), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			this.Read(input);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00012518 File Offset: 0x00010718
		public void Read(Stream input)
		{
			if (this.FileName == null)
			{
				throw new InvalidOperationException("If you call Read() directly with a stream, you must call SetFileName() first.");
			}
			this.Reader = new BinaryReader(input);
			if (this.Reader.ReadUInt32() != 1437209140u)
			{
				throw new InvalidDataException("Given file is not a VPK.");
			}
			this.Version = this.Reader.ReadUInt32();
			this.TreeSize = this.Reader.ReadUInt32();
			if (this.Version != 1u)
			{
				if (this.Version != 2u)
				{
					throw new InvalidDataException(string.Format("Bad VPK version. ({0})", this.Version));
				}
				this.FileDataSectionSize = this.Reader.ReadUInt32();
				this.ArchiveMD5SectionSize = this.Reader.ReadUInt32();
				this.OtherMD5SectionSize = this.Reader.ReadUInt32();
				this.SignatureSectionSize = this.Reader.ReadUInt32();
			}
			this.HeaderSize = (uint)input.Position;
			try
			{
				foreach (KeyValuePair<long, long> keyValuePair in new List<KeyValuePair<long, long>>
				{
					new KeyValuePair<long, long>(11690725L, 11696598L),
					new KeyValuePair<long, long>(12538664L, 12573406L),
					new KeyValuePair<long, long>(12579346L, 12587750L),
					new KeyValuePair<long, long>(12658344L, 12661126L),
					new KeyValuePair<long, long>(12676332L, 12741266L),
					new KeyValuePair<long, long>(12752733L, 12754970L)
				})
				{
					this.ReadFolder(keyValuePair.Key, keyValuePair.Value);
				}
			}
			catch
			{
				Logger.Warn("O9K // Failed to get textures");
				this.Reader.BaseStream.Seek(28L, SeekOrigin.Begin);
				this.ReadEntries();
			}
			if (this.Version == 2u)
			{
				input.Position += (long)((ulong)this.FileDataSectionSize);
				this.ReadArchiveMD5Section();
				this.ReadOtherMD5Section();
				this.ReadSignatureSection();
			}
			this.Reader.Dispose();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00012750 File Offset: 0x00010950
		public void ReadEntry(PackageEntry entry, out byte[] output)
		{
			output = new byte[(long)entry.SmallData.Length + (long)((ulong)entry.Length)];
			if (entry.SmallData.Length != 0)
			{
				entry.SmallData.CopyTo(output, 0);
			}
			if (entry.Length > 0u)
			{
				Stream stream = null;
				try
				{
					uint num = entry.Offset;
					if (entry.ArchiveIndex != 32767)
					{
						if (!this.IsDirVPK)
						{
							throw new InvalidOperationException("Given VPK is not a _dir, but entry is referencing an external archive.");
						}
						stream = new FileStream(string.Format("{0}_{1:D3}.vpk", this.FileName, entry.ArchiveIndex), FileMode.Open, FileAccess.Read);
					}
					else
					{
						stream = this.Reader.BaseStream;
						num += this.HeaderSize + this.TreeSize;
					}
					stream.Seek((long)((ulong)num), SeekOrigin.Begin);
					stream.Read(output, entry.SmallData.Length, (int)entry.Length);
				}
				finally
				{
					if (entry.ArchiveIndex != 32767 && stream != null)
					{
						stream.Close();
					}
				}
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00012850 File Offset: 0x00010A50
		public void SetFileName(string fileName)
		{
			if (fileName.EndsWith(".vpk", StringComparison.Ordinal))
			{
				fileName = fileName.Substring(0, fileName.Length - 4);
			}
			if (fileName.EndsWith("_dir", StringComparison.Ordinal))
			{
				this.IsDirVPK = true;
				fileName = fileName.Substring(0, fileName.Length - 4);
			}
			this.FileName = fileName;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00002B45 File Offset: 0x00000D45
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.Reader != null)
			{
				this.Reader.Dispose();
				this.Reader = null;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000128AC File Offset: 0x00010AAC
		private void GetFolderBytes()
		{
			Dictionary<string, List<PackageEntry>> dictionary = new Dictionary<string, List<PackageEntry>>();
			for (;;)
			{
				string text = this.Reader.ReadNullTermString(Encoding.UTF8);
				if (text == string.Empty)
				{
					goto IL_280;
				}
				if (text == " ")
				{
					text = string.Empty;
				}
				List<PackageEntry> list = new List<PackageEntry>();
				for (;;)
				{
					long position = this.Reader.BaseStream.Position;
					string directoryName = this.Reader.ReadNullTermString(Encoding.UTF8);
					if (directoryName == string.Empty)
					{
						break;
					}
					if (directoryName == " ")
					{
						directoryName = null;
					}
					Func<string, bool> <>9__0;
					for (;;)
					{
						string text2 = this.Reader.ReadNullTermString(Encoding.UTF8);
						if (text2 == string.Empty)
						{
							break;
						}
						List<string> list2 = new List<string>
						{
							"panorama/images/spellicons",
							"panorama/images/items",
							"panorama/images/heroes",
							"panorama/images/control_icons",
							"panorama/images/hud",
							"panorama/images/textures",
							"panorama/images/masks",
							"panorama/images/hero_selection"
						};
						bool flag;
						if (text == "vtex_c")
						{
							IEnumerable<string> source = list2;
							Func<string, bool> predicate;
							if ((predicate = <>9__0) == null)
							{
								predicate = (<>9__0 = delegate(string x)
								{
									string directoryName = directoryName;
									return directoryName != null && directoryName.StartsWith(x);
								});
							}
							flag = source.Any(predicate);
						}
						else
						{
							flag = false;
						}
						bool flag2 = flag;
						if (flag2 && !this.reading)
						{
							this.startPosition = position;
							this.reading = true;
						}
						else if (!flag2 && this.reading)
						{
							Console.WriteLine(string.Format("new KeyValuePair<long, long>({0}, {1}),", this.startPosition, position));
							this.reading = false;
						}
						PackageEntry packageEntry = new PackageEntry
						{
							FileName = text2,
							DirectoryName = directoryName,
							TypeName = text,
							CRC32 = this.Reader.ReadUInt32(),
							SmallData = new byte[(int)this.Reader.ReadUInt16()],
							ArchiveIndex = this.Reader.ReadUInt16(),
							Offset = this.Reader.ReadUInt32(),
							Length = this.Reader.ReadUInt32()
						};
						if (this.Reader.ReadUInt16() != 65535)
						{
							goto Block_12;
						}
						if (packageEntry.SmallData.Length != 0)
						{
							this.Reader.Read(packageEntry.SmallData, 0, packageEntry.SmallData.Length);
						}
						list.Add(packageEntry);
					}
				}
				dictionary.Add(text, list);
			}
			Block_12:
			throw new FormatException("Invalid terminator.");
			IL_280:
			this.Entries = dictionary;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00012B40 File Offset: 0x00010D40
		private void ReadArchiveMD5Section()
		{
			this.ArchiveMD5Entries = new List<ArchiveMD5SectionEntry>();
			if (this.ArchiveMD5SectionSize == 0u)
			{
				return;
			}
			uint num = this.ArchiveMD5SectionSize / 28u;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				this.ArchiveMD5Entries.Add(new ArchiveMD5SectionEntry
				{
					ArchiveIndex = this.Reader.ReadUInt32(),
					Offset = this.Reader.ReadUInt32(),
					Length = this.Reader.ReadUInt32(),
					Checksum = this.Reader.ReadBytes(16)
				});
				num2++;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00012BD0 File Offset: 0x00010DD0
		private void ReadEntries()
		{
			Dictionary<string, List<PackageEntry>> dictionary = new Dictionary<string, List<PackageEntry>>();
			for (;;)
			{
				string text = this.Reader.ReadNullTermString(Encoding.UTF8);
				if (text == string.Empty)
				{
					goto IL_165;
				}
				if (text == " ")
				{
					text = string.Empty;
				}
				List<PackageEntry> list = new List<PackageEntry>();
				for (;;)
				{
					string text2 = this.Reader.ReadNullTermString(Encoding.UTF8);
					if (text2 == string.Empty)
					{
						break;
					}
					if (text2 == " ")
					{
						text2 = null;
					}
					for (;;)
					{
						string text3 = this.Reader.ReadNullTermString(Encoding.UTF8);
						if (text3 == string.Empty)
						{
							break;
						}
						PackageEntry packageEntry = new PackageEntry
						{
							FileName = text3,
							DirectoryName = text2,
							TypeName = text,
							CRC32 = this.Reader.ReadUInt32(),
							SmallData = new byte[(int)this.Reader.ReadUInt16()],
							ArchiveIndex = this.Reader.ReadUInt16(),
							Offset = this.Reader.ReadUInt32(),
							Length = this.Reader.ReadUInt32()
						};
						if (this.Reader.ReadUInt16() != 65535)
						{
							goto Block_6;
						}
						if (packageEntry.SmallData.Length != 0)
						{
							this.Reader.Read(packageEntry.SmallData, 0, packageEntry.SmallData.Length);
						}
						list.Add(packageEntry);
					}
				}
				dictionary.Add(text, list);
			}
			Block_6:
			throw new FormatException("Invalid terminator.");
			IL_165:
			this.Entries = dictionary;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00012D4C File Offset: 0x00010F4C
		private void ReadFolder(long start, long end)
		{
			this.Reader.BaseStream.Seek(start, SeekOrigin.Begin);
			IL_132:
			while (this.Reader.BaseStream.Position < end)
			{
				string text = this.Reader.ReadNullTermString(Encoding.UTF8);
				for (;;)
				{
					string text2 = this.Reader.ReadNullTermString(Encoding.UTF8);
					if (text2.Length == 0)
					{
						goto IL_132;
					}
					PackageEntry packageEntry = new PackageEntry
					{
						FileName = text2,
						DirectoryName = text,
						TypeName = "vtex_c",
						CRC32 = this.Reader.ReadUInt32(),
						SmallData = new byte[(int)this.Reader.ReadUInt16()],
						ArchiveIndex = this.Reader.ReadUInt16(),
						Offset = this.Reader.ReadUInt32(),
						Length = this.Reader.ReadUInt32()
					};
					if (this.Reader.ReadUInt16() != 65535)
					{
						break;
					}
					if (packageEntry.SmallData.Length != 0)
					{
						this.Reader.Read(packageEntry.SmallData, 0, packageEntry.SmallData.Length);
					}
					string key = text.Replace("/", "\\") + "\\" + text2 + ".vtex_c";
					this.VtexEntries[key] = packageEntry;
				}
				throw new FormatException("Invalid terminator.");
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00012EA4 File Offset: 0x000110A4
		private void ReadOtherMD5Section()
		{
			if (this.OtherMD5SectionSize != 48u)
			{
				throw new InvalidDataException(string.Format("Encountered OtherMD5Section with size of {0} (should be 48)", this.OtherMD5SectionSize));
			}
			this.TreeChecksum = this.Reader.ReadBytes(16);
			this.ArchiveMD5EntriesChecksum = this.Reader.ReadBytes(16);
			this.WholeFileChecksum = this.Reader.ReadBytes(16);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00012F10 File Offset: 0x00011110
		private void ReadSignatureSection()
		{
			if (this.SignatureSectionSize == 0u)
			{
				return;
			}
			int count = this.Reader.ReadInt32();
			this.PublicKey = this.Reader.ReadBytes(count);
			int count2 = this.Reader.ReadInt32();
			this.Signature = this.Reader.ReadBytes(count2);
		}

		// Token: 0x04000071 RID: 113
		public const char DirectorySeparatorChar = '/';

		// Token: 0x04000072 RID: 114
		public const int MAGIC = 1437209140;

		// Token: 0x04000073 RID: 115
		private uint HeaderSize;

		// Token: 0x04000074 RID: 116
		private bool IsDirVPK;

		// Token: 0x04000075 RID: 117
		private BinaryReader Reader;

		// Token: 0x04000076 RID: 118
		private bool reading;

		// Token: 0x04000077 RID: 119
		private long startPosition;
	}
}
