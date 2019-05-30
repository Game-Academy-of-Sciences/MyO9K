using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Abilities.Heroes.Invoker.Helpers;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Heroes.Invoker
{
	// Token: 0x0200035C RID: 860
	[AbilityId(AbilityId.invoker_chaos_meteor)]
	public class ChaosMeteor : LineAbility, IInvokableAbility, IHarass, IActiveAbility
	{
		// Token: 0x06000E95 RID: 3733 RVA: 0x00027680 File Offset: 0x00025880
		public ChaosMeteor(Ability baseAbility)
		{
			AbilityId[] array = new AbilityId[3];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.38E80F136998F9B2676B4361853C199FD25531E4).FieldHandle);
			this.RequiredOrbs = array;
			base..ctor(baseAbility);
			this.invokeHelper = new InvokeHelper<ChaosMeteor>(this);
			this.ActivationDelayData = new SpecialData(baseAbility, "land_time");
			this.RadiusData = new SpecialData(baseAbility, "area_of_effect");
			this.RangeData = new SpecialData(baseAbility, "travel_distance");
			this.SpeedData = new SpecialData(baseAbility, "travel_speed");
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0000CD1E File Offset: 0x0000AF1E
		public bool CanBeInvoked
		{
			get
			{
				return this.IsInvoked || this.invokeHelper.CanInvoke(false);
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0000CD36 File Offset: 0x0000AF36
		public bool IsInvoked
		{
			get
			{
				return this.invokeHelper.IsInvoked;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0000CD43 File Offset: 0x0000AF43
		public override bool IsReady
		{
			get
			{
				return this.Level != 0u && base.RemainingCooldown <= 0f && base.Owner.Mana >= base.ManaCost;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0000CD72 File Offset: 0x0000AF72
		public override float Range
		{
			get
			{
				return this.RangeData.GetValue(this.invokeHelper.Wex.Level) + this.Radius;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0000CD96 File Offset: 0x0000AF96
		public AbilityId[] RequiredOrbs { get; }

		// Token: 0x06000E9B RID: 3739 RVA: 0x0000CD9E File Offset: 0x0000AF9E
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0000CDBF File Offset: 0x0000AFBF
		public bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false)
		{
			return this.invokeHelper.Invoke(currentOrbs, queue, bypass);
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0000CDCF File Offset: 0x0000AFCF
		public override bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
		{
			return this.Invoke(null, false, bypass) && base.UseAbility(position, queue, bypass);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0000CDE7 File Offset: 0x0000AFE7
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			this.invokeHelper.SetOwner(owner);
		}

		// Token: 0x040007A4 RID: 1956
		private readonly InvokeHelper<ChaosMeteor> invokeHelper;
	}
}
