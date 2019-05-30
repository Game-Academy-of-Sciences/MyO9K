using System;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.MonkeyKing.Abilities
{
	// Token: 0x020000FC RID: 252
	internal class TreeDance : BlinkAbility
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x00004988 File Offset: 0x00002B88
		public TreeDance(ActiveAbility ability) : base(ability)
		{
			this.trees = ObjectManager.GetEntities<Tree>().ToArray<Tree>();
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000049A1 File Offset: 0x00002BA1
		public override bool ShouldCast(TargetManager targetManager)
		{
			return targetManager.Target == null || base.Owner.Distance(targetManager.Target) >= 500f;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001A380 File Offset: 0x00018580
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			Vector3 targetPosition = target.Position;
			Vector3 ownerPosition = base.Owner.Position;
			Tree tree = (from x in this.trees
			where x.IsValid && x.IsAlive && x.Distance2D(ownerPosition) < this.Ability.CastRange
			orderby x.Distance2D(targetPosition)
			select x).FirstOrDefault((Tree x) => x.Distance2D(targetPosition) > 300f && x.Distance2D(targetPosition) < 800f);
			if (tree == null)
			{
				return false;
			}
			if (!base.Ability.UseAbility(tree, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(tree.Position);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001A450 File Offset: 0x00018650
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, float minDistance, float blinkDistance)
		{
			if (base.Owner.Distance(targetManager.Target) < minDistance)
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			Vector3 targetPosition = target.Position;
			Vector3 ownerPosition = base.Owner.Position;
			Tree tree = (from x in this.trees
			where x.IsValid && x.IsAlive && x.Distance2D(ownerPosition) < this.Ability.CastRange
			orderby x.Distance2D(targetPosition)
			select x).FirstOrDefault((Tree x) => x.Distance2D(targetPosition) < 400f);
			if (tree == null)
			{
				return false;
			}
			if (!base.Ability.UseAbility(tree, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(tree.Position);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001A534 File Offset: 0x00018734
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
		{
			if (base.Owner.Distance(toPosition) < 300f)
			{
				return false;
			}
			Vector3 ownerPosition = base.Owner.Position;
			Tree tree = (from x in this.trees
			where x.IsValid && x.IsAlive && x.Distance2D(ownerPosition) < this.Ability.CastRange
			orderby x.Distance2D(toPosition)
			select x).FirstOrDefault((Tree x) => x.Distance2D(toPosition) < 600f);
			if (tree == null)
			{
				return false;
			}
			if (!base.Ability.UseAbility(tree, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(tree.Position);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x040002C7 RID: 711
		private readonly Tree[] trees;
	}
}
