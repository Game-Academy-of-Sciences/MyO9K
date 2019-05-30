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

namespace O9K.Core.Entities.Abilities.Heroes.Undying
{
	// Token: 0x020001A4 RID: 420
	[AbilityId(AbilityId.undying_soul_rip)]
	public class SoulRip : RangedAbility, IHealthRestore, INuke, IActiveAbility
	{
		// Token: 0x06000886 RID: 2182 RVA: 0x00022288 File Offset: 0x00020488
		public SoulRip(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage_per_unit");
			this.unitSearchRadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00007DBD File Offset: 0x00005FBD
		public bool InstantHealthRestore { get; } = 1;

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00007DC5 File Offset: 0x00005FC5
		public string HealModifierName { get; } = string.Empty;

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00007DCD File Offset: 0x00005FCD
		public bool RestoresAlly { get; } = 1;

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00007DD5 File Offset: 0x00005FD5
		public bool RestoresOwner { get; } = 1;

		// Token: 0x0600088B RID: 2187 RVA: 0x000222E0 File Offset: 0x000204E0
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			int num = EntityManager9.Units.Count((Unit9 x) => x.IsUnit && !x.Equals(this.Owner) && !x.Equals(unit) && x.IsVisible && x.IsAlive && x.Distance(unit) < this.unitSearchRadiusData.GetValue(this.Level));
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = this.DamageData.GetValue(this.Level) * (float)num;
			return damage;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00022340 File Offset: 0x00020540
		public int HealthRestoreValue(Unit9 unit)
		{
			return (int)this.GetRawDamage(unit, null)[this.DamageType];
		}

		// Token: 0x04000428 RID: 1064
		private readonly SpecialData unitSearchRadiusData;
	}
}
