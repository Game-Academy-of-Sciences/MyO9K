using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Pangolier;
using O9K.Core.Entities.Units;
using SharpDX;

namespace O9K.AIO.Heroes.Pangolier.Abilities
{
	// Token: 0x020000E5 RID: 229
	internal class RollingThunder : NukeAbility
	{
		// Token: 0x060004A7 RID: 1191 RVA: 0x000046FE File Offset: 0x000028FE
		public RollingThunder(ActiveAbility ability) : base(ability)
		{
			this.rollingThunder = (RollingThunder)ability;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000189C8 File Offset: 0x00016BC8
		public Vector3 GetPosition(Unit9 target)
		{
			List<Vector3> list = new List<Vector3>();
			Vector3 position = base.Owner.Position;
			for (int i = 0; i < 40; i++)
			{
				for (int j = 0; j < 40; j++)
				{
					Vector2 vector;
					vector..ctor(this.navMesh.CellSize * (float)(i - 20) + position.X, this.navMesh.CellSize * (float)(j - 20) + position.Y);
					if ((this.navMesh.GetCellFlags(vector) & NavMeshCellFlags.InteractionBlocker) != NavMeshCellFlags.None)
					{
						list.Add(new Vector3(vector.X, vector.Y, 0f));
					}
				}
			}
			return (from x in list.Where(new Func<Vector3, bool>(this.CheckWall))
			orderby base.Owner.Distance(x)
			select x).FirstOrDefault<Vector3>();
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000471E File Offset: 0x0000291E
		public override bool ShouldCast(TargetManager targetManager)
		{
			return base.ShouldCast(targetManager) && base.Owner.GetAngle(targetManager.Target.Position, false) <= 0.75f;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00018A90 File Offset: 0x00016C90
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			UsableAbility usableAbility = usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.item_blink);
			if (usableAbility == null)
			{
				return base.Owner.Distance(targetManager.Target) < 600f;
			}
			return base.Owner.Distance(targetManager.Target) < usableAbility.Ability.CastRange + 200f;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00018B04 File Offset: 0x00016D04
		private bool CheckWall(Vector3 wall)
		{
			float num = base.Owner.Distance(wall);
			return base.Owner.GetAngle(wall, false) / this.rollingThunder.TurnRate + 0.5f <= num / this.rollingThunder.Speed && num <= 600f;
		}

		// Token: 0x0400028E RID: 654
		private readonly NavMeshPathfinding navMesh = new NavMeshPathfinding();

		// Token: 0x0400028F RID: 655
		private readonly RollingThunder rollingThunder;
	}
}
