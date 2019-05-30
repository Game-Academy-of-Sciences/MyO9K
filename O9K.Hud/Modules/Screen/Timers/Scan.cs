using System;
using System.ComponentModel.Composition;
using System.Linq;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Core;
using O9K.Hud.Helpers;
using SharpDX;

namespace O9K.Hud.Modules.Screen.Timers
{
	// Token: 0x0200002E RID: 46
	internal class Scan : IDisposable, IHudModule
	{
		// Token: 0x060000FE RID: 254 RVA: 0x0000A028 File Offset: 0x00008228
		[ImportingConstructor]
		public Scan(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
		{
			this.context = context;
			this.minimap = minimap;
			Menu menu = hudMenu.ScreenMenu.GetOrAdd<Menu>(new Menu("Timers")).Add<Menu>(new Menu("Scan"));
			this.enabled = menu.Add<MenuSwitcher>(new MenuSwitcher("Enabled", false, false));
			this.showRemaining = menu.Add<MenuSwitcher>(new MenuSwitcher("Remaining time", true, false)).SetTooltip("Show remaining time or ready time");
			this.hide = menu.Add<MenuSwitcher>(new MenuSwitcher("Auto hide", true, false)).SetTooltip("Hide timer if scan is ready");
			Menu menu2 = menu.Add<Menu>(new Menu("Settings"));
			this.textSize = menu2.Add<MenuSlider>(new MenuSlider("Size", 15, 10, 35, false));
			this.textPosition = new MenuVectorSlider(menu2, O9K.Core.Helpers.Hud.Info.ScanPosition + new Vector2(0f, 10f));
			Menu menu3 = hudMenu.MapMenu.Add<Menu>(new Menu("Scan"));
			this.showOnMap = menu3.Add<MenuSwitcher>(new MenuSwitcher("Show on map", true, false)).SetTooltip("Show enemy scan position");
			this.showOnMinimap = menu3.Add<MenuSwitcher>(new MenuSwitcher("Show on minimap", true, false)).SetTooltip("Show enemy scan position");
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000A184 File Offset: 0x00008384
		public void Activate()
		{
			this.context.Renderer.TextureManager.LoadFromDota("scan", "panorama\\images\\hud\\reborn\\icon_scan_on_dire_psd.vtex_c", 0, 0, false, 0, null);
			this.ownerTeam = EntityManager9.Owner.Team;
			this.enabled.ValueChange += this.EnabledOnValueChange;
			if (this.ownerTeam == Team.Dire)
			{
				Entity.OnFloatPropertyChange += this.OnFloatPropertyChangeDire;
				return;
			}
			Entity.OnFloatPropertyChange += this.OnFloatPropertyChangeRadiant;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000A210 File Offset: 0x00008410
		public void Dispose()
		{
			this.enabled.ValueChange -= this.EnabledOnValueChange;
			this.context.Renderer.Draw -= this.OnDrawTimer;
			this.context.Renderer.Draw -= this.OnDrawPosition;
			Entity.OnFloatPropertyChange -= this.OnFloatPropertyChangeDire;
			Entity.OnFloatPropertyChange -= this.OnFloatPropertyChangeRadiant;
			ObjectManager.OnAddEntity -= this.OnAddEntity;
			this.textPosition.Dispose();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000A2AC File Offset: 0x000084AC
		private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				this.context.Renderer.Draw += this.OnDrawTimer;
				return;
			}
			this.context.Renderer.Draw -= this.OnDrawTimer;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000A2FC File Offset: 0x000084FC
		private void OnAddEntity(EntityEventArgs args)
		{
			Unit unit = args.Entity as Unit;
			if (unit == null || unit.Team == this.ownerTeam || unit.DayVision != 0u || unit.Name != "npc_dota_thinker")
			{
				return;
			}
			if (unit.IsVisible)
			{
				if (unit.Modifiers.All((Modifier x) => x.Name != "modifier_radar_thinker"))
				{
					return;
				}
			}
			this.scanPosition = unit.Position;
			if (this.showOnMap)
			{
				this.scanRadius = new ParticleEffect("particles/ui_mouseactions/drag_selected_ring.vpcf", this.scanPosition);
				this.scanRadius.SetControlPoint(1u, new Vector3(255f, 0f, 0f));
				this.scanRadius.SetControlPoint(2u, new Vector3(-900f, 255f, 0f));
			}
			ObjectManager.OnAddEntity -= this.OnAddEntity;
			this.context.Renderer.Draw += this.OnDrawPosition;
			UpdateManager.BeginInvoke(delegate
			{
				this.context.Renderer.Draw -= this.OnDrawPosition;
				ParticleEffect particleEffect = this.scanRadius;
				if (particleEffect == null)
				{
					return;
				}
				particleEffect.Dispose();
			}, 8000);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000A430 File Offset: 0x00008630
		private void OnDrawPosition(IRenderer renderer)
		{
			try
			{
				if (this.showOnMinimap)
				{
					Rectangle9 rec = this.minimap.WorldToMinimap(this.scanPosition, 25f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
					renderer.DrawTexture("scan", rec, 0f, 1f);
				}
				if (this.showOnMap)
				{
					Rectangle9 rec2 = this.minimap.WorldToScreen(this.scanPosition, 35f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
					if (!rec2.IsZero)
					{
						renderer.DrawTexture("scan", rec2, 0f, 1f);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000A4EC File Offset: 0x000086EC
		private void OnDrawTimer(IRenderer renderer)
		{
			try
			{
				float num = this.scanCooldownEnd - Game.RawGameTime;
				if (num > 0f)
				{
					if (!this.showRemaining)
					{
						num += Game.GameTime;
					}
					O9K.Hud.Helpers.Drawer.DrawTextWithBackground(TimeSpan.FromSeconds((double)num).ToString("m\\:ss"), this.textSize, this.textPosition, renderer);
				}
				else if (!this.hide)
				{
					O9K.Hud.Helpers.Drawer.DrawTextWithBackground("Ready", this.textSize, this.textPosition, renderer);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000029CA File Offset: 0x00000BCA
		private void OnFloatPropertyChangeDire(Entity sender, FloatPropertyChangeEventArgs args)
		{
			if (args.OldValue >= args.NewValue || args.PropertyName != "m_fGoodRadarCooldown")
			{
				return;
			}
			this.ScanUsed(args.NewValue);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000029F9 File Offset: 0x00000BF9
		private void OnFloatPropertyChangeRadiant(Entity sender, FloatPropertyChangeEventArgs args)
		{
			if (args.OldValue >= args.NewValue || args.PropertyName != "m_fBadRadarCooldown")
			{
				return;
			}
			this.ScanUsed(args.NewValue);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00002A28 File Offset: 0x00000C28
		private void ScanUsed(float time)
		{
			this.scanCooldownEnd = time;
			ObjectManager.OnAddEntity += this.OnAddEntity;
			UpdateManager.BeginInvoke(delegate
			{
				ObjectManager.OnAddEntity -= this.OnAddEntity;
			}, 1000);
		}

		// Token: 0x040000B0 RID: 176
		private readonly IContext9 context;

		// Token: 0x040000B1 RID: 177
		private readonly MenuSwitcher enabled;

		// Token: 0x040000B2 RID: 178
		private readonly MenuSwitcher hide;

		// Token: 0x040000B3 RID: 179
		private readonly IMinimap minimap;

		// Token: 0x040000B4 RID: 180
		private readonly MenuSwitcher showOnMap;

		// Token: 0x040000B5 RID: 181
		private readonly MenuSwitcher showOnMinimap;

		// Token: 0x040000B6 RID: 182
		private readonly MenuVectorSlider textPosition;

		// Token: 0x040000B7 RID: 183
		private readonly MenuSlider textSize;

		// Token: 0x040000B8 RID: 184
		private Team ownerTeam;

		// Token: 0x040000B9 RID: 185
		private float scanCooldownEnd = 220f;

		// Token: 0x040000BA RID: 186
		private Vector3 scanPosition;

		// Token: 0x040000BB RID: 187
		private ParticleEffect scanRadius;

		// Token: 0x040000BC RID: 188
		private readonly MenuSwitcher showRemaining;
	}
}
