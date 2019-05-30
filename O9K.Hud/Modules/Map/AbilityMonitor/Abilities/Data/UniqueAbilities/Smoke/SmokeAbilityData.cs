using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.Core.Entities.Heroes;
using O9K.Core.Managers.Entity;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Helpers.Notificator.Notifications;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base;
using SharpDX;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Smoke
{
	// Token: 0x0200007E RID: 126
	internal class SmokeAbilityData : AbilityFullData
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0001A898 File Offset: 0x00018A98
		public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, ParticleEffect particle, Team allyTeam, INotificator notificator)
		{
			Vector3 position = particle.GetControlPoint(base.ControlPoint);
			if (EntityManager9.Heroes.Any((Hero9 x) => x.IsUnit && x.Team == allyTeam && x.IsAlive && x.Distance(position) < 800f))
			{
				return;
			}
			SimpleDrawableAbility drawableAbility = new SimpleDrawableAbility
			{
				AbilityTexture = base.AbilityId + "_rounded",
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
			if (base.ShowNotification && notificator != null)
			{
				notificator.PushNotification(new AbilityNotification(null, base.AbilityId.ToString()));
			}
		}
	}
}
