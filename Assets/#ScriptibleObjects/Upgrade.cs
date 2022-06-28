using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    public UpgradeType upgradeType;

    public int UpgradeCost;

    //** DRILL **//
    [HideInInspector] public int Drill_Cooldown;
    [HideInInspector] public int Drill_CooldownLevel;
    [HideInInspector] public int Drill_EfficiencyLevel;
    [HideInInspector] public int Drill_FuelUsingAmount;
    [HideInInspector] public int Drill_GoldPerDigging;
    [HideInInspector] public int Drill_RareMinePercent;

    //** PLAYER **//

    [HideInInspector] public int Player_Damage;
    [HideInInspector] public int Player_DamageLevel;
    [HideInInspector] public int Player_Health;
    [HideInInspector] public int Player_HealthLevel;
    [HideInInspector] public int Player_FillSpeed;
    [HideInInspector] public int Player_FillSpeedLevel;

    //** OIL PUMP STATION **//

    [HideInInspector] public int OilPump_ExtractionAmount;
    [HideInInspector] public int OilPump_ExtractionAmountLevel;
    [HideInInspector] public int OilPump_FuelCapacity;
    [HideInInspector] public int OilPump_FuelCapacityLevel;
    [HideInInspector] public int OilPump_Durability;
    [HideInInspector] public int OilPump_DurabilityLevel;

    //** GRINDER STATION **//
    [HideInInspector] public int Grinder_GoldIncome;
    [HideInInspector] public int Grinder_ScrapRequest;
    [HideInInspector] public int Grinder_ConvertTime;
    [HideInInspector] public int Grinder_EfficiencyLevel;
    [HideInInspector] public int Grinder_Durability;
    [HideInInspector] public int Grinder_DurabilityLevel;

    //** WARRIOR STATION **//

    [HideInInspector] public int Warrior_Damage;
    [HideInInspector] public int Warrior_DamageLevel;
    [HideInInspector] public int Warrior_Health;
    [HideInInspector] public int Warrior_HealthLevel;
    [HideInInspector] public int Warrior_Amount;
    [HideInInspector] public int Warrior_AmountLevel;
}

public enum UpgradeType
{
    DRILL_EFFICIENCY,
    DRILL_COOLDOWN,
    CHARACTER_DAMAGE,
    CHARACTER_HEALTH,
    CHARACTER_FILLSPEED,
    BASE_WALLS_DURABILITY,
    BASE_TOWER_DURABILITY,
    OILPUMP_EFFICIENCY,
    OILPUMP_DURABILITY,
    GRINDER_EFFICIENCY,
    GRINDER_DURABILITY,
    WARRIOR_DAMAGE,
    WARRIOR_HEALTH,
    WARRIOR_AMOUNT
}

