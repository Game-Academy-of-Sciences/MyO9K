using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.KillStealer;
using O9K.AIO.Modes.Permanent;
using O9K.Core.Entities.Abilities.Heroes.Weaver;
using O9K.Core.Entities.Heroes;
using O9K.Core.Extensions;
using O9K.Core.Managers.Entity;
using SharpDX;

namespace O9K.AIO.Heroes.Weaver.Modes
{
	// Token: 0x02000050 RID: 80
	internal class HealthTrackerMode : PermanentMode
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x000033B3 File Offset: 0x000015B3
		public HealthTrackerMode(BaseHero baseHero, PermanentModeMenu menu) : base(baseHero, menu)
		{
			UpdateHandlerExtensions.SetUpdateRate(this.Handler, 100);
			this.killSteal = baseHero.KillSteal;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000033E1 File Offset: 0x000015E1
		private TimeLapse TimeLapse
		{
			get
			{
				if (this.timeLapse == null)
				{
					this.timeLapse = EntityManager9.GetAbility<TimeLapse>(base.Owner.Hero);
				}
				return this.timeLapse;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000340D File Offset: 0x0000160D
		public override void Disable()
		{
			base.Disable();
			Drawing.OnDraw -= this.OnDraw;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000340D File Offset: 0x0000160D
		public override void Dispose()
		{
			base.Disable();
			Drawing.OnDraw -= this.OnDraw;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00003426 File Offset: 0x00001626
		public override void Enable()
		{
			base.Enable();
			Drawing.OnDraw += this.OnDraw;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000DE14 File Offset: 0x0000C014
		protected override void Execute()
		{
			Hero9 hero = base.Owner.Hero;
			if (hero == null || !hero.IsValid)
			{
				return;
			}
			float rawGameTime = Game.RawGameTime;
			this.healthTime[rawGameTime] = hero.Health;
			foreach (KeyValuePair<float, float> keyValuePair in this.healthTime.ToList<KeyValuePair<float, float>>())
			{
				float key = keyValuePair.Key;
				if (key + 6f < rawGameTime)
				{
					this.healthTime.Remove(key);
				}
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000DEC0 File Offset: 0x0000C0C0
		private void OnDraw(EventArgs args)
		{
			TimeLapse timeLapse = this.TimeLapse;
			if (timeLapse == null || !timeLapse.CanBeCasted(true))
			{
				return;
			}
			Hero9 hero = base.Owner.Hero;
			Vector2 healthBarPosition = hero.HealthBarPosition;
			if (healthBarPosition.IsZero)
			{
				return;
			}
			float time = Game.RawGameTime;
			float health = hero.Health;
			List<KeyValuePair<float, float>> list = (from x in this.healthTime
			orderby x.Key
			select x).ToList<KeyValuePair<float, float>>();
			float value = list.Find((KeyValuePair<float, float> x) => x.Key + 5f > time).Value;
			if (value <= health)
			{
				return;
			}
			float num = value / hero.MaximumHealth;
			Vector2 healthBarSize = hero.HealthBarSize;
			Vector2 vector = healthBarPosition + new Vector2(0f, healthBarSize.Y * 0.7f) + this.killSteal.AdditionalOverlayPosition;
			Vector2 vector2 = healthBarSize * new Vector2(num, 0.3f) + this.killSteal.AdditionalOverlayPosition;
			Drawing.DrawRect(vector, vector2, Color.DarkOliveGreen);
			Drawing.DrawRect(vector - new Vector2(1f), vector2 + new Vector2(1f), Color.Black, true);
			float value2 = list.Find((KeyValuePair<float, float> x) => x.Key + 4f > time).Value;
			if (value2 < value)
			{
				float num2 = (value - value2) / hero.MaximumHealth;
				Vector2 vector3 = healthBarSize * new Vector2(num2, 0.3f) + this.killSteal.AdditionalOverlayPosition;
				Vector2 vector4 = healthBarPosition + new Vector2(Math.Max(vector2.X - vector3.X, 0f), healthBarSize.Y * 0.7f) + this.killSteal.AdditionalOverlayPosition;
				Drawing.DrawRect(vector4, vector3, Color.LightGreen);
				Drawing.DrawRect(vector4 - new Vector2(1f), vector3 + new Vector2(1f), Color.Black, true);
			}
		}

		// Token: 0x040000F3 RID: 243
		private readonly Dictionary<float, float> healthTime = new Dictionary<float, float>();

		// Token: 0x040000F4 RID: 244
		private readonly KillSteal killSteal;

		// Token: 0x040000F5 RID: 245
		private TimeLapse timeLapse;
	}
}
