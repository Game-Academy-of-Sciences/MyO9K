using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Terrorblade
{
	// Token: 0x020001CD RID: 461
	[AbilityId(AbilityId.terrorblade_conjure_image)]
	public class ConjureImage : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000942 RID: 2370 RVA: 0x0000857C File Offset: 0x0000677C
		public ConjureImage(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00008597 File Offset: 0x00006797
		public string BuffModifierName { get; } = string.Empty;

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0000859F File Offset: 0x0000679F
		public bool BuffsAlly { get; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x000085A7 File Offset: 0x000067A7
		public bool BuffsOwner { get; } = 1;
	}
}
