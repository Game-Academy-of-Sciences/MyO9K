using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Heroes.AncientApparition
{
	// Token: 0x02000263 RID: 611
	[AbilityId(AbilityId.ancient_apparition_ice_blast)]
	public class IceBlast : CircleAbility
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x00024B00 File Offset: 0x00022D00
		public IceBlast(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius_min");
			this.radiusMaxData = new SpecialData(baseAbility, "radius_max");
			this.radiusGrowData = new SpecialData(baseAbility, "radius_grow");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0000A0AC File Offset: 0x000082AC
		public float MaxRadius
		{
			get
			{
				return this.radiusMaxData.GetValue(this.Level);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0000A0BF File Offset: 0x000082BF
		public override float CastRange { get; } = 9999999f;

		// Token: 0x06000B1C RID: 2844 RVA: 0x0000A0C7 File Offset: 0x000082C7
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			if (this.IsUsable)
			{
				return base.CanBeCasted(checkChanneling);
			}
			return this.iceBlastRelease.CanBeCasted(checkChanneling);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00024B64 File Offset: 0x00022D64
		public float GetRadius(Vector3 position)
		{
			float num = base.Owner.Distance(position) / this.Speed * this.radiusGrowData.GetValue(this.Level);
			return Math.Min(this.Radius + num, this.MaxRadius);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0000A0E5 File Offset: 0x000082E5
		public float GetReleaseFlyTime(Vector3 position)
		{
			return Math.Min(base.Owner.Distance(position) / this.iceBlastRelease.Speed, 2f);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0000A109 File Offset: 0x00008309
		public override bool UseAbility(bool queue = false, bool bypass = false)
		{
			return this.iceBlastRelease.UseAbility(queue, bypass);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00024BAC File Offset: 0x00022DAC
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.ancient_apparition_ice_blast_release)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				throw new ArgumentNullException("iceBlastRelease");
			}
			this.iceBlastRelease = (IceBlastRelease)EntityManager9.AddAbility(ability);
		}

		// Token: 0x040005A6 RID: 1446
		private readonly SpecialData radiusGrowData;

		// Token: 0x040005A7 RID: 1447
		private readonly SpecialData radiusMaxData;

		// Token: 0x040005A8 RID: 1448
		private IceBlastRelease iceBlastRelease;
	}
}
