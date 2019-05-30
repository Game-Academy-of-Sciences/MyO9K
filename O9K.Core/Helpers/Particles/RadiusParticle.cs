using System;
using Ensage;
using O9K.Core.Entities.Units;
using SharpDX;

namespace O9K.Core.Helpers.Particles
{
	// Token: 0x02000097 RID: 151
	public class RadiusParticle : IDisposable
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x00004C1C File Offset: 0x00002E1C
		public RadiusParticle(Vector3 position, Vector3 color, float radius)
		{
			this.particleEffect = new ParticleEffect("particles/ui_mouseactions/drag_selected_ring.vpcf", position);
			this.SetData(color, radius);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00004C3D File Offset: 0x00002E3D
		public RadiusParticle(Unit9 unit, Vector3 color, float radius)
		{
			this.particleEffect = new ParticleEffect("particles/ui_mouseactions/drag_selected_ring.vpcf", unit.BaseUnit);
			this.SetData(color, radius);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00004C63 File Offset: 0x00002E63
		public void ChangePosition(Vector3 position)
		{
			this.particleEffect.SetControlPoint(0u, position);
			this.particleEffect.FullRestart();
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00004C7D File Offset: 0x00002E7D
		public void Dispose()
		{
			this.particleEffect.Dispose();
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00004C8A File Offset: 0x00002E8A
		private void SetData(Vector3 color, float radius)
		{
			this.particleEffect.SetControlPoint(1u, color);
			this.particleEffect.SetControlPoint(2u, new Vector3(-radius, 255f, 0f));
		}

		// Token: 0x0400020B RID: 523
		private const string ParticleName = "particles/ui_mouseactions/drag_selected_ring.vpcf";

		// Token: 0x0400020C RID: 524
		private readonly ParticleEffect particleEffect;
	}
}
