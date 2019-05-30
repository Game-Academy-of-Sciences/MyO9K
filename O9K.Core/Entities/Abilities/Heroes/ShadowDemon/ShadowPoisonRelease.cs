using System;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Abilities.Heroes.ShadowDemon
{
	// Token: 0x020001E2 RID: 482
	[AbilityId(AbilityId.shadow_demon_shadow_poison_release)]
	public class ShadowPoisonRelease : ActiveAbility, INuke, IActiveAbility
	{
		// Token: 0x0600098C RID: 2444 RVA: 0x00006683 File Offset: 0x00004883
		public ShadowPoisonRelease(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x000089CF File Offset: 0x00006BCF
		public override DamageType DamageType
		{
			get
			{
				return this.shadowPoison.DamageType;
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00023318 File Offset: 0x00021518
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Modifier modifierByName = unit.BaseUnit.GetModifierByName("modifier_shadow_demon_shadow_poison");
			int num = (modifierByName != null) ? modifierByName.StackCount : 0;
			if (num <= 0)
			{
				return new Damage();
			}
			float value = this.maxStacksData.GetValue(this.Level);
			float num2 = Math.Min((float)num, value);
			float value2 = this.DamageData.GetValue(this.Level);
			float num3 = (float)((int)Math.Pow(2.0, (double)(num2 - 1f))) * value2;
			float num4 = Math.Max((float)num - value, 0f);
			if (num4 > 0f)
			{
				float value3 = this.overflowDamageData.GetValue(this.Level);
				num3 += num4 * value3;
			}
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = (float)((int)num3);
			return damage;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000233E4 File Offset: 0x000215E4
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.shadow_demon_shadow_poison)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				throw new ArgumentNullException("shadowPoison");
			}
			this.shadowPoison = (ShadowPoison)EntityManager9.AddAbility(ability);
			this.DamageData = new SpecialData(this.shadowPoison.BaseAbility, "stack_damage");
			this.maxStacksData = new SpecialData(this.shadowPoison.BaseAbility, "max_multiply_stacks");
			this.overflowDamageData = new SpecialData(this.shadowPoison.BaseAbility, "bonus_stack_damage");
		}

		// Token: 0x040004CA RID: 1226
		private SpecialData maxStacksData;

		// Token: 0x040004CB RID: 1227
		private SpecialData overflowDamageData;

		// Token: 0x040004CC RID: 1228
		private ShadowPoison shadowPoison;
	}
}
