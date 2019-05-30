using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Managers.Entity;
using SharpDX;

namespace O9K.Core.Entities.Heroes.Unique
{
	// Token: 0x020000D2 RID: 210
	[HeroId(HeroId.npc_dota_hero_slark)]
	internal class Slark : Hero9
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x000061C6 File Offset: 0x000043C6
		public Slark(Hero baseHero) : base(baseHero)
		{
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00021454 File Offset: 0x0001F654
		public override Vector3 GetPredictedPosition(float delay = 0f)
		{
			Unit9 unit = this.shadowDanceUnit;
			if (unit != null && unit.IsValid)
			{
				Vector3 position = this.shadowDanceUnit.Position;
				Vector3 result = this.lastShadowDancePosition.Extend2D(position, delay * Math.Min(base.Speed * (1.25f + this.shadowDanceLevel * 0.05f), 550f));
				this.lastShadowDancePosition = position;
				return result;
			}
			return base.GetPredictedPosition(delay);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000214C4 File Offset: 0x0001F6C4
		internal void ShadowDanced(bool added)
		{
			if (added)
			{
				this.shadowDanceUnit = EntityManager9.Units.FirstOrDefault((Unit9 x) => x.Name == "npc_dota_slark_visual" && x.Owner.Handle == base.Handle);
				Unit9 unit = this.shadowDanceUnit;
				this.lastShadowDancePosition = ((unit != null) ? unit.Position : Vector3.Zero);
				Ability9 ability = base.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.slark_shadow_dance);
				this.shadowDanceLevel = ((ability != null) ? ability.Level : 0u);
				return;
			}
			this.shadowDanceUnit = null;
			this.lastShadowDancePosition = Vector3.Zero;
		}

		// Token: 0x040002D9 RID: 729
		private Vector3 lastShadowDancePosition;

		// Token: 0x040002DA RID: 730
		private uint shadowDanceLevel;

		// Token: 0x040002DB RID: 731
		private Unit9 shadowDanceUnit;
	}
}
