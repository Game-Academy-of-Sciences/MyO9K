using System;
using System.Collections.Generic;
using System.Linq;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Helpers.Damage
{
	// Token: 0x0200009B RID: 155
	internal class DamageFactory : IDisposable
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x0001EDC0 File Offset: 0x0001CFC0
		public DamageFactory()
		{
			EntityManager9.AbilityAdded += this.OnAbilityAdded;
			EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00004CFA File Offset: 0x00002EFA
		public void Dispose()
		{
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001EE24 File Offset: 0x0001D024
		public IHasDamageAmplify GetAmplifier(string name)
		{
			IHasDamageAmplify hasDamageAmplify;
			if (this.amplifiers.TryGetValue(name, out hasDamageAmplify) && hasDamageAmplify.IsValid)
			{
				return hasDamageAmplify;
			}
			return null;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001EE4C File Offset: 0x0001D04C
		public IHasDamageBlock GetBlocker(string name)
		{
			IHasDamageBlock hasDamageBlock;
			if (this.blockers.TryGetValue(name, out hasDamageBlock) && hasDamageBlock.IsValid)
			{
				return hasDamageBlock;
			}
			return null;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001EE74 File Offset: 0x0001D074
		public IAppliesImmobility GetDisable(string name)
		{
			IAppliesImmobility appliesImmobility;
			if (this.disablers.TryGetValue(name, out appliesImmobility) && appliesImmobility.IsValid)
			{
				return appliesImmobility;
			}
			return null;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001EE9C File Offset: 0x0001D09C
		public IHasPassiveDamageIncrease GetPassive(string name)
		{
			IHasPassiveDamageIncrease hasPassiveDamageIncrease;
			if (this.passives.TryGetValue(name, out hasPassiveDamageIncrease) && hasPassiveDamageIncrease.IsValid)
			{
				return hasPassiveDamageIncrease;
			}
			return null;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001EEC4 File Offset: 0x0001D0C4
		private void OnAbilityAdded(Ability9 ability)
		{
			IHasDamageAmplify amplifier;
			if ((amplifier = (ability as IHasDamageAmplify)) != null)
			{
				if (amplifier.IsAmplifierPermanent)
				{
					ability.Owner.Amplifier(amplifier, true);
					return;
				}
				if (amplifier.IsAmplifierAddedToStats || this.amplifiers.ContainsKey(amplifier.AmplifierModifierName))
				{
					return;
				}
				this.amplifiers.Add(amplifier.AmplifierModifierName, amplifier);
				IEnumerable<Unit9> units = EntityManager9.Units;
				Func<Unit9, bool> <>9__0;
				Func<Unit9, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((Unit9 x) => x.HasModifier(amplifier.AmplifierModifierName)));
				}
				foreach (Unit9 unit in units.Where(predicate))
				{
					unit.Amplifier(amplifier, true);
				}
			}
			IHasPassiveDamageIncrease passive;
			if ((passive = (ability as IHasPassiveDamageIncrease)) != null)
			{
				if (passive.IsPassiveDamagePermanent)
				{
					ability.Owner.Passive(passive, true);
					return;
				}
				if (this.passives.ContainsKey(passive.PassiveDamageModifierName))
				{
					return;
				}
				this.passives.Add(passive.PassiveDamageModifierName, passive);
				IEnumerable<Unit9> units2 = EntityManager9.Units;
				Func<Unit9, bool> <>9__1;
				Func<Unit9, bool> predicate2;
				if ((predicate2 = <>9__1) == null)
				{
					predicate2 = (<>9__1 = ((Unit9 x) => x.HasModifier(passive.PassiveDamageModifierName)));
				}
				foreach (Unit9 unit2 in units2.Where(predicate2))
				{
					unit2.Passive(passive, true);
				}
			}
			IHasDamageBlock block;
			if ((block = (ability as IHasDamageBlock)) != null)
			{
				if (block.IsDamageBlockPermanent)
				{
					ability.Owner.Blocker(block, true);
					return;
				}
				if (this.blockers.ContainsKey(block.BlockModifierName))
				{
					return;
				}
				this.blockers.Add(block.BlockModifierName, block);
				IEnumerable<Unit9> units3 = EntityManager9.Units;
				Func<Unit9, bool> <>9__2;
				Func<Unit9, bool> predicate3;
				if ((predicate3 = <>9__2) == null)
				{
					predicate3 = (<>9__2 = ((Unit9 x) => x.HasModifier(block.BlockModifierName)));
				}
				foreach (Unit9 unit3 in units3.Where(predicate3))
				{
					unit3.Blocker(block, true);
				}
			}
			IAppliesImmobility appliesImmobility;
			if ((appliesImmobility = (ability as IAppliesImmobility)) != null)
			{
				if (this.disablers.ContainsKey(appliesImmobility.ImmobilityModifierName))
				{
					return;
				}
				this.disablers.Add(appliesImmobility.ImmobilityModifierName, appliesImmobility);
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001F188 File Offset: 0x0001D388
		private void OnAbilityRemoved(Ability9 ability)
		{
			IHasDamageAmplify amplifier;
			if ((amplifier = (ability as IHasDamageAmplify)) != null)
			{
				if (amplifier.IsAmplifierPermanent)
				{
					ability.Owner.Amplifier(amplifier, false);
					return;
				}
				if (amplifier.IsAmplifierAddedToStats)
				{
					return;
				}
				foreach (Unit9 unit in EntityManager9.Units)
				{
					unit.Amplifier(amplifier, false);
				}
				IHasDamageAmplify hasDamageAmplify = EntityManager9.Abilities.OfType<IHasDamageAmplify>().FirstOrDefault((IHasDamageAmplify x) => x.AmplifierModifierName == amplifier.AmplifierModifierName);
				if (hasDamageAmplify != null)
				{
					this.amplifiers[hasDamageAmplify.AmplifierModifierName] = hasDamageAmplify;
					IEnumerable<Unit9> units = EntityManager9.Units;
					Func<Unit9, bool> <>9__1;
					Func<Unit9, bool> predicate;
					if ((predicate = <>9__1) == null)
					{
						predicate = (<>9__1 = ((Unit9 x) => x.HasModifier(amplifier.AmplifierModifierName)));
					}
					using (IEnumerator<Unit9> enumerator = units.Where(predicate).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Unit9 unit2 = enumerator.Current;
							unit2.Amplifier(hasDamageAmplify, true);
						}
						goto IL_117;
					}
				}
				this.amplifiers.Remove(amplifier.AmplifierModifierName);
			}
			IL_117:
			IHasPassiveDamageIncrease passive;
			if ((passive = (ability as IHasPassiveDamageIncrease)) != null)
			{
				if (passive.IsPassiveDamagePermanent)
				{
					ability.Owner.Passive(passive, false);
					return;
				}
				foreach (Unit9 unit3 in EntityManager9.Units)
				{
					unit3.Passive(passive, false);
				}
				IHasPassiveDamageIncrease hasPassiveDamageIncrease = EntityManager9.Abilities.OfType<IHasPassiveDamageIncrease>().FirstOrDefault((IHasPassiveDamageIncrease x) => x.PassiveDamageModifierName == passive.PassiveDamageModifierName);
				if (hasPassiveDamageIncrease != null)
				{
					this.passives[hasPassiveDamageIncrease.PassiveDamageModifierName] = hasPassiveDamageIncrease;
					IEnumerable<Unit9> units2 = EntityManager9.Units;
					Func<Unit9, bool> <>9__3;
					Func<Unit9, bool> predicate2;
					if ((predicate2 = <>9__3) == null)
					{
						predicate2 = (<>9__3 = ((Unit9 x) => x.HasModifier(passive.PassiveDamageModifierName)));
					}
					using (IEnumerator<Unit9> enumerator = units2.Where(predicate2).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Unit9 unit4 = enumerator.Current;
							unit4.Passive(hasPassiveDamageIncrease, true);
						}
						goto IL_221;
					}
				}
				this.passives.Remove(passive.PassiveDamageModifierName);
			}
			IL_221:
			IHasDamageBlock block;
			if ((block = (ability as IHasDamageBlock)) != null)
			{
				if (block.IsDamageBlockPermanent)
				{
					ability.Owner.Blocker(block, false);
					return;
				}
				foreach (Unit9 unit5 in EntityManager9.Units)
				{
					unit5.Blocker(block, false);
				}
				IHasDamageBlock hasDamageBlock = EntityManager9.Abilities.OfType<IHasDamageBlock>().FirstOrDefault((IHasDamageBlock x) => x.BlockModifierName == block.BlockModifierName);
				if (hasDamageBlock != null)
				{
					this.blockers[hasDamageBlock.BlockModifierName] = hasDamageBlock;
					IEnumerable<Unit9> units3 = EntityManager9.Units;
					Func<Unit9, bool> <>9__5;
					Func<Unit9, bool> predicate3;
					if ((predicate3 = <>9__5) == null)
					{
						predicate3 = (<>9__5 = ((Unit9 x) => x.HasModifier(block.BlockModifierName)));
					}
					using (IEnumerator<Unit9> enumerator = units3.Where(predicate3).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Unit9 unit6 = enumerator.Current;
							unit6.Blocker(block, true);
						}
						goto IL_32F;
					}
				}
				this.blockers.Remove(block.BlockModifierName);
			}
			IL_32F:
			IAppliesImmobility disable;
			if ((disable = (ability as IAppliesImmobility)) != null)
			{
				IAppliesImmobility appliesImmobility = EntityManager9.Abilities.OfType<IAppliesImmobility>().FirstOrDefault((IAppliesImmobility x) => x.ImmobilityModifierName == disable.ImmobilityModifierName);
				if (appliesImmobility == null)
				{
					this.disablers.Remove(disable.ImmobilityModifierName);
					return;
				}
				this.disablers[disable.ImmobilityModifierName] = appliesImmobility;
			}
		}

		// Token: 0x04000212 RID: 530
		private readonly Dictionary<string, IHasDamageAmplify> amplifiers = new Dictionary<string, IHasDamageAmplify>();

		// Token: 0x04000213 RID: 531
		private readonly Dictionary<string, IHasDamageBlock> blockers = new Dictionary<string, IHasDamageBlock>();

		// Token: 0x04000214 RID: 532
		private readonly Dictionary<string, IAppliesImmobility> disablers = new Dictionary<string, IAppliesImmobility>();

		// Token: 0x04000215 RID: 533
		private readonly Dictionary<string, IHasPassiveDamageIncrease> passives = new Dictionary<string, IHasPassiveDamageIncrease>();
	}
}
