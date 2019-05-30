using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Exceptions;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Helpers
{
	// Token: 0x02000088 RID: 136
	public class SpecialData
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x0001DDF4 File Offset: 0x0001BFF4
		public SpecialData(Entity talentOwner, AbilityId talentId)
		{
			try
			{
				this.talent = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
				{
					if (x.Id == talentId)
					{
						Entity owner = x.Owner;
						EntityHandle? entityHandle = (owner != null) ? new EntityHandle?(owner.Handle) : null;
						EntityHandle handle = talentOwner.Handle;
						return entityHandle != null && (entityHandle == null || entityHandle.GetValueOrDefault() == handle);
					}
					return false;
				});
				if (this.talent != null)
				{
					this.talentValue = this.talent.AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "value").Value;
					this.getDataFunc = new Func<uint, float>(this.GetTalentValue);
				}
				else
				{
					this.getDataFunc = ((uint _) => 1f);
				}
			}
			catch
			{
				this.getDataFunc = ((uint _) => 0f);
				BrokenAbilityException ex = new BrokenAbilityException(talentId.ToString());
				Ability ability = this.talent;
				if (ability != null && ability.IsValid)
				{
					ex.Data["Ability"] = new
					{
						Ability = this.talent.Name
					};
				}
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001DF44 File Offset: 0x0001C144
		public SpecialData(Ability ability, AbilityId talentId, string name)
		{
			try
			{
				AbilitySpecialData abilitySpecialData = ability.AbilitySpecialData.First((AbilitySpecialData x) => x.Name == name);
				Unit unit = ability.Owner as Unit;
				if (unit != null)
				{
					this.talent = unit.Spellbook.Spells.FirstOrDefault((Ability x) => x.Id == talentId);
				}
				if (this.talent != null)
				{
					this.talentValue = this.talent.AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "value").Value;
					this.getDataFunc = new Func<uint, float>(this.GetValueWithTalent);
				}
				else
				{
					this.getDataFunc = new Func<uint, float>(this.GetValueDefault);
				}
				this.value = new float[abilitySpecialData.Count];
				uint num = 0u;
				Func<AbilitySpecialData, bool> <>9__3;
				while ((ulong)num < (ulong)((long)this.value.Length))
				{
					float[] array = this.value;
					int num2 = (int)num;
					IEnumerable<AbilitySpecialData> abilitySpecialData2 = ability.AbilitySpecialData;
					Func<AbilitySpecialData, bool> predicate;
					if ((predicate = <>9__3) == null)
					{
						predicate = (<>9__3 = ((AbilitySpecialData x) => x.Name == name));
					}
					array[num2] = abilitySpecialData2.First(predicate).GetValue(num);
					num += 1u;
				}
			}
			catch
			{
				this.getDataFunc = ((uint _) => 1f);
				BrokenAbilityException ex = new BrokenAbilityException(ability.Name);
				if (ability.IsValid)
				{
					ex.Data["Ability"] = new
					{
						Ability = ability.Name,
						SpecialData = name
					};
				}
				Logger.Error(ex, null);
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001E110 File Offset: 0x0001C310
		public SpecialData(Ability ability, string name)
		{
			try
			{
				AbilitySpecialData abilitySpecialData = ability.AbilitySpecialData.First((AbilitySpecialData x) => x.Name == name);
				AbilityId talentId = abilitySpecialData.SpecialBonusAbility;
				if (talentId != AbilityId.ability_base)
				{
					Unit unit = ability.Owner as Unit;
					if (unit != null)
					{
						this.talent = unit.Spellbook.Spells.FirstOrDefault((Ability x) => x.Id == talentId);
					}
				}
				if (this.talent != null)
				{
					this.talentValue = this.talent.AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "value").Value;
					this.getDataFunc = new Func<uint, float>(this.GetValueWithTalent);
				}
				else
				{
					this.getDataFunc = new Func<uint, float>(this.GetValueDefault);
				}
				this.value = new float[abilitySpecialData.Count];
				uint num = 0u;
				Func<AbilitySpecialData, bool> <>9__3;
				while ((ulong)num < (ulong)((long)this.value.Length))
				{
					float[] array = this.value;
					int num2 = (int)num;
					IEnumerable<AbilitySpecialData> abilitySpecialData2 = ability.AbilitySpecialData;
					Func<AbilitySpecialData, bool> predicate;
					if ((predicate = <>9__3) == null)
					{
						predicate = (<>9__3 = ((AbilitySpecialData x) => x.Name == name));
					}
					array[num2] = abilitySpecialData2.First(predicate).GetValue(num);
					num += 1u;
				}
			}
			catch
			{
				this.getDataFunc = ((uint _) => 1f);
				BrokenAbilityException ex = new BrokenAbilityException(ability.Name);
				if (ability.IsValid)
				{
					ex.Data["Ability"] = new
					{
						Ability = ability.Name,
						SpecialData = name
					};
				}
				Logger.Error(ex, null);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001E2F4 File Offset: 0x0001C4F4
		public SpecialData(Ability ability, Func<uint, int> baseData)
		{
			try
			{
				this.value = new float[Math.Max(ability.MaximumLevel, 1)];
				uint num = 0u;
				while ((ulong)num < (ulong)((long)this.value.Length))
				{
					this.value[(int)num] = (float)baseData(num);
					num += 1u;
				}
				this.getDataFunc = new Func<uint, float>(this.GetValueDefault);
			}
			catch
			{
				this.getDataFunc = ((uint _) => 1f);
				BrokenAbilityException ex = new BrokenAbilityException(ability.Name);
				if (ability.IsValid)
				{
					ex.Data["Ability"] = new
					{
						Ability = ability.Name,
						BaseSpecialData = baseData.Method.Name
					};
				}
				Logger.Error(ex, null);
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001E3D0 File Offset: 0x0001C5D0
		public SpecialData(string key)
		{
			try
			{
				string[] array = Game.FindKeyValues(key, KeyValueSource.Ability).StringValue.Split(new char[]
				{
					' '
				});
				this.value = new float[array.Length];
				uint num = 0u;
				while ((ulong)num < (ulong)((long)this.value.Length))
				{
					int num2;
					if (int.TryParse(array[(int)num], out num2))
					{
						this.value[(int)num] = (float)num2;
					}
					num += 1u;
				}
				this.getDataFunc = new Func<uint, float>(this.GetValueDefault);
			}
			catch (KeyValuesNotFoundException)
			{
				this.getDataFunc = ((uint _) => 0f);
			}
			catch
			{
				this.getDataFunc = ((uint _) => 0f);
				Logger.Error(new BrokenAbilityException(key), null);
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001E4C8 File Offset: 0x0001C6C8
		public SpecialData(Ability ability, Func<uint, float> baseData)
		{
			try
			{
				this.value = new float[Math.Max(ability.MaximumLevel, 1)];
				uint num = 0u;
				while ((ulong)num < (ulong)((long)this.value.Length))
				{
					this.value[(int)num] = baseData(num);
					num += 1u;
				}
				this.getDataFunc = new Func<uint, float>(this.GetValueDefault);
			}
			catch
			{
				this.getDataFunc = ((uint _) => 1f);
				BrokenAbilityException ex = new BrokenAbilityException(ability.Name);
				if (ability.IsValid)
				{
					ex.Data["Ability"] = new
					{
						Ability = ability.Name,
						BaseSpecialData = baseData.Method.Name
					};
				}
				Logger.Error(ex, null);
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000491C File Offset: 0x00002B1C
		public float GetTalentValue(uint level)
		{
			if (this.talent.Level == 0u)
			{
				return 0f;
			}
			return this.talentValue;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000493A File Offset: 0x00002B3A
		public float GetValue(uint level)
		{
			return this.getDataFunc(level);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00004948 File Offset: 0x00002B48
		public float GetValueDefault(uint level)
		{
			if (level == 0u)
			{
				return 0f;
			}
			return this.value[(int)(checked((IntPtr)(unchecked(Math.Min((long)((ulong)level), (long)this.value.Length) - 1L))))];
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001E5A4 File Offset: 0x0001C7A4
		public float GetValueWithTalent(uint level)
		{
			if (level == 0u)
			{
				return 0f;
			}
			float num = this.value[(int)(checked((IntPtr)(unchecked(Math.Min((long)((ulong)level), (long)this.value.Length) - 1L))))];
			Ability ability = this.talent;
			if (ability != null && ability.Level > 0u)
			{
				num += this.talentValue;
			}
			return num;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001E5F8 File Offset: 0x0001C7F8
		public float GetValueWithTalentMultiply(uint level)
		{
			if (level == 0u)
			{
				return 0f;
			}
			float num = this.value[(int)(checked((IntPtr)(unchecked(Math.Min((long)((ulong)level), (long)this.value.Length) - 1L))))];
			Ability ability = this.talent;
			if (ability != null && ability.Level > 0u)
			{
				num *= this.talentValue / 100f + 1f;
			}
			return num;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001E658 File Offset: 0x0001C858
		public float GetValueWithTalentMultiplySimple(uint level)
		{
			if (level == 0u)
			{
				return 0f;
			}
			float num = this.value[(int)(checked((IntPtr)(unchecked(Math.Min((long)((ulong)level), (long)this.value.Length) - 1L))))];
			Ability ability = this.talent;
			if (ability != null && ability.Level > 0u)
			{
				num *= this.talentValue;
			}
			return num;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001E6AC File Offset: 0x0001C8AC
		public float GetValueWithTalentSubtract(uint level)
		{
			if (level == 0u)
			{
				return 0f;
			}
			float num = this.value[(int)(checked((IntPtr)(unchecked(Math.Min((long)((ulong)level), (long)this.value.Length) - 1L))))];
			Ability ability = this.talent;
			if (ability != null && ability.Level > 0u)
			{
				num -= this.talentValue;
			}
			return num;
		}

		// Token: 0x040001E2 RID: 482
		private readonly Func<uint, float> getDataFunc;

		// Token: 0x040001E3 RID: 483
		private readonly Ability talent;

		// Token: 0x040001E4 RID: 484
		private readonly float talentValue;

		// Token: 0x040001E5 RID: 485
		private readonly float[] value;
	}
}
