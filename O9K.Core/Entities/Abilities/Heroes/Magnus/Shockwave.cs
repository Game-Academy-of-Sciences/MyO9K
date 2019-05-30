using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Magnus
{
	// Token: 0x0200020B RID: 523
	[AbilityId(AbilityId.magnataur_shockwave)]
	public class Shockwave : LineAbility, INuke, IActiveAbility
	{
		// Token: 0x06000A09 RID: 2569 RVA: 0x000239D4 File Offset: 0x00021BD4
		public Shockwave(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "shock_width");
			this.DamageData = new SpecialData(baseAbility, "shock_damage");
			this.SpeedData = new SpecialData(baseAbility, "shock_speed");
			this.scepterSpeedData = new SpecialData(baseAbility, "scepter_speed");
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0000910B File Offset: 0x0000730B
		public override float Speed
		{
			get
			{
				if (base.Owner.HasAghanimsScepter)
				{
					return this.scepterSpeedData.GetValue(this.Level);
				}
				return base.Speed;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00009132 File Offset: 0x00007332
		public override bool UnitTargetCast { get; }

		// Token: 0x04000514 RID: 1300
		private readonly SpecialData scepterSpeedData;
	}
}
