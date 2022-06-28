using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [Space(10), Header("***DRILL***")]
    public TextMeshProUGUI drillEfficiencyLevelText;
    public TextMeshProUGUI drillEfficiencyPriceText;
    public TextMeshProUGUI drillCooldownLevelText;
    public TextMeshProUGUI drillCooldownPriceText;

    public List<Upgrade> drillEfficiencyUpgrades = new List<Upgrade>();
    public List<Upgrade> drillCooldownUpgrades = new List<Upgrade>();

    [Header("Drill Properties")]
    public IntVariable drillFuelUsingAmount;
    public IntVariable drillGoldPerDigging;
    public IntVariable drillRareMinePercent;
    public IntVariable drillCooldown;
    public IntVariable drillCooldownLevel;
    public IntVariable drillEfficiencyLevel;

    [Space(10), Header("***PLAYER***")]
    public TextMeshProUGUI playerHealthLevelText;
    public TextMeshProUGUI playerHealthPriceText;
    public TextMeshProUGUI playerDamageLevelText;
    public TextMeshProUGUI playerDamagePriceText;

    public List<Upgrade> playerDamageUpgrades = new List<Upgrade>();
    public List<Upgrade> playerHealthUpgrades = new List<Upgrade>();

    [Header("Player Properties")]
    public IntVariable playerGold;
    public IntVariable playerLevel;
    public IntVariable playerDamage;
    public IntVariable playerHealth;
    public IntVariable playerHealthLevel;
    public IntVariable playerDamageLevel;

    private void Update()
    {
        drillEfficiencyLevelText.SetText("LV. " + drillEfficiencyLevel.Value);
        drillCooldownLevelText.SetText("LV. " + drillCooldownLevel.Value);

        if(drillEfficiencyUpgrades.Count > 0) drillEfficiencyPriceText.SetText("$" + drillEfficiencyUpgrades[drillEfficiencyLevel.Value - 1].UpgradeCost);
        if(drillCooldownUpgrades.Count > 0) drillCooldownPriceText.SetText("$" + drillCooldownUpgrades[drillCooldownLevel.Value - 1].UpgradeCost);

        playerDamageLevelText.SetText("LV. " + playerDamageLevel.Value);
        playerHealthLevelText.SetText("LV. " + playerHealthLevel.Value);

        if(playerHealthUpgrades.Count > 0) playerHealthPriceText.SetText("$" + playerHealthUpgrades[playerHealthLevel.Value - 1].UpgradeCost);
        if (playerDamageUpgrades.Count > 0) playerDamagePriceText.SetText("$" + playerDamageUpgrades[playerDamageLevel.Value - 1].UpgradeCost);
    }

    public void Close_UpgradePanels()
    {
        gameObject.SetActive(false);
    }

    #region Drill Upgrades
    public void Upgrade_Drill_Efficiency()
    {
        if (drillEfficiencyLevel.Value == drillCooldownUpgrades.Count) return;
        Upgrade upgrade = drillEfficiencyUpgrades[drillEfficiencyLevel.Value - 1];
        if ((playerGold.Value - upgrade.UpgradeCost) >= 0)
        {
            playerGold.Value -= upgrade.UpgradeCost;
            drillFuelUsingAmount.Value = upgrade.Drill_FuelUsingAmount;
            drillGoldPerDigging.Value = upgrade.Drill_GoldPerDigging;
            drillRareMinePercent.Value = upgrade.Drill_RareMinePercent;
            drillEfficiencyLevel.Value = upgrade.Drill_EfficiencyLevel;
        }
    }
    public void Upgrade_Drill_Cooldown()
    {
        if (drillCooldownLevel.Value == drillCooldownUpgrades.Count) return;
        Upgrade upgrade = drillCooldownUpgrades[drillCooldownLevel.Value - 1];
        if ((playerGold.Value - upgrade.UpgradeCost) >= 0)
        {
            playerGold.Value -= upgrade.UpgradeCost;
            drillCooldown.Value = upgrade.Drill_Cooldown;
            drillCooldownLevel.Value = upgrade.Drill_CooldownLevel;
        }
    }
    #endregion

    #region Player Upgrades
    public void Upgrade_Player_Damage()
    {
        if (playerDamageLevel.Value == playerDamageUpgrades.Count) return;
        Upgrade upgrade = playerDamageUpgrades[playerDamageLevel.Value - 1];
        if ((playerGold.Value - upgrade.UpgradeCost) >= 0)
        {
            playerGold.Value -= upgrade.UpgradeCost;
            playerDamage.Value = upgrade.Player_Damage;
            playerDamageLevel.Value= upgrade.Player_DamageLevel;
        }
    }

    public void Upgrade_Player_Health()
    {
        if (playerHealthLevel.Value == playerHealthUpgrades.Count) return;
        Upgrade upgrade = playerHealthUpgrades[playerHealthLevel.Value - 1];
        if ((playerGold.Value - upgrade.UpgradeCost) >= 0)
        {
            playerGold.Value -= upgrade.UpgradeCost;
            playerHealth.Value = upgrade.Player_Health;
            playerHealthLevel.Value = upgrade.Player_HealthLevel;
        }
    }
    #endregion
}
