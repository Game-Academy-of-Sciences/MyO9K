using System;
using Ensage;
using O9K.Core.Entities.Units;

namespace O9K.Core.Extensions
{
	// Token: 0x020000A1 RID: 161
	public static class DamageExtensions
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x0001F59C File Offset: 0x0001D79C
		public static float GetMeleeDamageMultiplier(Unit9 source, Unit9 target)
		{
			switch (target.ArmorType)
			{
			case ArmorType.Structure:
				switch (source.AttackType)
				{
				case AttackDamageType.Hero:
					return 0.5f;
				case AttackDamageType.Basic:
					return 0.7f;
				case AttackDamageType.Pierce:
					return 0.35f;
				case AttackDamageType.Siege:
					return 2.5f;
				}
				break;
			case ArmorType.Hero:
				switch (source.AttackType)
				{
				case AttackDamageType.Basic:
					return 0.75f;
				case AttackDamageType.Pierce:
					return 0.5f;
				case AttackDamageType.Siege:
					return 0.85f;
				}
				break;
			case ArmorType.Basic:
			{
				AttackDamageType attackType = source.AttackType;
				if (attackType == AttackDamageType.Pierce)
				{
					return 1.5f;
				}
				break;
			}
			}
			return 1f;
		}
	}
}
