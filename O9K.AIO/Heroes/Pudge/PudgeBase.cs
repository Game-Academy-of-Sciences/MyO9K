using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Pudge.Modes;
using O9K.AIO.Modes.KeyPress;
using O9K.AIO.Modes.Permanent;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Pudge
{
	// Token: 0x020000C9 RID: 201
	[HeroId(HeroId.npc_dota_hero_pudge)]
	internal class PudgeBase : BaseHero
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0001647C File Offset: 0x0001467C
		public PudgeBase(IContext9 context) : base(context)
		{
			this.hookAllyMode = new HookAllyMode(this, new KeyPressModeMenu(base.Menu.RootMenu, "Hook ally", null));
			this.suicideMode = new SuicideMode(this, new PermanentModeMenu(base.Menu.RootMenu, "Suicide", null));
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00004255 File Offset: 0x00002455
		public override void Dispose()
		{
			base.Dispose();
			this.hookAllyMode.Dispose();
			this.suicideMode.Dispose();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00004273 File Offset: 0x00002473
		protected override void DisableCustomModes()
		{
			this.hookAllyMode.Disable();
			this.suicideMode.Disable();
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000428B File Offset: 0x0000248B
		protected override void EnableCustomModes()
		{
			this.hookAllyMode.Enable();
			this.suicideMode.Enable();
		}

		// Token: 0x04000244 RID: 580
		private readonly HookAllyMode hookAllyMode;

		// Token: 0x04000245 RID: 581
		private readonly SuicideMode suicideMode;
	}
}
