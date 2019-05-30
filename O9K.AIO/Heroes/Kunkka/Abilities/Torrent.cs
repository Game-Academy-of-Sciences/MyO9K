using System;
using Ensage;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Kunkka.Abilities
{
	// Token: 0x02000131 RID: 305
	internal class Torrent : DisableAbility
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x00003482 File Offset: 0x00001682
		public Torrent(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x000052AE File Offset: 0x000034AE
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x000052B6 File Offset: 0x000034B6
		public Modifier Modifier { get; set; }

		// Token: 0x06000615 RID: 1557 RVA: 0x00005281 File Offset: 0x00003481
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return !comboMenu.GetAbilitySettingsMenu<XMarkOnlyMenu>(this).XMarkOnly && base.CanHit(targetManager, comboMenu);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000052A0 File Offset: 0x000034A0
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new XMarkOnlyMenu(base.Ability, simplifiedName);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001E0F4 File Offset: 0x0001C2F4
		public bool ShouldReturn(ActiveAbility xReturn, Vector3 xMarkPosition)
		{
			Modifier modifier = this.Modifier;
			if (modifier == null || !modifier.IsValid)
			{
				return false;
			}
			if (xMarkPosition.Distance2D(this.Modifier.Owner.Position, false) > base.Ability.Radius)
			{
				return false;
			}
			float num = base.Ability.ActivationDelay - this.Modifier.ElapsedTime;
			float num2 = xReturn.GetCastDelay() + 0.1f;
			return num <= num2;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001E088 File Offset: 0x0001C288
		public bool UseAbility(Vector3 position, TargetManager targetManager, Sleeper comboSleeper)
		{
			if (!base.Ability.UseAbility(position, false, false))
			{
				return false;
			}
			float num = base.Ability.GetHitTime(targetManager.Target) + 0.5f;
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.OrbwalkSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(num);
			return true;
		}
	}
}
