using System;
using System.Collections.Generic;
using Ensage;
using SharpDX;

namespace O9K.Core.Helpers
{
	// Token: 0x02000087 RID: 135
	public static class Drawer
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x0001DBE0 File Offset: 0x0001BDE0
		public static void AddGreenCircle(Vector3 position)
		{
			ParticleEffect particleEffect = new ParticleEffect("materials\\ensage_ui\\particles\\drag_selected_ring_mod.vpcf", position);
			particleEffect.SetControlPoint(1u, new Vector3(0f, 255f, 0f));
			particleEffect.SetControlPoint(2u, new Vector3(50f, 255f, 0f));
			Drawer.Particles.Add(particleEffect);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001DC3C File Offset: 0x0001BE3C
		public static void AddRedCircle(Vector3 position)
		{
			ParticleEffect particleEffect = new ParticleEffect("materials\\ensage_ui\\particles\\drag_selected_ring_mod.vpcf", position);
			particleEffect.SetControlPoint(1u, new Vector3(255f, 0f, 0f));
			particleEffect.SetControlPoint(2u, new Vector3(70f, 255f, 0f));
			Drawer.Particles.Add(particleEffect);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001DC98 File Offset: 0x0001BE98
		public static void Dispose()
		{
			foreach (ParticleEffect particleEffect in Drawer.Particles)
			{
				if (particleEffect != null)
				{
					particleEffect.Dispose();
				}
			}
			ParticleEffect particleEffect2 = Drawer.greenParticle;
			if (particleEffect2 != null)
			{
				particleEffect2.Dispose();
			}
			ParticleEffect particleEffect3 = Drawer.redParticle;
			if (particleEffect3 == null)
			{
				return;
			}
			particleEffect3.Dispose();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001DD0C File Offset: 0x0001BF0C
		public static void DrawGreenCircle(Vector3 position)
		{
			if (Drawer.greenParticle == null)
			{
				Drawer.greenParticle = new ParticleEffect("materials\\ensage_ui\\particles\\drag_selected_ring_mod.vpcf", position);
				Drawer.greenParticle.SetControlPoint(1u, new Vector3(0f, 255f, 0f));
				Drawer.greenParticle.SetControlPoint(2u, new Vector3(50f, 255f, 0f));
			}
			Drawer.greenParticle.SetControlPoint(0u, position);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001DD80 File Offset: 0x0001BF80
		public static void DrawRedCircle(Vector3 position)
		{
			if (Drawer.redParticle == null)
			{
				Drawer.redParticle = new ParticleEffect("materials\\ensage_ui\\particles\\drag_selected_ring_mod.vpcf", position);
				Drawer.redParticle.SetControlPoint(1u, new Vector3(255f, 0f, 0f));
				Drawer.redParticle.SetControlPoint(2u, new Vector3(70f, 255f, 0f));
			}
			Drawer.redParticle.SetControlPoint(0u, position);
		}

		// Token: 0x040001DF RID: 479
		private static readonly List<ParticleEffect> Particles = new List<ParticleEffect>();

		// Token: 0x040001E0 RID: 480
		private static ParticleEffect greenParticle;

		// Token: 0x040001E1 RID: 481
		private static ParticleEffect redParticle;
	}
}
