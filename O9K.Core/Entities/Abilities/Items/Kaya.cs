using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000174 RID: 372
	[AbilityId(AbilityId.item_kaya)]
	public class Kaya : PassiveAbility, IHasDamageAmplify
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x00007041 File Offset: 0x00005241
		public Kaya(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "spell_amp");
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0000707B File Offset: 0x0000527B
		public DamageType AmplifierDamageType { get; } = 2;

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00007083 File Offset: 0x00005283
		public string AmplifierModifierName { get; } = "modifier_item_kaya";

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0000708B File Offset: 0x0000528B
		public AmplifiesDamage AmplifiesDamage { get; } = 2;

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00007093 File Offset: 0x00005293
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0000709B File Offset: 0x0000529B
		public bool IsAmplifierPermanent { get; } = 1;

		// Token: 0x06000781 RID: 1921 RVA: 0x000070A3 File Offset: 0x000052A3
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			if (!this.IsUsable)
			{
				return 0f;
			}
			return this.amplifierData.GetValue(this.Level) / 100f;
		}

		// Token: 0x0400035B RID: 859
		private readonly SpecialData amplifierData;
	}
}
