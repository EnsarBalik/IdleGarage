using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BuyNewIsland : MonoBehaviour
{
    public string id;
    public Image fillImage;
    public ParticleSystem cloud;
    public GameObject _lock;
    public GameObject triggers;

    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera lockCam;
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
            cloud.Stop();
            _lock.SetActive(false);
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
            triggers.SetActive(false);
            gameObject.SetActive(false);
            cloud.Stop();
            _lock.SetActive(false);
            playerCam.gameObject.SetActive(true);
            lockCam.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag($"Player"))
        {
            BuySpendTime();
            playerCam.gameObject.SetActive(false);
            lockCam.gameObject.SetActive(true);
        }
    }

    private void BuySpendTime()
    {
        if (PlayerPrefs.GetInt("myCoin") >= cost)
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
            fillImage.fillAmount = 0;
            playerCam.gameObject.SetActive(true);
            lockCam.gameObject.SetActive(false);
        }
    }
}