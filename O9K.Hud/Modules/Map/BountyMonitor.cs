using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using Ensage.SDK.Helpers;
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

namespace O9K.Hud.Modules.Map
{
	// Token: 0x0200004B RID: 75
	internal class BountyMonitor : IDisposable, IHudModule
	{
		// Token: 0x060001BD RID: 445 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
		[ImportingConstructor]
		public BountyMonitor(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
		{
			this.context = context;
			this.minimap = minimap;
			Menu menu = hudMenu.MapMenu.Add<Menu>(new Menu("Bounty runes"));
			this.showOnMap = menu.Add<MenuSwitcher>(new MenuSwitcher("Show on map", true, false)).SetTooltip("Show picked bounty runes");
			this.showOnMinimap = menu.Add<MenuSwitcher>(new MenuSwitcher("Show on minimap", true, false)).SetTooltip("Show picked bounty runes");
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000E96C File Offset: 0x0000CB6C
		public void Activate()
		{
			this.context.Renderer.TextureManager.LoadFromDota("x", "panorama\\images\\hud\\reborn\\ping_icon_retreat_psd.vtex_c", 0, 0, false, 0, null);
			this.ownerTeam = EntityManager9.Owner.Team;
			foreach (Entity entity in from x in ObjectManager.GetEntities<Entity>()
			where x.NetworkName == "CDOTA_Item_RuneSpawner_Bounty"
			select x)
			{
				this.bountySpawns.Add(entity.Position);
			}
			if (this.bountySpawns.Count == 0)
			{
				return;
			}
			Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;
			UpdateManager.Subscribe(new Action(this.OnUpdate), 1000, true);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000031DA File Offset: 0x000013DA
		public void Dispose()
		{
			UpdateManager.Unsubscribe(new Action(this.OnUpdate));
			Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
			this.context.Renderer.Draw -= this.OnDraw;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		private void CheckRune(ParticleEffect particle)
		{
			try
			{
				Entity owner = particle.Owner;
				if (!(owner != null) || (owner.Team != this.ownerTeam && !owner.IsVisible))
				{
					Vector3 position = particle.GetControlPoint(0u);
					Vector3 item = this.bountySpawns.Find((Vector3 x) => x.Distance2D(position, false) < 500f);
					if (!item.IsZero)
					{
						if (!this.pickedRunes.Contains(item))
						{
							if (this.pickedRunes.Count == 0)
							{
								this.context.Renderer.Draw += this.OnDraw;
							}
							this.pickedRunes.Add(item);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000EB28 File Offset: 0x0000CD28
		private void OnDraw(IRenderer renderer)
		{
			try
			{
				foreach (Vector3 position in this.pickedRunes)
				{
					if (this.showOnMinimap)
					{
						Rectangle9 rec = this.minimap.WorldToMinimap(position, 20f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
						renderer.DrawTexture("x", rec, 0f, 1f);
					}
					if (this.showOnMap)
					{
						Rectangle9 rec2 = this.minimap.WorldToScreen(position, 40f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
						if (!rec2.IsZero)
						{
							renderer.DrawTexture("x", rec2, 0f, 1f);
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

		// Token: 0x060001C2 RID: 450 RVA: 0x0000EC28 File Offset: 0x0000CE28
		private void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
		{
			if (args.Name != "particles/generic_gameplay/rune_bounty_owner.vpcf")
			{
				return;
			}
			UpdateManager.BeginInvoke(delegate
			{
				this.CheckRune(args.ParticleEffect);
			}, 0);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000EC74 File Offset: 0x0000CE74
		private void OnUpdate()
		{
			try
			{
				if (this.pickedRunes.Count > 0)
				{
					Unit9[] source = (from x in EntityManager9.Units
					where x.IsUnit && x.Team == this.ownerTeam
					select x).ToArray<Unit9>();
					using (List<Vector3>.Enumerator enumerator = this.pickedRunes.ToList<Vector3>().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Vector3 pickedRune = enumerator.Current;
							if (source.Any((Unit9 x) => x.Distance(pickedRune) < 300f))
							{
								this.pickedRunes.Remove(pickedRune);
								if (this.pickedRunes.Count == 0)
								{
									this.context.Renderer.Draw -= this.OnDraw;
								}
							}
						}
					}
					if (Game.GameTime % 300f > 295f)
					{
						this.context.Renderer.Draw -= this.OnDraw;
						this.pickedRunes.Clear();
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x04000132 RID: 306
		private readonly List<Vector3> bountySpawns = new List<Vector3>();

		// Token: 0x04000133 RID: 307
		private readonly IContext9 context;

		// Token: 0x04000134 RID: 308
		private readonly IMinimap minimap;

		// Token: 0x04000135 RID: 309
		private readonly List<Vector3> pickedRunes = new List<Vector3>();

		// Token: 0x04000136 RID: 310
		private readonly MenuSwitcher showOnMap;

		// Token: 0x04000137 RID: 311
		private readonly MenuSwitcher showOnMinimap;

		// Token: 0x04000138 RID: 312
		private Team ownerTeam;
	}
}
