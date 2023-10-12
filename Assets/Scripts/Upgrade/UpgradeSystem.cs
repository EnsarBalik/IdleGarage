using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] public int speedLvl;
    [SerializeField] public int incomeLvl;
    [SerializeField] public int capacityLvl;
    
    [SerializeField] public int speedPrice;
    [SerializeField] public int incomePrice;
    [SerializeField] public int capacityPrice;
    
    public TextMeshProUGUI speed;
    public TextMeshProUGUI income;
    public TextMeshProUGUI capacity;

    private void Start()
    {
        speedLvl = PlayerPrefs.GetInt("speedLevel", speedLvl);
        incomeLvl = PlayerPrefs.GetInt("incomeLevel", incomeLvl);
        capacityLvl = PlayerPrefs.GetInt("capacityLevel", capacityLvl);
        
        speedPrice = PlayerPrefs.GetInt("speedPrice", speedPrice);
        incomePrice = PlayerPrefs.GetInt("incomePrice", incomePrice);
        capacityPrice = PlayerPrefs.GetInt("capacityPrice", capacityPrice);
        
        speed.text = $"$ {PlayerPrefs.GetInt("speedPrice", speedPrice)}";
        income.text = $"$ {PlayerPrefs.GetInt("incomePrice", incomePrice)}";
        capacity.text = $"$ {PlayerPrefs.GetInt("capacityPrice", capacityPrice)}";
        if (speedLvl >= 15)
        {
            speed.text = $"MAX LEVEL";
        }
        if (capacityLvl >= 15)
        {
            capacity.text = $"MAX LEVEL";
        }
        if (incomeLvl >= 15)
        {
            income.text = $"MAX LEVEL";
        }
    }

    public void IncreaseSpeed()
    {
        if (GameManager.instance.coin <= speedPrice) return;
        if (speedLvl < 15)
        {
            PlayerMove.instance.moveSpeed += 0.5f;
            speedLvl++;
            PlayerPrefs.SetInt("speedLevel", speedLvl);
            GameManager.instance.coin -= speedPrice;
            PlayerPrefs.SetInt("myCoin", GameManager.instance.coin);
            speedPrice += 170;
            PlayerPrefs.SetInt("speedPrice", speedPrice);
            speed.text = $"$ {speedPrice}";
        }
        else if (speedLvl >= 15)
        {
            speed.text = $"MAX LEVEL";
        }
    }

    public void IncreaseIncome()
    {
        if (GameManager.instance.coin <= incomePrice) return;
        if (incomeLvl < 15)
        {
            CostumerManager.instance.incomeRangeMin += 100;
            incomeLvl++;
            PlayerPrefs.SetInt("incomeLevel", incomeLvl);
            GameManager.instance.coin -= incomePrice;
            PlayerPrefs.SetInt("myCoin", GameManager.instance.coin);
            incomePrice += 200;
            PlayerPrefs.SetInt("incomePrice", incomePrice);
            income.text = $"$ {incomePrice}";
        }
        else if (incomeLvl >= 15)
        {
            income.text = $"MAX LEVEL";
        }
    }

    public void IncreaseCapacity()
    {
        if (GameManager.instance.coin <= capacityPrice) return;
        if (capacityLvl < 15)
        {
            ValueController.instance.carryCapacity++;
            capacityLvl++;
            PlayerPrefs.SetInt("capacityLevel", capacityLvl);
            GameManager.instance.coin -= capacityPrice;
            PlayerPrefs.SetInt("myCoin", GameManager.instance.coin);
            capacityPrice += 180;
            PlayerPrefs.SetInt("capacityPrice", capacityPrice);
            capacity.text = $"$ {capacityPrice}";
        }
        else if (capacityLvl >= 15)
        {
            capacity.text = $"MAX LEVEL";
        }
    }
}