using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.User;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuySalesArea : MonoBehaviour
{
    public string id;
    public TextMeshProUGUI SellAreaText;
    public Image SellAreaImage;
    public bool isSold;
    public int cost;

    private bool isCoolDown = true;
    private float coolDownSec = 1f;

    private void Start()
    {
        if (id == "SellArea0")
        {
            if (PlayerPrefs.GetFloat(id) == 0)
            {
                PlayerPrefs.SetFloat(id, 1f);
            }
        }

        isSold = PlayerPrefs.GetFloat(id) == 1f;

        SellAreaText.text = !isSold ? "$ " + cost : "Empty";
        if (!isSold) return;
        SellAreaImage.fillAmount = 1f;
    }

    private void LateUpdate()
    {
        if (!isSold) return;
        var salesAreaController = gameObject.transform.GetChild(0).GetComponent<SalesAreaController>();
        SellAreaText.text = salesAreaController.occupied ? " " : "Empty";
        SellAreaImage.color = Color.white;
        SellAreaText.color = Color.white;
    }

    private void Sale(int Cost)
    {
        if (isSold) return;
        Cost = cost;
        if (PlayerPrefs.GetInt("myCoin") >= Cost)
        {
            transform.GetChild(2).transform.DOShakeScale(0.5f, 1);
            SellAreaImage.color = Color.white;
            SellAreaText.color = Color.white;
            GameManager.instance.SpendMoney(cost);
            isSold = true;
            PlayerPrefs.SetFloat(id, 1f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isSold) return;
        if (other.gameObject.CompareTag("Player") && PlayerPrefs.GetInt("myCoin") >= cost)
        {
            SellAreaImage.gameObject.SetActive(true);
            if (!isCoolDown)
            {
                Sale(cost);
                isCoolDown = true;
            }

            if (isCoolDown)
            {
                SellAreaImage.fillAmount += 1 / coolDownSec * Time.deltaTime;
                if (SellAreaImage.fillAmount >= 1)
                {
                    //SellAreaImage.fillAmount = 0;
                    isCoolDown = false;
                }
            }
        }
    }
}