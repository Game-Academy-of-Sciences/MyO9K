using System;
using Ensage;

namespace O9K.Core.Helpers.Damage
{
	// Token: 0x0200009A RID: 154
	public class DamageAmplifier : Damage
	{
		// Token: 0x170000DB RID: 219
		public override float this[DamageType index]
		{
			get
			{
				float result;
				if (this.Damages.TryGetValue(index, out result))
				{
					return result;
				}
				return 1f;
			}
			set
			{
				this.Damages[index] = value;
			}
		}
	}
}
