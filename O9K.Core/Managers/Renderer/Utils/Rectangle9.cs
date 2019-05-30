using System;
using SharpDX;

namespace O9K.Core.Managers.Renderer.Utils
{
	// Token: 0x02000038 RID: 56
	public struct Rectangle9
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00013644 File Offset: 0x00011844
		public static Rectangle9 Zero
		{
			get
			{
				return new Rectangle9
				{
					IsZero = true
				};
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00002C8B File Offset: 0x00000E8B
		public Rectangle9 SinkToBottomRight(float width, float height)
		{
			return this.SinkToBottomRight(new Size2F(width, height));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00002C9A File Offset: 0x00000E9A
		public Rectangle9 SinkToBottomRight(Size2F size)
		{
			return new Rectangle9(this.Right - size.Width, this.Bottom - size.Height, size);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00002CBC File Offset: 0x00000EBC
		public Rectangle9 SinkToBottomRight(Vector2 size)
		{
			return this.SinkToBottomRight(new Size2F(size.X, size.Y));
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00002CD5 File Offset: 0x00000ED5
		public Rectangle9 SinkToBottomLeft(float width, float height)
		{
			return this.SinkToBottomLeft(new Size2F(width, height));
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public Rectangle9 SinkToBottomLeft(Size2F size)
		{
			return new Rectangle9(this.X, this.Bottom - size.Height, size);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00002CFF File Offset: 0x00000EFF
		public Rectangle9 SinkToBottomLeft(Vector2 size)
		{
			return this.SinkToBottomLeft(new Size2F(size.X, size.Y));
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00002D18 File Offset: 0x00000F18
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00002D20 File Offset: 0x00000F20
		public bool IsZero { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00002D29 File Offset: 0x00000F29
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00002D36 File Offset: 0x00000F36
		public float X
		{
			get
			{
				return this.Location.X;
			}
			set
			{
				this.Location.X = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00002D44 File Offset: 0x00000F44
		public Vector2 TopLeft
		{
			get
			{
				return new Vector2(this.Left, this.Top);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00002D57 File Offset: 0x00000F57
		public Vector2 TopRight
		{
			get
			{
				return new Vector2(this.Right, this.Top);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00002D6A File Offset: 0x00000F6A
		public Vector2 BottomLeft
		{
			get
			{
				return new Vector2(this.Left, this.Bottom);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00002D7D File Offset: 0x00000F7D
		public Vector2 BottomRight
		{
			get
			{
				return new Vector2(this.Right, this.Bottom);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00002D90 File Offset: 0x00000F90
		public bool Contains(Vector2 position)
		{
			return position.X >= this.Left && position.X <= this.Right && position.Y >= this.Top && position.Y <= this.Bottom;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00002D29 File Offset: 0x00000F29
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00002D36 File Offset: 0x00000F36
		public float Left
		{
			get
			{
				return this.Location.X;
			}
			set
			{
				this.Location.X = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00002DCF File Offset: 0x00000FCF
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00002DDC File Offset: 0x00000FDC
		public float Width
		{
			get
			{
				return this.Size.Width;
			}
			set
			{
				this.Size.Width = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00002DEA File Offset: 0x00000FEA
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00002DF7 File Offset: 0x00000FF7
		public float Height
		{
			get
			{
				return this.Size.Height;
			}
			set
			{
				this.Size.Height = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00013664 File Offset: 0x00011864
		public Vector2 Center
		{
			get
			{
				return new Vector2(this.Location.X + this.Size.Width / 2f, this.Location.Y + this.Size.Height / 2f);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00002E05 File Offset: 0x00001005
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00002D36 File Offset: 0x00000F36
		public float Right
		{
			get
			{
				return this.Location.X + this.Size.Width;
			}
			set
			{
				this.Location.X = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00002E1E File Offset: 0x0000101E
		public float Top
		{
			get
			{
				return this.Location.Y;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00002E2B File Offset: 0x0000102B
		public float Bottom
		{
			get
			{
				return this.Location.Y + this.Size.Height;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00002E1E File Offset: 0x0000101E
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00002E44 File Offset: 0x00001044
		public float Y
		{
			get
			{
				return this.Location.Y;
			}
			set
			{
				this.Location.Y = value;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00002E52 File Offset: 0x00001052
		public Rectangle9(float x, float y, float width, float height)
		{
			this.Location = new Vector2(x, y);
			this.Size = new Size2F(width, height);
			this.IsZero = false;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00002E76 File Offset: 0x00001076
		public Rectangle9(Vector2 location, Size2F size)
		{
			this.Location = location;
			this.Size = size;
			this.IsZero = false;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00002E8D File Offset: 0x0000108D
		public Rectangle9(Vector2 location, Vector2 size)
		{
			this.Location = location;
			this.Size = new Size2F(size.X, size.Y);
			this.IsZero = false;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00002EB4 File Offset: 0x000010B4
		public Rectangle9(float x, float y, Size2F size)
		{
			this.Location = new Vector2(x, y);
			this.Size = size;
			this.IsZero = false;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00002ED1 File Offset: 0x000010D1
		public Rectangle9(float x, float y, Vector2 size)
		{
			this.Location = new Vector2(x, y);
			this.Size = new Size2F(size.X, size.Y);
			this.IsZero = false;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00002EFE File Offset: 0x000010FE
		public Rectangle9(Vector2 location, float width, float height)
		{
			this.Location = location;
			this.Size = new Size2F(width, height);
			this.IsZero = false;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000136B0 File Offset: 0x000118B0
		public static Rectangle9 operator +(Rectangle9 rec, Size2F size)
		{
			return new Rectangle9(rec.Location.X, rec.Location.Y, rec.Size.Width + size.Width, rec.Size.Height + size.Height);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000136FC File Offset: 0x000118FC
		public static Rectangle9 operator *(Rectangle9 rec, Size2F size)
		{
			float num = rec.Size.Width * size.Width;
			float num2 = rec.Size.Height * size.Height;
			return new Rectangle9(rec.Location.X + (rec.Width - num) / 2f, rec.Location.Y + (rec.Height - num2) / 2f, num, num2);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00002F1B File Offset: 0x0000111B
		public static Rectangle9 operator +(Rectangle9 rec, Vector2 location)
		{
			return new Rectangle9(rec.Location.X + location.X, rec.Location.Y + location.Y, rec.Size);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00002F4C File Offset: 0x0000114C
		public static Rectangle9 operator *(Rectangle9 rec, Vector2 location)
		{
			return new Rectangle9(rec.Location.X * location.X, rec.Location.Y * location.Y, rec.Size);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0001376C File Offset: 0x0001196C
		public static Rectangle9 operator +(Rectangle9 rec, float size)
		{
			return new Rectangle9(rec.Location.X - size, rec.Location.Y - size, rec.Size.Width + size * 2f, rec.Size.Height + size * 2f);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000137C0 File Offset: 0x000119C0
		public static Rectangle9 operator -(Rectangle9 rec, float size)
		{
			return new Rectangle9(rec.Location.X + size / 2f, rec.Location.Y + size / 2f, rec.Size.Width - size, rec.Size.Height - size);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00002F7D File Offset: 0x0000117D
		public static Rectangle9 operator *(Rectangle9 rec, float size)
		{
			return rec * new Size2F(size, size);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00002F8C File Offset: 0x0000118C
		public static implicit operator RectangleF(Rectangle9 rec)
		{
			return new RectangleF(rec.Location.X, rec.Location.Y, rec.Size.Width, rec.Size.Height);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00013814 File Offset: 0x00011A14
		public static Rectangle9 operator +(Rectangle9 rec1, Rectangle9 rec2)
		{
			float num = Math.Min(rec1.Left, rec2.Left);
			float num2 = Math.Max(rec1.Right, rec2.Right);
			float num3 = Math.Min(rec1.Top, rec2.Top);
			float num4 = Math.Max(rec1.Bottom, rec2.Bottom);
			return new Rectangle9(num, num3, num2 - num, num4 - num3);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00013880 File Offset: 0x00011A80
		public override string ToString()
		{
			return string.Format("X:{0} Y:{1} Width:{2} Height:{3}", new object[]
			{
				this.X,
				this.Y,
				this.Width,
				this.Height
			});
		}

		// Token: 0x04000099 RID: 153
		public Size2F Size;

		// Token: 0x0400009A RID: 154
		public Vector2 Location;
	}
}
