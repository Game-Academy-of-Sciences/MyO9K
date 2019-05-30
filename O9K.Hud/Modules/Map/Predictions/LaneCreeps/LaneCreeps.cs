using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
using O9K.Hud.Modules.Map.Predictions.LaneCreeps.LaneData;
using SharpDX;

namespace O9K.Hud.Modules.Map.Predictions.LaneCreeps
{
	// Token: 0x02000059 RID: 89
	internal class LaneCreeps : IDisposable, IHudModule
	{
		// Token: 0x06000207 RID: 519 RVA: 0x0000FBF0 File Offset: 0x0000DDF0
		[ImportingConstructor]
		public LaneCreeps(IContext9 context, IMinimap minimap, IHudMenu hudMenu)
		{
			this.context = context;
			this.minimap = minimap;
			Menu menu = hudMenu.MapMenu.GetOrAdd<Menu>(new Menu("Predictions")).Add<Menu>(new Menu("Lane creeps"));
			this.enabled = menu.Add<MenuSwitcher>(new MenuSwitcher("Enabled", true, false));
			this.showOnMap = menu.Add<MenuSwitcher>(new MenuSwitcher("Show on map", true, false)).SetTooltip("Show predicted position on map");
			this.showOnMinimap = menu.Add<MenuSwitcher>(new MenuSwitcher("Show on minimap", true, false)).SetTooltip("Show predicted position on minimap");
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000351A File Offset: 0x0000171A
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.lanePaths = new LanePaths();
			this.enabled.ValueChange += this.EnabledOnValueChange;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000FCA8 File Offset: 0x0000DEA8
		public void Dispose()
		{
			this.enabled.ValueChange -= this.EnabledOnValueChange;
			this.context.Renderer.Draw -= this.OnDraw;
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			EntityManager9.UnitMonitor.UnitDied -= this.OnUnitRemoved;
			Entity.OnBoolPropertyChange -= this.OnBoolPropertyChange;
			UpdateManager.Unsubscribe(new Action(this.OnUpdate));
			this.creepWaves.Clear();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000FD50 File Offset: 0x0000DF50
		private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				if (e.OldValue)
				{
					if (AppDomain.CurrentDomain.GetAssemblies().Any((Assembly x) => !x.GlobalAssemblyCache && x.GetName().Name.Contains("PredictedCreepsLocation")))
					{
						O9K.Core.Helpers.Hud.DisplayWarning("O9K.Hud // PredictedCreepsLocation is already included in O9K.Hud", 10f);
					}
				}
				EntityManager9.UnitAdded += this.OnUnitAdded;
				EntityManager9.UnitRemoved += this.OnUnitRemoved;
				EntityManager9.UnitMonitor.UnitDied += this.OnUnitRemoved;
				Entity.OnBoolPropertyChange += this.OnBoolPropertyChange;
				UpdateManager.Subscribe(new Action(this.OnUpdate), 0, true);
				this.context.Renderer.Draw += this.OnDraw;
				return;
			}
			this.context.Renderer.Draw -= this.OnDraw;
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			EntityManager9.UnitMonitor.UnitDied -= this.OnUnitRemoved;
			Entity.OnBoolPropertyChange -= this.OnBoolPropertyChange;
			UpdateManager.Unsubscribe(new Action(this.OnUpdate));
			this.creepWaves.Clear();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		private void OnBoolPropertyChange(Entity sender, BoolPropertyChangeEventArgs args)
		{
			try
			{
				if (args.OldValue != args.NewValue && (args.OldValue || !args.NewValue) && !(args.PropertyName != "m_bIsWaitingToSpawn"))
				{
					Unit9 unit = EntityManager9.GetUnit(sender.Handle);
					if (!(unit == null) && unit.IsLaneCreep && unit.Team == this.ownerTeam)
					{
						foreach (CreepWave creepWave in from x in this.creepWaves
						where !x.IsSpawned
						select x)
						{
							creepWave.Spawn();
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000FF98 File Offset: 0x0000E198
		private void OnDraw(O9K.Core.Managers.Renderer.IRenderer renderer)
		{
			try
			{
				foreach (CreepWave creepWave in this.creepWaves)
				{
					if (creepWave.IsSpawned && !creepWave.IsVisible)
					{
						Vector3 predictedPosition = creepWave.PredictedPosition;
						string text = creepWave.Creeps.Count.ToString();
						if (this.showOnMinimap)
						{
							float num = 19f * O9K.Core.Helpers.Hud.Info.ScreenRatio;
							Rectangle9 rec = this.minimap.WorldToMinimap(predictedPosition, num);
							renderer.DrawText(rec + new Size2F(num, 0f), text, System.Drawing.Color.OrangeRed, RendererFontFlags.Left, num, "Calibri");
						}
						if (this.showOnMap)
						{
							float num2 = 32f * O9K.Core.Helpers.Hud.Info.ScreenRatio;
							Rectangle9 rec2 = this.minimap.WorldToScreen(predictedPosition, num2);
							if (!rec2.IsZero)
							{
								renderer.DrawText(rec2 + new Size2F(num2, 0f), text, System.Drawing.Color.OrangeRed, RendererFontFlags.Left, num2, "Calibri");
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

		// Token: 0x0600020D RID: 525 RVA: 0x0001011C File Offset: 0x0000E31C
		private void OnUnitAdded(Unit9 unit)
		{
			try
			{
				if (unit.IsLaneCreep && unit.Team != this.ownerTeam)
				{
					if (!unit.BaseUnit.IsSpawned && unit.BaseUnit.IsAlive)
					{
						LanePosition lane = this.lanePaths.GetCreepLane(unit);
						if (lane != LanePosition.Unknown)
						{
							CreepWave creepWave = this.creepWaves.SingleOrDefault((CreepWave x) => !x.IsSpawned && x.Lane == lane);
							if (creepWave == null)
							{
								this.creepWaves.Add(creepWave = new CreepWave(lane, this.lanePaths.GetLanePath(lane)));
							}
							creepWave.Creeps.Add(unit);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000101EC File Offset: 0x0000E3EC
		private void OnUnitRemoved(Unit9 unit)
		{
			try
			{
				if (unit.IsLaneCreep && unit.Team != this.ownerTeam)
				{
					CreepWave creepWave = this.creepWaves.Find((CreepWave x) => x.Creeps.Contains(unit));
					if (creepWave != null)
					{
						creepWave.Creeps.Remove(unit);
						if (!creepWave.IsValid)
						{
							this.creepWaves.Remove(creepWave);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00010288 File Offset: 0x0000E488
		private void OnUpdate()
		{
			try
			{
				bool flag = !this.mergeSleeper.IsSleeping;
				for (int i = this.creepWaves.Count - 1; i > -1; i--)
				{
					CreepWave wave = this.creepWaves[i];
					if (wave.IsSpawned)
					{
						if (!wave.IsValid)
						{
							this.creepWaves.RemoveAt(i);
						}
						else
						{
							if (flag)
							{
								CreepWave creepWave = this.creepWaves.Find((CreepWave x) => x.IsSpawned && x.Lane == wave.Lane && !x.Equals(wave) && x.PredictedPosition.Distance2D(wave.PredictedPosition, false) < 500f);
								if (creepWave != null)
								{
									creepWave.Creeps.AddRange(wave.Creeps);
									this.creepWaves.RemoveAt(i);
									goto IL_AF;
								}
							}
							wave.Update();
						}
					}
					IL_AF:;
				}
				if (flag)
				{
					this.mergeSleeper.Sleep(2f);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0400016F RID: 367
		private readonly IContext9 context;

		// Token: 0x04000170 RID: 368
		private readonly List<CreepWave> creepWaves = new List<CreepWave>();

		// Token: 0x04000171 RID: 369
		private readonly MenuSwitcher enabled;

		// Token: 0x04000172 RID: 370
		private readonly Sleeper mergeSleeper = new Sleeper();

		// Token: 0x04000173 RID: 371
		private readonly IMinimap minimap;

		// Token: 0x04000174 RID: 372
		private readonly MenuSwitcher showOnMap;

		// Token: 0x04000175 RID: 373
		private readonly MenuSwitcher showOnMinimap;

		// Token: 0x04000176 RID: 374
		private LanePaths lanePaths;

		// Token: 0x04000177 RID: 375
		private Team ownerTeam;
	}
}
