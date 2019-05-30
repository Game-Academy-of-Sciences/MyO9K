using System;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Mars.Abilities
{
	// Token: 0x02000106 RID: 262
	internal class SpearOfMars : NukeAbility
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x00004B08 File Offset: 0x00002D08
		public SpearOfMars(ActiveAbility ability) : base(ability)
		{
			this.trees = ObjectManager.GetEntities<Tree>().ToArray<Tree>();
			this.buildings = ObjectManager.GetEntities<Building>().ToArray<Building>();
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00004B3C File Offset: 0x00002D3C
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new SpearOfMarsMenu(base.Ability, simplifiedName);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001ADB0 File Offset: 0x00018FB0
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			if (!comboMenu.GetAbilitySettingsMenu<SpearOfMarsMenu>(this).StunOnly)
			{
				return base.CanHit(targetManager, comboMenu);
			}
			Unit9 target = targetManager.Target;
			if (target.IsMagicImmune && !base.Ability.CanHitSpellImmuneEnemy)
			{
				return false;
			}
			if (base.Owner.Distance(target) > base.Ability.CastRange)
			{
				return false;
			}
			if (target.HasModifier("modifier_mars_arena_of_blood_leash"))
			{
				return true;
			}
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			predictionInput.Range = base.Ability.CastRange;
			PredictionOutput9 simplePrediction = base.Ability.PredictionManager.GetSimplePrediction(predictionInput);
			Vector3 targetPosition = simplePrediction.TargetPosition;
			float num = base.Ability.Range - 200f;
			float radius = base.Ability.Radius;
			Polygon.Rectangle collisionRec = new Polygon.Rectangle(base.Owner.Position, targetPosition, radius);
			if (targetManager.AllEnemyHeroes.Any((Unit9 x) => x != target && collisionRec.IsInside(x.Position)))
			{
				return false;
			}
			foreach (Tree tree in from x in this.trees
			where x.IsValid && x.IsAlive
			select x)
			{
				if (new Polygon.Rectangle(targetPosition, Vector3Extensions.Extend2D(base.Owner.Position, targetPosition, num), radius - 50f).IsInside(tree.Position))
				{
					this.castPosition = simplePrediction.CastPosition;
					return true;
				}
			}
			foreach (Building building in from x in this.buildings
			where x.IsValid && x.IsAlive
			select x)
			{
				if (new Polygon.Rectangle(targetPosition, Vector3Extensions.Extend2D(base.Owner.Position, targetPosition, num), radius).IsInside(building.Position))
				{
					this.castPosition = simplePrediction.CastPosition;
					return true;
				}
			}
			for (int i = 0; i < 30; i++)
			{
				for (int j = 0; j < 30; j++)
				{
					Vector2 vector;
					vector..ctor(this.navMesh.CellSize * (float)(i - 15) + targetPosition.X, this.navMesh.CellSize * (float)(j - 15) + targetPosition.Y);
					if ((this.navMesh.GetCellFlags(vector) & NavMeshCellFlags.InteractionBlocker) != NavMeshCellFlags.None && new Polygon.Rectangle(targetPosition, Vector3Extensions.Extend2D(base.Owner.Position, targetPosition, num), radius - 100f).IsInside(vector))
					{
						this.castPosition = simplePrediction.CastPosition;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001B0C4 File Offset: 0x000192C4
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (this.castPosition.IsZero)
			{
				return base.UseAbility(targetManager, comboSleeper, aoe);
			}
			if (!base.Ability.UseAbility(this.castPosition, false, false))
			{
				return false;
			}
			this.castPosition = Vector3.Zero;
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			IDisable disable;
			if ((disable = (base.Ability as IDisable)) != null)
			{
				targetManager.Target.SetExpectedUnitState(disable.AppliesUnitState, base.Ability.GetHitTime(targetManager.Target));
			}
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x040002E4 RID: 740
		private Tree[] trees;

		// Token: 0x040002E5 RID: 741
		private Building[] buildings;

		// Token: 0x040002E6 RID: 742
		private readonly NavMeshPathfinding navMesh = new NavMeshPathfinding();

		// Token: 0x040002E7 RID: 743
		private Vector3 castPosition;
	}
}
