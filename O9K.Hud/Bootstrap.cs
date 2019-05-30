using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Ensage;
using Ensage.SDK.Service;
using Ensage.SDK.Service.Metadata;
using O9K.Core.Logger;
using O9K.Hud.Core;

namespace O9K.Hud
{
	// Token: 0x02000006 RID: 6
	[ExportPlugin("O9K // Hud", StartupMode.Auto, "Ensage", "1.0.0.0", "", 2147483647, new HeroId[]
	{

	})]
	internal class Bootstrap : Plugin
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000210C File Offset: 0x0000030C
		[ImportingConstructor]
		public Bootstrap([ImportMany] IEnumerable<IHudModule> modules)
		{
			this.modules = modules;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00004B9C File Offset: 0x00002D9C
		protected override void OnActivate()
		{
			try
			{
				foreach (IHudModule hudModule in from x in this.modules
				orderby x is IHudMenu descending
				select x)
				{
					hudModule.Activate();
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00004C20 File Offset: 0x00002E20
		protected override void OnDeactivate()
		{
			try
			{
				foreach (IHudModule hudModule in this.modules)
				{
					hudModule.Dispose();
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x04000009 RID: 9
		private readonly IEnumerable<IHudModule> modules;
	}
}
