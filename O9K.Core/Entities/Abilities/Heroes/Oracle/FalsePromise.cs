using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.Oracle
{
	// Token: 0x020001A7 RID: 423
	[AbilityId(AbilityId.oracle_false_promise)]
	public class FalsePromise : RangedAbility, IShield, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x000223E0 File Offset: 0x000205E0
		public FalsePromise(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00007E08 File Offset: 0x00006008
		public UnitState AppliesUnitState { get; } = 256L;

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x00007E10 File Offset: 0x00006010
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00007E18 File Offset: 0x00006018
		public string AmplifierModifierName { get; } = "modifier_oracle_false_promise";

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x00007E20 File Offset: 0x00006020
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00007E28 File Offset: 0x00006028
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x00007E30 File Offset: 0x00006030
		public bool IsAmplifierPermanent { get; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00007E38 File Offset: 0x00006038
		public string ShieldModifierName { get; } = "modifier_oracle_false_promise";

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00007E40 File Offset: 0x00006040
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00007E48 File Offset: 0x00006048
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x0600089C RID: 2204 RVA: 0x00006D18 File Offset: 0x00004F18
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return -1f;
		}
	}
}
