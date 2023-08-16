using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    public Image areaImage;
    private bool isCoolDown = true;
    private float coolDownSec = 1f;
    private bool isInUpgrade;

    public GameObject upgradeUI;

    private void Start()
    {
        instance = this;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isInUpgrade) return;
        if (other.gameObject.CompareTag("Player"))
        {
            areaImage.gameObject.SetActive(true);
            if (!isCoolDown)
            {
                isInUpgrade = true;
                upgradeUI.SetActive(true);
                isCoolDown = true;
            }

            if (isCoolDown)
            {
                areaImage.fillAmount += 1 / coolDownSec * Time.deltaTime;
                if (areaImage.fillAmount >= 1)
                {
                    isCoolDown = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        areaImage.fillAmount = 0;
        isCoolDown = true;
        isInUpgrade = false;
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
    }
}