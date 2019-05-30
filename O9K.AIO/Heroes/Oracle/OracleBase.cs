using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Oracle.Modes;
using O9K.AIO.Modes.KeyPress;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Oracle
{
	// Token: 0x02000049 RID: 73
	[HeroId(HeroId.npc_dota_hero_oracle)]
	internal class OracleBase : BaseHero
	{
		// Token: 0x0600019C RID: 412 RVA: 0x0000321B File Offset: 0x0000141B
		public OracleBase(IContext9 context) : base(context)
		{
			this.healAllyMode = new HealAllyMode(this, new KeyPressModeMenu(base.Menu.RootMenu, "Heal ally", null));
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00003246 File Offset: 0x00001446
		public override void Dispose()
		{
			base.Dispose();
			this.healAllyMode.Dispose();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00003259 File Offset: 0x00001459
		protected override void DisableCustomModes()
		{
			this.healAllyMode.Disable();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00003266 File Offset: 0x00001466
		protected override void EnableCustomModes()
		{
			this.healAllyMode.Enable();
		}

		// Token: 0x040000E8 RID: 232
		private readonly HealAllyMode healAllyMode;
	}
}
