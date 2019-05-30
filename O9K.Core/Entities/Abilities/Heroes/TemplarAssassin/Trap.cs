using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.TemplarAssassin
{
	// Token: 0x020002B4 RID: 692
	[AbilityId(AbilityId.templar_assassin_self_trap)]
	public class Trap : AreaOfEffectAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000C46 RID: 3142 RVA: 0x0000B064 File Offset: 0x00009264
		public Trap(Ability baseAbility) : base(baseAbility)
		{
			this.chargeTime = new SpecialData(baseAbility, "trap_max_charge_duration");
			this.RadiusData = new SpecialData(baseAbility, "trap_radius");
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0000B09A File Offset: 0x0000929A
		public override float CastPoint { get; }

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0000B0A2 File Offset: 0x000092A2
		public bool IsFullyCharged
		{
			get
			{
				return Game.RawGameTime > base.Owner.CreateTime + this.chargeTime.GetValue(this.Level);
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0000B0C8 File Offset: 0x000092C8
		public string DebuffModifierName { get; } = "modifier_templar_assassin_trap_slow";

		// Token: 0x0400064F RID: 1615
		private readonly SpecialData chargeTime;
	}
}
