using System;
using System.Linq;
using Ensage;
using Ensage.SDK.Extensions;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Timbersaw;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Timbersaw.Abilities
{
	// Token: 0x0200007C RID: 124
	internal class TimberChainBlink : BlinkAbility
	{
		// Token: 0x06000288 RID: 648 RVA: 0x00003975 File Offset: 0x00001B75
		public TimberChainBlink(ActiveAbility ability) : base(ability)
		{
			this.timberChain = (TimberChain)ability;
			this.trees = ObjectManager.GetEntities<Tree>().ToArray<Tree>();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000107B8 File Offset: 0x0000E9B8
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
		{
			Vector3 closestTree = this.GetClosestTree(toPosition);
			if (closestTree.IsZero)
			{
				return false;
			}
			if (!base.Ability.UseAbility(closestTree, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(closestTree);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00010820 File Offset: 0x0000EA20
		private Vector3 GetClosestTree(Vector3 preferedPosition)
		{
			Vector3 ownerPosition = base.Owner.Position;
			Vector3 vector = Vector3Extensions.Extend2D(ownerPosition, preferedPosition, base.Ability.CastRange);
			Tree[] array = (from x in this.trees
			where x.IsValid && x.IsAlive && x.Distance2D(ownerPosition) < this.Ability.CastRange
			orderby x.Distance2D(preferedPosition)
			select x).ToArray<Tree>();
			Tree[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				TimberChainBlink.<>c__DisplayClass4_1 <>c__DisplayClass4_2 = new TimberChainBlink.<>c__DisplayClass4_1();
				<>c__DisplayClass4_2.tree = array2[i];
				Vector3 position = <>c__DisplayClass4_2.tree.Position;
				if (ownerPosition.Distance2D(position, false) >= 500f && (<>c__DisplayClass4_2.tree.Position - ownerPosition).AngleBetween(vector - <>c__DisplayClass4_2.tree.Position) <= 60f)
				{
					Polygon.Rectangle polygon = new Polygon.Rectangle(ownerPosition, <>c__DisplayClass4_2.tree.Position, this.timberChain.ChainRadius);
					if (!array.Any((Tree x) => x != <>c__DisplayClass4_2.tree && polygon.IsInside(x.Position)))
					{
						return <>c__DisplayClass4_2.tree.Position;
					}
				}
			}
			return Vector3.Zero;
		}

		// Token: 0x0400015B RID: 347
		private readonly TimberChain timberChain;

		// Token: 0x0400015C RID: 348
		private readonly Tree[] trees;
	}
}
