using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class FuelTank : MonoBehaviour
{
    public IntVariable fuelAmount;
    public BoolVariable purchasedStatus;
    public IntVariable price;
    public IntVariable paidAmount;

    public GameObject durabilityHolder;
    public GameObject priceHolder;
    public GameObject activateZone;
    public TextMeshProUGUI fuelAmountText;
    public TextMeshProUGUI priceText;

    public Material unPurchasedMat;
    private Material defaultMat;

    private MeshRenderer meshRenderer;
    public Collider tankCol;

    public IntVariable playerFuelResource;
    public IntVariable playerGold;
    public FloatVariable playerFillSpeed;
    private Outline outline;

    public bool active;
    private float waitTime;

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        outline = GetComponent<Outline>();
        defaultMat = meshRenderer.material;
    }

    private void Update()
    {
        CheckPurchaseStatus();
        FillManagement();
    }

    private void FillManagement()
    {
        if (!purchasedStatus.status)
        {
            activateZone.SetActive(false);
            priceHolder.SetActive(true);
            durabilityHolder.SetActive(false);
            tankCol.isTrigger = true;
            meshRenderer.material = unPurchasedMat;
            priceText.SetText((price.Value - paidAmount.Value).ToString());
        }
        else
        {
            activateZone.SetActive(true);
            priceHolder.SetActive(false);
            durabilityHolder.SetActive(true);
            fuelAmountText.SetText(fuelAmount.Value.ToString());
            tankCol.isTrigger = false;
            meshRenderer.material = defaultMat;

            if (active)
            {
                outline.enabled = true;
                waitTime += Time.deltaTime;
                if (waitTime >= playerFillSpeed.Value)
                {
                    waitTime = 0;
                    if (playerFuelResource.Value <= 0) return;
                    if (fuelAmount.Value >= 100) return;
                    fuelAmount.Value++;
                    playerFuelResource.Value--;
                }
            }
            else
            {
                outline.enabled = false;
            }
        }
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
        }
    }
}
