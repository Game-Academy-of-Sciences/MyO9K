using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Slark
{
	// Token: 0x020002CE RID: 718
	[AbilityId(AbilityId.slark_dark_pact)]
	public class DarkPact : AreaOfEffectAbility, IShield, IActiveAbility
	{
		// Token: 0x06000CA1 RID: 3233 RVA: 0x00025C3C File Offset: 0x00023E3C
		public DarkPact(Ability baseAbility) : base(baseAbility)
		{
			this.ActivationDelayData = new SpecialData(baseAbility, "delay");
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "total_damage");
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0000B5BC File Offset: 0x000097BC
		public UnitState AppliesUnitState { get; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0000B5C4 File Offset: 0x000097C4
		public string ShieldModifierName { get; } = "modifier_slark_dark_pact";

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0000B5CC File Offset: 0x000097CC
		public bool ShieldsAlly { get; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0000B5D4 File Offset: 0x000097D4
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0000B5DC File Offset: 0x000097DC
		public override float GetHitTime(Unit9 unit)
		{
			if (base.Owner.Equals(unit))
			{
				return this.GetCastDelay();
			}
			return this.GetHitTime(unit.Position);
		}
	}
}
