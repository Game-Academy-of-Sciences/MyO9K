using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.SpiritBreaker.Modes;
using O9K.AIO.Modes.KeyPress;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.SpiritBreaker
{
	// Token: 0x02000044 RID: 68
	[HeroId(HeroId.npc_dota_hero_spirit_breaker)]
	internal class SpiritBreakerBase : BaseHero
	{
		// Token: 0x0600017D RID: 381 RVA: 0x000030E0 File Offset: 0x000012E0
		public SpiritBreakerBase(IContext9 context) : base(context)
		{
			this.chargeAwayMode = new ChargeAwayMode(this, new KeyPressModeMenu(base.Menu.RootMenu, "Charge away", null));
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000310B File Offset: 0x0000130B
		public override void Dispose()
		{
			base.Dispose();
			this.chargeAwayMode.Dispose();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000311E File Offset: 0x0000131E
		protected override void DisableCustomModes()
		{
			this.chargeAwayMode.Disable();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000312B File Offset: 0x0000132B
		protected override void EnableCustomModes()
		{
			this.chargeAwayMode.Enable();
		}

		// Token: 0x040000D8 RID: 216
		private readonly ChargeAwayMode chargeAwayMode;
	}
}
