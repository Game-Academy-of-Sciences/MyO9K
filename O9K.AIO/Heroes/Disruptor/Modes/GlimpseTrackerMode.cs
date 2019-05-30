using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Permanent;
using O9K.Core.Entities.Units;
using SharpDX;

namespace O9K.AIO.Heroes.Disruptor.Modes
{
	// Token: 0x0200014F RID: 335
	internal class GlimpseTrackerMode : PermanentMode
	{
		// Token: 0x06000696 RID: 1686 RVA: 0x0001FE9C File Offset: 0x0001E09C
		public GlimpseTrackerMode(BaseHero baseHero, PermanentModeMenu menu) : base(baseHero, menu)
		{
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001FEE8 File Offset: 0x0001E0E8
		protected override void Execute()
		{
			float time = Game.RawGameTime;
			foreach (Unit9 unit in base.TargetManager.EnemyHeroes)
			{
				Dictionary<float, Vector3> dictionary;
				if (!this.positions.TryGetValue(unit, out dictionary))
				{
					dictionary = new Dictionary<float, Vector3>();
					this.positions[unit] = dictionary;
				}
				else
				{
					foreach (KeyValuePair<float, Vector3> keyValuePair in dictionary.ToList<KeyValuePair<float, Vector3>>())
					{
						float key = keyValuePair.Key;
						if (key + 4.1f < time)
						{
							dictionary.Remove(key);
						}
					}
				}
				dictionary[time] = unit.Position;
			}
			if (!base.TargetManager.HasValidTarget)
			{
				this.RemoveGlimpseParticle();
				return;
			}
			Dictionary<float, Vector3> source;
			if (!this.positions.TryGetValue(base.TargetManager.Target, out source))
			{
				return;
			}
			Vector3 value = (from x in source
			orderby x.Key
			select x).FirstOrDefault((KeyValuePair<float, Vector3> x) => time - x.Key > 4f).Value;
			if (value.IsZero)
			{
				this.RemoveGlimpseParticle();
				return;
			}
			this.DrawGlimpseParticle(value);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00020070 File Offset: 0x0001E270
		private void DrawGlimpseParticle(Vector3 position)
		{
			if (this.targetParticleEffect == null)
			{
				this.targetParticleEffect = new ParticleEffect("materials\\ensage_ui\\particles\\target.vpcf", position);
				this.targetParticleEffect.SetControlPoint(6u, new Vector3(255f));
			}
			this.targetParticleEffect.SetControlPoint(2u, base.TargetManager.Target.Position);
			this.targetParticleEffect.SetControlPoint(5u, this.color);
			this.targetParticleEffect.SetControlPoint(7u, position);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000055B9 File Offset: 0x000037B9
		private void RemoveGlimpseParticle()
		{
			if (this.targetParticleEffect == null)
			{
				return;
			}
			this.targetParticleEffect.Dispose();
			this.targetParticleEffect = null;
		}

		// Token: 0x0400039A RID: 922
		private readonly Vector3 color = new Vector3((float)Color.Blue.R, (float)Color.Blue.G, (float)Color.Blue.B);

		// Token: 0x0400039B RID: 923
		private readonly Dictionary<Unit9, Dictionary<float, Vector3>> positions = new Dictionary<Unit9, Dictionary<float, Vector3>>();

		// Token: 0x0400039C RID: 924
		private ParticleEffect targetParticleEffect;
	}
}
