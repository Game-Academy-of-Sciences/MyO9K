using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Ensage;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.Items;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Core;
using O9K.Hud.Helpers;
using SharpDX;

namespace O9K.Hud.Modules.Map.Predictions
{
	// Token: 0x02000056 RID: 86
	internal class Courier : IDisposable, IHudModule
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x0000F668 File Offset: 0x0000D868
		[ImportingConstructor]
		public Courier(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
		{
			this.context = context;
			this.minimap = minimap;
			Menu menu = hudMenu.MapMenu.GetOrAdd<Menu>(new Menu("Predictions")).Add<Menu>(new Menu("Courier"));
			this.showOnMap = menu.Add<MenuSwitcher>(new MenuSwitcher("Show on map", true, false)).SetTooltip("Show predicted position on map");
			this.showOnMinimap = menu.Add<MenuSwitcher>(new MenuSwitcher("Show on minimap", true, false)).SetTooltip("Show predicted position on minimap");
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000F700 File Offset: 0x0000D900
		public void Activate()
		{
			if (Game.GameMode == GameMode.Turbo)
			{
				return;
			}
			this.context.Renderer.TextureManager.LoadFromDota("courier", "panorama\\images\\hud\\reborn\\icon_courier_standard_psd.vtex_c", 0, 0, false, 0, null);
			this.ownerTeam = EntityManager9.Owner.Team;
			EntityManager9.UnitAdded += this.OnUnitAdded;
			EntityManager9.UnitRemoved += this.OnUnitRemoved;
			this.context.Renderer.Draw += this.OnDraw;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000033C3 File Offset: 0x000015C3
		public void Dispose()
		{
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			this.context.Renderer.Draw -= this.OnDraw;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000F794 File Offset: 0x0000D994
		private void OnDraw(IRenderer renderer)
		{
			try
			{
				foreach (Unit9 unit in this.units)
				{
					if (unit.IsValid)
					{
						Vector3 position = unit.Position;
						if (this.showOnMinimap)
						{
							Rectangle9 rec = this.minimap.WorldToMinimap(position, 20f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
							if (rec.IsZero)
							{
								continue;
							}
							renderer.DrawTexture("courier", rec, 0f, 1f);
						}
						if (this.showOnMap && !unit.IsVisible)
						{
							Rectangle9 rec2 = this.minimap.WorldToScreen(position, 40f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
							if (!rec2.IsZero)
							{
								renderer.DrawTexture("courier", rec2, 0f, 1f);
							}
						}
					}
				}
			}
			catch (InvalidOperationException)
			{
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000F8BC File Offset: 0x0000DABC
		private void OnUnitAdded(Unit9 unit)
		{
			try
			{
				if (unit.IsCourier && unit.Team != this.ownerTeam)
				{
					this.units.Add(unit);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000F908 File Offset: 0x0000DB08
		private void OnUnitRemoved(Unit9 unit)
		{
			try
			{
				if (unit.IsCourier && unit.Team != this.ownerTeam)
				{
					this.units.Remove(unit);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x04000157 RID: 343
		private readonly IContext9 context;

		// Token: 0x04000158 RID: 344
		private readonly IMinimap minimap;

		// Token: 0x04000159 RID: 345
		private readonly MenuSwitcher showOnMap;

		// Token: 0x0400015A RID: 346
		private readonly MenuSwitcher showOnMinimap;

		// Token: 0x0400015B RID: 347
		private readonly List<Unit9> units = new List<Unit9>();

		// Token: 0x0400015C RID: 348
		private Team ownerTeam;
	}
}
