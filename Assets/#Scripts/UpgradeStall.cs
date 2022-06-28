using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStall : MonoBehaviour
{
    public GameObject UpgradesPanel;
    public GameObject drillUpgradePanel;
    public GameObject characterUpgradePanel;
    public GameObject baseUpgradePanel;
    public GameObject warriorUpgradePanel;
    public GameObject oilPumpStationUpgradePanel;
    public GameObject grinderStationUpgradePanel;
    public UpgradeStallType stallType;

    private void TogglePanel(bool x)
    {
        switch (stallType)
        {
            case UpgradeStallType.DRILL_UPGRADE: drillUpgradePanel.SetActive(x); break;
            case UpgradeStallType.CHARACTER_UPGRADE: characterUpgradePanel.SetActive(x); break;
            case UpgradeStallType.BASE_UPGRADE: baseUpgradePanel.SetActive(x); break;
            case UpgradeStallType.WARRIOR_UPGRADE: warriorUpgradePanel.SetActive(x); break;
            case UpgradeStallType.OILPUMPSTATION_UPGRADE: oilPumpStationUpgradePanel.SetActive(x); break;
            case UpgradeStallType.GRINDER_UPGRADE: grinderStationUpgradePanel.SetActive(x); break;
        }
        UpgradesPanel.SetActive(x);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TogglePanel(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TogglePanel(false);
        }
    }
}

public enum UpgradeStallType
{
    NONE,
    DRILL_UPGRADE,
    CHARACTER_UPGRADE,
    BASE_UPGRADE,
    WARRIOR_UPGRADE,
    OILPUMPSTATION_UPGRADE,
    GRINDER_UPGRADE
}
