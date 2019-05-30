using System;
using System.Collections.Generic;
using Ensage;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.ThinkerAbility
{
	// Token: 0x0200007D RID: 125
	internal class ThinkerUnitAbilityData : AbilityFullData
	{
		// Token: 0x060002CE RID: 718 RVA: 0x0001A7B8 File Offset: 0x000189B8
		public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, Unit unit, INotificator notificator)
		{
			Hero hero;
			if ((hero = (unit.Owner as Hero)) == null)
			{
				return;
			}
			HeroId heroId = hero.HeroId;
			if (heroId == HeroId.npc_dota_hero_tinker)
			{
				base.AbilityId = AbilityId.tinker_march_of_the_machines;
				string name = hero.Name;
				DrawableUnitAbility item = new DrawableUnitAbility
				{
					AbilityTexture = base.AbilityId + "_rounded",
					HeroTexture = name + "_rounded",
					MinimapHeroTexture = name + "_icon",
					Position = unit.Position,
					Unit = unit,
					Duration = base.Duration,
					ShowUntil = Game.RawGameTime + base.Duration,
					ShowHeroUntil = Game.RawGameTime + base.TimeToShow,
					ShowTimer = base.ShowTimer,
					Owner = hero
				};
				drawableAbilities.Add(item);
				return;
			}
		}
	}
}
