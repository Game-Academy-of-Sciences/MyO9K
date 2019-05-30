using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Ursa
{
	// Token: 0x02000289 RID: 649
	[AbilityId(AbilityId.ursa_enrage)]
	public class Enrage : ActiveAbility, IShield, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06000B96 RID: 2966 RVA: 0x000250B8 File Offset: 0x000232B8
		public Enrage(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "damage_reduction");
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0000A733 File Offset: 0x00008933
		public UnitState AppliesUnitState { get; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0000A73B File Offset: 0x0000893B
		public bool ProvidesStatusResistance
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_ursa_6);
				return abilityById != null && abilityById.Level > 0u;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0000A761 File Offset: 0x00008961
		public override bool TargetsEnemy { get; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0000A769 File Offset: 0x00008969
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0000A771 File Offset: 0x00008971
		public string AmplifierModifierName { get; } = "modifier_ursa_enrage";

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0000A779 File Offset: 0x00008979
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0000A781 File Offset: 0x00008981
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0000A789 File Offset: 0x00008989
		public bool IsAmplifierPermanent { get; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0000A791 File Offset: 0x00008991
		public string ShieldModifierName { get; } = "modifier_ursa_enrage";

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0000A799 File Offset: 0x00008999
		public bool ShieldsAlly { get; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0000A7A1 File Offset: 0x000089A1
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0000A7A9 File Offset: 0x000089A9
		protected override bool CanBeCastedWhileStunned
		{
			get
			{
				return base.Owner.HasAghanimsScepter;
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0000A7B6 File Offset: 0x000089B6
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / -100f;
		}

		// Token: 0x040005EE RID: 1518
		private readonly SpecialData amplifierData;
	}
}
