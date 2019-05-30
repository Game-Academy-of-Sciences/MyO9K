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

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Blink
{
	// Token: 0x02000092 RID: 146
	internal class BlinkItemAbilityData : AbilityFullData
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0001B924 File Offset: 0x00019B24
		public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, ParticleEffect particle, Team allyTeam, INotificator notificator)
		{
			Vector3 position = particle.GetControlPoint(base.ControlPoint);
			List<Ability9> list = (from x in EntityManager9.Abilities
			where x.Id == this.AbilityId && x.Owner.CanUseAbilities && x.Owner.IsAlive
			select x).ToList<Ability9>();
			if (list.Count == 0 || list.All((Ability9 x) => x.Owner.Team == allyTeam))
			{
				return;
			}
			if (list.Any((Ability9 x) => x.Owner.IsVisible && x.Owner.Distance(position) < 1500f && x.TimeSinceCasted < 0.5f))
			{
				return;
			}
			List<Ability9> list2 = (from x in list
			where x.Owner.Team != allyTeam && !x.Owner.IsVisible && x.RemainingCooldown <= 0f
			select x).ToList<Ability9>();
			if (list2.Count == 0)
			{
				return;
			}
			if (list2.Count == 1)
			{
				Unit9 owner = list2[0].Owner;
				DrawableAbility item = new DrawableAbility
				{
					AbilityTexture = base.AbilityId + "_rounded",
					HeroTexture = owner.Name + "_rounded",
					MinimapHeroTexture = owner.Name + "_icon",
					ShowUntil = Game.RawGameTime + base.TimeToShow,
					Position = ((Game.RawGameTime - owner.LastVisibleTime < 0.5f) ? owner.InFront(list2[0].Range - 200f, 0f, true) : position.SetZ(new float?((float)350)))
				};
				drawableAbilities.Add(item);
				return;
			}
			Ability9 ability = list2.Find((Ability9 x) => x.Owner.Distance(position) < 100f && Game.RawGameTime - x.Owner.LastVisibleTime < 0.5f);
			Unit9 unit = (ability != null) ? ability.Owner : null;
			if (unit != null)
			{
				DrawableAbility item2 = new DrawableAbility
				{
					AbilityTexture = base.AbilityId + "_rounded",
					HeroTexture = unit.Name + "_rounded",
					MinimapHeroTexture = unit.Name + "_icon",
					ShowUntil = Game.RawGameTime + base.TimeToShow,
					Position = unit.InFront(list2[0].Range - 200f, 0f, true)
				};
				drawableAbilities.Add(item2);
				return;
			}
			SimpleDrawableAbility item3 = new SimpleDrawableAbility
			{
				AbilityTexture = base.AbilityId + "_rounded",
				ShowUntil = Game.RawGameTime + base.TimeToShow,
				Position = (base.RawParticlePosition ? particle.Position : particle.GetControlPoint(base.ControlPoint)).SetZ(new float?((float)350))
			};
			drawableAbilities.Add(item3);
		}
	}
}
