using System;
using System.Collections.Generic;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Collision;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Base
{
	// Token: 0x020003E3 RID: 995
	public abstract class ActiveAbility : Ability9, IActiveAbility
	{
		// Token: 0x06001094 RID: 4244 RVA: 0x00028FC0 File Offset: 0x000271C0
		protected ActiveAbility(Ability baseAbility) : base(baseAbility)
		{
			this.UnitTargetCast = ((base.AbilityBehavior & AbilityBehavior.UnitTarget) > AbilityBehavior.None);
			this.PositionCast = ((base.AbilityBehavior & AbilityBehavior.Point) > AbilityBehavior.None);
			this.NoTargetCast = ((base.AbilityBehavior & AbilityBehavior.NoTarget) > AbilityBehavior.None);
			this.CanBeCastedWhileRooted = ((base.AbilityBehavior & AbilityBehavior.RootDisables) == AbilityBehavior.None);
			this.CanBeCastedWhileChanneling = ((base.AbilityBehavior & AbilityBehavior.IgnoreChannel) > AbilityBehavior.None);
			this.TargetsAlly = ((baseAbility.TargetTeamType & TargetTeamType.All) != TargetTeamType.Enemy);
			this.TargetsEnemy = ((baseAbility.TargetTeamType & TargetTeamType.All) != TargetTeamType.Allied);
			this.CastPoint = ((baseAbility.OverrideCastPoint < 0f) ? base.BaseAbility.GetCastPoint(0u) : 0f);
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x0000E8E5 File Offset: 0x0000CAE5
		public virtual bool BreaksLinkens
		{
			get
			{
				return this.UnitTargetCast;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x0000E8ED File Offset: 0x0000CAED
		public virtual float CastPoint { get; }

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x0000E8F5 File Offset: 0x0000CAF5
		public virtual CollisionTypes CollisionTypes { get; }

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x0000E8FD File Offset: 0x0000CAFD
		public virtual bool HasAreaOfEffect
		{
			get
			{
				return this.CollisionTypes == CollisionTypes.None && this.Radius > 0f;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x0000E916 File Offset: 0x0000CB16
		public virtual bool NoTargetCast { get; }

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x0000E91E File Offset: 0x0000CB1E
		public virtual bool PositionCast { get; }

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0000E926 File Offset: 0x0000CB26
		public virtual SkillShotType SkillShotType { get; }

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0000E92E File Offset: 0x0000CB2E
		public virtual float Speed
		{
			get
			{
				SpecialData speedData = this.SpeedData;
				if (speedData == null)
				{
					return 0f;
				}
				return speedData.GetValue(this.Level);
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x0000E94B File Offset: 0x0000CB4B
		public virtual bool TargetsAlly { get; }

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x0000E953 File Offset: 0x0000CB53
		public virtual bool TargetsEnemy { get; }

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x0000E95B File Offset: 0x0000CB5B
		public virtual bool UnitTargetCast { get; }

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x0000E963 File Offset: 0x0000CB63
		public virtual bool CanBeCastedWhileChanneling { get; }

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x0000E96B File Offset: 0x0000CB6B
		protected Sleeper ActionSleeper { get; } = new Sleeper();

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x0000E973 File Offset: 0x0000CB73
		protected virtual bool CanBeCastedWhileRooted { get; }

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x0000E97B File Offset: 0x0000CB7B
		protected virtual bool CanBeCastedWhileStunned { get; }

		// Token: 0x060010A4 RID: 4260 RVA: 0x00029098 File Offset: 0x00027298
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return !this.ActionSleeper.IsSleeping && this.IsReady && base.Owner.IsAlive && !base.Owner.IsCharging && (this.CanBeCastedWhileChanneling || !checkChanneling || !base.Owner.IsChanneling) && (this.CanBeCastedWhileStunned || !base.Owner.IsStunned) && (this.CanBeCastedWhileRooted || (!base.Owner.IsRooted && !base.Owner.IsLeashed)) && (!base.IsItem || !base.Owner.IsMuted) && (base.IsItem || !base.Owner.IsSilenced);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0000E8E2 File Offset: 0x0000CAE2
		public virtual bool CanHit(Unit9 target)
		{
			return true;
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0000E8E2 File Offset: 0x0000CAE2
		public virtual bool CanHit(Unit9 mainTarget, List<Unit9> aoeTargets, int minCount)
		{
			return true;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0000E983 File Offset: 0x0000CB83
		public virtual float GetCastDelay(Unit9 unit)
		{
			if (base.Owner.Equals(unit))
			{
				return this.GetCastDelay();
			}
			return this.GetCastDelay(unit.Position);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0000E9A6 File Offset: 0x0000CBA6
		public virtual float GetCastDelay(Vector3 position)
		{
			if (this.NoTargetCast)
			{
				return this.GetCastDelay();
			}
			return this.GetCastDelay() + base.Owner.GetTurnTime(position);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0000E9CA File Offset: 0x0000CBCA
		public virtual float GetCastDelay()
		{
			return this.CastPoint + Ability9.InputLag;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0000E9D8 File Offset: 0x0000CBD8
		public virtual float GetHitTime(Unit9 unit)
		{
			if (base.Owner.Equals(unit))
			{
				return this.GetCastDelay() + this.ActivationDelay;
			}
			return this.GetHitTime(unit.Position);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00029160 File Offset: 0x00027360
		public virtual float GetHitTime(Vector3 position)
		{
			float num = this.GetCastDelay(position) + this.ActivationDelay;
			if (this.Speed > 0f)
			{
				return num + base.Owner.Distance(position) / this.Speed;
			}
			return num;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x000291A0 File Offset: 0x000273A0
		public virtual PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
		{
			PredictionInput9 predictionInput = new PredictionInput9
			{
				Caster = base.Owner,
				Target = target,
				CollisionTypes = this.CollisionTypes,
				Delay = this.CastPoint + this.ActivationDelay + Ability9.InputLag,
				Speed = this.Speed,
				Range = this.Range,
				Radius = this.Radius,
				CastRange = this.CastRange,
				SkillShotType = this.SkillShotType,
				RequiresToTurn = !this.NoTargetCast
			};
			if (aoeTargets != null)
			{
				predictionInput.AreaOfEffect = this.HasAreaOfEffect;
				predictionInput.AreaOfEffectTargets = aoeTargets;
			}
			return predictionInput;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0000EA02 File Offset: 0x0000CC02
		public virtual PredictionOutput9 GetPredictionOutput(PredictionInput9 input)
		{
			return base.PredictionManager.GetPrediction(input);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0000EA10 File Offset: 0x0000CC10
		public virtual bool UseAbility(Unit9 mainTarget, List<Unit9> aoeTargets, HitChance minimumChance, int minAOETargets = 0, bool queue = false, bool bypass = false)
		{
			return this.UseAbility(queue, bypass);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		public virtual bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
		{
			return this.UseAbility(queue, bypass);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0000EA27 File Offset: 0x0000CC27
		public virtual bool UseAbility(bool queue = false, bool bypass = false)
		{
			bool flag = base.BaseAbility.UseAbility(queue, bypass);
			if (flag)
			{
				this.ActionSleeper.Sleep(0.1f);
			}
			return flag;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00029250 File Offset: 0x00027450
		public virtual bool UseAbility(Unit9 target, bool queue = false, bool bypass = false)
		{
			bool flag;
			if (this.UnitTargetCast)
			{
				flag = base.BaseAbility.UseAbility(target.BaseUnit, queue, bypass);
			}
			else if (this.PositionCast)
			{
				flag = base.BaseAbility.UseAbility(target.Position, queue, bypass);
			}
			else
			{
				flag = base.BaseAbility.UseAbility(queue, bypass);
			}
			if (flag)
			{
				this.ActionSleeper.Sleep(0.1f);
			}
			return flag;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0000EA49 File Offset: 0x0000CC49
		public virtual bool UseAbility(Tree target, bool queue = false, bool bypass = false)
		{
			bool flag = base.BaseAbility.UseAbility(target, queue, bypass);
			if (flag)
			{
				this.ActionSleeper.Sleep(0.1f);
			}
			return flag;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x000292BC File Offset: 0x000274BC
		public virtual bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
		{
			bool flag;
			if (this.PositionCast)
			{
				flag = base.BaseAbility.UseAbility(position, queue, bypass);
			}
			else
			{
				flag = base.BaseAbility.UseAbility(queue, bypass);
			}
			if (flag)
			{
				this.ActionSleeper.Sleep(0.1f);
			}
			return flag;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		public virtual bool UseAbility(Rune target, bool queue = false, bool bypass = false)
		{
			bool flag = base.BaseAbility.UseAbility(target, queue, bypass);
			if (flag)
			{
				this.ActionSleeper.Sleep(0.1f);
			}
			return flag;
		}

		// Token: 0x040008A2 RID: 2210
		protected SpecialData SpeedData;
	}
}
