using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.Grimstroke.Abilities
{
	// Token: 0x02000169 RID: 361
	internal class GrimstrokeDagon : NukeAbility
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x000032F0 File Offset: 0x000014F0
		public GrimstrokeDagon(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00005C60 File Offset: 0x00003E60
		public override bool ShouldCast(TargetManager targetManager)
		{
			return base.ShouldCast(targetManager) && targetManager.Target.HasModifier("modifier_grimstroke_soul_chain");
		}
	}
}
