using System;
using System.Collections.Generic;
using Ensage;
using O9K.Core.Entities.Units;
using O9K.Core.Prediction.Collision;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Base.Components.Base
{
	// Token: 0x02000400 RID: 1024
	public interface IActiveAbility
	{
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x0600113A RID: 4410
		Ability BaseAbility { get; }

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600113B RID: 4411
		uint Handle { get; }

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x0600113C RID: 4412
		AbilityId Id { get; }

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x0600113D RID: 4413
		float CastPoint { get; }

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x0600113E RID: 4414
		float CastRange { get; }

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x0600113F RID: 4415
		float Radius { get; }

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001140 RID: 4416
		float Speed { get; }

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001141 RID: 4417
		bool IsItem { get; }

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001142 RID: 4418
		CollisionTypes CollisionTypes { get; }

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001143 RID: 4419
		string DisplayName { get; }

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001144 RID: 4420
		string Name { get; }

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001145 RID: 4421
		string DefaultName { get; }

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001146 RID: 4422
		bool NoTargetCast { get; }

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001147 RID: 4423
		Unit9 Owner { get; }

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001148 RID: 4424
		bool PositionCast { get; }

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001149 RID: 4425
		bool IsValid { get; }

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600114A RID: 4426
		bool BreaksLinkens { get; }

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600114B RID: 4427
		bool UnitTargetCast { get; }

		// Token: 0x0600114C RID: 4428
		bool CanBeCasted(bool checkChanneling = true);

		// Token: 0x0600114D RID: 4429
		bool CanHit(Unit9 mainTarget, List<Unit9> aoeTargets, int minCount);

		// Token: 0x0600114E RID: 4430
		bool CanHit(Unit9 target);

		// Token: 0x0600114F RID: 4431
		float GetCastDelay(Vector3 position);

		// Token: 0x06001150 RID: 4432
		float GetCastDelay(Unit9 unit);

		// Token: 0x06001151 RID: 4433
		float GetCastDelay();

		// Token: 0x06001152 RID: 4434
		float GetHitTime(Vector3 position);

		// Token: 0x06001153 RID: 4435
		float GetHitTime(Unit9 unit);

		// Token: 0x06001154 RID: 4436
		PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null);

		// Token: 0x06001155 RID: 4437
		PredictionOutput9 GetPredictionOutput(PredictionInput9 input);

		// Token: 0x06001156 RID: 4438
		bool PiercesMagicImmunity(Unit9 target);

		// Token: 0x06001157 RID: 4439
		bool UseAbility(Unit9 mainTarget, List<Unit9> aoeTargets, HitChance minimumChance, int minAOETargets = 0, bool queue = false, bool bypass = false);

		// Token: 0x06001158 RID: 4440
		bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false);

		// Token: 0x06001159 RID: 4441
		bool UseAbility(bool queue = false, bool bypass = false);

		// Token: 0x0600115A RID: 4442
		bool UseAbility(Unit9 target, bool queue = false, bool bypass = false);

		// Token: 0x0600115B RID: 4443
		bool UseAbility(Tree target, bool queue = false, bool bypass = false);

		// Token: 0x0600115C RID: 4444
		bool UseAbility(Vector3 position, bool queue = false, bool bypass = false);

		// Token: 0x0600115D RID: 4445
		bool UseAbility(Rune target, bool queue = false, bool bypass = false);
	}
}
