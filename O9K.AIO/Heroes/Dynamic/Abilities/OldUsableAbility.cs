using System;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Dynamic.Abilities
{
	// Token: 0x0200019E RID: 414
	internal abstract class OldUsableAbility
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x00006299 File Offset: 0x00004499
		protected OldUsableAbility(IActiveAbility ability)
		{
			this.Ability = ability;
			this.Owner = this.Ability.Owner;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x000062C4 File Offset: 0x000044C4
		public Unit9 Owner { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x000062CC File Offset: 0x000044CC
		public IActiveAbility Ability { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x000062D4 File Offset: 0x000044D4
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x000062DC File Offset: 0x000044DC
		public MultiSleeper AbilitySleeper { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x000062E5 File Offset: 0x000044E5
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x000062ED File Offset: 0x000044ED
		public MultiSleeper OrbwalkSleeper { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x000062F6 File Offset: 0x000044F6
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x000062FE File Offset: 0x000044FE
		public MultiSleeper TargetSleeper { get; set; } = new MultiSleeper();

		// Token: 0x06000869 RID: 2153 RVA: 0x00026388 File Offset: 0x00024588
		public virtual bool CanBeCasted(ComboModeMenu menu)
		{
			return menu.IsAbilityEnabled(this.Ability) && !this.AbilitySleeper.IsSleeping(this.Ability.Handle) && !this.OrbwalkSleeper.IsSleeping(this.Ability.Owner.Handle) && this.Ability.CanBeCasted(true);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00006307 File Offset: 0x00004507
		public virtual bool CanBeCasted(Unit9 target, ComboModeMenu menu, bool canHitCheck = true)
		{
			return this.CanBeCasted(menu) && (!canHitCheck || this.CanHit(target)) && this.ShouldCast(target);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0000632E File Offset: 0x0000452E
		public virtual bool CanHit(Unit9 target)
		{
			return this.Ability.CanHit(target);
		}

		// Token: 0x0600086C RID: 2156
		public abstract bool ShouldCast(Unit9 target);

		// Token: 0x0600086D RID: 2157
		public abstract bool Use(Unit9 target);
	}
}
