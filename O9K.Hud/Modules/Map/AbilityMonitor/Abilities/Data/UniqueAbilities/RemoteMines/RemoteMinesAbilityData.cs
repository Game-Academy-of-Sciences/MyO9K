using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using Ensage.SDK.Geometry;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base;
using SharpDX;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.RemoteMines
{
	// Token: 0x02000081 RID: 129
	internal class RemoteMinesAbilityData : AbilityFullData
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x0001ABDC File Offset: 0x00018DDC
		public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, Unit unit, INotificator notificator)
		{
			DrawableRemoteMinesAbility drawableRemoteMinesAbility = drawableAbilities.OfType<DrawableRemoteMinesAbility>().FirstOrDefault((DrawableRemoteMinesAbility x) => x.Unit == null && x.Position.Distance2D(unit.Position, false) < 10f);
			if (drawableRemoteMinesAbility != null)
			{
				drawableRemoteMinesAbility.AddUnit(unit);
				return;
			}
			Entity owner = unit.Owner;
			string name = owner.Name;
			DrawableUnitAbility drawableUnitAbility = new DrawableUnitAbility
			{
				AbilityTexture = base.AbilityId + "_rounded",
				HeroTexture = name + "_rounded",
				MinimapHeroTexture = name + "_icon",
				Position = unit.Position,
				Unit = unit,
				IsShowingRange = base.ShowRange,
				Range = base.Range,
				RangeColor = base.RangeColor,
				Duration = base.Duration,
				ShowUntil = Game.RawGameTime + base.Duration,
				ShowHeroUntil = Game.RawGameTime + base.TimeToShow,
				Owner = owner
			};
			drawableUnitAbility.DrawRange();
			drawableAbilities.Add(drawableUnitAbility);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0001ACFC File Offset: 0x00018EFC
		public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, ParticleEffect particle, Team allyTeam, INotificator notificator)
		{
			Vector3 position = particle.GetControlPoint(base.ControlPoint);
			if (particle.Name.Contains("detonate"))
			{
				DrawableRemoteMinesAbility drawableRemoteMinesAbility = drawableAbilities.OfType<DrawableRemoteMinesAbility>().FirstOrDefault((DrawableRemoteMinesAbility x) => x.Position.Distance2D(position, false) < 10f);
				if (drawableRemoteMinesAbility != null)
				{
					if (drawableRemoteMinesAbility.IsShowingRange)
					{
						drawableRemoteMinesAbility.RemoveRange();
					}
					drawableAbilities.Remove(drawableRemoteMinesAbility);
					return;
				}
			}
			else
			{
				if (ObjectManager.GetEntitiesFast<Unit>().FirstOrDefault((Unit x) => x.Name == "npc_dota_techies_remote_mine" && x.Distance2D(position) < 10f) != null)
				{
					return;
				}
				Unit9 owner = EntityManager9.GetUnit(particle.Owner.Handle);
				if (owner == null)
				{
					return;
				}
				UpdateManager.BeginInvoke(delegate
				{
					try
					{
						if (particle.IsValid)
						{
							DrawableRemoteMinesAbility drawableRemoteMinesAbility2 = new DrawableRemoteMinesAbility
							{
								AbilityTexture = this.AbilityId + "_rounded",
								HeroTexture = owner.Name + "_rounded",
								MinimapHeroTexture = owner.Name + "_icon",
								Position = position.SetZ(new float?((float)350)),
								Duration = this.Duration,
								IsShowingRange = this.ShowRange,
								Range = this.Range,
								RangeColor = this.RangeColor,
								ShowUntil = Game.RawGameTime + this.Duration,
								ShowHeroUntil = Game.RawGameTime + this.TimeToShow,
								Owner = owner.BaseEntity
							};
							drawableRemoteMinesAbility2.DrawRange();
							drawableAbilities.Add(drawableRemoteMinesAbility2);
						}
					}
					catch (Exception exception)
					{
						Logger.Error(exception, null);
					}
				}, 1000);
			}
		}
	}
}
