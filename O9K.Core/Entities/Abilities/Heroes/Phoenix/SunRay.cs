using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Abilities.Heroes.Phoenix
{
	// Token: 0x020002FA RID: 762
	[AbilityId(AbilityId.phoenix_sun_ray)]
	public class SunRay : LineAbility
	{
		// Token: 0x06000D30 RID: 3376 RVA: 0x0000BC29 File Offset: 0x00009E29
		public SunRay(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "base_damage");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0000BC54 File Offset: 0x00009E54
		public override bool HasAreaOfEffect { get; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0000BC5C File Offset: 0x00009E5C
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x0000BC64 File Offset: 0x00009E64
		public bool IsSunRayMoving { get; private set; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0000BC6D File Offset: 0x00009E6D
		public bool IsSunRayActive
		{
			get
			{
				return !base.IsActive;
			}
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0000BC78 File Offset: 0x00009E78
		public override void Dispose()
		{
			base.Dispose();
			Player.OnExecuteOrder -= this.OnExecuteOrder;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0000BC91 File Offset: 0x00009E91
		public bool Stop()
		{
			return this.sunRayStop.UseAbility(false, false);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0000BCA0 File Offset: 0x00009EA0
		public bool ToggleMovement()
		{
			return this.sunRayToggleMovement.UseAbility(false, false);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00026624 File Offset: 0x00024824
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.phoenix_sun_ray_stop)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				throw new ArgumentNullException("sunRayStop");
			}
			this.sunRayStop = (SunRayStop)EntityManager9.AddAbility(ability);
			ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.phoenix_sun_ray_toggle_move)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				throw new ArgumentNullException("sunRayToggleMovement");
			}
			this.sunRayToggleMovement = (SunRayToggleMovement)EntityManager9.AddAbility(ability);
			if (owner.IsControllable)
			{
				Player.OnExecuteOrder += this.OnExecuteOrder;
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x000266E0 File Offset: 0x000248E0
		private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
		{
			try
			{
				if (args.Process && !args.IsQueued && (args.OrderId == OrderId.Ability || args.OrderId == OrderId.AbilityLocation))
				{
					EntityHandle handle = args.Ability.Handle;
					if (handle == base.Handle)
					{
						this.IsSunRayMoving = false;
					}
					else if (handle == this.sunRayToggleMovement.Handle)
					{
						this.IsSunRayMoving = !this.IsSunRayMoving;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x040006D0 RID: 1744
		private SunRayStop sunRayStop;

		// Token: 0x040006D1 RID: 1745
		private SunRayToggleMovement sunRayToggleMovement;
	}
}
