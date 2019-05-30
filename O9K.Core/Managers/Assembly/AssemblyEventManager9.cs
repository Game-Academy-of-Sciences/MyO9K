using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;

namespace O9K.Core.Managers.Assembly
{
	// Token: 0x0200007B RID: 123
	[Export(typeof(IAssemblyEventManager9))]
	public sealed class AssemblyEventManager9 : IAssemblyEventManager9
	{
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060003F4 RID: 1012 RVA: 0x0001D4E0 File Offset: 0x0001B6E0
		// (remove) Token: 0x060003F5 RID: 1013 RVA: 0x00004802 File Offset: 0x00002A02
		public event EventHandler<string> OnAssemblyLoad
		{
			add
			{
				foreach (string e in this.loadedAssemblies)
				{
					value(this, e);
				}
				this.AssemblyLoad += value;
			}
			remove
			{
				this.AssemblyLoad -= value;
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060003F6 RID: 1014 RVA: 0x0001D53C File Offset: 0x0001B73C
		// (remove) Token: 0x060003F7 RID: 1015 RVA: 0x0001D574 File Offset: 0x0001B774
		public event EventHandler OnEvaderPredictedDeath;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060003F8 RID: 1016 RVA: 0x0001D5AC File Offset: 0x0001B7AC
		// (remove) Token: 0x060003F9 RID: 1017 RVA: 0x0001D5E4 File Offset: 0x0001B7E4
		private event EventHandler<string> AssemblyLoad;

		// Token: 0x060003FA RID: 1018 RVA: 0x0001D61C File Offset: 0x0001B81C
		public void AssemblyLoaded()
		{
			string name = Assembly.GetCallingAssembly().GetName().Name;
			this.loadedAssemblies.Add(name);
			EventHandler<string> assemblyLoad = this.AssemblyLoad;
			if (assemblyLoad == null)
			{
				return;
			}
			assemblyLoad(this, name);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000480B File Offset: 0x00002A0B
		public void InvokeOnEvaderPredictedDeath()
		{
			EventHandler onEvaderPredictedDeath = this.OnEvaderPredictedDeath;
			if (onEvaderPredictedDeath == null)
			{
				return;
			}
			onEvaderPredictedDeath(this, EventArgs.Empty);
		}

		// Token: 0x040001CE RID: 462
		private readonly List<string> loadedAssemblies = new List<string>();
	}
}
