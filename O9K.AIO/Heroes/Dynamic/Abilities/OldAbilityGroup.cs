using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Dynamic.Abilities
{
	// Token: 0x02000199 RID: 409
	internal class OldAbilityGroup<TType, TAbility> : IOldAbilityGroup where TType : class, IActiveAbility where TAbility : OldUsableAbility
	{
		// Token: 0x06000848 RID: 2120 RVA: 0x00025FC4 File Offset: 0x000241C4
		public OldAbilityGroup(BaseHero baseHero)
		{
			foreach (Type type in from x in Assembly.GetExecutingAssembly().GetTypes()
			where !x.IsAbstract && x.IsClass && typeof(TAbility).IsAssignableFrom(x)
			select x)
			{
				foreach (AbilityIdAttribute abilityIdAttribute in type.GetCustomAttributes<AbilityIdAttribute>())
				{
					this.UniqueAbilities.Add(abilityIdAttribute.AbilityId, type);
				}
			}
			this.AbilitySleeper = baseHero.AbilitySleeper;
			this.OrbwalkSleeper = baseHero.OrbwalkSleeper;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x000061C2 File Offset: 0x000043C2
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x000061CA File Offset: 0x000043CA
		public List<TAbility> Abilities { get; protected set; } = new List<TAbility>();

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x000061D3 File Offset: 0x000043D3
		protected MultiSleeper AbilitySleeper { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x000061DB File Offset: 0x000043DB
		protected virtual HashSet<AbilityId> Ignored { get; } = new HashSet<AbilityId>();

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x000061E3 File Offset: 0x000043E3
		protected MultiSleeper OrbwalkSleeper { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x000061EB File Offset: 0x000043EB
		protected Dictionary<AbilityId, Type> UniqueAbilities { get; } = new Dictionary<AbilityId, Type>();

		// Token: 0x0600084F RID: 2127 RVA: 0x000260B8 File Offset: 0x000242B8
		public virtual bool AddAbility(Ability9 ability)
		{
			if (this.IsIgnored(ability))
			{
				return false;
			}
			TType ttype = ability as TType;
			if (ttype == null)
			{
				return false;
			}
			Type typeFromHandle;
			if (!this.UniqueAbilities.TryGetValue(ability.Id, out typeFromHandle))
			{
				typeFromHandle = typeof(TAbility);
			}
			TAbility tability = (TAbility)((object)Activator.CreateInstance(typeFromHandle, new object[]
			{
				ttype
			}));
			tability.AbilitySleeper = this.AbilitySleeper;
			tability.OrbwalkSleeper = this.OrbwalkSleeper;
			this.Abilities.Add(tability);
			this.OrderAbilities();
			return true;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00026158 File Offset: 0x00024358
		public bool CanBeCasted(HashSet<AbilityId> check, ComboModeMenu menu, Unit9 target)
		{
			IEnumerable<TAbility> abilities = this.Abilities;
			Func<TAbility, bool> <>9__0;
			Func<TAbility, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((TAbility x) => check.Contains(x.Ability.Id)));
			}
			foreach (TAbility tability in abilities.Where(predicate))
			{
				if (tability.Ability.IsValid && !tability.CanHit(target) && tability.CanBeCasted(menu) && (!target.IsMagicImmune || tability.Ability.PiercesMagicImmunity(target)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00026224 File Offset: 0x00024424
		public void RemoveAbility(Ability9 ability)
		{
			TAbility tability = this.Abilities.Find((TAbility x) => x.Ability.Handle == ability.Handle);
			if (tability == null)
			{
				return;
			}
			IDisposable disposable;
			if ((disposable = (tability as IDisposable)) != null)
			{
				disposable.Dispose();
			}
			this.Abilities.Remove(tability);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00026284 File Offset: 0x00024484
		public virtual bool Use(Unit9 target, ComboModeMenu menu, params AbilityId[] except)
		{
			foreach (TAbility tability in this.Abilities)
			{
				if (tability.Ability.IsValid && !except.Contains(tability.Ability.Id) && tability.CanBeCasted(target, menu, true) && tability.Use(target))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000061F3 File Offset: 0x000043F3
		protected virtual bool IsIgnored(Ability9 ability)
		{
			return this.Ignored.Contains(ability.Id);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00026320 File Offset: 0x00024520
		protected virtual void OrderAbilities()
		{
			this.Abilities = (from x in this.Abilities
			orderby x.Ability is IChanneled, x.Ability.CastPoint
			select x).ToList<TAbility>();
		}
	}
}
