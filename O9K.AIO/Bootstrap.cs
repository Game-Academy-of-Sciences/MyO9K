using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using Ensage;
using Ensage.SDK.Service;
using Ensage.SDK.Service.Metadata;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Menu;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;

namespace O9K.AIO
{
	// Token: 0x02000005 RID: 5
	[ExportPlugin("O9K // AIO", StartupMode.Auto, "Ensage", "1.0.0.0", "", 2147483647, new HeroId[]
	{

	})]
	public class Bootstrap : Plugin
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000020CC File Offset: 0x000002CC
		[ImportingConstructor]
		public Bootstrap(IContext9 context)
		{
			this.context = context;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00007880 File Offset: 0x00005A80
		protected override void OnActivate()
		{
			try
			{
				Hero localHero = ObjectManager.LocalHero;
				Type type = Array.Find<Type>(Assembly.GetExecutingAssembly().GetTypes(), delegate(Type x)
				{
					if (!x.IsAbstract && x.IsClass && typeof(BaseHero).IsAssignableFrom(x))
					{
						HeroIdAttribute customAttribute = x.GetCustomAttribute<HeroIdAttribute>();
						return ((customAttribute != null) ? new HeroId?(customAttribute.HeroId) : null) == localHero.HeroId;
					}
					return false;
				});
				if (type == null)
				{
					Logger.Warn("O9K.AIO // Hero is not supported");
					Logger.Warn("O9K.AIO // Dynamic combo will be loaded");
					this.hero = new BaseHero(this.context);
				}
				else if (!this.freeHeroes.Contains(localHero.HeroId) && !this.context.FeatureManager.IsFeatureActive(0))
				{
					Hud.DisplayWarning("O9K.AIO // You need O9K.AIO.Unlocker for current hero", 10f);
					this.disabledMenu = new DisabledMenuManager(localHero, this.context.MenuManager);
				}
				else
				{
					this.hero = (BaseHero)Activator.CreateInstance(type, new object[]
					{
						this.context
					});
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00007980 File Offset: 0x00005B80
		protected override void OnDeactivate()
		{
			try
			{
				BaseHero baseHero = this.hero;
				if (baseHero != null)
				{
					baseHero.Dispose();
				}
				DisabledMenuManager disabledMenuManager = this.disabledMenu;
				if (disabledMenuManager != null)
				{
					disabledMenuManager.Dispose();
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x0400000A RID: 10
		private readonly IContext9 context;

		// Token: 0x0400000B RID: 11
		private readonly HashSet<HeroId> freeHeroes = new HashSet<HeroId>
		{
			HeroId.npc_dota_hero_weaver,
			HeroId.npc_dota_hero_morphling
		};

		// Token: 0x0400000C RID: 12
		private DisabledMenuManager disabledMenu;

		// Token: 0x0400000D RID: 13
		private BaseHero hero;
	}
}
