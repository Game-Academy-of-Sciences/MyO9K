using System;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.TemplarAssassin.Abilities
{
	// Token: 0x02000090 RID: 144
	internal class PsionicTrap : DebuffAbility
	{
		// Token: 0x060002DA RID: 730 RVA: 0x000034DD File Offset: 0x000016DD
		public PsionicTrap(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00011B88 File Offset: 0x0000FD88
		public override bool ShouldCast(TargetManager targetManager)
		{
			return base.ShouldCast(targetManager) && !EntityManager9.Units.Any((Unit9 x) => x.IsAlly(this.Owner) && (x.IsAlive || x.DeathTime + 0.5f > Game.RawGameTime) && x.Name == "npc_dota_templar_assassin_psionic_trap" && x.Distance(targetManager.Target) < this.Ability.Radius);
		}
	}
}
