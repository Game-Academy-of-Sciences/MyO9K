using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.Items;
using O9K.Hud.Core;
using O9K.Hud.Modules.Units.Ranges.Abilities;

namespace O9K.Hud.Modules.Units.Ranges
{
	// Token: 0x0200000B RID: 11
	internal class Ranges : IDisposable, IHudModule
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000021B1 File Offset: 0x000003B1
		[ImportingConstructor]
		public Ranges(IContext9 context, IHudMenu hudMenu)
		{
			this.context = context;
			this.menu = hudMenu.UnitsMenu.Add<Menu>(new Menu("Ranges"));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000059CC File Offset: 0x00003BCC
		public void Activate()
		{
			this.context.Renderer.TextureManager.LoadFromDota("attack", "panorama\\images\\hud\\reborn\\ping_icon_attack_psd.vtex_c", 0, 0, false, 0, null);
			EntityManager9.UnitAdded += this.OnUnitAdded;
			EntityManager9.UnitRemoved += this.OnUnitRemoved;
			EntityManager9.AbilityAdded += this.OnAbilityAdded;
			EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
			UpdateManager.Subscribe(new Action(this.OnUpdate), 3000, true);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00005A64 File Offset: 0x00003C64
		public void Dispose()
		{
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
			UpdateManager.Unsubscribe(new Action(this.OnUpdate));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00005AC8 File Offset: 0x00003CC8
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (!ability.IsTalent && !ability.IsStolen)
				{
					RangeUnit rangeUnit = this.rangeUnits.Find((RangeUnit x) => x.Handle == ability.Owner.Handle);
					if (rangeUnit != null)
					{
						rangeUnit.AddAbility(ability);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00005B44 File Offset: 0x00003D44
		private void OnAbilityRemoved(Ability9 ability)
		{
			try
			{
				if (!ability.IsTalent && !ability.IsStolen)
				{
					RangeUnit rangeUnit = this.rangeUnits.Find((RangeUnit x) => x.Handle == ability.Owner.Handle);
					if (rangeUnit != null)
					{
						rangeUnit.RemoveAbility(ability);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000021E6 File Offset: 0x000003E6
		private void OnUnitAdded(Unit9 unit)
		{
			if (!unit.IsHero || unit.IsIllusion)
			{
				return;
			}
			this.rangeUnits.Add(new RangeUnit(unit, this.menu));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00005BC0 File Offset: 0x00003DC0
		private void OnUnitRemoved(Unit9 unit)
		{
			if (!unit.IsHero || unit.IsIllusion)
			{
				return;
			}
			RangeUnit rangeUnit = this.rangeUnits.Find((RangeUnit x) => x.Handle == unit.Handle);
			if (rangeUnit == null)
			{
				return;
			}
			rangeUnit.Dispose();
			this.rangeUnits.Remove(rangeUnit);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00005C24 File Offset: 0x00003E24
		private void OnUpdate()
		{
			try
			{
				foreach (RangeUnit rangeUnit in this.rangeUnits)
				{
					rangeUnit.UpdateRanges();
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x04000020 RID: 32
		private readonly IContext9 context;

		// Token: 0x04000021 RID: 33
		private readonly Menu menu;

		// Token: 0x04000022 RID: 34
		private readonly List<RangeUnit> rangeUnits = new List<RangeUnit>();
	}
}
