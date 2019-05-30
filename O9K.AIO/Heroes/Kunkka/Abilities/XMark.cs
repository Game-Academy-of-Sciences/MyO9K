using System;
using O9K.AIO.Abilities;
using O9K.Core.Entities.Abilities.Base;
using SharpDX;

namespace O9K.AIO.Heroes.Kunkka.Abilities
{
	// Token: 0x02000133 RID: 307
	internal class XMark : TargetableAbility
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x00002FCA File Offset: 0x000011CA
		public XMark(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000530F File Offset: 0x0000350F
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x00005317 File Offset: 0x00003517
		public Vector3 Position { get; set; }
	}
}
