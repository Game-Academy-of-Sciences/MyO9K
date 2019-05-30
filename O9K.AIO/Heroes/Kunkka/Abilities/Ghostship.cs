using System;
using Ensage;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Kunkka;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Kunkka.Abilities
{
	// Token: 0x02000130 RID: 304
	internal class Ghostship : NukeAbility
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x0000521C File Offset: 0x0000341C
		public Ghostship(ActiveAbility ability) : base(ability)
		{
			this.ghostship = (Ghostship)ability;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00005231 File Offset: 0x00003431
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x00005239 File Offset: 0x00003439
		public float HitTime { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00005242 File Offset: 0x00003442
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0000524A File Offset: 0x0000344A
		public Vector3 Position { get; set; }

		// Token: 0x0600060D RID: 1549 RVA: 0x00005253 File Offset: 0x00003453
		public void CalculateTimings(Vector3 position, float time)
		{
			this.Position = position;
			this.HitTime = time + this.ghostship.GhostshipDistance / this.ghostship.Speed - 0.1f;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00005281 File Offset: 0x00003481
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return !comboMenu.GetAbilitySettingsMenu<XMarkOnlyMenu>(this).XMarkOnly && base.CanHit(targetManager, comboMenu);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x000052A0 File Offset: 0x000034A0
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new XMarkOnlyMenu(base.Ability, simplifiedName);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001E02C File Offset: 0x0001C22C
		public bool ShouldReturn(ActiveAbility xReturn, Vector3 xMarkPosition)
		{
			if (this.Position.IsZero)
			{
				return false;
			}
			if (xMarkPosition.Distance2D(this.Position, false) > base.Ability.Radius)
			{
				return false;
			}
			float num = this.HitTime - Game.RawGameTime;
			float num2 = xReturn.GetCastDelay() + 0.1f;
			return num <= num2;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001E088 File Offset: 0x0001C288
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

		// Token: 0x04000357 RID: 855
		private readonly Ghostship ghostship;
	}
}
