﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClanManager.Models
{
    public class CharacterRawJSONData
    {
        public Records records { get; set; }

        public CharacterRawJSONData(Records r)
        {
            this.records = r;
        }
        [Serializable]
        public class BaseAbility
        {
            public double attack_power_value { get; set; }
            public double attack_pierce_value { get; set; }
            public double attack_defend_pierce_rate { get; set; }
            public double attack_parry_pierce_rate { get; set; }
            public double attack_hit_value { get; set; }
            public double attack_hit_rate { get; set; }
            public double attack_concentrate_value { get; set; }
            public double attack_perfect_parry_damage_rate { get; set; }
            public double attack_counter_damage_rate { get; set; }
            public double attack_critical_value { get; set; }
            public double attack_critical_rate { get; set; }
            public double attack_stiff_duration_level { get; set; }
            public double attack_damage_modify_diff { get; set; }
            public double attack_damage_modify_rate { get; set; }
            public double hate_power_value { get; set; }
            public double hate_power_rate { get; set; }
            public double max_hp { get; set; }
            public double defend_power_value { get; set; }
            public double defend_physical_damage_reduce_rate { get; set; }
            public double aoe_defend_power_value { get; set; }
            public double aoe_defend_damage_reduce_rate { get; set; }
            public double defend_dodge_value { get; set; }
            public double defend_dodge_rate { get; set; }
            public double counter_damage_reduce_rate { get; set; }
            public double defend_parry_value { get; set; }
            public double defend_parry_reduce_rate { get; set; }
            public double perfect_parry_damage_reduce_rate { get; set; }
            public double defend_parry_rate { get; set; }
            public double defend_critical_value { get; set; }
            public double defend_critical_rate { get; set; }
            public double defend_critical_damage_rate { get; set; }
            public double defend_stiff_duration_level { get; set; }
            public double defend_damage_modify_diff { get; set; }
            public double defend_damage_modify_rate { get; set; }
            public double hp_regen { get; set; }
            public double hp_regen_combat { get; set; }
            public double heal_power_rate { get; set; }
            public double heal_power_value { get; set; }
            public double heal_power_diff { get; set; }
            public int attack_critical_damage_value { get; set; }
            public double attack_critical_damage_rate { get; set; }
            public int attack_attribute_fire_value { get; set; }
            public double attack_attribute_fire_rate { get; set; }
            public int attack_attribute_ice_value { get; set; }
            public double attack_attribute_ice_rate { get; set; }
            public int attack_attribute_wind_value { get; set; }
            public double attack_attribute_wind_rate { get; set; }
            public int attack_attribute_earth_value { get; set; }
            public double attack_attribute_earth_rate { get; set; }
            public int attack_attribute_lightning_value { get; set; }
            public double attack_attribute_lightning_rate { get; set; }
            public int attack_attribute_void_value { get; set; }
            public double attack_attribute_void_rate { get; set; }
            public int abnormal_attack_power_value { get; set; }
            public double abnormal_attack_power_rate { get; set; }
            public int pc_attack_power_value { get; set; }
            public int boss_attack_power_value { get; set; }
            public int pc_defend_power_value { get; set; }
            public double pc_defend_power_rate { get; set; }
            public int boss_defend_power_value { get; set; }
            public double boss_defend_power_rate { get; set; }
            public int abnormal_defend_power_value { get; set; }
            public double abnormal_defend_power_rate { get; set; }
            public int int_attack_power_value { get; set; }
            public int int_attack_pierce_value { get; set; }
            public int int_attack_defend_pierce_rate { get; set; }
            public int int_attack_parry_pierce_rate { get; set; }
            public int int_attack_hit_value { get; set; }
            public int int_attack_hit_rate { get; set; }
            public int int_attack_concentrate_value { get; set; }
            public int int_attack_perfect_parry_damage_rate { get; set; }
            public int int_attack_counter_damage_rate { get; set; }
            public int int_attack_critical_value { get; set; }
            public int int_attack_critical_rate { get; set; }
            public int int_attack_critical_damage_rate { get; set; }
            public int int_attack_stiff_duration_level { get; set; }
            public int int_attack_damage_modify_diff { get; set; }
            public int int_attack_damage_modify_rate { get; set; }
            public int int_hate_power_value { get; set; }
            public int int_hate_power_rate { get; set; }
            public int int_max_hp { get; set; }
            public int int_defend_power_value { get; set; }
            public int int_aoe_defend_power_value { get; set; }
            public int int_defend_physical_damage_reduce_rate { get; set; }
            public int int_aoe_defend_damage_reduce_rate { get; set; }
            public int int_defend_dodge_value { get; set; }
            public int int_defend_dodge_rate { get; set; }
            public int int_counter_damage_reduce_rate { get; set; }
            public int int_defend_parry_value { get; set; }
            public int int_defend_parry_reduce_rate { get; set; }
            public int int_perfect_parry_damage_reduce_rate { get; set; }
            public int int_defend_parry_rate { get; set; }
            public int int_defend_critical_value { get; set; }
            public int int_defend_critical_rate { get; set; }
            public int int_defend_critical_damage_rate { get; set; }
            public int int_defend_stiff_duration_level { get; set; }
            public int int_defend_damage_modify_diff { get; set; }
            public int int_defend_damage_modify_rate { get; set; }
            public int int_hp_regen { get; set; }
            public int int_hp_regen_combat { get; set; }
            public int int_heal_power_rate { get; set; }
            public int int_heal_power_value { get; set; }
            public int int_heal_power_diff { get; set; }
        }
        [Serializable]
        public class EquippedAbility
        {
            public double attack_power_value { get; set; }
            public double attack_pierce_value { get; set; }
            public double attack_defend_pierce_rate { get; set; }
            public double attack_parry_pierce_rate { get; set; }
            public double attack_hit_value { get; set; }
            public double attack_hit_rate { get; set; }
            public double attack_concentrate_value { get; set; }
            public double attack_perfect_parry_damage_rate { get; set; }
            public double attack_counter_damage_rate { get; set; }
            public double attack_critical_value { get; set; }
            public double attack_critical_rate { get; set; }
            public double attack_stiff_duration_level { get; set; }
            public double attack_damage_modify_diff { get; set; }
            public double attack_damage_modify_rate { get; set; }
            public double hate_power_value { get; set; }
            public double hate_power_rate { get; set; }
            public double max_hp { get; set; }
            public double defend_power_value { get; set; }
            public double defend_physical_damage_reduce_rate { get; set; }
            public double aoe_defend_power_value { get; set; }
            public double aoe_defend_damage_reduce_rate { get; set; }
            public double defend_dodge_value { get; set; }
            public double defend_dodge_rate { get; set; }
            public double counter_damage_reduce_rate { get; set; }
            public double defend_parry_value { get; set; }
            public double defend_parry_reduce_rate { get; set; }
            public double perfect_parry_damage_reduce_rate { get; set; }
            public double defend_parry_rate { get; set; }
            public double defend_critical_value { get; set; }
            public double defend_critical_rate { get; set; }
            public double defend_critical_damage_rate { get; set; }
            public double defend_stiff_duration_level { get; set; }
            public double defend_damage_modify_diff { get; set; }
            public double defend_damage_modify_rate { get; set; }
            public double hp_regen { get; set; }
            public double hp_regen_combat { get; set; }
            public double heal_power_rate { get; set; }
            public double heal_power_value { get; set; }
            public double heal_power_diff { get; set; }
            public int attack_critical_damage_value { get; set; }
            public double attack_critical_damage_rate { get; set; }
            public int attack_attribute_fire_value { get; set; }
            public double attack_attribute_fire_rate { get; set; }
            public int attack_attribute_ice_value { get; set; }
            public double attack_attribute_ice_rate { get; set; }
            public int attack_attribute_wind_value { get; set; }
            public double attack_attribute_wind_rate { get; set; }
            public int attack_attribute_earth_value { get; set; }
            public double attack_attribute_earth_rate { get; set; }
            public int attack_attribute_lightning_value { get; set; }
            public double attack_attribute_lightning_rate { get; set; }
            public int attack_attribute_void_value { get; set; }
            public double attack_attribute_void_rate { get; set; }
            public int abnormal_attack_power_value { get; set; }
            public double abnormal_attack_power_rate { get; set; }
            public int pc_attack_power_value { get; set; }
            public int boss_attack_power_value { get; set; }
            public int pc_defend_power_value { get; set; }
            public double pc_defend_power_rate { get; set; }
            public int boss_defend_power_value { get; set; }
            public double boss_defend_power_rate { get; set; }
            public int abnormal_defend_power_value { get; set; }
            public double abnormal_defend_power_rate { get; set; }
            public int int_attack_power_value { get; set; }
            public int int_attack_pierce_value { get; set; }
            public int int_attack_defend_pierce_rate { get; set; }
            public int int_attack_parry_pierce_rate { get; set; }
            public int int_attack_hit_value { get; set; }
            public int int_attack_hit_rate { get; set; }
            public int int_attack_concentrate_value { get; set; }
            public int int_attack_perfect_parry_damage_rate { get; set; }
            public int int_attack_counter_damage_rate { get; set; }
            public int int_attack_critical_value { get; set; }
            public int int_attack_critical_rate { get; set; }
            public int int_attack_critical_damage_rate { get; set; }
            public int int_attack_stiff_duration_level { get; set; }
            public int int_attack_damage_modify_diff { get; set; }
            public int int_attack_damage_modify_rate { get; set; }
            public int int_hate_power_value { get; set; }
            public int int_hate_power_rate { get; set; }
            public int int_max_hp { get; set; }
            public int int_defend_power_value { get; set; }
            public int int_aoe_defend_power_value { get; set; }
            public int int_defend_physical_damage_reduce_rate { get; set; }
            public int int_aoe_defend_damage_reduce_rate { get; set; }
            public int int_defend_dodge_value { get; set; }
            public int int_defend_dodge_rate { get; set; }
            public int int_counter_damage_reduce_rate { get; set; }
            public int int_defend_parry_value { get; set; }
            public int int_defend_parry_reduce_rate { get; set; }
            public int int_perfect_parry_damage_reduce_rate { get; set; }
            public int int_defend_parry_rate { get; set; }
            public int int_defend_critical_value { get; set; }
            public int int_defend_critical_rate { get; set; }
            public int int_defend_critical_damage_rate { get; set; }
            public int int_defend_stiff_duration_level { get; set; }
            public int int_defend_damage_modify_diff { get; set; }
            public int int_defend_damage_modify_rate { get; set; }
            public int int_hp_regen { get; set; }
            public int int_hp_regen_combat { get; set; }
            public int int_heal_power_rate { get; set; }
            public int int_heal_power_value { get; set; }
            public int int_heal_power_diff { get; set; }
        }
        [Serializable]
        public class TotalAbility
        {
            public double attack_power_value { get; set; }
            public double attack_pierce_value { get; set; }
            public double attack_defend_pierce_rate { get; set; }
            public double attack_parry_pierce_rate { get; set; }
            public double attack_hit_value { get; set; }
            public double attack_hit_rate { get; set; }
            public double attack_concentrate_value { get; set; }
            public double attack_perfect_parry_damage_rate { get; set; }
            public double attack_counter_damage_rate { get; set; }
            public double attack_critical_value { get; set; }
            public double attack_critical_rate { get; set; }
            public double attack_stiff_duration_level { get; set; }
            public double attack_damage_modify_diff { get; set; }
            public double attack_damage_modify_rate { get; set; }
            public double hate_power_value { get; set; }
            public double hate_power_rate { get; set; }
            public double max_hp { get; set; }
            public double defend_power_value { get; set; }
            public double defend_physical_damage_reduce_rate { get; set; }
            public double aoe_defend_power_value { get; set; }
            public double aoe_defend_damage_reduce_rate { get; set; }
            public double defend_dodge_value { get; set; }
            public double defend_dodge_rate { get; set; }
            public double counter_damage_reduce_rate { get; set; }
            public double defend_parry_value { get; set; }
            public double defend_parry_reduce_rate { get; set; }
            public double perfect_parry_damage_reduce_rate { get; set; }
            public double defend_parry_rate { get; set; }
            public double defend_critical_value { get; set; }
            public double defend_critical_rate { get; set; }
            public double defend_critical_damage_rate { get; set; }
            public double defend_stiff_duration_level { get; set; }
            public double defend_damage_modify_diff { get; set; }
            public double defend_damage_modify_rate { get; set; }
            public double hp_regen { get; set; }
            public double hp_regen_combat { get; set; }
            public double heal_power_rate { get; set; }
            public double heal_power_value { get; set; }
            public double heal_power_diff { get; set; }
            public int attack_critical_damage_value { get; set; }
            public double attack_critical_damage_rate { get; set; }
            public int attack_attribute_fire_value { get; set; }
            public double attack_attribute_fire_rate { get; set; }
            public int attack_attribute_ice_value { get; set; }
            public double attack_attribute_ice_rate { get; set; }
            public int attack_attribute_wind_value { get; set; }
            public double attack_attribute_wind_rate { get; set; }
            public int attack_attribute_earth_value { get; set; }
            public double attack_attribute_earth_rate { get; set; }
            public int attack_attribute_lightning_value { get; set; }
            public double attack_attribute_lightning_rate { get; set; }
            public int attack_attribute_void_value { get; set; }
            public double attack_attribute_void_rate { get; set; }
            public int abnormal_attack_power_value { get; set; }
            public double abnormal_attack_power_rate { get; set; }
            public int pc_attack_power_value { get; set; }
            public int boss_attack_power_value { get; set; }
            public int pc_defend_power_value { get; set; }
            public double pc_defend_power_rate { get; set; }
            public int boss_defend_power_value { get; set; }
            public double boss_defend_power_rate { get; set; }
            public int abnormal_defend_power_value { get; set; }
            public double abnormal_defend_power_rate { get; set; }
            public int int_attack_power_value { get; set; }
            public int int_attack_pierce_value { get; set; }
            public int int_attack_defend_pierce_rate { get; set; }
            public int int_attack_parry_pierce_rate { get; set; }
            public int int_attack_hit_value { get; set; }
            public int int_attack_hit_rate { get; set; }
            public int int_attack_concentrate_value { get; set; }
            public int int_attack_perfect_parry_damage_rate { get; set; }
            public int int_attack_counter_damage_rate { get; set; }
            public int int_attack_critical_value { get; set; }
            public int int_attack_critical_rate { get; set; }
            public int int_attack_critical_damage_rate { get; set; }
            public int int_attack_stiff_duration_level { get; set; }
            public int int_attack_damage_modify_diff { get; set; }
            public int int_attack_damage_modify_rate { get; set; }
            public int int_hate_power_value { get; set; }
            public int int_hate_power_rate { get; set; }
            public int int_max_hp { get; set; }
            public int int_defend_power_value { get; set; }
            public int int_aoe_defend_power_value { get; set; }
            public int int_defend_physical_damage_reduce_rate { get; set; }
            public int int_aoe_defend_damage_reduce_rate { get; set; }
            public int int_defend_dodge_value { get; set; }
            public int int_defend_dodge_rate { get; set; }
            public int int_counter_damage_reduce_rate { get; set; }
            public int int_defend_parry_value { get; set; }
            public int int_defend_parry_reduce_rate { get; set; }
            public int int_perfect_parry_damage_reduce_rate { get; set; }
            public int int_defend_parry_rate { get; set; }
            public int int_defend_critical_value { get; set; }
            public int int_defend_critical_rate { get; set; }
            public int int_defend_critical_damage_rate { get; set; }
            public int int_defend_stiff_duration_level { get; set; }
            public int int_defend_damage_modify_diff { get; set; }
            public int int_defend_damage_modify_rate { get; set; }
            public int int_hp_regen { get; set; }
            public int int_hp_regen_combat { get; set; }
            public int int_heal_power_rate { get; set; }
            public int int_heal_power_value { get; set; }
            public int int_heal_power_diff { get; set; }
        }
        [Serializable]
        public class PointAbility
        {
            public int offense_point { get; set; }
            public int defense_point { get; set; }
            public int attack_power_value { get; set; }
            public int attack_attribute_value { get; set; }
            public int max_hp { get; set; }
            public int defend_power_value { get; set; }
        }
        [Serializable]
        public class Records
        {
            public BaseAbility base_ability { get; set; }
            public EquippedAbility equipped_ability { get; set; }
            public TotalAbility total_ability { get; set; }
            public PointAbility point_ability { get; set; }
        }

        [Serializable]
        public class RootObject
        {
            public Records records { get; set; }
            public string result { get; set; }
        }
    }
}