using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Phoenix
{
	// Token: 0x020002FC RID: 764
	[AbilityId(AbilityId.phoenix_supernova)]
	public class Supernova : AreaOfEffectAbility, IShield, IActiveAbility
	{
		// Token: 0x06000D3D RID: 3389 RVA: 0x00026878 File Offset: 0x00024A78
		public Supernova(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "aura_radius");
			this.DamageData = new SpecialData(baseAbility, "damage_per_sec");
			this.castRangeData = new SpecialData(baseAbility, "cast_range_tooltip_scepter");
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x0000BCAF File Offset: 0x00009EAF
		public UnitState AppliesUnitState { get; } = 256L;

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0000A7A9 File Offset: 0x000089A9
		public override bool UnitTargetCast
		{
			get
			{
				return base.Owner.HasAghanimsScepter;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0000BA09 File Offset: 0x00009C09
		public override bool NoTargetCast
		{
			get
			{
				return !this.UnitTargetCast;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0000BCB7 File Offset: 0x00009EB7
		public string ShieldModifierName { get; } = "modifier_phoenix_sun";

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0000BCBF File Offset: 0x00009EBF
		public bool ShieldsAlly { get; }

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x0000BCC7 File Offset: 0x00009EC7
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x0000BCCF File Offset: 0x00009ECF
		protected override float BaseCastRange
		{
			get
			{
				if (this.UnitTargetCast)
				{
					return this.castRangeData.GetValue(this.Level);
				}
				return base.BaseCastRange;
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x000268E0 File Offset: 0x00024AE0
		public override bool UseAbility(bool queue = false, bool bypass = false)
		{
			bool flag = this.UnitTargetCast ? base.BaseAbility.UseAbility(base.Owner.BaseUnit, queue, bypass) : base.BaseAbility.UseAbility(queue, bypass);
			if (flag)
			{
				base.ActionSleeper.Sleep(0.1f);
			}
			return flag;
		}

		// Token: 0x040006D5 RID: 1749
		private readonly SpecialData castRangeData;
	}
}
