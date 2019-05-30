using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Pangolier
{
	// Token: 0x02000307 RID: 775
	[AbilityId(AbilityId.pangolier_shield_crash)]
	public class ShieldCrash : AreaOfEffectAbility, INuke, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06000D5B RID: 3419 RVA: 0x000269E8 File Offset: 0x00024BE8
		public ShieldCrash(Ability baseAbility) : base(baseAbility)
		{
			this.ActivationDelayData = new SpecialData(baseAbility, "jump_duration");
			this.activationDelayThunderData = new SpecialData(baseAbility, "jump_duration_gyroshell");
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.castRangeData = new SpecialData(baseAbility, "jump_horizontal_distance");
			this.castRangeThunderData = new SpecialData(baseAbility, "jump_height_gyroshell");
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0000BE1D File Offset: 0x0000A01D
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0000BE25 File Offset: 0x0000A025
		public string AmplifierModifierName { get; } = "modifier_pangolier_shield_crash_buff";

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0000BE2D File Offset: 0x0000A02D
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0000BE35 File Offset: 0x0000A035
		public override float ActivationDelay
		{
			get
			{
				if (base.Owner.HasModifier("modifier_pangolier_gyroshell"))
				{
					return this.activationDelayThunderData.GetValue(this.Level);
				}
				return base.ActivationDelay;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0000BE61 File Offset: 0x0000A061
		public override float CastRange
		{
			get
			{
				if (base.Owner.HasModifier("modifier_pangolier_gyroshell"))
				{
					return this.castRangeThunderData.GetValue(this.Level);
				}
				return this.castRangeData.GetValue(this.Level);
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0000BE98 File Offset: 0x0000A098
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
		public bool IsAmplifierPermanent { get; }

		// Token: 0x06000D63 RID: 3427 RVA: 0x0000BEA8 File Offset: 0x0000A0A8
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			Modifier modifier = target.BaseModifiers.FirstOrDefault((Modifier x) => x.Name == this.AmplifierModifierName);
			return (float)((modifier != null) ? modifier.StackCount : 0) / -100f;
		}

		// Token: 0x040006E4 RID: 1764
		private readonly SpecialData activationDelayThunderData;

		// Token: 0x040006E5 RID: 1765
		private readonly SpecialData castRangeData;

		// Token: 0x040006E6 RID: 1766
		private readonly SpecialData castRangeThunderData;
	}
}
