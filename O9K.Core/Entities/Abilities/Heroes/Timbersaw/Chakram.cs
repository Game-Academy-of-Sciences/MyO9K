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

namespace O9K.Core.Entities.Abilities.Heroes.Timbersaw
{
	// Token: 0x020002A8 RID: 680
	[AbilityId(AbilityId.shredder_chakram)]
	[AbilityId(AbilityId.shredder_chakram_2)]
	public class Chakram : CircleAbility, INuke, IActiveAbility
	{
		// Token: 0x06000C08 RID: 3080 RVA: 0x0000ACEE File Offset: 0x00008EEE
		public Chakram(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.DamageData = new SpecialData(baseAbility, "pass_damage");
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0000AD2A File Offset: 0x00008F2A
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x0000AD32 File Offset: 0x00008F32
		public ReturnChakram ReturnChakram { get; private set; }

		// Token: 0x06000C0B RID: 3083 RVA: 0x0000AD3B File Offset: 0x00008F3B
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			return base.GetRawDamage(unit, remainingHealth) * 2f;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0000AD4F File Offset: 0x00008F4F
		public override bool UseAbility(bool queue = false, bool bypass = false)
		{
			return this.ReturnChakram.UseAbility(queue, bypass);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00025454 File Offset: 0x00023654
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == ((this.Id == AbilityId.shredder_chakram) ? AbilityId.shredder_return_chakram : AbilityId.shredder_return_chakram_2))
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				throw new ArgumentNullException("ReturnChakram");
			}
			this.ReturnChakram = (ReturnChakram)EntityManager9.AddAbility(ability);
		}
	}
}
