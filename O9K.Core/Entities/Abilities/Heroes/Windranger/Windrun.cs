using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Windranger
{
	// Token: 0x020001C8 RID: 456
	[AbilityId(AbilityId.windrunner_windrun)]
	public class Windrun : ActiveAbility, ISpeedBuff, IBuff, IShield, IActiveAbility
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x00022DF4 File Offset: 0x00020FF4
		public Windrun(Ability baseAbility) : base(baseAbility)
		{
			this.bonusMoveSpeedData = new SpecialData(baseAbility, "movespeed_bonus_pct");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00008487 File Offset: 0x00006687
		public UnitState AppliesUnitState { get; } = 4L;

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00004993 File Offset: 0x00002B93
		public override float CastPoint
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0000848F File Offset: 0x0000668F
		public string ShieldModifierName { get; } = "modifier_windrunner_windrun";

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00008497 File Offset: 0x00006697
		public bool ShieldsAlly { get; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0000849F File Offset: 0x0000669F
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x000084A7 File Offset: 0x000066A7
		public string BuffModifierName { get; } = "modifier_windrunner_windrun";

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x000084AF File Offset: 0x000066AF
		public bool BuffsAlly { get; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x000084B7 File Offset: 0x000066B7
		public bool BuffsOwner { get; } = 1;

		// Token: 0x06000935 RID: 2357 RVA: 0x000084BF File Offset: 0x000066BF
		public float GetSpeedBuff(Unit9 unit)
		{
			return unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level) / 100f;
		}

		// Token: 0x04000495 RID: 1173
		private readonly SpecialData bonusMoveSpeedData;
	}
}
