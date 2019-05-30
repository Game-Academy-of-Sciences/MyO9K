using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using O9K.Core.Managers.Entity;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Heroes.Riki
{
	// Token: 0x020002E8 RID: 744
	[AbilityId(AbilityId.riki_tricks_of_the_trade)]
	public class TricksOfTheTrade : CircleAbility, IShield, IChanneled, IActiveAbility
	{
		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002600C File Offset: 0x0002420C
		public TricksOfTheTrade(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "range");
			this.attackRateData = new SpecialData(baseAbility, "attack_rate");
			this.channelTimeData = new SpecialData(baseAbility, new Func<uint, float>(baseAbility.GetChannelTime));
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0000B9F3 File Offset: 0x00009BF3
		public override float CastRange
		{
			get
			{
				if (this.NoTargetCast)
				{
					return 0f;
				}
				return base.CastRange;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x0000BA09 File Offset: 0x00009C09
		public override bool NoTargetCast
		{
			get
			{
				return !this.UnitTargetCast;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0000A7A9 File Offset: 0x000089A9
		public override bool UnitTargetCast
		{
			get
			{
				return base.Owner.HasAghanimsScepter;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0000BA14 File Offset: 0x00009C14
		public float AttackRate
		{
			get
			{
				return this.attackRateData.GetValue(this.Level);
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0000BA27 File Offset: 0x00009C27
		public UnitState AppliesUnitState { get; } = 256L;

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0000BA2F File Offset: 0x00009C2F
		public float ChannelTime
		{
			get
			{
				return this.channelTimeData.GetValue(this.Level);
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0000BA42 File Offset: 0x00009C42
		public bool IsActivatesOnChannelStart { get; } = 1;

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0000BA4A File Offset: 0x00009C4A
		public string ShieldModifierName { get; } = "modifier_riki_tricks_of_the_trade_phase";

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0000BA52 File Offset: 0x00009C52
		public bool ShieldsAlly { get; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x0000BA5A File Offset: 0x00009C5A
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x06000CFC RID: 3324 RVA: 0x00026080 File Offset: 0x00024280
		public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
		{
			PredictionInput9 predictionInput = base.GetPredictionInput(target, aoeTargets);
			if (predictionInput.CastRange > 0f)
			{
				predictionInput.CastRange -= predictionInput.Radius;
				predictionInput.Range -= predictionInput.Radius;
			}
			return predictionInput;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000260CC File Offset: 0x000242CC
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage rawAttackDamage = base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
			CloakAndDagger cloakAndDagger = this.cloakAndDagger;
			return rawAttackDamage + ((cloakAndDagger != null) ? cloakAndDagger.GetRawDamage(unit, null) : null);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00026114 File Offset: 0x00024314
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.riki_permanent_invisibility)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				return;
			}
			this.cloakAndDagger = (CloakAndDagger)EntityManager9.AddAbility(ability);
		}

		// Token: 0x040006B2 RID: 1714
		private readonly SpecialData attackRateData;

		// Token: 0x040006B3 RID: 1715
		private readonly SpecialData channelTimeData;

		// Token: 0x040006B4 RID: 1716
		private CloakAndDagger cloakAndDagger;
	}
}
