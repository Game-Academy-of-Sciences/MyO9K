using System;
using System.Collections.Generic;
using System.Linq;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Helpers.Range
{
	// Token: 0x02000093 RID: 147
	internal class RangeFactory : IDisposable
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x00004B85 File Offset: 0x00002D85
		public RangeFactory()
		{
			EntityManager9.AbilityAdded += this.OnAbilityAdded;
			EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00004BBA File Offset: 0x00002DBA
		public void Dispose()
		{
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001E9DC File Offset: 0x0001CBDC
		public IHasRangeIncrease GetRange(string name)
		{
			IHasRangeIncrease hasRangeIncrease;
			if (this.ranges.TryGetValue(name, out hasRangeIncrease) && hasRangeIncrease.IsValid)
			{
				return hasRangeIncrease;
			}
			return null;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001EA04 File Offset: 0x0001CC04
		private void OnAbilityAdded(Ability9 ability)
		{
			IHasRangeIncrease range;
			if ((range = (ability as IHasRangeIncrease)) != null)
			{
				if (range.IsRangeIncreasePermanent)
				{
					ability.Owner.Range(range, true);
					return;
				}
				if (this.ranges.ContainsKey(range.RangeModifierName))
				{
					return;
				}
				this.ranges.Add(range.RangeModifierName, range);
				IEnumerable<Unit9> units = EntityManager9.Units;
				Func<Unit9, bool> <>9__0;
				Func<Unit9, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((Unit9 x) => x.HasModifier(range.RangeModifierName)));
				}
				foreach (Unit9 unit in units.Where(predicate))
				{
					unit.Range(range, true);
				}
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001EAE8 File Offset: 0x0001CCE8
		private void OnAbilityRemoved(Ability9 ability)
		{
			IHasRangeIncrease range;
			if ((range = (ability as IHasRangeIncrease)) != null)
			{
				if (range.IsRangeIncreasePermanent)
				{
					ability.Owner.Range(range, false);
					return;
				}
				foreach (Unit9 unit in EntityManager9.Units)
				{
					unit.Range(range, false);
				}
				IHasRangeIncrease hasRangeIncrease = EntityManager9.Abilities.OfType<IHasRangeIncrease>().FirstOrDefault((IHasRangeIncrease x) => x.RangeModifierName == range.RangeModifierName);
				if (hasRangeIncrease != null)
				{
					this.ranges[hasRangeIncrease.RangeModifierName] = hasRangeIncrease;
					IEnumerable<Unit9> units = EntityManager9.Units;
					Func<Unit9, bool> <>9__1;
					Func<Unit9, bool> predicate;
					if ((predicate = <>9__1) == null)
					{
						predicate = (<>9__1 = ((Unit9 x) => x.HasModifier(range.RangeModifierName)));
					}
					using (IEnumerator<Unit9> enumerator = units.Where(predicate).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Unit9 unit2 = enumerator.Current;
							unit2.Range(range, true);
						}
						return;
					}
				}
				this.ranges.Remove(range.RangeModifierName);
			}
		}

		// Token: 0x04000202 RID: 514
		private readonly Dictionary<string, IHasRangeIncrease> ranges = new Dictionary<string, IHasRangeIncrease>();
	}
}
