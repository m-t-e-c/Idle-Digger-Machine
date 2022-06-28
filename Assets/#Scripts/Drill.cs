using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Drill : MonoBehaviour
{

    public static Action OnEngineCooldown;

    [Header("References")]
    public List<FuelTank> fuelTanks = new List<FuelTank>();

    public GameObject rareMineral;
    public GameObject remainTimeHolder;
    public GameObject activatorHolder;
    public ParticleSystem smokeParticle;

    [Header("UI Elements")]
    public TextMeshProUGUI remainTimeText;
    public TextMeshProUGUI activationHolderText;

    [Header("Drill State")]
    public bool engineStarted;
    public bool cooldown;

    [Header("Drill Properties")]
    public IntVariable fuelUsingAmount;
    public FloatVariable diggingTime;
    public IntVariable rareMinePercent;
    public IntVariable goldPerDigging;
    public IntVariable minedGolds;
    public IntVariable cooldownTime;

    private Animator anim;
    private IntVariable currentTankFuel;
    private float remainTime;
    private float waitTime;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        remainTimeHolder.SetActive(false);
    }

    private void Update()
    {
        DrillControl();
    }

    private void DrillControl()
    {
        if (engineStarted)
        {
            remainTimeText.SetText((diggingTime.Value - remainTime).ToString());
            activatorHolder.SetActive(false);
            anim.SetBool("Drill", true);
            remainTimeHolder.SetActive(true);
            if (currentTankFuel)
            {
                if (currentTankFuel.Value <= 0)
                {
                    currentTankFuel.Value = 0;
                    engineStarted = false;
                    CheckFuelTanks();
                    return;
                }

                waitTime += Time.deltaTime;
                if (waitTime > 1f)
                {
                    waitTime = 0;
                    remainTime++;
                    int remainFuel = currentTankFuel.Value - fuelUsingAmount.Value;
                    if(remainFuel < 0)
                    {
                        currentTankFuel.Value = 0;
                    }
                    else currentTankFuel.Value -= fuelUsingAmount.Value;
                    

                    if (remainTime >= diggingTime.Value)
                    {
                        remainTime = 0;
                        remainTimeHolder.SetActive(false);
                        engineStarted = false;
                        minedGolds.Value += goldPerDigging.Value;
                        OnEngineCooldown?.Invoke();
                        StartCoroutine(CooldownStart());
                    }
                }
            }
        }
        else
        {
            anim.SetBool("Drill", false);
            activatorHolder.SetActive(true);
            smokeParticle.Stop();
        }
    }

    private IEnumerator CooldownStart()
    {
        cooldown = true;
        float time = 0;
        while(time < cooldownTime.Value)
        {
            time += Time.deltaTime;
            activationHolderText.SetText(((int)(cooldownTime.Value - time)).ToString());
            yield return null;
        }

        cooldown = false;
        activationHolderText.SetText("START THE ENGINE");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (cooldown) return;
            CheckFuelTanks();
        }
    }

    private void CheckFuelTanks()
    {
        foreach (FuelTank tank in fuelTanks)
        {
            if (tank.purchasedStatus.status)
            {
                if (tank.fuelAmount.Value > 0)
                {
                    currentTankFuel = tank.fuelAmount;
                    engineStarted = true;
                    smokeParticle.Play();
                    break;
                }
            }
        }
    }
}
