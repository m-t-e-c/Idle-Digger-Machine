using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Upgrade))]
public class UpgradeEditor : Editor
{

    Upgrade upgrade;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        upgrade = (Upgrade)target;
        EditorGUILayout.Space(10);

        switch (upgrade.upgradeType)
        {
            case UpgradeType.DRILL_EFFICIENCY:
                EditorGUILayout.LabelField("Drill Upgrade Values");
                Draw_DrillEfficiency();
                break;
            case UpgradeType.DRILL_COOLDOWN:
                EditorGUILayout.LabelField("Drill Cooldown Values");
                Draw_DrillCooldown();
                break;

            case UpgradeType.CHARACTER_DAMAGE:
                EditorGUILayout.LabelField("Character Damage Values");
                Draw_CharacterDamage();
                break;
            case UpgradeType.CHARACTER_HEALTH:
                EditorGUILayout.LabelField("Character Health Values");
                Draw_CharacterHealth();
                break;
            case UpgradeType.CHARACTER_FILLSPEED:
                EditorGUILayout.LabelField("Character FillSpeed Values");
                Draw_CharacterFillSpeed();
                break;

            case UpgradeType.OILPUMP_EFFICIENCY:
                EditorGUILayout.LabelField("OilPump Efficiency Values");
                Draw_OilPumpEfficiency();
                break;
            case UpgradeType.OILPUMP_DURABILITY:
                EditorGUILayout.LabelField("OilPump Durability Values");
                Draw_OilPumpDurability();
                break;
            case UpgradeType.GRINDER_EFFICIENCY:
                EditorGUILayout.LabelField("Grinder Efficiency Values");
                Draw_GrinderEfficiency();
                break;
            case UpgradeType.GRINDER_DURABILITY:
                EditorGUILayout.LabelField("Grinder Durability Values");
                Draw_GrinderDurability();
                break;
            case UpgradeType.WARRIOR_HEALTH:
                EditorGUILayout.LabelField("Warrior Health Values");
                Draw_WarriorHealth();
                break;
            case UpgradeType.WARRIOR_DAMAGE:
                EditorGUILayout.LabelField("Warrior Damage Values");
                Draw_WarriorDamage();
                break;
            case UpgradeType.WARRIOR_AMOUNT:
                EditorGUILayout.LabelField("Warrior Amount Values");
                Draw_WarriorAmount();
                break;

        }
        EditorGUILayout.Space(10);
    }
    private void Draw_DrillEfficiency()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Fuel Using Amount");
        upgrade.Drill_FuelUsingAmount = EditorGUILayout.IntField(upgrade.Drill_FuelUsingAmount);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Gold Per Digging");
        upgrade.Drill_GoldPerDigging = EditorGUILayout.IntField(upgrade.Drill_GoldPerDigging);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Rare Mine Percent");
        upgrade.Drill_RareMinePercent = EditorGUILayout.IntField(upgrade.Drill_RareMinePercent);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("EfficiencyLevel");
        upgrade.Drill_EfficiencyLevel = EditorGUILayout.IntField(upgrade.Drill_EfficiencyLevel);
        EditorGUILayout.EndHorizontal();
    }
    private void Draw_DrillCooldown()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cooldown");
        upgrade.Drill_Cooldown = EditorGUILayout.IntField(upgrade.Drill_Cooldown);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cooldown Level");
        upgrade.Drill_CooldownLevel = EditorGUILayout.IntField(upgrade.Drill_CooldownLevel);
        EditorGUILayout.EndHorizontal();
    }

    private void Draw_CharacterDamage()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Damage");
        upgrade.Player_Damage = EditorGUILayout.IntField(upgrade.Player_Damage);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Damage Level");
        upgrade.Player_DamageLevel = EditorGUILayout.IntField(upgrade.Player_DamageLevel);
        EditorGUILayout.EndHorizontal();
    }
    private void Draw_CharacterHealth()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Health");
        upgrade.Player_Health = EditorGUILayout.IntField(upgrade.Player_Health);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Health Level");
        upgrade.Player_HealthLevel = EditorGUILayout.IntField(upgrade.Player_HealthLevel);
        EditorGUILayout.EndHorizontal();
    }
    private void Draw_CharacterFillSpeed()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Fill Speed");
        upgrade.Player_FillSpeed = EditorGUILayout.IntField(upgrade.Player_FillSpeed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Fill Speed Level");
        upgrade.Player_FillSpeedLevel = EditorGUILayout.IntField(upgrade.Player_FillSpeedLevel);
        EditorGUILayout.EndHorizontal();
    }

    private void Draw_OilPumpEfficiency()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Extraction Amount");
        upgrade.OilPump_ExtractionAmount = EditorGUILayout.IntField(upgrade.OilPump_ExtractionAmount);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Extraction Level");
        upgrade.OilPump_ExtractionAmountLevel = EditorGUILayout.IntField(upgrade.OilPump_ExtractionAmountLevel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Fuel Capacity");
        upgrade.OilPump_FuelCapacity = EditorGUILayout.IntField(upgrade.OilPump_FuelCapacity);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Fuel Capacity Level");
        upgrade.OilPump_FuelCapacityLevel = EditorGUILayout.IntField(upgrade.OilPump_FuelCapacityLevel);
        EditorGUILayout.EndHorizontal();

    }
    private void Draw_OilPumpDurability()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Durability");
        upgrade.OilPump_Durability = EditorGUILayout.IntField(upgrade.OilPump_Durability);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Durability Level");
        upgrade.OilPump_DurabilityLevel = EditorGUILayout.IntField(upgrade.OilPump_DurabilityLevel);
        EditorGUILayout.EndHorizontal();
    }

    private void Draw_GrinderEfficiency()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Gold Income");
        upgrade.Grinder_GoldIncome = EditorGUILayout.IntField(upgrade.Grinder_GoldIncome);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Scrap Request");
        upgrade.Grinder_ScrapRequest = EditorGUILayout.IntField(upgrade.Grinder_ScrapRequest);
        EditorGUILayout.EndHorizontal(); 
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Convert Time");
        upgrade.Grinder_ConvertTime = EditorGUILayout.IntField(upgrade.Grinder_ConvertTime);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Efficiency Level");
        upgrade.Grinder_EfficiencyLevel = EditorGUILayout.IntField(upgrade.Grinder_EfficiencyLevel);
        EditorGUILayout.EndHorizontal();
    }
    private void Draw_GrinderDurability()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Durability");
        upgrade.Grinder_Durability = EditorGUILayout.IntField(upgrade.Grinder_Durability);
        EditorGUILayout.EndHorizontal(); 
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Durability Level");
        upgrade.Grinder_DurabilityLevel = EditorGUILayout.IntField(upgrade.Grinder_DurabilityLevel);
        EditorGUILayout.EndHorizontal();
    }

    private void Draw_WarriorDamage()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Damage");
        upgrade.Warrior_Damage = EditorGUILayout.IntField(upgrade.Warrior_Damage);
        EditorGUILayout.EndHorizontal(); 
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Damage Level");
        upgrade.Warrior_DamageLevel = EditorGUILayout.IntField(upgrade.Warrior_DamageLevel);
        EditorGUILayout.EndHorizontal();
    }
    private void Draw_WarriorHealth()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Health");
        upgrade.Warrior_Health = EditorGUILayout.IntField(upgrade.Warrior_Health);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Health Level");
        upgrade.Warrior_HealthLevel = EditorGUILayout.IntField(upgrade.Warrior_HealthLevel);
        EditorGUILayout.EndHorizontal();
    }
    private void Draw_WarriorAmount()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Amount");
        upgrade.Warrior_Amount = EditorGUILayout.IntField(upgrade.Warrior_Amount);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Amount Level");
        upgrade.Warrior_AmountLevel = EditorGUILayout.IntField(upgrade.Warrior_AmountLevel);
        EditorGUILayout.EndHorizontal();
    }
}
