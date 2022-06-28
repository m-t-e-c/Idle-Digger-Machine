using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldCrate : MonoBehaviour
{
    public TextMeshProUGUI diggedGoldAmountText;
    public GameObject goldBars1;
    public GameObject goldBars2;
    public GameObject goldBars3;
    public GameObject goldBars4;
    public GameObject goldBars5;

    public IntVariable drillDiggedGolds;
    public IntVariable playerGoldResource;
    public IntVariable playerTotalGold;

    public Outline outline;

    public bool active;
    private float waitTime;

    private void Update()
    {
        if (active)
        {
            if (drillDiggedGolds.Value > 0)
            {
                waitTime += Time.deltaTime;
                if (waitTime >= 0.2f)
                {
                    drillDiggedGolds.Value--;
                    playerGoldResource.Value++;
                    playerTotalGold.Value++;
                }
            }
        }

        GoldBarsControl();
    }

    private void GoldBarsControl()
    {
        if (drillDiggedGolds.Value > 10) goldBars1.SetActive(true);
        else if (drillDiggedGolds.Value < 10) goldBars1.SetActive(false);

        if (drillDiggedGolds.Value > 20) goldBars2.SetActive(true);
        else if (drillDiggedGolds.Value < 20) goldBars2.SetActive(false);

        if (drillDiggedGolds.Value > 40) goldBars3.SetActive(true);
        else if (drillDiggedGolds.Value < 40) goldBars3.SetActive(false);

        if (drillDiggedGolds.Value > 80) goldBars4.SetActive(true);
        else if (drillDiggedGolds.Value < 80) goldBars4.SetActive(false);

        if (drillDiggedGolds.Value > 120) goldBars5.SetActive(true);
        else if (drillDiggedGolds.Value < 120) goldBars5.SetActive(false);

        diggedGoldAmountText.SetText(drillDiggedGolds.Value.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            active = true;
            outline.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            active = false;
            outline.enabled = false;
        }
    }
}
