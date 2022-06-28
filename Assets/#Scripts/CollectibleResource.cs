using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CollectibleResource : MonoBehaviour
{
    public ResourceType resourceType;

    private Player player;
    private GameObject iconObj;
    public GameObject destroyParticle;

    public IntVariable playerFuelResource;
    public IntVariable playerMetalResource;

    public bool collected;
    public bool interacted;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        iconObj = transform.GetChild(0).gameObject;
        transform.DOLocalMoveY(0.5f, 1f).SetLoops(Int32.MaxValue,LoopType.Yoyo);
    }

    private void Update()
    {
        LookAtCamera();
        InteractionControl();
    }

    private void LookAtCamera()
    {
        transform.rotation = Quaternion.LookRotation(iconObj.transform.position - Camera.main.transform.position);
    }

    private void InteractionControl()
    {
        if (!interacted) return;
        if (collected) return;
        transform.DOMoveY(3f, 1f).OnComplete(() => transform.DOMove(player.transform.position + new Vector3(0,0.85f,0), 0.2f).OnComplete(() => CollectControl()));
    }

    private void CollectControl()
    {
        if (collected) return;
        collected = true;

        if (destroyParticle)
        {
            Instantiate(destroyParticle, transform.position, Quaternion.identity)
                .ParentSet(GlobalReferences.instance.debris);
        }

        switch (resourceType)
        {
            case ResourceType.FUEL: playerFuelResource.Value += 2; break;
            case ResourceType.METAL: playerMetalResource.Value += 2; break;
        }
        iconObj.SetActive(false);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interacted) return;
            interacted = true;
        }
    }
}

public enum ResourceType
{
    METAL,
    FUEL
}
