using System;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.EmberSpirit;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.EmberSpirit.Abilities
{
	// Token: 0x02000183 RID: 387
	internal class FireRemnant : NukeAbility
	{
		// Token: 0x060007E6 RID: 2022 RVA: 0x00005FAF File Offset: 0x000041AF
		public FireRemnant(ActiveAbility ability) : base(ability)
		{
			this.remnant = (FireRemnant)ability;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00005FC4 File Offset: 0x000041C4
		public int Charges
		{
			get
			{
				return this.remnant.Charges;
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00024154 File Offset: 0x00022354
		public float GetDamage(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			float speed = this.remnant.Speed;
			int num = EntityManager9.Units.Count((Unit9 x) => x.IsUnit && x.IsAlly(this.Owner) && x.Name == "npc_dota_ember_spirit_remnant" && x.Distance(target.GetPredictedPosition(this.Owner.Distance(x) / speed)) < this.Ability.Radius);
			return (float)(this.remnant.GetDamage(target) * (this.remnant.Charges + num));
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x000241C4 File Offset: 0x000223C4
		public float GetRequiredRemnants(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			float speed = this.remnant.Speed;
			int damage = this.remnant.GetDamage(target);
			if (damage <= 0)
			{
				return 0f;
			}
			int num = EntityManager9.Units.Count((Unit9 x) => x.IsUnit && x.IsAlly(this.Owner) && x.Name == "npc_dota_ember_spirit_remnant" && x.Distance(target.GetPredictedPosition(this.Owner.Distance(x) / speed)) < this.Ability.Radius);
			float num2 = target.Health - (float)(num * damage);
			int num3 = 0;
			while (num2 > 0f)
			{
				num2 -= (float)damage;
				num3++;
			}
			return (float)Math.Min(this.Charges, num3);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00024268 File Offset: 0x00022468
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, null);
			predictionInput.Delay += base.Owner.Distance(targetManager.Target) / base.Ability.Speed;
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			if (!base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			float hitTime = base.Ability.GetHitTime(targetManager.Target);
			base.Sleeper.Sleep(hitTime * 0.6f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x04000458 RID: 1112
		private readonly FireRemnant remnant;
	}
}
