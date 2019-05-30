using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Windranger
{
	// Token: 0x020001C6 RID: 454
	[AbilityId(AbilityId.windrunner_powershot)]
	public class Powershot : LineAbility, INuke, IChanneled, IActiveAbility
	{
		// Token: 0x06000920 RID: 2336 RVA: 0x00022D14 File Offset: 0x00020F14
		public Powershot(Ability baseAbility) : base(baseAbility)
		{
			this.ChannelTime = baseAbility.GetChannelTime(0u);
			this.RadiusData = new SpecialData(baseAbility, "arrow_width");
			this.SpeedData = new SpecialData(baseAbility, "arrow_speed");
			this.DamageData = new SpecialData(baseAbility, "powershot_damage");
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00008406 File Offset: 0x00006606
		public override bool HasAreaOfEffect { get; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0000840E File Offset: 0x0000660E
		public override float ActivationDelay
		{
			get
			{
				return this.ChannelTime;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00008416 File Offset: 0x00006616
		public float ChannelTime { get; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0000841E File Offset: 0x0000661E
		public bool IsActivatesOnChannelStart { get; } = 1;

		// Token: 0x06000925 RID: 2341 RVA: 0x00008426 File Offset: 0x00006626
		public int GetCurrentDamage(Unit9 unit)
		{
			return (int)((float)this.GetDamage(unit) * (base.BaseAbility.ChannelTime / this.ChannelTime));
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00022D70 File Offset: 0x00020F70
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage rawDamage;
			Damage result = rawDamage = base.GetRawDamage(unit, remainingHealth);
			DamageType damageType = this.DamageType;
			rawDamage[damageType] *= 0.8f;
			return result;
		}
	}
}
