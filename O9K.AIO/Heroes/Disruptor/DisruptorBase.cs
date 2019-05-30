using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Disruptor.Modes;
using O9K.AIO.Modes.Permanent;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Disruptor
{
	// Token: 0x0200014D RID: 333
	[HeroId(HeroId.npc_dota_hero_disruptor)]
	internal class DisruptorBase : BaseHero
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x00005507 File Offset: 0x00003707
		public DisruptorBase(IContext9 context) : base(context)
		{
			this.glimpseTrackerMode = new GlimpseTrackerMode(this, new PermanentModeMenu(base.Menu.RootMenu, "Glimpse tracker", null));
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00005532 File Offset: 0x00003732
		public override void Dispose()
		{
			base.Dispose();
			this.glimpseTrackerMode.Dispose();
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00005545 File Offset: 0x00003745
		protected override void DisableCustomModes()
		{
			this.glimpseTrackerMode.Disable();
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00005552 File Offset: 0x00003752
		protected override void EnableCustomModes()
		{
			this.glimpseTrackerMode.Enable();
		}

		// Token: 0x04000391 RID: 913
		private readonly GlimpseTrackerMode glimpseTrackerMode;
	}
}
