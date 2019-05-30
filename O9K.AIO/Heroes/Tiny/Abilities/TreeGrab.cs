using System;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Tiny.Abilities
{
	// Token: 0x02000074 RID: 116
	internal class TreeGrab : UsableAbility
	{
		// Token: 0x06000268 RID: 616 RVA: 0x00003867 File Offset: 0x00001A67
		public TreeGrab(ActiveAbility ability) : base(ability)
		{
			this.trees = ObjectManager.GetEntities<Tree>().ToArray<Tree>();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00003880 File Offset: 0x00001A80
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			return false;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00002E73 File Offset: 0x00001073
		public override bool ShouldCast(TargetManager targetManager)
		{
			return true;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0001027C File Offset: 0x0000E47C
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Vector3 ownerPosition = base.Owner.Position;
			Tree tree = (from x in this.trees
			where x.IsValid && x.IsAlive && x.Distance2D(ownerPosition) < this.Ability.CastRange
			orderby this.Owner.GetAngle(x.Position, false)
			select x).FirstOrDefault<Tree>();
			if (tree == null)
			{
				return false;
			}
			if (!base.Ability.UseAbility(tree, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x04000149 RID: 329
		private readonly Tree[] trees;
	}
}
