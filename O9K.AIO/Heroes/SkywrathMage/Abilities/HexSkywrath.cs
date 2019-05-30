using System;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.SkywrathMage.Abilities
{
	// Token: 0x020000AA RID: 170
	internal class HexSkywrath : DisableAbility
	{
		// Token: 0x06000371 RID: 881 RVA: 0x00003482 File Offset: 0x00001682
		public HexSkywrath(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00013BF0 File Offset: 0x00011DF0
		public override bool ShouldCast(TargetManager targetManager)
		{
			if (!base.ShouldCast(targetManager))
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			if (target.IsRooted)
			{
				return target.Abilities.Any((Ability9 x) => (x is IShield || x is IDisable || x is IBlink) && x.CanBeCasted(false));
			}
			return true;
		}
	}
}
