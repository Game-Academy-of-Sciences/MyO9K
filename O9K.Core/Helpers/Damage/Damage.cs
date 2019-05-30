using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ensage;

namespace O9K.Core.Helpers.Damage
{
	// Token: 0x02000099 RID: 153
	public class Damage : IEnumerable<KeyValuePair<DamageType, float>>, IEnumerable
	{
		// Token: 0x170000DA RID: 218
		public virtual float this[DamageType index]
		{
			get
			{
				float result;
				this.Damages.TryGetValue(index, out result);
				return result;
			}
			set
			{
				this.Damages[index] = value;
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001EC40 File Offset: 0x0001CE40
		public static Damage operator +(Damage left, Damage right)
		{
			if (right == null)
			{
				return left;
			}
			foreach (KeyValuePair<DamageType, float> keyValuePair in right)
			{
				DamageType key = keyValuePair.Key;
				left[key] += keyValuePair.Value;
			}
			return left;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001ECA8 File Offset: 0x0001CEA8
		public static Damage operator *(Damage left, float right)
		{
			foreach (KeyValuePair<DamageType, float> keyValuePair in left.ToList<KeyValuePair<DamageType, float>>())
			{
				DamageType key = keyValuePair.Key;
				left[key] *= right;
			}
			return left;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00004CC5 File Offset: 0x00002EC5
		public IEnumerator<KeyValuePair<DamageType, float>> GetEnumerator()
		{
			return this.Damages.GetEnumerator();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001ED10 File Offset: 0x0001CF10
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<DamageType, float> keyValuePair in this.Damages)
			{
				stringBuilder.Append(keyValuePair.Key).Append(": ").Append(keyValuePair.Value).Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00004CD7 File Offset: 0x00002ED7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000211 RID: 529
		protected readonly Dictionary<DamageType, float> Damages = new Dictionary<DamageType, float>();
	}
}
