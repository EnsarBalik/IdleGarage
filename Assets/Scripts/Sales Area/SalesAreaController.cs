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
    public GameObject costumerManager;

    public Transform place;
    public Transform standHere;

    public bool occupied;

    private void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        costumerManager = GameObject.Find("/Costumer Manager");
        var valuableList = ValueController.instance.valuableList;
        if (valuableList.Count > 1 && !occupied && BuySalesArea.isSold)
        {
            if (PlayerPrefs.GetInt("gorev") < 1f)
            {
                PlayerPrefs.SetInt("gorev", 1);
                PlayerPrefs.SetInt("gorev2", 1);
                taskmanager.taskmanagersc.updatetaskselector();
            }

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
            costumerManager.GetComponent<CostumerManager>().FindSaleArea(this, standHere);
        }
    }
}