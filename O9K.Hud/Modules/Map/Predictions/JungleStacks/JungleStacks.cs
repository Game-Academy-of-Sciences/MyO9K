using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using Ensage.SDK.Helpers;
using Ensage.SDK.Renderer;
using O9K.Core.Entities.Units;
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

namespace O9K.Hud.Modules.Map.Predictions.JungleStacks
{
	// Token: 0x02000060 RID: 96
	internal class JungleStacks : IDisposable, IHudModule
	{
		// Token: 0x0600021E RID: 542 RVA: 0x00010EB4 File Offset: 0x0000F0B4
		[ImportingConstructor]
		public JungleStacks(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
		{
			this.context = context;
			this.minimap = minimap;
			Menu menu = hudMenu.MapMenu.GetOrAdd<Menu>(new Menu("Predictions")).Add<Menu>(new Menu("Jungle stacks"));
			this.showOnMinimap = menu.Add<MenuSwitcher>(new MenuSwitcher("Show on minimap", true, false)).SetTooltip("Show predicted stacks on minimap");
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000035C9 File Offset: 0x000017C9
		public void Activate()
		{
			this.showOnMinimap.ValueChange += this.ShowOnMinimapOnValueChange;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00011214 File Offset: 0x0000F414
		public void Dispose()
		{
			this.showOnMinimap.ValueChange -= this.ShowOnMinimapOnValueChange;
			this.context.Renderer.Draw -= this.OnDraw;
			UpdateManager.Unsubscribe(new Action(this.OnUpdate));
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00011268 File Offset: 0x0000F468
		private void OnDraw(O9K.Core.Managers.Renderer.IRenderer renderer)
		{
			try
			{
				foreach (KeyValuePair<Vector3, float> keyValuePair in this.stacks)
				{
					if (keyValuePair.Value > 1f)
					{
						float num = 16f * O9K.Core.Helpers.Hud.Info.ScreenRatio;
						Rectangle9 rec = this.minimap.WorldToMinimap(keyValuePair.Key, num);
						renderer.DrawText(rec + new Size2F(num, 0f), keyValuePair.Value.ToString(), System.Drawing.Color.Orange, RendererFontFlags.Left, num, "Calibri");
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

		// Token: 0x06000222 RID: 546 RVA: 0x00011340 File Offset: 0x0000F540
		private void OnUpdate()
		{
			try
			{
				Unit9[] source = (from x in EntityManager9.Units
				where x.Team == Team.Neutral && x.BaseUnit.IsAlive
				select x).ToArray<Unit9>();
				Vector3[] array = this.campPositions;
				for (int i = 0; i < array.Length; i++)
				{
					Vector3 camp = array[i];
					List<Unit9> source2 = (from x in source
					where x.Position.Distance2D(camp, false) < 600f
					select x).ToList<Unit9>();
					int num = source2.Count((Unit9 x) => this.singleCreeps.Contains(x.Name)) + source2.Count((Unit9 x) => this.doubleCreeps.Contains(x.Name)) / 2;
					this.stacks[camp] = (float)(num - 1);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00011418 File Offset: 0x0000F618
		private void ShowOnMinimapOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				UpdateManager.Subscribe(new Action(this.OnUpdate), 5000, true);
				this.context.Renderer.Draw += this.OnDraw;
				return;
			}
			UpdateManager.Unsubscribe(new Action(this.OnUpdate));
			this.context.Renderer.Draw -= this.OnDraw;
		}

		// Token: 0x0400018A RID: 394
		private readonly IContext9 context;

		// Token: 0x0400018B RID: 395
		private readonly IMinimap minimap;

		// Token: 0x0400018C RID: 396
		private readonly MenuSwitcher showOnMinimap;

		// Token: 0x0400018D RID: 397
		private readonly Vector3[] campPositions = new Vector3[]
		{
			new Vector3(-4767f, -359f, 384f),
			new Vector3(-3557f, 972f, 383f),
			new Vector3(-2393f, -571f, 384f),
			new Vector3(237f, -1773f, 256f),
			new Vector3(-125f, -3303f, 384f),
			new Vector3(-1724f, -4204f, 256f),
			new Vector3(488f, -4619f, 384f),
			new Vector3(3054f, -4605f, 255f),
			new Vector3(4659f, -4350f, 256f),
			new Vector3(-4127f, 3509f, 256f),
			new Vector3(-2399f, 4956f, 255f),
			new Vector3(-1701f, 4377f, 384f),
			new Vector3(-801f, 2361f, 384f),
			new Vector3(-24f, 3577f, 384f),
			new Vector3(1411f, 3439f, 384f),
			new Vector3(2656f, 101f, 384f),
			new Vector3(4585f, 928f, 384f),
			new Vector3(4284f, -191f, 384f)
		};

		// Token: 0x0400018E RID: 398
		private readonly HashSet<string> doubleCreeps = new HashSet<string>
		{
			"npc_dota_neutral_satyr_soulstealer",
			"npc_dota_neutral_mud_golem",
			"npc_dota_neutral_gnoll_assassin"
		};

		// Token: 0x0400018F RID: 399
		private readonly HashSet<string> singleCreeps = new HashSet<string>
		{
			"npc_dota_neutral_harpy_storm",
			"npc_dota_neutral_ghost",
			"npc_dota_neutral_forest_troll_high_priest",
			"npc_dota_neutral_kobold_taskmaster",
			"npc_dota_neutral_alpha_wolf",
			"npc_dota_neutral_ogre_magi",
			"npc_dota_neutral_satyr_hellcaller",
			"npc_dota_neutral_centaur_khan",
			"npc_dota_neutral_dark_troll_warlord",
			"npc_dota_neutral_enraged_wildkin",
			"npc_dota_neutral_polar_furbolg_ursa_warrior",
			"npc_dota_neutral_big_thunder_lizard",
			"npc_dota_neutral_black_dragon",
			"npc_dota_neutral_granite_golem",
			"npc_dota_neutral_prowler_shaman"
		};

		// Token: 0x04000190 RID: 400
		private readonly Dictionary<Vector3, float> stacks = new Dictionary<Vector3, float>();
	}
}
