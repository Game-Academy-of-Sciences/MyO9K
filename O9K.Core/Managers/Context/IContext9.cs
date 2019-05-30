using System;
using O9K.Core.Managers.Assembly;
using O9K.Core.Managers.Feature;
using O9K.Core.Managers.Input;
using O9K.Core.Managers.Menu;
using O9K.Core.Managers.Renderer;

namespace O9K.Core.Managers.Context
{
	// Token: 0x02000079 RID: 121
	public interface IContext9
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003E9 RID: 1001
		IFeatureManager9 FeatureManager { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003EA RID: 1002
		IInputManager9 InputManager { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003EB RID: 1003
		IRendererManager9 Renderer { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003EC RID: 1004
		IAssemblyEventManager9 AssemblyEventManager { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003ED RID: 1005
		IMenuManager9 MenuManager { get; }
	}
}
