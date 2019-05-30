using System;
using Ensage;

namespace O9K.Core.Helpers
{
	// Token: 0x02000092 RID: 146
	public sealed class Sleeper
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x0000240E File Offset: 0x0000060E
		public Sleeper()
		{
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00004AF8 File Offset: 0x00002CF8
		public Sleeper(float seconds)
		{
			this.sleepTime = Game.RawGameTime + seconds;
			this.sleeping = true;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00004B14 File Offset: 0x00002D14
		public bool IsSleeping
		{
			get
			{
				if (this.sleeping)
				{
					this.sleeping = (Game.RawGameTime < this.sleepTime);
				}
				return this.sleeping;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00004B37 File Offset: 0x00002D37
		public float RemainingSleepTime
		{
			get
			{
				if (!this.IsSleeping)
				{
					return 0f;
				}
				return this.sleepTime - Game.RawGameTime;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00004B53 File Offset: 0x00002D53
		public static implicit operator bool(Sleeper item)
		{
			return item.IsSleeping;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001E9A0 File Offset: 0x0001CBA0
		public void ExtendSleep(float seconds)
		{
			float rawGameTime = Game.RawGameTime;
			if (this.sleepTime > rawGameTime)
			{
				this.sleepTime += seconds;
			}
			else
			{
				this.sleepTime = rawGameTime + seconds;
			}
			this.sleeping = true;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00004B5B File Offset: 0x00002D5B
		public void Reset()
		{
			this.sleepTime = 0f;
			this.sleeping = false;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00004B6F File Offset: 0x00002D6F
		public void Sleep(float seconds)
		{
			this.sleepTime = Game.RawGameTime + seconds;
			this.sleeping = true;
		}

		// Token: 0x04000200 RID: 512
		private bool sleeping;

		// Token: 0x04000201 RID: 513
		private float sleepTime;
	}
}
