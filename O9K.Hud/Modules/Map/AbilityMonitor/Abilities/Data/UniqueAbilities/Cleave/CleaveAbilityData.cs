using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using Ensage.SDK.Geometry;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base;
using SharpDX;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Cleave
{
	// Token: 0x02000090 RID: 144
	internal class CleaveAbilityData : AbilityFullData
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0001B808 File Offset: 0x00019A08
		public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, ParticleEffect particle, Team allyTeam, INotificator notificator)
		{
			Unit9 unit = EntityManager9.GetUnit(particle.Owner.Handle);
			if (unit == null || unit.IsVisible)
			{
				return;
			}
			Vector3 v = base.RawParticlePosition ? particle.Position : particle.GetControlPoint(base.ControlPoint);
			if (v.Distance2D(unit.BaseUnit.Position, false) < 50f)
			{
				return;
			}
			string name = unit.Name;
			DrawableAbility drawableAbility = new DrawableAbility
			{
				AbilityTexture = base.AbilityId + "_rounded",
				HeroTexture = name + "_rounded",
				MinimapHeroTexture = name + "_icon",
				ShowUntil = Game.RawGameTime + base.TimeToShow,
				Position = v.SetZ(new float?((float)350))
			};
			if (base.Replace)
			{
				IDrawableAbility drawableAbility2 = drawableAbilities.LastOrDefault((IDrawableAbility x) => x.AbilityTexture == drawableAbility.AbilityTexture && x.HeroTexture == drawableAbility.HeroTexture);
				if (drawableAbility2 != null)
				{
					drawableAbilities.Remove(drawableAbility2);
				}
			}
			drawableAbilities.Add(drawableAbility);
		}
	}
}
