using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Dynamic.Abilities;
using O9K.AIO.Heroes.Dynamic.Abilities.Blinks;
using O9K.AIO.Heroes.Dynamic.Abilities.Buffs;
using O9K.AIO.Heroes.Dynamic.Abilities.Debuffs;
using O9K.AIO.Heroes.Dynamic.Abilities.Disables;
using O9K.AIO.Heroes.Dynamic.Abilities.Harasses;
using O9K.AIO.Heroes.Dynamic.Abilities.Nukes;
using O9K.AIO.Heroes.Dynamic.Abilities.Shields;
using O9K.AIO.Heroes.Dynamic.Abilities.Specials;
using O9K.AIO.Modes.Combo;
using O9K.AIO.Modes.MoveCombo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Dynamic.Units
{
	// Token: 0x02000197 RID: 407
	internal class DynamicUnit : ControllableUnit
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x00025B98 File Offset: 0x00023D98
		public DynamicUnit(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu, BaseHero baseHero) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			this.Blinks = new BlinkAbilityGroup(baseHero);
			this.Nukes = new NukeAbilityGroup(baseHero);
			this.Debuffs = new DebuffAbilityGroup(baseHero);
			this.Disables = new DisableAbilityGroup(baseHero);
			this.Buffs = new BuffAbilityGroup(baseHero);
			this.Harasses = new HarassAbilityGroup(baseHero);
			this.Shields = new ShieldAbilityGroup(baseHero);
			this.Specials = new SpecialAbilityGroup(baseHero);
			this.Blinks.Disables = this.Disables;
			this.Blinks.Specials = this.Specials;
			this.Disables.Specials = this.Specials;
			this.Buffs.Blinks = this.Blinks;
			this.Disables.Blinks = this.Blinks;
			this.Shields.Blinks = this.Blinks;
			this.groups.Add(this.Nukes);
			this.groups.Add(this.Debuffs);
			this.groups.Add(this.Blinks);
			this.groups.Add(this.Disables);
			this.groups.Add(this.Buffs);
			this.groups.Add(this.Harasses);
			this.groups.Add(this.Shields);
			this.groups.Add(this.Specials);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00006182 File Offset: 0x00004382
		public NukeAbilityGroup Nukes { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0000618A File Offset: 0x0000438A
		public DebuffAbilityGroup Debuffs { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00006192 File Offset: 0x00004392
		public BlinkAbilityGroup Blinks { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0000619A File Offset: 0x0000439A
		public DisableAbilityGroup Disables { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x000061A2 File Offset: 0x000043A2
		public BuffAbilityGroup Buffs { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x000061AA File Offset: 0x000043AA
		public HarassAbilityGroup Harasses { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x000061B2 File Offset: 0x000043B2
		public SpecialAbilityGroup Specials { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x000061BA File Offset: 0x000043BA
		public ShieldAbilityGroup Shields { get; }

		// Token: 0x06000844 RID: 2116 RVA: 0x00025DDC File Offset: 0x00023FDC
		public override void AddAbility(ActiveAbility ability, IEnumerable<ComboModeMenu> comboMenus, MoveComboModeMenu moveMenu)
		{
			if (this.ignoredAbilities.Contains(ability.Id))
			{
				return;
			}
			using (List<IOldAbilityGroup>.Enumerator enumerator = this.groups.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.AddAbility(ability))
					{
						foreach (ComboModeMenu comboModeMenu in comboMenus)
						{
							comboModeMenu.AddComboAbility(ability);
						}
					}
				}
			}
			base.AddMoveComboAbility(ability, moveMenu);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00025E80 File Offset: 0x00024080
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			Unit9 target = targetManager.Target;
			return this.Buffs.Use(target, comboModeMenu, new AbilityId[0]) || this.Shields.Use(target, comboModeMenu, new AbilityId[0]) || this.Debuffs.UseAmplifiers(target, comboModeMenu) || this.Disables.UseBlinkDisable(target, comboModeMenu) || this.Disables.Use(target, comboModeMenu, new AbilityId[0]) || this.Debuffs.Use(target, comboModeMenu, new AbilityId[0]) || this.Nukes.Use(target, comboModeMenu, new AbilityId[0]) || this.Harasses.Use(target, comboModeMenu, new AbilityId[0]) || this.Specials.Use(target, comboModeMenu, new AbilityId[0]) || this.Blinks.Use(target, comboModeMenu, new AbilityId[0]);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00025F70 File Offset: 0x00024170
		public override void RemoveAbility(ActiveAbility ability)
		{
			foreach (IOldAbilityGroup oldAbilityGroup in this.groups)
			{
				oldAbilityGroup.RemoveAbility(ability);
			}
		}

		// Token: 0x0400047F RID: 1151
		private readonly List<IOldAbilityGroup> groups = new List<IOldAbilityGroup>();

		// Token: 0x04000480 RID: 1152
		private readonly HashSet<AbilityId> ignoredAbilities = new HashSet<AbilityId>
		{
			AbilityId.ember_spirit_activate_fire_remnant,
			AbilityId.item_tpscroll,
			AbilityId.item_recipe_travel_boots,
			AbilityId.item_recipe_travel_boots_2,
			AbilityId.item_enchanted_mango,
			AbilityId.item_ghost,
			AbilityId.item_hand_of_midas,
			AbilityId.item_dust,
			AbilityId.item_mekansm,
			AbilityId.item_guardian_greaves,
			AbilityId.item_glimmer_cape,
			AbilityId.item_sphere,
			AbilityId.item_bfury,
			AbilityId.item_quelling_blade,
			AbilityId.item_shadow_amulet,
			AbilityId.item_magic_stick,
			AbilityId.item_magic_wand,
			AbilityId.item_bloodstone,
			AbilityId.item_branches
		};
	}
}
