using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Blink;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Cleave;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.FireRemnant;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Item;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Maelstorm;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Poof;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.RemoteMines;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Smoke;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.ThinkerAbility;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Wards;
using SharpDX;

namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data
{
	// Token: 0x02000076 RID: 118
	internal class AbilityData
	{
		// Token: 0x0600028D RID: 653 RVA: 0x000159C8 File Offset: 0x00013BC8
		public AbilityData()
		{
			Dictionary<string, AbilityFullData> dictionary = new Dictionary<string, AbilityFullData>();
			string key = "npc_dota_observer_wards";
			WardAbilityData wardAbilityData = new WardAbilityData();
			wardAbilityData.AbilityId = AbilityId.item_ward_observer;
			wardAbilityData.UnitName = "npc_dota_observer_wards";
			wardAbilityData.ShowRange = true;
			wardAbilityData.Range = Ability.GetAbilityDataById(AbilityId.item_ward_observer).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "vision_range").Value + 150f;
			wardAbilityData.RangeColor = new Vector3(255f, 255f, 0f);
			wardAbilityData.Duration = Ability.GetAbilityDataById(AbilityId.item_ward_observer).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "lifetime").Value;
			dictionary.Add(key, wardAbilityData);
			string key2 = "npc_dota_sentry_wards";
			WardAbilityData wardAbilityData2 = new WardAbilityData();
			wardAbilityData2.AbilityId = AbilityId.item_ward_sentry;
			wardAbilityData2.UnitName = "npc_dota_sentry_wards";
			wardAbilityData2.ShowRange = true;
			wardAbilityData2.Range = Ability.GetAbilityDataById(AbilityId.item_ward_sentry).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "true_sight_range").Value + 150f;
			wardAbilityData2.RangeColor = new Vector3(30f, 100f, 255f);
			wardAbilityData2.Duration = Ability.GetAbilityDataById(AbilityId.item_ward_sentry).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "lifetime").Value;
			dictionary.Add(key2, wardAbilityData2);
			string key3 = "npc_dota_pugna_nether_ward_1";
			AbilityFullData abilityFullData = new AbilityFullData();
			abilityFullData.AbilityId = AbilityId.pugna_nether_ward;
			abilityFullData.ShowRange = true;
			abilityFullData.Range = Ability.GetAbilityDataById(AbilityId.pugna_nether_ward).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "radius").Value + 150f;
			abilityFullData.RangeColor = new Vector3(124f, 252f, 0f);
			abilityFullData.Duration = Ability.GetAbilityDataById(AbilityId.pugna_nether_ward).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "ward_duration_tooltip").Value;
			dictionary.Add(key3, abilityFullData);
			string key4 = "npc_dota_pugna_nether_ward_2";
			AbilityFullData abilityFullData2 = new AbilityFullData();
			abilityFullData2.AbilityId = AbilityId.pugna_nether_ward;
			abilityFullData2.ShowRange = true;
			abilityFullData2.Range = Ability.GetAbilityDataById(AbilityId.pugna_nether_ward).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "radius").Value + 150f;
			abilityFullData2.RangeColor = new Vector3(124f, 252f, 0f);
			abilityFullData2.Duration = Ability.GetAbilityDataById(AbilityId.pugna_nether_ward).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "ward_duration_tooltip").Value;
			dictionary.Add(key4, abilityFullData2);
			string key5 = "npc_dota_pugna_nether_ward_3";
			AbilityFullData abilityFullData3 = new AbilityFullData();
			abilityFullData3.AbilityId = AbilityId.pugna_nether_ward;
			abilityFullData3.ShowRange = true;
			abilityFullData3.Range = Ability.GetAbilityDataById(AbilityId.pugna_nether_ward).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "radius").Value + 150f;
			abilityFullData3.RangeColor = new Vector3(124f, 252f, 0f);
			abilityFullData3.Duration = Ability.GetAbilityDataById(AbilityId.pugna_nether_ward).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "ward_duration_tooltip").Value;
			dictionary.Add(key5, abilityFullData3);
			string key6 = "npc_dota_pugna_nether_ward_4";
			AbilityFullData abilityFullData4 = new AbilityFullData();
			abilityFullData4.AbilityId = AbilityId.pugna_nether_ward;
			abilityFullData4.ShowRange = true;
			abilityFullData4.Range = Ability.GetAbilityDataById(AbilityId.pugna_nether_ward).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "radius").Value + 150f;
			abilityFullData4.RangeColor = new Vector3(124f, 252f, 0f);
			abilityFullData4.Duration = Ability.GetAbilityDataById(AbilityId.pugna_nether_ward).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "ward_duration_tooltip").Value;
			dictionary.Add(key6, abilityFullData4);
			string key7 = "npc_dota_venomancer_plague_ward_1";
			AbilityFullData abilityFullData5 = new AbilityFullData();
			abilityFullData5.AbilityId = AbilityId.venomancer_plague_ward;
			abilityFullData5.Duration = Ability.GetAbilityDataById(AbilityId.venomancer_plague_ward).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "duration").Value;
			dictionary.Add(key7, abilityFullData5);
			string key8 = "npc_dota_venomancer_plague_ward_2";
			AbilityFullData abilityFullData6 = new AbilityFullData();
			abilityFullData6.AbilityId = AbilityId.venomancer_plague_ward;
			abilityFullData6.Duration = Ability.GetAbilityDataById(AbilityId.venomancer_plague_ward).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "duration").Value;
			dictionary.Add(key8, abilityFullData6);
			string key9 = "npc_dota_venomancer_plague_ward_3";
			AbilityFullData abilityFullData7 = new AbilityFullData();
			abilityFullData7.AbilityId = AbilityId.venomancer_plague_ward;
			abilityFullData7.Duration = Ability.GetAbilityDataById(AbilityId.venomancer_plague_ward).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "duration").Value;
			dictionary.Add(key9, abilityFullData7);
			string key10 = "npc_dota_venomancer_plague_ward_4";
			AbilityFullData abilityFullData8 = new AbilityFullData();
			abilityFullData8.AbilityId = AbilityId.venomancer_plague_ward;
			abilityFullData8.Duration = Ability.GetAbilityDataById(AbilityId.venomancer_plague_ward).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "duration").Value;
			dictionary.Add(key10, abilityFullData8);
			string key11 = "npc_dota_unit_tombstone1";
			AbilityFullData abilityFullData9 = new AbilityFullData();
			abilityFullData9.AbilityId = AbilityId.undying_tombstone;
			abilityFullData9.Duration = Ability.GetAbilityDataById(AbilityId.undying_tombstone).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "duration").Value;
			dictionary.Add(key11, abilityFullData9);
			string key12 = "npc_dota_unit_tombstone2";
			AbilityFullData abilityFullData10 = new AbilityFullData();
			abilityFullData10.AbilityId = AbilityId.undying_tombstone;
			abilityFullData10.Duration = Ability.GetAbilityDataById(AbilityId.undying_tombstone).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "duration").Value;
			dictionary.Add(key12, abilityFullData10);
			string key13 = "npc_dota_unit_tombstone3";
			AbilityFullData abilityFullData11 = new AbilityFullData();
			abilityFullData11.AbilityId = AbilityId.undying_tombstone;
			abilityFullData11.Duration = Ability.GetAbilityDataById(AbilityId.undying_tombstone).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "duration").Value;
			dictionary.Add(key13, abilityFullData11);
			string key14 = "npc_dota_unit_tombstone4";
			AbilityFullData abilityFullData12 = new AbilityFullData();
			abilityFullData12.AbilityId = AbilityId.undying_tombstone;
			abilityFullData12.Duration = Ability.GetAbilityDataById(AbilityId.undying_tombstone).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "duration").Value;
			dictionary.Add(key14, abilityFullData12);
			string key15 = "npc_dota_techies_land_mine";
			AbilityFullData abilityFullData13 = new AbilityFullData();
			abilityFullData13.AbilityId = AbilityId.techies_land_mines;
			abilityFullData13.Duration = 1E+08f;
			abilityFullData13.TimeToShow = 0f;
			abilityFullData13.ShowTimer = false;
			abilityFullData13.ShowRange = true;
			abilityFullData13.Range = Ability.GetAbilityDataById(AbilityId.techies_land_mines).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "radius").Value + 50f;
			abilityFullData13.RangeColor = new Vector3(255f, 0f, 0f);
			dictionary.Add(key15, abilityFullData13);
			string key16 = "npc_dota_techies_stasis_trap";
			AbilityFullData abilityFullData14 = new AbilityFullData();
			abilityFullData14.AbilityId = AbilityId.techies_stasis_trap;
			abilityFullData14.Duration = 1E+08f;
			abilityFullData14.TimeToShow = 0f;
			abilityFullData14.ShowTimer = false;
			abilityFullData14.ShowRange = true;
			abilityFullData14.Range = Ability.GetAbilityDataById(AbilityId.techies_stasis_trap).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "activation_radius").Value + 50f;
			abilityFullData14.RangeColor = new Vector3(65f, 105f, 225f);
			dictionary.Add(key16, abilityFullData14);
			string key17 = "npc_dota_techies_remote_mine";
			RemoteMinesAbilityData remoteMinesAbilityData = new RemoteMinesAbilityData();
			remoteMinesAbilityData.AbilityId = AbilityId.techies_remote_mines;
			remoteMinesAbilityData.Duration = Ability.GetAbilityDataById(AbilityId.techies_remote_mines).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "duration").Value;
			remoteMinesAbilityData.ShowRange = true;
			remoteMinesAbilityData.Range = Ability.GetAbilityDataById(AbilityId.techies_remote_mines).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "radius").Value + 50f;
			remoteMinesAbilityData.RangeColor = new Vector3(0f, 255f, 0f);
			dictionary.Add(key17, remoteMinesAbilityData);
			string key18 = "npc_dota_clinkz_skeleton_archer";
			AbilityFullData abilityFullData15 = new AbilityFullData();
			abilityFullData15.AbilityId = AbilityId.clinkz_burning_army;
			abilityFullData15.TimeToShow = 0f;
			abilityFullData15.Duration = Ability.GetAbilityDataById(AbilityId.clinkz_burning_army).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "duration").Value;
			dictionary.Add(key18, abilityFullData15);
			string key19 = "npc_dota_zeus_cloud";
			AbilityFullData abilityFullData16 = new AbilityFullData();
			abilityFullData16.AbilityId = AbilityId.zuus_cloud;
			abilityFullData16.TimeToShow = 0f;
			abilityFullData16.Duration = Ability.GetAbilityDataById(AbilityId.zuus_cloud).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "cloud_duration").Value;
			dictionary.Add(key19, abilityFullData16);
			dictionary.Add("npc_dota_templar_assassin_psionic_trap", new AbilityFullData
			{
				AbilityId = AbilityId.templar_assassin_psionic_trap,
				Duration = 1E+08f,
				TimeToShow = 0f,
				ShowTimer = false
			});
			dictionary.Add("npc_dota_invoker_forged_spirit", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_forge_spirit,
				TimeToShow = 5f
			});
			dictionary.Add("npc_dota_juggernaut_healing_ward", new AbilityFullData
			{
				AbilityId = AbilityId.juggernaut_healing_ward,
				TimeToShow = 5f
			});
			dictionary.Add("npc_dota_stormspirit_remnant", new AbilityFullData
			{
				AbilityId = AbilityId.storm_spirit_static_remnant,
				TimeToShow = 5f
			});
			dictionary.Add("npc_dota_necronomicon_warrior_1", new AbilityFullData
			{
				AbilityId = AbilityId.item_necronomicon_3,
				TimeToShow = 5f
			});
			dictionary.Add("npc_dota_necronomicon_warrior_2", new AbilityFullData
			{
				AbilityId = AbilityId.item_necronomicon_3,
				TimeToShow = 5f
			});
			dictionary.Add("npc_dota_necronomicon_warrior_3", new AbilityFullData
			{
				AbilityId = AbilityId.item_necronomicon_3,
				TimeToShow = 5f
			});
			dictionary.Add("npc_dota_thinker", new ThinkerUnitAbilityData
			{
				TimeToShow = 5f
			});
			this.Units = dictionary;
			Dictionary<string, AbilityFullData> dictionary2 = new Dictionary<string, AbilityFullData>();
			dictionary2.Add("particles/units/heroes/hero_abaddon/abaddon_aphotic_shield_hit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.abaddon_aphotic_shield
			});
			dictionary2.Add("particles/units/heroes/hero_abaddon/abaddon_curse_counter_stack.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.abaddon_frostmourne,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_abaddon/abaddon_borrowed_time_heal.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.abaddon_borrowed_time
			});
			dictionary2.Add("particles/units/heroes/hero_alchemist/alchemist_acid_spray_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.alchemist_acid_spray
			});
			dictionary2.Add("particles/units/heroes/hero_alchemist/alchemist_unstableconc_bottles.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.alchemist_unstable_concoction,
				ShowNotification = true
			});
			dictionary2.Add("particles/units/heroes/hero_alchemist/alchemist_unstable_concoction_explosion.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.alchemist_unstable_concoction_throw
			});
			dictionary2.Add("particles/units/heroes/hero_alchemist/alchemist_chemichalrage_effect.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.alchemist_chemical_rage
			});
			dictionary2.Add("particles/units/heroes/hero_ancient_apparition/ancient_apparition_cold_feet_marker.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.ancient_apparition_cold_feet,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_ancient_apparition/ancient_ice_vortex.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.ancient_apparition_ice_vortex
			});
			dictionary2.Add("particles/econ/items/ancient_apparition/ancient_apparation_ti8/ancient_ice_vortex_ti8.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.ancient_apparition_ice_vortex
			});
			dictionary2.Add("particles/units/heroes/hero_antimage/antimage_blade_hit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.antimage_mana_break,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/antimage/antimage_weapon_basher_ti5/antimage_blade_hit_basher_ti_5.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.antimage_mana_break,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/antimage/antimage_weapon_basher_ti5_gold/antimage_blade_hit_basher_ti_5_gold.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.antimage_mana_break,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_antimage/antimage_blink_start.vpcf", new BlinkAbilityData
			{
				AbilityId = AbilityId.antimage_blink
			});
			dictionary2.Add("particles/econ/items/antimage/antimage_ti7_golden/antimage_blink_start_ti7_golden.vpcf", new BlinkAbilityData
			{
				AbilityId = AbilityId.antimage_blink
			});
			dictionary2.Add("particles/econ/items/antimage/antimage_ti7/antimage_blink_start_ti7.vpcf", new BlinkAbilityData
			{
				AbilityId = AbilityId.antimage_blink
			});
			dictionary2.Add("particles/units/heroes/hero_arc_warden/arc_warden_magnetic_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.arc_warden_magnetic_field
			});
			dictionary2.Add("particles/units/heroes/hero_arc_warden/arc_warden_wraith_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.arc_warden_spark_wraith
			});
			dictionary2.Add("particles/units/heroes/hero_arc_warden/arc_warden_tempest_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.arc_warden_tempest_double
			});
			dictionary2.Add("particles/units/heroes/hero_arc_warden/arc_warden_flux_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.arc_warden_flux
			});
			dictionary2.Add("particles/units/heroes/hero_axe/axe_beserkers_call_owner.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.axe_berserkers_call,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/econ/items/axe/axe_helm_shoutmask/axe_beserkers_call_owner_shoutmask.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.axe_berserkers_call,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_axe/axe_battle_hunger_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.axe_battle_hunger
			});
			dictionary2.Add("particles/econ/items/axe/axe_cinder/axe_cinder_battle_hunger_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.axe_battle_hunger
			});
			dictionary2.Add("particles/units/heroes/hero_axe/axe_culling_blade.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.axe_culling_blade,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_axe/axe_culling_blade_boost.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.axe_culling_blade
			});
			dictionary2.Add("particles/units/heroes/hero_bane/bane_sap.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.bane_brain_sap
			});
			dictionary2.Add("particles/units/heroes/hero_batrider/batrider_flamebreak.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.batrider_flamebreak
			});
			dictionary2.Add("particles/units/heroes/hero_batrider/batrider_stickynapalm_impact.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.batrider_sticky_napalm,
				ControlPoint = 2u
			});
			dictionary2.Add("particles/units/heroes/hero_batrider/batrider_firefly_ember.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.batrider_firefly
			});
			dictionary2.Add("particles/units/heroes/hero_batrider/batrider_firefly_debuff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.batrider_firefly,
				SearchOwner = true
			});
			dictionary2.Add("particles/econ/items/batrider/batrider_ti8_immortal_mount/batrider_ti8_immortal_firefly_debuff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.batrider_firefly,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_batrider/batrider_flaming_lasso.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.batrider_flaming_lasso
			});
			dictionary2.Add("particles/units/heroes/hero_beastmaster/beastmaster_wildaxe.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.beastmaster_wild_axes
			});
			dictionary2.Add("particles/units/heroes/hero_beastmaster/beastmaster_call_bird.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.beastmaster_call_of_the_wild_hawk
			});
			dictionary2.Add("particles/units/heroes/hero_beastmaster/beastmaster_call_boar.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.beastmaster_call_of_the_wild_boar
			});
			dictionary2.Add("particles/units/heroes/hero_beastmaster/beastmaster_primal_roar.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.beastmaster_primal_roar
			});
			dictionary2.Add("particles/units/heroes/hero_bloodseeker/bloodseeker_bloodritual_ring.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.bloodseeker_blood_bath
			});
			dictionary2.Add("particles/units/heroes/hero_bounty_hunter/bounty_hunter_windwalk.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.bounty_hunter_wind_walk
			});
			dictionary2.Add("particles/units/heroes/hero_bounty_hunter/bounty_hunter_hand_r.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.bounty_hunter_jinada
			});
			dictionary2.Add("particles/units/heroes/hero_brewmaster/brewmaster_thunder_clap.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.brewmaster_thunder_clap
			});
			dictionary2.Add("particles/units/heroes/hero_brewmaster/brewmaster_cinder_brew_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.brewmaster_cinder_brew
			});
			dictionary2.Add("particles/units/heroes/hero_brewmaster/brewmaster_drunkenbrawler_crit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.brewmaster_drunken_brawler
			});
			dictionary2.Add("particles/units/heroes/hero_brewmaster/brewmaster_earth_ambient.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.brewmaster_primal_split,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_brewmaster/brewmaster_earth_death.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.brewmaster_primal_split,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_brewmaster/brewmaster_windwalk.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.brewmaster_storm_wind_walk
			});
			dictionary2.Add("particles/units/heroes/hero_bristleback/bristleback_quill_spray.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.bristleback_quill_spray
			});
			dictionary2.Add("particles/econ/items/bristleback/bristle_spikey_spray/bristle_spikey_quill_spray.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.bristleback_quill_spray
			});
			dictionary2.Add("particles/units/heroes/hero_bristleback/bristleback_back_dmg.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.bristleback_bristleback
			});
			dictionary2.Add("particles/units/heroes/hero_bristleback/bristleback_side_dmg.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.bristleback_bristleback
			});
			dictionary2.Add("particles/units/heroes/hero_bristleback/bristleback_viscous_nasal_goo_debuff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.bristleback_viscous_nasal_goo,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_broodmother/broodmother_spin_web_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.broodmother_spin_web
			});
			dictionary2.Add("particles/units/heroes/hero_broodmother/broodmother_hunger_buff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.broodmother_insatiable_hunger
			});
			dictionary2.Add("particles/units/heroes/hero_broodmother/broodmother_spiderlings_spawn.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.broodmother_spawn_spiderlings,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_centaur/centaur_warstomp.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.centaur_hoof_stomp,
				ControlPoint = 2u
			});
			dictionary2.Add("particles/units/heroes/hero_centaur/centaur_double_edge.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.centaur_double_edge
			});
			dictionary2.Add("particles/units/heroes/hero_centaur/centaur_stampede.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.centaur_stampede,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_chaos_knight/chaos_knight_bolt_msg.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.chaos_knight_chaos_bolt,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_chaos_knight/chaos_knight_reality_rift.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.chaos_knight_reality_rift,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/chaos_knight/chaos_knight_ti7_shield/chaos_knight_ti7_reality_rift.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.chaos_knight_reality_rift,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/chaos_knight/chaos_knight_ti7_shield/chaos_knight_ti7_golden_reality_rift.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.chaos_knight_reality_rift,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_chen/chen_cast_1.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.chen_penitence
			});
			dictionary2.Add("particles/units/heroes/hero_chen/chen_cast_2.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.chen_divine_favor
			});
			dictionary2.Add("particles/units/heroes/hero_chen/chen_cast_3.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.chen_holy_persuasion
			});
			dictionary2.Add("particles/units/heroes/hero_chen/chen_teleport_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.chen_holy_persuasion
			});
			dictionary2.Add("particles/units/heroes/hero_chen/chen_cast_4.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.chen_hand_of_god
			});
			dictionary2.Add("particles/units/heroes/hero_clinkz/clinkz_strafe_dodge.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.clinkz_strafe,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_clinkz/clinkz_windwalk.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.clinkz_wind_walk,
				ShowNotification = true
			});
			dictionary2.Add("particles/units/heroes/hero_clinkz/clinkz_death_pact.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.clinkz_burning_army,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_rattletrap/rattletrap_battery_shrapnel.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.rattletrap_battery_assault,
				ControlPoint = 1u,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_rattletrap/rattletrap_cog_ambient.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.rattletrap_power_cogs,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_rattletrap/rattletrap_rocket_flare.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.rattletrap_rocket_flare
			});
			dictionary2.Add("particles/econ/items/clockwerk/clockwerk_paraflare/clockwerk_para_rocket_flare.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.rattletrap_rocket_flare
			});
			dictionary2.Add("particles/units/heroes/hero_rattletrap/rattletrap_hookshot.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.rattletrap_hookshot
			});
			dictionary2.Add("particles/units/heroes/hero_crystalmaiden/maiden_crystal_nova.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.crystal_maiden_crystal_nova
			});
			dictionary2.Add("particles/econ/items/crystal_maiden/crystal_maiden_cowl_of_ice/maiden_crystal_nova_cowlofice.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.crystal_maiden_crystal_nova
			});
			dictionary2.Add("particles/units/heroes/hero_crystalmaiden/maiden_frostbite.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.crystal_maiden_frostbite,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/econ/items/crystal_maiden/ti7_immortal_shoulder/cm_ti7_immortal_frostbite_proj.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.crystal_maiden_frostbite,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_crystalmaiden/maiden_freezing_field_snow.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.crystal_maiden_freezing_field
			});
			dictionary2.Add("particles/econ/items/crystal_maiden/crystal_maiden_maiden_of_icewrack/maiden_freezing_field_snow_arcana1.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.crystal_maiden_freezing_field
			});
			dictionary2.Add("particles/units/heroes/hero_dark_seer/dark_seer_vacuum.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.dark_seer_vacuum
			});
			dictionary2.Add("particles/units/heroes/hero_dark_seer/dark_seer_ion_shell_damage.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.dark_seer_ion_shell,
				Replace = true,
				TimeToShow = 1f
			});
			dictionary2.Add("particles/units/heroes/hero_dark_seer/dark_seer_wall_of_replica.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.dark_seer_wall_of_replica
			});
			dictionary2.Add("particles/units/heroes/hero_dark_willow/dark_willow_bramble_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.dark_willow_bramble_maze
			});
			dictionary2.Add("particles/units/heroes/hero_dark_willow/dark_willow_shadow_realm.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.dark_willow_shadow_realm
			});
			dictionary2.Add("particles/units/heroes/hero_dark_willow/dark_willow_ley_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.dark_willow_cursed_crown
			});
			dictionary2.Add("particles/econ/items/dark_willow/dark_willow_ti8_immortal_head/dw_crimson_ti8_immortal_cursed_crown_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.dark_willow_cursed_crown
			});
			dictionary2.Add("particles/econ/items/dark_willow/dark_willow_ti8_immortal_head/dw_ti8_immortal_cursed_crown_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.dark_willow_cursed_crown
			});
			dictionary2.Add("particles/units/heroes/hero_dark_willow/dark_willow_wisp_aoe.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.dark_willow_bedlam
			});
			dictionary2.Add("particles/units/heroes/hero_dark_willow/dark_willow_wisp_spell_channel.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.dark_willow_terrorize,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_dazzle/dazzle_shadow_wave.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.dazzle_shadow_wave
			});
			dictionary2.Add("particles/units/heroes/hero_death_prophet/death_prophet_carrion_swarm.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.death_prophet_carrion_swarm
			});
			dictionary2.Add("particles/econ/items/death_prophet/death_prophet_acherontia/death_prophet_acher_swarm.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.death_prophet_carrion_swarm
			});
			dictionary2.Add("particles/units/heroes/hero_death_prophet/death_prophet_silence_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.death_prophet_silence
			});
			dictionary2.Add("particles/units/heroes/hero_death_prophet/death_prophet_spiritsiphon.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.death_prophet_spirit_siphon
			});
			dictionary2.Add("particles/units/heroes/hero_death_prophet/death_prophet_spirit_glow.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.death_prophet_exorcism,
				Replace = true,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_disruptor/disruptor_thunder_strike_buff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.disruptor_thunder_strike
			});
			dictionary2.Add("particles/econ/items/disruptor/disruptor_ti8_immortal_weapon/disruptor_ti8_immortal_thunder_strike_buff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.disruptor_thunder_strike
			});
			dictionary2.Add("particles/units/heroes/hero_doom_bringer/doom_bringer_scorched_earth_debuff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.doom_bringer_scorched_earth,
				Replace = true,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_doom_bringer/doom_bringer_devour.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.doom_bringer_devour
			});
			dictionary2.Add("particles/econ/items/doom/doom_ti8_immortal_arms/doom_ti8_immortal_devour.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.doom_bringer_devour
			});
			dictionary2.Add("particles/units/heroes/hero_dragon_knight/dragon_knight_dragon_tail_dragonform.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.dragon_knight_dragon_tail
			});
			dictionary2.Add("particles/units/heroes/hero_dragon_knight/dragon_knight_dragon_tail_knightform.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.dragon_knight_dragon_tail
			});
			dictionary2.Add("particles/units/heroes/hero_earth_spirit/espirit_bouldersmash_caster.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.earth_spirit_boulder_smash
			});
			dictionary2.Add("particles/units/heroes/hero_earth_spirit/espirit_rollingboulder.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.earth_spirit_rolling_boulder
			});
			dictionary2.Add("particles/econ/items/earth_spirit/earth_spirit_ti6_boulder/espirit_ti6_rollingboulder.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.earth_spirit_rolling_boulder
			});
			dictionary2.Add("particles/units/heroes/hero_earth_spirit/espirit_geomagentic_grip_caster.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.earth_spirit_geomagnetic_grip
			});
			dictionary2.Add("particles/units/heroes/hero_earthshaker/earthshaker_totem_leap_blur.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.earthshaker_enchant_totem,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/econ/items/earthshaker/earthshaker_totem_ti6/earthshaker_totem_ti6_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.earthshaker_enchant_totem
			});
			dictionary2.Add("particles/econ/items/earthshaker/earthshaker_totem_ti6/earthshaker_totem_ti6_leap_blur.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.earthshaker_enchant_totem,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_elder_titan/elder_titan_echo_stomp_impact_physical.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.elder_titan_echo_stomp,
				Replace = true,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_elder_titan/elder_titan_echo_stomp_impact_magical.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.elder_titan_echo_stomp,
				Replace = true,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_elder_titan/elder_titan_ancestral_spirit_ambient.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.elder_titan_ancestral_spirit,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_elder_titan/elder_titan_earth_splitter.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.elder_titan_earth_splitter
			});
			dictionary2.Add("particles/units/heroes/hero_ember_spirit/ember_spirit_searing_chains_start.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.ember_spirit_searing_chains
			});
			dictionary2.Add("particles/units/heroes/hero_ember_spirit/ember_spirit_sleight_of_fist_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.ember_spirit_sleight_of_fist
			});
			dictionary2.Add("particles/units/heroes/hero_ember_spirit/emberspirit_flame_shield_aoe_impact.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.ember_spirit_flame_guard,
				Replace = true,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_ember_spirit/ember_spirit_flameguard.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.ember_spirit_flame_guard
			});
			string key20 = "particles/units/heroes/hero_ember_spirit/ember_spirit_fire_remnant.vpcf";
			FireRemnantAbilityData fireRemnantAbilityData = new FireRemnantAbilityData();
			fireRemnantAbilityData.AbilityId = AbilityId.ember_spirit_fire_remnant;
			fireRemnantAbilityData.StartControlPoint = 1u;
			fireRemnantAbilityData.Duration = Ability.GetAbilityDataById(AbilityId.venomancer_plague_ward).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "duration").Value + 3f;
			dictionary2.Add(key20, fireRemnantAbilityData);
			dictionary2.Add("particles/units/heroes/hero_ember_spirit/ember_spirit_remnant_dash.vpcf", new FireRemnantAbilityData
			{
				AbilityId = AbilityId.ember_spirit_fire_remnant
			});
			dictionary2.Add("particles/units/heroes/hero_enchantress/enchantress_enchant.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.enchantress_enchant,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_enchantress/enchantress_natures_attendants_heal.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.enchantress_natures_attendants,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_enigma/enigma_demonic_conversion.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.enigma_demonic_conversion,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_enigma/enigma_midnight_pulse.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.enigma_midnight_pulse
			});
			dictionary2.Add("particles/units/heroes/hero_faceless_void/faceless_void_time_walk_slow.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.faceless_void_time_walk
			});
			dictionary2.Add("particles/econ/items/faceless_void/faceless_void_jewel_of_aeons/fv_time_walk_slow_jewel.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.faceless_void_time_walk
			});
			dictionary2.Add("particles/units/heroes/hero_faceless_void/faceless_void_time_lock_bash.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.faceless_void_time_lock,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_faceless_void/faceless_void_backtrack.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.faceless_void_time_lock,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_grimstroke/grimstroke_cast2_ground.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.grimstroke_dark_artistry
			});
			dictionary2.Add("particles/units/heroes/hero_grimstroke/grimstroke_cast_phantom.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.grimstroke_ink_creature
			});
			dictionary2.Add("particles/units/heroes/hero_grimstroke/grimstroke_phantom_attacked.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.grimstroke_ink_creature,
				ControlPoint = 1u,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_grimstroke/grimstroke_cast_ink_swell.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.grimstroke_spirit_walk
			});
			dictionary2.Add("particles/units/heroes/hero_grimstroke/grimstroke_ink_swell_aoe.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.grimstroke_spirit_walk
			});
			dictionary2.Add("particles/units/heroes/hero_grimstroke/grimstroke_ink_swell_tick_damage.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.grimstroke_spirit_walk,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_gyrocopter/gyro_rocket_barrage.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.gyrocopter_rocket_barrage,
				Replace = true,
				TimeToShow = 2f
			});
			dictionary2.Add("particles/units/heroes/hero_gyrocopter/gyro_calldown_first.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.gyrocopter_call_down
			});
			dictionary2.Add("particles/units/heroes/hero_huskar/huskar_life_break.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.huskar_life_break,
				SearchOwner = true
			});
			dictionary2.Add("particles/econ/items/huskar/huskar_searing_dominator/huskar_searing_life_break.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.huskar_life_break,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_invoker/invoker_quas_orb.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_wex,
				Replace = true,
				TimeToShow = 2f
			});
			dictionary2.Add("particles/econ/items/invoker/invoker_apex/invoker_apex_quas_orb.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_wex,
				Replace = true,
				TimeToShow = 2f
			});
			dictionary2.Add("particles/econ/items/invoker/invoker_ti6/invoker_ti6_quas_orb.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_wex,
				Replace = true,
				TimeToShow = 2f
			});
			dictionary2.Add("particles/units/heroes/hero_invoker/invoker_wex_orb.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_wex,
				Replace = true,
				TimeToShow = 2f
			});
			dictionary2.Add("particles/econ/items/invoker/invoker_ti6/invoker_ti6_exort_orb.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_wex,
				Replace = true,
				TimeToShow = 2f
			});
			dictionary2.Add("particles/econ/items/invoker/invoker_apex/invoker_apex_wex_orb.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_wex,
				Replace = true,
				TimeToShow = 2f
			});
			dictionary2.Add("particles/units/heroes/hero_invoker/invoker_exort_orb.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_wex,
				Replace = true,
				TimeToShow = 2f
			});
			dictionary2.Add("particles/econ/items/invoker/invoker_apex/invoker_apex_exort_orb.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_wex,
				Replace = true,
				TimeToShow = 2f
			});
			dictionary2.Add("particles/econ/items/invoker/invoker_ti6/invoker_ti6_wex_orb.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_wex,
				Replace = true,
				TimeToShow = 2f
			});
			dictionary2.Add("particles/units/heroes/hero_invoker/invoker_cold_snap.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_cold_snap,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_invoker/invoker_ice_wall.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_ice_wall
			});
			dictionary2.Add("particles/units/heroes/hero_invoker/invoker_emp.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_emp
			});
			dictionary2.Add("particles/units/heroes/hero_invoker/invoker_chaos_meteor_fly.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_chaos_meteor
			});
			dictionary2.Add("particles/econ/items/invoker/invoker_ti7/invoker_ti7_alacrity_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.invoker_alacrity
			});
			dictionary2.Add("particles/units/heroes/hero_wisp/wisp_tether.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.wisp_tether
			});
			dictionary2.Add("particles/econ/items/wisp/wisp_tether_ti7.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.wisp_tether
			});
			dictionary2.Add("particles/units/heroes/hero_wisp/wisp_guardian_explosion_small.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.wisp_spirits,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_wisp/wisp_guardian_explosion.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.wisp_spirits,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/wisp/wisp_guardian_explosion_ti7.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.wisp_spirits,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_wisp/wisp_tether_hit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.wisp_tether,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_wisp/wisp_overcharge.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.wisp_overcharge
			});
			dictionary2.Add("particles/econ/items/wisp/wisp_overcharge_ti7.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.wisp_overcharge
			});
			dictionary2.Add("particles/units/heroes/hero_wisp/wisp_relocate_marker_endpoint.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.wisp_relocate
			});
			dictionary2.Add("particles/econ/items/wisp/wisp_relocate_marker_ti7_endpoint.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.wisp_relocate
			});
			dictionary2.Add("particles/units/heroes/hero_jakiro/jakiro_dual_breath_ice.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.jakiro_dual_breath
			});
			dictionary2.Add("particles/econ/items/jakiro/jakiro_ti8_immortal_head/jakiro_ti8_dual_breath_ice.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.jakiro_dual_breath
			});
			dictionary2.Add("particles/units/heroes/hero_jakiro/jakiro_ice_path.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.jakiro_ice_path
			});
			dictionary2.Add("particles/econ/items/jakiro/jakiro_ti7_immortal_head/jakiro_ti7_immortal_head_ice_path.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.jakiro_ice_path
			});
			dictionary2.Add("particles/units/heroes/hero_jakiro/jakiro_liquid_fire_explosion.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.jakiro_liquid_fire,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_jakiro/jakiro_liquid_fire_ready.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.jakiro_liquid_fire,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_juggernaut/juggernaut_blade_fury_tgt.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.juggernaut_blade_fury,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/juggernaut/jugg_ti8_sword/juggernaut_crimson_blade_fury_abyssal_tgt.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.juggernaut_blade_fury,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/juggernaut/jugg_ti8_sword/juggernaut_blade_fury_abyssal_tgt_golden.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.juggernaut_blade_fury,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/juggernaut/jugg_arcana/juggernaut_arcana_crit_tgt.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.juggernaut_blade_dance,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/juggernaut/jugg_arcana/juggernaut_arcana_v2_crit_tgt.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.juggernaut_blade_dance,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_keeper_of_the_light/keeper_of_the_light_illuminate_charge.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.keeper_of_the_light_illuminate
			});
			dictionary2.Add("particles/units/heroes/hero_keeper_of_the_light/keeper_of_the_light_blinding_light_aoe.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.keeper_of_the_light_blinding_light
			});
			dictionary2.Add("particles/units/heroes/hero_keeper_of_the_light/keeper_chakra_magic.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.keeper_of_the_light_chakra_magic,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_kunkka/kunkka_spell_torrent_splash.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.kunkka_torrent
			});
			dictionary2.Add("particles/econ/items/kunkka/kunkka_weapon_whaleblade_retro/kunkka_spell_torrent_retro_splash_whaleblade.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.kunkka_torrent
			});
			dictionary2.Add("particles/econ/items/kunkka/divine_anchor/hero_kunkka_dafx_skills/kunkka_spell_torrent_splash_fxset.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.kunkka_torrent
			});
			dictionary2.Add("particles/units/heroes/hero_kunkka/kunkka_spell_x_spot.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.kunkka_x_marks_the_spot
			});
			dictionary2.Add("particles/econ/items/kunkka/divine_anchor/hero_kunkka_dafx_skills/kunkka_spell_x_spot_fxset.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.kunkka_x_marks_the_spot
			});
			dictionary2.Add("particles/units/heroes/hero_kunkka/kunkka_spell_tidebringer.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.kunkka_tidebringer,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/econ/items/kunkka/divine_anchor/hero_kunkka_dafx_weapon/kunkka_spell_tidebringer_fxset.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.kunkka_tidebringer,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_legion_commander/legion_commander_odds_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.legion_commander_overwhelming_odds,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_legion_commander/legion_commander_press_hero.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.legion_commander_press_the_attack
			});
			dictionary2.Add("particles/units/heroes/hero_legion_commander/legion_commander_press.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.legion_commander_press_the_attack
			});
			dictionary2.Add("particles/units/heroes/hero_legion_commander/legion_commander_courage_hit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.legion_commander_moment_of_courage,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_leshrac/leshrac_diabolic_edict.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.leshrac_diabolic_edict,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_leshrac/leshrac_lightning_bolt.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.leshrac_lightning_storm,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_leshrac/leshrac_pulse_nova.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.leshrac_pulse_nova,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_lich/lich_ice_age_dmg.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lich_frost_shield,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_lich/lich_gaze.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lich_sinister_gaze,
				ControlPoint = 2u
			});
			dictionary2.Add("particles/units/heroes/hero_life_stealer/life_stealer_open_wounds_impact.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.life_stealer_open_wounds,
				SearchOwner = true
			});
			dictionary2.Add("particles/econ/items/lina/lina_head_headflame/lina_spell_dragon_slave_impact_headflame.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lina_dragon_slave,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_lina/lina_spell_laguna_blade.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lina_laguna_blade
			});
			dictionary2.Add("particles/units/heroes/hero_lion/lion_spell_impale_staff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lion_impale
			});
			dictionary2.Add("particles/units/heroes/hero_lion/lion_spell_mana_drain.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lion_mana_drain,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/econ/items/lion/lion_demon_drain/lion_spell_mana_drain_demon.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lion_mana_drain,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_lion/lion_spell_finger_of_death.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lion_finger_of_death
			});
			dictionary2.Add("particles/units/heroes/hero_rubick/rubick_finger_of_death.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lion_finger_of_death
			});
			dictionary2.Add("particles/econ/items/lion/lion_ti8/lion_spell_finger_ti8.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lion_finger_of_death
			});
			dictionary2.Add("particles/econ/items/lion/lion_ti8/lion_spell_finger_death_arcana.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lion_finger_of_death
			});
			dictionary2.Add("particles/units/heroes/hero_lone_druid/lone_druid_bear_spawn.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lone_druid_spirit_bear
			});
			dictionary2.Add("particles/units/heroes/hero_lone_druid/lone_druid_spiritlink_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lone_druid_spirit_link
			});
			dictionary2.Add("particles/units/heroes/hero_lone_druid/lone_druid_savage_roar.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lone_druid_savage_roar,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_lone_druid/lone_druid_battle_cry_buff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lone_druid_true_form_battle_cry
			});
			dictionary2.Add("particles/units/heroes/hero_lone_druid/lone_druid_true_form.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lone_druid_true_form
			});
			dictionary2.Add("particles/units/heroes/hero_lone_druid/true_form_lone_druid.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lone_druid_true_form
			});
			dictionary2.Add("particles/units/heroes/hero_lone_druid/lone_druid_bear_entangle.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lone_druid_spirit_bear_entangle,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_luna/luna_lucent_beam_precast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.luna_lucent_beam
			});
			dictionary2.Add("particles/units/heroes/hero_luna/luna_eclipse_precast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.luna_eclipse
			});
			dictionary2.Add("particles/units/heroes/hero_lycan/lycan_summon_wolves_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lycan_summon_wolves
			});
			dictionary2.Add("particles/units/heroes/hero_lycan/lycan_howl_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lycan_howl,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_lycan/lycan_shapeshift_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.lycan_shapeshift,
				RawParticlePosition = true
			});
			dictionary2.Add("particles/units/heroes/hero_magnataur/magnataur_shockwave_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.magnataur_shockwave,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/econ/items/magnataur/shock_of_the_anvil/magnataur_shockanvil_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.magnataur_shockwave,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_magnataur/magnataur_empower_cleave_effect.vpcf", new CleaveAbilityData
			{
				AbilityId = AbilityId.magnataur_empower,
				ControlPoint = 2u,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_magnataur/magnataur_skewer.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.magnataur_skewer,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_magnataur/magnataur_reverse_polarity.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.magnataur_reverse_polarity
			});
			dictionary2.Add("particles/units/heroes/hero_mars/mars_shield_bash_crit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.mars_gods_rebuke,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_medusa/medusa_mystic_snake_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.medusa_mystic_snake
			});
			dictionary2.Add("particles/units/heroes/hero_medusa/medusa_mana_shield_end.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.medusa_mana_shield
			});
			dictionary2.Add("particles/units/heroes/hero_medusa/medusa_mana_shield_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.medusa_mana_shield
			});
			dictionary2.Add("particles/units/heroes/hero_medusa/medusa_stone_gaze_active.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.medusa_stone_gaze,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_meepo/meepo_earthbind_projectile_fx.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.meepo_earthbind
			});
			dictionary2.Add("particles/units/heroes/hero_meepo/meepo_poof_end.vpcf", new PoofAbilityData
			{
				AbilityId = AbilityId.meepo_poof
			});
			dictionary2.Add("particles/econ/items/meepo/meepo_colossal_crystal_chorus/meepo_divining_rod_poof_end.vpcf", new PoofAbilityData
			{
				AbilityId = AbilityId.meepo_poof
			});
			dictionary2.Add("particles/units/heroes/hero_mirana/mirana_starfall_attack.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.mirana_starfall,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/mirana/mirana_starstorm_bow/mirana_starstorm_starfall_attack.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.mirana_starfall,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_mirana/mirana_moonlight_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.mirana_invis,
				ShowNotification = true
			});
			dictionary2.Add("particles/units/heroes/hero_monkey_king/monkey_king_strike_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.monkey_king_boundless_strike
			});
			dictionary2.Add("particles/econ/items/monkey_king/ti7_weapon/mk_ti7_golden_immortal_strike_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.monkey_king_boundless_strike
			});
			dictionary2.Add("particles/econ/items/monkey_king/ti7_weapon/mk_ti7_immortal_strike_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.monkey_king_boundless_strike
			});
			dictionary2.Add("particles/units/heroes/hero_monkey_king/monkey_king_jump_trail.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.monkey_king_tree_dance,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_monkey_king/monkey_king_spring.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.monkey_king_primal_spring
			});
			dictionary2.Add("particles/econ/items/monkey_king/arcana/water/monkey_king_spring_arcana_water.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.monkey_king_primal_spring
			});
			dictionary2.Add("particles/econ/items/monkey_king/arcana/fire/monkey_king_spring_arcana_fire.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.monkey_king_primal_spring
			});
			dictionary2.Add("particles/units/heroes/hero_monkey_king/monkey_king_quad_tap_hit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.monkey_king_jingu_mastery
			});
			dictionary2.Add("particles/units/heroes/hero_monkey_king/monkey_king_fur_army_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.monkey_king_wukongs_command
			});
			dictionary2.Add("particles/units/heroes/hero_monkey_king/monkey_king_disguise.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.monkey_king_mischief
			});
			dictionary2.Add("particles/econ/items/morphling/morphling_crown_of_tears/morphling_crown_waveform_dmg.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.morphling_waveform,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_morphling/morphling_replicate_finish.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.morphling_morph_replicate,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_siren/siren_net.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.naga_siren_ensnare,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_siren/naga_siren_riptide.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.naga_siren_rip_tide
			});
			dictionary2.Add("particles/econ/items/naga/naga_ti8_immortal_tail/naga_ti8_immortal_riptide.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.naga_siren_rip_tide
			});
			dictionary2.Add("particles/units/heroes/hero_furion/furion_sprout.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.furion_sprout
			});
			dictionary2.Add("particles/units/heroes/hero_furion/furion_teleport.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.furion_teleportation,
				ShowNotification = true
			});
			dictionary2.Add("particles/econ/items/natures_prophet/natures_prophet_weapon_sufferwood/furion_teleport_sufferwood.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.furion_teleportation,
				ShowNotification = true
			});
			dictionary2.Add("particles/units/heroes/hero_furion/furion_teleport_end.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.furion_teleportation,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/econ/items/natures_prophet/natures_prophet_weapon_sufferwood/furion_teleport_end_sufferwood.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.furion_teleportation,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_furion/furion_force_of_nature_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.furion_force_of_nature,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_furion/furion_wrath_of_nature_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.furion_wrath_of_nature,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_necrolyte/necrolyte_sadist.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.necrolyte_sadist
			});
			dictionary2.Add("particles/units/heroes/hero_night_stalker/nightstalker_void_hit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.night_stalker_void,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_night_stalker/nightstalker_crippling_fear_aura.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.night_stalker_crippling_fear,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_night_stalker/nightstalker_dark_buff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.night_stalker_darkness
			});
			dictionary2.Add("particles/units/heroes/hero_nyx_assassin/nyx_assassin_vendetta_start.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.nyx_assassin_vendetta,
				ShowNotification = true
			});
			dictionary2.Add("particles/units/heroes/hero_nyx_assassin/nyx_assassin_vendetta.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.nyx_assassin_vendetta
			});
			dictionary2.Add("particles/units/heroes/hero_nyx_assassin/nyx_assassin_impale_hit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.nyx_assassin_impale,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/nyx_assassin/nyx_assassin_ti6/nyx_assassin_impale_hit_ti6.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.nyx_assassin_impale,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_nyx_assassin/nyx_assassin_burrow.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.nyx_assassin_burrow
			});
			dictionary2.Add("particles/units/heroes/hero_nyx_assassin/nyx_assassin_burrow_water.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.nyx_assassin_burrow
			});
			dictionary2.Add("particles/units/heroes/hero_nyx_assassin/nyx_assassin_burrow_exit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.nyx_assassin_unburrow
			});
			dictionary2.Add("particles/units/heroes/hero_nyx_assassin/nyx_assassin_burrow_exit_water.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.nyx_assassin_unburrow
			});
			dictionary2.Add("particles/units/heroes/hero_ogre_magi/ogre_magi_fireblast_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.ogre_magi_fireblast
			});
			dictionary2.Add("particles/units/heroes/hero_ogre_magi/ogre_magi_ignite_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.ogre_magi_ignite
			});
			dictionary2.Add("particles/units/heroes/hero_ogre_magi/ogre_magi_bloodlust_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.ogre_magi_bloodlust
			});
			dictionary2.Add("particles/units/heroes/hero_omniknight/omniknight_purification_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.omniknight_purification
			});
			dictionary2.Add("particles/econ/items/omniknight/hammer_ti6_immortal/omniknight_purification_immortal_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.omniknight_purification
			});
			dictionary2.Add("particles/units/heroes/hero_omniknight/omniknight_repel_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.omniknight_repel
			});
			dictionary2.Add("particles/econ/items/omniknight/omni_ti8_head/omniknight_repel_cast_ti8.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.omniknight_repel
			});
			dictionary2.Add("particles/units/heroes/hero_oracle/oracle_fortune_channel.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.oracle_fortunes_end
			});
			dictionary2.Add("particles/econ/items/oracle/oracle_fortune_ti7/oracle_fortune_ti7_channel.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.oracle_fortunes_end
			});
			dictionary2.Add("particles/units/heroes/hero_oracle/oracle_fatesedict_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.oracle_fates_edict
			});
			dictionary2.Add("particles/units/heroes/hero_oracle/oracle_fatesedict_hit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.oracle_fates_edict,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_oracle/oracle_purifyingflames_hit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.oracle_purifying_flames,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_oracle/oracle_false_promise_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.oracle_false_promise,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_oracle/oracle_false_promise_heal.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.oracle_false_promise,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_obsidian_destroyer/obsidian_destroyer_prison_start.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.obsidian_destroyer_astral_imprisonment
			});
			dictionary2.Add("particles/units/heroes/hero_obsidian_destroyer/obsidian_destroyer_matter_debuff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.obsidian_destroyer_equilibrium,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/outworld_devourer/od_ti8/od_ti8_santies_eclipse_area.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.obsidian_destroyer_sanity_eclipse
			});
			dictionary2.Add("particles/units/heroes/hero_obsidian_destroyer/obsidian_destroyer_sanity_eclipse_area.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.obsidian_destroyer_sanity_eclipse
			});
			dictionary2.Add("particles/units/heroes/hero_pangolier/pangolier_swashbuckler.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pangolier_swashbuckle,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/pangolier/pangolier_immortal_musket/pangolier_immortal_swashbuckler.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pangolier_swashbuckle,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_pangolier/pangolier_tailthump.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pangolier_shield_crash
			});
			dictionary2.Add("particles/econ/items/pangolier/pangolier_ti8_immortal/pangolier_ti8_immortal_shield_crash.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pangolier_shield_crash
			});
			dictionary2.Add("particles/units/heroes/hero_pangolier/pangolier_gyroshell_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pangolier_gyroshell
			});
			dictionary2.Add("particles/units/heroes/hero_pangolier/pangolier_luckyshot_disarm_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pangolier_lucky_shot
			});
			dictionary2.Add("particles/units/heroes/hero_phantom_assassin/phantom_assassin_phantom_strike_start.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phantom_assassin_phantom_strike
			});
			dictionary2.Add("particles/units/heroes/hero_phantom_assassin/phantom_assassin_active_start.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phantom_assassin_blur
			});
			dictionary2.Add("particles/econ/items/phantom_assassin/phantom_assassin_weapon_generic/phantom_assassin_ambient_blade_generic.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phantom_assassin_coup_de_grace
			});
			dictionary2.Add("particles/units/heroes/hero_phantom_assassin/phantom_assassin_crit_impact.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phantom_assassin_coup_de_grace
			});
			dictionary2.Add("particles/econ/items/phantom_assassin/phantom_assassin_arcana_elder_smith/phantom_assassin_crit_arcana_swoop.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phantom_assassin_coup_de_grace
			});
			dictionary2.Add("particles/units/heroes/hero_phantom_assassin/phantom_assassin_crit_impact_mechanical.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phantom_assassin_coup_de_grace
			});
			dictionary2.Add("particles/econ/items/phantom_assassin/phantom_assassin_arcana_elder_smith/phantom_assassin_crit_mechanical_arcana_swoop.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phantom_assassin_coup_de_grace
			});
			dictionary2.Add("particles/units/heroes/hero_phantom_lancer/phantomlancer_spiritlance_caster.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phantom_lancer_spirit_lance,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/phantom_lancer/phantom_lancer_immortal_ti6/phantom_lancer_immortal_ti6_spiritlance_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phantom_lancer_spirit_lance,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_phantom_lancer/phantom_lancer_doppleganger_aoe.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phantom_lancer_doppelwalk,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_phoenix/phoenix_icarus_dive.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phoenix_icarus_dive
			});
			dictionary2.Add("particles/units/heroes/hero_phoenix/phoenix_fire_spirit_launch.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phoenix_launch_fire_spirit,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_phoenix/phoenix_sunray.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phoenix_sun_ray
			});
			dictionary2.Add("particles/econ/items/phoenix/phoenix_solar_forge/phoenix_sunray_solar_forge.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phoenix_sun_ray
			});
			dictionary2.Add("particles/units/heroes/hero_phoenix/phoenix_sunray_beam_friend.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phoenix_sun_ray,
				Replace = true,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/econ/items/phoenix/phoenix_solar_forge/phoenix_sunray_beam_friend_solar_forge.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phoenix_sun_ray,
				Replace = true,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_phoenix/phoenix_supernova_start.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.phoenix_supernova,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_pudge/pudge_meathook.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pudge_meat_hook
			});
			dictionary2.Add("particles/econ/items/pudge/pudge_trapper_beam_chain/pudge_nx_meathook.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pudge_meat_hook
			});
			dictionary2.Add("particles/econ/items/pudge/pudge_hook_whale/pudge_meathook_whale.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pudge_meat_hook
			});
			dictionary2.Add("particles/econ/items/pudge/pudge_ti6_immortal/pudge_ti6_meathook.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pudge_meat_hook
			});
			dictionary2.Add("particles/econ/items/pudge/pudge_ti6_immortal_gold/pudge_ti6_meathook_gold.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pudge_meat_hook
			});
			dictionary2.Add("particles/econ/items/pudge/pudge_ti6_immortal/pudge_ti6_witness_meathook.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pudge_meat_hook
			});
			dictionary2.Add("particles/econ/items/pudge/pudge_scorching_talon/pudge_scorching_talon_meathook.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pudge_meat_hook
			});
			dictionary2.Add("particles/econ/items/pudge/pudge_dragonclaw/pudge_meathook_dragonclaw.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pudge_meat_hook
			});
			dictionary2.Add("particles/econ/items/pudge/pudge_harvester/pudge_meathook_harvester.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pudge_meat_hook
			});
			dictionary2.Add("particles/units/heroes/hero_pugna/pugna_netherblast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pugna_nether_blast
			});
			dictionary2.Add("particles/units/heroes/hero_pugna/pugna_life_drain.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pugna_life_drain,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_pugna/pugna_life_give.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.pugna_life_drain,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_queenofpain/queen_shadow_strike_body.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.queenofpain_shadow_strike
			});
			dictionary2.Add("particles/econ/items/queen_of_pain/qop_ti8_immortal/queen_ti8_shadow_strike_body.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.queenofpain_shadow_strike
			});
			dictionary2.Add("particles/units/heroes/hero_queenofpain/queen_blink_start.vpcf", new BlinkAbilityData
			{
				AbilityId = AbilityId.queenofpain_blink
			});
			dictionary2.Add("particles/units/heroes/hero_razor/razor_plasmafield.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.razor_plasma_field
			});
			dictionary2.Add("particles/econ/items/razor/razor_ti6/razor_plasmafield_ti6.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.razor_plasma_field
			});
			dictionary2.Add("particles/units/heroes/hero_razor/razor_storm_lightning_strike.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.razor_eye_of_the_storm,
				Replace = true,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_riki/riki_smokebomb.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.riki_smoke_screen,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_riki/riki_backstab.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.riki_permanent_invisibility
			});
			dictionary2.Add("particles/units/heroes/hero_riki/riki_tricks.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.riki_tricks_of_the_trade
			});
			dictionary2.Add("particles/units/heroes/hero_rubick/rubick_telekinesis.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.rubick_telekinesis,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_rubick/rubick_fade_bolt_head.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.rubick_fade_bolt
			});
			dictionary2.Add("particles/econ/items/rubick/rubick_ti8_immortal/rubick_ti8_immortal_fade_bolt_head.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.rubick_fade_bolt
			});
			dictionary2.Add("particles/units/heroes/hero_sandking/sandking_sandstorm.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.sandking_sand_storm
			});
			dictionary2.Add("particles/units/heroes/hero_sandking/sandking_caustic_finale_debuff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.sandking_caustic_finale,
				SearchOwner = true
			});
			dictionary2.Add("particles/econ/items/sand_king/sandking_ti7_arms/sandking_ti7_caustic_finale_debuff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.sandking_caustic_finale,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_sandking/sandking_caustic_finale_explode.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.sandking_caustic_finale,
				SearchOwner = true
			});
			dictionary2.Add("particles/econ/items/sand_king/sandking_ti7_arms/sandking_ti7_caustic_finale_explode.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.sandking_caustic_finale,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_sandking/sandking_epicenter_tell.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.sandking_epicenter
			});
			dictionary2.Add("particles/units/heroes/hero_shadow_demon/shadow_demon_disruption.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shadow_demon_disruption
			});
			dictionary2.Add("particles/units/heroes/hero_shadow_demon/shadow_demon_soul_catcher_v2_projected_ground.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shadow_demon_soul_catcher,
				ControlPoint = 2u
			});
			dictionary2.Add("particles/units/heroes/hero_shadow_demon/shadow_demon_shadow_poison_release.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shadow_demon_shadow_poison_release
			});
			dictionary2.Add("particles/econ/items/shadow_demon/sd_ti7_shadow_poison/sd_ti7_shadow_poison_release.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shadow_demon_shadow_poison_release
			});
			dictionary2.Add("particles/units/heroes/hero_shadow_demon/shadow_demon_demonic_purge_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shadow_demon_demonic_purge
			});
			dictionary2.Add("particles/units/heroes/hero_nevermore/nevermore_shadowraze_debuff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.nevermore_shadowraze3,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_nevermore/nevermore_necro_souls.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.nevermore_necromastery,
				ControlPoint = 1u,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/shadow_fiend/sf_fire_arcana/sf_fire_arcana_necro_souls.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.nevermore_necromastery,
				ControlPoint = 1u,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_nevermore/nevermore_wings.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.nevermore_requiem,
				RawParticlePosition = true
			});
			dictionary2.Add("particles/econ/items/shadow_fiend/sf_fire_arcana/sf_fire_arcana_wings.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.nevermore_requiem,
				RawParticlePosition = true
			});
			dictionary2.Add("particles/units/heroes/hero_shadowshaman/shadowshaman_ether_shock.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shadow_shaman_ether_shock
			});
			dictionary2.Add("particles/econ/items/shadow_shaman/shadow_shaman_ti8/shadow_shaman_ti8_ether_shock.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shadow_shaman_ether_shock
			});
			dictionary2.Add("particles/econ/items/shadow_shaman/shadow_shaman_ti8/shadow_shaman_crimson_ti8_ether_shock.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shadow_shaman_ether_shock
			});
			dictionary2.Add("particles/units/heroes/hero_shadowshaman/shadowshaman_shackle.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shadow_shaman_shackles
			});
			dictionary2.Add("particles/units/heroes/hero_silencer/silencer_curse_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.silencer_curse_of_the_silent
			});
			dictionary2.Add("particles/units/heroes/hero_silencer/silencer_last_word_status_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.silencer_last_word
			});
			dictionary2.Add("particles/units/heroes/hero_silencer/silencer_global_silence.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.silencer_global_silence
			});
			dictionary2.Add("particles/units/heroes/hero_skywrath_mage/skywrath_mage_concussive_shot_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.skywrath_mage_concussive_shot
			});
			dictionary2.Add("particles/units/heroes/hero_skywrath_mage/skywrath_mage_ancient_seal_debuff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.skywrath_mage_ancient_seal,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_skywrath_mage/skywrath_mage_mystic_flare_ambient.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.skywrath_mage_mystic_flare,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_slark/slark_dark_pact_pulses_body.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.slark_dark_pact
			});
			dictionary2.Add("particles/econ/items/slark/slark_head_immortal/slark_immortal_dark_pact_pulses_body.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.slark_dark_pact
			});
			dictionary2.Add("particles/units/heroes/hero_slark/slark_pounce_trail.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.slark_pounce
			});
			dictionary2.Add("particles/econ/items/slark/slark_ti6_blade/slark_ti6_pounce_trail.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.slark_pounce
			});
			dictionary2.Add("particles/econ/items/slark/slark_ti6_blade/slark_ti6_pounce_trail_gold.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.slark_pounce
			});
			dictionary2.Add("particles/units/heroes/hero_sniper/sniper_shrapnel_launch.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.sniper_shrapnel
			});
			dictionary2.Add("particles/units/heroes/hero_sniper/sniper_headshot_slow.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.sniper_headshot,
				SearchOwner = true
			});
			dictionary2.Add("particles/econ/items/sniper/sniper_immortal_cape/sniper_immortal_cape_headshot_slow.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.sniper_headshot,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_spectre/spectre_desolate.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.spectre_desolate,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_spirit_breaker/spirit_breaker_greater_bash.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.spirit_breaker_greater_bash,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/spirit_breaker/spirit_breaker_weapon_ti8/spirit_breaker_bash_ti8.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.spirit_breaker_greater_bash,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_stormspirit/stormspirit_overload_discharge.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.storm_spirit_overload,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/storm_spirit/strom_spirit_ti8/storm_sprit_ti8_overload_discharge.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.storm_spirit_overload,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_stormspirit/stormspirit_ball_lightning.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.storm_spirit_ball_lightning,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/storm_spirit/storm_spirit_orchid_hat/stormspirit_orchid_ball_lightning.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.storm_spirit_ball_lightning,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_sven/sven_spell_storm_bolt_lightning.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.sven_storm_bolt
			});
			dictionary2.Add("particles/units/heroes/hero_sven/sven_spell_great_cleave.vpcf", new CleaveAbilityData
			{
				AbilityId = AbilityId.sven_great_cleave,
				ControlPoint = 2u,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_sven/sven_spell_great_cleave_crit.vpcf", new CleaveAbilityData
			{
				AbilityId = AbilityId.sven_great_cleave,
				ControlPoint = 2u,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_sven/sven_spell_great_cleave_gods_strength.vpcf", new CleaveAbilityData
			{
				AbilityId = AbilityId.sven_great_cleave,
				ControlPoint = 1u,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_sven/sven_spell_great_cleave_gods_strength_crit.vpcf", new CleaveAbilityData
			{
				AbilityId = AbilityId.sven_great_cleave,
				ControlPoint = 1u,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_sven/sven_spell_warcry.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.sven_warcry,
				ControlPoint = 2u
			});
			dictionary2.Add("particles/econ/items/sven/sven_warcry_ti5/sven_spell_warcry_ti_5.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.sven_warcry,
				ControlPoint = 2u
			});
			dictionary2.Add("particles/units/heroes/hero_sven/sven_warcry_buff_shield_hit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.sven_warcry,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_sven/sven_spell_gods_strength.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.sven_gods_strength,
				RawParticlePosition = true
			});
			dictionary2.Add("particles/units/heroes/hero_techies/techies_blast_off.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.techies_suicide
			});
			string key21 = "particles/units/heroes/hero_techies/techies_remote_mine_plant.vpcf";
			RemoteMinesAbilityData remoteMinesAbilityData2 = new RemoteMinesAbilityData();
			remoteMinesAbilityData2.AbilityId = AbilityId.techies_remote_mines;
			remoteMinesAbilityData2.ControlPoint = 1u;
			remoteMinesAbilityData2.Duration = Ability.GetAbilityDataById(AbilityId.techies_remote_mines).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "duration").Value;
			remoteMinesAbilityData2.ShowRange = true;
			remoteMinesAbilityData2.Range = Ability.GetAbilityDataById(AbilityId.techies_remote_mines).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "radius").Value + 50f;
			remoteMinesAbilityData2.RangeColor = new Vector3(0f, 255f, 0f);
			dictionary2.Add(key21, remoteMinesAbilityData2);
			dictionary2.Add("particles/units/heroes/hero_techies/techies_remote_mines_detonate.vpcf", new RemoteMinesAbilityData
			{
				AbilityId = AbilityId.techies_remote_mines,
				ControlPoint = 0u
			});
			string key22 = "particles/econ/items/techies/techies_arcana/techies_remote_mine_plant_arcana.vpcf";
			RemoteMinesAbilityData remoteMinesAbilityData3 = new RemoteMinesAbilityData();
			remoteMinesAbilityData3.AbilityId = AbilityId.techies_remote_mines;
			remoteMinesAbilityData3.ControlPoint = 1u;
			remoteMinesAbilityData3.Duration = Ability.GetAbilityDataById(AbilityId.techies_remote_mines).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == "duration").Value;
			dictionary2.Add(key22, remoteMinesAbilityData3);
			dictionary2.Add("particles/econ/items/techies/techies_arcana/techies_remote_mines_detonate_arcana.vpcf", new RemoteMinesAbilityData
			{
				AbilityId = AbilityId.techies_remote_mines,
				ControlPoint = 0u
			});
			dictionary2.Add("particles/units/heroes/hero_templar_assassin/templar_assassin_refraction.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.templar_assassin_refraction
			});
			dictionary2.Add("particles/units/heroes/hero_templar_assassin/templar_assassin_psi_blade.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.templar_assassin_psi_blades,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/templar_assassin/templar_assassin_focal/ta_focal_psi_blade.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.templar_assassin_psi_blades,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_templar_assassin/templar_assassin_refract_hit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.templar_assassin_psi_blades,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_terrorblade/terrorblade_reflection_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.terrorblade_reflection
			});
			dictionary2.Add("particles/units/heroes/hero_terrorblade/terrorblade_sunder.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.terrorblade_sunder,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_tidehunter/tidehunter_anchor_hero.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.tidehunter_anchor_smash
			});
			dictionary2.Add("particles/units/heroes/hero_tidehunter/tidehunter_spell_ravage.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.tidehunter_ravage
			});
			dictionary2.Add("particles/units/heroes/hero_shredder/shredder_whirling_death.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shredder_whirling_death
			});
			dictionary2.Add("particles/units/heroes/hero_shredder/shredder_timber_chain_tree.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shredder_timber_chain
			});
			dictionary2.Add("particles/units/heroes/hero_shredder/shredder_reactive_hit.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shredder_reactive_armor,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_shredder/shredder_chakram_stay.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shredder_chakram,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/shredder/hero_shredder_icefx/shredder_chakram_stay_ice.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shredder_chakram,
				Replace = true
			});
			dictionary2.Add("particles/econ/items/shredder/hero_shredder_icefx/shredder_chakram_return_ice.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shredder_return_chakram,
				ControlPoint = 1u,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_shredder/shredder_chakram_return.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.shredder_return_chakram,
				ControlPoint = 1u,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_tinker/tinker_missile_dud.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.tinker_heat_seeking_missile
			});
			dictionary2.Add("particles/units/heroes/hero_tinker/tinker_rearm.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.tinker_rearm,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_tiny/tiny_avalanche.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.tiny_avalanche,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_tiny/tiny_craggy_cleave.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.tiny_toss_tree,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_treant/treant_leech_seed.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.treant_leech_seed
			});
			dictionary2.Add("particles/units/heroes/hero_treant/treant_livingarmor.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.treant_living_armor
			});
			dictionary2.Add("particles/econ/items/treant_protector/ti7_shoulder/treant_ti7_livingarmor.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.treant_living_armor
			});
			dictionary2.Add("particles/units/heroes/hero_treant/treant_eyesintheforest.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.treant_eyes_in_the_forest,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_treant/treant_overgrowth_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.treant_overgrowth
			});
			dictionary2.Add("particles/units/heroes/hero_troll_warlord/troll_warlord_whirling_axe_melee.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.troll_warlord_whirling_axes_melee,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_tusk/tusk_snowball.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.tusk_snowball
			});
			dictionary2.Add("particles/units/heroes/hero_tusk/tusk_tag_team.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.tusk_tag_team
			});
			dictionary2.Add("particles/units/heroes/hero_tusk/tusk_walruspunch_start.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.tusk_walrus_punch
			});
			dictionary2.Add("particles/units/heroes/hero_tusk/tusk_walruskick_tgt.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.tusk_walrus_kick
			});
			dictionary2.Add("particles/units/heroes/heroes_underlord/abyssal_underlord_firestorm_wave.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.abyssal_underlord_firestorm,
				Replace = true,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/heroes_underlord/underlord_pit_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.abyssal_underlord_pit_of_malice
			});
			dictionary2.Add("particles/econ/items/underlord/underlord_ti8_immortal_weapon/underlord_crimson_ti8_immortal_pitofmalice_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.abyssal_underlord_pit_of_malice
			});
			dictionary2.Add("particles/units/heroes/heroes_underlord/abyssal_underlord_darkrift_target.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.abyssal_underlord_dark_rift,
				SearchOwner = true,
				TimeToShow = 5f,
				ShowNotification = true
			});
			dictionary2.Add("particles/units/heroes/hero_undying/undying_decay.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.undying_decay,
				RawParticlePosition = true
			});
			dictionary2.Add("particles/econ/items/undying/undying_pale_augur/undying_pale_augur_decay.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.undying_decay,
				RawParticlePosition = true
			});
			dictionary2.Add("particles/units/heroes/hero_undying/undying_soul_rip_damage.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.undying_soul_rip,
				ControlPoint = 2u,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_undying/undying_soul_rip_heal.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.undying_soul_rip,
				Replace = true,
				RawParticlePosition = true
			});
			dictionary2.Add("particles/units/heroes/hero_ursa/ursa_overpower_buff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.ursa_overpower
			});
			dictionary2.Add("particles/units/heroes/hero_ursa/ursa_enrage_buff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.ursa_enrage,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_ursa/ursa_fury_swipes.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.ursa_fury_swipes
			});
			dictionary2.Add("particles/units/heroes/hero_venomancer/venomancer_venomous_gale_mouth.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.venomancer_venomous_gale
			});
			dictionary2.Add("particles/econ/items/venomancer/veno_ti8_immortal_head/veno_ti8_immortal_gale_mouth.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.venomancer_venomous_gale
			});
			dictionary2.Add("particles/units/heroes/hero_venomancer/venomancer_poison_nova.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.venomancer_poison_nova
			});
			dictionary2.Add("particles/units/heroes/hero_viper/viper_poison_debuff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.viper_poison_attack,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_viper/viper_nethertoxin_proj.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.viper_nethertoxin
			});
			dictionary2.Add("particles/econ/items/viper/viper_immortal_tail_ti8/viper_immortal_ti8_nethertoxin_proj.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.viper_nethertoxin
			});
			dictionary2.Add("particles/units/heroes/hero_viper/viper_viper_strike_warmup.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.viper_viper_strike,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_visage/visage_grave_chill_caster.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.visage_grave_chill
			});
			dictionary2.Add("particles/units/heroes/hero_visage/visage_soul_assumption_beams.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.visage_soul_assumption,
				RawParticlePosition = true
			});
			dictionary2.Add("particles/units/heroes/hero_visage/visage_summon_familiars.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.visage_summon_familiars,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_warlock/warlock_fatal_bonds_hit_parent.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.warlock_fatal_bonds
			});
			dictionary2.Add("particles/units/heroes/hero_warlock/warlock_shadow_word_debuff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.warlock_shadow_word,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_warlock/warlock_upheaval.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.warlock_upheaval,
				RawParticlePosition = true
			});
			dictionary2.Add("particles/econ/items/warlock/warlock_staff_hellborn/warlock_upheaval_hellborn.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.warlock_upheaval,
				RawParticlePosition = true
			});
			dictionary2.Add("particles/units/heroes/hero_warlock/warlock_rain_of_chaos_staff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.warlock_rain_of_chaos
			});
			dictionary2.Add("particles/units/heroes/hero_weaver/weaver_swarm_debuff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.weaver_the_swarm,
				Replace = true
			});
			dictionary2.Add("particles/units/heroes/hero_weaver/weaver_shukuchi.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.weaver_shukuchi,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/econ/items/weaver/weaver_immortal_ti6/weaver_immortal_ti6_shukuchi.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.weaver_shukuchi,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/econ/items/weaver/weaver_immortal_ti6/weaver_immortal_ti6_shukuchi_portal.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.weaver_shukuchi
			});
			dictionary2.Add("particles/units/heroes/hero_windrunner/windrunner_shackleshot_single.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.windrunner_shackleshot,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_windrunner/windrunner_shackleshot_pair.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.windrunner_shackleshot,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_windrunner/windrunner_shackleshot_pair_tree.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.windrunner_shackleshot,
				SearchOwner = true
			});
			dictionary2.Add("particles/units/heroes/hero_winter_wyvern/wyvern_arctic_burn_start.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.winter_wyvern_arctic_burn
			});
			dictionary2.Add("particles/units/heroes/hero_winter_wyvern/wyvern_cold_embrace_start.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.winter_wyvern_cold_embrace
			});
			dictionary2.Add("particles/units/heroes/hero_winter_wyvern/wyvern_cold_embrace_buff.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.winter_wyvern_cold_embrace
			});
			dictionary2.Add("particles/units/heroes/hero_witchdoctor/witchdoctor_maledict_aoe.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.witch_doctor_maledict
			});
			dictionary2.Add("particles/econ/items/witch_doctor/wd_ti8_immortal_head/wd_ti8_immortal_maledict_aoe.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.witch_doctor_maledict
			});
			dictionary2.Add("particles/units/heroes/hero_skeletonking/skeletonking_hellfireblast_warmup.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.skeleton_king_hellfire_blast
			});
			dictionary2.Add("particles/units/heroes/hero_skeletonking/wraith_king_vampiric_aura_lifesteal.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.skeleton_king_vampiric_aura,
				Replace = true,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/units/heroes/hero_skeletonking/wraith_king_reincarnate.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.skeleton_king_reincarnation
			});
			dictionary2.Add("particles/units/heroes/hero_zuus/zuus_arc_lightning_head.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.zuus_arc_lightning
			});
			dictionary2.Add("particles/econ/items/zeus/zeus_ti8_immortal_arms/zeus_ti8_immortal_arc_head.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.zuus_arc_lightning
			});
			dictionary2.Add("particles/units/heroes/hero_zuus/zuus_lightning_bolt_start.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.zuus_lightning_bolt
			});
			dictionary2.Add("particles/units/heroes/hero_zuus/zuus_thundergods_wrath_start.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.zuus_thundergods_wrath,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/econ/items/zeus/arcana_chariot/zeus_arcana_thundergods_wrath_start.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.zuus_thundergods_wrath,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/econ/items/zeus/arcana_chariot/zeus_arcana_thundergods_wrath.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.zuus_thundergods_wrath,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/items2_fx/smoke_of_deceit.vpcf", new SmokeAbilityData
			{
				AbilityId = AbilityId.item_smoke_of_deceit,
				ShowNotification = true
			});
			dictionary2.Add("particles/items_fx/blink_dagger_start.vpcf", new BlinkItemAbilityData
			{
				AbilityId = AbilityId.item_blink
			});
			dictionary2.Add("particles/econ/items/zeus/arcana_chariot/zeus_arcana_blink_start.vpcf", new BlinkItemAbilityData
			{
				AbilityId = AbilityId.item_blink
			});
			dictionary2.Add("particles/items_fx/battlefury_cleave.vpcf", new CleaveAbilityData
			{
				AbilityId = AbilityId.item_bfury,
				ControlPoint = 2u,
				Replace = true
			});
			dictionary2.Add("particles/items2_fx/refresher.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.item_refresher
			});
			dictionary2.Add("particles/items2_fx/vanguard_active_launch.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.item_crimson_guard,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/items2_fx/pipe_of_insight.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.item_hood_of_defiance,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/items2_fx/pipe_of_insight_launch.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.item_pipe,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/items2_fx/vanguard_active_impact.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.item_crimson_guard,
				ControlPoint = 1u,
				Replace = true
			});
			dictionary2.Add("particles/items_fx/dagon.vpcf", new NoOwnerItemData
			{
				AbilityId = AbilityId.item_dagon_5
			});
			dictionary2.Add("particles/items2_fx/urn_of_shadows.vpcf", new NoOwnerItemData
			{
				AbilityId = AbilityId.item_urn_of_shadows
			});
			dictionary2.Add("particles/items3_fx/glimmer_cape_initial_flash.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.item_glimmer_cape
			});
			dictionary2.Add("particles/items2_fx/veil_of_discord.vpcf", new NoOwnerItemData
			{
				AbilityId = AbilityId.item_veil_of_discord
			});
			dictionary2.Add("particles/items2_fx/hand_of_midas.vpcf", new NoOwnerItemData
			{
				AbilityId = AbilityId.item_hand_of_midas,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/items4_fx/spirit_vessel_cast.vpcf", new NoOwnerItemData
			{
				AbilityId = AbilityId.item_spirit_vessel
			});
			dictionary2.Add("particles/items2_fx/shivas_guard_impact.vpcf", new NoOwnerItemData
			{
				AbilityId = AbilityId.item_shivas_guard,
				SearchOwner = true,
				Replace = true
			});
			dictionary2.Add("particles/items_fx/bloodstone_heal.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.item_bloodstone,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/items4_fx/meteor_hammer_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.item_meteor_hammer,
				ControlPoint = 1u
			});
			dictionary2.Add("particles/items_fx/chain_lightning.vpcf", new MaelstormAbilityData
			{
				AbilityId = AbilityId.item_maelstrom,
				Replace = true
			});
			dictionary2.Add("particles/neutral_fx/neutral_centaur_khan_war_stomp.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.centaur_khan_war_stomp
			});
			dictionary2.Add("particles/neutral_fx/ursa_thunderclap.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.polar_furbolg_ursa_warrior_thunder_clap,
				RawParticlePosition = true
			});
			dictionary2.Add("particles/neutral_fx/satyr_hellcaller_cast.vpcf", new AbilityFullData
			{
				AbilityId = AbilityId.satyr_hellcaller_shockwave
			});
			this.Particles = dictionary2;
			this.AbilityUnitVision = new Dictionary<AbilityId, AbilityFullData>
			{
				{
					AbilityId.shredder_timber_chain,
					new AbilityFullData
					{
						Vision = 100,
						IgnoreUnitAbility = true
					}
				},
				{
					AbilityId.rattletrap_hookshot,
					new AbilityFullData
					{
						Vision = 100,
						IgnoreUnitAbility = true
					}
				},
				{
					AbilityId.weaver_the_swarm,
					new AbilityFullData
					{
						Vision = 100,
						IgnoreUnitAbility = true
					}
				},
				{
					AbilityId.grimstroke_dark_artistry,
					new AbilityFullData
					{
						Vision = 160,
						IgnoreUnitAbility = true
					}
				},
				{
					AbilityId.tusk_ice_shards,
					new AbilityFullData
					{
						Vision = 200
					}
				},
				{
					AbilityId.tiny_toss_tree,
					new AbilityFullData
					{
						Vision = 200
					}
				},
				{
					AbilityId.batrider_flamebreak,
					new AbilityFullData
					{
						Vision = 300,
						IgnoreUnitAbility = true
					}
				},
				{
					AbilityId.medusa_mystic_snake,
					new AbilityFullData
					{
						Vision = 300,
						IgnoreUnitAbility = true
					}
				},
				{
					AbilityId.oracle_fortunes_end,
					new AbilityFullData
					{
						Vision = 300,
						IgnoreUnitAbility = true
					}
				},
				{
					AbilityId.meepo_earthbind,
					new AbilityFullData
					{
						Vision = 300,
						IgnoreUnitAbility = true
					}
				},
				{
					AbilityId.kunkka_ghostship,
					new AbilityFullData
					{
						Vision = 400,
						IgnoreUnitAbility = true
					}
				},
				{
					AbilityId.venomancer_venomous_gale,
					new AbilityFullData
					{
						Vision = 400,
						IgnoreUnitAbility = true
					}
				},
				{
					AbilityId.shadow_demon_shadow_poison,
					new AbilityFullData
					{
						Vision = 400
					}
				},
				{
					AbilityId.disruptor_glimpse,
					new AbilityFullData
					{
						Vision = 400,
						IgnoreUnitAbility = true
					}
				},
				{
					AbilityId.phantom_assassin_stifling_dagger,
					new AbilityFullData
					{
						Vision = 450
					}
				},
				{
					AbilityId.ancient_apparition_ice_blast,
					new AbilityFullData
					{
						Vision = 550
					}
				}
			};
			this.AddAbility(AbilityId.mirana_arrow, "arrow_vision", false, true);
			this.AddAbility(AbilityId.arc_warden_spark_wraith, "wraith_vision_radius", false, false);
			this.AddAbility(AbilityId.rattletrap_rocket_flare, "vision_radius", true, false);
			this.AddAbility(AbilityId.invoker_tornado, "vision_distance", false, false);
			this.AddAbility(AbilityId.puck_illusory_orb, "orb_vision", false, false);
			this.AddAbility(AbilityId.skywrath_mage_arcane_bolt, "bolt_vision", false, false);
			this.AddAbility(AbilityId.skywrath_mage_concussive_shot, "shot_vision", true, false);
			this.AddAbility(AbilityId.sven_storm_bolt, "vision_radius", true, false);
			this.AddAbility(AbilityId.invoker_chaos_meteor, "vision_distance", true, false);
			this.AddAbility(AbilityId.windrunner_powershot, "vision_radius", false, false);
			this.AddAbility(AbilityId.storm_spirit_ball_lightning, "ball_lightning_vision_radius", true, false);
			this.AddAbility(AbilityId.vengefulspirit_wave_of_terror, "vision_aoe", false, false);
			this.AddAbility(AbilityId.lich_chain_frost, "vision_radius", true, false);
			this.AddAbility(AbilityId.mars_spear, "spear_vision", false, false);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600028E RID: 654 RVA: 0x000039F7 File Offset: 0x00001BF7
		public Dictionary<string, AbilityFullData> Units { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600028F RID: 655 RVA: 0x000039FF File Offset: 0x00001BFF
		public Dictionary<string, AbilityFullData> Particles { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00003A07 File Offset: 0x00001C07
		public Dictionary<AbilityId, AbilityFullData> AbilityUnitVision { get; }

		// Token: 0x06000291 RID: 657 RVA: 0x0001A3A0 File Offset: 0x000185A0
		private void AddAbility(AbilityId id, string specialData, bool ignore = false, bool notification = false)
		{
			this.AbilityUnitVision.Add(id, new AbilityFullData
			{
				Vision = (int)Ability.GetAbilityDataById(id).AbilitySpecialData.First((AbilitySpecialData x) => x.Name == specialData).Value,
				IgnoreUnitAbility = ignore,
				ShowNotification = notification
			});
		}
	}
}
