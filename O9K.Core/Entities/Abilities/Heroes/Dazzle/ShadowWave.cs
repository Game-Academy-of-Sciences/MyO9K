using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Abilities.Heroes.Dazzle
{
	// Token: 0x020001AC RID: 428
	[AbilityId(AbilityId.dazzle_shadow_wave)]
	public class ShadowWave : RangedAreaOfEffectAbility, IHealthRestore, INuke, IActiveAbility
	{
		// Token: 0x060008B2 RID: 2226 RVA: 0x000224F4 File Offset: 0x000206F4
		public ShadowWave(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.RadiusData = new SpecialData(baseAbility, "bounce_radius");
			this.damageRadius = new SpecialData(baseAbility, "damage_radius");
			this.targetsData = new SpecialData(baseAbility, "max_targets");
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00007F43 File Offset: 0x00006143
		public bool InstantHealthRestore { get; } = 1;

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00007F4B File Offset: 0x0000614B
		public float DamageRadius
		{
			get
			{
				return this.damageRadius.GetValue(this.Level);
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00007F5E File Offset: 0x0000615E
		public string HealModifierName { get; } = string.Empty;

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00007F66 File Offset: 0x00006166
		public bool RestoresAlly { get; } = 1;

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00007F6E File Offset: 0x0000616E
		public bool RestoresOwner { get; } = 1;

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002256C File Offset: 0x0002076C
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			float value = this.DamageData.GetValue(this.Level);
			int num = EntityManager9.Units.Count((Unit9 x) => x.IsUnit && x.IsVisible && x.IsAlive && x.IsEnemy(unit) && x.Distance(unit) < this.DamageRadius);
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = (float)((int)(value * Math.Max(0f, Math.Min((float)num, this.targetsData.GetValue(this.Level)))));
			return damage;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00007F76 File Offset: 0x00006176
		public int HealthRestoreValue(Unit9 unit)
		{
			return (int)this.DamageData.GetValue(this.Level);
		}

		// Token: 0x04000448 RID: 1096
		private readonly SpecialData damageRadius;

		// Token: 0x04000449 RID: 1097
		private readonly SpecialData targetsData;
	}
}
