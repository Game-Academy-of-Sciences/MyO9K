using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.EmberSpirit.Modes;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.EmberSpirit
{
	// Token: 0x02000179 RID: 377
	[HeroId(HeroId.npc_dota_hero_ember_spirit)]
	internal class EmberSpiritBase : BaseHero
	{
		// Token: 0x060007BA RID: 1978 RVA: 0x00005E88 File Offset: 0x00004088
		public EmberSpiritBase(IContext9 context) : base(context)
		{
			this.autoChainsMode = new AutoChainsMode(this, new AutoChainsModeMenu(base.Menu.RootMenu, "Auto chains", "Hold \"w\" to auto cast chains when using fist manually"));
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00005EB7 File Offset: 0x000040B7
		public override void Dispose()
		{
			base.Dispose();
			this.autoChainsMode.Dispose();
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00005ECA File Offset: 0x000040CA
		protected override void DisableCustomModes()
		{
			this.autoChainsMode.Disable();
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00005ED7 File Offset: 0x000040D7
		protected override void EnableCustomModes()
		{
			this.autoChainsMode.Enable();
		}

		// Token: 0x0400043D RID: 1085
		private readonly AutoChainsMode autoChainsMode;
	}
}
