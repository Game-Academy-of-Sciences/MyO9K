using System;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;

namespace O9K.Core.Entities
{
	// Token: 0x020000AD RID: 173
	public abstract class Entity9 : IEquatable<Entity9>
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x00005140 File Offset: 0x00003340
		protected Entity9(Entity entity)
		{
			this.Name = entity.Name;
			this.BaseEntity = entity;
			this.Handle = entity.Handle;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0000516C File Offset: 0x0000336C
		public virtual string TextureName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0001F998 File Offset: 0x0001DB98
		public virtual string DisplayName
		{
			get
			{
				if (this.displayName == null)
				{
					try
					{
						this.displayName = LocalizationHelper.LocalizeName(this.BaseEntity);
					}
					catch
					{
						this.displayName = this.Name;
					}
				}
				return this.displayName;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00005174 File Offset: 0x00003374
		public string DefaultName
		{
			get
			{
				if (this.defaultName == null)
				{
					this.defaultName = this.Name.RemoveLevel();
				}
				return this.defaultName;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00005195 File Offset: 0x00003395
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0000519D File Offset: 0x0000339D
		public Unit9 Owner { get; internal set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x000051A6 File Offset: 0x000033A6
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x000051AE File Offset: 0x000033AE
		public float DeathTime { get; internal set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x000051B7 File Offset: 0x000033B7
		public Entity BaseEntity { get; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x000051BF File Offset: 0x000033BF
		public uint Handle { get; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000051C7 File Offset: 0x000033C7
		public bool IsValid
		{
			get
			{
				return this.BaseEntity.IsValid;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x000051D4 File Offset: 0x000033D4
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x000051DC File Offset: 0x000033DC
		public string Name { get; protected set; }

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001F9E8 File Offset: 0x0001DBE8
		public static bool operator ==(Entity9 obj1, Entity9 obj2)
		{
			uint? num = (obj1 != null) ? new uint?(obj1.Handle) : null;
			return num == ((obj2 != null) ? new uint?(obj2.Handle) : null);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000051E5 File Offset: 0x000033E5
		public static implicit operator Entity(Entity9 entity)
		{
			return entity.BaseEntity;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001FA4C File Offset: 0x0001DC4C
		public static bool operator !=(Entity9 obj1, Entity9 obj2)
		{
			uint? num = (obj1 != null) ? new uint?(obj1.Handle) : null;
			return num != ((obj2 != null) ? new uint?(obj2.Handle) : null);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001FAB4 File Offset: 0x0001DCB4
		public bool Equals(Entity9 other)
		{
			return this.Handle == ((other != null) ? new uint?(other.Handle) : null);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000051ED File Offset: 0x000033ED
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Entity9);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000051FB File Offset: 0x000033FB
		public override int GetHashCode()
		{
			return (int)this.Handle;
		}

		// Token: 0x04000240 RID: 576
		private string defaultName;

		// Token: 0x04000241 RID: 577
		private string displayName;
	}
}
