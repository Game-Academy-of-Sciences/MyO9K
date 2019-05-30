using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Invoker;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Units.Unique
{
	// Token: 0x020000BC RID: 188
	[UnitName("npc_dota_invoker_forged_spirit")]
	internal class ForgedSpirit : Unit9
	{
		// Token: 0x060005E7 RID: 1511 RVA: 0x00020C40 File Offset: 0x0001EE40
		public ForgedSpirit(Unit baseUnit) : base(baseUnit)
		{
			ForgeSpirit forgeSpirit = (ForgeSpirit)EntityManager9.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.invoker_forge_spirit);
			if (forgeSpirit == null)
			{
				return;
			}
			this.BaseAttackRange = forgeSpirit.ForgeSpiritAttackRange;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00005EAD File Offset: 0x000040AD
		internal override float BaseAttackRange { get; } = 900f;
	}
}
