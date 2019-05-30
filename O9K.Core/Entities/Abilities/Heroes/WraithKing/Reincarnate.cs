using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.WraithKing
{
	// Token: 0x0200026A RID: 618
	[AbilityId(AbilityId.skeleton_king_reincarnation)]
	public class Reincarnate : PassiveAbility, IHasDamageAmplify
	{
		// Token: 0x06000B38 RID: 2872 RVA: 0x0000A1D3 File Offset: 0x000083D3
		public Reincarnate(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0000A1F5 File Offset: 0x000083F5
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0000A1FD File Offset: 0x000083FD
		public string AmplifierModifierName { get; } = "modifier_skeleton_king_reincarnation_scepter_active";

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0000A205 File Offset: 0x00008405
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0000A20D File Offset: 0x0000840D
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0000A215 File Offset: 0x00008415
		public bool IsAmplifierPermanent { get; }

		// Token: 0x06000B3E RID: 2878 RVA: 0x00006D18 File Offset: 0x00004F18
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return -1f;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0000A21D File Offset: 0x0000841D
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return this.IsReady;
		}
	}
}
