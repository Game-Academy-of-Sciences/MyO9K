using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Juggernaut.Modes;
using O9K.AIO.Modes.Permanent;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Juggernaut
{
	// Token: 0x0200015F RID: 351
	[HeroId(HeroId.npc_dota_hero_juggernaut)]
	internal class JuggernautBase : BaseHero
	{
		// Token: 0x0600073C RID: 1852 RVA: 0x00005A9D File Offset: 0x00003C9D
		public JuggernautBase(IContext9 context) : base(context)
		{
			this.controlWardMode = new ControlWardMode(this, new PermanentModeMenu(base.Menu.RootMenu, "Healing ward control", null));
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00005AC8 File Offset: 0x00003CC8
		public override void Dispose()
		{
			base.Dispose();
			this.controlWardMode.Dispose();
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00005ADB File Offset: 0x00003CDB
		protected override void DisableCustomModes()
		{
			this.controlWardMode.Disable();
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00005AE8 File Offset: 0x00003CE8
		protected override void EnableCustomModes()
		{
			this.controlWardMode.Enable();
		}

		// Token: 0x040003F4 RID: 1012
		private readonly ControlWardMode controlWardMode;
	}
}
