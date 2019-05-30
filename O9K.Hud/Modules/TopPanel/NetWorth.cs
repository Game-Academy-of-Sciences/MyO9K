using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Heroes.Unique;
using O9K.Core.Entities.Units;
using O9K.Core.Entities.Units.Unique;
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

namespace O9K.Hud.Modules.TopPanel
{
	// Token: 0x02000026 RID: 38
	internal class NetWorth : IDisposable, IHudModule
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00007C78 File Offset: 0x00005E78
		[ImportingConstructor]
		public NetWorth(IContext9 context, ITopPanel topPanel, IHudMenu hudMenu)
		{
			this.context = context;
			this.topPanel = topPanel;
			Menu menu = hudMenu.TopPanelMenu.Add<Menu>(new Menu("Net worth"));
			this.show = menu.Add<MenuSwitcher>(new MenuSwitcher("Enabled", "enabled", true, false)).SetTooltip("Show net worth lead");
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00002786 File Offset: 0x00000986
		public void Activate()
		{
			this.LoadTextures();
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007CE4 File Offset: 0x00005EE4
		public void Dispose()
		{
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
			this.context.Renderer.Draw -= this.OnDraw;
			this.show.ValueChange -= this.ShowOnValueChange;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007D48 File Offset: 0x00005F48
		private void LoadTextures()
		{
			this.context.Renderer.TextureManager.LoadFromDota("net_worth_bg_top", "panorama\\images\\masks\\chat_preview_opacity_mask_png.vtex_c", 0, 0, false, 0, new Vector4?(new Vector4(0f, 0f, 0f, 0.6f)));
			this.context.Renderer.TextureManager.LoadFromDota("net_worth_arrow_ally", "panorama\\images\\hud\\reborn\\arrow_gold_dif_psd.vtex_c", 0, 0, false, 0, null);
			this.context.Renderer.TextureManager.LoadFromDota("net_worth_arrow_enemy", "panorama\\images\\hud\\reborn\\arrow_plus_stats_red_psd.vtex_c", 0, 0, false, 0, null);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007DF0 File Offset: 0x00005FF0
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.IsItem && !ability.Owner.IsIllusion)
				{
					Unit9 owner = ability.Owner;
					if (owner != null)
					{
						Meepo meepo;
						if ((meepo = (owner as Meepo)) != null && !meepo.IsMainMeepo)
						{
							return;
						}
						if (owner is SpiritBear)
						{
							goto IL_4E;
						}
					}
					if (!ability.Owner.IsHero)
					{
						return;
					}
					IL_4E:
					Dictionary<Team, int> dictionary = this.teams;
					Team team = ability.Owner.Team;
					dictionary[team] += (int)ability.BaseItem.Cost;
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007E94 File Offset: 0x00006094
		private void OnAbilityRemoved(Ability9 ability)
		{
			try
			{
				if (ability.IsItem && !ability.Owner.IsIllusion)
				{
					Unit9 owner = ability.Owner;
					if (owner != null)
					{
						Meepo meepo;
						if ((meepo = (owner as Meepo)) != null && !meepo.IsMainMeepo)
						{
							return;
						}
						if (owner is SpiritBear)
						{
							goto IL_4E;
						}
					}
					if (!ability.Owner.IsHero)
					{
						return;
					}
					IL_4E:
					Dictionary<Team, int> dictionary = this.teams;
					Team team = ability.Owner.Team;
					dictionary[team] -= (int)ability.BaseItem.Cost;
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007F38 File Offset: 0x00006138
		private void OnDraw(IRenderer renderer)
		{
			try
			{
				int num = this.teams[Team.Radiant] - this.teams[Team.Dire];
				string text;
				Team team;
				if (num > 0)
				{
					text = Math.Ceiling((double)((float)num / 1000f)) + "k";
					team = Team.Radiant;
				}
				else
				{
					text = Math.Ceiling((double)((float)num / -1000f)) + "k";
					team = Team.Dire;
				}
				float screenRatio = O9K.Core.Helpers.Hud.Info.ScreenRatio;
				Rectangle9 scorePosition = this.topPanel.GetScorePosition(team);
				scorePosition.Y += scorePosition.Height + 1f;
				scorePosition.Height = 22f * screenRatio;
				float fontSize = 15f * screenRatio;
				float num2 = (scorePosition.Width - (renderer.MeasureText(text, fontSize, "Calibri").X + 24f * screenRatio)) / 2f;
				renderer.DrawTexture("net_worth_bg_top", scorePosition, 0f, 1f);
				renderer.DrawTexture((this.ownerTeam == team) ? "net_worth_arrow_ally" : "net_worth_arrow_enemy", new Vector2(scorePosition.X + num2, scorePosition.Y + 4f * screenRatio), new Vector2(12f * screenRatio), 0f, 1f);
				renderer.DrawText(new Vector2(scorePosition.X + num2 + 15f * screenRatio, scorePosition.Y), text, System.Drawing.Color.White, fontSize, "Calibri");
			}
			catch (InvalidOperationException)
			{
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000080F4 File Offset: 0x000062F4
		private void ShowOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				this.teams[Team.Radiant] = 0;
				this.teams[Team.Dire] = 0;
				EntityManager9.AbilityAdded += this.OnAbilityAdded;
				EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
				this.context.Renderer.Draw += this.OnDraw;
				return;
			}
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
			this.context.Renderer.Draw -= this.OnDraw;
		}

		// Token: 0x04000079 RID: 121
		private readonly IContext9 context;

		// Token: 0x0400007A RID: 122
		private readonly MenuSwitcher show;

		// Token: 0x0400007B RID: 123
		private readonly Dictionary<Team, int> teams = new Dictionary<Team, int>();

		// Token: 0x0400007C RID: 124
		private readonly ITopPanel topPanel;

		// Token: 0x0400007D RID: 125
		private Team ownerTeam;
	}
}
