using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.EmberSpirit
{
	// Token: 0x02000381 RID: 897
	[AbilityId(AbilityId.ember_spirit_fire_remnant)]
	public class FireRemnant : CircleAbility
	{
		// Token: 0x06000F60 RID: 3936 RVA: 0x0000D8F3 File Offset: 0x0000BAF3
		public FireRemnant(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "speed_multiplier");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0000D92F File Offset: 0x0000BB2F
		public override bool HasAreaOfEffect { get; }

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0000D937 File Offset: 0x0000BB37
		public int Charges
		{
			get
			{
				Modifier modifier = base.Owner.GetModifier("modifier_ember_spirit_fire_remnant_charge_counter");
				if (modifier == null)
				{
					return 0;
				}
				return modifier.StackCount;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0000D954 File Offset: 0x0000BB54
		public override float Speed
		{
			get
			{
				return this.SpeedData.GetValue(this.Level) / 100f * base.Owner.Speed;
			}
		}
	}
}
