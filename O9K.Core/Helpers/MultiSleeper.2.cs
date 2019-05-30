using System;
using System.Collections;
using System.Collections.Generic;

namespace O9K.Core.Helpers
{
	// Token: 0x02000091 RID: 145
	public class MultiSleeper<T> : IEnumerable<KeyValuePair<T, Sleeper>>, IEnumerable
	{
		// Token: 0x170000D7 RID: 215
		public Sleeper this[T key]
		{
			get
			{
				Sleeper result;
				if (!this.sleepers.TryGetValue(key, out result))
				{
					result = (this.sleepers[key] = new Sleeper());
				}
				return result;
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00004A75 File Offset: 0x00002C75
		public void ExtendSleep(T key, float seconds)
		{
			this[key].ExtendSleep(seconds);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00004A84 File Offset: 0x00002C84
		public IEnumerator<KeyValuePair<T, Sleeper>> GetEnumerator()
		{
			return this.sleepers.GetEnumerator();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00004A96 File Offset: 0x00002C96
		public bool IsSleeping(T key)
		{
			return this[key].IsSleeping;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00004AA4 File Offset: 0x00002CA4
		public void Remove(T key)
		{
			this.sleepers.Remove(key);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00004AB3 File Offset: 0x00002CB3
		public void Reset(T key)
		{
			this[key].Reset();
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00004AC1 File Offset: 0x00002CC1
		public void Reset()
		{
			this.sleepers.Clear();
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00004ACE File Offset: 0x00002CCE
		public void Sleep(T key, float seconds)
		{
			this[key].Sleep(seconds);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00004ADD File Offset: 0x00002CDD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040001FF RID: 511
		private readonly Dictionary<T, Sleeper> sleepers = new Dictionary<T, Sleeper>();
	}
}
