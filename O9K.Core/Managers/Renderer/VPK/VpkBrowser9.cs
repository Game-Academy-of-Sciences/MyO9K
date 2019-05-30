using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Linq;
using Ensage;
using SteamDatabase.ValvePak;

namespace O9K.Core.Managers.Renderer.VPK
{
	// Token: 0x02000036 RID: 54
	[Export]
	public sealed class VpkBrowser9
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00002BE6 File Offset: 0x00000DE6
		[ImportingConstructor]
		public VpkBrowser9() : this(Path.Combine(Game.GamePath, "game", "dota", "pak01_dir.vpk"))
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00002C07 File Offset: 0x00000E07
		public VpkBrowser9(string fileName)
		{
			if (Path.GetExtension(fileName) != ".vpk")
			{
				throw new ArgumentException("only vpk files are valid");
			}
			this.package = new Package9();
			this.package.Read(fileName);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000130A0 File Offset: 0x000112A0
		public Bitmap FindImage(string fileName)
		{
			PackageEntry packageEntry = this.package.FindVtexEntry(fileName);
			if (packageEntry == null)
			{
				return null;
			}
			string typeName = packageEntry.TypeName;
			if (!(typeName == "png"))
			{
				if (!(typeName == "vtex_c"))
				{
					goto IL_A7;
				}
			}
			else
			{
				byte[] buffer;
				this.package.ReadEntry(packageEntry, out buffer);
				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					return new Bitmap(memoryStream);
				}
			}
			byte[] buffer2;
			this.package.ReadEntry(packageEntry, out buffer2);
			using (MemoryStream memoryStream2 = new MemoryStream(buffer2))
			{
				VTex vtex = new ResourceFile(memoryStream2).ResourceEntries.OfType<VTex>().FirstOrDefault<VTex>();
				return (vtex != null) ? vtex.Bitmap : null;
			}
			IL_A7:
			throw new ArgumentException(string.Format("{0} is not supported yet", packageEntry.TypeName));
		}

		// Token: 0x0400008F RID: 143
		private readonly Package9 package;
	}
}
