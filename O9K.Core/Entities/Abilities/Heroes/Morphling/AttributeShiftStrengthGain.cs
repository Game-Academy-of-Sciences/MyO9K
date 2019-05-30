using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Abilities.Heroes.Morphling
{
	// Token: 0x020001FD RID: 509
	[AbilityId(AbilityId.morphling_morph_str)]
	public class AttributeShiftStrengthGain : ToggleAbility, IHealthRestore, IActiveAbility
	{
		// Token: 0x060009DF RID: 2527 RVA: 0x00008E7F File Offset: 0x0000707F
		public AttributeShiftStrengthGain(Ability baseAbility) : base(baseAbility)
		{
			this.attributeGainData = new SpecialData(baseAbility, "morph_rate_tooltip");
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00008EB2 File Offset: 0x000070B2
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00008EBA File Offset: 0x000070BA
		public AttributeShiftAgilityGain AttributeShiftAgilityGain { get; private set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00008EC3 File Offset: 0x000070C3
		public override bool CanBeCastedWhileChanneling { get; } = 1;

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00008ECB File Offset: 0x000070CB
		public bool InstantHealthRestore { get; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00008ED3 File Offset: 0x000070D3
		public string HealModifierName { get; } = "modifier_morphling_morph_str";

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00008EDB File Offset: 0x000070DB
		public bool RestoresAlly { get; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00008EE3 File Offset: 0x000070E3
		public bool RestoresOwner { get; } = 1;

		// Token: 0x060009E7 RID: 2535 RVA: 0x00008EEB File Offset: 0x000070EB
		public float HealthGain(float seconds)
		{
			return this.attributeGainData.GetValue(this.Level) * seconds * 20f;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0000372C File Offset: 0x0000192C
		public int HealthRestoreValue(Unit9 unit)
		{
			return 0;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000237B8 File Offset: 0x000219B8
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.morphling_morph_agi)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				throw new ArgumentNullException("AttributeShiftAgilityGain");
			}
			this.AttributeShiftAgilityGain = (AttributeShiftAgilityGain)EntityManager9.AddAbility(ability);
		}

		// Token: 0x040004FD RID: 1277
		private readonly SpecialData attributeGainData;
	}
}
