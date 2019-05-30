using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base;
using SharpDX;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Maelstorm
{
	// Token: 0x02000087 RID: 135
	internal class MaelstormAbilityData : AbilityFullData
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x0001B110 File Offset: 0x00019310
		public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, ParticleEffect particle, Team allyTeam, INotificator notificator)
		{
			Vector3 position = (base.RawParticlePosition ? particle.Position : particle.GetControlPoint(base.ControlPoint)).SetZ(new float?((float)350));
			if (EntityManager9.AllyUnits.Any((Unit9 x) => x.IsAlive && x.Distance(position) < 400f))
			{
				return;
			}
			Ability9 ability = EntityManager9.Abilities.FirstOrDefault((Ability9 x) => (x.Id == this.AbilityId || x.Id == AbilityId.item_mjollnir) && x.Owner.Team != allyTeam && x.Owner.CanUseAbilities);
			Unit9 unit = (ability != null) ? ability.Owner : null;
			if (unit == null || unit.IsVisible)
			{
				return;
			}
			string text;
			if (unit.IsHero)
			{
				text = unit.Name;
			}
			else
			{
				Unit9 owner = unit.Owner;
				text = ((owner != null) ? owner.Name : null);
				if (text == null)
				{
					return;
				}
			}
			DrawableAbility drawableAbility = new DrawableAbility
			{
				AbilityTexture = base.AbilityId + "_rounded",
				HeroTexture = text + "_rounded",
				MinimapHeroTexture = text + "_icon",
				ShowUntil = Game.RawGameTime + base.TimeToShow,
				Position = position
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
