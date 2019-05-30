using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base;
using SharpDX;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.FireRemnant
{
	// Token: 0x0200008C RID: 140
	internal class FireRemnantAbilityData : AbilityFullData
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00003F36 File Offset: 0x00002136
		// (set) Token: 0x0600030C RID: 780 RVA: 0x00003F3E File Offset: 0x0000213E
		public uint StartControlPoint { get; set; }

		// Token: 0x0600030D RID: 781 RVA: 0x0001B590 File Offset: 0x00019790
		public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, ParticleEffect particle, Team allyTeam, INotificator notificator)
		{
			if (particle.Name.Contains("dash"))
			{
				drawableAbilities.RemoveAll((IDrawableAbility x) => x is DrawableFireRemnantAbility);
				return;
			}
			Unit9 unit = EntityManager9.GetUnit(particle.Owner.Owner.Handle);
			if (unit == null)
			{
				return;
			}
			Vector3 startPosition = particle.GetControlPoint(this.StartControlPoint);
			if (!unit.IsVisible)
			{
				DrawableAbility item = new DrawableAbility
				{
					AbilityTexture = base.AbilityId + "_rounded",
					HeroTexture = unit.Name + "_rounded",
					MinimapHeroTexture = unit.Name + "_icon",
					ShowUntil = Game.RawGameTime + base.TimeToShow,
					Position = startPosition.SetZ(new float?((float)350))
				};
				drawableAbilities.Add(item);
			}
			DrawableFireRemnantAbility[] remnants = drawableAbilities.OfType<DrawableFireRemnantAbility>().ToArray<DrawableFireRemnantAbility>();
			Unit unit2 = ObjectManager.GetEntitiesFast<Unit>().Concat(ObjectManager.GetDormantEntities<Unit>()).FirstOrDefault((Unit x) => x.IsAlive && x.Name == "npc_dota_ember_spirit_remnant" && x.Distance2D(startPosition) < 1500f && remnants.All((DrawableFireRemnantAbility z) => z.Unit != x));
			if (unit2 == null)
			{
				return;
			}
			DrawableFireRemnantAbility item2 = new DrawableFireRemnantAbility
			{
				AbilityTexture = base.AbilityId + "_rounded",
				HeroTexture = unit.Name + "_rounded",
				MinimapHeroTexture = unit.Name + "_icon",
				Position = particle.GetControlPoint(base.ControlPoint).SetZ(new float?((float)350)),
				Duration = base.Duration,
				ShowUntil = Game.RawGameTime + base.Duration,
				ShowHeroUntil = Game.RawGameTime + base.TimeToShow,
				Owner = unit.BaseEntity,
				Unit = unit2
			};
			drawableAbilities.Add(item2);
		}
	}
}
