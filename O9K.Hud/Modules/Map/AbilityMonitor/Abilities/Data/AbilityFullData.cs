using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Helpers.Notificator.Notifications;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base;
using SharpDX;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data
{
	// Token: 0x02000074 RID: 116
	internal class AbilityFullData
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000387D File Offset: 0x00001A7D
		// (set) Token: 0x0600026B RID: 619 RVA: 0x00003885 File Offset: 0x00001A85
		public bool SearchOwner { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000388E File Offset: 0x00001A8E
		// (set) Token: 0x0600026D RID: 621 RVA: 0x00003896 File Offset: 0x00001A96
		public bool ShowRange { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000389F File Offset: 0x00001A9F
		// (set) Token: 0x0600026F RID: 623 RVA: 0x000038A7 File Offset: 0x00001AA7
		public float Range { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000270 RID: 624 RVA: 0x000038B0 File Offset: 0x00001AB0
		// (set) Token: 0x06000271 RID: 625 RVA: 0x000038B8 File Offset: 0x00001AB8
		public Vector3 RangeColor { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000272 RID: 626 RVA: 0x000038C1 File Offset: 0x00001AC1
		// (set) Token: 0x06000273 RID: 627 RVA: 0x000038C9 File Offset: 0x00001AC9
		public bool IgnoreUnitAbility { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000038D2 File Offset: 0x00001AD2
		// (set) Token: 0x06000275 RID: 629 RVA: 0x000038DA File Offset: 0x00001ADA
		public bool ShowTimer { get; set; } = true;

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000038E3 File Offset: 0x00001AE3
		// (set) Token: 0x06000277 RID: 631 RVA: 0x000038EB File Offset: 0x00001AEB
		public float TimeToShow { get; set; } = 3f;

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000038F4 File Offset: 0x00001AF4
		// (set) Token: 0x06000279 RID: 633 RVA: 0x000038FC File Offset: 0x00001AFC
		public float Duration { get; set; } = 3f;

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00003905 File Offset: 0x00001B05
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000390D File Offset: 0x00001B0D
		public int Vision { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00003916 File Offset: 0x00001B16
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000391E File Offset: 0x00001B1E
		public uint ControlPoint { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00003927 File Offset: 0x00001B27
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000392F File Offset: 0x00001B2F
		public bool RawParticlePosition { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00003938 File Offset: 0x00001B38
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00003940 File Offset: 0x00001B40
		public bool ShowNotification { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00003949 File Offset: 0x00001B49
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00003951 File Offset: 0x00001B51
		public AbilityId AbilityId { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000395A File Offset: 0x00001B5A
		// (set) Token: 0x06000285 RID: 645 RVA: 0x00003962 File Offset: 0x00001B62
		public bool Replace { get; set; }

		// Token: 0x06000286 RID: 646 RVA: 0x0001562C File Offset: 0x0001382C
		public virtual void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, Unit unit, INotificator notificator)
		{
			Entity owner = unit.Owner;
			if (owner == null)
			{
				return;
			}
			string name = owner.Name;
			Vector3 position = unit.Position;
			DrawableUnitAbility drawableUnitAbility = new DrawableUnitAbility
			{
				AbilityTexture = this.AbilityId + "_rounded",
				HeroTexture = name + "_rounded",
				MinimapHeroTexture = name + "_icon",
				Position = position.SetZ(new float?(Math.Min(position.Z, 350f))),
				Unit = unit,
				IsShowingRange = this.ShowRange,
				RangeColor = this.RangeColor,
				Range = this.Range,
				Duration = this.Duration,
				ShowUntil = Game.RawGameTime + this.Duration,
				ShowHeroUntil = Game.RawGameTime + this.TimeToShow,
				ShowTimer = this.ShowTimer,
				Owner = owner
			};
			drawableUnitAbility.DrawRange();
			drawableAbilities.Add(drawableUnitAbility);
			if (this.ShowNotification && notificator != null)
			{
				notificator.PushNotification(new AbilityNotification(name, this.AbilityId.ToString()));
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00015764 File Offset: 0x00013964
		public virtual void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, Ability9 ability, Unit unit, INotificator notificator)
		{
			if (this.IgnoreUnitAbility)
			{
				return;
			}
			Unit9 owner = ability.Owner;
			if (owner.IsVisible)
			{
				return;
			}
			string name = owner.Name;
			DrawableAbility item = new DrawableAbility
			{
				AbilityTexture = ability.Name + "_rounded",
				HeroTexture = name + "_rounded",
				MinimapHeroTexture = name + "_icon",
				Position = unit.Position,
				ShowUntil = Game.RawGameTime + this.TimeToShow
			};
			drawableAbilities.Add(item);
			if (this.ShowNotification && notificator != null)
			{
				notificator.PushNotification(new AbilityNotification(name, ability.Name));
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00015814 File Offset: 0x00013A14
		public virtual void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, ParticleEffect particle, Team allyTeam, INotificator notificator)
		{
			Unit9 unit;
			if (!this.SearchOwner && particle.Owner is Unit)
			{
				unit = EntityManager9.GetUnit(particle.Owner.Handle);
			}
			else
			{
				Ability9 ability = EntityManager9.Abilities.FirstOrDefault((Ability9 x) => x.Id == this.AbilityId && x.Owner.Team != allyTeam && x.Owner.CanUseAbilities);
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
			Vector3 v = this.RawParticlePosition ? particle.Position : particle.GetControlPoint(this.ControlPoint);
			if (v.IsZero)
			{
				Logger.Error("ParticleZero", particle.Name);
				return;
			}
			DrawableAbility drawableAbility = new DrawableAbility
			{
				AbilityTexture = this.AbilityId + "_rounded",
				HeroTexture = text + "_rounded",
				MinimapHeroTexture = text + "_icon",
				ShowUntil = Game.RawGameTime + this.TimeToShow,
				Position = v.SetZ(new float?((float)350))
			};
			if (this.Replace)
			{
				IDrawableAbility drawableAbility2 = drawableAbilities.LastOrDefault((IDrawableAbility x) => x.AbilityTexture == drawableAbility.AbilityTexture && x.HeroTexture == drawableAbility.HeroTexture);
				if (drawableAbility2 != null)
				{
					drawableAbilities.Remove(drawableAbility2);
				}
			}
			drawableAbilities.Add(drawableAbility);
			if (this.ShowNotification && notificator != null)
			{
				notificator.PushNotification(new AbilityNotification(text, this.AbilityId.ToString()));
			}
		}
	}
}
