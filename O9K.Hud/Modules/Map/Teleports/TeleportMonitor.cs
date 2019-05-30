using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using Ensage;
using Ensage.SDK.Helpers;
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

namespace O9K.Hud.Modules.Map.Teleports
{
	// Token: 0x02000054 RID: 84
	internal class TeleportMonitor : IDisposable, IHudModule
	{
		// Token: 0x060001DF RID: 479 RVA: 0x0000F19C File Offset: 0x0000D39C
		[ImportingConstructor]
		public TeleportMonitor(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
		{
			this.context = context;
			this.minimap = minimap;
			Menu menu = hudMenu.MapMenu.Add<Menu>(new Menu("Teleports"));
			this.showOnMap = menu.Add<MenuSwitcher>(new MenuSwitcher("Show on map", true, false)).SetTooltip("Show enemy teleport locations");
			this.showOnMinimap = menu.Add<MenuSwitcher>(new MenuSwitcher("Show on minimap", true, false)).SetTooltip("Show enemy teleport locations");
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00003328 File Offset: 0x00001528
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.renderer = this.context.Renderer;
			Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000335C File Offset: 0x0000155C
		public void Dispose()
		{
			Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000336F File Offset: 0x0000156F
		private void AddTeleport(Teleport tp)
		{
			if (tp.HeroId == HeroId.npc_dota_hero_base)
			{
				return;
			}
			if (this.teleports.Count == 0)
			{
				this.renderer.Draw += this.OnDraw;
			}
			this.teleports.Add(tp);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000F314 File Offset: 0x0000D514
		private void CheckTeleport(ParticleEffect particle)
		{
			try
			{
				if (particle.IsValid)
				{
					Vector3 controlPoint = particle.GetControlPoint(2u);
                    System.Drawing.Color color = System.Drawing.Color.FromArgb((int)(255.0 * Math.Round((double)controlPoint.X, 2)), (int)(255.0 * Math.Round((double)controlPoint.Y, 2)), (int)(255.0 * Math.Round((double)controlPoint.Z, 2)));
					uint playerId;
					if (this.colors.TryGetValue(color, out playerId))
					{
						Player playerById = ObjectManager.GetPlayerById(playerId);
						if (!(playerById == null) && playerById.Team != this.ownerTeam)
						{
							if (particle.Name.Contains("start"))
							{
								Hero hero = playerById.Hero;
								if (hero != null && hero.IsVisible)
								{
									return;
								}
							}
							this.AddTeleport(new Teleport(playerById.SelectedHeroId, particle.GetControlPoint(0u), color));
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000F414 File Offset: 0x0000D614
		private void OnDraw(IRenderer renderer1)
		{
			try
			{
				float rawGameTime = Game.RawGameTime;
				foreach (Teleport item in this.teleports.ToList<Teleport>())
				{
					if (rawGameTime > item.DisplayUntil)
					{
						this.teleports.Remove(item);
						if (this.teleports.Count == 0)
						{
							this.renderer.Draw -= this.OnDraw;
						}
					}
					else
					{
						if (this.showOnMinimap)
						{
							Rectangle9 rec = this.minimap.WorldToMinimap(item.Position, 20f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
							this.renderer.DrawCircle(rec.Center, 10f * O9K.Core.Helpers.Hud.Info.ScreenRatio, item.Color, 3f);
							this.renderer.DrawTexture(item.MinimapTexture, rec, 0f, 1f);
						}
						if (this.showOnMap)
						{
							Rectangle9 rec2 = this.minimap.WorldToScreen(item.Position, 40f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
							if (!rec2.IsZero)
							{
								this.renderer.DrawCircle(rec2.Center, 21f * O9K.Core.Helpers.Hud.Info.ScreenRatio, item.Color, 4f);
								this.renderer.DrawTexture(item.MapTexture, rec2, 0f, 1f);
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

		// Token: 0x060001E5 RID: 485 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
		private void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
		{
			string name = args.Name;
			if (name == "particles/items2_fx/teleport_start.vpcf" || name == "particles/items2_fx/teleport_end.vpcf" || name == "particles/econ/items/tinker/boots_of_travel/teleport_start_bots.vpcf" || name == "particles/econ/items/tinker/boots_of_travel/teleport_end_bots.vpcf")
			{
				UpdateManager.BeginInvoke(delegate
				{
					this.CheckTeleport(args.ParticleEffect);
				}, 0);
				return;
			}
		}

		// Token: 0x0400014D RID: 333
		private readonly Dictionary<System.Drawing.Color, uint> colors = new Dictionary<System.Drawing.Color, uint>
		{
			{
				System.Drawing.Color.FromArgb(51, 117, 255),
				0u
			},
			{
                System.Drawing.Color.FromArgb(102, 255, 191),
				1u
			},
			{
                System.Drawing.Color.FromArgb(191, 0, 191),
				2u
			},
			{
                System.Drawing.Color.FromArgb(242, 239, 10),
				3u
			},
			{
                System.Drawing.Color.FromArgb(255, 107, 0),
				4u
			},
			{
                System.Drawing.Color.FromArgb(255, 135, 193),
				5u
			},
			{
                System.Drawing.Color.FromArgb(160, 181, 71),
				6u
			},
			{
                System.Drawing.Color.FromArgb(102, 216, 247),
				7u
			},
			{
                System.Drawing.Color.FromArgb(0, 130, 33),
				8u
			},
			{
                System.Drawing.Color.FromArgb(163, 104, 0),
				9u
			}
		};

		// Token: 0x0400014E RID: 334
		private readonly IContext9 context;

		// Token: 0x0400014F RID: 335
		private readonly IMinimap minimap;

		// Token: 0x04000150 RID: 336
		private readonly MenuSwitcher showOnMap;

		// Token: 0x04000151 RID: 337
		private readonly MenuSwitcher showOnMinimap;

		// Token: 0x04000152 RID: 338
		private readonly List<Teleport> teleports = new List<Teleport>();

		// Token: 0x04000153 RID: 339
		private Team ownerTeam;

		// Token: 0x04000154 RID: 340
		private IRendererManager9 renderer;
	}
}
