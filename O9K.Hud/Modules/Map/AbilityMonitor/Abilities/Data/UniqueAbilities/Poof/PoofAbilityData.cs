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

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Poof
{
	// Token: 0x02000085 RID: 133
	internal class PoofAbilityData : AbilityFullData
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0001AF90 File Offset: 0x00019190
		public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, ParticleEffect particle, Team allyTeam, INotificator notificator)
		{
			if (particle.GetControlPoint(1u) == new Vector3(-1f))
			{
				return;
			}
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
			string text;
			if (unit2.IsHero)
			{
				text = unit2.Name;
			}
			else
			{
				Unit9 owner = unit2.Owner;
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
				Position = (base.RawParticlePosition ? particle.Position : particle.GetControlPoint(base.ControlPoint)).SetZ(new float?((float)350))
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
