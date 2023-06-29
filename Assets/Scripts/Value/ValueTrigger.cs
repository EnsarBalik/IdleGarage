using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValueTrigger : MonoBehaviour
{
    public Image fillImage;
    private bool isCoolDown = true;
    private float coolDownSec = 1f;

    private float cargoArricalTime;
    private bool cargoCoolDown = true;
    private float cargoCoolDownSec = 10f;
    public TextMeshProUGUI cargoArriveTimeText;

    private void Start()
    {
        fillImage.fillAmount = 0;
    }

    private void Update()
    {
        CargoArrivalTime();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag($"Player"))
        {
            if (transform.childCount <= 0) return;
            fillImage.gameObject.SetActive(true);
            if (!isCoolDown)
            {
                ValueController.instance.StackObjet(transform.GetChild(0).gameObject,
                    ValueController.instance.valuableList.Count - 1);
                transform.GetChild(0).parent = ValueController.instance.transform;
                isCoolDown = true;
            }

            if (isCoolDown)
            {
                fillImage.fillAmount += 1 / coolDownSec * Time.deltaTime;
                if (fillImage.fillAmount >= 1)
                {
                    fillImage.fillAmount = 0;
                    isCoolDown = false;
                }
            }
            //StartCoroutine(ValueController.instance.StackValues(gameObject, transform.GetChild(0).gameObject, ValueController.instance.valuableList.Count - 1, 1f));
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fillImage.gameObject.SetActive(false);
        }
    }

    private void CargoArrivalTime()
    {
        if (!cargoCoolDown)
        {
            cargoCoolDown = true;
            Debug.Log("Cargo Arrived");
        }

        if (cargoCoolDown)
        {
            cargoArricalTime += 1 / cargoCoolDownSec * Time.deltaTime;
            if (cargoArricalTime >= 1)
            {
                cargoArricalTime = 0;
                cargoCoolDown = false;
            }
        }

        var test = Mathf.CeilToInt(cargoArricalTime * 10);

        cargoArriveTimeText.text = test.ToString();
    }
}