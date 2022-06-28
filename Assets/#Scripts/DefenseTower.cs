using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenseTower : MonoBehaviour
{
    public GameObject durabilityHolder;
    public GameObject priceHolder;
    public TextMeshProUGUI durabilityAmountText;
    public TextMeshProUGUI priceText;

    public Collider towerCol;
    public Material unPurchasedMat;
    private Material defaultMat;
    private MeshRenderer meshRenderer;

    [Header("Tower Properties")]
    public IntVariable durability;
    public IntVariable paidAmount;
    public IntVariable price;
    public BoolVariable purchasedStatus;

    public IntVariable towerAttackSpeed;
    public IntVariable towerDamage;
    public IntVariable towerRange;

    [Header("Player Properties")]
    public IntVariable playerGold;

    private bool active;
    private float waitTime;


    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        defaultMat = meshRenderer.material;
        InvokeRepeating("EnemyAgro",0.1f, 5f);
    }

    private void Update()
    {
        CheckPurchaseStatus();
        AttackControl();
    }

    private void AttackControl()
    {
        if (!purchasedStatus.status)
        {
            priceHolder.SetActive(true);
            durabilityHolder.SetActive(false);
            towerCol.isTrigger = true;
            meshRenderer.material = unPurchasedMat;
            priceText.SetText((price.Value - paidAmount.Value).ToString());
        }
        else
        {
            priceHolder.SetActive(false);
            durabilityHolder.SetActive(true);
            durabilityAmountText.SetText(durability.Value.ToString());
            towerCol.isTrigger = false;
            meshRenderer.material = defaultMat;
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
