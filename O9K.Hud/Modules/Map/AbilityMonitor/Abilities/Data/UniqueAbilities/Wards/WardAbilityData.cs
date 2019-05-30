using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base;
using SharpDX;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Wards
{
	// Token: 0x0200007A RID: 122
	internal class WardAbilityData : AbilityFullData
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00003BAB File Offset: 0x00001DAB
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00003BB3 File Offset: 0x00001DB3
		public string UnitName { get; set; }

		// Token: 0x060002C6 RID: 710 RVA: 0x0001A598 File Offset: 0x00018798
		public void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, Vector3 position)
		{
			DrawableWardAbility drawableWardAbility = new DrawableWardAbility
			{
				AbilityUnitName = this.UnitName,
				AbilityTexture = base.AbilityId + "_rounded",
				Position = position,
				Duration = base.Duration,
				IsShowingRange = base.ShowRange,
				Range = base.Range,
				RangeColor = base.RangeColor,
				ShowUntil = Game.RawGameTime + base.Duration
			};
			drawableWardAbility.DrawRange();
			drawableAbilities.Add(drawableWardAbility);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0001A628 File Offset: 0x00018828
		public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, Unit unit, INotificator notificator)
		{
			DrawableWardAbility drawableWardAbility = drawableAbilities.OfType<DrawableWardAbility>().FirstOrDefault((DrawableWardAbility x) => x.Unit == null && x.AbilityUnitName == this.UnitName && x.Position.Distance2D(unit.Position, false) < 400f);
			if (drawableWardAbility != null)
			{
				drawableWardAbility.AddUnit(unit);
				return;
			}
			Modifier modifier = unit.Modifiers.FirstOrDefault((Modifier x) => x.Name == "modifier_item_buff_ward");
			DrawableWardAbility drawableWardAbility2 = new DrawableWardAbility
			{
				AbilityUnitName = this.UnitName,
				AbilityTexture = base.AbilityId + "_rounded",
				Position = unit.Position,
				Unit = unit,
				Duration = base.Duration,
				IsShowingRange = base.ShowRange,
				Range = base.Range,
				RangeColor = base.RangeColor,
				AddedTime = Game.RawGameTime - ((modifier != null) ? modifier.ElapsedTime : 0f),
				ShowUntil = Game.RawGameTime + ((modifier != null) ? modifier.RemainingTime : base.Duration)
			};
			drawableWardAbility2.DrawRange();
			drawableAbilities.Add(drawableWardAbility2);
		}
	}
}
