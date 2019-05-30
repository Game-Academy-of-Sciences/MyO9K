using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Components;

namespace O9K.Core.Entities.Abilities.Base
{
	// Token: 0x020003DE RID: 990
	public abstract class AutoCastAbility : RangedAbility, IToggleable
	{
		// Token: 0x06001084 RID: 4228 RVA: 0x00006527 File Offset: 0x00004727
		protected AutoCastAbility(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x0000E813 File Offset: 0x0000CA13
		public override bool BreaksLinkens { get; }

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x0000E81B File Offset: 0x0000CA1B
		// (set) Token: 0x06001087 RID: 4231 RVA: 0x00028F70 File Offset: 0x00027170
		public virtual bool Enabled
		{
			get
			{
				return base.BaseAbility.IsAutoCastEnabled;
			}
			set
			{
				bool flag = false;
				if (value)
				{
					if (!this.Enabled)
					{
						flag = base.BaseAbility.ToggleAutocastAbility();
					}
				}
				else if (this.Enabled)
				{
					flag = base.BaseAbility.ToggleAutocastAbility();
				}
				if (flag)
				{
					base.ActionSleeper.Sleep(0.1f);
				}
			}
		}
	}
}
