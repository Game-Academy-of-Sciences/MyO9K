using System;
using System.Collections.Generic;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using O9K.Core.Prediction;

namespace O9K.Core.Entities.Abilities.Base
{
	// Token: 0x020003E4 RID: 996
	public abstract class Ability9 : Entity9, IDisposable
	{
		// Token: 0x060010B5 RID: 4277 RVA: 0x00029304 File Offset: 0x00027504
		protected Ability9(Ability baseAbility) : base(baseAbility)
		{
			this.BaseAbility = baseAbility;
			this.BaseItem = (this.BaseAbility as Item);
			this.IsItem = (this.BaseItem != null);
			this.AbilityBehavior = baseAbility.AbilityBehavior;
			this.DamageType = baseAbility.DamageType;
			this.Id = baseAbility.Id;
			this.TextureName = baseAbility.TextureName;
			this.MaximumLevel = baseAbility.MaximumLevel;
			this.DamageData = new SpecialData(baseAbility, new Func<uint, int>(baseAbility.GetDamage));
			this.DurationData = new SpecialData(baseAbility, new Func<uint, float>(baseAbility.GetDuration));
			if (this.IsItem)
			{
				this.IsDisplayingCharges = (this.BaseItem.IsStackable || this.BaseItem.IsRequiringCharges || this.BaseItem.IsDisplayingCharges);
			}
			else
			{
				this.IsUltimate = (baseAbility.AbilityType == AbilityType.Ultimate);
				this.IsActive = (baseAbility.IsActivated && !baseAbility.IsHidden);
				this.IsStolen = baseAbility.IsStolen;
			}
			SpellPierceImmunityType spellPierceImmunityType = baseAbility.SpellPierceImmunityType;
			if (spellPierceImmunityType > SpellPierceImmunityType.AlliesYes)
			{
				if (spellPierceImmunityType == SpellPierceImmunityType.EnemiesYes)
				{
					this.CanHitSpellImmuneEnemy = 1;
					this.CanHitSpellImmuneAlly = 1;
					return;
				}
			}
			else
			{
				this.CanHitSpellImmuneAlly = 1;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x0000EA8F File Offset: 0x0000CC8F
		public static float InputLag
		{
			get
			{
				return Game.Ping / 1000f;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x0000EA9C File Offset: 0x0000CC9C
		public override string TextureName { get; }

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		public int MaximumLevel { get; }

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0000EAAC File Offset: 0x0000CCAC
		public bool IsDisplayingCharges { get; }

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
		public virtual float Radius
		{
			get
			{
				SpecialData radiusData = this.RadiusData;
				if (radiusData == null)
				{
					return 0f;
				}
				return radiusData.GetValue(this.Level);
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x0000EAD1 File Offset: 0x0000CCD1
		public virtual bool IsTalent { get; }

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x00029454 File Offset: 0x00027654
		public override string DisplayName
		{
			get
			{
				if (this.displayName == null)
				{
					try
					{
						this.displayName = LocalizationHelper.LocalizeName(this.BaseAbility);
					}
					catch
					{
						this.displayName = base.Name;
					}
				}
				return this.displayName;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0000EAD9 File Offset: 0x0000CCD9
		// (set) Token: 0x060010BE RID: 4286 RVA: 0x0000EAE1 File Offset: 0x0000CCE1
		public bool IsCasting { get; internal set; }

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0000EAEA File Offset: 0x0000CCEA
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x0000EAF2 File Offset: 0x0000CCF2
		public bool IsChanneling { get; internal set; }

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0000EAFB File Offset: 0x0000CCFB
		public AbilityBehavior AbilityBehavior { get; }

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x0000EB03 File Offset: 0x0000CD03
		public virtual float ActivationDelay
		{
			get
			{
				SpecialData activationDelayData = this.ActivationDelayData;
				if (activationDelayData == null)
				{
					return 0f;
				}
				return activationDelayData.GetValue(this.Level);
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x0000EB20 File Offset: 0x0000CD20
		public Ability BaseAbility { get; }

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0000EB28 File Offset: 0x0000CD28
		public Item BaseItem { get; }

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x0000EB30 File Offset: 0x0000CD30
		public virtual bool CanHitSpellImmuneAlly { get; }

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x0000EB38 File Offset: 0x0000CD38
		public virtual bool CanHitSpellImmuneEnemy { get; }

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0000EB40 File Offset: 0x0000CD40
		public virtual DamageType DamageType { get; }

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x0000EB48 File Offset: 0x0000CD48
		public float Duration
		{
			get
			{
				return this.DurationData.GetValue(this.Level);
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x0000EB5B File Offset: 0x0000CD5B
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x0000EB63 File Offset: 0x0000CD63
		public AbilityId Id { get; protected set; }

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		public bool IsControllable
		{
			get
			{
				return base.Owner.IsControllable;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x0000EB79 File Offset: 0x0000CD79
		// (set) Token: 0x060010CD RID: 4301 RVA: 0x0000EB81 File Offset: 0x0000CD81
		public bool IsInvisibility { get; protected set; }

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x0000EB8A File Offset: 0x0000CD8A
		public bool IsItem { get; }

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0000EB92 File Offset: 0x0000CD92
		public virtual bool IsReady
		{
			get
			{
				return this.IsUsable && this.Level != 0u && this.RemainingCooldown <= 0f && base.Owner.Mana >= this.ManaCost;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x0000EBC9 File Offset: 0x0000CDC9
		public bool IsStolen { get; }

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x0000EBD1 File Offset: 0x0000CDD1
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x0000EBD9 File Offset: 0x0000CDD9
		public bool IsUltimate { get; protected set; }

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0000EBE2 File Offset: 0x0000CDE2
		public virtual bool IsUsable
		{
			get
			{
				if (this.IsItem)
				{
					return this.IsActive && !this.ItemEnableTimeSleeper.IsSleeping;
				}
				return this.IsActive;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0000EC0B File Offset: 0x0000CE0B
		public virtual uint Level
		{
			get
			{
				return this.BaseAbility.Level;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0000EC18 File Offset: 0x0000CE18
		public float ManaCost
		{
			get
			{
				return this.BaseAbility.ManaCost;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x000294A4 File Offset: 0x000276A4
		public virtual float TimeSinceCasted
		{
			get
			{
				float cooldownLength = this.BaseAbility.CooldownLength;
				if (cooldownLength > 0f)
				{
					return cooldownLength - this.BaseAbility.Cooldown;
				}
				return 9999999f;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x000294D8 File Offset: 0x000276D8
		public float RemainingCooldown
		{
			get
			{
				float num = this.BaseAbility.Cooldown;
				if (!base.Owner.IsVisible)
				{
					num -= Game.RawGameTime - base.Owner.LastVisibleTime;
				}
				return num;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0000EC27 File Offset: 0x0000CE27
		public float Cooldown
		{
			get
			{
				return this.BaseAbility.CooldownLength;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public virtual bool IntelligenceAmplify { get; } = 1;

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x0000E327 File Offset: 0x0000C527
		public virtual float Range
		{
			get
			{
				return this.CastRange;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x0000EC3C File Offset: 0x0000CE3C
		public virtual float CastRange
		{
			get
			{
				return this.BaseCastRange;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x0000EC44 File Offset: 0x0000CE44
		// (set) Token: 0x060010DD RID: 4317 RVA: 0x0000EC4C File Offset: 0x0000CE4C
		public IPredictionManager9 PredictionManager { get; private set; }

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0000EC55 File Offset: 0x0000CE55
		// (set) Token: 0x060010DF RID: 4319 RVA: 0x0000EC5D File Offset: 0x0000CE5D
		internal bool IsActive { get; set; }

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x0000EC66 File Offset: 0x0000CE66
		internal Sleeper ItemEnableTimeSleeper { get; } = new Sleeper();

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0000EC6E File Offset: 0x0000CE6E
		protected virtual float BaseCastRange { get; }

		// Token: 0x060010E2 RID: 4322 RVA: 0x0000EC76 File Offset: 0x0000CE76
		public static implicit operator Ability(Ability9 ability)
		{
			return ability.BaseAbility;
		}

		// Token: 0x060010E3 RID: 4323
		public abstract bool CanBeCasted(bool checkChanneling = true);

		// Token: 0x060010E4 RID: 4324 RVA: 0x0000EC7E File Offset: 0x0000CE7E
		public virtual void Dispose()
		{
			this.IsActive = false;
			base.Owner.Ability(this, false);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00029514 File Offset: 0x00027714
		public virtual int GetDamage(Unit9 unit)
		{
			float num = 0f;
			foreach (KeyValuePair<DamageType, float> keyValuePair in this.GetRawDamage(unit, null))
			{
				float damageAmplification = unit.GetDamageAmplification(base.Owner, keyValuePair.Key, this.IntelligenceAmplify);
				float damageBlock = unit.GetDamageBlock(keyValuePair.Key);
				num += (keyValuePair.Value - damageBlock) * damageAmplification;
			}
			return (int)num;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00022A3C File Offset: 0x00020C3C
		public virtual Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = this.DamageData.GetValue(this.Level);
			return damage;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0000EC94 File Offset: 0x0000CE94
		public bool PiercesMagicImmunity(Unit9 target)
		{
			return (target.IsEnemy(base.Owner) && this.CanHitSpellImmuneEnemy) || (target.IsAlly(base.Owner) && this.CanHitSpellImmuneAlly);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
		internal virtual void SetOwner(Unit9 owner)
		{
			base.Owner = owner;
			base.Owner.Ability(this, true);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0000ECDA File Offset: 0x0000CEDA
		internal void SetPrediction(IPredictionManager9 predictionManager)
		{
			this.PredictionManager = predictionManager;
		}

		// Token: 0x040008AF RID: 2223
		protected SpecialData ActivationDelayData;

		// Token: 0x040008B0 RID: 2224
		protected SpecialData DamageData;

		// Token: 0x040008B1 RID: 2225
		protected SpecialData DurationData;

		// Token: 0x040008B2 RID: 2226
		protected SpecialData RadiusData;

		// Token: 0x040008B3 RID: 2227
		private string displayName;
	}
}
