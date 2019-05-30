using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Ensage;
using O9K.Core.Helpers;
using O9K.Core.Managers.Input;
using O9K.Core.Managers.Input.EventArgs;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Hud.Core;

namespace O9K.Hud.Modules.Screen
{
	// Token: 0x0200002A RID: 42
	internal class Zoom : IDisposable, IHudModule
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x000096CC File Offset: 0x000078CC
		[ImportingConstructor]
		public Zoom(IInputManager9 inputManager, IHudMenu hudMenu)
		{
			this.inputManager = inputManager;
			Menu orAdd = hudMenu.ScreenMenu.GetOrAdd<Menu>(new Menu("Zoom"));
			this.enabled = orAdd.Add<MenuSwitcher>(new MenuSwitcher("Enabled", false, false));
			this.zoom = orAdd.Add<MenuSlider>(new MenuSlider("Zoom", 1400, 1000, 3000, false)).SetTooltip("Default: " + 1134);
			this.key = orAdd.Add<MenuHoldKey>(new MenuHoldKey("Key", Key.LeftCtrl, false)).SetTooltip("Change zoom with a key and mouse wheel");
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000288D File Offset: 0x00000A8D
		public void Activate()
		{
			this.enabled.ValueChange += this.EnabledOnValueChange;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00009820 File Offset: 0x00007A20
		public void Dispose()
		{
			this.enabled.ValueChange -= this.EnabledOnValueChange;
			this.key.ValueChange -= this.KeyOnValueChange;
			this.zoom.ValueChange -= this.ZoomOnValueChange;
			this.inputManager.MouseWheel -= this.OnMouseWheel;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000988C File Offset: 0x00007A8C
		private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				if (e.OldValue)
				{
					if (AppDomain.CurrentDomain.GetAssemblies().Any((Assembly x) => !x.GlobalAssemblyCache && x.GetName().Name.Contains("Zoomhack")))
					{
						O9K.Core.Helpers.Hud.DisplayWarning("O9K.Hud // ZoomHack is already included in O9K.Hud", 10f);
					}
				}
				foreach (KeyValuePair<string, int> keyValuePair in this.consoleCommands)
				{
					Game.GetConsoleVar(keyValuePair.Key).SetValue(keyValuePair.Value);
				}
				this.zoomVar = Game.GetConsoleVar("dota_camera_distance");
				this.zoom.ValueChange += this.ZoomOnValueChange;
				this.key.ValueChange += this.KeyOnValueChange;
				return;
			}
			this.key.ValueChange -= this.KeyOnValueChange;
			this.zoom.ValueChange -= this.ZoomOnValueChange;
			this.inputManager.MouseWheel -= this.OnMouseWheel;
			this.zoomVar.SetValue(1134);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000028A6 File Offset: 0x00000AA6
		private void KeyOnValueChange(object sender, O9K.Core.Managers.Menu.EventArgs.KeyEventArgs e)
		{
			if (e.NewValue)
			{
				this.inputManager.MouseWheel += this.OnMouseWheel;
				return;
			}
			this.inputManager.MouseWheel -= this.OnMouseWheel;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000028DF File Offset: 0x00000ADF
		private void OnMouseWheel(object sender, MouseWheelEventArgs e)
		{
			this.zoom.Value += (e.Up ? -50 : 50);
			e.Process = false;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002908 File Offset: 0x00000B08
		private void ZoomOnValueChange(object sender, SliderEventArgs e)
		{
			this.zoomVar.SetValue(e.NewValue);
		}

		// Token: 0x04000097 RID: 151
		private const int DefaultZoom = 1134;

		// Token: 0x04000098 RID: 152
		private readonly Dictionary<string, int> consoleCommands = new Dictionary<string, int>
		{
			{
				"cam_showangles",
				0
			},
			{
				"dota_camera_fog_end_zoomed_in",
				4500
			},
			{
				"dota_camera_fog_end_zoomed_out",
				6000
			},
			{
				"dota_camera_fog_start_zoomed_in",
				2000
			},
			{
				"dota_camera_fog_start_zoomed_out",
				4500
			},
			{
				"dota_height_fog_scale",
				0
			},
			{
				"fog_enable",
				0
			},
			{
				"fog_end",
				3000
			},
			{
				"fog_override",
				1
			},
			{
				"r_farz",
				10000
			},
			{
				"dota_use_particle_fow",
				0
			}
		};

		// Token: 0x04000099 RID: 153
		private readonly MenuSwitcher enabled;

		// Token: 0x0400009A RID: 154
		private readonly IInputManager9 inputManager;

		// Token: 0x0400009B RID: 155
		private readonly MenuHoldKey key;

		// Token: 0x0400009C RID: 156
		private readonly MenuSlider zoom;

		// Token: 0x0400009D RID: 157
		private ConVar zoomVar;
	}
}
