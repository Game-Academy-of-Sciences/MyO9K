using System;
using System.IO;
using Ensage;
using SharpDX;

namespace O9K.Core.Managers.Renderer
{
	// Token: 0x0200002F RID: 47
	public interface ITextureManager : IDisposable
	{
		// Token: 0x060000F0 RID: 240
		bool IsTextureLoaded(string textureKey);

		// Token: 0x060000F1 RID: 241
		void LoadFromDota(AbilityId abilityId, bool rounded = false);

		// Token: 0x060000F2 RID: 242
		void LoadFromDota(string unitName, bool rounded = false);

		// Token: 0x060000F3 RID: 243
		void LoadFromDota(HeroId heroId, bool rounded = false, bool icon = false);

		// Token: 0x060000F4 RID: 244
		void LoadFromDota(string textureKey, string file, int w = 0, int h = 0, bool rounded = false, int brightness = 0, Vector4? colorRatio = null);

		// Token: 0x060000F5 RID: 245
		void LoadFromFile(string textureKey, string file);

		// Token: 0x060000F6 RID: 246
		void LoadFromMemory(string textureKey, byte[] data);

		// Token: 0x060000F7 RID: 247
		void LoadFromResource(string textureKey, string file);

		// Token: 0x060000F8 RID: 248
		void LoadFromStream(string textureKey, Stream stream);

		// Token: 0x060000F9 RID: 249
		void LoadOutlineFromDota(string textureKey, string file, int brightness = 0, Vector4? colorRatio = null);
	}
}
