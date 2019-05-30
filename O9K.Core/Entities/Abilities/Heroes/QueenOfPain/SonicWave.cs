using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Heroes.QueenOfPain
{
	// Token: 0x020001EA RID: 490
	[AbilityId(AbilityId.queenofpain_sonic_wave)]
	public class SonicWave : ConeAbility, INuke, IActiveAbility
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x0002351C File Offset: 0x0002171C
		public SonicWave(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "starting_aoe");
			this.EndRadiusData = new SpecialData(baseAbility, "final_aoe");
			this.RangeData = new SpecialData(baseAbility, "distance");
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.scepterDamageData = new SpecialData(baseAbility, "damage_scepter");
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00023598 File Offset: 0x00021798
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			if (base.Owner.HasAghanimsScepter)
			{
				Damage damage = new Damage();
				DamageType damageType = this.DamageType;
				damage[damageType] = this.scepterDamageData.GetValue(this.Level);
				return damage;
			}
			return base.GetRawDamage(unit, remainingHealth);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000235E0 File Offset: 0x000217E0
		public override bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
		{
			position = base.Owner.Position.Extend2D(position, Math.Min(this.CastRange, base.Owner.Distance(position)));
			bool flag = base.BaseAbility.UseAbility(position, queue, bypass);
			if (flag)
			{
				base.ActionSleeper.Sleep(0.1f);
			}
			return flag;
		}

		// Token: 0x040004D9 RID: 1241
		private readonly SpecialData scepterDamageData;
	}
}
