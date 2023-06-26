using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BuyNewIsland : MonoBehaviour
{
    public string id;
    public Image fillImage;
    public GameObject island;
    public GameObject triggers;
    public bool isSold;
    public int cost;

    private bool isCoolDown = true;
    private float coolDownSec = 1f;
    private void Start()
    {
        isSold = PlayerPrefs.GetFloat(id) == 1f;
        
        fillImage.fillAmount = 0;
        if (isSold)
        {
            island.SetActive(true);
            triggers.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void Sale(int Cost)
    {
        if (isSold) return;
        if (PlayerPrefs.GetInt("myCoin") >= Cost)
        {
            GameManager.instance.SpendMoney(Cost);
            isSold = true;
            PlayerPrefs.SetFloat(id, 1f);
            fillImage.gameObject.SetActive(false);
            island.SetActive(true);
            triggers.SetActive(false);
            gameObject.SetActive(false);
            island.transform.DOShakeScale(0.5f, 10f, 10, 360f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag($"Player"))
        {
            fillImage.gameObject.SetActive(true);
            if (!isCoolDown)
            {
                Sale(cost);
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