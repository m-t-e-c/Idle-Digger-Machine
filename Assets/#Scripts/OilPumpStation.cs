using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OilPumpStation : MonoBehaviour
{
    public Animator anim;
    public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    public List<Material> defaultMaterials = new List<Material>();
    public Material unPuchasedMat;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI fuelAmountText;

    public GameObject fuelAmountHolder;
    public GameObject priceHolder;

    public BoxCollider pumpCol;
    public SphereCollider purchaseCollider;
    public SphereCollider collectCollider;

    [Header("Player Properties")]
    public IntVariable playerGold;
    public IntVariable playerFuelResource;

    [Header("Oil Pump Unique Properties")]
    public IntVariable durability;
    public IntVariable price;
    public IntVariable paidAmount;
    public BoolVariable purchasedStatus;
    public IntVariable fuelStorage;

    [Header("Oil Pump Common Properties")]
    public IntVariable extractionAmount;
    public IntVariable fuelCapacity;

    public Outline outline;

    public bool active;

    private float waitTime;
    private float extractionWaitTime;

    private void Start()
    {
        for(int i = 0; i < meshRenderers.Count; i++)
        {
            defaultMaterials.Add(meshRenderers[i].material);
            if (purchasedStatus.status) meshRenderers[i].material = defaultMaterials[i];
            else meshRenderers[i].material = unPuchasedMat;
        }
    }

    private void Update()
    {
        CheckPurchaseStatus();
        FuelCollectControl();
        FuelExtraction();
    }

    private void CheckPurchaseStatus()
    {
        if (active)
        {
            if (purchasedStatus.status) return;
            waitTime += Time.deltaTime;
            if (waitTime >= 0.01f)
            {
                waitTime = 0;
                if (playerGold.Value > 0)
                {
                    playerGold.Value--;
                    paidAmount.Value++;
                }

                if (paidAmount.Value == price.Value)
                {
                    purchasedStatus.status = true;
                    active = false;
                    for (int i = 0; i < meshRenderers.Count; i++)
                    {
                        meshRenderers[i].material = defaultMaterials[i];
                    }
                }
            }
        }
    }

    private void FuelExtraction()
    {
        if (!purchasedStatus.status) return;
        if (fuelStorage.Value == fuelCapacity.Value) return;

        extractionWaitTime += Time.deltaTime;
        if(extractionWaitTime >= 1f)
        {
            extractionWaitTime = 0;
            fuelStorage.Value+= extractionAmount.Value;
        }
    }

    private void FuelCollectControl()
    {
        if (!purchasedStatus.status)
        {
            anim.SetBool("Pump", false);
            priceText.SetText((price.Value - paidAmount.Value).ToString());
            purchaseCollider.enabled = true;
            collectCollider.enabled = false;
            fuelAmountHolder.SetActive(false);
            priceHolder.SetActive(true);
            pumpCol.enabled = false;
        }
        else
        {
            anim.SetBool("Pump", true);
            fuelAmountText.SetText(fuelStorage.Value + "/" + fuelCapacity.Value);
            purchaseCollider.enabled = false;
            collectCollider.enabled = true;
            fuelAmountHolder.SetActive(true);
            priceHolder.SetActive(false);
            pumpCol.enabled = true;

            if (fuelStorage.Value == 0) return;
            if (active)
            {
                outline.enabled = true;
                waitTime += Time.deltaTime;
                if (waitTime >= 0.02f)
                {
                    waitTime = 0;
                    fuelStorage.Value--;
                    playerFuelResource.Value++;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            active = true;
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