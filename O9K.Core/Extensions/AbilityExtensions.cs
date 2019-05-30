using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Types;

namespace O9K.Core.Extensions
{
	// Token: 0x020000A0 RID: 160
	public static class AbilityExtensions
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public static bool CanBlock(this IHasDamageBlock block, DamageType damageType)
		{
			return (block.BlockDamageType & damageType) > DamageType.None;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00004DFD File Offset: 0x00002FFD
		public static bool IsDisable(this Ability9 ability)
		{
			return ability is IDisable;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00004E08 File Offset: 0x00003008
		public static bool IsDisarm(this IDisable disable, bool hex = true)
		{
			return (hex || !disable.IsHex()) && (disable.AppliesUnitState & UnitState.Disarmed) > (UnitState)0UL;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00004E24 File Offset: 0x00003024
		public static bool IsHex(this IDisable disable)
		{
			return (disable.AppliesUnitState & UnitState.Hexed) > (UnitState)0UL;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00004E34 File Offset: 0x00003034
		public static bool IsIncomingDamageAmplifier(this IHasDamageAmplify amplifier)
		{
			return (amplifier.AmplifiesDamage & AmplifiesDamage.Incoming) > AmplifiesDamage.None;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00004E41 File Offset: 0x00003041
		public static bool IsInvulnerability(this IDisable disable)
		{
			return (disable.AppliesUnitState & UnitState.Invulnerable) > (UnitState)0UL;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00004E54 File Offset: 0x00003054
		public static bool IsMagicalDamageAmplifier(this IHasDamageAmplify amplifier)
		{
			return (amplifier.AmplifierDamageType & DamageType.Magical) > DamageType.None;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00004E61 File Offset: 0x00003061
		public static bool IsMagicalDamageBlock(this IHasDamageBlock block)
		{
			return (block.BlockDamageType & DamageType.Magical) > DamageType.None;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00004E6E File Offset: 0x0000306E
		public static bool IsMagicImmunity(this IShield shield)
		{
			return (shield.AppliesUnitState & UnitState.MagicImmune) > (UnitState)0UL;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00004E81 File Offset: 0x00003081
		public static bool IsMute(this IDisable disable)
		{
			return (disable.AppliesUnitState & UnitState.Muted) > (UnitState)0UL;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00004E91 File Offset: 0x00003091
		public static bool IsOutgoingDamageAmplifier(this IHasDamageAmplify amplifier)
		{
			return (amplifier.AmplifiesDamage & AmplifiesDamage.Outgoing) > AmplifiesDamage.None;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00004E9E File Offset: 0x0000309E
		public static bool IsPhysicalDamageAmplifier(this IHasDamageAmplify amplifier)
		{
			return (amplifier.AmplifierDamageType & DamageType.Physical) > DamageType.None;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00004EAB File Offset: 0x000030AB
		public static bool IsPhysicalDamageBlock(this IHasDamageBlock block)
		{
			return (block.BlockDamageType & DamageType.Physical) > DamageType.None;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00004EB8 File Offset: 0x000030B8
		public static bool IsPureDamageAmplifier(this IHasDamageAmplify amplifier)
		{
			return (amplifier.AmplifierDamageType & DamageType.Pure) > DamageType.None;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00004EC5 File Offset: 0x000030C5
		public static bool IsPureDamageBlock(this IHasDamageBlock block)
		{
			return (block.BlockDamageType & DamageType.Pure) > DamageType.None;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00004ED2 File Offset: 0x000030D2
		public static bool IsRoot(this IDisable disable)
		{
			return (disable.AppliesUnitState & UnitState.Rooted) > (UnitState)0UL;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00004EE1 File Offset: 0x000030E1
		public static bool IsShied(this Ability9 ability)
		{
			return ability is IShield;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00004EEC File Offset: 0x000030EC
		public static bool IsSilence(this IDisable disable, bool hex = true)
		{
			return (hex || !disable.IsHex()) && (disable.AppliesUnitState & UnitState.Silenced) > (UnitState)0UL;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00004F08 File Offset: 0x00003108
		public static bool IsStun(this IDisable disable)
		{
			return (disable.AppliesUnitState & UnitState.Stunned) > (UnitState)0UL;
		}
	}
}
