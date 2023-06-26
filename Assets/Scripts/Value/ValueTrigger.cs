using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class ValueTrigger : MonoBehaviour
{
    public Image fillImage;
    private bool isCoolDown = true;
    private float coolDownSec = 1f;


    private void Start()
    {
        fillImage.fillAmount = 0;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag($"Player"))
        {
            if (transform.childCount <= 0) return;
            fillImage.gameObject.SetActive(true);
            if (!isCoolDown)
            {
                ValueController.instance.StackObjet(transform.GetChild(0).gameObject, ValueController.instance.valuableList.Count - 1);
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
}