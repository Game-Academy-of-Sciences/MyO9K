using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using Ensage.SDK.Geometry;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Helpers.Notificator.Notifications;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base;
using SharpDX;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Item
{
	// Token: 0x02000089 RID: 137
	internal class NoOwnerItemData : AbilityFullData
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0001B2C0 File Offset: 0x000194C0
		public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, ParticleEffect particle, Team allyTeam, INotificator notificator)
		{
			Vector3 position = base.RawParticlePosition ? particle.Position : particle.GetControlPoint(base.ControlPoint);
			if (position.IsZero)
			{
				Logger.Error("ParticleZero", particle.Name);
				return;
			}
			Ability9 ability = (from x in EntityManager9.Abilities
			where x.Id == this.AbilityId && x.Owner.CanUseAbilities
			orderby x.Owner.Position.Distance2D(position, false)
			select x).FirstOrDefault<Ability9>();
			Unit9 unit = (ability != null) ? ability.Owner : null;
			if (unit == null || unit.IsVisible || unit.Team == allyTeam)
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
				Position = position.SetZ(new float?((float)350))
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
			if (base.ShowNotification && notificator != null)
			{
				notificator.PushNotification(new AbilityNotification(name, base.AbilityId.ToString()));
			}
		}
	}
}
