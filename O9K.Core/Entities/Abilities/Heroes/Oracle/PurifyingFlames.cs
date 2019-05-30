using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Oracle
{
	// Token: 0x020001AA RID: 426
	[AbilityId(AbilityId.oracle_purifying_flames)]
	public class PurifyingFlames : RangedAbility, IHealthRestore, INuke, IActiveAbility
	{
		// Token: 0x060008A8 RID: 2216 RVA: 0x000224A4 File Offset: 0x000206A4
		public PurifyingFlames(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.scepterCastPoint = new SpecialData(baseAbility, "castpoint_scepter");
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00007ED1 File Offset: 0x000060D1
		public bool InstantHealthRestore { get; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00007ED9 File Offset: 0x000060D9
		public override float CastPoint
		{
			get
			{
				if (base.Owner.HasAghanimsScepter)
				{
					return this.scepterCastPoint.GetValue(this.Level);
				}
				return base.CastPoint;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00007F00 File Offset: 0x00006100
		public string HealModifierName { get; } = string.Empty;

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00007F08 File Offset: 0x00006108
		public bool RestoresAlly { get; } = 1;

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00007F10 File Offset: 0x00006110
		public bool RestoresOwner { get; } = 1;

		// Token: 0x060008AE RID: 2222 RVA: 0x0000372C File Offset: 0x0000192C
		public int HealthRestoreValue(Unit9 unit)
		{
			return 0;
		}

		// Token: 0x04000442 RID: 1090
		private readonly SpecialData scepterCastPoint;
	}
}
