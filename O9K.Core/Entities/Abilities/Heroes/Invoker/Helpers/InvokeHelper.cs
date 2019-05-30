using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Invoker.BaseAbilities;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.Invoker.Helpers
{
	// Token: 0x02000367 RID: 871
	internal class InvokeHelper<T> where T : ActiveAbility, IInvokableAbility
	{
		// Token: 0x06000F09 RID: 3849 RVA: 0x0000D441 File Offset: 0x0000B641
		public InvokeHelper(T ability)
		{
			this.invokableAbility = ability;
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0000D467 File Offset: 0x0000B667
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x0000D46F File Offset: 0x0000B66F
		public Exort Exort { get; private set; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0000D478 File Offset: 0x0000B678
		public bool IsInvoked
		{
			get
			{
				return !this.invokableAbility.BaseAbility.IsHidden || this.invokeTime + 0.5f > Game.RawGameTime;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x0000D4A6 File Offset: 0x0000B6A6
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x0000D4AE File Offset: 0x0000B6AE
		public Quas Quas { get; private set; }

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0000D4B7 File Offset: 0x0000B6B7
		// (set) Token: 0x06000F10 RID: 3856 RVA: 0x0000D4BF File Offset: 0x0000B6BF
		public Wex Wex { get; private set; }

		// Token: 0x06000F11 RID: 3857 RVA: 0x00027B30 File Offset: 0x00025D30
		public bool CanInvoke(bool checkAbilityManaCost)
		{
			if (this.IsInvoked)
			{
				return true;
			}
			Invoke invoke = this.invoke;
			return invoke != null && invoke.CanBeCasted(true) && (!checkAbilityManaCost || this.owner.Mana >= this.invoke.ManaCost + this.invokableAbility.ManaCost);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00027B90 File Offset: 0x00025D90
		public bool Invoke(List<AbilityId> currentOrbs, bool queue, bool bypass)
		{
			if (this.IsInvoked)
			{
				return true;
			}
			Invoke invoke = this.invoke;
			if (invoke == null || !invoke.CanBeCasted(true))
			{
				return false;
			}
			List<AbilityId> castedOrbs = currentOrbs ?? (from x in this.owner.BaseUnit.Modifiers
			where !x.IsHidden && this.orbModifiers.ContainsKey(x.Name)
			select this.orbModifiers[x.Name]).ToList<AbilityId>();
			using (IEnumerator<AbilityId> enumerator = this.GetMissingOrbs(castedOrbs).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AbilityId id = enumerator.Current;
					ActiveAbility activeAbility = this.myOrbs.FirstOrDefault((ActiveAbility x) => x.BaseAbility.Id == id && x.CanBeCasted(true));
					if (activeAbility == null)
					{
						return false;
					}
					if (!activeAbility.UseAbility(queue, bypass))
					{
						return false;
					}
				}
			}
			bool flag = this.invoke.UseAbility(queue, bypass);
			if (flag)
			{
				this.invokeTime = Game.RawGameTime;
			}
			return flag;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00027C98 File Offset: 0x00025E98
		public void SetOwner(Unit9 newOwner)
		{
			this.owner = newOwner;
			Ability ability;
			if ((ability = newOwner.GetAbilityById(AbilityId.invoker_wex)) == null)
			{
				ability = EntityManager<Ability>.Entities.FirstOrDefault((Ability x) => x.IsValid && x.Id == AbilityId.invoker_wex);
			}
			Ability ability2 = ability;
			if (ability2 != null)
			{
				this.Wex = new Wex(ability2);
				this.Wex.SetOwner(newOwner);
				this.orbModifiers.Add(this.Wex.ModifierName, this.Wex.BaseAbility.Id);
				this.myOrbs.Add(this.Wex);
			}
			Ability ability3;
			if ((ability3 = newOwner.GetAbilityById(AbilityId.invoker_quas)) == null)
			{
				ability3 = EntityManager<Ability>.Entities.FirstOrDefault((Ability x) => x.IsValid && x.Id == AbilityId.invoker_quas);
			}
			Ability ability4 = ability3;
			if (ability4 != null)
			{
				this.Quas = new Quas(ability4);
				this.Quas.SetOwner(newOwner);
				this.orbModifiers.Add(this.Quas.ModifierName, this.Quas.BaseAbility.Id);
				this.myOrbs.Add(this.Quas);
			}
			Ability ability5;
			if ((ability5 = newOwner.GetAbilityById(AbilityId.invoker_exort)) == null)
			{
				ability5 = EntityManager<Ability>.Entities.FirstOrDefault((Ability x) => x.IsValid && x.Id == AbilityId.invoker_exort);
			}
			Ability ability6 = ability5;
			if (ability6 != null)
			{
				this.Exort = new Exort(ability6);
				this.Exort.SetOwner(newOwner);
				this.orbModifiers.Add(this.Exort.ModifierName, this.Exort.BaseAbility.Id);
				this.myOrbs.Add(this.Exort);
			}
			Ability abilityById = newOwner.GetAbilityById(AbilityId.invoker_invoke);
			if (abilityById != null)
			{
				this.invoke = new Invoke(abilityById);
				this.invoke.SetOwner(newOwner);
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00027E8C File Offset: 0x0002608C
		private IEnumerable<AbilityId> GetMissingOrbs(List<AbilityId> castedOrbs)
		{
			List<AbilityId> orbs = castedOrbs.ToList<AbilityId>();
			List<AbilityId> list = (from x in this.invokableAbility.RequiredOrbs
			where !orbs.Remove(x)
			select x).ToList<AbilityId>();
			if (list.Count == 0)
			{
				return Enumerable.Empty<AbilityId>();
			}
			castedOrbs.RemoveRange(0, Math.Max(castedOrbs.Count - this.invokableAbility.RequiredOrbs.Length + list.Count, 0));
			castedOrbs.AddRange(list);
			return list.Concat(this.GetMissingOrbs(castedOrbs));
		}

		// Token: 0x040007C8 RID: 1992
		private readonly T invokableAbility;

		// Token: 0x040007C9 RID: 1993
		private readonly List<ActiveAbility> myOrbs = new List<ActiveAbility>();

		// Token: 0x040007CA RID: 1994
		private readonly Dictionary<string, AbilityId> orbModifiers = new Dictionary<string, AbilityId>(3);

		// Token: 0x040007CB RID: 1995
		private Invoke invoke;

		// Token: 0x040007CC RID: 1996
		private float invokeTime;

		// Token: 0x040007CD RID: 1997
		private Unit9 owner;
	}
}
