using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Items;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Grimstroke.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Grimstroke.Units
{
	// Token: 0x02000168 RID: 360
	[UnitName("npc_dota_hero_grimstroke")]
	internal class Grimstroke : ControllableUnit
	{
		// Token: 0x0600076A RID: 1898 RVA: 0x00022894 File Offset: 0x00020A94
		public Grimstroke(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.grimstroke_dark_artistry,
					(ActiveAbility x) => this.stroke = new DebuffAbility(x)
				},
				{
					AbilityId.grimstroke_ink_creature,
					(ActiveAbility x) => this.embrace = new DisableAbility(x)
				},
				{
					AbilityId.grimstroke_spirit_walk,
					(ActiveAbility x) => this.ink = new InkSwell(x)
				},
				{
					AbilityId.grimstroke_soul_chain,
					(ActiveAbility x) => this.bind = new Soulbind(x)
				},
				{
					AbilityId.item_blink,
					(ActiveAbility x) => this.blink = new BlinkAbility(x)
				},
				{
					AbilityId.item_ethereal_blade,
					(ActiveAbility x) => this.ethereal = new EtherealBlade(x)
				},
				{
					AbilityId.item_sheepstick,
					(ActiveAbility x) => this.hex = new DisableAbility(x)
				},
				{
					AbilityId.item_shivas_guard,
					(ActiveAbility x) => this.shiva = new DebuffAbility(x)
				},
				{
					AbilityId.item_orchid,
					(ActiveAbility x) => this.orchid = new DisableAbility(x)
				},
				{
					AbilityId.item_bloodthorn,
					(ActiveAbility x) => this.bloodthorn = new Bloodthorn(x)
				},
				{
					AbilityId.item_nullifier,
					(ActiveAbility x) => this.nullifier = new Nullifier(x)
				},
				{
					AbilityId.item_rod_of_atos,
					(ActiveAbility x) => this.atos = new DisableAbility(x)
				},
				{
					AbilityId.item_veil_of_discord,
					(ActiveAbility x) => this.veil = new DebuffAbility(x)
				},
				{
					AbilityId.item_heavens_halberd,
					(ActiveAbility x) => this.halberd = new DisableAbility(x)
				},
				{
					AbilityId.item_dagon_5,
					(ActiveAbility x) => this.dagon = new GrimstrokeDagon(x)
				}
			};
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00022A04 File Offset: 0x00020C04
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.blink, 700f, 450f) || abilityHelper.UseAbility(this.bind, true) || abilityHelper.UseAbility(this.hex, true) || abilityHelper.UseAbility(this.bloodthorn, true) || abilityHelper.UseAbility(this.orchid, true) || abilityHelper.UseAbility(this.nullifier, true) || abilityHelper.UseAbility(this.halberd, true) || abilityHelper.UseAbility(this.atos, true) || abilityHelper.UseAbility(this.veil, true) || abilityHelper.UseAbility(this.dagon, true) || abilityHelper.UseAbility(this.shiva, true) || abilityHelper.UseAbility(this.embrace, true) || abilityHelper.UseAbility(this.ethereal, true) || abilityHelper.UseAbility(this.ink, true) || abilityHelper.UseAbility(this.stroke, true);
		}

		// Token: 0x0400040D RID: 1037
		private DisableAbility atos;

		// Token: 0x0400040E RID: 1038
		private Soulbind bind;

		// Token: 0x0400040F RID: 1039
		private BlinkAbility blink;

		// Token: 0x04000410 RID: 1040
		private DisableAbility bloodthorn;

		// Token: 0x04000411 RID: 1041
		private NukeAbility dagon;

		// Token: 0x04000412 RID: 1042
		private DisableAbility embrace;

		// Token: 0x04000413 RID: 1043
		private EtherealBlade ethereal;

		// Token: 0x04000414 RID: 1044
		private DisableAbility halberd;

		// Token: 0x04000415 RID: 1045
		private DisableAbility hex;

		// Token: 0x04000416 RID: 1046
		private ShieldAbility ink;

		// Token: 0x04000417 RID: 1047
		private Nullifier nullifier;

		// Token: 0x04000418 RID: 1048
		private DisableAbility orchid;

		// Token: 0x04000419 RID: 1049
		private DebuffAbility shiva;

		// Token: 0x0400041A RID: 1050
		private DebuffAbility stroke;

		// Token: 0x0400041B RID: 1051
		private DebuffAbility veil;
	}
}
