using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.Abaddon
{
	// Token: 0x020003D8 RID: 984
	[AbilityId(AbilityId.abaddon_borrowed_time)]
	public class BorrowedTime : ActiveAbility, IHasDamageAmplify
	{
		// Token: 0x06001059 RID: 4185 RVA: 0x0000E697 File Offset: 0x0000C897
		public BorrowedTime(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x0000E6C0 File Offset: 0x0000C8C0
		public override bool TargetsEnemy { get; }

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0000E6D0 File Offset: 0x0000C8D0
		public string AmplifierModifierName { get; } = "modifier_abaddon_borrowed_time";

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x0000E6D8 File Offset: 0x0000C8D8
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0000E6E0 File Offset: 0x0000C8E0
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x0000E6E8 File Offset: 0x0000C8E8
		public bool IsAmplifierPermanent { get; }

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		protected override bool CanBeCastedWhileStunned { get; } = 1;

		// Token: 0x06001061 RID: 4193 RVA: 0x00006D18 File Offset: 0x00004F18
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return -1f;
		}
	}
}
