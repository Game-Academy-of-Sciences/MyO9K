using System;
using System.ComponentModel.Composition;
using O9K.Core.Managers.Assembly;
using O9K.Core.Managers.Feature;
using O9K.Core.Managers.Input;
using O9K.Core.Managers.Menu;
using O9K.Core.Managers.Renderer;

namespace O9K.Core.Managers.Context
{
	// Token: 0x0200007A RID: 122
	[Export(typeof(IContext9))]
	public sealed class Context9 : IContext9
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x00004794 File Offset: 0x00002994
		[ImportingConstructor]
		public Context9(Lazy<IFeatureManager9> featureManager, Lazy<IAssemblyEventManager9> assemblyEventManager, Lazy<IRendererManager9> renderer, Lazy<IMenuManager9> menuManager, Lazy<IInputManager9> inputManager)
		{
			this.featureManager = featureManager;
			this.assemblyEventManager = assemblyEventManager;
			this.renderer = renderer;
			this.menuManager = menuManager;
			this.inputManager = inputManager;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000047C1 File Offset: 0x000029C1
		public IMenuManager9 MenuManager
		{
			get
			{
				return this.menuManager.Value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000047CE File Offset: 0x000029CE
		public IInputManager9 InputManager
		{
			get
			{
				return this.inputManager.Value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000047DB File Offset: 0x000029DB
		public IRendererManager9 Renderer
		{
			get
			{
				return this.renderer.Value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000047E8 File Offset: 0x000029E8
		public IAssemblyEventManager9 AssemblyEventManager
		{
			get
			{
				return this.assemblyEventManager.Value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x000047F5 File Offset: 0x000029F5
		public IFeatureManager9 FeatureManager
		{
			get
			{
				return this.featureManager.Value;
			}
		}

		// Token: 0x040001C9 RID: 457
		private readonly Lazy<IAssemblyEventManager9> assemblyEventManager;

		// Token: 0x040001CA RID: 458
		private readonly Lazy<IFeatureManager9> featureManager;

		// Token: 0x040001CB RID: 459
		private readonly Lazy<IInputManager9> inputManager;

		// Token: 0x040001CC RID: 460
		private readonly Lazy<IMenuManager9> menuManager;

		// Token: 0x040001CD RID: 461
		private readonly Lazy<IRendererManager9> renderer;
	}
}
