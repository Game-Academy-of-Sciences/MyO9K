using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Alchemist
{
	// Token: 0x020003D3 RID: 979
	[AbilityId(AbilityId.alchemist_chemical_rage)]
	public class ChemicalRage : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06001047 RID: 4167 RVA: 0x0000E57E File Offset: 0x0000C77E
		public ChemicalRage(Ability baseAbility) : base(baseAbility)
		{
			this.attackTimeData = new SpecialData(baseAbility, "base_attack_time");
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0000E5AA File Offset: 0x0000C7AA
		public float AttackTime
		{
			get
			{
				return this.attackTimeData.GetValue(this.Level);
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x0000E5BD File Offset: 0x0000C7BD
		public string BuffModifierName { get; } = "modifier_alchemist_chemical_rage";

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0000E5C5 File Offset: 0x0000C7C5
		public bool BuffsAlly { get; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x0000E5CD File Offset: 0x0000C7CD
		public bool BuffsOwner { get; } = 1;

		// Token: 0x0400087B RID: 2171
		private readonly SpecialData attackTimeData;
	}
}
