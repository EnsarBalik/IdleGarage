using System;
using System.Collections;
using System.Collections.Generic;
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

    public Image fillImage;
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
    }

    private void LateUpdate()
    {
        if (!isSold) return;
        var salesAreaController = gameObject.transform.GetChild(1).GetComponent<SalesAreaController>();
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
            fillImage.gameObject.SetActive(true);
            if (!isCoolDown)
            {
                Sale(cost);
                isCoolDown = true;
                fillImage.gameObject.SetActive(false);
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