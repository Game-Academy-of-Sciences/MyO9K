using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Timbersaw;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Timbersaw.Abilities
{
	// Token: 0x02000081 RID: 129
	internal class TimberChain : NukeAbility
	{
		// Token: 0x06000297 RID: 663 RVA: 0x00003A65 File Offset: 0x00001C65
		public TimberChain(ActiveAbility ability) : base(ability)
		{
			this.timberChain = (TimberChain)ability;
			this.trees = ObjectManager.GetEntities<Tree>().ToArray<Tree>();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00010B14 File Offset: 0x0000ED14
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			predictionInput.Delay += base.Owner.Distance(target) / base.Ability.Speed;
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			Vector3 ownerPosition = base.Owner.Position;
			this.castPosition = predictionOutput.CastPosition;
			Polygon.Rectangle polygon = new Polygon.Rectangle(ownerPosition, this.castPosition, this.timberChain.ChainRadius);
			Tree[] array = (from x in this.trees
			where x.IsValid && x.IsAlive && x.Distance2D(ownerPosition) < this.Ability.CastRange
			select x).ToArray<Tree>();
			foreach (Tree tree3 in array)
			{
				if (polygon.IsInside(tree3.Position))
				{
					return false;
				}
			}
			Vector3 end = Vector3Extensions.Extend2D(ownerPosition, this.castPosition, base.Ability.CastRange);
			polygon = new Polygon.Rectangle(this.castPosition, end, this.timberChain.Radius);
			foreach (Tree tree2 in array)
			{
				if (polygon.IsInside(tree2.Position))
				{
					this.castPosition = tree2.Position;
					return true;
				}
			}
			if (base.Ability.Level < 4u || ownerPosition.Distance2D(this.castPosition, false) < 400f)
			{
				return false;
			}
			IEnumerable<Tree> source = array;
			Func<Tree, float> <>9__1;
			Func<Tree, float> keySelector;
			if ((keySelector = <>9__1) == null)
			{
				keySelector = (<>9__1 = ((Tree x) => x.Distance2D(this.castPosition)));
			}
			using (IEnumerator<Tree> enumerator = source.OrderBy(keySelector).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Tree tree = enumerator.Current;
					Vector3 position = tree.Position;
					if ((position.Distance2D(this.castPosition, false) <= 500f || target.GetAngle(position, false) <= 0.75f) && ownerPosition.Distance2D(this.castPosition, false) >= position.Distance2D(this.castPosition, false))
					{
						polygon = new Polygon.Rectangle(ownerPosition, position, this.timberChain.ChainRadius);
						if (!array.Any((Tree x) => !x.Equals(tree) && polygon.IsInside(x.Position)))
						{
							this.castPosition = position;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00010DD4 File Offset: 0x0000EFD4
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			return usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.item_blink) == null || base.Owner.Distance(targetManager.Target) < 800f;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00010E28 File Offset: 0x0000F028
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(this.castPosition, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(this.castPosition);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(base.Ability.GetHitTime(this.castPosition));
			return true;
		}

		// Token: 0x04000165 RID: 357
		private readonly TimberChain timberChain;

		// Token: 0x04000166 RID: 358
		private readonly Tree[] trees;

		// Token: 0x04000167 RID: 359
		private Vector3 castPosition;
	}
}
