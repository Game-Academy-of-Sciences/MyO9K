using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using Ensage.SDK.Helpers;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Menu.Items;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Core;
using O9K.Hud.Helpers;
using SharpDX;

namespace O9K.Hud.Modules.Map
{
	// Token: 0x02000050 RID: 80
	internal class Farm : IDisposable, IHudModule
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		[ImportingConstructor]
		public Farm(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
		{
			this.context = context;
			this.minimap = minimap;
			Menu menu = hudMenu.MapMenu.Add<Menu>(new Menu("Farm"));
			menu.Add<MenuText>(new MenuText("This feature is not reliable"));
			this.showOnMap = menu.Add<MenuSwitcher>(new MenuSwitcher("Show on map", true, false)).SetTooltip("Show when enemy attacks neutrals/roshan");
			this.showOnMinimap = menu.Add<MenuSwitcher>(new MenuSwitcher("Show on minimap", true, false)).SetTooltip("Show when enemy attacks neutrals/roshan");
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000EE38 File Offset: 0x0000D038
		public void Activate()
		{
			this.context.Renderer.TextureManager.LoadFromDota("attack", "panorama\\images\\hud\\reborn\\ping_icon_attack_psd.vtex_c", 0, 0, false, 0, null);
			Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00003295 File Offset: 0x00001495
		public void Dispose()
		{
			this.context.Renderer.Draw -= this.OnDraw;
			Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000EE84 File Offset: 0x0000D084
		private void CheckAttack(ParticleEffect particle)
		{
			try
			{
				if (particle.IsValid && particle.Owner.Team == Team.Neutral && !particle.Owner.IsVisible)
				{
					Vector3 position = particle.GetControlPoint(0u);
					Sleeper value = this.attacks.FirstOrDefault((KeyValuePair<Vector3, Sleeper> x) => x.Key.Distance2D(position, false) < 500f).Value;
					if (value != null)
					{
						value.Sleep(3f);
					}
					else
					{
						if (this.attacks.Count == 0)
						{
							this.context.Renderer.Draw += this.OnDraw;
						}
						this.attacks[position] = new Sleeper(3f);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000EF5C File Offset: 0x0000D15C
		private void OnDraw(IRenderer renderer)
		{
			try
			{
				foreach (KeyValuePair<Vector3, Sleeper> keyValuePair in this.attacks.ToList<KeyValuePair<Vector3, Sleeper>>())
				{
					if (!keyValuePair.Value.IsSleeping)
					{
						this.attacks.Remove(keyValuePair.Key);
						if (this.attacks.Count == 0)
						{
							this.context.Renderer.Draw -= this.OnDraw;
						}
					}
					else
					{
						if (this.showOnMinimap)
						{
							Rectangle9 rec = this.minimap.WorldToMinimap(keyValuePair.Key, 20f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
							renderer.DrawTexture("attack", rec, 0f, 1f);
						}
						if (this.showOnMap)
						{
							Rectangle9 rec2 = this.minimap.WorldToScreen(keyValuePair.Key, 40f * O9K.Core.Helpers.Hud.Info.ScreenRatio);
							if (!rec2.IsZero)
							{
								renderer.DrawTexture("attack", rec2, 0f, 1f);
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

		// Token: 0x060001D3 RID: 467 RVA: 0x0000F0E4 File Offset: 0x0000D2E4
		private void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
		{
			if (args.Name != "particles/generic_gameplay/generic_hit_blood.vpcf")
			{
				return;
			}
			UpdateManager.BeginInvoke(delegate
			{
				this.CheckAttack(args.ParticleEffect);
			}, 0);
		}

		// Token: 0x0400013F RID: 319
		private readonly Dictionary<Vector3, Sleeper> attacks = new Dictionary<Vector3, Sleeper>();

		// Token: 0x04000140 RID: 320
		private readonly IContext9 context;

		// Token: 0x04000141 RID: 321
		private readonly IMinimap minimap;

		// Token: 0x04000142 RID: 322
		private readonly MenuSwitcher showOnMap;

		// Token: 0x04000143 RID: 323
		private readonly MenuSwitcher showOnMinimap;
	}
}
