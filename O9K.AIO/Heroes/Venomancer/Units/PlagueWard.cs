using System;
using System.Linq;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.Venomancer.Units
{
	// Token: 0x02000058 RID: 88
	[UnitName("npc_dota_venomancer_plague_ward_1")]
	[UnitName("npc_dota_venomancer_plague_ward_2")]
	[UnitName("npc_dota_venomancer_plague_ward_3")]
	[UnitName("npc_dota_venomancer_plague_ward_4")]
	internal class PlagueWard : ControllableUnit
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00003511 File Offset: 0x00001711
		public PlagueWard(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000E450 File Offset: 0x0000C650
		public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
		{
			if (target == null || target.HasModifier("modifier_venomancer_poison_sting_ward"))
			{
				Unit9 unit = (from x in EntityManager9.EnemyHeroes
				where base.Owner.CanAttack(x, 0f) && !x.HasModifier("modifier_venomancer_poison_sting_ward")
				orderby x.Distance(base.Owner)
				select x).FirstOrDefault<Unit9>();
				if (unit != null)
				{
					target = unit;
				}
			}
			return base.Orbwalk(target, attack, move, comboMenu);
		}
	}
}
