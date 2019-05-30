using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Heroes.Phoenix
{
	// Token: 0x020002F3 RID: 755
	[AbilityId(AbilityId.phoenix_launch_fire_spirit)]
	public class LaunchFireSpirit : CircleAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000D1B RID: 3355 RVA: 0x0000BB32 File Offset: 0x00009D32
		public LaunchFireSpirit(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "spirit_speed");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0000BB68 File Offset: 0x00009D68
		public string DebuffModifierName { get; } = "modifier_phoenix_fire_spirit_burn";

		// Token: 0x06000D1D RID: 3357 RVA: 0x0000BB70 File Offset: 0x00009D70
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			if (this.IsUsable)
			{
				return base.CanBeCasted(checkChanneling);
			}
			return this.fireSpirits.CanBeCasted(checkChanneling);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0000BB8E File Offset: 0x00009D8E
		public override bool UseAbility(bool queue = false, bool bypass = false)
		{
			return this.fireSpirits.UseAbility(queue, bypass);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0000BB9D File Offset: 0x00009D9D
		public override bool UseAbility(Unit9 mainTarget, List<Unit9> aoeTargets, HitChance minimumChance, int minAOETargets = 0, bool queue = false, bool bypass = false)
		{
			if (!this.IsUsable)
			{
				return this.fireSpirits.UseAbility(queue, bypass);
			}
			return base.UseAbility(mainTarget, aoeTargets, minimumChance, minAOETargets, queue, bypass);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0000BBC6 File Offset: 0x00009DC6
		public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
		{
			if (!this.IsUsable)
			{
				this.fireSpirits.UseAbility(queue, bypass);
			}
			return base.UseAbility(target, minimumChance, queue, bypass);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00026400 File Offset: 0x00024600
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.phoenix_fire_spirits)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				throw new ArgumentNullException("fireSpirits");
			}
			this.fireSpirits = (FireSpirits)EntityManager9.AddAbility(ability);
		}

		// Token: 0x040006C8 RID: 1736
		private FireSpirits fireSpirits;
	}
}
