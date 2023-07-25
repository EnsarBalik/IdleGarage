using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SalesAreaController : MonoBehaviour
{
    public static SalesAreaController instance;

    public BuySalesArea BuySalesArea;

    public Transform place;
    public Transform standHere;

    public bool occupied;

    private void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        var valuableList = ValueController.instance.valuableList;
        if (valuableList.Count > 1 && !occupied && BuySalesArea.isSold)
        {
            valuableList[^1].transform.DOJump(place.position, 0.5f, 0, 0.5f);
            valuableList[^1].transform.parent = transform;
            valuableList[^1].transform.rotation = quaternion.identity;
            valuableList[^1].transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            GameObject randomProduct = valuableList[^1].transform.GetChild(1).transform.GetChild(Random.Range(1, 25))
                .gameObject;
            GameObject box = valuableList[^1].transform.GetChild(1).transform.GetChild(0).gameObject;
            box.SetActive(false);
            randomProduct.SetActive(true);
            valuableList.Remove(valuableList[^1]);
            occupied = true;
            CostumerStateManager.instance.FindProduct(this, standHere);
        }
    }
}