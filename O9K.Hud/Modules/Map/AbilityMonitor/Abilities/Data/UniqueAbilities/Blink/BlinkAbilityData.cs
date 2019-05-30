using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Blink
{
	// Token: 0x02000094 RID: 148
	internal class BlinkAbilityData : AbilityFullData
	{
		// Token: 0x06000322 RID: 802 RVA: 0x0001BBC4 File Offset: 0x00019DC4
		public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, ParticleEffect particle, Team allyTeam, INotificator notificator)
		{
			Unit9 unit;
			if (!base.SearchOwner && particle.Owner is Unit)
			{
				unit = EntityManager9.GetUnit(particle.Owner.Handle);
			}
			else
			{
				Ability9 ability = EntityManager9.Abilities.FirstOrDefault((Ability9 x) => x.Id == this.AbilityId && x.Owner.Team != allyTeam);
				unit = ((ability != null) ? ability.Owner : null);
			}
			Unit9 unit2 = unit;
			if (unit2 == null || unit2.IsVisible)
			{
				return;
			}
			if (Game.RawGameTime - unit2.LastVisibleTime >= 1f)
			{
				DrawableAbility item = new DrawableAbility
				{
					AbilityTexture = base.AbilityId + "_rounded",
					HeroTexture = unit2.Name + "_rounded",
					MinimapHeroTexture = unit2.Name + "_icon",
					ShowUntil = Game.RawGameTime + base.TimeToShow,
					Position = particle.GetControlPoint(base.ControlPoint)
				};
				drawableAbilities.Add(item);
				return;
			}
			Ability9 ability2 = unit2.Abilities.FirstOrDefault((Ability9 x) => x.Id == this.AbilityId);
			if (ability2 == null)
			{
				return;
			}
			DrawableAbility item2 = new DrawableAbility
			{
				AbilityTexture = base.AbilityId + "_rounded",
				HeroTexture = unit2.Name + "_rounded",
				MinimapHeroTexture = unit2.Name + "_icon",
				ShowUntil = Game.RawGameTime + base.TimeToShow,
				Position = unit2.InFront(ability2.Range, 0f, true)
			};
			drawableAbilities.Add(item2);
		}
	}
}
